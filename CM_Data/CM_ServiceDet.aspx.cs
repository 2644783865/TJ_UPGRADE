using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_ServiceDet : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        string id = "";
        string htdh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UserID.Value = Session["UserID"].ToString();
                UserName.Value = Session["UserName"].ToString();
                if (Request.QueryString["id"] != null)
                {
                    id = Request.QueryString["id"];
                }
                if (Request.QueryString["dh"] != null)
                {
                    htdh = Request.QueryString["dh"];
                }
                if (Request.QueryString["action"] == "add")
                {
                    swry.Text = Session["UserName"].ToString();
                }
                else if (Request.QueryString["action"] == "view")
                {
                    bind_info();
                    djwj1.Visible = false;
                    djwj2.Visible = false;
                    djwj3.Visible = false;
                    djwj4.Visible = false;
                    btnLoad.Visible = false;
                    txt_htbh.Enabled = false;
                    txt_htmc.Enabled = false;
                    txt_xmmc.Enabled = false;
                    txt_gcmc.Enabled = false;
                    txt_khmc.Enabled = false;
                    txt_khfwnr.Enabled = false;
                    txt_fwry.Enabled = false;
                    yqfwsj.Disabled = true;
                    kssj.Disabled = true;
                    jssj.Disabled = true;
                    drp_jsqk.Enabled = false;
                    bz.Enabled = false;
                }
                else if (Request.QueryString["action"] == "mod")
                {
                    bind_info();
                }
                else
                {
                    bind_info();
                    djwj1.Visible = false;
                    djwj2.Visible = false;
                    djwj3.Visible = false;
                    djwj4.Visible = false;
                    txt_htbh.Enabled = false;
                    txt_htmc.Enabled = false;
                    txt_xmmc.Enabled = false;
                    txt_gcmc.Enabled = false;
                    txt_khmc.Enabled = false;
                    txt_khfwnr.Enabled = false;
                    txt_fwry.Enabled = false;
                    yqfwsj.Disabled = true;
                    kssj.Disabled = true;
                    jssj.Disabled = true;
                    drp_jsqk.Enabled = false;
                    bz.Enabled = false;
                    message.Visible = true;
                }
                this.GetQueryData("WJ_WJLX=0 AND WJ_HTBH='" + htdh + "'");
                this.GetQueryData2("WJ_WJLX=1 AND WJ_HTBH='" + htdh + "'");
                this.GetQueryData3("WJ_WJLX=2 AND WJ_HTBH='" + htdh + "'");
                this.GetQueryData4("WJ_WJLX=3 AND WJ_HTBH='" + htdh + "'");
            }
        }
        #region "客户满意度调查表数据分页"
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar(string condition)
        {
            InitPager(condition);
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager(string queryCondition)
        {
            pager.TableName = "CM_SHFWWJ";
            pager.PrimaryKey = "WJ_DH";
            pager.ShowFields = "WJ_DH,WJ_HTBH,WJ_HTMC,WJ_XMMC,WJ_GCMC,WJ_KHMC,WJ_SQR";
            pager.OrderField = "WJ_DH";
            pager.StrWhere = queryCondition;
            pager.OrderType = 0;//按时间升序序排列
            pager.PageSize = 10;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel1);
            if (NoDataPanel1.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
            foreach (GridViewRow gr in GridView1.Rows)
            {
                Label zdr = (Label)gr.FindControl("zdr");
                if (Request.QueryString["action"] == "view")
                {
                    gr.FindControl("hyperlink1").Visible = false;
                    gr.FindControl("hyperlink3").Visible = false;
                }
                else
                {
                    if (zdr.Text.Trim() == UserName.Value)
                    {
                        gr.FindControl("hyperlink1").Visible = true;
                        gr.FindControl("hyperlink3").Visible = true;
                    }
                    else
                    {
                        gr.FindControl("hyperlink1").Visible = false;
                        gr.FindControl("hyperlink3").Visible = false;
                    }
                }
            }
        }
        /// <summary>
        /// 按条件绑定数据
        /// </summary>
        /// <param name="condition"></param>
        private void GetQueryData(string condition)
        {
            this.InitVar(condition);
            this.bindGrid();
        }
        #endregion
        #region "总结报告数据分页"
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar2(string condition)
        {
            InitPager2(condition);
            UCPaging2.PageChanged += new UCPaging.PageHandler(Pager_PageChanged2);
            UCPaging2.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager2(string queryCondition)
        {
            pager.TableName = "CM_SHFWWJ";
            pager.PrimaryKey = "WJ_DH";
            pager.ShowFields = "WJ_DH as WJ_DH2,WJ_HTBH as WJ_HTBH2,WJ_HTMC as WJ_HTMC2,WJ_XMMC as WJ_XMMC2,WJ_GCMC as WJ_GCMC2,WJ_KHMC as WJ_KHMC2,WJ_SQR as WJ_SQR2";
            pager.OrderField = "WJ_DH";
            pager.StrWhere = queryCondition;
            pager.OrderType = 0;//按时间升序序排列
            pager.PageSize = 10;
        }
        void Pager_PageChanged2(int pageNumber)
        {
            bindGrid2();
        }
        private void bindGrid2()
        {
            pager.PageIndex = UCPaging2.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView2, UCPaging2, NoDataPanel2);
            if (NoDataPanel2.Visible)
            {
                UCPaging2.Visible = false;
            }
            else
            {
                UCPaging2.Visible = true;
                UCPaging2.InitPageInfo();  //分页控件中要显示的控件
            }
            foreach (GridViewRow gr in GridView2.Rows)
            {
                Label zdr2 = (Label)gr.FindControl("zdr2");
                if (Request.QueryString["action"] == "view")
                {
                    gr.FindControl("hyperlink1").Visible = false;
                    gr.FindControl("hyperlink3").Visible = false;
                }
                else
                {
                    if (zdr2.Text.Trim() == UserName.Value)
                    {
                        gr.FindControl("hyperlink4").Visible = true;
                        gr.FindControl("hyperlink6").Visible = true;
                    }
                    else
                    {
                        gr.FindControl("hyperlink4").Visible = false;
                        gr.FindControl("hyperlink6").Visible = false;
                    }
                }
            }
        }
        /// <summary>
        /// 按条件绑定数据
        /// </summary>
        /// <param name="condition"></param>
        private void GetQueryData2(string condition)
        {
            this.InitVar2(condition);
            this.bindGrid2();
        }
        #endregion
        #region "周工作报告数据分页"
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar3(string condition)
        {
            InitPager3(condition);
            UCPaging3.PageChanged += new UCPaging.PageHandler(Pager_PageChanged3);
            UCPaging3.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager3(string queryCondition)
        {
            pager.TableName = "CM_SHFWWJ";
            pager.PrimaryKey = "WJ_DH";
            pager.ShowFields = "WJ_DH as WJ_DH3,WJ_HTBH as WJ_HTBH3,WJ_HTMC as WJ_HTMC3,WJ_XMMC as WJ_XMMC3,WJ_GCMC as WJ_GCMC3,WJ_KHMC as WJ_KHMC3,WJ_SQR as WJ_SQR3";
            pager.OrderField = "WJ_DH";
            pager.StrWhere = queryCondition;
            pager.OrderType = 0;//按时间升序序排列
            pager.PageSize = 10;
        }
        void Pager_PageChanged3(int pageNumber)
        {
            bindGrid3();
        }
        private void bindGrid3()
        {
            pager.PageIndex = UCPaging3.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView3, UCPaging3, NoDataPanel3);
            if (NoDataPanel3.Visible)
            {
                UCPaging3.Visible = false;
            }
            else
            {
                UCPaging3.Visible = true;
                UCPaging3.InitPageInfo();  //分页控件中要显示的控件
            }
            foreach (GridViewRow gr in GridView3.Rows)
            {
                Label zdr3 = (Label)gr.FindControl("zdr3");
                if (Request.QueryString["action"] == "view")
                {
                    gr.FindControl("hyperlink1").Visible = false;
                    gr.FindControl("hyperlink3").Visible = false;
                }
                else
                {
                    if (zdr3.Text.Trim() == UserName.Value)
                    {
                        gr.FindControl("hyperlink7").Visible = true;
                        gr.FindControl("hyperlink9").Visible = true;
                    }
                    else
                    {
                        gr.FindControl("hyperlink7").Visible = false;
                        gr.FindControl("hyperlink9").Visible = false;
                    }
                }
            }
        }
        /// <summary>
        /// 按条件绑定数据
        /// </summary>
        /// <param name="condition"></param>
        private void GetQueryData3(string condition)
        {
            this.InitVar3(condition);
            this.bindGrid3();
        }
        #endregion
        #region "总结报告数据分页"
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar4(string condition)
        {
            InitPager4(condition);
            UCPaging4.PageChanged += new UCPaging.PageHandler(Pager_PageChanged4);
            UCPaging4.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager4(string queryCondition)
        {
            pager.TableName = "CM_SHFWWJ";
            pager.PrimaryKey = "WJ_DH";
            pager.ShowFields = "WJ_DH as WJ_DH4,WJ_HTBH as WJ_HTBH4,WJ_HTMC as WJ_HTMC4,WJ_XMMC as WJ_XMMC4,WJ_GCMC as WJ_GCMC4,WJ_KHMC as WJ_KHMC4,WJ_SQR as WJ_SQR4";
            pager.OrderField = "WJ_DH";
            pager.StrWhere = queryCondition;
            pager.OrderType = 0;//按时间升序序排列
            pager.PageSize = 10;
        }
        void Pager_PageChanged4(int pageNumber)
        {
            bindGrid4();
        }
        private void bindGrid4()
        {
            pager.PageIndex = UCPaging4.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView4, UCPaging4, NoDataPanel4);
            if (NoDataPanel4.Visible)
            {
                UCPaging4.Visible = false;
            }
            else
            {
                UCPaging4.Visible = true;
                UCPaging4.InitPageInfo();  //分页控件中要显示的控件
            }
            foreach (GridViewRow gr in GridView4.Rows)
            {
                Label zdr4 = (Label)gr.FindControl("zdr4");
                if (Request.QueryString["action"] == "view")
                {
                    gr.FindControl("hyperlink1").Visible = false;
                    gr.FindControl("hyperlink3").Visible = false;
                }
                else
                {
                    if (zdr4.Text.Trim() == UserName.Value)
                    {
                        gr.FindControl("hyperlink10").Visible = true;
                        gr.FindControl("hyperlink12").Visible = true;
                    }
                    else
                    {
                        gr.FindControl("hyperlink10").Visible = false;
                        gr.FindControl("hyperlink12").Visible = false;
                    }
                }
            }
        }
        /// <summary>
        /// 按条件绑定数据
        /// </summary>
        /// <param name="condition"></param>
        private void GetQueryData4(string condition)
        {
            this.InitVar4(condition);
            this.bindGrid4();
        }
        #endregion
        public void bind_info()
        {
            string sqltext = "select SH_DH,SH_HTBH,SH_HTMC,SH_XMMC,SH_GCMC,SH_KHMC,SH_FWR,SH_FWNR,CONVERT(varchar(100), SH_KSSJ, 23) AS SH_KSSJ,CONVERT(varchar(100), SH_JSSJ, 23) AS SH_JSSJ,SH_SWRY,CONVERT(varchar(100), SH_HTYQSJ, 23) AS SH_HTYQSJ,SH_JSQK,SH_BZ from CM_SHFW where SH_DH='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            if (dr.Read())
            {
                dh.Text = dr["SH_DH"].ToString();
                txt_htbh.Text = dr["SH_HTBH"].ToString();
                txt_htmc.Text = dr["SH_HTMC"].ToString();
                txt_xmmc.Text = dr["SH_XMMC"].ToString();
                txt_gcmc.Text = dr["SH_GCMC"].ToString();
                txt_khmc.Text = dr["SH_KHMC"].ToString();
                txt_fwry.Text = dr["SH_FWR"].ToString();
                txt_khfwnr.Text = dr["SH_FWNR"].ToString();
                kssj.Value = dr["SH_KSSJ"].ToString();
                jssj.Value = dr["SH_JSSJ"].ToString();
                swry.Text = dr["SH_SWRY"].ToString();
                drp_jsqk.SelectedIndex = Convert.ToInt32(dr["SH_JSQK"].ToString());
                bz.Text = dr["SH_BZ"].ToString();
            }
            dr.Close();
        }
        protected void btnReturn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            if (Request.QueryString["action"] == "add")
            {
                sqltext = "SELECT MAX(SH_DH) + 1 AS SHDH FROM CM_SHFW";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                int dh = Convert.ToInt32(dt.Rows[0]["SHDH"].ToString());
                sqltext = "insert into CM_SHFW(SH_DH,SH_HTBH,SH_HTMC,SH_XMMC,SH_GCMC,SH_KHMC,SH_FWR,SH_FWNR,SH_KSSJ,SH_JSSJ,SH_SWRY,SH_HTYQSJ,SH_JSQK,SH_BZ)" +
                    "Values(" + dh + ",'" + txt_htbh.Text.Trim() + "','" + txt_htmc.Text.Trim() + "','" + txt_xmmc.Text.Trim() + "','" + txt_gcmc.Text.Trim() + "','" + txt_khmc.Text.Trim() + "','" + txt_fwry.Text.Trim() + "','" + txt_khfwnr.Text.Trim() + "','" + kssj.Value.Trim() + "','" + jssj.Value.Trim() + "','" + swry.Text.Trim() + "','" + yqfwsj.Value.Trim() + "','" + drp_jsqk.SelectedIndex + "','" + bz.Text.Trim() + "')";
                DBCallCommon.ExeSqlText(sqltext);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('登记成功！');window.opener=null;window.open('','_self');window.close();", true);
            }
            else if (Request.QueryString["action"] == "mod")
            {
                sqltext = "Update CM_SHFW set SH_DH='" + dh.Text + "',SH_HTBH='" + txt_htbh.Text.Trim() + "',SH_HTMC='" + txt_htmc.Text.Trim() + "'," +
                    "SH_XMMC='" + txt_xmmc.Text.Trim() + "',SH_GCMC='" + txt_gcmc.Text.Trim() + "',SH_KHMC='" + txt_khmc.Text.Trim() + "'," +
                    "SH_FWR='" + txt_fwry.Text.Trim() + "',SH_FWNR='" + txt_khfwnr.Text.Trim() + "',SH_KSSJ='" + kssj.Value.Trim() + "'," +
                    "SH_JSSJ='" + jssj.Value.Trim() + "',SH_SWRY='" + swry.Text.Trim() + "',SH_HTYQSJ='" + yqfwsj.Value.Trim() + "',SH_JSQK= '" + drp_jsqk.SelectedIndex + "',SH_BZ='" + bz.Text.Trim() + "' where SH_DH='" + dh.Text + "'";
                DBCallCommon.ExeSqlText(sqltext);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('修改成功！');window.opener=null;window.open('','_self');window.close();", true);
            }
            else
            {
                sqltext = "Delete  FROM CM_SHFW where SH_DH='" + dh.Text + "'";
                DBCallCommon.ExeSqlText(sqltext);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！');window.opener=null;window.open('','_self');window.close();", true);
            }
        }
    }
}
