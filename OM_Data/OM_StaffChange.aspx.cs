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

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_StaffChange : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "人员增减统计";
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                this.BindYearMoth(ddlYear, ddlMonth);
                GetDepartment();//绑定部门下拉框
                databind();//人员增减数据绑定
            }
            CheckUser(ControlFinder);
        }

        #region =======================查询=========================

        protected void Query(object sender, EventArgs e)
        {
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
            pager.TableName = "OM_StaffChange";
            pager.PrimaryKey = "";
            pager.ShowFields = "SC_STDepID,SC_STDep,SC_DepNum,SC_YearMonth,SC_IncNum,SC_DecNum,SC_IncPersonInfo,SC_DecPersonInfo,SC_ChangeInfo,SC_Note";
            pager.OrderField = "SC_STDepID,SC_YearMonth";
            pager.StrWhere = StrWhere();
            pager.OrderType = 0;
            pager.PageSize = int.Parse(ddl_pageno.SelectedValue);
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, rptstaffchange, UCPaging1, palNoData);
            if (palNoData.Visible)
            {
                UCPaging1.Visible = false;//如果筛选结果没有，则UCPaging不显示
                prow.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                prow.Visible = true;
                UCPaging1.InitPageInfo();
            }
            int lbnownum = 0;
            int lbincnum = 0;
            int lbdecnum = 0;
            Label lb_lastnum = (Label)rptstaffchange.Controls[rptstaffchange.Controls.Count - 1].FindControl("lb_lastnum");
            Label lb_nownum = (Label)rptstaffchange.Controls[rptstaffchange.Controls.Count - 1].FindControl("lb_nownum");
            Label lb_incnum = (Label)rptstaffchange.Controls[rptstaffchange.Controls.Count - 1].FindControl("lb_incnum");
            Label lb_decnum = (Label)rptstaffchange.Controls[rptstaffchange.Controls.Count - 1].FindControl("lb_decnum");
            for (int i = 0; i < rptstaffchange.Items.Count; i++)
            {
                Label s = (Label)rptstaffchange.Items[i].FindControl("lbXuHao");

                Label lbSC_DepNum = (Label)rptstaffchange.Items[i].FindControl("lbSC_DepNum");
                Label lbSC_IncNum = (Label)rptstaffchange.Items[i].FindControl("lbSC_IncNum");
                Label lbSC_DecNum = (Label)rptstaffchange.Items[i].FindControl("lbSC_DecNum");

                s.Text = (i + 1 + (pager.PageIndex - 1) * UCPaging1.PageSize).ToString();
                lbnownum += CommonFun.ComTryInt(lbSC_DepNum.Text.ToString().Trim());
                lbincnum += CommonFun.ComTryInt(lbSC_IncNum.Text.ToString().Trim());
                lbdecnum += CommonFun.ComTryInt(lbSC_DecNum.Text.ToString().Trim());
            }
            lb_nownum.Text = lbnownum.ToString();
            lb_incnum.Text = lbincnum.ToString();
            lb_decnum.Text = lbdecnum.ToString();
            lb_lastnum.Text = (lbnownum - lbincnum + lbdecnum).ToString();

            CellsMerge(rptstaffchange);
        }

        private string StrWhere()
        {
            string strWhere = " 1=1 ";
            if (ddlPartment.SelectedValue != "00")
            {
                strWhere += "and SC_STDepID ='" + ddlPartment.SelectedValue + "' ";
            }
            if (txtName.Text.ToString().Trim() != "")
            {
                strWhere += " and ( SC_IncPersonInfo like '%" + txtName.Text.ToString().Trim() + "%' or SC_DecPersonInfo like '%" + txtName.Text.ToString().Trim() + "%')";
            }
            if (ddlYear.SelectedValue != "00" && (txtEndTime.Text.ToString().Trim() == "" || txtEndTime.Text.ToString().Trim() == null))
            {
                strWhere += " and SC_YearMonth like '" + ddlYear.SelectedItem.Text.ToString().Trim() + "%'";
            }
            if (ddlMonth.SelectedValue != "00" && (txtEndTime.Text.ToString().Trim() == "" || txtEndTime.Text.ToString().Trim() == null))
            {
                string month_select = "-" + ddlMonth.SelectedItem.Text.ToString().Trim();
                strWhere += " and SC_YearMonth like '%" + month_select + "%'";
            }
            if (txtEndTime.Text.ToString().Trim() != "")
            {
                strWhere += " and SC_YearMonth <= '" + txtEndTime.Text.ToString().Trim() + "'";
                if (ddlYear.SelectedValue != "00")
                {
                    string startyearmonth = "";
                    if (ddlMonth.SelectedValue == "00")
                        startyearmonth = ddlYear.SelectedItem.Text.ToString().Trim() + "-01";
                    else
                        startyearmonth = ddlYear.SelectedItem.Text.ToString().Trim() + "-" + ddlMonth.SelectedItem.Text.ToString().Trim();
                    strWhere += " and SC_YearMonth>= '" + startyearmonth + "'";
                }
            }
            return strWhere;
        }
        #endregion

        private void GetDepartment()//绑定部门
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlPartment.DataSource = dt;
            ddlPartment.DataTextField = "DEP_NAME";
            ddlPartment.DataValueField = "DEP_CODE";
            ddlPartment.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddlPartment.Items.Insert(0, item);
            ddlPartment.SelectedValue = "00";
        }

        private void BindYearMoth(DropDownList ddl_Year, DropDownList ddl_Month)
        {
            for (int i = 0; i < 30; i++)
            {
                ddl_Year.Items.Add(new ListItem(DateTime.Now.AddYears(-i).Year.ToString(), DateTime.Now.AddYears(-i).Year.ToString()));
            }
            ddl_Year.Items.Insert(0, new ListItem("-请选择-", "00"));
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
        }

        protected void btnReset_Click(object sender, EventArgs e) //重置搜索条件
        {
            txtEndTime.Text = "";
            UCPaging1.CurrentPage = 1;
            databind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            int k = 0;//记录选中的行数
            for (int j = 0; j < rptstaffchange.Items.Count; j++)
            {
                if (((System.Web.UI.WebControls.CheckBox)rptstaffchange.Items[j].FindControl("chk")).Checked)
                {
                    k++;
                }
            }
            if (k > 0)
            {
                for (int i = 0; i < rptstaffchange.Items.Count; i++)
                {
                    if (((System.Web.UI.WebControls.CheckBox)rptstaffchange.Items[i].FindControl("chk")).Checked)
                    {
                        string lbSC_STDepID = ((System.Web.UI.WebControls.Label)rptstaffchange.Items[i].FindControl("lbSC_STDepID")).Text.Trim();
                        string lbSC_YearMonth = ((System.Web.UI.WebControls.Label)rptstaffchange.Items[i].FindControl("lbSC_YearMonth")).Text.Trim();
                        string txtSC_ChangeInfo = ((System.Web.UI.WebControls.TextBox)rptstaffchange.Items[i].FindControl("txtSC_ChangeInfo")).Text.Trim();
                        string txtSC_Note = ((System.Web.UI.WebControls.TextBox)rptstaffchange.Items[i].FindControl("txtSC_Note")).Text.Trim();
                        string sql = "update OM_StaffChange set SC_ChangeInfo='" + txtSC_ChangeInfo + "',SC_Note='" + txtSC_Note + "'where SC_STDepID='" + lbSC_STDepID + "'and SC_YearMonth='" + lbSC_YearMonth + "'";
                        list.Add(sql);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要调整的数据！');", true);
                return;
            }

            //更新
            DBCallCommon.ExecuteTrans(list);
            UCPaging1.CurrentPage = 1;
            databind();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (ddlYear.SelectedValue == "00" || ddlMonth.SelectedValue == "00")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择更新年月！');", true);
                return;
            }
            else
            {
                List<string> list = new List<string>();
                string yearmonth = ddlYear.SelectedValue + "-" + ddlMonth.SelectedValue;
                string nextyear = "";
                string nextmonth = "";
                try
                {
                    if (CommonFun.ComTryInt(ddlMonth.SelectedValue.ToString().Trim()) == 12)
                    {
                        nextyear = (CommonFun.ComTryInt(ddlYear.SelectedValue.ToString().Trim()) + 1).ToString().Trim();
                        nextmonth = "01";
                    }
                    else
                    {
                        nextyear = ddlYear.SelectedValue.ToString().Trim();
                        nextmonth = (CommonFun.ComTryInt(ddlMonth.SelectedValue.ToString().Trim()) + 1).ToString("00").Trim();
                    }
                }
                catch
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('获取年月份出错！');", true);
                    return;
                }
                if (CommonFun.ComTryInt(ddlYear.SelectedValue) >= 2015)
                {
                    string sqlstaff = "select ST_ID,ST_NAME,ST_DEPID,ST_POSITION,ST_PD, b.DEP_NAME, ST_INTIME,ST_DATATIME from TBDS_STAFFINFO_record as a left join TBDS_DEPINFO as b on  a.ST_POSITION=b.DEP_CODE WHERE ST_ID is not null and ST_NAME<>''and ST_NAME is not null and ST_DEPID is not null and ST_PD='0' and ST_DATATIME is not null and substring(ST_DATATIME,1,4)='" + nextyear + "' and substring(ST_DATATIME,6,2)='" + nextmonth + "'order by ST_DATATIME,ST_ID";
                    DataTable dtstaff = DBCallCommon.GetDTUsingSqlText(sqlstaff);

                    if (dtstaff.Rows.Count > 0)
                    {
                        string sqldepnum = "";
                        DataTable dtdepnum;
                        string sqlincnum = "";
                        DataTable dtincnum;
                        string sqldecnum = "";
                        DataTable dtdecnum;
                        string sqlvalid = "";
                        DataTable dtvalid;
                        string sqlupdate = "";
                        string sqldep = "select distinct ST_DEPID,DEP_NAME from TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE where ST_DEPID is not null";
                        DataTable dtdep = DBCallCommon.GetDTUsingSqlText(sqldep);
                        for (int i = 0; i < dtdep.Rows.Count; i++)
                        {
                            //部门人数
                            sqldepnum = "select count(*) from TBDS_STAFFINFO_record where substring(ST_DATATIME,1,4)='" + nextyear + "' and substring(ST_DATATIME,6,2)='" + nextmonth + "' and ST_ID is not null and ST_NAME<>''and ST_NAME is not null and ST_DEPID is not null and ST_PD='0' and ST_DEPID='" + dtdep.Rows[i][0].ToString() + "'";
                            dtdepnum = DBCallCommon.GetDTUsingSqlText(sqldepnum);

                            //增减人员信息
                            sqlincnum = "select ST_ID,ST_NAME,ST_DEPID,ST_DEPID1,ST_POSITION,ST_PD, b.DEP_NAME, ST_INTIME,ST_DATATIME from TBDS_STAFFINFO_record as a left join TBDS_DEPINFO as b on  a.ST_POSITION=b.DEP_CODE WHERE ST_ID is not null and ST_NAME<>''and ST_NAME is not null and ST_DEPID is not null and ST_PD='0' and ST_DATATIME is not null and substring(ST_DATATIME,1,4)='" + nextyear + "' and substring(ST_DATATIME,6,2)='" + nextmonth + "'and ST_DEPID='" + dtdep.Rows[i][0].ToString() + "'and ST_ID not in (select ST_ID from TBDS_STAFFINFO_record as a left join TBDS_DEPINFO as b on  a.ST_POSITION=b.DEP_CODE WHERE ST_ID is not null and ST_NAME<>''and ST_NAME is not null and ST_DEPID is not null and ST_PD='0' and ST_DATATIME is not null and substring(ST_DATATIME,1,4)='" + ddlYear.SelectedValue + "' and substring(ST_DATATIME,6,2)='" + ddlMonth.SelectedValue + "'and ST_DEPID='" + dtdep.Rows[i][0].ToString() + "') order by ST_DATATIME,ST_ID";
                            dtincnum = DBCallCommon.GetDTUsingSqlText(sqlincnum);

                            sqldecnum = "select ST_ID,ST_NAME,ST_DEPID,ST_DEPID1,ST_POSITION,ST_PD, b.DEP_NAME, ST_INTIME,ST_DATATIME from TBDS_STAFFINFO_record as a left join TBDS_DEPINFO as b on  a.ST_POSITION=b.DEP_CODE WHERE ST_ID is not null and ST_NAME<>''and ST_NAME is not null and ST_DEPID is not null and ST_PD='0' and ST_DATATIME is not null and substring(ST_DATATIME,1,4)='" + ddlYear.SelectedValue + "' and substring(ST_DATATIME,6,2)='" + ddlMonth.SelectedValue + "'and ST_DEPID='" + dtdep.Rows[i][0].ToString() + "'and ST_ID not in (select ST_ID from TBDS_STAFFINFO_record as a left join TBDS_DEPINFO as b on  a.ST_POSITION=b.DEP_CODE WHERE ST_ID is not null and ST_NAME<>''and ST_NAME is not null and ST_DEPID is not null and ST_PD='0' and ST_DATATIME is not null and substring(ST_DATATIME,1,4)='" + nextyear + "' and substring(ST_DATATIME,6,2)='" + nextmonth + "'and ST_DEPID='" + dtdep.Rows[i][0].ToString() + "') order by ST_DATATIME,ST_ID";
                            dtdecnum = DBCallCommon.GetDTUsingSqlText(sqldecnum);

                            string incperson = "";
                            string decperson = "";
                            for (int m = 0; m < dtincnum.Rows.Count; m++)
                            {
                                if (dtincnum.Rows[m]["ST_DEPID1"].ToString() != null && dtincnum.Rows[m]["ST_DEPID1"].ToString() != "")
                                    incperson += dtincnum.Rows[m]["ST_NAME"].ToString() + "(" + dtincnum.Rows[m]["ST_DEPID1"].ToString() + ":" + dtincnum.Rows[m]["DEP_NAME"].ToString() + ")/";
                                else
                                    incperson += dtincnum.Rows[m]["ST_NAME"].ToString() + "(" + dtincnum.Rows[m]["DEP_NAME"].ToString() + ")/";
                            }
                            for (int n = 0; n < dtdecnum.Rows.Count; n++)
                            {
                                if (dtdecnum.Rows[n]["ST_DEPID1"].ToString() != null && dtdecnum.Rows[n]["ST_DEPID1"].ToString() != "")
                                    decperson += dtdecnum.Rows[n]["ST_NAME"].ToString() + "(" + dtdecnum.Rows[n]["ST_DEPID1"].ToString() + ":" + dtdecnum.Rows[n]["DEP_NAME"].ToString() + ")/";
                                else
                                    decperson += dtdecnum.Rows[n]["ST_NAME"].ToString() + "(" + dtdecnum.Rows[n]["DEP_NAME"].ToString() + ")/";
                            }
                            if (dtincnum.Rows.Count > 0)
                            {
                                incperson = incperson.Substring(0, incperson.LastIndexOf('/'));
                            }
                            if (dtdecnum.Rows.Count > 0)
                            {
                                decperson = decperson.Substring(0, decperson.LastIndexOf('/'));
                            }
                            sqlvalid = "select * from OM_StaffChange where SC_YearMonth='" + yearmonth + "'";
                            dtvalid = DBCallCommon.GetDTUsingSqlText(sqlvalid);
                            if (dtvalid.Rows.Count > 0)//更新
                            {
                                sqlupdate = "update OM_StaffChange set SC_DepNum='" + dtdepnum.Rows[0][0].ToString() + "',SC_STDepID='" + dtdep.Rows[i][0].ToString() + "', SC_STDep='" + dtdep.Rows[i][1].ToString() + "', SC_YearMonth='" + yearmonth + "', SC_IncNum='" + dtincnum.Rows.Count.ToString() + "', SC_DecNum='" + dtdecnum.Rows.Count.ToString() + "', SC_IncPersonInfo='" + incperson + "', SC_DecPersonInfo='" + decperson + "'where SC_STDepID='" + dtdep.Rows[i][0].ToString() + "'and SC_YearMonth='" + yearmonth + "'";
                                list.Add(sqlupdate);
                            }
                            else//新增
                            {
                                sqlupdate = "insert into  OM_StaffChange ( SC_DepNum,SC_STDepID,SC_STDep,SC_YearMonth,SC_IncNum,SC_DecNum,SC_IncPersonInfo,SC_DecPersonInfo)values('" + dtdepnum.Rows[0][0].ToString() + "','" + dtdep.Rows[i][0].ToString() + "', '" + dtdep.Rows[i][1].ToString() + "', '" + yearmonth + "', '" + dtincnum.Rows.Count.ToString() + "', '" + dtdecnum.Rows.Count.ToString() + "', '" + incperson + "', '" + decperson + "')";
                                list.Add(sqlupdate);
                            }
                        }
                        try
                        {
                            DBCallCommon.ExecuteTrans(list);
                            UCPaging1.CurrentPage = 1;
                            databind();
                        }
                        catch
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('更新失败，请联系管理员！');", true);
                            return;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + ddlYear.SelectedValue + "年" + ddlMonth.SelectedValue + "月没有相关数据！');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + ddlYear.SelectedValue + "年没有相关数据！');", true);
                    return;
                }
            }
        }

        private void CellsMerge(Repeater rpt)//合并单元格
        {
            if (rpt.Items.Count < 1)
            {
                return;
            }
            HtmlTableCell tdmergefirst = rpt.Items[0].FindControl("tdmerge") as HtmlTableCell;
            tdmergefirst.RowSpan = (tdmergefirst.RowSpan == -1) ? 1 : tdmergefirst.RowSpan;
            for (int i = 1; i < rpt.Items.Count; i++)
            {
                HtmlTableCell tdmergei = rpt.Items[i].FindControl("tdmerge") as HtmlTableCell;
                tdmergei.RowSpan = (tdmergei.RowSpan == -1) ? 1 : tdmergei.RowSpan;
                if (tdmergefirst.InnerText == tdmergei.InnerText)
                {
                    tdmergei.Visible = false;
                    tdmergefirst.RowSpan++;
                    tdmergefirst.VAlign = VerticalAlign.Middle.ToString();
                }
                else
                {
                    tdmergefirst = tdmergei;
                }
            }
        }

        protected void rptstaffchange_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                DataRowView dr = (DataRowView)e.Item.DataItem;
                Label lbLasDeptNum = (Label)e.Item.FindControl("lbLasDeptNum");
                Label lbSC_DepNum = (Label)e.Item.FindControl("lbSC_DepNum");
                Label lbSC_IncNum = (Label)e.Item.FindControl("lbSC_IncNum");
                Label lbSC_DecNum = (Label)e.Item.FindControl("lbSC_DecNum");
                lbLasDeptNum.Text = (CommonFun.ComTryInt(lbSC_DepNum.Text.Trim()) - CommonFun.ComTryInt(lbSC_IncNum.Text.Trim()) + CommonFun.ComTryInt(lbSC_DecNum.Text.Trim())).ToString();
                if (lbSC_IncNum.Text != "0")
                    lbSC_IncNum.ForeColor = System.Drawing.Color.FromName("#9E4DB3");
                if (lbSC_DecNum.Text != "0")
                    lbSC_DecNum.ForeColor = System.Drawing.Color.Red;
                //if (e.Item.ItemIndex != -1)
                //{
                //    ((TextBox)e.Item.FindControl("txtSC_IncPersonInfo")).Text = SubStr(dr["SC_IncPersonInfo"].ToString(), 22);
                //    ((TextBox)e.Item.FindControl("txtSC_DecPersonInfo")).Text = SubStr(dr["SC_DecPersonInfo"].ToString(), 22);
                //}
            }
        }

        public string SubStr(string sString, int nLeng)
        {
            if (sString.Length <= nLeng)
            {
                return sString;
            }
            string sNewStr = sString.Substring(0, nLeng);
            sNewStr = sNewStr + "...";
            return sNewStr;
        }
    }
}
