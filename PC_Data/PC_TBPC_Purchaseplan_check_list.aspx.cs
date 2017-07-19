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
using System.IO;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchaseplan_check_list : System.Web.UI.Page
    {
        double sumnum = 0;//合计数量
        protected void Page_Load(object sender, EventArgs e)
        {
            //仓库
            string action = string.Empty;
            btn_delete.Attributes.Add("OnClick", "Javascript:return confirm('会删除整批物料，是否确定删除?');");
            if (!IsPostBack)
            {
                if (Request.QueryString["action"] != null)
                    action = Request.QueryString["action"];
                if (action == "ws")
                {
                    radio_view0.Visible = false;
                    radio_view0.Checked = false;
                    radio_view1.Checked = true;
                }
                initpageinfo();
                getArticle();
                //CheckUser(ControlFinder);
            }
            getwlzywtj();
            getwlzydsh();
            getwlzybh();
        }
        //绑定dropdownlist
        private void initpageinfo()
        {
            string sqltext = "";
            sqltext = "select distinct PR_REVIEWA,PR_REVIEWANM from View_TBPC_MARSTOUSE_TOTAL_ALL where PR_REVIEWA is not null and PR_REVIEWA<>''";
            string dataText = "PR_REVIEWANM";
            string dataValue = "PR_REVIEWA";
            DBCallCommon.BindDdl(drp_zdr, sqltext, dataText, dataValue);
            drp_zdr.SelectedIndex = 0;

            sqltext = "select distinct PR_REVIEWB,PR_REVIEWBNM from View_TBPC_MARSTOUSE_TOTAL_ALL where PR_REVIEWB is not null and PR_REVIEWB<>''";
            dataText = "PR_REVIEWBNM";
            dataValue = "PR_REVIEWB";
            DBCallCommon.BindDdl(drp_shr, sqltext, dataText, dataValue);
            drp_shr.SelectedIndex = 0;
        }

        private void getArticle()      //取得Article数据
        {
            int cup = Convert.ToInt32(this.lb_CurrentPage.Text);  //当前页数,初始化为地1页
            PagedDataSource ps = new PagedDataSource();
            ps.DataSource = CreateDataSource().DefaultView;
            ps.AllowPaging = true;
            ps.PageSize = 100;     //每页显示的数据的行数
            ps.CurrentPageIndex = cup - 1;
            lb_count.Text = ps.DataSourceCount.ToString(); //获取记录总数
            lb_page.Text = ps.PageCount.ToString(); //获取总页数

            this.DropDownList1.Items.Clear();
            for (int i = 1; i < ps.PageCount + 1; i++)
            {
                this.DropDownList1.Items.Add(i.ToString());
            }
            LinkUp.Enabled = true;
            LinkDown.Enabled = true;

            try
            {
                DropDownList1.SelectedIndex = Convert.ToInt32(cup.ToString()) - 1;
                tbpc_pchsplanrvwchecklistRepeater.DataSource = ps;
                tbpc_pchsplanrvwchecklistRepeater.DataBind();
                if (tbpc_pchsplanrvwchecklistRepeater.Items.Count > 0)
                {
                    NoDataPane.Visible = false;
                }
                else
                {
                    NoDataPane.Visible = true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public System.Data.DataTable CreateDataSource()
        {
            string sqltext = "";
            sqltext = "SELECT PR_PCODE,PR_REVIEWA,PR_REVIEWANM,PJ_NAME,TSA_ENGNAME," +
                      "PR_REVIEWATIME,PR_REVIEWB,PR_REVIEWBNM,allshstate,PUR_ISSTOUSE," +
                      "PR_REVIEWBTIME,PR_STATE,PR_NOTE,ptcode,marid,marnm,margg,marcz,margb,num,usenum,time  " +
                      "FROM (select left(PR_REVIEWATIME,10) as time,* from View_TBPC_MARSTOUSE_TOTAL_ALL)t ORDER BY PR_REVIEWATIME DESC,ptcode asc";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DataView dv = null;
            if (Radio_quanbu.Checked)//全部
            {
                dv = dt.DefaultView;
                dv.RowFilter = "";
                dt = dv.ToTable();
            }
            if (Radio_my.Checked)//我的任务
            {
                if (Session["POSITION"].ToString().Trim() == "0501")
                {
                    dv = dt.DefaultView;
                    dv.RowFilter = "allshstate='0' and time>='2015-07-15'";
                    dt = dv.ToTable();
                }
                else
                {
                    dv = dt.DefaultView;
                    dv.RowFilter = "PR_REVIEWA='" + Session["UserID"].ToString() + "' or PR_REVIEWB='" + Session["UserID"].ToString() + "'";
                    dt = dv.ToTable();
                }
            }

            if (radio_view0.Checked)//未提交
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PR_STATE='1'";
                dt = dv.ToTable();
            }
            if (radio_view1.Checked)//待审核
            {
                dv = dt.DefaultView;
                dv.RowFilter = "allshstate='0'";
                dt = dv.ToTable();
            }
            if (radio_view2.Checked)//已审核
            {
                dv = dt.DefaultView;
                dv.RowFilter = "allshstate='2'";
                dt = dv.ToTable();
            }
            if (radio_view3.Checked)//驳回
            {
                dv = dt.DefaultView;
                dv.RowFilter = "allshstate='1'";
                dt = dv.ToTable();
            }
            if (SFTZ.SelectedIndex == 0)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_ISSTOUSE='0'";
                dt = dv.ToTable();
            }
            else if (SFTZ.SelectedIndex == 1)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_ISSTOUSE='1'";
                dt = dv.ToTable();
            }
            else if (SFTZ.SelectedIndex == 2)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_ISSTOUSE='2'";
                dt = dv.ToTable();
            }
            if (tb_pj.Text != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PJ_NAME like '%" + tb_pj.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_eng.Text != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "TSA_ENGNAME like '%" + tb_eng.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_name.Text != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "marnm like '%" + tb_name.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_StartTime.Text != "")
            {
                string startdate = tb_StartTime.Text.ToString() == "" ? "1900-01-01" : tb_StartTime.Text.ToString();
                dv = dt.DefaultView;
                dv.RowFilter = "PR_REVIEWATIME>='" + startdate + "'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_EndTime.Text != "")
            {
                string enddate = tb_EndTime.Text.ToString() == "" ? "2100-01-01" : tb_EndTime.Text.ToString();
                enddate = enddate + " 23:59:59";
                dv = dt.DefaultView;
                dv.RowFilter = "PR_REVIEWATIME<='" + enddate + "'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_orderno.Text != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PR_PCODE LIKE '%" + tb_orderno.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_cz.Text != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "marcz like '%" + tb_cz.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_ptc.Text != "")//计划号
            {
                dv = dt.DefaultView;
                dv.RowFilter = "ptcode like '%" + tb_ptc.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_marid.Text != "")//物料编码
            {
                dv = dt.DefaultView;
                dv.RowFilter = "marid like '%" + tb_marid.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            //if (tb_gb.Text != "")
            //{
            //    dv = dt.DefaultView;
            //    dv.RowFilter = "margb like '%" + tb_gb.Text + "%'"; //对dataView进行筛选 
            //    dt = dv.ToTable();
            //}


            if (tb_gg.Text != "")//物料规格
            {
                string margg = tb_gg.Text.Trim();
                margg = DvRowFilter(margg);
                dv = dt.DefaultView;
                dv.RowFilter = "margg like '%" + margg + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }

            if (tb_cz.Text != "")//物料材质
            {
                dv = dt.DefaultView;
                dv.RowFilter = "marcz like '%" + tb_cz.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }

            if (drp_zdr.SelectedIndex != 0)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PR_REVIEWA = '" + drp_zdr.SelectedValue + "'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (drp_shr.SelectedIndex != 0)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PR_REVIEWB = '" + drp_shr.SelectedValue + "'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            return dt;

        }

        public static string DvRowFilter(string rowFilter) //特殊字符转义
        {

            return rowFilter.Replace("[", "[[ ")
            .Replace("]", " ]]")
            .Replace("*", "[*]")
            .Replace("%", "[%]")
            .Replace("[[ ", "[[]")
            .Replace(" ]]", "[]]")
            .Replace("\'", "''");
        }

        protected void LinkDown_Click(object sender, EventArgs e) //下一页按钮代码
        {
            if (lb_CurrentPage.Text.ToString() != lb_page.Text.ToString())
            {
                lb_CurrentPage.Text = Convert.ToString(Convert.ToInt32(lb_CurrentPage.Text) + 1);
                DropDownList1.SelectedIndex = Convert.ToInt32(lb_CurrentPage.Text) - 1;
                getArticle();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经是最后一页');", true);
            }

        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e) //跳转到指定页代码
        {
            int page = Convert.ToInt16((DropDownList1.SelectedItem.Value));
            lb_CurrentPage.Text = page.ToString();
            getArticle();
        }
        protected void LinkUp_Click(object sender, EventArgs e)  //上一页按钮代码
        {
            if (Convert.ToInt32(lb_CurrentPage.Text) > 1)
            {
                lb_CurrentPage.Text = Convert.ToString(Convert.ToInt32(lb_CurrentPage.Text) - 1);
                DropDownList1.SelectedIndex = Convert.ToInt32(lb_CurrentPage.Text) - 1;
                getArticle();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经是第一页');", true);
            }
        }
        protected void LinkFirst_Click(object sender, EventArgs e)  //跳到第一页代码
        {
            if (lb_CurrentPage.Text != "1")
            {
                lb_CurrentPage.Text = "1";
                getArticle();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经是第一页');", true);
            }
        }
        protected void LinkLast_Click(object sender, EventArgs e)  //跳到最后一页代码
        {
            if (lb_CurrentPage.Text.ToString() != lb_page.Text.ToString())
            {
                lb_CurrentPage.Text = lb_page.Text.ToString();
                getArticle();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经是最后一页');", true);
            }

        }
        protected void Radio_quanbu_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }
        protected void Radio_my_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }
        protected void SFTZ_Changed(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }
        protected void radio_view0_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }
        protected void radio_view1_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }
        protected void radio_view2_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }
        protected void radio_view3_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }


        //protected void btn_fanshen_Click(object sender, EventArgs e)//对反审有效，通过时先检测是否分工，未分工时，直接更改状态为待审核状态，已分工时提示不能撤销；驳回是返回到待审核状态
        //{
        //    string pcode = "";
        //    string pcode1 = "";
        //    string sqltext1;
        //    string sqltext2;
        //    int i = 0;
        //    foreach (RepeaterItem Reitem in tbpc_pchsplanrvwchecklistRepeater.Items)
        //    {
        //        System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
        //        if (cbx.Checked)
        //        {
        //            pcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PR_PCODE")).Text.ToString();
        //            if (pcode != pcode1)
        //            {
        //                pcode1 = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PR_PCODE")).Text.ToString();
        //                i++;
        //                string sql = "select PR_REVIEWB from View_TBPC_MARSTOUSETOTAL where PR_PCODE='" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("PR_PCODE")).Text.ToString() + "'";
        //                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
        //                if (dr.Read())
        //                {
        //                    string shrname = dr["PR_REVIEWB"].ToString();
        //                    string loginname = Session["UserID"].ToString();
        //                    if (shrname != loginname)
        //                    {
        //                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有权限进行反审！');", true);
        //                    }
        //                    else
        //                    {
        //                        if (radio_view3.Checked)//已驳回
        //                        {
        //                            sqltext1 = "UPDATE TBPC_MARSTOUSETOTAL SET PR_STATE='2',PR_REVIEWBTIME='',PR_REVIEWBADVC='' WHERE PR_PCODE='" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("PR_PCODE")).Text.ToString() + "'";
        //                            //sqltext2 = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='0' WHERE PUR_PCODE='" + ((Label)Reitem.FindControl("PR_PCODE")).Text.ToString() + "'";
        //                            DBCallCommon.ExeSqlText(sqltext1);
        //                            //DBCallCommon.ExeSqlText(sqltext2);
        //                        }
        //                        else
        //                        {
        //                            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText("SELECT PR_STATE FROM TBPC_MARSTOUSETOTAL WHERE PR_PCODE='" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("PR_PCODE")).Text.ToString() + "'");
        //                            if (dt.Rows.Count > 0)
        //                            {
        //                                sqltext1 = "UPDATE TBPC_MARSTOUSETOTAL SET PR_STATE='2',PR_REVIEWBTIME='',PR_REVIEWBADVC='',PR_SHSTATE='0'  WHERE PR_PCODE='" + ((System.Web.UI.WebControls.Label)Reitem.FindControl("PR_PCODE")).Text.ToString() + "'";
        //                                DBCallCommon.ExeSqlText(sqltext1);
        //                            }
        //                        }
        //                    }
        //                }
        //                dr.Close();
        //            }

        //        }

        //    }
        //    if (i == 0)
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
        //        return;
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('反审成功！');", true);
        //        lb_CurrentPage.Text = "1";
        //        getArticle();
        //    }
        //}

        protected void cbxselectall_CheckedChanged(object sender, EventArgs e)
        {
            if (cbxselectall.Checked)
            {
                for (int i = 0; i < tbpc_pchsplanrvwchecklistRepeater.Items.Count; i++)
                {
                    RepeaterItem Reitem = tbpc_pchsplanrvwchecklistRepeater.Items[i];
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < tbpc_pchsplanrvwchecklistRepeater.Items.Count; i++)
                {
                    RepeaterItem Reitem = tbpc_pchsplanrvwchecklistRepeater.Items[i];
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = false;
                    }
                }
            }
        }
        public string get_state(string i)
        {
            string statestr = "";
            if (i == "0")
            {
                statestr = "未调整";
            }
            else if (i == "1")
            {
                statestr = "调整中";
            }
            else if (i == "2")
            {
                statestr = "已调整";
            }
            return statestr;
        }

        public string get_prlist_state(string i)
        {
            string statestr = "";
            if (i == "0" || i == "1")
            {
                statestr = "未提交";
            }
            else
            {
                if (i == "2")
                {
                    statestr = "待审核";
                }
                else
                {
                    if (i == "3")
                    {
                        statestr = "已驳回";
                    }
                    else
                    {
                        if (Convert.ToInt32(i) >= 4)
                        {
                            statestr = "通过";
                        }
                    }
                }
            }
            return statestr;
        }

        protected void tbpc_pchsplanrvwchecklistRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string ptcode = ((System.Web.UI.WebControls.Label)e.Item.FindControl("ptcode")).Text;
                string sql = "select sqrnm from View_TBPC_PURCHASEPLAN_RVW where ptcode='" + ptcode + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PR_SQRNM")).Text = dt.Rows[0]["sqrnm"].ToString();
                }
                if (((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_ISSTOUSE")).Text != "已调整")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_ISSTOUSE")).ForeColor = System.Drawing.Color.Red;
                }

                if ((e.Item.FindControl("usenum") as System.Web.UI.WebControls.Label).Text != "") //占用数量合计
                {
                    sumnum += Convert.ToDouble((e.Item.FindControl("usenum") as System.Web.UI.WebControls.Label).Text);
                }
            }
            if (e.Item.ItemType == ListItemType.Footer)
            {
                (e.Item.FindControl("LabelSumZhanYong") as System.Web.UI.WebControls.Label).Text = Math.Round(sumnum, 4).ToString();
            }
        }

        //删除库存占用单
        protected void btn_delete_click(object sender, EventArgs e)
        {
            int temp = candelete();
            int j = 0;
            if (temp == 0)
            {
                foreach (RepeaterItem Retem in tbpc_pchsplanrvwchecklistRepeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbk = Retem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                    if (cbk.Checked)
                    {
                        string code = ((System.Web.UI.WebControls.Label)Retem.FindControl("PR_PCODE")).Text;//批号
                        string sqlde = "select PUR_PTCODE,PUR_NEWSQCODE,PUR_SQCODE,PUR_USTNUM  from TBPC_MARSTOUSEALLDETAIL where PUR_PCODE='" + code + "'";
                        System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlde);
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string ptcode1 = dt1.Rows[i]["PUR_PTCODE"].ToString();
                            string newsqcode = dt1.Rows[i]["PUR_NEWSQCODE"].ToString();
                            string oldsqcode = dt1.Rows[i]["PUR_SQCODE"].ToString();
                            double num = Convert.ToDouble(dt1.Rows[i]["PUR_USTNUM"].ToString());
                            //**********************改变备库量*********************
                            string sqltext = "select * from TBWS_STORAGE where SQ_CODE ='" + oldsqcode + "' AND SQ_PTC='备库'";
                            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                            if (dt.Rows.Count > 0)
                            {
                                string sqltext1 = "update TBWS_STORAGE set SQ_NUM=SQ_NUM+" + num + " where SQ_CODE ='" + oldsqcode + "' AND SQ_PTC='备库'";
                                DBCallCommon.ExeSqlText(sqltext1);
                                string sqltext2 = "delete from TBWS_STORAGE where SQ_CODE='" + newsqcode + "'";
                                DBCallCommon.ExeSqlText(sqltext2);
                            }
                            else
                            {
                                string sqltext3 = "update TBWS_STORAGE set SQ_PTC='备库',SQ_CODE='" + oldsqcode + "' where SQ_CODE='" + newsqcode + "'";
                                DBCallCommon.ExeSqlText(sqltext3);
                            }
                        }

                        string sql = "select PUR_PTCODE from TBPC_MARSTOUSEALL where PUR_PCODE='" + code + "'";
                        System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql);
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            string ptcode = dt2.Rows[i]["PUR_PTCODE"].ToString();

                            sql = "select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "'";
                            System.Data.DataTable dt5 = DBCallCommon.GetDTUsingSqlText(sql);
                            if (dt5.Rows.Count > 0)
                            {
                                string cgr = dt5.Rows[0]["PUR_CGMAN"].ToString();
                                if (dt5.Rows.Count == 2)
                                {
                                    double num1 = Convert.ToDouble(dt5.Rows[0]["PUR_NUM"].ToString());
                                    double fznum1 = Convert.ToDouble(dt5.Rows[0]["PUR_FZNUM"].ToString());
                                    if (cgr == "")
                                    {
                                        sql = "update TBPC_PURCHASEPLAN set PUR_STATE='0' where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
                                    }
                                    else
                                    {
                                        sql = "update TBPC_PURCHASEPLAN set PUR_STATE='4' where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
                                    }
                                    DBCallCommon.ExeSqlText(sql);
                                }
                                else
                                {
                                    double num1 = Convert.ToDouble(dt5.Rows[0]["PUR_NUM"].ToString());
                                    double fznum1 = Convert.ToDouble(dt5.Rows[0]["PUR_FZNUM"].ToString());
                                    if (cgr == "")
                                    {
                                        sql = "update TBPC_PURCHASEPLAN set PUR_STATE='0'  where PUR_PTCODE='" + ptcode + "'";
                                    }
                                    else
                                    {
                                        sql = "update TBPC_PURCHASEPLAN set PUR_STATE='4'  where PUR_PTCODE='" + ptcode + "'";
                                    }
                                    DBCallCommon.ExeSqlText(sql);
                                }
                            }

                            string sql2 = "delete from TBPC_MARSTOUSEALLDETAIL where PUR_PTCODE='" + ptcode + "'";
                            DBCallCommon.ExeSqlText(sql2);

                        }
                        string sqltext4 = "delete from TBPC_MARSTOUSEALL where PUR_PCODE='" + code + "'";
                        DBCallCommon.ExeSqlText(sqltext4);
                        sqltext4 = "delete from TBPC_MARSTOUSETOTAL where PR_PCODE='" + code + "'";
                        DBCallCommon.ExeSqlText(sqltext4);
                    }
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！');", true);
                lb_CurrentPage.Text = "1";
                getArticle();
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您不是制单人，无权删除！');", true);
            }

        }

        //判断能否删除
        private int candelete()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//制单是否为登录用户
            string postid = "";
            string userid = Session["UserID"].ToString();
            foreach (RepeaterItem Reitem in tbpc_pchsplanrvwchecklistRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        postid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PR_REVIEWA")).Text;
                        if (postid != userid)//登录人不是制单人
                        {
                            j++;
                            break;
                        }

                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (j > 0)//登录人不是制单人
            {
                temp = 2;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }

        //未提交
        private void getwlzywtj()
        {
            string sqltext = "";
            int num = 0;
            sqltext = "select  count(*) from TBPC_MARSTOUSETOTAL where PR_STATE='1'  and PR_REVIEWA='" + Session["UserID"].ToString() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            if (num == 0)
            {
                lb_wlzygl1.Visible = false;
            }
            else
            {
                lb_wlzygl1.Visible = true;
                lb_wlzygl1.Text = "(" + num.ToString() + ")";
            }
        }

        //待审核
        private void getwlzydsh()
        {
            string sqltext = "";
            int num = 0;
            sqltext = "select  count(distinct(PR_PCODE)) from View_TBPC_MARSTOUSE_TOTAL_ALL where allshstate='0' and PR_REVIEWB='" + Session["UserID"].ToString() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            if (num == 0)
            {
                lb_wlzygl2.Visible = false;
            }
            else
            {
                lb_wlzygl2.Visible = true;
                lb_wlzygl2.Text = "(" + num.ToString() + ")";
            }
        }

        //已驳回
        private void getwlzybh()
        {
            string sqltext = "";
            int num = 0;
            sqltext = "select  count(distinct(PR_PCODE)) from View_TBPC_MARSTOUSE_TOTAL_ALL where allshstate='1' and PR_REVIEWA='" + Session["UserID"].ToString() + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num = Convert.ToInt32(dr[0].ToString());
            }
            dr.Close();
            if (num == 0)
            {
                lb_wlzygl3.Visible = false;
            }
            else
            {
                lb_wlzygl3.Visible = true;
                lb_wlzygl3.Text = "(" + num.ToString() + ")";
            }
        }

        protected void QueryButton_Click(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }

        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            tb_orderno.Text = "";
            tb_StartTime.Text = "";
            tb_EndTime.Text = "";
            tb_name.Text = "";
            tb_ptc.Text = "";
            tb_cz.Text = "";
            tb_gg.Text = "";
            tb_pj.Text = "";
            tb_eng.Text = "";
            tb_marid.Text = "";

        }

        //导出
        protected void btn_daochu_Click(object sender, EventArgs e)
        {
            int temp = ifselect();
            if (temp == 0)
            {
                string ordercode = "";
                string code = "";
                foreach (RepeaterItem Reitem in tbpc_pchsplanrvwchecklistRepeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        if (ordercode == "" || code != ((System.Web.UI.WebControls.Label)Reitem.FindControl("PR_PCODE")).Text.ToString())
                        {
                            ordercode += ((System.Web.UI.WebControls.Label)Reitem.FindControl("PR_PCODE")).Text.ToString() + "|";
                        }
                        code = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PR_PCODE")).Text.ToString();
                    }
                }
                ordercode = ordercode.Replace("|", "','");
                ordercode = ordercode.Substring(0, ordercode.LastIndexOf(",")).ToString();
                ordercode = "'" + ordercode;
                string sqltext = "";
                sqltext = "SELECT PR_PCODE,PR_REVIEWA,PR_REVIEWANM,PJ_NAME,TSA_ENGNAME,PR_REVIEWATIME,PR_REVIEWB,PR_REVIEWBNM," +
                          "PR_REVIEWBTIME,PR_STATE,PR_NOTE,ptcode,marid,marnm,margg,marcz,margb,usenum,marunit,allnote  " +
                          "from View_TBPC_MARSTOUSE_TOTAL_ALL  where PR_PCODE in (" + ordercode + ")";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                ExportDataItem(dt);
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要导出的占用单！！！');", true);
            }

        }

        private int ifselect()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            foreach (RepeaterItem Reitem in tbpc_pchsplanrvwchecklistRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
        private void ExportDataItem(System.Data.DataTable objdt)
        {
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("采购占用单明细") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //m_xlApp.Visible = false;    // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            //System.Data.DataTable dt = objdt;

            //// 填充数据
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    wksheet.Cells[i + 3, 1] = Convert.ToString(i + 1);//序号

            //    wksheet.Cells[i + 3, 2] = "'" + dt.Rows[i]["PR_PCODE"].ToString();//占用单号

            //    wksheet.Cells[i + 3, 3] = "'" + dt.Rows[i]["PJ_NAME"].ToString();//项目

            //    wksheet.Cells[i + 3, 4] = "'" + dt.Rows[i]["TSA_ENGNAME"].ToString();//工程

            //    wksheet.Cells[i + 3, 5] = "'" + dt.Rows[i]["PR_REVIEWANM"].ToString();//制单人

            //    wksheet.Cells[i + 3, 6] = "'" + dt.Rows[i]["PR_REVIEWATIME"].ToString();//制单日期

            //    wksheet.Cells[i + 3, 7] = "'" + dt.Rows[i]["PR_REVIEWBNM"].ToString();//审核人

            //    wksheet.Cells[i + 3, 8] = "'" + dt.Rows[i]["PR_REVIEWBTIME"].ToString();//审核时间

            //    wksheet.Cells[i + 3, 9] = "'" + dt.Rows[i]["ptcode"].ToString();//计划跟踪号

            //    wksheet.Cells[i + 3, 10] = "'" + dt.Rows[i]["marid"].ToString();//物料编码

            //    wksheet.Cells[i + 3, 11] = "'" + dt.Rows[i]["marnm"].ToString();//名称

            //    wksheet.Cells[i + 3, 12] = "'" + dt.Rows[i]["margg"].ToString();//规格

            //    wksheet.Cells[i + 3, 13] = "'" + dt.Rows[i]["marcz"].ToString();//材质

            //    wksheet.Cells[i + 3, 14] = "'" + dt.Rows[i]["margb"].ToString();//国标

            //    wksheet.Cells[i + 3, 15] = dt.Rows[i]["usenum"].ToString();//使用库存数量

            //    wksheet.Cells[i + 3, 16] = "'" + dt.Rows[i]["marunit"].ToString();//单位

            //    wksheet.Cells[i + 3, 17] = "'" + dt.Rows[i]["allnote"].ToString();//备注

            //    wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 17]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            //    wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 17]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            //    wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 17]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            //}
            ////设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            //string filename = Server.MapPath("/PC_Data/ExportFile/" + "采购占用单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ////ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
            string filename = "采购占用单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("采购占用单明细.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据

                for (int i = 0; i < objdt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    row.CreateCell(1).SetCellValue("'" + objdt.Rows[i]["PR_PCODE"].ToString());//占用单号
                    row.CreateCell(2).SetCellValue("'" + objdt.Rows[i]["PJ_NAME"].ToString());//项目
                    row.CreateCell(3).SetCellValue("'" + objdt.Rows[i]["TSA_ENGNAME"].ToString());//工程
                    row.CreateCell(4).SetCellValue("'" + objdt.Rows[i]["PR_REVIEWANM"].ToString());//制单人
                    row.CreateCell(5).SetCellValue("'" + objdt.Rows[i]["PR_REVIEWATIME"].ToString());//制单日期
                    row.CreateCell(6).SetCellValue("'" + objdt.Rows[i]["PR_REVIEWBNM"].ToString());//审核人
                    row.CreateCell(7).SetCellValue("'" + objdt.Rows[i]["PR_REVIEWBTIME"].ToString());//审核时间
                    row.CreateCell(8).SetCellValue("'" + objdt.Rows[i]["ptcode"].ToString());//计划跟踪号
                    row.CreateCell(9).SetCellValue("'" + objdt.Rows[i]["marid"].ToString());//物料编码
                    row.CreateCell(10).SetCellValue("'" + objdt.Rows[i]["marnm"].ToString());//名称
                    row.CreateCell(11).SetCellValue("'" + objdt.Rows[i]["margg"].ToString());//规格
                    row.CreateCell(12).SetCellValue("'" + objdt.Rows[i]["marcz"].ToString());//材质
                    row.CreateCell(13).SetCellValue("'" + objdt.Rows[i]["margb"].ToString());//国标
                    row.CreateCell(14).SetCellValue(objdt.Rows[i]["usenum"].ToString());//使用库存数量
                    row.CreateCell(15).SetCellValue("'" + objdt.Rows[i]["marunit"].ToString());//单位
                    row.CreateCell(18).SetCellValue("'" + objdt.Rows[i]["allnote"].ToString());//备注
                }

                #endregion

                for (int i = 0; i <= objdt.Columns.Count; i++)
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

        /// <summary>
        /// 输出Excel文件并退出
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="workbook"></param>
        #region MyRegion
        //private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        //{
        //    try
        //    {

        //        workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
        //        workbook.Close(Type.Missing, Type.Missing, Type.Missing);
        //        m_xlApp.Workbooks.Close();
        //        m_xlApp.Quit();
        //        m_xlApp.Application.Quit();

        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
        //        System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

        //        wksheet = null;
        //        workbook = null;
        //        m_xlApp = null;
        //        GC.Collect();

        //        //下载

        //        System.IO.FileInfo path = new System.IO.FileInfo(filename);

        //        //同步，异步都支持
        //        HttpResponse contextResponse = HttpContext.Current.Response;
        //        contextResponse.Redirect(string.Format("~/PC_Data/ExportFile/{0}", path.Name), false);
        //    }
        //    catch (Exception e)
        //    {
        //        throw e;
        //    }
        //} 
        #endregion
        protected void tb_pj_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (tb_pj.Text.ToString().Contains("|"))
            {
                Cname = tb_pj.Text.Substring(0, tb_pj.Text.ToString().IndexOf("|"));
                tb_pj.Text = Cname.Trim();
            }
            else if (tb_pj.Text == "")
            {

            }
        }
        protected void tb_eng_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (tb_eng.Text.ToString().Contains("|"))
            {
                Cname = tb_eng.Text.Substring(0, tb_eng.Text.ToString().IndexOf("|"));
                tb_eng.Text = Cname.Trim();
            }
            else if (tb_eng.Text == "")
            {

            }
        }

    }
}
