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

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Xie_Mana_List : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {

                GetSele();
                InitVar();
                bindRepeater();
            }
          
            if (wxstatus.SelectedValue.ToString() == "2")
            {
                btn_cmpprc.Visible = false;//已执行的订单不能再次询比价
            }
            else if (wxstatus.SelectedValue.ToString() == "4")
            {
                btn_cmpprc.Visible = true;
            }
            //2017.3修改
            string sql_btn_cmpprc = "select d.DEP_NAME as DEP_POSITION from TBDS_STAFFINFO as a left join TBDS_DEPINFO as b on a.ST_DEPID=b.DEP_CODE left join TBDS_DEPINFO as d on a.ST_POSITION = d.DEP_CODE where ST_ID='" + Session["UserID"].ToString() + "'";
            DataTable dt_btn_cmpprc = DBCallCommon.GetDTUsingSqlText(sql_btn_cmpprc);
            if (dt_btn_cmpprc.Rows.Count > 0)
            {
                if (dt_btn_cmpprc.Rows[0]["DEP_POSITION"].ToString() == "外协管理员")
                {
                    btn_cmpprc.Enabled = true;
                }
                else
                {
                    btn_cmpprc.Enabled = false;

                }
            }
            //if (Session["UserID"].ToString() == "184")
            //{
            //    btn_cmpprc.Enabled = true;
            //}
            else
            {
                btn_cmpprc.Enabled = false;
            
            }
            CheckUser(ControlFinder);
        }
        protected void wxstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;

                InitVar();
                this.bindRepeater();

        }
        protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
                InitVar();
                this.bindRepeater();
        }
        protected int isselected()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//选择的数据中是否包含有已询价的数据
            //int count = 0;
            foreach (RepeaterItem Reitem in tbpc_otherpurbill_list_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        string ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_PID")).Text;
                        string wxtype = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_wxtype")).Text;
                        string msid = ((Label)Reitem.FindControl("MS_ID")).Text;
                        string mscode = ((Label)Reitem.FindControl("MS_CODE")).Text;
                        string sql = "select PIC_PTCODE from TBMP_IQRCMPPRICE where PIC_PTCODE='" + ptcode + "'and PIC_WXTYPE='" + wxtype + "'and PIC_TASKID='"+msid+"'and PIC_CODE='"+mscode+"'";
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        if (dt.Rows.Count > 0)
                        {
                            j++;
                        }
                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (j > 0)//选择的数据中是否包含有已询价的数据
            {
               temp = 2;
            }

            else
            {
                temp = 0;//可以下推
            }
            return temp;
        }
        /// <summary>
        /// 连选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_Click(object sender, EventArgs e)
        {
            CheckBoxSelectDefine(tbpc_otherpurbill_list_Repeater, "CKBOX_SELECT");
        }
        /// <summary>
        /// CheckBox连选(此函数勿动)
        /// </summary>
        /// <param name="smartgridview"></param>
        /// <param name="ckbname"></param>
        public void CheckBoxSelectDefine(Repeater tbpc_otherpurbill_list_Repeater, string ckbname)
        {
            int startindex = -1;
            int endindex = -1;
            for (int i = 0; i < tbpc_otherpurbill_list_Repeater.Items.Count; i++)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)tbpc_otherpurbill_list_Repeater.Items[i].FindControl(ckbname);
                if (cbx.Checked)
                {
                    startindex = i;
                    break;
                }
            }
            for (int j = tbpc_otherpurbill_list_Repeater.Items.Count - 1; j > -1; j--)
            {
                System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)tbpc_otherpurbill_list_Repeater.Items[j].FindControl(ckbname);
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
                    System.Web.UI.WebControls.CheckBox cbx = (System.Web.UI.WebControls.CheckBox)tbpc_otherpurbill_list_Repeater.Items[k].FindControl(ckbname);
                    cbx.Checked = true;
                }
            }
        }
        protected void btn_cmpprc_Click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            int temp = isselected();
            if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');window.location.reload(); ", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择的数据中包含已询比价的记录，请重新选择!');window.location.reload(); ", true);
            }
            else
            {
                string sheetcode = encodesheetno();//生成比价单号
                string sqltext1;
                string sqltext2;
                string sqltext3;
                string sqltext4;
                string ptcode = "";
                double num = 0;
                string type = "";
                string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string manid = Session["UserID"].ToString();
                //sqltext1 = "INSERT INTO TBMP_IQRCMPPRCRVW(ICL_SHEETNO,ICL_IQRDATE," +
                //         "ICL_REVIEWA,ICL_CSTATE,ICL_STATE,ICL_WXTYPE) VALUES('" + sheetcode + "','" + time + "','" + manid + "',0,0,'" + type + "')";
                //sqltextlist.Add(sqltext1);
                foreach (RepeaterItem Reitem in tbpc_otherpurbill_list_Repeater.Items)
                {
                    if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
                    {
                        type = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_wxtype")).Text;
                        string pjid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_PJID")).Text;
                        string engid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_ENGID")).Text;
                        string engname = ((Label)Reitem.FindControl("MS_ENGNAME")).Text;
                        string note = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_NOTE")).Text;
                        string mname = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_NAME")).Text;
                        string guige = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_GUIGE")).Text;
                        string caizhi = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_CAIZHI")).Text;
                        ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_PID")).Text;
                        string tuhao = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_TUHAO")).Text;
                        string zongxu = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_ZONGXU")).Text;
                        string mscode = ((Label)Reitem.FindControl("MS_CODE")).Text;
                        num = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_NUM")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_NUM")).Text);
                        //fznum = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_RPFZNUM")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_RPFZNUM")).Text);
                        //zdjweight = fznum;
                        double length = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_LEN")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_LEN")).Text);
                        double width = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_WIDTH")).Text == "" ? "0" : ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_WIDTH")).Text);
                        //string keycoms = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_KEYCOMS")).Text;
                        string shape = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_MASHAPE")).Text;
                        string taskid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_ID")).Text;
                        sqltext2 = "INSERT INTO TBMP_IQRCMPPRICE(PIC_CODE,PIC_SHEETNO,PIC_ZONGXU,PIC_TASKID,PIC_WXTYPE,PIC_PJID,PIC_ENGID,PIC_ENGNAME," +
                                   "PIC_QUANTITY,PIC_LENGTH,PIC_WIDTH,PIC_ZXNUM," +
                                   "PIC_PTCODE,PIC_KEYCOMS,PIC_TUHAO,PIC_MASHAPE,PIC_NOTE,PIC_MNAME,PIC_GUIGE,PIC_CAIZHI)  " +
                                   "VALUES('"+mscode+"','" + sheetcode + "','"+zongxu+"','" + taskid + "','" + type + "','" + pjid + "','" + engid + "','"+engname+"'," +
                                   "'" + num + "','" + length + "','" + width + "','" + num + "', " +
                                   "'" + ptcode + "','','" + tuhao + "','" + shape + "','" + note + "','" + mname + "','" + guige + "','" + caizhi + "')";
                        sqltextlist.Add(sqltext2);
                        //sqltext3 = "UPDATE TBMP_TASKDQO SET MS_scwaixie='2' WHERE " +
                        //           "MS_ID='" +((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_ID")).Text + "' ";//生成比价单
                        //sqltextlist.Add(sqltext3);
                        sqltext4 = "UPDATE TBPM_WXDetail SET MS_scwaixie='2' WHERE " +
                                   "MS_CODE='" +((System.Web.UI.WebControls.Label)Reitem.FindControl("MS_CODE")).Text + "' ";//生成比价单
                        sqltextlist.Add(sqltext4);

                    }
                }
                sqltext1 = "INSERT INTO TBMP_IQRCMPPRCRVW(ICL_SHEETNO,ICL_IQRDATE," +
                           "ICL_REVIEWA,ICL_CSTATE,ICL_STATE,ICL_WXTYPE) VALUES('" + sheetcode + "','" + time + "','" + manid + "',0,0,'" + type + "')";
                sqltextlist.Add(sqltext1);
                DBCallCommon.ExecuteTrans(sqltextlist);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('PM_Xie_check_detail.aspx?sheetno=" + sheetcode + "');", true);
            }
        }
        protected string encodesheetno()//比价单编号8位
        {
            string sheetcode = "";
            string sqltext = "SELECT  max(ICL_SHEETNO) as ICL_SHEETNO FROM TBMP_IQRCMPPRCRVW";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows[0]["ICL_SHEETNO"].ToString() != "" && dt.Rows[0]["ICL_SHEETNO"].ToString() != null)
            {
                sheetcode = dt.Rows[0]["ICL_SHEETNO"].ToString();//单号
                sheetcode = Convert.ToString(Convert.ToInt32(sheetcode) + 1);
                sheetcode = sheetcode.PadLeft(8, '0');
            }
            else
            {
                sheetcode = "00000001";
            }
            return sheetcode;
        }
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
           string sqltext="";
            
            if (wxstatus.SelectedValue.ToString() == "4")
            {
                sqltext += "and MS_scwaixie='4'";
            }
            else if (wxstatus.SelectedValue.ToString() == "2")
            {
                sqltext += "and MS_scwaixie='2'";//1外协提交中 2 已比价 3外协驳回 4 未比价
            }
            else if (wxstatus.SelectedValue.ToString() == "5")
            {
                sqltext += "and MS_scwaixie='5'";
            }
            if (ddltype.SelectedIndex.ToString() == "1")
            {
                sqltext += "and MS_wxtype='工序外协'";
            }
            else
                if (ddltype.SelectedIndex.ToString() == "2")
                {
                    sqltext += "and MS_wxtype='成品外协'";
                }
            sqltext += GetStrCondition();
            pager.TableName = "VIEW_TM_WXDetail";
            pager.PrimaryKey = "MS_ID";
            pager.ShowFields = "*";
            pager.OrderField = "MS_PID desc ,MS_WSID";
            pager.StrWhere = "MS_PID in (select distinct PM_PID from  dbo.TBPM_SCWXRVW where PM_SPZT=3)" + sqltext;
            //pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 1;
            pager.PageSize = 100;
        }
        private void Pager_PageChanged(int pageNumber)
        {
            InitPager();
            bindRepeater();
        }
        /// <summary>
        /// 绑定tbpc_otherpurbill_list_Repeater数据
        /// </summary> 
        private void bindRepeater()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, tbpc_otherpurbill_list_Repeater, UCPaging1, NoDataPanel);
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
        protected void reset_Click(object sender, EventArgs e)
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
            UCPaging1.CurrentPage = 1;
            InitVar();
            bindRepeater();

        }
        private string GetStrCondition()
        {
            string strWhere = "";
            if (screen1.SelectedValue != "0" || screen2.SelectedValue != "0" || screen3.SelectedValue != "0" || screen4.SelectedValue != "0")
            {
                if (SelectStr(screen1, ddlRelation1, Txt1.Text, "") != "")
                {
                    strWhere += "and (" + SelectStr(screen1, ddlRelation1, Txt1.Text, "");
                }
                else
                {
                    strWhere += "and (1=1 ";
                }
                strWhere += SelectStr(screen2, ddlRelation2, Txt2.Text, ddlLogic1.SelectedValue);
                strWhere += SelectStr(screen3, ddlRelation3, Txt3.Text, ddlLogic2.SelectedValue);
                strWhere += SelectStr(screen4, ddlRelation4, Txt4.Text, ddlLogic3.SelectedValue);
                strWhere += SelectStr(screen5, ddlRelation5, Txt5.Text, ddlLogic4.SelectedValue);
                strWhere += SelectStr(screen6, ddlRelation6, Txt6.Text, ddlLogic5.SelectedValue);
                strWhere += ")";
            }
            return  strWhere;
        }
        private string SelectStr(DropDownList ddl, DropDownList ddl1, string txt, string logic) //选择条件拼接字符串
        {
            string sqlstr = string.Empty;
            if (ddl.SelectedValue != "0")
            {
                switch (ddl1.SelectedValue)
                {
                    case "0":
                        sqlstr = string.Format("{0} {1} like '%{2}%' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "1":
                        sqlstr = string.Format("{0} {1} not like '%{2}%' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "2":
                        sqlstr = string.Format("{0} {1}='{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "3":
                        sqlstr = string.Format("{0} {1}!='{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "4":
                        sqlstr = string.Format("{0} {1}>'{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "5":
                        sqlstr = string.Format("{0} {1}>='{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "6":
                        sqlstr = string.Format("{0} {1}<'{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                    case "7":
                        sqlstr = string.Format("{0} {1}<='{2}' ", logic, ddl.SelectedValue, txt);
                        break;
                }
            }
            return sqlstr;
        }
        protected void search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetStrCondition();
            InitVar();
            bindRepeater();

        }
        private void GetSele()
        {
            foreach (Control item in select.Controls[1].Controls[0].Controls)
            {
                if (item is DropDownList)
                {
                    if (item.ID.Contains("screen"))
                    {
                        ((DropDownList)item).DataSource = bindItemList();
                        ((DropDownList)item).DataTextField = "value";
                        ((DropDownList)item).DataValueField = "key";
                        ((DropDownList)item).DataBind();
                        ((DropDownList)item).SelectedValue = "0";
                    }
                }
            }
        }
        private Dictionary<string, string> bindItemList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();
            ItemList.Add("0", "请选择");
            ItemList.Add("MS_PID", "批号");
            ItemList.Add("MS_ENGID", "任务号");
            ItemList.Add("MS_ENGNAME", "设备名称");
            ItemList.Add("MS_TUHAO", "图号");
            ItemList.Add("MS_ZONGXU", "总序");
            ItemList.Add("MS_NAME", "名称");
            ItemList.Add("MS_WSID", "外协单号");
            ItemList.Add("MS_PROCESS", "加工工序");
            // ItemList.Add("zdtime", "时间");
            return ItemList;
        }
        /// <summary>
        /// 变更取消比价
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_bgcancel_Click(object sender, EventArgs e)
        {
            int i = 0;
            List<string> list = new List<string>();
            string sqltext;
            foreach (RepeaterItem reitem in tbpc_otherpurbill_list_Repeater.Items)
            {
                CheckBox cb = (CheckBox)reitem.FindControl("CKBOX_SELECT");
                if (cb.Checked)
                {
                    i++;
                    string mscode = ((Label)reitem.FindControl("MS_CODE")).Text;
                    sqltext = "update   TBPM_WXDetail  set MS_scwaixie='5' where MS_CODE='" + mscode + "'";
                    list.Add(sqltext);
                }
            }
                if (i > 0)
                {
                    DBCallCommon.ExecuteTrans(list);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！共有"+i+"个产品取消比价！');window.location.reload(); ", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
                }
            
            
             
        }
    }
}
