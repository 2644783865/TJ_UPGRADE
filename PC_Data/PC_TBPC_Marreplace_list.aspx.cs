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
    public partial class PC_TBPC_Marreplace_list : BasicPage
    {
        public string gloabstate
        {
            get
            {
                object str = ViewState["gloabstate"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabstate"] = value;
            }
        }
        public string gloabsheetno
        {
            get
            {
                object str = ViewState["gloabsheetno"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabsheetno"] = value;
            }
        }
        public string gloabptc//计划跟踪号
        {
            get
            {
                object str = ViewState["gloabptc"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabptc"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_delete.Attributes.Add("OnClick", "Javascript:return confirm('会删除整批物料，是否确定删除?');");
            if (!IsPostBack)
            {

                string sqltext = "";
                sqltext = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_DEPID='05' and ST_PD='0'";
                string dataText = "ST_NAME";
                string dataValue = "ST_ID";
                DBCallCommon.BindDdl(drp_stu, sqltext, dataText, dataValue);
                drp_stu.SelectedIndex = 0;
                getArticle();
            }
            if (Session["UserDeptID"].ToString() == "04")
            {
                btnqrck.Visible = true;
            }
            getwtj();
            getdsh();
            getbh();
            ControlVisible();
            CheckUser(ControlFinder);
        }

        //2016.11.11修改，次数为基数艾广修特设，做确定查看处理，显示查看按钮

        private void ControlVisible()
        {
            foreach (RepeaterItem item in Marreplace_list_Repeater.Items)
            {
                LinkButton btn_Confirm_ck_f = item.FindControl("btn_Confirm_ck") as LinkButton;
                string[] sfqu_ck = btn_Confirm_ck_f.CommandArgument.ToString().Split(',');
                string mp_code = sfqu_ck[0].Trim();
                string mp_ck_bt = sfqu_ck[1].Trim();
                btn_Confirm_ck_f.Visible = false;
                if (mp_ck_bt == "1" && Session["UserName"].ToString() == "艾广修")
                {
                    btn_Confirm_ck_f.Visible = true;
                }
            }
        }
        //绑定dropdownlist，业务员查询条件
        protected void btn_Confirm_Click(object sender, EventArgs e)
        {
            string[] sfqu_ck = ((LinkButton)sender).CommandArgument.ToString().Split(',');
            string mp_code = sfqu_ck[0].Trim();
            string mp_ck_bt = sfqu_ck[1].Trim();
            string sql_ck_qr = "update TBPC_MARREPLACETOTAL set MP_CK_BT='2' where mp_code='" + mp_code + "'";
            DBCallCommon.ExeSqlText(sql_ck_qr);
        }

        private void getArticle()      //取得Article数据
        {
            int cup = Convert.ToInt32(this.lb_CurrentPage.Text);  //当前页数,初始化为地1页
            PagedDataSource ps = new PagedDataSource();
            ps.DataSource = CreateDataSource().DefaultView;
            ps.AllowPaging = true;
            ps.PageSize = 50;     //每页显示的数据的行数
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
                Marreplace_list_Repeater.DataSource = ps;
                Marreplace_list_Repeater.DataBind();
                if (Marreplace_list_Repeater.Items.Count > 0)
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
            if (rad_daishen.Checked)//待审核
            {
                if (Session["UserDeptID"].ToString() == "04" && rad_mypart.Checked)
                {
                    sqltext = "SELECT mpcode, plancode, zdreson, zdrid, zdtime, shraid, shratime, shrayj, shrbid,MP_CKSHRID,MP_CKSHRTIME,MP_CKSTATE,ST_NAME, " +
                               "shrbtime, shrbyj, totalstate, totalnote, zdrnm, shranm, shrbnm, engid, engnm, pjid, pjnm,ptcode,marid,marnm,marguige,marguobiao,marcaizhi,MP_SFDY ,MP_LEADER,scbd,mp_ck_bt  " +
                                "FROM View_TBPC_MARREPLACE_all_total where engid in(select MTA_ID from TBMP_MANUTSASSGN where MTA_DUY='" + Session["UserName"].ToString() + "') and (totalstate='001' or totalstate='111' or totalstate='211') or (totalstate='311' and (MP_CKSHRID is not null or MP_CKSHRID!='') and (MP_CKSHRTIME is null or MP_CKSHRTIME='')) and  marguige like '%" + tb_gg.Text.Trim() + "%' ORDER BY zdtime desc,mpcode desc,ptcode asc";
                }
                else
                {
                    sqltext = "SELECT mpcode, plancode, zdreson, zdrid, zdtime, shraid, shratime, shrayj, shrbid,MP_CKSHRID,MP_CKSHRTIME,MP_CKSTATE,ST_NAME, " +
                               "shrbtime, shrbyj, totalstate, totalnote, zdrnm, shranm, shrbnm, engid, engnm, pjid, pjnm,ptcode,marid,marnm,marguige,marguobiao,marcaizhi,MP_SFDY ,MP_LEADER,scbd,mp_ck_bt  " +
                                "FROM View_TBPC_MARREPLACE_all_total where (totalstate='001' or totalstate='111' or totalstate='211') or (totalstate='311' and (MP_CKSHRID is not null or MP_CKSHRID!='') and (MP_CKSHRTIME is null or MP_CKSHRTIME='')) and  marguige like '%" + tb_gg.Text.Trim() + "%' ORDER BY zdtime desc,mpcode desc,ptcode asc";
                }
            }
            else if (rad_tongguo.Checked)//通过
            {
                if (Session["UserDeptID"].ToString() == "04" && rad_mypart.Checked)
                {
                    sqltext = "SELECT mpcode, plancode, zdreson, zdrid, zdtime, shraid, shratime, shrayj, shrbid,MP_CKSHRID,MP_CKSHRTIME,MP_CKSTATE,ST_NAME, " +
                          "shrbtime, shrbyj, totalstate, totalnote, zdrnm, shranm, shrbnm, engid, engnm, pjid, pjnm,ptcode,marid,marnm,marguige,marguobiao,marcaizhi,MP_SFDY,MP_LEADER,scbd,mp_ck_bt   " +
                           "FROM View_TBPC_MARREPLACE_all_total where engid in(select MTA_ID from TBMP_MANUTSASSGN where MTA_DUY='" + Session["UserName"].ToString() + "') and (totalstate='311' and ((MP_CKSHRID is null or MP_CKSHRID='') and (MP_CKSHRTIME is null or MP_CKSHRTIME='')) or ((MP_CKSHRID is not null or MP_CKSHRID!='') and (MP_CKSHRTIME is not null or MP_CKSHRTIME!='') and MP_CKSTATE='1'))  and  marguige like '%" + tb_gg.Text.Trim() + "%' ORDER BY zdtime desc,mpcode desc,ptcode asc";
                }
                else
                {
                    sqltext = "SELECT mpcode, plancode, zdreson, zdrid, zdtime, shraid, shratime, shrayj, shrbid,MP_CKSHRID,MP_CKSHRTIME,MP_CKSTATE,ST_NAME, " +
                              "shrbtime, shrbyj, totalstate, totalnote, zdrnm, shranm, shrbnm, engid, engnm, pjid, pjnm,ptcode,marid,marnm,marguige,marguobiao,marcaizhi,MP_SFDY,MP_LEADER,scbd,mp_ck_bt   " +
                               "FROM View_TBPC_MARREPLACE_all_total where (totalstate='311' and ((MP_CKSHRID is null or MP_CKSHRID='') and (MP_CKSHRTIME is null or MP_CKSHRTIME='')) or ((MP_CKSHRID is not null or MP_CKSHRID!='') and (MP_CKSHRTIME is not null or MP_CKSHRTIME!='') and MP_CKSTATE='1'))  and  marguige like '%" + tb_gg.Text.Trim() + "%' ORDER BY zdtime desc,mpcode desc,ptcode asc";
                }
            }
            else
            {
                if (Session["UserDeptID"].ToString() == "04" && rad_mypart.Checked)
                {
                    sqltext = "SELECT mpcode, plancode, zdreson, zdrid, zdtime, shraid, shratime, shrayj, shrbid,MP_CKSHRID,MP_CKSHRTIME,MP_CKSTATE,ST_NAME, " +
                           "shrbtime, shrbyj, totalstate, totalnote, zdrnm, shranm, shrbnm, engid, engnm, pjid, pjnm,ptcode,marid,marnm,marguige,marguobiao,marcaizhi,MP_SFDY,MP_LEADER,scbd,mp_ck_bt   " +
                            "FROM View_TBPC_MARREPLACE_all_total where engid in(select MTA_ID from TBMP_MANUTSASSGN where MTA_DUY='" + Session["UserName"].ToString() + "') and marguige like '%" + tb_gg.Text.Trim() + "%' ORDER BY zdtime desc,mpcode desc,ptcode asc";
                }
                else
                {
                    sqltext = "SELECT mpcode, plancode, zdreson, zdrid, zdtime, shraid, shratime, shrayj, shrbid,MP_CKSHRID,MP_CKSHRTIME,MP_CKSTATE,ST_NAME, " +
                               "shrbtime, shrbyj, totalstate, totalnote, zdrnm, shranm, shrbnm, engid, engnm, pjid, pjnm,ptcode,marid,marnm,marguige,marguobiao,marcaizhi,MP_SFDY,MP_LEADER,scbd,mp_ck_bt   " +
                                "FROM View_TBPC_MARREPLACE_all_total where  marguige like '%" + tb_gg.Text.Trim() + "%' ORDER BY zdtime desc,mpcode desc,ptcode asc";
                }
            }

            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DataView dv = null;
            if (rad_all.Checked)//全部任务
            {
                dv = dt.DefaultView;
                dt = dv.ToTable();
            }
            if (rad_mypart.Checked)//我的任务
            {

                dv = dt.DefaultView;
                if (Session["UserDeptID"].ToString() == "04")
                {
                    dv.RowFilter = "engid is not null"; //对dataView进行筛选
                }
                else
                {
                    dv.RowFilter = "zdrid='" + Session["UserID"].ToString() + "' OR (MP_LEADER='" + Session["UserID"].ToString() + "' and totalstate='001') OR (shraid='" + Session["UserID"].ToString() + "' and totalstate='111') OR (shrbid='" + Session["UserID"].ToString() + "' and totalstate='211') or (MP_CKSHRID='" + Session["UserID"].ToString() + "' and totalstate='311')"; //对dataView进行筛选
                }
                dt = dv.ToTable();
            }
            if (rad_weitijiao.Checked)//未提交
            {
                dv = dt.DefaultView;
                dv.RowFilter = "totalstate='000'";
                dt = dv.ToTable();
            }

            if (rad_bohui.Checked)//驳回
            {
                dv = dt.DefaultView;
                dv.RowFilter = "totalstate='300' OR totalstate='200' or (MP_CKSTATE='2' and totalstate='311') or totalstate='002'";
                dt = dv.ToTable();
            }
            if (tb_pj.Text != "")//项目
            {
                dv = dt.DefaultView;
                dv.RowFilter = "pjnm like '%" + tb_pj.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_eng.Text != "")//工程
            {
                dv = dt.DefaultView;
                dv.RowFilter = "engnm like '%" + tb_eng.Text.Trim() + "%'"; //对dataView进行筛选 
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
            if (tb_gb.Text != "")//国标
            {
                dv = dt.DefaultView;
                dv.RowFilter = "marguobiao like '%" + tb_gb.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_StartTime.Text != "")//开始日期
            {
                string startdate = tb_StartTime.Text.ToString() == "" ? "1900-01-01" : tb_StartTime.Text.ToString();
                dv = dt.DefaultView;
                dv.RowFilter = "zdtime>='" + startdate + "'"; //对dataView进行筛选 
                dt = dv.ToTable();
                //dataView.Sort = "PCODE ASC";
            }
            if (tb_EndTime.Text != "")//结束日期
            {
                string enddate = tb_EndTime.Text.ToString() == "" ? "2100-01-01" : tb_EndTime.Text.ToString();
                enddate = enddate + " 23:59:59";
                dv = dt.DefaultView;
                dv.RowFilter = "zdtime<='" + enddate + "'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_orderno.Text != "")//单据编号
            {
                dv = dt.DefaultView;
                dv.RowFilter = "mpcode like '%" + tb_orderno.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (drp_stu.SelectedIndex != 0)//制单人
            {
                dv = dt.DefaultView;
                dv.RowFilter = "zdrid='" + drp_stu.SelectedValue + "'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_name.Text != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "marnm like '%" + tb_name.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_cz.Text != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "marcaizhi like '%" + tb_cz.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (rad_wdy.Checked)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "MP_SFDY='0'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (rad_ydy.Checked)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "MP_SFDY<>'0'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            return dt;
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
            ControlVisible();

        }
        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e) //跳转到指定页代码
        {
            int page = Convert.ToInt16((DropDownList1.SelectedItem.Value));
            lb_CurrentPage.Text = page.ToString();
            getArticle();
            ControlVisible();
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
            ControlVisible();
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
            ControlVisible();
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
            ControlVisible();
        }
        protected void rad_all_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            ControlVisible();
        }
        protected void rad_mypart_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            ControlVisible();
        }
        protected void rad_weitijiao_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            ControlVisible();
        }
        protected void rad_daishen_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            ControlVisible();
        }

        protected void rad_tongguo_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            ControlVisible();
        }
        protected void rad_bohui_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            ControlVisible();
        }
        //全选checkbox
        protected void selectall_CheckedChanged(object sender, EventArgs e)
        {
            if (selectall.Checked)
            {
                foreach (RepeaterItem Reitem in Marreplace_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = true;
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Reitem in Marreplace_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = false;
                    }
                }
            }
            ControlVisible();
        }
        public string get_marreplist_state(string i)
        {
            string statestr = "";
            if (i == "000")
            {
                statestr = "未提交";
            }
            else
            {
                if (i == "111" || i == "211")
                {
                    statestr = "待审核";
                }
                else
                {

                    if (i == "200" || i == "300")
                    {
                        statestr = "驳回";
                    }
                    else
                    {
                        statestr = "通过";
                    }

                }
            }
            return statestr;
        }

        protected void Marreplace_list_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                string ckshr = ((System.Web.UI.WebControls.Label)e.Item.FindControl("MP_CKSHRID")).Text;
                if (Session["UserDeptID"].ToString() == "04")
                {
                    string scbdstr = ((HtmlInputHidden)e.Item.FindControl("lbscbd")).Value;
                    if (scbdstr.ToString().Trim() == "1")
                    {
                        ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#00E600";
                    }
                }
                if (ckshr == System.DBNull.Value.ToString() || ckshr == System.String.Empty)
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("MP_CKSHRNM")).Text = "——";
                }
                string state = ((System.Web.UI.WebControls.Label)e.Item.FindControl("MP_STATE1")).Text;
                if (state == "000")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("MP_STATE")).Text = "未提交";
                }
                else if (state == "111")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("MP_STATE")).Text = "技术员待审核";
                }
                else if (state == "100")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("MP_STATE")).Text = "技术员驳回";
                }
                else if (state == "211")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("MP_STATE")).Text = "负责人待审核";
                }
                else if (state == "200")
                {
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("MP_STATE")).Text = "负责人驳回";
                }
                else if (state == "311")
                {
                    if (ckshr == System.DBNull.Value.ToString() || ckshr == System.String.Empty)
                    {
                        ((System.Web.UI.WebControls.Label)e.Item.FindControl("MP_STATE")).Text = "通过";
                    }
                    else
                    {
                        string cktime = ((System.Web.UI.WebControls.Label)e.Item.FindControl("MP_CKSHRTIME")).Text;
                        if (cktime == System.DBNull.Value.ToString() || cktime == System.String.Empty)
                        {
                            ((System.Web.UI.WebControls.Label)e.Item.FindControl("MP_STATE")).Text = "储运待审核";
                        }
                        else
                        {
                            ((System.Web.UI.WebControls.Label)e.Item.FindControl("MP_STATE")).Text = "通过";
                        }
                    }
                }
            }
            if (e.Item.ItemType == ListItemType.Header)
            {

            }
            ControlVisible();
        }
        //删除代用单,代用则状态回到4，相似代用则状态回到1
        protected void btn_delete_click(object sender, EventArgs e)
        {
            int temp = candelete();
            int j = 0;
            if (temp == 0 || temp == 3)
            {
                foreach (RepeaterItem Retem in Marreplace_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbk = Retem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                    if (cbk.Checked)
                    {
                        string code = ((System.Web.UI.WebControls.Label)Retem.FindControl("MP_CODE")).Text;

                        string sqlde = "select MP_PTCODE,MP_NEWSQCODE,MP_OLDSQCODE,MP_NEWNUMA,MP_NEWNUMB  from TBPC_MARREPLACEDETAIL where MP_CODE='" + code + "'";
                        System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlde);
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            string newsqcode = dt1.Rows[i]["MP_NEWSQCODE"].ToString();
                            string oldsqcode = dt1.Rows[i]["MP_OLDSQCODE"].ToString();
                            double num = Convert.ToDouble(dt1.Rows[i]["MP_NEWNUMA"].ToString());
                            double fznum = Convert.ToDouble(dt1.Rows[i]["MP_NEWNUMB"].ToString());
                            //**********************改变备库量*********************
                            string sqltext = "select * from TBWS_STORAGE where SQ_CODE='" + oldsqcode + "' AND SQ_PTC='备库'";
                            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                            if (dt.Rows.Count > 0)
                            {
                                string sqltext1 = "update TBWS_STORAGE set SQ_NUM=SQ_NUM+" + num + ",SQ_FZNUM=MP_NEWSQCODE+" + fznum + " where SQ_CODE='" + oldsqcode + "' AND SQ_PTC='备库'";
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

                        string leixin = code.Substring(4, 1).ToString();
                        string sql = "select MP_PTCODE from TBPC_MARREPLACEALL where MP_CODE='" + code + "'";
                        System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql);
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            string ptcode = dt2.Rows[i]["MP_PTCODE"].ToString();
                            if (leixin == "0")
                            {
                                sql = "select * from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "'";
                                System.Data.DataTable dt5 = DBCallCommon.GetDTUsingSqlText(sql);
                                if (dt5.Rows.Count == 2)//表示拆分过
                                {
                                    double num1 = Convert.ToDouble(dt5.Rows[0]["PUR_NUM"].ToString());
                                    double fznum1 = Convert.ToDouble(dt5.Rows[0]["PUR_FZNUM"].ToString());
                                    sql = "update TBPC_PURCHASEPLAN set PUR_STATE='0',PUR_PTASMAN='',PUR_PTASTIME='',PUR_CGMAN=''  where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
                                    DBCallCommon.ExeSqlText(sql);
                                }
                                else
                                {
                                    double num1 = Convert.ToDouble(dt5.Rows[0]["PUR_NUM"].ToString());
                                    double fznum1 = Convert.ToDouble(dt5.Rows[0]["PUR_FZNUM"].ToString());
                                    sql = "update TBPC_PURCHASEPLAN set PUR_STATE='0',PUR_PTASMAN='',PUR_PTASTIME='',PUR_CGMAN='' where PUR_PTCODE='" + ptcode + "'";
                                    DBCallCommon.ExeSqlText(sql);
                                }
                                if (j == 0)
                                {
                                    string sql3 = "select PUR_PCODE from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "'";
                                    System.Data.DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sql3);
                                    if (dt3.Rows.Count > 0)
                                    {
                                        string pcode = dt3.Rows[0]["PUR_PCODE"].ToString();
                                        sql = "update TBPC_PCHSPLANRVW set PR_STATE='0' where PR_SHEETNO='" + pcode + "'";
                                        DBCallCommon.ExeSqlText(sql);
                                    }
                                }
                                j++;

                            }
                            else if (leixin == "1")
                            {
                                string sql1 = "update TBPC_PURCHASEPLAN set PUR_STATE='4' where PUR_PTCODE='" + ptcode + "'";
                                DBCallCommon.ExeSqlText(sql1);
                            }
                        }
                        string sqltext4 = "delete from TBPC_MARREPLACETOTAL where MP_CODE='" + code + "'";
                        string sqltext5 = "delete from TBPC_MARREPLACEALL where MP_CODE='" + code + "'";
                        string sqltext6 = "delete from TBPC_MARREPLACEDETAIL where MP_CODE='" + code + "'";
                        DBCallCommon.ExeSqlText(sqltext4);
                        DBCallCommon.ExeSqlText(sqltext5);
                        DBCallCommon.ExeSqlText(sqltext6);
                    }
                }
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
            ControlVisible();

        }

        //判断能否删除
        private int candelete()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//制单是否为登录用户
            int k = 0;
            string state = "";//状态
            string postid = "";
            string userid = Session["UserID"].ToString();
            foreach (RepeaterItem Reitem in Marreplace_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        state = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MP_STATE1")).Text.ToString();
                        postid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MP_FILLFMID")).Text;
                        if (postid != userid)//登录人不是制单人
                        {
                            j++;
                            break;
                        }
                        else if (state == "311" || state == "111" || state == "211")//审核通过或审核中
                        {
                            k++;
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
            else if (k > 0)//选择的数据中包含审核通过内容
            {
                temp = 3;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }

        //代用单未提交审核数量
        private void getwtj()
        {
            string sqltext = "";
            int num = 0;
            if (rad_mypart.Checked)
            {
                sqltext = "select count(*) from TBPC_MARREPLACETOTAL where  " +
                             "MP_FILLFMID='" + Session["UserID"].ToString() +
                             "' and MP_STATE='000'";
            }
            else if (rad_all.Checked)
            {
                sqltext = "select count(*) from TBPC_MARREPLACETOTAL where  MP_STATE='000'";
            }

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num += Convert.ToInt32(dr[0].ToString());
            }

            dr.Close();
            if (num == 0)
            {
                lb_dydsh3.Visible = false;
            }
            else
            {
                lb_dydsh3.Visible = true;
                lb_dydsh3.Text = "(" + num.ToString() + ")";
            }
        }

        //代用单待审核数量
        private void getdsh()
        {
            string sqltext = "";
            int num = 0;
            if (rad_mypart.Checked)
            {
                sqltext = "select count(*) from TBPC_MARREPLACETOTAL where  " +
                                     "(MP_REVIEWAID='" + Session["UserID"].ToString() +
                                     "' and MP_STATE='111')";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    num += Convert.ToInt32(dr[0].ToString());
                }
                dr.Close();
                sqltext = "select count(*) from TBPC_MARREPLACETOTAL where  " +
                     "(MP_CHARGEID='" + Session["UserID"].ToString() +
                     "' and MP_STATE='211')";
                SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr1.Read())
                {
                    num += Convert.ToInt32(dr1[0].ToString());
                }
                dr1.Close();

                sqltext = "select count(*) from TBPC_MARREPLACETOTAL where  " +
                     "(MP_CKSHRID='" + Session["UserID"].ToString() +
                     "' and MP_STATE='311' and (MP_CKSHRTIME='' or MP_CKSHRTIME is null))";
                SqlDataReader dr2 = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr2.Read())
                {
                    num += Convert.ToInt32(dr2[0].ToString());
                }
                dr2.Close();
            }
            else if (rad_all.Checked)
            {
                sqltext = "select count(*) from TBPC_MARREPLACETOTAL where  MP_STATE='111' or MP_STATE='211' or (MP_STATE='311' and MP_CKSTATE='0')";
                SqlDataReader dr2 = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr2.Read())
                {
                    num += Convert.ToInt32(dr2[0].ToString());
                }
                dr2.Close();
            }
            if (num == 0)
            {
                lb_dydsh1.Visible = false;
            }
            else
            {
                lb_dydsh1.Visible = true;
                lb_dydsh1.Text = "(" + num.ToString() + ")";
            }
        }

        //代用单驳回数量
        private void getbh()
        {
            string sqltext = "";
            int num = 0;
            if (rad_mypart.Checked)
            {
                sqltext = "select count(*) from TBPC_MARREPLACETOTAL where  " +
                                     "(MP_FILLFMID='" + Session["UserID"].ToString() +
                                     "' and (MP_STATE='200' or MP_STATE='300' or (MP_STATE='311' and MP_CKSTATE='2')))";
            }
            else if (rad_all.Checked)
            {
                sqltext = "select count(*) from TBPC_MARREPLACETOTAL where  MP_STATE='200' or MP_STATE='300' or (MP_STATE='311' and MP_CKSTATE='2')";
            }

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                num += Convert.ToInt32(dr[0].ToString());
            }

            if (num == 0)
            {
                lb_dydsh2.Visible = false;
            }
            else
            {
                lb_dydsh2.Visible = true;
                lb_dydsh2.Text = "(" + num.ToString() + ")";
            }
        }

        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
 
        }

        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {
            tb_orderno.Text = "";
            tb_StartTime.Text = "";
            tb_EndTime.Text = "";
            tb_name.Text = "";
            tb_cz.Text = "";
            tb_gg.Text = "";
            tb_pj.Text = "";
            tb_eng.Text = "";
            drp_stu.SelectedIndex = 0;
            QueryButton_Click(null, null);
            ControlVisible();
        }

        protected void QueryButton_Click(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            ControlVisible();
        }

        //导出
        protected void btn_daochu_Click(object sender, EventArgs e)
        {
            int temp = ifselect();
            if (temp == 0)
            {
                string ordercode = "";
                string code = "";
                foreach (RepeaterItem Reitem in Marreplace_list_Repeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx.Checked)
                    {
                        if (ordercode == "" || code != ((System.Web.UI.WebControls.Label)Reitem.FindControl("MP_CODE")).Text.ToString())
                        {
                            ordercode += ((System.Web.UI.WebControls.Label)Reitem.FindControl("MP_CODE")).Text.ToString() + "/";
                        }
                        code = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MP_CODE")).Text.ToString();
                    }
                }
                ordercode = ordercode.Replace("/", "','");
                ordercode = ordercode.Substring(0, ordercode.LastIndexOf(",")).ToString();
                ordercode = "'" + ordercode;
                string sqltext = "";
                sqltext = "SELECT mpcode, engnm, pjnm,ptcode, zdrnm, zdtime, " +
                       " totalstate, totalnote,marid,marnm,marguige,marguobiao,marcaizhi,num,fznum,marcgunit,fzunit,length,width,allnote,  " +
                       "detailmarid,detailmarnm,detailmarguige,detailmarcaizhi,detailmarguobiao,detailmarnuma,detailmarnumb,detailfzunit,detailmarunit,detaillength,detailwidth,detailnote  " +
                       "FROM View_TBPC_MARREPLACE_total_all_detail where mpcode in (" + ordercode + ")";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                ExportDataItem(dt);
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要导出的代用单！！！');", true);
            }
            ControlVisible();
        }

        private int ifselect()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            foreach (RepeaterItem Reitem in Marreplace_list_Repeater.Items)
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

        private void ExportDataItem(System.Data.DataTable dt)
        {
            //Application m_xlApp = new Application();
            //Workbooks workbooks = m_xlApp.Workbooks;
            //Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            //Worksheet wksheet;
            //workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("采购代用单明细表") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            //m_xlApp.Visible = false;    // Excel不显示  
            //m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            //wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            //System.Data.DataTable dt = objdt;

            //// 填充数据
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    wksheet.Cells[i + 4, 1] = Convert.ToString(i + 1);//序号

            //    wksheet.Cells[i + 4, 2] = "'" + dt.Rows[i]["mpcode"].ToString();//代用单号

            //    wksheet.Cells[i + 4, 3] = "'" + dt.Rows[i]["pjnm"].ToString();//项目

            //    wksheet.Cells[i + 4, 4] = "'" + dt.Rows[i]["engnm"].ToString();//工程

            //    wksheet.Cells[i + 4, 5] = "'" + dt.Rows[i]["zdrnm"].ToString();//制单人

            //    wksheet.Cells[i + 4, 6] = "'" + dt.Rows[i]["zdtime"].ToString();//制单日期

            //    wksheet.Cells[i + 4, 7] = "'" + dt.Rows[i]["ptcode"].ToString();//计划跟踪号

            //    wksheet.Cells[i + 4, 8] = "'" + dt.Rows[i]["marid"].ToString();//物料编码

            //    wksheet.Cells[i + 4, 9] = "'" + dt.Rows[i]["marnm"].ToString();//名称

            //    wksheet.Cells[i + 4, 10] = "'" + dt.Rows[i]["marguige"].ToString();//规格

            //    wksheet.Cells[i + 4, 11] = "'" + dt.Rows[i]["marcaizhi"].ToString();//材质

            //    wksheet.Cells[i + 4, 12] = "'" + dt.Rows[i]["marguobiao"].ToString();//国标

            //    wksheet.Cells[i + 4, 13] = dt.Rows[i]["num"].ToString();//数量

            //    wksheet.Cells[i + 4, 14] = "'" + dt.Rows[i]["marcgunit"].ToString();//单位

            //    wksheet.Cells[i + 4, 15] = dt.Rows[i]["fznum"].ToString();//辅助数量

            //    wksheet.Cells[i + 4, 16] = "'" + dt.Rows[i]["fzunit"].ToString();//辅助单位

            //    wksheet.Cells[i + 4, 17] = "'" + dt.Rows[i]["length"].ToString();//长度

            //    wksheet.Cells[i + 4, 18] = "'" + dt.Rows[i]["width"].ToString();//宽度

            //    wksheet.Cells[i + 4, 19] = "'" + dt.Rows[i]["allnote"].ToString();//备注

            //    wksheet.Cells[i + 4, 20] = "'" + dt.Rows[i]["detailmarid"].ToString();//物料编码

            //    wksheet.Cells[i + 4, 21] = "'" + dt.Rows[i]["detailmarnm"].ToString();//名称

            //    wksheet.Cells[i + 4, 22] = "'" + dt.Rows[i]["detailmarguige"].ToString();//规格

            //    wksheet.Cells[i + 4, 23] = "'" + dt.Rows[i]["detailmarcaizhi"].ToString();//材质

            //    wksheet.Cells[i + 4, 24] = "'" + dt.Rows[i]["detailmarguobiao"].ToString();//国标

            //    wksheet.Cells[i + 4, 25] = dt.Rows[i]["detailmarnuma"].ToString();//数量

            //    wksheet.Cells[i + 4, 26] = "'" + dt.Rows[i]["detailmarunit"].ToString();//单位

            //    wksheet.Cells[i + 4, 27] = dt.Rows[i]["detailmarnumb"].ToString();//辅助数量

            //    wksheet.Cells[i + 4, 28] = "'" + dt.Rows[i]["detailfzunit"].ToString();//辅助单位

            //    wksheet.Cells[i + 4, 29] = "'" + dt.Rows[i]["detaillength"].ToString();//长度

            //    wksheet.Cells[i + 4, 30] = "'" + dt.Rows[i]["detailwidth"].ToString();//宽度

            //    wksheet.Cells[i + 4, 31] = "'" + dt.Rows[i]["detailnote"].ToString();//备注
            //    wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 31]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
            //    wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 31]).VerticalAlignment = XlVAlign.xlVAlignCenter;
            //    wksheet.get_Range(wksheet.Cells[i + 4, 1], wksheet.Cells[i + 4, 31]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            //}
            ////设置列宽
            //wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            //string filename = Server.MapPath("/PC_Data/ExportFile/" + "采购代用单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            //ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);

            string filename = "采购代用单明细" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("采购代用单明细表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);

                #region 写入数据

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 3);

                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));//序号
                    row.CreateCell(1).SetCellValue("" + dt.Rows[i]["mpcode"].ToString());//代用单号
                    row.CreateCell(2).SetCellValue("" + dt.Rows[i]["pjnm"].ToString());//项目
                    row.CreateCell(3).SetCellValue("" + dt.Rows[i]["engnm"].ToString());//工程
                    row.CreateCell(4).SetCellValue("" + dt.Rows[i]["zdrnm"].ToString());//制单人
                    row.CreateCell(5).SetCellValue("" + dt.Rows[i]["zdtime"].ToString());//制单日期
                    row.CreateCell(6).SetCellValue("" + dt.Rows[i]["ptcode"].ToString());//计划跟踪号
                    row.CreateCell(7).SetCellValue("" + dt.Rows[i]["marid"].ToString());//物料编码
                    row.CreateCell(8).SetCellValue("" + dt.Rows[i]["marnm"].ToString());//名称
                    row.CreateCell(9).SetCellValue("" + dt.Rows[i]["marguige"].ToString());//规格
                    row.CreateCell(10).SetCellValue("" + dt.Rows[i]["marcaizhi"].ToString());//材质
                    row.CreateCell(11).SetCellValue("" + dt.Rows[i]["marguobiao"].ToString());//国标
                    row.CreateCell(12).SetCellValue(dt.Rows[i]["num"].ToString());//数量
                    row.CreateCell(13).SetCellValue("" + dt.Rows[i]["marcgunit"].ToString());//单位
                    row.CreateCell(14).SetCellValue(dt.Rows[i]["fznum"].ToString());//辅助数量
                    row.CreateCell(15).SetCellValue("" + dt.Rows[i]["fzunit"].ToString());//辅助单位
                    row.CreateCell(16).SetCellValue("" + dt.Rows[i]["length"].ToString());//长度
                    row.CreateCell(17).SetCellValue("" + dt.Rows[i]["width"].ToString());//宽度
                    row.CreateCell(18).SetCellValue("" + dt.Rows[i]["allnote"].ToString());//备注
                    row.CreateCell(19).SetCellValue("" + dt.Rows[i]["detailmarid"].ToString());//物料编码
                    row.CreateCell(20).SetCellValue("" + dt.Rows[i]["detailmarnm"].ToString());//名称
                    row.CreateCell(21).SetCellValue("" + dt.Rows[i]["detailmarguige"].ToString());//规格
                    row.CreateCell(22).SetCellValue("" + dt.Rows[i]["detailmarcaizhi"].ToString());//材质
                    row.CreateCell(23).SetCellValue("" + dt.Rows[i]["detailmarguobiao"].ToString());//国标
                    row.CreateCell(24).SetCellValue(dt.Rows[i]["detailmarnuma"].ToString());//数量
                    row.CreateCell(25).SetCellValue("" + dt.Rows[i]["detailmarunit"].ToString());//单位
                    row.CreateCell(26).SetCellValue(dt.Rows[i]["detailmarnumb"].ToString());//辅助数量
                    row.CreateCell(27).SetCellValue("" + dt.Rows[i]["detailfzunit"].ToString());//辅助单位
                    row.CreateCell(28).SetCellValue("" + dt.Rows[i]["detaillength"].ToString());//长度
                    row.CreateCell(29).SetCellValue("" + dt.Rows[i]["detailwidth"].ToString());//宽度
                    row.CreateCell(30).SetCellValue("" + dt.Rows[i]["detailnote"].ToString());//备注
                }

                #endregion

                for (int i = 0; i <= dt.Columns.Count; i++)
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
        private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        {
            try
            {

                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();
                m_xlApp.Application.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

                wksheet = null;
                workbook = null;
                m_xlApp = null;
                GC.Collect();

                //下载

                System.IO.FileInfo path = new System.IO.FileInfo(filename);

                //同步，异步都支持
                HttpResponse contextResponse = HttpContext.Current.Response;
                contextResponse.Redirect(string.Format("~/PC_Data/ExportFile/{0}", path.Name), false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

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
            ControlVisible();
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
            ControlVisible();
        }

        protected void btnqrck_click(object sender, EventArgs e)
        {
            int count = 0;
            string ptc = "";
            List<string> listscqr = new List<string>();
            for (int i = 0; i < Marreplace_list_Repeater.Items.Count; i++)
            {

                if ((Marreplace_list_Repeater.Items[i].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    count++;
                    ptc = ((System.Web.UI.WebControls.Label)Marreplace_list_Repeater.Items[i].FindControl("ptcode")).Text;
                    string sqlqrck = "update TBPC_MARREPLACEALL set scbd='1' where MP_PTCODE='" + ptc + "'";
                    listscqr.Add(sqlqrck);
                }
            }
            if (count > 0)
            {
                DBCallCommon.ExecuteTrans(listscqr);
            }
            else
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('请选择要确认的项!')</script>");
                return;
            }
            lb_CurrentPage.Text = "1";
            getArticle();
            ControlVisible();
        }

    }
}
