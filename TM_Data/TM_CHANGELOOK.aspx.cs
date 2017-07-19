using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_CHANGELOOK : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.BindPerson();
                bind();
            }
        }
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            //取得显示分页界面的那一行
            GridViewRow pagerRow = GridView1.BottomPagerRow;
            if (pagerRow != null)
            {
                //取得第一页。上一页。下一页。最后一页的超级链接
                LinkButton lnkBtnFirst = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnFirst");
                LinkButton lnkBtnPrev = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnPrev");
                LinkButton lnkBtnNext = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnNext");
                LinkButton lnkBtnLast = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnLast");

                //设置何时应该禁用第一页。上一页。下一页。最后一页的超级链接
                if (GridView1.PageIndex == 0)
                {
                    lnkBtnFirst.Enabled = false;
                    lnkBtnPrev.Enabled = false;
                }
                else if (GridView1.PageIndex == GridView1.PageCount - 1)
                {
                    lnkBtnNext.Enabled = false;
                    lnkBtnLast.Enabled = false;
                }
                else if (GridView1.PageCount <= 0)
                {
                    lnkBtnFirst.Enabled = false;
                    lnkBtnPrev.Enabled = false;
                    lnkBtnNext.Enabled = false;
                    lnkBtnLast.Enabled = false;
                }
                //从显示分页的行中取得用来显示页次与切换分页的DropDownList控件
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("page_DropDownList");

                //根据欲显示的数据源的总页数，创建DropDownList控件的下拉菜单内容
                if (pageList != null)
                {
                    int intPage;
                    for (intPage = 0; intPage <= GridView1.PageCount - 1; intPage++)
                    {
                        //创建一个ListItem对象来存放分页列表
                        int pageNumber = intPage + 1;
                        ListItem item = new ListItem(pageNumber.ToString());

                        //交替显示背景颜色
                        switch (pageNumber % 2)
                        {
                            case 0: item.Attributes.Add("style", "background:#CDC9C2;");
                                break;
                            case 1: item.Attributes.Add("style", "color:red; background:white;");
                                break;
                        }
                        if (intPage == GridView1.PageIndex)
                        {
                            item.Selected = true;
                        }
                        pageList.Items.Add(item);
                    }
                }
                //显示当前所在页数与总页数
                System.Web.UI.WebControls.Label pagerLabel = (System.Web.UI.WebControls.Label)pagerRow.Cells[0].FindControl("lblCurrrentPage");

                if (pagerLabel != null)
                {

                    int currentPage = GridView1.PageIndex + 1;
                    pagerLabel.Text = "第" + currentPage.ToString() + "页（共" + GridView1.PageCount.ToString() + " 页）";

                }
            }

        }

        protected void page_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //取得显示分页界面的那一行
            GridViewRow pagerRow = GridView1.BottomPagerRow;
            //从显示页数的行中取得显示页数的DropDownList控件
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("page_DropDownList");
            //将GridView移至用户所选择的页数
            GridView1.PageIndex = pageList.SelectedIndex;
            bind();
            //getData();不用数据源需要绑定
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变 
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
                //鼠标移出时，行背景色变 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }

        protected void bind()
        {
            this.SetRadioButtonTip();
            string sqltext = "";
            sqltext = "SELECT * FROM [dbo].[View_PC_MPTEMPCHANGE] where ";
            sqltext += this.GetStrWhere();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }

        protected void QueryButton_Click(object sender, EventArgs e)
        {
            bind();
        }

        
        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {
            tb_ptcode.Text = "";
            tb_pjnm.Text = "";
            tb_engnm.Text = "";
            tb_marid.Text = "";
            tb_marnm.Text = "";
            tb_margg.Text = "";
            tb_marcz.Text = "";
            tb_margb.Text = "";
            bind();
        }

        /// <summary>
        /// 连选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_OnClick(object sender, EventArgs e)
        {
            CheckBoxSelectDefine(GridView1, "ckbSelect");
        }

        /// <summary>
        /// CheckBox连选(此函数勿动)
        /// </summary>
        /// <param name="smartgridview"></param>
        /// <param name="ckbname"></param>
        public void CheckBoxSelectDefine(GridView smartgridview, string ckbname)
        {
            int startindex = -1;
            int endindex = -1;
            for (int i = 0; i < smartgridview.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)smartgridview.Rows[i].FindControl(ckbname);
                if (cbx.Checked)
                {
                    startindex = i;
                    break;
                }
            }

            for (int j = smartgridview.Rows.Count - 1; j > -1; j--)
            {
                CheckBox cbx = (CheckBox)smartgridview.Rows[j].FindControl(ckbname);
                if (cbx.Checked)
                {
                    endindex = j;
                    break;
                }
            }

            if (startindex < 0 || endindex < 0 || startindex == endindex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('需要勾选2条记录！！！');", true);
            }
            else
            {
                for (int k = startindex; k <= endindex; k++)
                {
                    CheckBox cbx = (CheckBox)smartgridview.Rows[k].FindControl(ckbname);
                    cbx.Checked = true;
                }
            }
        }
        /// <summary>
        /// 查询RadioButtonList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblCheckType_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            if (rblCheckType.SelectedValue == "1" || rblCheckType.SelectedValue == "2")//待备库和我的任务
            {
                btnToStore.Visible = false;
                btnCancleBK.Visible = false;
            }
            else if (rblCheckType.SelectedValue == "3")//待执行
            {
                btnToStore.Visible = false;
                btnCancleBK.Visible = true;
            }
            else
            {
                btnToStore.Visible = false;
                btnCancleBK.Visible = false;
            }
            this.bind();
        }
        /// <summary>
        /// 获取查询条件
        /// </summary>
        /// <returns></returns>
        protected string GetStrWhere()
        {
            string sqltext = " MP_STATE='1' and MP_BGZXNUM=0 and MP_BGZXFZNUM=0 ";
            if (rblCheckType.SelectedValue == "0")//全部
            {
                sqltext += " ";
            }
            else if (rblCheckType.SelectedValue == "1")//待执行
            {
                sqltext += " and MP_EXECSTATE='0'";
            }
            else if (rblCheckType.SelectedValue == "2")//我的任务
            {
                sqltext += " and MP_EXECSTATE='0' and MP_CHSUBMITNMID='" + Session["UserID"].ToString() + "' ";
            }
            else if (rblCheckType.SelectedValue == "3")//待备库
            {
                sqltext += " and MP_EXECSTATE='1'";
            }
            else if (rblCheckType.SelectedValue == "4")//调整中
            {
                sqltext += " and MP_EXECSTATE='2'";
            }
            else if (rblCheckType.SelectedValue == "5")//调整完成
            {
                sqltext += " and MP_EXECSTATE='3'";
            }
            else if (rblCheckType.SelectedValue == "6")//已关闭
            {
                sqltext += " and MP_EXECSTATE='4'";
            }  

            if (tb_ptcode.Text != "")
            {
                sqltext += " and [MP_CHPTCODE] like '%" + tb_ptcode.Text.Trim() + "%'";
            }
            if (tb_pjnm.Text != "")
            {
                sqltext += " and [MP_PROJNAME] like '%" + tb_pjnm.Text.Trim() + "%'";
            }
            if (tb_engnm.Text != "")
            {
                sqltext += " and [TSA_ENGNAME] like '%" + tb_engnm.Text.Trim() + "%'";
            }
            if (tb_marid.Text != "")
            {
                sqltext += " and [MP_MARID] like '%" + tb_marid.Text.Trim() + "%'";
            }
            if (tb_marnm.Text != "")
            {
                sqltext += " and [MNAME] like '%" + tb_marnm.Text.Trim() + "%'";
            }
            if (tb_margg.Text != "")
            {
                sqltext += " and [GUIGE] like '%" + tb_margg.Text.Trim() + "%'";
            }
            if (tb_marcz.Text != "")
            {
                sqltext += " and [CAIZHI] like '%" + tb_marcz.Text.Trim() + "%'";
            }
            if (tb_margb.Text != "")
            {
                sqltext += " and [GB] like '%" + tb_margb.Text.Trim() + "%'";
            }
            //图号
            sqltext += " and [MP_TUHAO] like '%"+txtTuhao.Text.Trim()+"%'";
            //提交人
            sqltext += " and (MP_SUBNAME like '%" + txtSubID.Text.Trim() + "%' or [MP_SUBNAME] is null)";
            //执行人
            sqltext += " and ([MP_EXECNAME] like '%" + txtExecID.Text.Trim() + "%' or [MP_EXECNAME] is null)";
            //备注
            if (txtBK_Note.Text.Trim() == "")
            {
                sqltext += " and ([MP_BKNOTE]='' or [MP_BKNOTE] is null)";
            }
            else
            {
                sqltext += " and ([MP_BKNOTE] like '%" + txtBK_Note.Text.Trim() + "%' or [MP_BKNOTE] is null)";
            }
            return sqltext;
        }
        /// <summary>
        /// 备库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnToStore_OnClick(object sender, EventArgs e)
        {
            if (ddlBKPersonName.SelectedIndex != 0)
            {
                List<string> list_sql = new List<string>();
                foreach (GridViewRow grow in GridView1.Rows)
                {
                    if (((CheckBox)grow.FindControl("ckbSelect")).Checked)
                    {
                        string mp_id = ((Label)grow.FindControl("lblID")).Text.Trim();
                        string sqltext = "update TBPC_MPTEMPCHANGE set MP_EXECSTATE='1',MP_SUBID='" + Session["UserID"].ToString() + "',MP_SUBTIME='" + System.DateTime.Now.ToString("yyyy-MM-dd") + "',MP_EXECID='" + ddlBKPersonName.SelectedValue + "' where MP_ID='" + mp_id + "'";
                        list_sql.Add(sqltext);
                    }
                }
                DBCallCommon.ExecuteTrans(list_sql);
                this.bind();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择【储运备库人】！！！');", true);
            }
        }
        /// <summary>
        /// 取消备库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancleBK_OnClick(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            foreach (GridViewRow grow in GridView1.Rows)
            {
                if (((CheckBox)grow.FindControl("ckbSelect")).Checked)
                {
                    string mp_id = ((Label)grow.FindControl("lblID")).Text.Trim();
                    string sqltext = "update TBPC_MPTEMPCHANGE set MP_EXECSTATE='0',MP_SUBID='',MP_SUBTIME='',MP_EXECID='' where MP_ID='" + mp_id + "' and MP_EXECSTATE='1'";
                    list_sql.Add(sqltext);
                }
            }
            DBCallCommon.ExecuteTrans(list_sql);
            this.bind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！！！');", true);
        }


        protected void SetRadioButtonTip()
        {
            string selectValue = rblCheckType.SelectedValue;
            rblCheckType.Items.Clear();
            int all_bk = 0;
            int my_bk = 0;
            int bk_wait = 0;

            string sqltext_all_bk = "select count(*) from View_PC_MPTEMPCHANGE where MP_STATE='1' and MP_BGZXNUM=0 and MP_BGZXFZNUM=0 and MP_EXECSTATE='0'";
            string sqltext_my_bk = "select count(*) from View_PC_MPTEMPCHANGE where MP_STATE='1' and MP_BGZXNUM=0 and MP_BGZXFZNUM=0 and MP_EXECSTATE='0' and MP_CHSUBMITNMID='"+Session["UserID"].ToString()+"'";
            string sqltext_bk_wait = "select count(*) from View_PC_MPTEMPCHANGE where MP_STATE='1' and MP_BGZXNUM=0 and MP_BGZXFZNUM=0 and MP_EXECSTATE='1'";
            all_bk =Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_all_bk).Rows[0][0].ToString());
            my_bk = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_my_bk).Rows[0][0].ToString());
            bk_wait = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_bk_wait).Rows[0][0].ToString());

            rblCheckType.Items.Add(new ListItem("全部", "0"));

            if (all_bk != 0)
            {
                rblCheckType.Items.Add(new ListItem("未执行" + "</label><label><font color=red>(" + all_bk + ")</font>", "1"));
            }
            else
            {
                rblCheckType.Items.Add(new ListItem("未执行", "1"));
            }

            if (my_bk != 0)
            {
                rblCheckType.Items.Add(new ListItem("我的任务" + "</label><label><font color=red>(" + my_bk + ")</font>", "2"));
            }
            else
            {
                rblCheckType.Items.Add(new ListItem("我的任务", "2"));
            }

            if (bk_wait != 0)
            {
                rblCheckType.Items.Add(new ListItem("待备库" + "</label><label><font color=red>(" + bk_wait + ")</font>", "3"));
            }
            else
            {
                rblCheckType.Items.Add(new ListItem("待备库", "3"));
            }

            rblCheckType.Items.Add(new ListItem("调整中", "4"));
            rblCheckType.Items.Add(new ListItem("调整完成", "5"));
            rblCheckType.Items.Add(new ListItem("关闭", "6"));

            rblCheckType.SelectedValue = selectValue;
        }

        protected void BindPerson()
        {
            string sql = "SELECT ST_CODE,ST_NAME collate  Chinese_PRC_CS_AS_KS_WS AS [ST_NAME] FROM [dbo].[TBDS_STAFFINFO] WHERE [ST_CODE] LIKE '0702%' AND ST_PD='0' ORDER BY [ST_NAME] collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "ST_NAME";
            string dataValue = "ST_CODE";
            DBCallCommon.BindDdl(ddlBKPersonName, sql, dataText, dataValue);
        }
    }
}
