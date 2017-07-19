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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_GDGZSCSP_detail : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                asd.action = Request.QueryString["action"];
                asd.id = Request.QueryString["id"];
                asd.userid = Session["UserID"].ToString();
                asd.username = Session["UserName"].ToString();
                UCPaging1.CurrentPage = 1;
                InitVar();
                BindData();
            }
            InitVar();
        }
        private class asd
        {
            public static string userid;
            public static string id;
            public static string username;
            public static string action;
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
            if (asd.action == "add")
            {
                pager_org.TableName = "View_OM_GDGZ";
            }
            else
            {
                pager_org.TableName = "View_OM_GDGZSC_List";
            }
            pager_org.PrimaryKey = "";
            pager_org.ShowFields = "Person_GH,ST_NAME,DEP_NAME,GDGZ,ST_SEQUEN,NOTE";
            pager_org.OrderField = "DEP_NAME";
            pager_org.StrWhere = StrWhere();
            pager_org.OrderType = 1;//降序排列
            pager_org.PageSize = 50;
        }

        /// <summary>
        /// 换页事件
        /// </summary>
        private void Pager_PageChanged(int pageNumber)
        {
            BindData();
        }
        private void BindData()
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
            for (int j = 0; j < rptGDGZ.Items.Count; j++)
            {
                Label s = (Label)rptGDGZ.Items[j].FindControl("lbXuHao");
                s.Text = (j + 1 + (pager_org.PageIndex - 1) * UCPaging1.PageSize).ToString();
            }
            if (asd.action == "add")
            {
                lbZDR.Text = asd.username;
                hidZDRID.Value = asd.userid;
                lbZDR_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                txtSpbh.Text = GetSPBH();
            }
            else
            {
                string sql = "select OM_GDGZSCSP.SPBH as SPBH,SPJB,ZDR_ID,ZDR_Name,ZD_Time,ZDR_Note,SHR1_ID,SHR1_Name,SHR1_JL,SHR1_Time,SHR1_Note,isnull(SHR2_ID,'') as SHR2_ID,isnull(SHR2_Name,'') as SHR2_Name,isnull(SHR2_Time,'') as SHR2_Time,isnull(SHR2_Note,'') as SHR2_Note,ISNULL(SHR2_JL,'') as SHR2_JL from OM_GDGZSCSP left join OM_GDGZSC_List on  OM_GDGZSCSP.SPBH=OM_GDGZSC_List.SPBH where OM_GDGZSCSP.SPBH='" + asd.id + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                if (dr.Read())
                {
                    if (asd.action == "alert")
                    {
                        txtSpbh.Text = dr["SPBH"].ToString();
                        hidZDRID.Value=dr["ZDR_ID"].ToString();
                        lbZDR.Text = dr["ZDR_Name"].ToString();
                        txtZDR_JY.Text = dr["ZDR_Note"].ToString();
                        lbZDR_SJ.Text = dr["ZD_Time"].ToString();
                        txtSPR1.Text = dr["SHR1_Name"].ToString();
                        txtSPR2.Text = dr["SHR2_Name"].ToString();
                        if (dr["SPJB"].ToString() == "1")
                        {
                            if (rblSPJB.SelectedValue == "2")
                                rblSPJB.SelectedValue = "2";
                            else
                                rblSPJB.SelectedValue = "1";
                        }
                        if (dr["SPJB"].ToString() == "2")
                        {
                            if (rblSPJB.SelectedValue == "1")
                                rblSPJB.SelectedValue = "1";
                            else
                                rblSPJB.SelectedValue = "2";
                        }
                    }
                    else
                    {
                        rblSPJB.SelectedValue = dr["SPJB"].ToString();
                        txtSpbh.Text = dr["SPBH"].ToString();
                        lbZDR.Text = dr["ZDR_Name"].ToString();
                        txtZDR_JY.Text = dr["ZDR_Note"].ToString();
                        lbZDR_SJ.Text = dr["ZD_Time"].ToString();
                        hidSPR1ID.Value = dr["SHR1_ID"].ToString();
                        txtSPR1.Text = dr["SHR1_Name"].ToString();
                        rblSPR1_JL.SelectedValue = dr["SHR1_JL"].ToString();
                        lbSPR1_SJ.Text = dr["SHR1_Time"].ToString();
                        txtSPR1_JY.Text = dr["SHR1_Note"].ToString();
                        if (rblSPJB.SelectedValue == "2")
                        {
                            hidSPR2ID.Value = dr["SHR2_ID"].ToString();
                            txtSPR2.Text = dr["SHR2_Name"].ToString();
                            rblSPR2_JL.SelectedValue = dr["SHR2_JL"].ToString();
                            lbSPR2_SJ.Text = dr["SHR2_Time"].ToString();
                            txtSPR2_JY.Text = dr["SHR2_Note"].ToString();
                        }
                    }
                }
            }
            PowerControl();
        }

        //控件可见性和可用性
        private void PowerControl()
        {
            SPR1.Visible = false;
            SPR2.Visible = false;
            if (rblSPJB.SelectedValue == "1")
            {
                SPR1.Visible = true;
            }
            if (rblSPJB.SelectedValue == "2")
            {
                SPR1.Visible = true;
                SPR2.Visible = true;
            }
            if (asd.action == "add" || asd.action == "alert")
            {
                panShenhe.Enabled = true;
                panZDR.Enabled = true;
                rblSPJB.Enabled = true;
                hlSelect1.Visible = true;
                hlSelect2.Visible = true;
                btnSave.Visible = true;
            }
            if (asd.action == "check")
            {
                panShenhe.Enabled = true;
                if (asd.userid == hidSPR1ID.Value.Trim())
                {
                    rblSPR1_JL.Enabled = true;
                    txtSPR1_JY.Enabled = true;
                }
                if (asd.userid == hidSPR2ID.Value.Trim())
                {
                    rblSPR2_JL.Enabled = true;
                    txtSPR2_JY.Enabled = true;
                }
                btnSave.Visible = true;
            }
        }

        protected void rblSPJB_onchange(object sender, EventArgs e)
        {
            BindData();
        }

        //绑定审批编号
        protected string GetSPBH()
        {
            string SPBH = "";
            string sql = "select max(SPBH) as SPBH from OM_GDGZSCSP";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows[0][0].ToString() == "")
            {
                SPBH = "G-SC" + DateTime.Now.ToString("yyMMdd") + "-0001";
            }
            else
            {
                SPBH = "G-SC" + DateTime.Now.ToString("yyMMdd") + "-" + (CommonFun.ComTryInt(dt.Rows[0][0].ToString().Split('-')[2]) + 1).ToString().PadLeft(4, '0');
            }
            return SPBH;
        }

        private string StrWhere()
        {
            string strWhere = "ST_ID in (" + asd.id.Substring(0, asd.id.Length - 1) + ")";
            if (asd.action != "add")
            {
                strWhere = "SPBH='" + asd.id + "'";
            }
            return strWhere;
        }

        protected int SaveValid()
        {
            int a = 0;
            if (asd.userid == hidZDRID.Value.Trim())
            {
                if (rblSPJB.SelectedValue == "")
                {
                    a = 1;
                }
                else if ((rblSPJB.SelectedValue == "1" && txtSPR1.Text == "") || (rblSPJB.SelectedValue == "1" && hidSPR1ID.Value == ""))
                {
                    a = 1;
                }
                else if (rblSPJB.SelectedValue == "2")
                {
                    if (txtSPR1.Text == "" || hidSPR1ID.Value == "" || txtSPR2.Text == "" || hidSPR2ID.Value == "")
                    {
                        a = 1;
                    }
                }
            }
            else if (rblSPJB.SelectedValue == "1")
            {
                if ((asd.userid == hidSPR1ID.Value.Trim() && rblSPR1_JL.SelectedValue == ""))
                {
                    a = 2;
                }
            }
            else if (rblSPJB.SelectedValue == "2")
            {
                if ((asd.userid == hidSPR1ID.Value.Trim() && rblSPR1_JL.SelectedValue == "") || (asd.userid == hidSPR2ID.Value.Trim() && rblSPR2_JL.SelectedValue == ""))
                {
                    a = 2;
                }
            }
            return a;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int a = SaveValid();
            if (a == 1)
            {
                Response.Write("<script>alert('请选择审批人后再提交审批！');</script>");
                return;
            }
            if (a == 2)
            {
                Response.Write("<script>alert('请选择同意或不同意！');</script>");
                return;
            }
            string sqlText = "";
            List<string> list = new List<string>();
            if (asd.action == "add")
            {
                if (rblSPJB.SelectedValue == "1")
                {
                    sqlText = "insert into OM_GDGZSCSP(SPBH,SC_Num,SPJB,ZDR_ID,ZDR_Name,ZDR_Note,ZD_Time,SHR1_ID,SHR1_Name,SPZT) values('" + txtSpbh.Text.Trim() + "','" + rptGDGZ.Items.Count.ToString() + "','" + rblSPJB.SelectedValue.Trim() + "','" + hidZDRID.Value.Trim() + "','" + lbZDR.Text.Trim() + "', '" + txtZDR_JY.Text.Trim() + "','" + lbZDR_SJ.Text.Trim() + "','" + hidSPR1ID.Value.Trim() + "','" + txtSPR1.Text.Trim() + "','0')";
                }
                else
                {
                    sqlText = "insert into OM_GDGZSCSP(SPBH,SC_Num,SPJB,ZDR_ID,ZDR_Name,ZDR_Note,ZD_Time,SHR1_ID,SHR1_Name,SHR2_ID,SHR2_Name,SPZT) values('" + txtSpbh.Text.Trim() + "','" + rptGDGZ.Items.Count.ToString() + "','" + rblSPJB.SelectedValue.Trim() + "','" + hidZDRID.Value.Trim() + "','" + lbZDR.Text.Trim() + "', '" + txtZDR_JY.Text.Trim() + "','" + lbZDR_SJ.Text.Trim() + "','" + hidSPR1ID.Value.Trim() + "','" + txtSPR1.Text.Trim() + "','" + hidSPR2ID.Value.Trim() + "','" + txtSPR2.Text.Trim() + "','0')";
                }
                list.Add(sqlText);
                string sql = "insert into OM_GDGZSC_List(Person_GH,GDGZ,tzedu,DATE,ST_ID,NOTE,XGRST_ID,XGRST_NAME,XGTIME) select Person_GH,GDGZ,tzedu,DATE,ST_ID,NOTE,XGRST_ID,XGRST_NAME,XGTIME from OM_GDGZ where ST_ID in (" + asd.id.Substring(0, asd.id.Length - 1) + ")";
                list.Add(sql);
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                    string sql1 = "update OM_GDGZSC_List set SPBH= '" + txtSpbh.Text.Trim() + "',SPZT='0' where SPBH is NULL and ST_ID in (" + asd.id.Substring(0, asd.id.Length - 1) + ")";
                    try
                    {
                        DBCallCommon.ExeSqlText(sql1);
                        Response.Write("<script>alert('保存成功！');</script>");
                        rblSPJB.Enabled = false;
                        panZDR.Enabled = false;
                        btnSave.Visible = false;
                        btnSubmit.Visible = true;
                    }
                    catch
                    {
                        Response.Write("<script>alert('数据操作失败，请联系管理员！');</script>");
                        return;
                    }
                }
                catch
                {
                    Response.Write("<script>alert('数据操作错误，请联系管理员！');</script>");
                    return;
                }
            }
            else
            {
                if (asd.action == "alert")
                {
                    if (rblSPJB.SelectedValue == "2")
                    {
                        lbZDR_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        sqlText = "update OM_GDGZSCSP set SPJB='" + rblSPJB.SelectedValue.Trim() + "', ZDR_Note='" + txtZDR_JY.Text.Trim() + "',ZD_Time='" + lbZDR_SJ.Text.Trim() + "',SHR1_ID='" + hidSPR1ID.Value.Trim() + "',SHR1_Name='" + txtSPR1.Text.Trim() + "',SHR2_ID='" + hidSPR2ID.Value.Trim() + "',SHR2_Name='" + txtSPR2.Text.Trim() + "' where SPBH='" + txtSpbh.Text.Trim() + "'";
                    }
                    else
                    {
                        lbZDR_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        sqlText = "update OM_GDGZSCSP set SPJB='" + rblSPJB.SelectedValue.Trim() + "', ZDR_Note='" + txtZDR_JY.Text.Trim() + "',ZD_Time='" + lbZDR_SJ.Text.Trim() + "',SHR1_ID='" + hidSPR1ID.Value.Trim() + "',SHR1_Name='" + txtSPR1.Text.Trim() + "',SHR2_ID=NULL,SHR2_Name=NULL where SPBH='" + txtSpbh.Text.Trim() + "'";
                    }
                }
                if (asd.action == "check")
                {
                    if (rblSPJB.SelectedValue == "1")
                    {
                        lbSPR1_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        sqlText = "update OM_GDGZSCSP set SHR1_Time='" + lbSPR1_SJ.Text.Trim() + "',SHR1_JL='" + rblSPR1_JL.Text.Trim() + "',SHR1_Note='" + txtSPR1_JY.Text.Trim() + "' where SPBH='" + txtSpbh.Text.Trim() + "'";
                    }
                    else
                    {
                        if (asd.userid == hidSPR1ID.Value.Trim())
                        {
                            lbSPR1_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            sqlText = "update OM_GDGZSCSP set SHR1_Time='" + lbSPR1_SJ.Text.Trim() + "',SHR1_JL='" + rblSPR1_JL.Text.Trim() + "',SHR1_Note='" + txtSPR1_JY.Text.Trim() + "' where SPBH='" + txtSpbh.Text.Trim() + "'";
                        }
                        else
                        {
                            lbSPR2_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            sqlText = "update OM_GDGZSCSP set SHR2_Time='" + lbSPR2_SJ.Text.Trim() + "',SHR2_JL='" + rblSPR2_JL.Text.Trim() + "',SHR2_Note='" + txtSPR2_JY.Text.Trim() + "' where SPBH='" + txtSpbh.Text.Trim() + "'";
                        }
                    }
                }
                try
                {
                    DBCallCommon.ExeSqlText(sqlText);
                    Response.Write("<script>alert('保存成功！');</script>");
                    rblSPJB.Enabled = false;
                    panZDR.Enabled = false;
                    btnSave.Visible = false;
                    btnSubmit.Visible = true;
                }
                catch
                {
                    Response.Write("<script>alert('数据操作错误，请联系管理员！');</script>");
                }
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            string sqlText = "";
            string sql = "";
            string sqlupdateykgw = "";
            if (asd.action == "add" || asd.action == "alert")
            {
                sqlText = "update OM_GDGZSCSP set SPZT='1'where SPBH='" + txtSpbh.Text.Trim() + "'";
                list.Add(sqlText);
                sql = "update OM_GDGZSC_List set SPZT='1'where SPBH='" + txtSpbh.Text.Trim() + "'";
                list.Add(sql);

                //邮件提醒
                string sprid = "";
                string sptitle = "";
                string spcontent = "";
                sprid = hidSPR1ID.Value.Trim();
                sptitle = "岗位工资删除审批";
                spcontent = "编号" + txtSpbh.Text.Trim() + "的岗位工资删除需要您审批，请登录查看！";
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                try
                {
                    DBCallCommon.ExecuteTrans(list);
                    if (asd.action == "add")
                        Response.Write("<script>window.close()</script>");
                    else
                        Response.Redirect("OM_GDGZSCSP.aspx");
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交审批出现问题，请联系管理员！');", true);
                    return;
                }
            }
            if (asd.action == "check")
            {
                if (rblSPR1_JL.SelectedValue == "n" || rblSPR2_JL.SelectedValue == "n")
                {
                    sqlText = "update OM_GDGZSCSP set SPZT='4' where SPBH='" + txtSpbh.Text.Trim() + "'";
                    list.Add(sqlText);
                    sql = "update OM_GDGZSC_List set SPZT='4' where SPBH='" + txtSpbh.Text.Trim() + "'";
                    list.Add(sqlText);
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        Response.Redirect("OM_GDGZSCSP.aspx");
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交审批出现问题，请联系管理员！');", true);
                        return;
                    }
                }
                else
                {
                    if (rblSPJB.SelectedValue == "2")
                    {
                        if (asd.userid == hidSPR1ID.Value.Trim() && rblSPR1_JL.SelectedValue == "y")
                        {
                            sqlText = "update OM_GDGZSCSP set SPZT='2'where SPBH='" + txtSpbh.Text.Trim() + "'";
                            list.Add(sqlText);
                            sql = "update OM_GDGZSC_List set SPZT='2'where SPBH='" + txtSpbh.Text.Trim() + "'";
                            list.Add(sql);

                            //邮件提醒
                            string sprid = "";
                            string sptitle = "";
                            string spcontent = "";
                            sprid = hidSPR2ID.Value.Trim();
                            sptitle = "岗位工资删除审批";
                            spcontent = "编号" + txtSpbh.Text.Trim() + "的岗位工资删除需要您审批，请登录查看！";
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);
                        }
                        else if (asd.userid == hidSPR2ID.Value.Trim() && rblSPR2_JL.SelectedValue == "y")
                        {
                            sqlText = "update OM_GDGZSCSP set SPZT='3'where SPBH='" + txtSpbh.Text.Trim() + "'";
                            list.Add(sqlText);
                            sql = "update OM_GDGZSC_List set SPZT='3'where SPBH='" + txtSpbh.Text.Trim() + "'";
                            list.Add(sql);
                            string sql0 = "select ST_ID,GDGZ from OM_GDGZSC_List where SPBH='" + txtSpbh.Text.Trim() + "'";
                            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sql0);
                            string stid0 = "";
                            for (int j = 0; j < dt0.Rows.Count; j++)
                            {
                                sqlupdateykgw = "update OM_SALARYBASEDATA set YKGW_BASEDATANEW=0 where ST_ID='" + dt0.Rows[j][0].ToString() + "'";
                                list.Add(sqlupdateykgw);
                                stid0 += dt0.Rows[j][0].ToString() + ',';
                            }
                            string sql1 = "delete from OM_GDGZ where ST_ID in (" + stid0.Substring(0, stid0.Length - 1) + ")";
                            list.Add(sql1);
                        }
                    }
                    else if (rblSPJB.SelectedValue == "1")
                    {
                        if (asd.userid == hidSPR1ID.Value.Trim() && rblSPR1_JL.SelectedValue == "y")
                        {
                            sqlText = "update OM_GDGZSCSP set SPZT='3'where SPBH='" + txtSpbh.Text.Trim() + "'";
                            list.Add(sqlText);
                            sql = "update OM_GDGZSC_List set SPZT='3'where SPBH='" + txtSpbh.Text.Trim() + "'";
                            list.Add(sql);
                            string sql0 = "select ST_ID,GDGZ from OM_GDGZSC_List where SPBH='" + txtSpbh.Text.Trim() + "'";
                            DataTable dt0 = DBCallCommon.GetDTUsingSqlText(sql0);
                            string stid0 = "";
                            for (int j = 0; j < dt0.Rows.Count; j++)
                            {
                                sqlupdateykgw = "update OM_SALARYBASEDATA set YKGW_BASEDATANEW=0 where ST_ID='" + dt0.Rows[j][0].ToString() + "'";
                                list.Add(sqlupdateykgw);
                                stid0 += dt0.Rows[j][0].ToString() + ',';
                            }
                            string sql1 = "delete from OM_GDGZ where ST_ID in (" + stid0.Substring(0, stid0.Length - 1) + ")";
                            list.Add(sql1);
                        }
                    }
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                        Response.Redirect("OM_GDGZSCSP.aspx");
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交审批出现问题，请联系管理员！');", true);
                        return;
                    }
                }
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            if (asd.action == "add"||asd.action=="read")
                Response.Write("<script>window.close()</script>");
            else
                Response.Redirect("OM_GDGZSCSP.aspx");
        }
    }
}
