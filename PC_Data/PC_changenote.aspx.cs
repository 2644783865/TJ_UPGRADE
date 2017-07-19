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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_changenote : System.Web.UI.Page
    {
        PagerQueryParam pager_org = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
            this.InitVar();
            if (radio_weiqueren.Checked)
            {
                btntongguo.Visible = true;
                btnbutongguo.Visible = true;
            }
            else
            {
                btntongguo.Visible = false;
                btnbutongguo.Visible = false;
            }
        }

        #region  分页
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        private void InitPager()
        {
            pager_org.TableName = "TBPC_changebeizhu as a left join View_TBPC_PLAN_PLACE as b on a.change_PTC=b.Aptcode";
            pager_org.PrimaryKey = "ID";
            pager_org.ShowFields = "*";
            pager_org.OrderField = "change_PTC";
            pager_org.StrWhere = strwhere();
            pager_org.OrderType = 1;//升序排列
            pager_org.PageSize = 30;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamWithPriKey(pager_org);
            CommonFun.Paging(dt, purchaseplan_start_list_Repeater, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }

        }

        private string strwhere()
        {
            string sqltext = "1=1";
            if (radio_weiqueren.Checked)
            {
                sqltext += " and changestate='0' or changestate='' or changestate is null";
            }
            if (radio_tongguo.Checked)
            {
                sqltext += " and changestate='1'";
            }
            if (radio_butongguo.Checked)
            {
                sqltext += " and changestate='2'";
            }
            if (tbptc.Text.Trim() != "")
            {
                sqltext += " and change_PTC like '%" + tbptc.Text.Trim() + "%'";
            }
            return sqltext;
        }
        #endregion
        protected void radio_CheckedChanged(object sender, EventArgs e)
        {
            this.InitVar();
            this.bindGrid();
        }
        //查询
        protected void btnsearch_click(object sender, EventArgs e)
        {
            this.InitVar();
            this.bindGrid();
        }
        //通过
        protected void btntongguo_click(object sender, EventArgs e)
        {
            List<string> listsql = new List<string>();
            int m = 0;
            string sqltext = "";
            for (int r = 0; r < purchaseplan_start_list_Repeater.Items.Count; r++)
            {
                if ((purchaseplan_start_list_Repeater.Items[r].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    m++;
                }
            }
            if (m == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择任何项!')", true);
                return;
            }

            string querenren = Session["UserName"].ToString().Trim();
            string querentime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim();
            for (int i = 0; i < purchaseplan_start_list_Repeater.Items.Count; i++)
            {
                if ((purchaseplan_start_list_Repeater.Items[i].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptcodechange = (purchaseplan_start_list_Repeater.Items[i].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text.Trim();
                    string changecontent = (purchaseplan_start_list_Repeater.Items[i].FindControl("changecontent") as System.Web.UI.WebControls.TextBox).Text.Trim();
                    string sql = "update TBPC_changebeizhu set querenren='" + querenren + "',querentime='" + querentime + "',changestate='1' where change_PTC='" + ptcodechange + "'";
                    listsql.Add(sql);
                    //更新物料信息
                    sqltext = "update TBPC_PURCHASEPLAN set PUR_NOTE=PUR_NOTE+'(" + changecontent + ")' where PUR_PTCODE='" + ptcodechange + "'";
                    listsql.Add(sqltext);
                    sqltext = "update TBPC_IQRCMPPRICE set PIC_NOTE=PIC_NOTE+'(" + changecontent + ")' where PIC_PTCODE='" + ptcodechange + "'";
                    listsql.Add(sqltext);
                    sqltext = "update TBPC_PURORDERDETAIL set PO_NOTE=PO_NOTE+'(" + changecontent + ")' where PO_PTCODE='" + ptcodechange + "'";
                    listsql.Add(sqltext);
                }
            }
            DBCallCommon.ExecuteTrans(listsql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功!')", true);
            this.InitVar();
            this.bindGrid();
        }
        //不通过
        protected void btnbutongguo_click(object sender, EventArgs e)
        {
            List<string> listsql = new List<string>();
            int m = 0;
            for (int r = 0; r < purchaseplan_start_list_Repeater.Items.Count; r++)
            {
                if ((purchaseplan_start_list_Repeater.Items[r].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    m++;
                }
            }
            if (m == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择任何项!')", true);
                return;
            }

            string querenren = Session["UserName"].ToString().Trim();
            string querentime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim();
            for (int i = 0; i < purchaseplan_start_list_Repeater.Items.Count; i++)
            {
                if ((purchaseplan_start_list_Repeater.Items[i].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptcodechange = (purchaseplan_start_list_Repeater.Items[i].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text;
                    string sql = "update TBPC_changebeizhu set querenren='" + querenren + "',querentime='" + querentime + "',changestate='2' where change_PTC='" + ptcodechange + "'";
                    listsql.Add(sql);
                }
            }
            DBCallCommon.ExecuteTrans(listsql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功!')", true);
            this.InitVar();
            this.bindGrid();
        }
        //删除
        protected void btndelete_click(object sender, EventArgs e)
        {
            List<string> listsql = new List<string>();
            int m = 0;
            int n = 0;
            for (int r = 0; r < purchaseplan_start_list_Repeater.Items.Count; r++)
            {
                if ((purchaseplan_start_list_Repeater.Items[r].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    m++;
                }
                if ((purchaseplan_start_list_Repeater.Items[r].FindControl("changername") as System.Web.UI.WebControls.Label).Text.Trim() != Session["UserName"].ToString().Trim() || (purchaseplan_start_list_Repeater.Items[r].FindControl("changestate") as System.Web.UI.WebControls.Label).Text.Trim() == "确认通过")
                {
                    n++;
                }
            }
            if (m == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择任何项!')", true);
                return;
            }
            if (n > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已通过项和非本人操作数据无法删除!')", true);
                return;
            }
            
            for (int i = 0; i < purchaseplan_start_list_Repeater.Items.Count; i++)
            {
                if ((purchaseplan_start_list_Repeater.Items[i].FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox).Checked)
                {
                    string ptcodechange = (purchaseplan_start_list_Repeater.Items[i].FindControl("Aptcode") as System.Web.UI.WebControls.TextBox).Text.Trim();
                    string sql = "delete from TBPC_changebeizhu where change_PTC='" + ptcodechange + "'";
                    listsql.Add(sql);
                }
            }
            DBCallCommon.ExecuteTrans(listsql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功!')", true);
            this.InitVar();
            this.bindGrid();
        }
    }
}
