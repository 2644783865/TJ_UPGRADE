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
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class staff_record : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "员工基本信息管理";
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            //deletebt.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            //deletebt.Click += new EventHandler(deletebt_Click);
            if (!IsPostBack)
            {
                this.BindYearMoth(ddlYear, ddlMonth);
                GetDepartment();//绑定部门下拉框
                GetXueLi();//学历下拉框
                GetSele();//绑定筛选信息
                databind();//人员信息表绑定
                Account();//数据统计信息绑定

            }
            CheckUser(ControlFinder);

        }

        //protected void ddlIfzaizhi_OnSelectedIndexChanged(object sender, EventArgs e)
        //{
        //    foreach (GridViewRow gr in SmartGridView1.Rows)
        //    {
        //        System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)gr.FindControl("checkboxstaff");
        //        System.Web.UI.WebControls.Label lb = (System.Web.UI.WebControls.Label)gr.FindControl("ST_ID");
        //        if (cbx.Checked == true)
        //        {
        //            if (ddlIfzaizhi.SelectedValue == "0")
        //            {
        //                string sql11 = "update TBDS_STAFFINFO set ST_PD='" + ddlIfzaizhi.SelectedValue.ToString() + "'where ST_ID='"+lb.Text.ToString()+"'";
        //                DBCallCommon.ExeSqlText(sql11);
        //            }
        //            if (ddlIfzaizhi.SelectedValue == "1")
        //            {
        //                string sql12 = "update TBDS_STAFFINFO set ST_PD='" + ddlIfzaizhi.SelectedValue.ToString() + "'where ST_ID='" + lb.Text.ToString() + "'";
        //                DBCallCommon.ExeSqlText(sql12);
        //            }
        //        }
        //    }
        //}
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
            pager.TableName = "View_TBDS_STAFFINFO_record";
            pager.PrimaryKey = "ST_ID";
            pager.ShowFields = "*";
            pager.OrderField = "ST_ID";
            pager.StrWhere = StrWhere();
            pager.OrderType = 0;
            pager.PageSize = int.Parse(ddl_pageno.SelectedValue);
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, SmartGridView1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;//如果筛选结果没有，则UCPaging不显示
                //deletebt.Visible = false;//如果筛选结果没有，则删除按钮不显示
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
                //deletebt.Visible = true;
            }
            string sql = "select ST_ID from View_TBDS_STAFFINFO_record where " + StrWhere();
            lb_People.Text = DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString();//多少行就有多少人
        }

        private string StrWhere()
        {
            string datetime_now = DateTime.Now.ToString("yyyy-MM");
            datetime_now += "-01";
            string strWhere = " 1=1 ";

            if (rblIfZaizhi.Text != "全部")
            {
                strWhere += " and  ST_PD='" + rblIfZaizhi.SelectedValue.ToString() + "'";
            }
            string str = DDlpartment.SelectedValue;
            string str1 = DDlxueli.SelectedValue;
            if (str != "00")
            {
                //strWhere += "and ST_PD=0 AND ST_DEPID ='" + str + "'";
                strWhere += "and ST_DEPID ='" + str + "'";
            }
            if (str1 != "00")
            {
                strWhere += "and ST_XUELI ='" + str1 + "' ";
            }
            if (str == "00" && str1 == "00")
            {
                if (name.Text.ToString().Trim() != "")
                {
                    strWhere += " and ST_NAME ='" + name.Text.ToString().Trim() + "'";
                }
            }
            else
            {
                if (name.Text.ToString().Trim() != "")
                {
                    strWhere += " and ST_NAME ='" + name.Text.ToString().Trim() + "'";
                }
            }
            if (ddlYear.SelectedValue != "00")
            {
                strWhere += " and ST_DATATIME like '" + ddlYear.SelectedItem.Text.ToString().Trim() + "%'";
            }
            if (ddlMonth.SelectedValue != "00")
            {
                string month_select = "-" + ddlMonth.SelectedItem.Text.ToString().Trim() + "-";
                strWhere += " and ST_DATATIME like '%" + month_select + "%'";
            }
            if (ddlYear.SelectedItem.Text.ToString().Trim() == "-请选择-" && ddlMonth.SelectedItem.Text.ToString().Trim() == "-请选择-" && name.Text.ToString().Trim() == "")
            {
                strWhere += " and ST_DATATIME='" + datetime_now + "'";
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
            string sqlText = "select distinct XL_ID,XL_NAME from TBDS_XUELI";
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
            string totalNum = DBCallCommon.GetDTUsingSqlText("select count(*) from View_TBDS_STAFFINFO_record where " + StrWhere()).Rows[0][0].ToString();

            string sql = "select DEP_NAME,ST_DEPID,count(DEP_NAME) as ST_TOTAL from View_TBDS_STAFFINFO_record where " + StrWhere() + " group by DEP_NAME,ST_DEPID order by ST_DEPID";
            rep_TJ1.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            rep_TJ1.DataBind();
            lb_Total.Text = totalNum;

            sql = "select ST_XUELINM ,count(ST_XUELINM) as ST_NUM,'" + totalNum + "' as ST_TOTAL from View_TBDS_STAFFINFO_record where  " + StrWhere() + " group by ST_XUELINM,ST_XUELI order by cast(ST_XUELI as int) desc";
            rep_TJ2.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            rep_TJ2.DataBind();
            lb_Total1.Text = totalNum;
            lb_Prop1.Text = string.Format("{0:f2}", float.Parse(lb_Total1.Text) / float.Parse(lb_Total.Text) * 100).ToString() + "%";

            sql = "select case when ST_ZHICHDJ='5' then '正高级职称' when  ST_ZHICHDJ='4' then '副高级职称'  when   ST_ZHICHDJ='2' then '中级职称' when  ST_ZHICHDJ='1' then '初级职称' else '' end as ST_ZHICHNM,ST_ZHICHDJ,count(ST_ZHICHDJ) as ST_NUM,'" + totalNum + "' as ST_TOTAL from View_TBDS_STAFFINFO_record where ST_ZHICHDJ!=''  and ST_ZHICHDJ is not null and " + StrWhere() + " group by ST_ZHICHDJ order by ST_ZHICHDJ desc";
            System.Data.DataTable dtZC = DBCallCommon.GetDTUsingSqlText(sql);
            sql = "select case when ST_ZHIJIDJ='5' then '高级技师' when  ST_ZHIJIDJ='4' then '技师'  when  ST_ZHIJIDJ='3' then '高级技能' when   ST_ZHIJIDJ='2' then '中级技能' when  ST_ZHIJIDJ='1' then '初级技能' else '' end as ST_ZHICHNM, ST_ZHIJIDJ as ST_ZHICHDJ,count(ST_ZHIJIDJ) as ST_NUM,'" + totalNum + "' as ST_TOTAL  from View_TBDS_STAFFINFO_record where ST_ZHIJIDJ!=''  and ST_ZHIJIDJ is not null and " + StrWhere() + "  group by ST_ZHIJIDJ order by  ST_ZHIJIDJ desc";
            System.Data.DataTable dtZJ = DBCallCommon.GetDTUsingSqlText(sql);
            sql = "select '无' as ST_ZHICHNM,count(*) as ST_NUM,'" + totalNum + "' as ST_TOTAL  from View_TBDS_STAFFINFO_record where (ST_ZHIJIDJ='' or ST_ZHIJIDJ is null) and (ST_ZHICHDJ='' or ST_ZHICHDJ is null) and " + StrWhere();
            System.Data.DataTable dtWU = DBCallCommon.GetDTUsingSqlText(sql);
            dtZC.Merge(dtZJ);
            dtZC.Merge(dtWU);
            rep_TJ3.DataSource = dtZC;
            rep_TJ3.DataBind();
            lb_Total2.Text = totalNum;
            lb_Prop2.Text = string.Format("{0:f2}", float.Parse(lb_Total2.Text) / float.Parse(lb_Total.Text) * 100).ToString() + "%";

            sql = "SELECT ST_AREA, ST_NUM,(SELECT  COUNT(*)  FROM  dbo.View_TBDS_STAFFINFO_record where " + StrWhere() + ") AS ST_TOTAL FROM  ( SELECT     '55<=X' AS ST_AREA, COUNT(*) AS ST_NUM FROM dbo.View_TBDS_STAFFINFO_record  WHERE  (ST_AGE >= 55 and " + StrWhere() + ") UNION ALL SELECT  '45<=X<55' AS ST_AREA, COUNT(*) AS ST_NUM FROM   dbo.View_TBDS_STAFFINFO_record  WHERE  (ST_AGE >= 45 AND ST_AGE< 55 and " + StrWhere() + ") UNION ALL  SELECT  '35<=X<45' AS ST_AREA, COUNT(*) AS ST_NUM  FROM   dbo.View_TBDS_STAFFINFO_record  WHERE (ST_AGE >= 35 AND ST_AGE< 45 and  " + StrWhere() + ") UNION ALL SELECT     '25<=X<35' AS ST_AREA, COUNT(*) AS ST_NUM  FROM   dbo.View_TBDS_STAFFINFO_record  WHERE   (ST_AGE >= 25 AND ST_AGE< 35  and " + StrWhere() + ") UNION ALL SELECT   'X<25' AS ST_AREA, COUNT(*) AS ST_NUM FROM dbo.View_TBDS_STAFFINFO_record  WHERE  (ST_AGE < 25 and " + StrWhere() + ") ) AS derivedtbl_1";
            rep_TJ4.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            rep_TJ4.DataBind();
            lb_Total3.Text = totalNum;
            lb_Prop3.Text = string.Format("{0:f2}", float.Parse(lb_Total3.Text) / float.Parse(lb_Total.Text) * 100).ToString() + "%";

            sql = "select ST_GENDER,count(ST_GENDER) as ST_NUM from View_TBDS_STAFFINFO_record where " + StrWhere() + " group by ST_GENDER";
            rep_TJ5.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            rep_TJ5.DataBind();
            lb_Total4.Text = totalNum;

            sql = "select ST_SEQUEN,count(ST_SEQUEN) as ST_NUM,'" + totalNum + "' as ST_TOTAL  from (select case when ST_SEQUEN is null or ST_SEQUEN='' then '其他' else ST_SEQUEN end as ST_SEQUEN from View_TBDS_STAFFINFO_record where " + StrWhere() + ")a group by ST_SEQUEN";
            rep_TJ6.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            rep_TJ6.DataBind();
            lb_Total5.Text = totalNum;
            lb_Prop5.Text = string.Format("{0:f2}", float.Parse(lb_Total5.Text) / float.Parse(lb_Total.Text) * 100).ToString() + "%";

            sql = "select ST_CONTR,count(ST_CONTR) as ST_NUM,'" + totalNum + "' as ST_TOTAL   from (select case when ST_CONTR is null or ST_CONTR='' then ' 其他' else ST_CONTR end as ST_CONTR from View_TBDS_STAFFINFO_record where  " + StrWhere() + ")a group by ST_CONTR order by ST_CONTR desc";
            rep_TJ7.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
            rep_TJ7.DataBind();
            lb_Total6.Text = totalNum;
            lb_Prop6.Text = string.Format("{0:f2}", float.Parse(lb_Total6.Text) / float.Parse(lb_Total.Text) * 100).ToString() + "%";
        }


        //protected void deletebt_Click(object sender, EventArgs e)
        //{
        //    string strId = string.Empty;
        //    //DDlpartment.SelectedValue = "00";
        //    foreach (GridViewRow gr in SmartGridView1.Rows)
        //    {
        //        System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("checkboxstaff");
        //        if (chk.Checked)
        //        {
        //            strId += "'" + ((System.Web.UI.WebControls.Label)gr.FindControl("ST_ID")).Text + "'" + ",";
        //        }
        //    }
        //    if (strId.Length > 0)
        //    {
        //        strId = strId.Substring(0, strId.Length - 1);
        //        string sql = "update TBDS_STAFFINFO set ST_PD=1 where ST_ID in (" + strId + ")";
        //        DBCallCommon.ExeSqlText(sql);
        //        databind();
        //    }
        //}

        protected string editYg(string YgId, string DataTIME)
        {
            return "javascript:window.showModalDialog('Sta_RecordDetail.aspx?action=look&ST_DATATIME=" + DataTIME + "&ST_ID=" + YgId + "','','DialogWidth=1020px;DialogHeight=600px')";
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
            foreach (Control item in select.Controls[1].Controls[0].Controls)
            {
                if (!string.IsNullOrEmpty(item.ID))
                {
                    if (item is System.Web.UI.WebControls.TextBox)
                    {
                        ((System.Web.UI.WebControls.TextBox)item).Text = "";
                    }
                    if (item is DropDownList)
                    {
                        if (item.ID.Substring(0, 6).ToString() == "screen")
                        {
                            ((DropDownList)item).SelectedValue = "0";
                        }
                        else if (item.ID.Substring(0, 8).ToString() == "ddlLogic")
                        {
                            ((DropDownList)item).SelectedValue = "OR";
                        }
                        else if (item.ID.Substring(0, 11).ToString() == "ddlRelation")
                        {
                            ((DropDownList)item).SelectedValue = "0";
                        }
                    }
                }
            }
            DDlpartment.SelectedValue = "00";
            DDlxueli.SelectedValue = "00";
            name.Text = "";
            UCPaging1.CurrentPage = 1;
            databind();
        }

        #region 导出功能

        protected void btnexport_Click(object sender, EventArgs e) //导出
        {
            string sqltext = "select ST_WORKNO,ST_WORKNOOLD,ST_NAME,ST_ATTANDNO,ST_SEQUEN,ST_GENDER,ST_PEOPLE,b.DEP_NAME as ST_DEPNAME,ST_DEPID1,d.DEP_NAME as POSITION,ST_INTIME,ST_ZHENG,ST_IDCARD,ST_TELE,ST_CONTR,ST_CONTRTIME,ST_CONTRSTART,ST_CONTREND,ST_BIRTHDAY,ST_REGIST,ST_MARRY,ST_POLITICAL,ST_JOBTIME,ST_EQXUELI,ST_XUELITYPE,ST_XUEWEI,ST_BIYE,ST_ZHUANYE,ST_BIYETIME,ST_EQZHICH,ST_ZHICH,ST_EMPTIME,ST_GETTIME,ST_ADDRESS,ST_HOMETELE,ST_CONTACTADDRESS,ST_SYMONEY,ST_ZZMONEY,ST_TXED,ST_NEXTED,ST_REASON,ST_NOTECER,ST_AGE,ST_YGTIME,ST_SBTIME,ST_ZFTIME,ST_RETIRE,ST_SECRET,ST_RESTCARD,ST_CLOTH,ST_JOBCARD,ST_ZHUSU,ST_SHOUXU from TBDS_STAFFINFO_record as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE left join TBDS_DEPINFO as d on a.ST_POSITION = d.DEP_CODE where " + StrWhere();
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

        private void BindYearMoth(DropDownList ddl_Year, DropDownList ddl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                ddl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddl_Year.Items.Insert(0, new ListItem("-请选择-", "00"));
            //dpl_Year.SelectedIndex = 0;
            for (int k = 1; k <= 12; k++)
            {
                string j = k.ToString();
                if (k < 10)
                {
                    j = "0" + k.ToString();
                }
                ddl_Month.Items.Add(new ListItem(j.ToString(), j.ToString()));
            }
            ddl_Month.Items.Insert(0, new ListItem("-请选择-", "00"));
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            ddlMonth.SelectedValue = DateTime.Now.Month.ToString().PadLeft(2, '0');
            //dpl_Month.SelectedIndex = 0;
        }

    }
}

