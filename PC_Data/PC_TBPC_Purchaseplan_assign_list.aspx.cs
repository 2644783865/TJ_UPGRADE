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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchaseplan_assign_list : BasicPage
    {
        public string gloabstate//状态，审核通过未下推1，已分工4，相似代用2，询比价6、下订单7
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
            btn_hclose.Attributes.Add("onclick", "form.target='_blank'");
            if (!IsPostBack)
            {
                //hid_filter.Text = "View_TBPC_PURCHASEPLAN_IRQ_ORDER" + "/" + Session["UserID"].ToString();
                initpager();
                initpower();
                getArticle();
            }
            CheckUser(ControlFinder);
        }

        private void initpager()
        {
            string sqltext = "";
            sqltext = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_DEPID='05' and ST_PD='0'";
            string dataText = "ST_NAME";
            string dataValue = "ST_ID";
            DBCallCommon.BindDdl(drp_stu, sqltext, dataText, dataValue);
            drp_stu.SelectedIndex = 0;
            if (Request.QueryString["ptc"] != null)
            {
                gloabptc = Request.QueryString["ptc"].ToString();
            }
            else
            {
                gloabptc = "";
            }
        }

        private void initpower()
        {

        }
        protected void btn_search_click(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }
        protected void btn_clear_click(object sender, EventArgs e)
        {
            Tb_PJNAME.Text = "";
            Tb_ENGNAME.Text = "";
            tb_riqit.Text = "";
            tb_riqif.Text = "";
            Tb_pcode.Text = "";
            drp_stu.SelectedIndex = 0;
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
                tbpc_pchsplanassign_listtRepeater.DataSource = ps;
                tbpc_pchsplanassign_listtRepeater.DataBind();
                if (tbpc_pchsplanassign_listtRepeater.Items.Count > 0)
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
        public DataTable CreateDataSource()
        {
            string sqltext = "";
            //string tableuser = hid_filter.Text;
            //string filter = "";
            //sqltext = "SELECT tableuser, filter FROM TBPC_FILTER_INFO where tableuser='" + tableuser + "'";
            //SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            //while (dr.Read())
            //{
            //    filter = dr[1].ToString();
            //}
            //dr.Close();
            sqltext = "SELECT planno AS PUR_PCODE, pjid AS PUR_PJID, pjnm AS PUR_PJNAME,PUR_ID,PUR_MASHAPE," +
                      "engid AS PUR_ENGID, engnm AS PUR_ENGNAME,ptcode AS PUR_PTCODE, " +
                      "marid AS PUR_MARID,marnm AS PUR_MARNAME,PUR_MASHAPE,margg AS PUR_MARNORM, " +
                      "marcz AS PUR_MARTERIAL,margb AS PUR_GUOBIAO,marunit AS PUR_NUNIT," +
                      "rpnum AS PUR_RPNUM,fznum AS PUR_RPFZNUM,marfzunit,fgrid AS PUR_PTASMAN," +
                      "fgrnm AS PUR_PTASMANNM,fgtime AS PUR_PTASTIME," +
                      "cgrid AS PUR_CGMAN, cgrnm AS PUR_CGMANNM,purstate AS PUR_STATE," +
                      "purnote AS PUR_NOTE,'' AS PIC_SHEETNO,'' AS PO_SHEETNO,PUR_TUHAO,sqrnm,length,width,PUR_QUXIAOREASON,PUR_IFFAST  " +
                      "FROM View_TBPC_PURCHASEPLAN_RVW where purstate>='3' and PUR_CSTATE='0' and (Pue_Closetype!='3' or Pue_Closetype is null) and purstate!='8' and purstate!='9'";
            if (rad_closelist.Checked)
            {
                sqltext = "SELECT planno AS PUR_PCODE, pjid AS PUR_PJID, pjnm AS PUR_PJNAME,PUR_ID,PUR_MASHAPE," +
                                     "engid AS PUR_ENGID, engnm AS PUR_ENGNAME,ptcode AS PUR_PTCODE, " +
                                     "marid AS PUR_MARID,marnm AS PUR_MARNAME,PUR_MASHAPE,margg AS PUR_MARNORM, " +
                                     "marcz AS PUR_MARTERIAL,margb AS PUR_GUOBIAO,marunit AS PUR_NUNIT," +
                                     "rpnum AS PUR_RPNUM,fznum AS PUR_RPFZNUM,marfzunit,fgrid AS PUR_PTASMAN," +
                                     "fgrnm AS PUR_PTASMANNM,fgtime AS PUR_PTASTIME," +
                                     "cgrid AS PUR_CGMAN, cgrnm AS PUR_CGMANNM,purstate AS PUR_STATE," +
                                     "purnote AS PUR_NOTE,'' AS PIC_SHEETNO,'' AS PO_SHEETNO,PUR_TUHAO,sqrnm,length,width,PUR_QUXIAOREASON,PUR_IFFAST  " +
                                     "FROM View_TBPC_PURCHASEPLAN_RVW where purstate>='3' and (PUR_CSTATE='1' or PUR_CSTATE='2') and (Pue_Closetype!='3' or Pue_Closetype is null) and purstate!='8' and purstate!='9' and rpnum>0 and fznum>0";
            }
            if (rad_yzanting.Checked)
            {
                sqltext = "SELECT planno AS PUR_PCODE, pjid AS PUR_PJID, pjnm AS PUR_PJNAME,PUR_ID,PUR_MASHAPE," +
                                     "engid AS PUR_ENGID, engnm AS PUR_ENGNAME,ptcode AS PUR_PTCODE, " +
                                     "marid AS PUR_MARID,marnm AS PUR_MARNAME,PUR_MASHAPE,margg AS PUR_MARNORM, " +
                                     "marcz AS PUR_MARTERIAL,margb AS PUR_GUOBIAO,marunit AS PUR_NUNIT," +
                                     "rpnum AS PUR_RPNUM,fznum AS PUR_RPFZNUM,marfzunit,fgrid AS PUR_PTASMAN," +
                                     "fgrnm AS PUR_PTASMANNM,fgtime AS PUR_PTASTIME," +
                                     "cgrid AS PUR_CGMAN, cgrnm AS PUR_CGMANNM,purstate AS PUR_STATE," +
                                     "purnote AS PUR_NOTE,'' AS PIC_SHEETNO,'' AS PO_SHEETNO,PUR_TUHAO,sqrnm,length,width,PUR_QUXIAOREASON,PUR_IFFAST  " +
                                     "FROM View_TBPC_PURCHASEPLAN_RVW where purstate>='3' and PUR_CSTATE='0' and Pue_Closetype='3' and purstate!='8' and purstate!='9'";
            }
            if (rad_wlcode.Checked)
            {
                sqltext += " order by marid ";
            }
            else
            {
                sqltext += " order by ptcode ";
            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DataView dv = null;
            //if (filter != "")
            //{
            //    dv = dt.DefaultView;
            //    dv.RowFilter = filter;
            //    dt = dv.ToTable();
            //}
            if (rad_stall.Checked)
            {
                dv = dt.DefaultView;
                dt = dv.ToTable();
            }
            else if (rad_weifengong.Checked)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_STATE='3'";//未分工
                dt = dv.ToTable();
                btn_back.Visible = true;
                btn_hclose.Visible = true;
                btn_zanting.Visible = true;
                btn_qxzanting.Visible = false;
            }
            else if (rad_yifengong.Checked)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_STATE>='4'";//分工
                dt = dv.ToTable();
            }
            else if (rad_stwzx.Checked)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_STATE='4'";//分工未执行
                dt = dv.ToTable();
            }
            else if (rad_stxbj.Checked)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_STATE>='6'";//询比价
                dt = dv.ToTable();
            }
            else if (rad_stxdd.Checked)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_STATE='7'";//订单
                dt = dv.ToTable();
            }
            else//默认全部
            {
                dv = dt.DefaultView;
                dt = dv.ToTable();
            }
            if (Tb_PJNAME.Text != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_MARNAME like '%" + Tb_PJNAME.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_guige.Text != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_MARNORM like '%" + tb_guige.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (Tb_ENGNAME.Text != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_PTCODE like '%" + Tb_ENGNAME.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (tb_riqif.Text != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_PTASTIME>='" + tb_riqif.Text + "'"; //对dataView进行筛选 
                dt = dv.ToTable();
                //dataView.Sort = "PCODE ASC";
            }
            if (tb_riqit.Text != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_PTASTIME<='" + tb_riqit.Text + "'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (Tb_pcode.Text != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_PCODE like '%" + Tb_pcode.Text.Trim() + "%'"; //对dataView进行筛选 
                dt = dv.ToTable();
            }
            if (drp_stu.SelectedValue.ToString() != "-请选择-")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_CGMAN='" + drp_stu.SelectedValue.ToString() + "'";
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
        protected void rad_stall_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            btn_back.Visible = false;
            btn_hclose.Visible = false;
            btn_zanting.Visible = false;
            btn_qxzanting.Visible = false;
        }
        protected void rad_weifengong_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            btn_back.Visible = true;
            btn_hclose.Visible = true;
            btn_zanting.Visible = true;
            btn_qxzanting.Visible = false;
        }
        protected void rad_yifengong_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            btn_back.Visible = false;
            btn_hclose.Visible = false;
            btn_zanting.Visible = false;
            btn_qxzanting.Visible = false;
        }
        protected void rad_stwzx_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            btn_back.Visible = false;
            btn_hclose.Visible = false;
            btn_zanting.Visible = false;
            btn_qxzanting.Visible = false;
        }
        protected void rad_stxbj_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            btn_back.Visible = false;
            btn_hclose.Visible = false;
            btn_zanting.Visible = false;
            btn_qxzanting.Visible = false;
        }
        protected void rad_stxdd_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            btn_back.Visible = false;
            btn_hclose.Visible = false;
            btn_zanting.Visible = false;
            btn_qxzanting.Visible = false;
        }

        protected void rad_ptcode_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }
        protected void rad_wlcode_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
        }


        protected void rad_closelist_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            btn_back.Visible = false;
            btn_hclose.Visible = false;
            btn_zanting.Visible = false;
            btn_qxzanting.Visible = false;
        }

        protected void rad_yzanting_CheckedChanged(object sender, EventArgs e)
        {
            lb_CurrentPage.Text = "1";
            getArticle();
            btn_back.Visible = false;
            btn_hclose.Visible = false;
            btn_zanting.Visible = false;
            btn_qxzanting.Visible = true;
        }


        protected void tbpc_pchsplanassign_listtRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string state = "";
            string jhpno = "";
            string bjdsheetno = "";
            string ddsheetno = "";
            string ptcode = "";
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //订单、比价单处理
                #region
             //   state = ((Label)e.Item.FindControl("PUR_STATE")).Text;
                state = ((HtmlInputHidden)e.Item.FindControl("PUR_STATE")).Value;

                ptcode = ((Label)e.Item.FindControl("PUR_PTCODE")).Text;
                if (Convert.ToInt32(state) >= 4)
                {
                    jhpno = ((Label)e.Item.FindControl("PUR_PCODE")).Text;
                    ((Label)e.Item.FindControl("PUR_FG")).ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)e.Item.FindControl("Hypfg")).NavigateUrl = "PC_TBPC_Purchaseplan_assign.aspx?sheetno=" + jhpno + "&ptc=" + ptcode + "";
                }
                if (Convert.ToInt32(state) >= 6)
                {
                    bjdsheetno = ((Label)e.Item.FindControl("PIC_SHEETNO")).Text;
                    ((Label)e.Item.FindControl("PUR_BJD")).ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)e.Item.FindControl("Hypbjd")).NavigateUrl = "TBPC_IQRCMPPRCLST_checked_detail.aspx?sheetno=" + bjdsheetno + "&ptc=" + ptcode + "";
                }
                if (Convert.ToInt32(state) >= 7)
                {
                    ddsheetno = ((Label)e.Item.FindControl("PO_SHEETNO")).Text;
                    ((Label)e.Item.FindControl("PUR_DD")).ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)e.Item.FindControl("Hypdd")).NavigateUrl = "PC_TBPC_PurOrder.aspx?orderno=" + ddsheetno + "&ptc=" + ptcode + "";
                }

                string num = ((Label)e.Item.FindControl("PUR_RPNUM")).Text == "" ? "0" : ((Label)e.Item.FindControl("PUR_RPNUM")).Text;
                if (decimal.Parse(num) == 0)
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "Yellow";
                }
                string quixaoreason = ((Label)e.Item.FindControl("PUR_QUXIAOREASON")).Text.Trim();
                if (quixaoreason != "")
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "red";
                }


                string PUR_IFFAST = ((Label)e.Item.FindControl("PUR_IFFAST")).Text.Trim();
                if (PUR_IFFAST == "1")
                {
                    ((Label)e.Item.FindControl("lbjiaji")).Visible = true;
                }
                #endregion
            }
        }

        //protected void btn_hclose_Click(object sender, EventArgs e)
        //{
        //    string sqltext = "";
        //    string ptcode = "";
        //    foreach (RepeaterItem Reitem in tbpc_pchsplanassign_listtRepeater.Items)
        //    {
        //        CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
        //        if (cbx.Checked)
        //        {
        //            ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text;
        //            sqltext = "update TBPC_PURCHASEPLAN set (PUR_CSTATE='1' or PUR_CSTATE='2') where PUR_PTCODE='" + ptcode + "'";//行关闭
        //            DBCallCommon.ExeSqlText(sqltext); 
        //        }
        //    }
        //    lb_CurrentPage.Text = "1";
        //    getArticle();
        //}

        protected void btn_back_Click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            string sqltext = "";
            string ptcode = "";
            string pcode = "";
            int i = 0;
            foreach (RepeaterItem Reitem in tbpc_pchsplanassign_listtRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                    ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text;
                    pcode = ((Label)Reitem.FindControl("PUR_PCODE")).Text;
                    sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='0',PUR_CSTATE='0',PUR_BACKNOTE='" + tbreason.Text.ToString().Trim() + "' where PUR_PTCODE='" + ptcode + "'";
                    sqltextlist.Add(sqltext);
                    sqltext = "update TBPC_PCHSPLANRVW set PR_STATE='0' where PR_SHEETNO='" + pcode + "'";
                    sqltextlist.Add(sqltext);
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据');", true);
                ModalPopupExtenderSearch.Hide();
                getArticle();
            }
            else
            {
                DBCallCommon.ExecuteTrans(sqltextlist);
                lb_CurrentPage.Text = "1";
                ModalPopupExtenderSearch.Hide();
                getArticle();
            } 
        }


        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
            getArticle();
        }





        public string get_pur_fg(string fgst)
        {
            string statestr = "";
            if (Convert.ToInt32(fgst) >= 4)
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }
        public string get_pur_bjd(string bjdst)
        {
            string statestr = "";
            if (Convert.ToInt32(bjdst) >= 6)
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }
        public string get_pur_dd(string ddst)
        {
            string statestr = "";
            if (Convert.ToInt32(ddst) >= 7)
            {
                statestr = "是";
            }
            else
            {

                statestr = "否";

            }
            return statestr;
        }

        protected void selectall_CheckedChanged(object sender, EventArgs e)
        {
            if (selectall.Checked)
            {
                foreach (RepeaterItem Reitem in tbpc_pchsplanassign_listtRepeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        if (cbx.Enabled != false)
                        {
                            cbx.Checked = true;
                        }
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Reitem in tbpc_pchsplanassign_listtRepeater.Items)
                {
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = false;
                    }
                }
            }
        }
        protected void btn_LX_click(object sender, EventArgs e)
        {
            int i = 0;
            int j = 0;
            int start = 0;
            int finish = 0;
            int k = 0;
            foreach (RepeaterItem Reitem in tbpc_pchsplanassign_listtRepeater.Items)
            {
                j++;
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                if (cbx.Checked)
                {
                    i++;
                    if (start == 0)
                    {
                        start = j;
                    }
                    else
                    {
                        finish = j;
                    }
                }
            }
            if (i == 2)
            {
                foreach (RepeaterItem Reitem in tbpc_pchsplanassign_listtRepeater.Items)
                {
                    k++;
                    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                    if (k >= start && k <= finish)
                    {
                        if (cbx.Enabled == true)
                        {
                            cbx.Checked = true;
                        }

                    }
                    if (k > finish)
                    {
                        cbx.Checked = false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择连续的区间！');", true);
            }
        }
        protected void btn_QX_click(object sender, EventArgs e)
        {
            foreach (RepeaterItem Reitem in tbpc_pchsplanassign_listtRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
                cbx.Checked = false;
            }
        }

        protected void btn_hclose_Click(object sender, EventArgs e)
        {
            int j = 0;
            string pcode1 = "";
            string pcode2 = "";
            string shape = "";
            string ptcode = "";
            foreach (RepeaterItem Reitem in tbpc_pchsplanassign_listtRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    j++;
                    pcode1 = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PCODE")).Text;
                    if (j == 1)
                    {
                        pcode2 = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PCODE")).Text;
                        shape = ((System.Web.UI.WebControls.HiddenField)Reitem.FindControl("Hid_MaShape")).Value;
                    }
                    if (pcode1 != pcode2)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择同一批次的物料！');", true);
                        return;
                    }
                    else
                    {
                        ptcode = ptcode + ((System.Web.UI.WebControls.HiddenField)Reitem.FindControl("Hid_PurId")).Value + ",";
                    }

                }
            }
            if (j == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据！');", true);
            }
            else
            {
                ptcode = ptcode.Substring(0, ptcode.LastIndexOf(","));
                Response.Redirect("~/PC_Data/PC_Data_hangclose.aspx?num=2&shape=" + shape + "&orderno=" + pcode2 + "&arry=" + ptcode + "");
            }
        }


        //任务暂停
        protected void btn_zanting_Click(object sender, EventArgs e)
        {
            List<string> sql_list = new List<string>();
            string sql = "";
            string ptc = "";
            int i = 0;
            foreach (RepeaterItem Reitem in tbpc_pchsplanassign_listtRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                    ptc = ((Label)Reitem.FindControl("PUR_PTCODE")).Text;
                    sql = "update TBPC_PURCHASEPLAN set Pue_Closetype='3',PUR_CSTATE='0' where PUR_PTCODE='" + ptc + "'";
                    sql_list.Add(sql);
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据');", true);
                ModalPopupExtenderSearch.Hide();
                getArticle();
            }
            else
            {
                DBCallCommon.ExecuteTrans(sql_list);
                lb_CurrentPage.Text = "1";
                getArticle();
            }
        }

        //取消暂停
        protected void btn_qxzanting_Click(object sender, EventArgs e)
        {
            List<string> sql_list = new List<string>();
            string sql = "";
            string ptc = "";
            int i = 0;
            foreach (RepeaterItem Reitem in tbpc_pchsplanassign_listtRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                    ptc = ((Label)Reitem.FindControl("PUR_PTCODE")).Text;
                    sql = "update TBPC_PURCHASEPLAN set Pue_Closetype=NULL,PUR_CSTATE='0' where PUR_PTCODE='" + ptc + "'";
                    sql_list.Add(sql);
                }
            }
            if (i == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据');", true);
                ModalPopupExtenderSearch.Hide();
                getArticle();
            }
            else
            {
                DBCallCommon.ExecuteTrans(sql_list);
                lb_CurrentPage.Text = "1";
                getArticle();
            }
        }
    }
}
