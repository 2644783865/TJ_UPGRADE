using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Microsoft.Office.Interop.Excel;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class Sta_detail : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "员工基本信息管理";
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            deletebt.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            deletebt.Click += new EventHandler(deletebt_Click);
            if (!IsPostBack)
            {

                GetddlZzmm();
                GetddlHkType();
                GetSequce();
                GetHetongZt();
                GetDepartment();//绑定部门下拉框
                GetXueLi();//学历下拉框
                GetSele();//绑定筛选信息
                databind();//人员信息表绑定
                Account();//数据统计信息绑定
                //Age_change();
                AgeUpdate();
                GetWarning();
            }
            CheckUser(ControlFinder);
        }



        private void GetddlZzmm()
        {
            string sqlText = "select distinct ST_POLITICAL from TBDS_STAFFINFO where ST_POLITICAL is not null and ST_POLITICAL<>''";//distinct查询结果只保留一种，过滤掉重复内容。
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlZzmm.DataSource = dt;
            ddlZzmm.DataTextField = "ST_POLITICAL";
            ddlZzmm.DataValueField = "ST_POLITICAL";
            ddlZzmm.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddlZzmm.Items.Insert(0, item);
            ddlZzmm.SelectedValue = "00";
        }

        private void GetHetongZt()
        {
            string sqlText = "select distinct ST_CONTR from TBDS_STAFFINFO where ST_CONTR is not null and ST_CONTR<>''";//distinct查询结果只保留一种，过滤掉重复内容。
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlHetongZt.DataSource = dt;
            ddlHetongZt.DataTextField = "ST_CONTR";
            ddlHetongZt.DataValueField = "ST_CONTR";
            ddlHetongZt.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddlHetongZt.Items.Insert(0, item);
            ddlHetongZt.SelectedValue = "00";
        }

        private void GetddlHkType()
        {
            string sqlText = "select distinct ST_REGIST from TBDS_STAFFINFO where ST_REGIST is not null and ST_CONTR<>''";//distinct查询结果只保留一种，过滤掉重复内容。
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlHkType.DataSource = dt;
            ddlHkType.DataTextField = "ST_REGIST";
            ddlHkType.DataValueField = "ST_REGIST";
            ddlHkType.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddlHkType.Items.Insert(0, item);
            ddlHkType.SelectedValue = "00";
        }

        private void GetSequce()
        {
            string sqlText = "select distinct ST_SEQUEN from TBDS_STAFFINFO where ST_SEQUEN is not null and ST_SEQUEN<>''";//distinct查询结果只保留一种，过滤掉重复内容。
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlSequce.DataSource = dt;
            ddlSequce.DataTextField = "ST_SEQUEN";
            ddlSequce.DataValueField = "ST_SEQUEN";
            ddlSequce.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddlSequce.Items.Insert(0, item);
            ddlSequce.SelectedValue = "00";
        }

        private void GetWarning()
        {
            string warningDate = DateTime.Now.AddMonths(2).ToString("yyyy-MM-dd");
            string sql = "select count(1) from TBDS_STAFFINFO where ST_CONTREND<='" + warningDate + "' and ST_PD='0' and (ST_CONTREND is not null and ST_CONTREND<>'' )";
            System.Data.DataTable dtWarn = DBCallCommon.GetDTUsingSqlText(sql);
            if (dtWarn.Rows.Count > 0)
            {
                lblConWarn.Text = "(" + dtWarn.Rows[0][0].ToString() + ")";
            }
        }

        protected void ddlIfzaizhi_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> list_state = new List<string>();
            string sqlstaff = "";
            string sqlrecord = "";
            foreach (GridViewRow gr in SmartGridView1.Rows)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)gr.FindControl("checkboxstaff");
                System.Web.UI.WebControls.Label lb = (System.Web.UI.WebControls.Label)gr.FindControl("ST_ID");
                System.Web.UI.WebControls.Label lb_name = (System.Web.UI.WebControls.Label)gr.FindControl("ST_NAME");
                if (cbx.Checked == true)
                {
                    if (ddlIfzaizhi.SelectedValue != "3")
                    {
                        sqlstaff = "update TBDS_STAFFINFO set ST_PD='" + ddlIfzaizhi.SelectedValue.ToString() + "'where ST_ID='" + lb.Text.ToString() + "'";
                        list_state.Add(sqlstaff);
                        sqlrecord = "insert into TBDS_STAFFINFO_EditRecord (bSTID, bSTNAME, Type, EditTime, EditPerId, EditPerName, Caozuo) values ('0','" + lb_name.Text.Trim() + "','人员信息表','" + DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss") + "','" + Session["UserId"].ToString() + "','" + Session["UserName"].ToString() + "','修改_在职状态')";
                        list_state.Add(sqlrecord);
                    }
                }
            }
            if (list_state.Count > 0)
            {
                try
                {
                    DBCallCommon.ExecuteTrans(list_state);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('成功修改员工在职状态！');", true);
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('修改失败！');", true);
                    return;
                }
            }
            UCPaging1.CurrentPage = 1;
            databind();
        }
        #region ==================================================查询===========================================================
        protected void DDlpartment_SelectedIndexChanged(object sender, EventArgs e)
        {
            Account();
            UCPaging1.CurrentPage = 1;
            databind();
        }

        protected void rblIfZaizhi_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Account();
            UCPaging1.CurrentPage = 1;
            databind();
        }
        protected void DDlxueli_SelectedIndexChanged(object sender, EventArgs e)
        {
            Account();
            UCPaging1.CurrentPage = 1;
            databind();
        }
        #endregion

        #region 初始化分页
        void Pager_PageChanged(int pageNumber)
        {
            databind();
        }

        private void databind()
        {
            SmartGridView1.DataSource = null;
            SmartGridView1.DataBind();
            pager.TableName = "View_TBDS_STAFFINFO";
            pager.PrimaryKey = "ST_ID";
            pager.ShowFields = "*";
            if (rblIfZaizhi.SelectedValue == "1")
            {
                pager.OrderField = "ST_LZSJ";
                pager.OrderType = 1;
            }
            else
            {
                pager.OrderField = "ST_POSITION,ST_ID";
                pager.OrderType = 0;
            }
            pager.StrWhere = StrWhere();
          //  pager.OrderType = 0;
            pager.PageSize = int.Parse(ddl_pageno.SelectedValue);
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, SmartGridView1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;//如果筛选结果没有，则UCPaging不显示
                deletebt.Visible = false;//如果筛选结果没有，则删除按钮不显示
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
                deletebt.Visible = true;
            }
            string sql = "select ST_ID from View_TBDS_STAFFINFO where " + StrWhere();
            lb_People.Text = DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString();//多少行就有多少人
        }

        private string StrWhere()
        {
            string strWhere = " 0=0 ";
            if (rblIfZaizhi.SelectedValue != "3")
            {
                strWhere += " and ST_PD='" + rblIfZaizhi.SelectedValue.ToString() + "'";
            }
            else
            {
                strWhere += " and ST_PD in ('0','2')";
            }
            string str = DDlpartment.SelectedValue;
            string str1 = DDlxueli.SelectedValue;
            if (str != "00")
            {
                //strWhere += "and ST_PD=0 AND ST_DEPID ='" + str + "'";
                strWhere += " and ST_DEPID ='" + str + "'";
            }
            if (str1 != "00")
            {
                strWhere += " and ST_XUELI ='" + str1 + "' ";
            }
            if (name.Text.ToString().Trim() != "")
            {
                strWhere += " and  ST_NAME ='" + name.Text.ToString().Trim() + "'";
            }
            if (ddlSequce.SelectedValue != "00")
            {
                strWhere += " and  ST_SEQUEN ='" + ddlSequce.SelectedValue + "'";
            }
            if (ddlHkType.SelectedValue != "00")
            {
                strWhere += " and  ST_REGIST ='" + ddlHkType.SelectedValue + "'";
            }
            if (ddlHetongZt.SelectedValue != "00")
            {
                strWhere += " and  ST_CONTR ='" + ddlHetongZt.SelectedValue + "'";
            }
            if (ddlZzmm.SelectedValue != "00")
            {
                strWhere += " and  ST_POLITICAL ='" + ddlZzmm.SelectedValue + "'";
            }
            if (ddlZcdj.SelectedValue != "")
            {
                strWhere += " and  ST_ZHICHDJ ='" + ddlZcdj.SelectedValue + "'";
            }
            if (ddlZjdj.SelectedValue != "")
            {
                strWhere += " and  ST_ZHIJIDJ ='" + ddlZjdj.SelectedValue + "'";
            }
            if (screen1.SelectedValue != "0" || screen2.SelectedValue != "0" || screen3.SelectedValue != "0" || screen4.SelectedValue != "0" || screen5.SelectedValue != "0" || screen6.SelectedValue != "0" || screen7.SelectedValue != "0" || screen8.SelectedValue != "0" || screen9.SelectedValue != "0" || screen10.SelectedValue != "0")
            {
                if (SelectStr(screen1, ddlRelation1, Txt1.Text, "", khz1, khy1) != "")
                {
                    strWhere += " and (" + SelectStr(screen1, ddlRelation1, Txt1.Text, "", khz1, khy1);
                }
                else
                {
                    strWhere += " and (1=1 ";
                }
                strWhere += SelectStr(screen2, ddlRelation2, Txt2.Text, ddlLogic1.SelectedValue, khz2, khy2);
                strWhere += SelectStr(screen3, ddlRelation3, Txt3.Text, ddlLogic2.SelectedValue, khz3, khy3);
                strWhere += SelectStr(screen4, ddlRelation4, Txt4.Text, ddlLogic3.SelectedValue, khz4, khy4);
                strWhere += SelectStr(screen5, ddlRelation5, Txt5.Text, ddlLogic4.SelectedValue, khz5, khy5);
                strWhere += SelectStr(screen6, ddlRelation6, Txt6.Text, ddlLogic5.SelectedValue, khz6, khy6);
                strWhere += SelectStr(screen7, ddlRelation7, Txt7.Text, ddlLogic6.SelectedValue, khz7, khy7);
                strWhere += SelectStr(screen8, ddlRelation8, Txt8.Text, ddlLogic7.SelectedValue, khz8, khy8);
                strWhere += SelectStr(screen9, ddlRelation9, Txt9.Text, ddlLogic8.SelectedValue, khz9, khy9);
                strWhere += SelectStr(screen10, ddlRelation10, Txt10.Text, ddlLogic9.SelectedValue, khz10, khy10);
                strWhere += ")";
            }
            if (cbxConWarn.Checked == true)
            {
                string warningDate = DateTime.Now.AddMonths(2).ToString("yyyy-MM-dd");
                strWhere += " and  ST_CONTREND<='" + warningDate + "' and ST_CONTREND is not null and ST_CONTREND<>''";
            }
            if (txtLZSJS.Text.Trim() != "")
                strWhere += " and  isnull(ST_LZSJ,'')>='" + txtLZSJS.Text.Trim() + "'";
            if (txtLZSJE.Text.Trim() != "")
                strWhere += " and  isnull(ST_LZSJ,'')<='" + txtLZSJE.Text.Trim() + "'";
            return strWhere;
        }

        private string SelectStr(DropDownList ddl, DropDownList ddl1, string txt, string logic, DropDownList zuo, DropDownList you) //选择条件拼接字符串
        {
            string sqlstr = string.Empty;
            //string sql = "select DEP_CODE from TBDS_DEPINFO where DEP_NAME='" + txt + "'";
            //System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            //if (dt.Rows.Count > 0)
            //{
            //    txt = dt.Rows[0]["DEP_CODE"].ToString();
            //}
            if (ddl.SelectedValue != "0")
            {
                switch (ddl1.SelectedValue)
                {
                    case "0":
                        sqlstr = string.Format(" {0} {3} {1} like '%{2}%' {4} ", logic, ddl.SelectedValue, txt, zuo.SelectedValue, you.SelectedValue);
                        break;
                    case "1":
                        sqlstr = string.Format("{0} {3}  {1} not like '%{2}%' {4} ", logic, ddl.SelectedValue, txt, zuo.SelectedValue, you.SelectedValue);
                        break;
                    case "2":
                        sqlstr = string.Format("{0} {3}  {1}='{2}' {4} ", logic, ddl.SelectedValue, txt, zuo.SelectedValue, you.SelectedValue);
                        break;
                    case "3":
                        sqlstr = string.Format(" {0} {3} {1}!='{2}' {4} ", logic, ddl.SelectedValue, txt, zuo.SelectedValue, you.SelectedValue);
                        break;
                    case "4":
                        sqlstr = string.Format("{0} {3}  {1}>'{2}' {4} ", logic, ddl.SelectedValue, txt, zuo.SelectedValue, you.SelectedValue);
                        break;
                    case "5":
                        sqlstr = string.Format(" {0} {3} {1}>='{2}' {4} ", logic, ddl.SelectedValue, txt, zuo.SelectedValue, you.SelectedValue);
                        break;
                    case "6":
                        sqlstr = string.Format("{0} {3}  {1}<'{2}'  {4}", logic, ddl.SelectedValue, txt, zuo.SelectedValue, you.SelectedValue);
                        break;
                    case "7":
                        sqlstr = string.Format("{0} {3}  {1}<='{2}' {4} ", logic, ddl.SelectedValue, txt, zuo.SelectedValue, you.SelectedValue);
                        break;
                }
            }
            return sqlstr;
        }

        #endregion

        /// <summary>
        /// 年龄更新
        /// </summary>
        private void AgeUpdate()
        {
            string sql = "EXECUTE dbo.Pro_OM_AgeUpdate";
            try
            {
                DBCallCommon.ExeSqlText(sql);
            }
            catch (Exception e)
            {                
                throw e;
            }
            
        }
        /// <summary>
        /// 年龄更新
        /// </summary>
        //private void Age_change()
        //{
        //    string time_now = DateTime.Today.ToString("yyyy-MM-dd");
        //    string time_now_year = time_now.Substring(0, 4);
        //    time_now = time_now.Substring(time_now.Length - 6);
        //    string sql_age = "select ST_BIRTHDAY,ST_NAME from TBDS_STAFFINFO where ST_BIRTHDAY like '%" + time_now + "'";
        //    System.Data.DataTable dt_sql_age = DBCallCommon.GetDTUsingSqlText(sql_age);
        //    if (dt_sql_age.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt_sql_age.Rows.Count; i++)
        //        {
        //            string age_sql = dt_sql_age.Rows[i]["ST_BIRTHDAY"].ToString();
        //            string sql_name = dt_sql_age.Rows[i]["ST_NAME"].ToString();
        //            string age_sql_year = age_sql.Substring(0, 4);
        //            int age_sql_now = Convert.ToInt32(time_now_year) - Convert.ToInt32(age_sql_year);
        //            string sql_update_age = "update TBDS_STAFFINFO set ST_AGE='" + age_sql_now + "' where ST_BIRTHDAY like '%" + time_now + "' and ST_NAME='" + sql_name + "'";
        //            DBCallCommon.ExeSqlText(sql_update_age);
        //        }
        //    }
        //}

        private void GetDepartment()//绑定部门
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";//distinct查询结果只保留一种，过滤掉重复内容。
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            DDlpartment.DataSource = dt;
            DDlpartment.DataTextField = "DEP_NAME";
            DDlpartment.DataValueField = "DEP_CODE";
            DDlpartment.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            DDlpartment.Items.Insert(0, item);
            DDlpartment.SelectedValue = "00";
        }

        private void GetXueLi()//绑定学历
        {
            string sqlText = "select XL_ID,XL_NAME from TBDS_XUELI order by cast(XL_ID as int) desc";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            DDlxueli.DataTextField = "XL_NAME";
            DDlxueli.DataValueField = "XL_ID";
            DDlxueli.DataSource = dt;

            DDlxueli.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            DDlxueli.Items.Insert(0, item);
            DDlxueli.SelectedValue = "00";
        }

        private void GetSele()//绑定筛选信息
        {
            string sqlText = "select * from VIEW_TBDS_SELE";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            foreach (Control item in select.Controls)
            {
                if (item is DropDownList)
                {
                    if (item.ID.Contains("screen"))
                    {
                        ((DropDownList)item).DataSource = dt;
                        ((DropDownList)item).DataTextField = "name";
                        ((DropDownList)item).DataValueField = "id";
                        ((DropDownList)item).DataBind();
                        ((DropDownList)item).SelectedValue = "0";
                    }
                }
            }
        }

        private void Account()//统计信息
        {
            string sql = "select DEP_NAME,ST_DEPID,count(DEP_NAME) as ST_TOTAL from View_TBDS_STAFFINFO where " + StrWhere() + " group by DEP_NAME,ST_DEPID order by ST_DEPID";
            rep_TJ1.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            rep_TJ1.DataBind();
            string totalNum = DBCallCommon.GetDTUsingSqlText("select count(*) from View_TBDS_STAFFINFO where " + StrWhere()).Rows[0][0].ToString();
            lb_Total.Text = totalNum;
            sql = "select ST_XUELINM ,count(ST_XUELINM) as ST_NUM,'" + totalNum + "' as ST_TOTAL from View_TBDS_STAFFINFO where  " + StrWhere() + " group by ST_XUELINM,ST_XUELI order by cast(ST_XUELI as int) desc";
            rep_TJ2.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            rep_TJ2.DataBind();
            lb_Total1.Text = totalNum;
            lb_Prop1.Text = string.Format("{0:f2}", float.Parse(lb_Total1.Text) / float.Parse(lb_Total.Text) * 100).ToString() + "%";

            sql = "select case when ST_ZHICHDJ='5' then '正高级职称' when  ST_ZHICHDJ='4' then '副高级职称'  when   ST_ZHICHDJ='2' then '中级职称' when  ST_ZHICHDJ='1' then '初级职称' else '' end as ST_ZHICHNM,ST_ZHICHDJ,count(ST_ZHICHDJ) as ST_NUM,'" + totalNum + "' as ST_TOTAL from View_TBDS_STAFFINFO where ST_ZHICHDJ!=''  and ST_ZHICHDJ is not null and " + StrWhere() + " group by ST_ZHICHDJ order by ST_ZHICHDJ desc";
            System.Data.DataTable dtZC = DBCallCommon.GetDTUsingSqlText(sql);
            sql = "select case when ST_ZHIJIDJ='5' then '高级技师' when  ST_ZHIJIDJ='4' then '技师'  when  ST_ZHIJIDJ='3' then '高级技能' when   ST_ZHIJIDJ='2' then '中级技能' when  ST_ZHIJIDJ='1' then '初级技能' else '' end as ST_ZHICHNM, ST_ZHIJIDJ as ST_ZHICHDJ,count(ST_ZHIJIDJ) as ST_NUM,'" + totalNum + "' as ST_TOTAL  from View_TBDS_STAFFINFO where ST_ZHIJIDJ!=''  and ST_ZHIJIDJ is not null and " + StrWhere() + "  group by ST_ZHIJIDJ order by  ST_ZHIJIDJ desc";
            System.Data.DataTable dtZJ = DBCallCommon.GetDTUsingSqlText(sql);
            sql = "select '无' as ST_ZHICHNM,count(*) as ST_NUM,'" + totalNum + "' as ST_TOTAL  from View_TBDS_STAFFINFO where (ST_ZHIJIDJ='' or ST_ZHIJIDJ is null) and (ST_ZHICHDJ='' or ST_ZHICHDJ is null) and " + StrWhere();
            System.Data.DataTable dtWU = DBCallCommon.GetDTUsingSqlText(sql);
            dtZC.Merge(dtZJ);
            dtZC.Merge(dtWU);
            rep_TJ3.DataSource = dtZC;
            rep_TJ3.DataBind();
            lb_Total2.Text = totalNum;
            lb_Prop2.Text = string.Format("{0:f2}", float.Parse(lb_Total2.Text) / float.Parse(lb_Total.Text) * 100).ToString() + "%";

            sql = "SELECT ST_AREA, ST_NUM,(SELECT  COUNT(*)  FROM  dbo.View_TBDS_STAFFINFO where " + StrWhere() + ") AS ST_TOTAL FROM  ( SELECT     '55<=X' AS ST_AREA, COUNT(*) AS ST_NUM FROM dbo.View_TBDS_STAFFINFO  WHERE  (ST_AGE >= 55 and " + StrWhere() + ") UNION ALL SELECT  '45<=X<55' AS ST_AREA, COUNT(*) AS ST_NUM FROM   dbo.View_TBDS_STAFFINFO  WHERE  (ST_AGE >= 45 AND ST_AGE< 55 and " + StrWhere() + ") UNION ALL  SELECT  '35<=X<45' AS ST_AREA, COUNT(*) AS ST_NUM  FROM   dbo.View_TBDS_STAFFINFO  WHERE (ST_AGE >= 35 AND ST_AGE< 45 and  " + StrWhere() + ") UNION ALL SELECT     '25<=X<35' AS ST_AREA, COUNT(*) AS ST_NUM  FROM   dbo.View_TBDS_STAFFINFO  WHERE   (ST_AGE >= 25 AND ST_AGE< 35  and " + StrWhere() + ") UNION ALL SELECT   'X<25' AS ST_AREA, COUNT(*) AS ST_NUM FROM dbo.View_TBDS_STAFFINFO  WHERE  (ST_AGE < 25 and " + StrWhere() + ") ) AS derivedtbl_1";
            rep_TJ4.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            rep_TJ4.DataBind();
            lb_Total3.Text = totalNum;
            lb_Prop3.Text = string.Format("{0:f2}", float.Parse(lb_Total3.Text) / float.Parse(lb_Total.Text) * 100).ToString() + "%";

            sql = "select ST_GENDER,count(ST_GENDER) as ST_NUM from View_TBDS_STAFFINFO where " + StrWhere() + " group by ST_GENDER";
            rep_TJ5.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            rep_TJ5.DataBind();
            lb_Total4.Text = totalNum;
            sql = "select ST_SEQUEN,count(ST_SEQUEN) as ST_NUM,'" + totalNum + "' as ST_TOTAL  from (select case when ST_SEQUEN is null or ST_SEQUEN='' then '其他' else ST_SEQUEN end as ST_SEQUEN from View_TBDS_STAFFINFO where " + StrWhere() + ")a group by ST_SEQUEN";
            rep_TJ6.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            rep_TJ6.DataBind();
            lb_Total5.Text = totalNum;
            lb_Prop5.Text = string.Format("{0:f2}", float.Parse(lb_Total5.Text) / float.Parse(lb_Total.Text) * 100).ToString() + "%";

            sql = "select ST_CONTR,count(ST_CONTR) as ST_NUM,'" + totalNum + "' as ST_TOTAL  from (select case when ST_CONTR is null or ST_CONTR='' then ' 其他' else ST_CONTR end as ST_CONTR from View_TBDS_STAFFINFO where  " + StrWhere() + ")a group by ST_CONTR order by ST_CONTR desc";
            rep_TJ7.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            rep_TJ7.DataBind();
            lb_Total6.Text = totalNum;
            lb_Prop6.Text = string.Format("{0:f2}", float.Parse(lb_Total6.Text) / float.Parse(lb_Total.Text) * 100).ToString() + "%";

            //离职信息
            sql = "select LZ_BUMEN,LZ_BUMENID ,count(LZ_PERSON) as RS,(select count(LZ_ID) from OM_LIZHISHOUXU where LZ_SPZT='y') as ZS from OM_LIZHISHOUXU where LZ_SPZT='y' group by LZ_BUMENID,LZ_BUMEN";
            rptLZXX.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            rptLZXX.DataBind();
            sql = "select count(LZ_ID) from  OM_LIZHISHOUXU where LZ_SPZT='y'";
            System.Data.DataTable dtLZ = DBCallCommon.GetDTUsingSqlText(sql);
            lbLZHJ.Text = dtLZ.Rows[0][0].ToString();
        }


        protected void deletebt_Click(object sender, EventArgs e)
        {
            string strId = string.Empty;
            //DDlpartment.SelectedValue = "00";
            foreach (GridViewRow gr in SmartGridView1.Rows)
            {
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("checkboxstaff");
                if (chk.Checked)
                {
                    strId += "'" + ((System.Web.UI.WebControls.Label)gr.FindControl("ST_ID")).Text + "'" + ",";
                }
            }
            if (strId.Length > 0)
            {
                strId = strId.Substring(0, strId.Length - 1);
                string sql = "update TBDS_STAFFINFO set ST_PD=1 where ST_ID in (" + strId + ")";
                DBCallCommon.ExeSqlText(sql);
                databind();
            }
        }

        protected string editYg(string YgId)
        {
            return "javascript:window.showModalDialog('Sta_StaffEdit.aspx?action=edit&ST_ID=" + YgId + "','','DialogWidth=1020px;DialogHeight=600px')";
        }
        protected string viewYg(string YgId)
        {
            return "javascript:window.showModalDialog('Sta_StaffEdit.aspx?action=view&ST_ID=" + YgId + "','','DialogWidth=1020px;DialogHeight=600px')";
        }

        protected void search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            databind();
        }

        protected void SmartGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType.ToString() == "DataRow")
            {
                string id = ((System.Web.UI.WebControls.Label)e.Row.FindControl("ST_ID")).Text;
                e.Row.Attributes.Add("title", "双击查看详情");
                e.Row.Attributes.Add("ondblclick", "javascript:window.showModalDialog('Sta_operate.aspx?action=look&ST_ID=" + id + "','','DialogWidth=800px;DialogHeight=500px')");
                e.Row.Attributes.Add("onclick", "RowClick(this)");
                string pd = ((System.Web.UI.WebControls.Label)e.Row.FindControl("ST_PD")).Text;
                if (pd == "1")
                {
                    e.Row.BackColor = System.Drawing.Color.Gray;
                }
            }
        }

        protected void reset_Click(object sender, EventArgs e) //重置搜索条件
        {
            Control item1 = select.Controls[0];
            foreach (Control item in select.Controls)
            {
                if (!string.IsNullOrEmpty(item.ID))
                {
                    if (item is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)item).Text = "";
                    }
                    if (item is DropDownList)
                    {

                        ((DropDownList)item).SelectedIndex = 0;

                    }
                }
            }
            DDlpartment.SelectedValue = "00";
            DDlxueli.SelectedValue = "00";
            name.Text = "";
            ddlSequce.SelectedIndex = 0;
            ddlHkType.SelectedIndex = 0;
            ddlHetongZt.SelectedIndex = 0;
            ddlZzmm.SelectedIndex = 0;
            ddlZcdj.SelectedIndex = 0;
            ddlZjdj.SelectedIndex = 0;
            UCPaging1.CurrentPage = 1;
            databind();
        }

        #region 导出功能

        protected void btnexport_Click(object sender, EventArgs e) //导出
        {
            string sqltext = "select ST_WORKNO,ST_WORKNOOLD,ST_NAME,ST_ATTANDNO,ST_SEQUEN,ST_GENDER,ST_PEOPLE,b.DEP_NAME as ST_DEPNAME,ST_DEPID1,d.DEP_NAME as POSITION,ST_INTIME,ST_ZHENG,ST_IDCARD,ST_TELE,ST_CONTR,ST_CONTRTIME,ST_CONTRSTART,ST_CONTREND,ST_LZSJ,ST_BIRTHDAY,ST_REGIST,ST_MARRY,ST_POLITICAL,ST_JOBTIME,ST_EQXUELI,ST_XUELITYPE,ST_XUEWEI,ST_BIYE,ST_ZHUANYE,ST_BIYETIME,ST_EQZHICH,ST_ZHICH,ST_EMPTIME,ST_GETTIME,ST_ADDRESS,ST_HOMETELE,ST_CONTACTADDRESS,ST_SYMONEY,ST_ZZMONEY,ST_TXED,ST_NEXTED,ST_REASON,ST_NOTECER,ST_AGE,ST_YGTIME,ST_SBTIME,ST_ZFTIME,ST_RETIRE,ST_SECRET,ST_RESTCARD,ST_CLOTH,ST_JOBCARD,ST_ZHUSU,ST_SHOUXU from TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE left join TBDS_DEPINFO as d on a.ST_POSITION = d.DEP_CODE where " + StrWhere();
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }
        private void ExportDataItem(System.Data.DataTable objdt)
        {
            string filename = "人员信息表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("人员信息表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < objdt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(objdt.Rows[i][j].ToString());
                    }
                }

                //for (int i = 0; i < objdt.Columns.Count; i++)
                //{
                //    sheet0.AutoSizeColumn(i);
                //}
                sheet0.ForceFormulaRecalculation = true;

                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion

        protected void btnprint_Click(object sender, EventArgs e)
        {
            string strId = string.Empty;
            foreach (GridViewRow gr in SmartGridView1.Rows)
            {
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("checkboxstaff");
                if (chk.Checked)
                {
                    strId = ((System.Web.UI.WebControls.Label)gr.FindControl("ST_ID")).Text;
                }
            }
            string script = string.Format(@"btnPrint_onclick('{0}');", strId);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "error", script, true);
        }


    }
}
