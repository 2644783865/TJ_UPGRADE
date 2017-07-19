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
    public partial class PC_TBPC_Purchaseplan_assign_detail : System.Web.UI.Page
    {
        public string gloabstate//状态，询比价6、下订单7
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
        public string gloabshape
        {
            get
            {
                object str = ViewState["gloabshape"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabshape"] = value;
            }
        }
        public string gloabding
        {
            get
            {
                object str = ViewState["gloabding"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabding"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_fanshen.Attributes.Add("OnClick", "Javascript:return confirm('确定取消,执行此操作将取消任务分工?');");
            btn_fanshen.Click += new EventHandler(btn_fanshen_Click);
            if (!IsPostBack)
            {
                Initpage();
                initpower();
                repeaterdatabind();//绑定数据源
            }
        }

        private void Initpage()
        {
            if (Request.QueryString["sheetno"] != null)
            {
                gloabsheetno = Request.QueryString["sheetno"].ToString();
            }
            else
            {
                gloabsheetno = "";
            }
            if (Request.QueryString["ptc"] != null)
            {
                gloabptc = Request.QueryString["ptc"].ToString();
            }
            else
            {
                gloabptc = "";
            }
            TextBox_pid.Text = gloabsheetno;
            Tb_shijian.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            ddl_caigouyuanbind();//采购员下拉框数据绑定
            Init_marty();
        }
        private void initpower()
        {

        }
        private void Init_marty()
        {
            string sqltext = "SELECT TY_ID AS PUR_TY_ID,TY_NAME AS PUR_TY_NAME FROM TBMA_TYPEINFO WHERE (TY_ID IN (SELECT DISTINCT SUBSTRING(PUR_MARID, 1, 5) AS Expr1 FROM TBPC_PURCHASEPLAN WHERE PUR_PCODE='" + TextBox_pid.Text.ToString() + "'))";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                DropDownList_TY.DataSource = dt;
                DropDownList_TY.DataBind();
                ListItem item = new ListItem();
                item.Value = "00";
                item.Text = "--全部--";
                DropDownList_TY.Items.Insert(0, item);
                DropDownList_TY.Items[0].Selected = true;
            }
        }
        private void ddl_caigouyuanbind()
        {
            string sqltext = "";
            sqltext = "select ST_NAME AS PUR_ST_NAME,ST_ID AS PUR_ST_CODE " +
                     "from TBDS_STAFFINFO WHERE ST_DEPID='" +
                     Session["UserDeptID"].ToString() + "' and ST_POSITION!='0504' and ST_PD='0' ORDER BY ST_CODE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ddl_caigouyuan.DataSource = dt;
            ddl_caigouyuan.DataBind();
            ListItem item = new ListItem();
            item.Text = "--请选择--";
            item.Value = "00";
            ddl_caigouyuan.Items.Insert(0, item);
            ddl_caigouyuan.SelectedValue = "00";
        }
        private void repeaterdatabind()
        {
            string sqltext;
            sqltext = "SELECT planno AS PUR_PCODE, pjid AS PUR_PJID, pjnm AS PUR_PJNAME, "+
                      "engid AS PUR_ENGID, engnm AS PUR_ENGNAME, ptcode AS PUR_PTCODE, "+
                      "marid AS PUR_MARID,marnm AS PUR_MARNAME,PUR_MASHAPE, margg AS PUR_MARNORM,"+
                      "marcz AS PUR_MARTERIAL, margb AS PUR_GUOBIAO,isnull(num,0) AS PUR_NUM, "+
                      "isnull(fznum,0) AS PUR_FZNUM,marunit AS PUR_NUNIT,isnull(rpnum,0) AS PUR_RPNUM,"+
                      "marfzunit AS PUR_FZUNIT,isnull(rpfznum,0) AS PUR_RPFZNUM,jstimerq AS PUR_TIMEQ,sqrnm, " +
                      "fgrid AS PUR_PTASMAN,fgrnm AS PUR_PTASMANNM,fgtime AS PUR_PTASTIME," +
                      "cgrid as PUR_CGMANCODE,cgrnm as PUR_CGMANNAME,keycoms AS PUR_KEYCOMS,"+
                      "purstate AS PUR_STATE,purnote AS PUR_NOTE,PUR_MASHAPE,isnull(length,0) as length,isnull(width,0) as width,PUR_TUHAO  " +
                      "FROM View_TBPC_PURCHASEPLAN_RVW  WHERE planno='" + TextBox_pid.Text.ToString() + "' AND  purstate>=3  and PUR_CSTATE='0'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                string shape = dt.Rows[0]["PUR_MASHAPE"].ToString();
                string marid = dt.Rows[0]["PUR_MARID"].ToString();
                double length = Convert.ToDouble(dt.Rows[0]["length"].ToString());
                double width = Convert.ToDouble(dt.Rows[0]["width"].ToString());
                string submarid = marid.Substring(0, 5).ToString();
               
            }

            DataView dv = null;
            if (rad_all.Checked)
            {
                dv = dt.DefaultView;
                dt = dv.ToTable();
            }
            else if (rad_wfg.Checked)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_STATE='3'";
                dt = dv.ToTable();
            }
            else if (rad_yfg.Checked)
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_STATE>='4'";
                dt = dv.ToTable();
            }
            else
            {
                dv = dt.DefaultView;
                dt = dv.ToTable();
            }
            if (DropDownList_TY.SelectedValue.ToString() != "00")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "PUR_MARID LIKE '" + DropDownList_TY.SelectedValue + "%'";
                dt = dv.ToTable();
            }
            tbpc_purshaseplanrealityRepeater.DataSource = dt;
            tbpc_purshaseplanrealityRepeater.DataBind();
            if (tbpc_purshaseplanrealityRepeater.Items.Count > 0)
            {
                NoDataPane.Visible = false;
            }
            else
            {
                NoDataPane.Visible = true;
            }
        }
        protected void rad_all_CheckedChanged(object sender, EventArgs e)//所有任务
        {
            repeaterdatabind();
        }
        protected void rad_wfg_CheckedChanged(object sender, EventArgs e)//未分工任务
        {
            repeaterdatabind();
        }
        protected void rad_yfg_CheckedChanged(object sender, EventArgs e)//已分工任务
        {
            repeaterdatabind();
        }
        protected void DropDownList_TY_SelectedIndexChanged(object sender, EventArgs e)
        {
            repeaterdatabind();
        }
        protected void Button_save_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            string state = "";
            SqlCommand sqlCmd = new SqlCommand();
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            sqlConn.Open();
            sqlCmd.Connection = sqlConn;
            foreach (RepeaterItem Reitem in tbpc_purshaseplanrealityRepeater.Items)
            {
                state = ((Label)Reitem.FindControl("PUR_STATE")).Text;
                if (state == "3")
                {
                    sqltext = "update TBPC_PURCHASEPLAN set PUR_CGMAN=@PUR_CGMANCODE  " +
                              "where PUR_PTCODE=@PUR_PTCODE and PUR_CSTATE='0'";
                    sqlCmd.CommandText = sqltext;
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddWithValue("@PUR_PTCODE", ((TextBox)Reitem.FindControl("PUR_PTCODE")).Text);
                    sqlCmd.Parameters.AddWithValue("@PUR_CGMANCODE", ((Label)Reitem.FindControl("PUR_CGMANCODE")).Text);
                    int rowsnum = sqlCmd.ExecuteNonQuery();
                }
            }
            DBCallCommon.closeConn(sqlConn);
            repeaterdatabind();
            ddl_caigouyuan.SelectedIndex = 0;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);

        }
        protected void finish_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            string state = "";
            string ptcode = "";
            string cgid = "";
            string cgnm = "";
            int i = 0;
            List<string> sqltextlist = new List<string>();
            foreach (RepeaterItem Reitem in tbpc_purshaseplanrealityRepeater.Items)
            {
                state = ((Label)Reitem.FindControl("PUR_STATE")).Text;
                ptcode = ((TextBox)Reitem.FindControl("PUR_PTCODE")).Text;
                cgid = ((Label)Reitem.FindControl("PUR_CGMANCODE")).Text;
                cgnm = ((Label)Reitem.FindControl("PUR_CGMANNAME")).Text;
                if (state == "3" && cgnm != "")
                {
                    i++;
                    sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_CGMAN='" + cgid +"',PUR_STATE='4',PUR_PTASMAN='" +
                              Session["UserID"].ToString() + "',PUR_PTASTIME='" +
                              DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "',Pue_Closetype=NULL  WHERE PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";
                    sqltextlist.Add(sqltext);
                }
            }
            if (i > 0)
            {
                DBCallCommon.ExecuteTrans(sqltextlist);
                Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_assign_list.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择采购员！');", true);
            }
        }
        //取消已分工
        protected void btn_fanshen_Click(object sender, EventArgs e)
        {
            int temp = canfanshen();
            string sqltext = "";
            string state = "";
            string ptcode = "";
            List<string> sqltextlist = new List<string>();
            if (temp == 0)
            {
                foreach (RepeaterItem Reitem in tbpc_purshaseplanrealityRepeater.Items)
                {
                    CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        if (cbx.Checked)
                        {
                            state = ((Label)Reitem.FindControl("PUR_STATE")).Text;
                            ptcode = ((TextBox)Reitem.FindControl("PUR_PTCODE")).Text;
                            if (state == "4")
                            {
                                sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_CGMAN='',PUR_STATE='3',PUR_PTASMAN='',PUR_PTASTIME=''  WHERE PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";
                                sqltextlist.Add(sqltext);
                                //DBCallCommon.ExeSqlText(sqltext);
                            }
                        }
                    }
                }
                DBCallCommon.ExecuteTrans(sqltextlist);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('取消分工成功！');", true);
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('分工人不是登录用户，你无权修改！');", true);
            }
            else if (temp == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择了未分工的数据，操作失败！');", true);
            }
            else if (temp == 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('任务已执行，操作失败！');", true);
            }
            repeaterdatabind();//绑定数据源
        }
        //判断能否取消
        private int canfanshen()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//分工人是否为登录用户
            int k = 0;//选择的数据中是否包含未分工数据
            int l = 0;//选择的数据是否包含已执行数据（询比价、代用）
            int state = 0;//状态
            string postid = "";
            string userid = Session["UserID"].ToString();
            foreach (RepeaterItem Reitem in tbpc_purshaseplanrealityRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        state = Convert.ToInt32(((Label)Reitem.FindControl("PUR_STATE")).Text);
                        postid = ((Label)Reitem.FindControl("PUR_PTASMAN")).Text;
                        if (postid != userid)
                        {
                            j++;
                            break;
                        }
                        else if (state <= 3)//未分工
                        {
                            k++;
                            break;
                        }
                        else if (state > 4)//询比价or代用
                        {
                            l++;
                            break;
                        }
                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (j > 0)
            {
                temp = 2;
            }
            else if (k > 0)//选择的数据中包含未分工数据
            {
                temp = 3;
            }
            else if (l > 0)//选择的数据中包含询比价or代用的数据
            {
                temp = 4;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_assign_list.aspx");
        }
        protected int isselected()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//选择的数据中是否包含已下推数据，或关闭
            int count = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplanrealityRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        if (((Label)Reitem.FindControl("PUR_STATE")).Text != "3")//已分工
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
            else if (j > 0)//选择的数据中是否包含不能询比价的数据
            {
                temp = 2;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }

        protected void selectall_CheckedChanged(object sender, EventArgs e)
        {
            if (selectall.Checked)
            {
                for (int i = 0; i < tbpc_purshaseplanrealityRepeater.Items.Count; i++)
                {
                    RepeaterItem Reitem = tbpc_purshaseplanrealityRepeater.Items[i];
                    CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = true;
                    }
                }
            }
            else
            {
                for (int i = 0; i < tbpc_purshaseplanrealityRepeater.Items.Count; i++)
                {
                    RepeaterItem Reitem = tbpc_purshaseplanrealityRepeater.Items[i];
                    CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = false;
                    }
                }
            }
        }
        protected void ddl_caigouyuan_SelectedIndexChanged(object sender, EventArgs e)
        {
            int temp = isselected();
            if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据中包含已分工的记录，请重新选择');", true);
            }
            else
            {
                if (ddl_caigouyuan.SelectedValue.ToString() == "00")//没选采购员
                {
                    for (int i = 0; i < tbpc_purshaseplanrealityRepeater.Items.Count; i++)
                    {
                        RepeaterItem Reitem = tbpc_purshaseplanrealityRepeater.Items[i];
                        CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                        if (cbx != null)//存在行
                        {
                            if (cbx.Checked)
                            {
                                ((Label)Reitem.FindControl("PUR_CGMANNAME")).Text = "";
                                ((Label)Reitem.FindControl("PUR_CGMANCODE")).Text = "";
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < tbpc_purshaseplanrealityRepeater.Items.Count; i++)
                    {
                        RepeaterItem Reitem = tbpc_purshaseplanrealityRepeater.Items[i];
                        CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                        if (cbx != null)//存在行
                        {
                            if (cbx.Checked)
                            {
                                ((Label)Reitem.FindControl("PUR_CGMANNAME")).Text = ddl_caigouyuan.SelectedItem.Text.ToString();
                                ((Label)Reitem.FindControl("PUR_CGMANCODE")).Text = ddl_caigouyuan.SelectedValue.ToString();
                                cbx.Checked = false;
                                selectall.Checked = false;
                            }
                        }
                    }
                }
            }
        }
        public string get_fg_state(string i)
        {
            string statestr = "";
            if (Convert.ToInt32(i) >= 4)
            {
                statestr = "是";
            }
            else
            {
                statestr = "否";
            }
            return statestr;
        }

        protected void tbpc_purshaseplanrealityRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {
               
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox tbptcode = (TextBox)e.Item.FindControl("PUR_PTCODE");
                Label lbnum = (Label)e.Item.FindControl("PUR_RPNUM");
                string sqlgetzyinfo = "";
                if (tbptcode.Text.Trim().Contains("&") && ((tbptcode.Text.Trim().LastIndexOf("&") + 5) > tbptcode.Text.Trim().Length))
                {
                    sqlgetzyinfo = "select sum(PUR_NUM) as tolpur_num from TBPC_PURCHASEPLAN where PUR_CSTATE='1' and PUR_PTCODE like '" + tbptcode.Text.Trim().Substring(0, tbptcode.Text.Trim().LastIndexOf("&")) + "%'";
                }
                else
                {
                    sqlgetzyinfo = "select sum(PUR_NUM) as tolpur_num from TBPC_PURCHASEPLAN where PUR_CSTATE='1' and PUR_PTCODE like '" + tbptcode.Text.Trim() + "%'";
                }
                DataTable dtgetzyinfo = DBCallCommon.GetDTUsingSqlText(sqlgetzyinfo);
                if (CommonFun.ComTryDecimal(dtgetzyinfo.Rows[0]["tolpur_num"].ToString().Trim()) > 0)
                {
                    lbnum.BackColor = System.Drawing.Color.Yellow;
                    lbnum.ToolTip = "该项目下占用数量：" + dtgetzyinfo.Rows[0]["tolpur_num"].ToString().Trim();
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
            foreach (RepeaterItem Reitem in tbpc_purshaseplanrealityRepeater.Items)
            {
                j++;
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;
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
                foreach (RepeaterItem Reitem in tbpc_purshaseplanrealityRepeater.Items)
                {
                    k++;
                    CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;
                    if (k >= start && k <= finish)
                    {
                        cbx.Checked = true;
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
            foreach (RepeaterItem Reitem in tbpc_purshaseplanrealityRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;
                cbx.Checked = false;
            }
        }
    }
}
