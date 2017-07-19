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
using System.Text;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Office.Interop.Excel;
using System.Collections.Generic;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_PURCHASEPLAN_VIEW : BasicPage
    {

        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();

            if (!IsPostBack)
            {
                bindGrid();

                bindGV();//绑定条件框

                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;


            }
        }

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }

        //初始化分布信息
        private void InitPager()
        {
            // View_SM_PURCHASEPLAN_FROM_VIEW

            pager.TableName = "View_SM_PURCHASEPLAN_VIEW";//从技术任务分工中读取数据

            pager.PrimaryKey = "ptcode";

            pager.ShowFields = "left(sqrtime,10) as sqrtime,depnm,sqrnm,planno,ptcode,pjnm,engnm,marid,marnm,margg,marcz,margb,PUR_TUHAO,length,width,marunit,cast(num as float) as num,marfzunit,cast(fznum as float) as fznum,rpnum,rpfznum,PUR_MASHAPE,changestate,stousestate,xsreplacestate,replacestate,ddreplacestate,purstate,PUR_ZYDY,purnote";

            pager.OrderField = DropDownListType.SelectedValue;//计划时间

            pager.StrWhere = CreateStrCondition();

            pager.OrderType = Convert.ToInt32(RadioButtonListOrderBy.SelectedValue);//按时间降序排列

            pager.PageSize = 30;

           

        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindnum(string strwhere)
        {
            string sql1 = "select cast(isnull(sum(num),0)as float) as NUM from View_SM_PURCHASEPLAN_VIEW where " + strwhere;

            SqlDataReader sdr1 = DBCallCommon.GetDRUsingSqlText(sql1);

            if (sdr1.Read())
            {
                hfdnum.Value = sdr1["NUM"].ToString();
            }
            sdr1.Close();



        }

        private void bindGrid()
        {
            bindnum(pager.StrWhere);

            pager.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
           
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("ondblclick", "ShowPLan('" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["planno"].ToString()) + "','" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["PUR_MASHAPE"].ToString()) + "');");
                
                if (e.Row.Cells[19].Text != "正常")
                {
                    
                    //变更
                    e.Row.Cells[19].Attributes.Add("onClick", "ShowChangeModal('" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["ptcode"].ToString()) + "');");
                }

                if (e.Row.Cells[20].Text != "正常")
                {
                    //占有
                    e.Row.Cells[20].Attributes.Add("onClick", "ShowStoUseModal('" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["ptcode"].ToString()) + "');");
                }

                if (e.Row.Cells[21].Text != "正常")
                {
                    //相似占用，相似代用

                    e.Row.Cells[21].Attributes.Add("onClick", "ShowReplaceModal('" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["ptcode"].ToString()) + "');");
                }
                if (e.Row.Cells[22].Text != "正常")
                {
                    //代用
                    e.Row.Cells[22].Attributes.Add("onClick", "ShowReplaceModal('" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["ptcode"].ToString()) + "');");
                }
                if (e.Row.Cells[23].Text != "正常")
                {
                    //订单代用
                    e.Row.Cells[23].Attributes.Add("onClick", "ShowReplaceModal('" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["ptcode"].ToString()) + "');");
                }
                
                //占用拆分问题
                string strsql = "select ordernum,isclose,innum,warehouse,warehouseposition,outnum from dbo.ordertointoout ('" + GridView1.DataKeys[e.Row.RowIndex]["ptcode"].ToString() + "')";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(strsql);
                if (dr.Read())
                {
                    if (GridView1.DataKeys[e.Row.RowIndex]["purstate"].ToString() == "7")
                    {
                        (e.Row.FindControl("LabelCKNUM") as System.Web.UI.WebControls.Label).Text = dr["ordernum"].ToString();

                        if (dr["isclose"].ToString() == "1")
                        {
                            (e.Row.FindControl("LabelCKNUM") as System.Web.UI.WebControls.Label).ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        (e.Row.FindControl("LabelCKNUM") as System.Web.UI.WebControls.Label).Text = "0";
                    }

                    (e.Row.FindControl("LabelINNUM") as System.Web.UI.WebControls.Label).Text = dr["innum"].ToString();

                    (e.Row.FindControl("LabelOUTNUM") as System.Web.UI.WebControls.Label).Text = dr["outnum"].ToString();

                    (e.Row.FindControl("Labelwarehouse") as System.Web.UI.WebControls.Label).Text = dr["warehouse"].ToString();
                    (e.Row.FindControl("Labelwarehouseposition") as System.Web.UI.WebControls.Label).Text = dr["warehouseposition"].ToString();


                }
                dr.Close();
                if ((e.Row.FindControl("LabelINNUM") as System.Web.UI.WebControls.Label).Text != "0")
                {
                    e.Row.Cells[15].Attributes.Add("onClick", "ShowINModal('" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["ptcode"].ToString()) + "');");
                }
                if ((e.Row.FindControl("LabelOUTNUM") as System.Web.UI.WebControls.Label).Text != "0")
                {

                    e.Row.Cells[18].Attributes.Add("onClick", "ShowOutModal('" + Server.UrlEncode(GridView1.DataKeys[e.Row.RowIndex]["ptcode"].ToString()) + "');");
                }

            }
            
            
            else if (e.Row.RowType == DataControlRowType.Footer)//表尾加合计
            {
                e.Row.Cells[1].Text = "合计:";
                e.Row.Cells[1].HorizontalAlign = HorizontalAlign.Center;
                e.Row.Cells[11].Text = hfdnum.Value;
                e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Center;
            }
        }

      

        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;

        }

        protected void TypeOrOrderBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }

        protected void QueryButton_Click(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            bindGrid();
            refreshStyle();
        }

        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {
            ClearStrCondtion();
            resetSubcondition();
        }




        private void ClearStrCondtion()
        {
            DropDownListChange.ClearSelection();
            DropDownListChange.Items[0].Selected = true;

            DropDownListStoUse.ClearSelection();
            DropDownListStoUse.Items[0].Selected = true;

            DropDownListXSReplace.ClearSelection();
            DropDownListXSReplace.Items[0].Selected = true;

            DropDownListReplace.ClearSelection();
            DropDownListReplace.Items[0].Selected = true;

            DropDownListDDReplace.ClearSelection();
            DropDownListDDReplace.Items[0].Selected = true;

            //是否下推
            DropDownListPushState.ClearSelection();
            DropDownListDDReplace.Items[0].Selected = true;


            TextBoxPro.Text = string.Empty;
            TextBoxEng.Text = string.Empty;
            TextBoxDep.Text = string.Empty;
            TextBoxMan.Text = string.Empty;
            DropDownListMarType.ClearSelection();
            DropDownListMarType.Items[0].Selected = true;
            TextBoxMarID.Text = string.Empty;
            TextBoxMarNM.Text = string.Empty;
            TextBoxGG.Text = string.Empty;
            TextBoxCZ.Text = string.Empty;
            TextBoxStartDate.Text = string.Empty;
            TextBoxEndTime.Text = string.Empty;
            TextBoxPTC.Text = string.Empty;
            TextBoxTUHAO.Text = string.Empty;

        }



        private string CreateStrCondition()
        {
            string StrCondition = string.Empty;

            //时间条件

            if ((TextBoxStartDate.Text.Trim() == "") && (TextBoxEndTime.Text.Trim() == ""))
            {
                StrCondition += " sqrtime >= '" + TextBoxStartDate.Text.Trim() + "'";
            }
            if ((TextBoxStartDate.Text.Trim() == "") && (TextBoxEndTime.Text.Trim() != ""))
            {
                StrCondition += " sqrtime <= '" + TextBoxEndTime.Text.Trim() + "'";
            }
            if ((TextBoxStartDate.Text.Trim() != "") && (TextBoxEndTime.Text.Trim() == ""))
            {
                StrCondition += " sqrtime >= '" + TextBoxStartDate.Text.Trim() + "'";
            }
            if ((TextBoxStartDate.Text.Trim() != "") && (TextBoxEndTime.Text.Trim() != ""))
            {
                StrCondition += " sqrtime between '" + TextBoxStartDate.Text.Trim() + "' and '" + TextBoxEndTime.Text.Trim() + "'";
            }


            //变更
            if (StrCondition != string.Empty && DropDownListChange.SelectedValue != string.Empty)
            {
                StrCondition += " and changestate='" + DropDownListChange.SelectedValue + "'";
            }
            else if (StrCondition == string.Empty && DropDownListChange.SelectedValue != string.Empty)
            {
                StrCondition += " changestate='" + DropDownListChange.SelectedValue + "'";
            }

            //占用
            if (StrCondition != string.Empty && DropDownListStoUse.SelectedValue != string.Empty)
            {
                StrCondition += " and stousestate='" + DropDownListStoUse.SelectedValue + "'";
            }
            else if (StrCondition == string.Empty && DropDownListStoUse.SelectedValue != string.Empty)
            {
                StrCondition += " stousestate='" + DropDownListStoUse.SelectedValue + "'";
            }

            //相似占用
            if (StrCondition != string.Empty && DropDownListXSReplace.SelectedValue != string.Empty)
            {
                StrCondition += " and xsreplacestate='" + DropDownListXSReplace.SelectedValue + "'";
            }
            else if (StrCondition == string.Empty && DropDownListXSReplace.SelectedValue != string.Empty)
            {
                StrCondition += " xsreplacestate='" + DropDownListXSReplace.SelectedValue + "'";
            }

            //代用
            if (StrCondition != string.Empty && DropDownListReplace.SelectedValue != string.Empty)
            {
                StrCondition += " and replacestate='" + DropDownListReplace.SelectedValue + "'";
            }
            else if (StrCondition == string.Empty && DropDownListReplace.SelectedValue != string.Empty)
            {
                StrCondition += " replacestate='" + DropDownListReplace.SelectedValue + "'";
            }

            //订单代用
            if (StrCondition != string.Empty && DropDownListDDReplace.SelectedValue != string.Empty)
            {
                StrCondition += " and ddreplacestate='" + DropDownListDDReplace.SelectedValue + "'";
            }
            else if (StrCondition == string.Empty && DropDownListDDReplace.SelectedValue != string.Empty)
            {
                StrCondition += " ddreplacestate='" + DropDownListDDReplace.SelectedValue + "'";
            }
            if (DropDownListPushState.SelectedValue != "0")
            {
                if (DropDownListPushState.SelectedValue == "1")
                {
                    //未下推
                    if (StrCondition != string.Empty)
                    {
                        StrCondition += " and purstate < '3' and prstate<'5' ";
                    }
                    else if (StrCondition == string.Empty)
                    {
                        StrCondition += " purstate < '3' and prstate<'5' ";
                    }

                }
                else if (DropDownListPushState.SelectedValue == "2")
                {
                    //已下推
                    if (StrCondition != string.Empty)
                    {
                        StrCondition += " and purstate > '3'";
                    }
                    else if (StrCondition == string.Empty)
                    {
                        StrCondition += " purstate > '3' ";
                    }
                }

            }



            //项目名称
            if (StrCondition != string.Empty && TextBoxPro.Text.Trim() != string.Empty)
            {
                StrCondition += " and pjnm like '%" + TextBoxPro.Text.Trim() + "%'";
            }
            else if (StrCondition == string.Empty && TextBoxPro.Text.Trim() != string.Empty)
            {
                StrCondition += " pjnm like '%" + TextBoxPro.Text.Trim() + "%'";
            }
            //工程名称
            if (StrCondition != string.Empty && TextBoxEng.Text.Trim() != string.Empty)
            {
                StrCondition += " and engnm like '%" + TextBoxEng.Text.Trim() + "%'";
            }
            else if (StrCondition == string.Empty && TextBoxEng.Text.Trim() != string.Empty)
            {
                StrCondition += " engnm like '%" + TextBoxEng.Text.Trim() + "%'";
            }
            //部门
            if (StrCondition != string.Empty && TextBoxDep.Text.Trim() != string.Empty)
            {
                StrCondition += " and depnm like '%" + TextBoxDep.Text.Trim() + "%'";
            }
            else if (StrCondition == string.Empty && TextBoxDep.Text.Trim() != string.Empty)
            {
                StrCondition += " depnm like '%" + TextBoxDep.Text.Trim() + "%'";
            }
            //技术员
            if (StrCondition != string.Empty && TextBoxMan.Text.Trim() != string.Empty)
            {
                StrCondition += " and sqrnm like '%" + TextBoxMan.Text.Trim() + "%'";
            }
            else if (StrCondition == string.Empty && TextBoxMan.Text.Trim() != string.Empty)
            {
                StrCondition += " sqrnm like '%" + TextBoxMan.Text.Trim() + "%'";
            }

            //物料类型
            if (StrCondition != string.Empty && DropDownListMarType.SelectedValue != string.Empty)
            {
                StrCondition += " and PUR_MASHAPE= '" + DropDownListMarType.SelectedValue + "'";
            }
            else if (StrCondition == string.Empty && DropDownListMarType.SelectedValue != string.Empty)
            {
                StrCondition += " PUR_MASHAPE = '" + DropDownListMarType.SelectedValue + "'";
            }
            //标识号
            if (StrCondition != string.Empty && TextBoxTUHAO.Text.Trim() != string.Empty)
            {
                StrCondition += " and PUR_TUHAO like '%" + TextBoxTUHAO.Text.Trim() + "%'";
            }
            else if (StrCondition == string.Empty && TextBoxTUHAO.Text.Trim() != string.Empty)
            {
                StrCondition += " PUR_TUHAO like '%" + TextBoxTUHAO.Text.Trim() + "%'";
            }

            //计划跟踪号
            if (StrCondition != string.Empty && TextBoxPTC.Text.Trim() != string.Empty)
            {
                StrCondition += " and ptcode like '%" + TextBoxPTC.Text.Trim() + "%'";
            }
            else if (StrCondition == string.Empty && TextBoxPTC.Text.Trim() != string.Empty)
            {
                StrCondition += " ptcode like '%" + TextBoxPTC.Text.Trim() + "%'";
            }

            if (CheckBoxReplace.Checked)
            {
                //表明查询相似占用，查代用之前的物料的规格型号  01.07.00422

                string ReplaceCondition = "";

                if (ReplaceCondition != string.Empty && TextBoxMarID.Text.Trim() != string.Empty)
                {
                    ReplaceCondition += " and marid like '%" + TextBoxMarID.Text.Trim() + "%'";
                }
                else if (ReplaceCondition == string.Empty && TextBoxMarID.Text.Trim() != string.Empty)
                {
                    ReplaceCondition += " marid like '%" + TextBoxMarID.Text.Trim() + "%'";
                }
                //物料名称
                if (ReplaceCondition != string.Empty && TextBoxMarNM.Text.Trim() != string.Empty)
                {
                    ReplaceCondition += " and marnm like '%" + TextBoxMarNM.Text.Trim() + "%'";
                }
                else if (ReplaceCondition == string.Empty && TextBoxMarNM.Text.Trim() != string.Empty)
                {
                    ReplaceCondition += " marnm like '%" + TextBoxMarNM.Text.Trim() + "%'";
                }
                //规格
                if (ReplaceCondition != string.Empty && TextBoxGG.Text.Trim() != string.Empty)
                {
                    ReplaceCondition += " and marguige like '%" + TextBoxGG.Text.Trim() + "%'";
                }
                else if (ReplaceCondition == string.Empty && TextBoxGG.Text.Trim() != string.Empty)
                {
                    ReplaceCondition += " marguige like '%" + TextBoxGG.Text.Trim() + "%'";
                }

                //材质
                if (ReplaceCondition != string.Empty && TextBoxCZ.Text.Trim() != string.Empty)
                {
                    ReplaceCondition += " and marcaizhi like '%" + TextBoxCZ.Text.Trim() + "%'";
                }
                else if (ReplaceCondition == string.Empty && TextBoxCZ.Text.Trim() != string.Empty)
                {
                    ReplaceCondition += " marcaizhi like '%" + TextBoxCZ.Text.Trim() + "%'";
                }

                if (StrCondition != string.Empty && ReplaceCondition != string.Empty)
                {
                    StrCondition += " and ptcode in (select ptcode FROM View_TBPC_MARREPLACE_total_all_detail where " + ReplaceCondition + "  )";
                }
                else if (StrCondition == string.Empty && ReplaceCondition != string.Empty)
                {
                    StrCondition += " ptcode in (select ptcode FROM View_TBPC_MARREPLACE_total_all_detail where " + ReplaceCondition + "  )";
                }

            }
            else
            {
                //表明既不查相似占用也不查代用

                //物料代码
                if (StrCondition != string.Empty && TextBoxMarID.Text.Trim() != string.Empty)
                {
                    StrCondition += " and marid like '%" + TextBoxMarID.Text.Trim() + "%'";
                }
                else if (StrCondition == string.Empty && TextBoxMarID.Text.Trim() != string.Empty)
                {
                    StrCondition += " marid like '%" + TextBoxMarID.Text.Trim() + "%'";
                }
                //物料名称
                if (StrCondition != string.Empty && TextBoxMarNM.Text.Trim() != string.Empty)
                {
                    StrCondition += " and marnm like '%" + TextBoxMarNM.Text.Trim() + "%'";
                }
                else if (StrCondition == string.Empty && TextBoxMarNM.Text.Trim() != string.Empty)
                {
                    StrCondition += " marnm like '%" + TextBoxMarNM.Text.Trim() + "%'";
                }
                //规格
                if (StrCondition != string.Empty && TextBoxGG.Text.Trim() != string.Empty)
                {
                    StrCondition += " and margg like '%" + TextBoxGG.Text.Trim() + "%'";
                }
                else if (StrCondition == string.Empty && TextBoxGG.Text.Trim() != string.Empty)
                {
                    StrCondition += " margg like '%" + TextBoxGG.Text.Trim() + "%'";
                }
                //材质
                if (StrCondition != string.Empty && TextBoxCZ.Text.Trim() != string.Empty)
                {
                    StrCondition += " and marcz like '%" + TextBoxCZ.Text.Trim() + "%'";
                }
                else if (StrCondition == string.Empty && TextBoxCZ.Text.Trim() != string.Empty)
                {
                    StrCondition += " marcz like '%" + TextBoxCZ.Text.Trim() + "%'";
                }

            }

            string SubCondtion = GetSubCondtion();

            if (SubCondtion != "")
            {

                //AND可以变化


                if (SubCondtion != "")

                    StrCondition += DropDownListFatherLogic.SelectedValue + " (" + SubCondtion + ")";

            }
            else
            {
                if (SubCondtion != "")
                    StrCondition += SubCondtion;
            }


            return StrCondition;

        }


        #region ShowModual (无用)


        [System.Web.Services.WebMethodAttribute(),
           System.Web.Script.Services.ScriptMethodAttribute()]

        public static string GetDDInfo(string contextKey)
        {
            StringBuilder sTemp = new StringBuilder();

            sTemp.Append("<table style='background-color:#f3f3f3; border: #A8B7EC 3px solid;font-size:10pt; font-family:Verdana;' cellspacing='0' cellpadding='3'>");
            sTemp.Append("<tr><td colspan='15' style='background-color:#A8B7EC; color:white;'><b>计划跟踪号:&nbsp;" + contextKey + "</b></td>");
            sTemp.Append("<td style='background-color:#A8B7EC; color:white;' align='right'  valign='middle'><a onclick='document.body.click(); return false;' style='cursor: pointer;color: #FFFFFF; text-align: center; text-decoration: none; padding: 5px;' title='关闭'><b>X</b></a></td></tr>");

            string sql_gotopur = "select ptcode from View_TBPC_PURCHASEPLAN where ptcode='" + contextKey + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_gotopur);
            if (dr.HasRows)
            {
                sTemp.Append("<tr><td><b></b></td>");
                sTemp.Append("<td align='center'><b>订单编号</b></td>");
                sTemp.Append("<td align='center'><b>供应商</b></td>");
                sTemp.Append("<td align='center'><b>制单人</b></td>");
                sTemp.Append("<td align='center'><b>日期</b></td>");
                sTemp.Append("<td align='center'><b>物料代码</b></td>");
                sTemp.Append("<td align='center'><b>物料名称</b></td>");
                sTemp.Append("<td align='center'><b>材质</b></td>");
                sTemp.Append("<td align='center'><b>规格</b></td>");
                sTemp.Append("<td align='center'><b>国标</b></td>");
                sTemp.Append("<td align='center'><b>长(mm)</b></td>");
                sTemp.Append("<td align='center'><b>宽(mm)</b></td>");
                sTemp.Append("<td align='center'><b>数量</b></td>");
                sTemp.Append("<td align='center'><b>单位</b></td>");
                sTemp.Append("<td align='center'><b>辅助数量</b></td>");
                sTemp.Append("<td align='center'><b>辅助单位</b></td></tr>");



                string sql = "select '订单执行量' as RowName,orderno,suppliernm,shrnm,left(shtime,10) as date,marid,marnm,marcz,margg,margb,length,width,zxnum,marunit,zxfznum,marfzunit from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where ptcode='" + contextKey + "'" +
                    " union all " +
                    "select '订单到货量' as RowName,'' as orderno,'' as suppliernm,'' as shrnm,left(recdate,10) as date,marid,marnm,marcz,margg,margb,length,width,recgdnum,marunit,recgdfznum,marfzunit from View_TBPC_PURORDERDETAIL_PLAN_TOTAL where ptcode='" + contextKey + "'";

                System.Data.DataTable dt_sql = DBCallCommon.GetDTUsingSqlText(sql);

                if (dt_sql.Rows.Count > 0)
                {
                    //订单状态
                    string sql_order_state = " select '订单状态',case when isnull(num,0)=0 then '未下推订单' when recgdnum=0 and detailstate='1' and detailcstate='0' then'未到货' when detailstate='2' and detailcstate='0' then '全部到货'  when (recgdnum-num)<0 and detailstate='1' and detailcstate='0' then '部分到货' when  detailstate='1' and detailcstate='1' then '手动关闭' when  detailstate='4' and detailcstate='0' then '变更取消' end  from View_TBPC_PURORDERDETAIL_PLAN_MPLAN where ptcode='" + contextKey + "'";

                    System.Data.DataTable dt_sql_order_state = DBCallCommon.GetDTUsingSqlText(sql_order_state);

                    for (int i = 0; i < dt_sql.Rows.Count; i++)
                    {
                        sTemp.Append("<tr style='border-width:1px;border-color:Black;'>");

                        for (int j = 0; j < dt_sql.Columns.Count; j++)
                        {
                            if (j == 0)
                            {
                                sTemp.Append("<td  style='white-space:pre;' align='center'><b>" + dt_sql.Rows[i][j].ToString() + "</b></td>");
                            }
                            else
                            {
                                sTemp.Append("<td  style='white-space:pre;' align='center'>" + dt_sql.Rows[i][j].ToString() + "</td>");
                            }
                        }
                        sTemp.Append("</tr>");
                    }

                    if (dt_sql_order_state.Rows.Count == 0)
                    {
                        sTemp.Append("<tr style='border-width:1px'>");
                        sTemp.Append("<td  style='white-space:pre;' align='center'><b>订单状态</b></td>");
                        sTemp.Append("<td  style='white-space:pre;color:Red;' align='center' colspan='15'><b>订单未生成</b></td></tr>");
                    }
                    else
                    {
                        sTemp.Append("<tr style='border-width:1px'>");
                        sTemp.Append("<td  style='white-space:pre;' align='center'><b>" + dt_sql_order_state.Rows[0][0].ToString() + "</b></td>");
                        sTemp.Append("<td  style='white-space:pre;color:Red;' align='center' colspan='15'><b>" + dt_sql_order_state.Rows[0][1].ToString() + "</b></td></tr>");
                    }
                }
                else
                {
                    string sql_order_state = " select '订单状态',case when purstate='0' then '初始化' when purstate='1' then '(占用)审核通过' when purstate='2' then '相似代用' when purstate='3' then '下推'  when purstate='4' then '分工' when  purstate='5' then '代用' when purstate='6' then '询比价' when purstate='7' then '到订单' end  from View_TBPC_PURCHASEPLAN where ptcode='" + contextKey + "'";

                    System.Data.DataTable dt_sql_order_state = DBCallCommon.GetDTUsingSqlText(sql_order_state);

                    sTemp.Append("<tr style='border-width:1px'>");
                    sTemp.Append("<td  style='white-space:pre;' align='center'><b>" + dt_sql_order_state.Rows[0][0].ToString() + "</b></td>");
                    sTemp.Append("<td  style='white-space:pre;color:Red;' align='center' colspan='15'><b>" + dt_sql_order_state.Rows[0][1].ToString() + "</b></td></tr>");
                }
            }

            else
            {
                sTemp.Append("<tr><td colspan='16'><i>未下推采购...</i></td></tr>");
            }

            sTemp.Append("</table>");

            return sTemp.ToString();



        }
        #endregion


        #region 导出

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string ordertype = string.Empty;

            if (RadioButtonListOrderBy.SelectedValue == "1")
            {
                ordertype = "DESC";
            }
            else
            { 
                ordertype = "ASC"; 
            }

            string sqlstr = string.Empty;

            if (CreateStrCondition() == string.Empty)
            {
                sqlstr = "select ptcode as 计划跟踪号,marid as 物料编码,marnm as 物料名称,margg as 规格,marcz as 材质,margb as 国标,标识号 AS PUR_TUHAO,length as 长,width as 宽,marunit as 单位,num as 计划数量,cast(fznum as float) AS 辅助数量,changestate as 变更,stousestate as 占用,xsreplacestate as 相似占用,replacestate as 代用,ddreplacestate as 订单代用,pjnm as 项目,engnm as 工程,PUR_MASHAPE as 物料类型,depnm as 部门,sqrnm as 技术员,LEFT(sqrtime,10) AS 计划日期,PUR_ZYDY AS 行关闭备注,purnote AS 备注 from View_SM_PURCHASEPLAN_VIEW  order by " + DropDownListType.SelectedValue + " " + ordertype;

            }
            else
            {
                sqlstr = "select ptcode as 计划跟踪号,marid as 物料编码,marnm as 物料名称,margg as 规格,marcz as 材质,margb as 国标,标识号 AS PUR_TUHAO,length as 长,width as 宽,marunit as 单位,num as 计划数量,cast(fznum as float) AS 辅助数量,changestate as 变更,stousestate as 占用,xsreplacestate as 相似占用,replacestate as 代用,ddreplacestate as 订单代用,pjnm as 项目,engnm as 工程,PUR_MASHAPE as 物料类型,depnm as 部门,sqrnm as 技术员,LEFT(sqrtime,10) AS 计划日期,PUR_ZYDY AS 行关闭备注,purnote AS 备注 from View_SM_PURCHASEPLAN_VIEW where " + CreateStrCondition() + " order by " + DropDownListType.SelectedValue + " " + ordertype;

            }
            ExportExcel(sqlstr);

        }

        private void ExportExcel(string strsql)
        {
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(strsql);

            ApplicationClass ac = new ApplicationClass();
            ac.Visible = false;     // Excel不显示  
            Workbook wkbook = ac.Workbooks.Add(true);   // 添加工作簿  
            Worksheet wksheet = (Worksheet)wkbook.ActiveSheet;    // 获得工作表  

            ac.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            int rowCount = dt.Rows.Count;

            int colCount = dt.Columns.Count;

            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            wksheet.get_Range("A1", wksheet.Cells[1 + rowCount, colCount + 1]).NumberFormatLocal = "@";

            object[,] dataArray = new object[rowCount, colCount + 1];

            for (int i = 0; i < rowCount; i++)
            {
                dataArray[i, 0] = (object)(i + 1);

                for (int j = 0; j < colCount; j++)
                {

                    dataArray[i, j + 1] = dt.Rows[i][j];

                }
            }

            //设置表头
            wksheet.Cells[1, 1] = "序号";

            for (int i = 0; i < dt.Columns.Count; i++)
            {
                wksheet.Cells[1, i + 2] = dt.Columns[i].ColumnName;
              
                if (dt.Columns[i].ColumnName == "计划数量")
                {
                    wksheet.get_Range(getExcelColumnLabel(i + 2) + "1", wksheet.Cells[1 + rowCount, i + 2]).NumberFormatLocal = "G/通用格式";
                }
            }

            wksheet.get_Range("A2", wksheet.Cells[1 + rowCount, colCount + 1]).Value2 = dataArray;

            //设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/SM_Data/ExportFile/" + "需要计划" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");
            wkbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wkbook.Close(Type.Missing, Type.Missing, Type.Missing);
            ac.Quit();
            wkbook = null;
            ac = null;
            GC.Collect();    // 强制垃圾回收  

            if (File.Exists(filename))
            {
                DownloadFile.Send(Context, filename);
            }
        }

        //将EXCEL列数转换为列字母
        private string getExcelColumnLabel(int index)
        {
            String rs = "";

            do
            {
                index--;
                rs = ((char)(index % 26 + (int)'A')) + rs;
                index = (int)((index - index % 26) / 26);
            } while (index > 0);

            return rs;
        }

        #endregion


        #region 条件框


        private void bindGV()
        {
            GridViewSearch.DataSource = CreateTable();

            GridViewSearch.DataBind();
        }
        private System.Data.DataTable CreateTable()
        {
            System.Data.DataTable dt = new System.Data.DataTable();

            dt.Columns.Add(new DataColumn("index", typeof(int)));


            for (int i = 0; i < 10; i++)
            {
                DataRow row = dt.NewRow();
                row["index"] = i;
                dt.Rows.Add(row);
            }

            return dt;
        }



        private Dictionary<string, string> bindItemList()
        {
            //PUR_ZYDY

            Dictionary<string, string> ItemList = new Dictionary<string, string>();

            ItemList.Add("NO", "");
            ItemList.Add("pjnm", "项目名称");
            ItemList.Add("engnm", "工程名称");
            ItemList.Add("marid", "物料编码");
            ItemList.Add("marnm", "物料名称");
            ItemList.Add("margg", "规格型号");
            ItemList.Add("margb", "国标");
            ItemList.Add("marcz", "材质");
            ItemList.Add("PUR_TUHAO", "标识号");
            ItemList.Add("ptcode", "计划跟踪号");
            ItemList.Add("sqrtime", "计划时间");
            ItemList.Add("depnm", "部门");
            ItemList.Add("sqrnm", "技术员");            
            ItemList.Add("length", "长");
            ItemList.Add("width", "宽");
            ItemList.Add("num", "计划数量");
            ItemList.Add("fznum", "辅助数量");
            ItemList.Add("changestate", "变更");
            ItemList.Add("stousestate", "占用");
            ItemList.Add("xsreplacestate", "相似占用");
            ItemList.Add("replacestate", "代用");
            ItemList.Add("ddreplacestate", "订单代用");

            return ItemList;
        }

        protected void GridViewSearch_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                ddl.DataTextField = "value";
                ddl.DataValueField = "key";
                ddl.DataSource = bindItemList();
                ddl.DataBind();

                if (gr.RowIndex == 0)
                {
                    (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Visible = false;
                }
            }
        }

        protected string GetSubCondtion()
        {
            string subCondition = "";

            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                if (gr.RowIndex == 0)
                {

                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                    if (ddl.SelectedValue != "NO")
                    {
                        System.Web.UI.WebControls.TextBox txtValue = (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox);

                        DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                        subCondition = ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim());
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                    if (ddl.SelectedValue != "NO")
                    {
                        DropDownList ddlLogic = (gr.FindControl("DropDownListLogic") as DropDownList);

                        System.Web.UI.WebControls.TextBox txtValue = (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox);

                        DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                        subCondition += " " + ddlLogic.SelectedValue + " " + ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim());
                    }

                    else
                    {
                        break;
                    }
                }
            }
            return subCondition;
        }

        private string ConvertRelation(string field, string relation, string fieldValue)
        {
            string obj = string.Empty;



            switch (relation)
            {
                case "0":
                    {
                        //包含

                        obj = field + "  LIKE  '%" + fieldValue + "%'";
                        break;
                    }
                case "1":
                    {
                        //等于
                        obj = field + "  =  '" + fieldValue + "'";
                        break;
                    }
                case "2":
                    {
                        //不等于
                        obj = field + "  !=  '" + fieldValue + "'";
                        break;
                    }
                case "3":
                    {
                        //大于
                        obj = field + "  >  '" + fieldValue + "'";
                        break;
                    }
                case "4":
                    {
                        //大于或等于
                        obj = field + "  >=  '" + fieldValue + "'";
                        break;
                    }
                case "5":
                    {
                        //小于
                        obj = field + "  <  '" + fieldValue + "'";
                        break;
                    }
                case "6":
                    {
                        //小于或等于
                        obj = field + "  <=  '" + fieldValue + "'";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return obj;
        }

        private void resetSubcondition()
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                DropDownList ddl = gr.FindControl("DropDownListName") as DropDownList;
                foreach (ListItem lt in ddl.Items)
                {
                    if (lt.Selected)
                        lt.Selected = false;
                }
                ddl.Items[0].Selected = true;
                (gr.FindControl("TextBoxValue") as System.Web.UI.WebControls.TextBox).Text = string.Empty; ;
            }

            refreshStyle();
        }
        private void refreshStyle()
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                if ((gr.FindControl("DropDownListName") as DropDownList).SelectedValue != "NO")
                {
                    if (gr.RowIndex != 0)
                    {
                        (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "block");
                        (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
                    }

                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_name") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_relation") as System.Web.UI.WebControls.TextBox).Style.Add("display", "none");
                }
                else
                {
                    (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_logic") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_name") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_relation") as System.Web.UI.WebControls.TextBox).Style.Add("display", "block");
                }
            }
        }

        #endregion



    }
}
