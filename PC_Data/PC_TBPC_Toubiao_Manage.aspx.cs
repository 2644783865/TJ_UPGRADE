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
using System.Data.SqlClient;
using System.Drawing;
using System.Collections.Generic;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Toubiao_Manage : BasicPage
    {
        PagerQueryParamGroupBy pager = new PagerQueryParamGroupBy();

        ////页数
        public int ObjPageSize
        {
            get
            {
                if (ViewState["ObjPageSize"] == null)
                {
                    //默认是升序
                    ViewState["ObjPageSize"] = 120;
                }

                return Convert.ToInt32(ViewState["ObjPageSize"]);
            }
            set
            {
                ViewState["ObjPageSize"] = value;
            }
        }
        public string gloabstate//状态、下订单7
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
        public string NUM1
        {
            get
            {
                object str = ViewState["NUM1"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["NUM1"] = value;
            }
        }
        public string NUM2
        {
            get
            {
                object str = ViewState["NUM2"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["NUM2"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                hid_filter.Text = "View_TBPC_TOUBIAODETAIL" + "/" + Session["UserID"].ToString();
                initpager1();
                getArticle(true);
            }
            CheckUser(ControlFinder);
        }

        private void initpager1()
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
                btn_back.Visible = true;
                rad_stxdd.Checked = true;
                rad_mypart.Checked = false;
                rad_all.Checked = true;
                rad_stwzx.Checked = false;
            }
            else
            {
                gloabptc = "";
            }
            if (Request.QueryString["num1"] != null)
            {
                NUM1 = Request.QueryString["num1"].ToString();
            }
            else
            {
                NUM1 = "";
            }
            if (Request.QueryString["num2"] != null)
            {
                NUM2 = Request.QueryString["num2"].ToString();
            }
            else
            {
                NUM2 = "";
            }
            if (NUM1 == "1")
            {
                rad_all.Checked = true;
                rad_mypart.Checked = false;
            }
            else if (NUM1 == "2")
            {
                rad_all.Checked = false;
                rad_mypart.Checked = true;
            }
            if (NUM2 == "1")
            {
                rad_stall.Checked = true; rad_stwzx.Checked = false; rad_stxdd.Checked = false;
            }
            else if (NUM2 == "2")
            {
                rad_stall.Checked = false; rad_stwzx.Checked = true; rad_stxdd.Checked = false;
            }
            else if (NUM2 == "3")
            {
                rad_stall.Checked = false; rad_stwzx.Checked = false; rad_stxdd.Checked = true;
            }
        }
        private void BindPage(string tableName, string PrimaryKey, string ShowFields, string OrderField, string GroupField, int OrderType, string StrWhere, int PageSize, bool isFristPage)
        {

            InitVar(tableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFristPage);

        }


        private void InitVar(string tableName, string PrimaryKey, string ShowFields, string OrderField, string GroupField, int OrderType, string StrWhere, int PageSize, bool isFristPage)
        {

            InitPager(tableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize);//初始化页面

            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数

            if (isFristPage)
            {
                UCPaging1.CurrentPage = 1;
            }

            bindData();
        }

        //初始化分页信息
        private void InitPager(string tableName, string PrimaryKey, string ShowFields, string OrderField, string GroupField, int OrderType, string StrWhere, int PageSize)
        {
            pager.TableName = tableName;

            pager.PrimaryKey = PrimaryKey;

            pager.ShowFields = ShowFields;

            if (string.IsNullOrEmpty(GroupField))

                pager.OrderField = OrderField;
            else

                pager.OrderField = GroupField;

            pager.GroupField = GroupField;

            pager.OrderType = OrderType;

            pager.StrWhere = StrWhere;

            pager.PageSize = PageSize;
        }

        protected void bindData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager);
            CommonFun.Paging(dt, tbpc_purshaseplanassignRepeater, UCPaging1, NoDataPane);
            if (NoDataPane.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            //clearData();
        }

        void Pager_PageChanged(int pageNumber)
        {
            getArticle(false);
        }
        protected void btn_search_click(object sender, EventArgs e)
        {
            getArticle(true);
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

        private void getArticle(bool isFirstPage)      //取得Article数据
        {
            CreateDataSource();

            string TableName = "View_TBPC_TOUBIAODETAIL";

            string PrimaryKey = "";

            string ShowFields = "pur_id,planno AS PUR_PCODE,PUR_TUHAO," +
                             "ptcode as PUR_PTCODE,marid as PUR_MARID,marnm as PUR_MARNAME," +
                             "margg as PUR_MARNORM,marcz as PUR_MARTERIAL,margb as PUR_GUOBIAO,marunit as PUR_NUNIT," +
                             "marfzunit as FZUNIT,length as PUR_LENGTH,width as PUR_WIDTH,rpnum as PUR_RPNUM," +
                             "rpfznum as PUR_RPFZNUM,jstimerq as PUR_TIMEQ,fgrnm AS PUR_PTASMAN,fgtime as PUR_PTASTIME, " +
                            "cgrid as PUR_CGMAN,cgrnm as PUR_CGMANNM,keycoms as PUR_KEYCOMS,purstate as PUR_STATE," +
                            "purnote as PUR_NOTE,picno as PIC_SHEETNO,CONVERT(varchar(12) , irqdata, 102 ) as ICL_IQRDATE,SUBSTRING(date,1,10) as STDATE,SUBSTRING(fidate,1,10) as FIDATE," +
                            "piczdid as ICL_REVIEWA,piczdnm as ICL_REVIEWANM,marzxnum as ZXNUM,marzxfznum as ZXFZNUM," +
                            "orderno as PO_SHEETNO,IB_SUPPLY as PUR_SUPPLY,CS_NAME as PUR_SUPPLYNAME,price as PUR_PRICE,taxrate as PUR_TAXRATE,IB_STATE as state,PUR_MASHAPE ";

            //数据库中的主键
            string OrderField = "planno DESC,ptcode";

            string GroupField = "";

            int OrderType = 0;
            /**/
            string StrWhere = ViewState["sqlwhere"].ToString();

            int PageSize = ObjPageSize;

            BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);

        }

        public void CreateDataSource()
        {
            string sqlwhere = "";
            string sqltext = "";
            string tableuser = hid_filter.Text;
            string filter = "";
            sqltext = "SELECT tableuser, filter FROM TBPC_FILTER_INFO where tableuser='" + tableuser + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                filter = dr[1].ToString();
            }
            dr.Close();

            sqlwhere = "PUR_CSTATE='0'";

            if (filter != "")
            {
                sqlwhere = sqlwhere + " and " + filter + "";
            }
            if (gloabptc != "")
            {
                sqlwhere = sqlwhere + " and ptcode='" + gloabptc + "'";
            }
            if (rad_all.Checked)
            {
                sqlwhere = sqlwhere + " and purstate>='4'";
            }
            else if (rad_mypart.Checked)
            {
                sqlwhere = sqlwhere + " and purstate>='4' and cgrid='" + Session["UserID"].ToString() + "'";
            }
            if (rad_stwzx.Checked)
            {
                sqlwhere = sqlwhere + " and purstate='4'";
            }
            if (rad_stxdd.Checked)
            {
                sqlwhere = sqlwhere + " and purstate>='7'";
            }
            if (Tb_PJNAME.Text != "")
            {
                sqlwhere = sqlwhere + " and pjnm like '%" + Tb_PJNAME.Text.Trim() + "%'";
            }
            if (Tb_ENGNAME.Text != "")
            {
                sqlwhere = sqlwhere + " and engnm like '%" + Tb_ENGNAME.Text.Trim() + "%'";
            }
            if (tb_riqif.Text != "")
            {
                sqlwhere = sqlwhere + " and irqdata>'" + tb_riqif.Text + "'";
            }
            if (tb_riqit.Text != "")
            {
                string enddate = tb_riqit.Text.ToString() == "" ? "2100-01-01" : tb_riqit.Text.ToString();
                enddate = enddate + " 23:59:59";
                sqlwhere = sqlwhere + " and irqdata<'" + enddate + "'";
            }
            if (Tb_pcode.Text != "")
            {
                sqlwhere = sqlwhere + " and planno like '%" + Tb_pcode.Text.Trim() + "%'";
            }
            if (drp_stu.SelectedValue.ToString() != "-请选择-")
            {
                sqlwhere = sqlwhere + " and cgrid='" + drp_stu.SelectedValue.ToString() + "'";
            }
            ViewState["sqlwhere"] = sqlwhere;
        }

       
       
        //全选
        protected void selectall_CheckedChanged(object sender, EventArgs e)
        {
            if (selectall.Checked)
            {
                foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
                {
                    CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                    if (cbx != null)//存在行
                    {
                        cbx.Checked = true;
                    }
                }
            }
            else
            {
                foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
                {
                    CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
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
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
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
                foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
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
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;
                cbx.Checked = false;
            }
        }

        protected void rad_all_CheckedChanged(object sender, EventArgs e)//所有任务
        {
            getArticle(true);
        }
        protected void rad_mypart_CheckedChanged(object sender, EventArgs e)//我的任务
        {
            getArticle(true);
        }
        protected void rad_stall_CheckedChanged(object sender, EventArgs e)//全部
        {
            getArticle(true);
        }
        protected void rad_stwzx_CheckedChanged(object sender, EventArgs e)//未执行
        {
            getArticle(true);
        }
        protected void rad_stxdd_CheckedChanged(object sender, EventArgs e)//下订单
        {
            getArticle(true);
        }

        protected int isselected()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//选择的数据中是否包含有已询价的数据
            int k = 0;
            //int count = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    i++;
                    string ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text;
                    string sql = "select PO_CODE from TBPC_PURORDERDETAIL where PO_PTCODE='" + ptcode + "' and PO_CSTATE!='2'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        k++;
                    }
                    else if (Convert.ToInt32(((Label)Reitem.FindControl("PUR_STATE")).Text) >= 6)
                    {
                        j++;
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
            else if (k > 0)
            {
                temp = 3;
            }
            else
            {
                temp = 0;//可以下推
            }
            return temp;
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_assign.aspx");
        }
        private bool is_sameeng()//判断是否同一批同一工程
        {
            string temppcode = "";
            bool temp = true;
            int i = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        if (i == 1)
                        {
                            temppcode = ((Label)Reitem.FindControl("PUR_PCODE")).Text.ToString();
                        }
                        else
                        {
                            if (temppcode != ((Label)Reitem.FindControl("PUR_PCODE")).Text.ToString())
                            {
                                temp = false;
                                break;
                            }
                        }
                    }
                }
            }
            return temp;
        }

        private bool is_samesupply()//判断是否同一供应商
        {
            string temppcode = "";
            bool temp = true;
            int i = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        if (i == 1)
                        {
                            temppcode = ((Label)Reitem.FindControl("PUR_SUPPLY")).Text.ToString();
                        }
                        else
                        {
                            if (temppcode != ((Label)Reitem.FindControl("PUR_SUPPLY")).Text.ToString())
                            {
                                temp = false;
                                break;
                            }
                        }
                    }
                }
            }
            return temp;
        }

      
        protected void tbpc_purshaseplanassignRepeaterbind(object sender, RepeaterItemEventArgs e)
        {
            string state = "";
            string ddsheetno = "";
            string ptcode = "";

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
              
                //订单处理
                #region
                state = ((Label)e.Item.FindControl("PUR_STATE")).Text;
                ptcode = ((Label)e.Item.FindControl("PUR_PTCODE")).Text;
                if (Convert.ToInt32(state) >= 7)
                {
                    ddsheetno = ((Label)e.Item.FindControl("PO_SHEETNO")).Text;
                    ((Label)e.Item.FindControl("PUR_DD")).ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)e.Item.FindControl("Hypdd")).NavigateUrl = "PC_TBPC_PurOrder.aspx?orderno=" + ddsheetno + "&ptc=" + ptcode + "";
                }
                #endregion

                double num = Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_RPNUM")).Text);
                if (num == 0)
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#FFFF00";
                }
            }
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

        //生成订单号
        private string encodeorderno()
        {
            string pi_id = "";
            string tag_pi_id = "ZBPORD";
            string end_pi_id = "";
            string sqltext = "SELECT TOP 1 PO_CODE FROM TBPC_PURORDERTOTAL WHERE PO_CODE LIKE '" + tag_pi_id + "%' ORDER BY PO_CODE DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                end_pi_id = Convert.ToString(Convert.ToInt32(dt.Rows[0]["PO_CODE"].ToString().Substring((dt.Rows[0]["PO_CODE"].ToString().Length - 8), 8)) + 1);
                end_pi_id = end_pi_id.PadLeft(8, '0');
            }
            else
            {
                end_pi_id = "00000001";
            }
            pi_id = tag_pi_id + end_pi_id;
            return pi_id;
        }

        //生成投标订单
        protected void btn_toubiaoM_Click(object sender, EventArgs e)
        {
            int j = 0;
            double rpnum = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    rpnum = Convert.ToDouble(((Label)Reitem.FindControl("PUR_RPNUM")).Text == "" ? "0" : ((Label)Reitem.FindControl("PUR_RPNUM")).Text);
                    if (rpnum == 0)
                    {
                        j++;
                    }
                }
            }
            if (j > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('包含有采购数量为0的数据，不能下推订单！');", true);
            }
            else 
            {
                int temp = isselected();
                if (temp == 0)//是否选择数据
                {
                    if (is_samesupply())//是否同一供应商
                    {
                        string sqltext = "";
                        List<string> sqltextlist = new List<string>();
                        string planpcode = "";
                        string ptcode = "";
                        string tuhao = "";
                        string marid = "";
                        double num = 0;
                        double fznum = 0;
                        double length = 0;
                        double width = 0;
                        double price = 0;
                        double taxrate = 17;
                        string note = "";
                        string supply = "";
                        string shape = "";
                        string pjid = "";
                        string engid = "";
                        string mpcode = encodeorderno();
                        //int i = 0;
                        //int j = 0;
                        foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
                        {
                            CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                            if (cbx.Checked)
                            {
                                pjid ="";
                                engid = "";
                                planpcode = ((Label)Reitem.FindControl("PUR_PCODE")).Text;
                                ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text;
                                marid = ((Label)Reitem.FindControl("PUR_MARID")).Text;
                                tuhao = ((Label)Reitem.FindControl("PUR_TUHAO")).Text;
                                num = Convert.ToDouble(((Label)Reitem.FindControl("PUR_RPNUM")).Text == "" ? "0" : ((Label)Reitem.FindControl("PUR_RPNUM")).Text);
                                fznum = Convert.ToDouble(((Label)Reitem.FindControl("PUR_RPFZNUM")).Text == "" ? "0" : ((Label)Reitem.FindControl("PUR_RPFZNUM")).Text);
                                length = Convert.ToDouble(((Label)Reitem.FindControl("PUR_LENGTH")).Text);
                                width = Convert.ToDouble(((Label)Reitem.FindControl("PUR_WIDTH")).Text);
                                price = Convert.ToDouble(((Label)Reitem.FindControl("PUR_PRICE")).Text);
                                taxrate = Convert.ToDouble(((Label)Reitem.FindControl("PUR_TAXRATE")).Text);
                                supply = ((Label)Reitem.FindControl("PUR_SUPPLY")).Text;
                                note = ((Label)Reitem.FindControl("PUR_NOTE")).Text;
                                shape = ((Label)Reitem.FindControl("PUR_MASHAPE")).Text;
                                note = note + " " + shape;

                                sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='7' WHERE PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";//计划状态改为7为投标订单
                                sqltextlist.Add(sqltext);

                                sqltext = "INSERT INTO TBPC_PURORDERDETAIL (PO_CODE,PO_PCODE, PO_MARID, PO_LENGTH, PO_WIDTH, PO_QUANTITY, PO_FZNUM," +
                                          "PO_ZXNUM, PO_ZXFZNUM,PO_CTAXUPRICE,PO_TAXRATE, PO_PTCODE,PO_MASHAPE,PO_TUHAO,PO_NOTE,PO_PJID,PO_ENGID)" +
                                          "VALUES('" + mpcode + "','" + ptcode + "','" + marid + "','" + length + "','" + width + "'," +
                                          "'" + num + "','" + fznum + "','" + num + "','" + fznum + "','" + price + "','" + taxrate + "','" + ptcode + "','" + shape + "','" + tuhao + "','" + note + "','" + pjid + "','" + engid + "')";
                                sqltextlist.Add(sqltext);
                            }
                        }
                        sqltext = "INSERT INTO TBPC_PURORDERTOTAL(PO_CODE,PO_SUPPLIERID,PO_ZDDATE,PO_ZDID,PO_DEPID)  " +
                                  "VALUES('" + mpcode + "','" + supply + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "','" +
                                   Session["UserID"].ToString() + "','" + Session["UserDeptID"].ToString() + "')";
                        sqltextlist.Add(sqltext);
                        DBCallCommon.ExecuteTrans(sqltextlist);
                        //DBCallCommon.ExeSqlText(sqltext);

                        //2013年4月8日 16:00:55  
                        //Response.Redirect("~/PC_Data/TBPC_Purorderdetail_xiugai.aspx?orderno=" + mpcode + "");//转到订单管理页面
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "ddopen('" + mpcode + "');", true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择同一个供应商下的物料，本次操作无效！');", true);
                    }
                }
                else if (temp == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
                }
                else if (temp >= 2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择了已下推关联的数据,本次操作无效！');", true);
                }
            }
            
        }

        protected void Tb_PJNAME_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (Tb_PJNAME.Text.ToString().Contains("|"))
            {
                Cname = Tb_PJNAME.Text.Substring(0, Tb_PJNAME.Text.ToString().IndexOf("|"));
                Tb_PJNAME.Text = Cname.Trim();
            }
            else if (Tb_PJNAME.Text == "")
            {

            }
        }
        protected void Tb_ENGNAME_Textchanged(object sender, EventArgs e)
        {
            string Cname = "";
            if (Tb_ENGNAME.Text.ToString().Contains("|"))
            {
                Cname = Tb_ENGNAME.Text.Substring(0, Tb_ENGNAME.Text.ToString().IndexOf("|"));
                Tb_ENGNAME.Text = Cname.Trim();
            }
            else if (Tb_ENGNAME.Text == "")
            {

            }
        }

        protected void btn_add_Click(object sender, EventArgs e)//追加订单
        {
            int j = 0;
            double num = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
                {
                    num = Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_RPNUM")).Text);
                    if (num == 0)
                    {
                        j++;
                    }
                }
            }
            if (j > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('包含数量为0的数据，不能追加订单！');", true);
            }
            else
            {
                int temp = isselected();
                string ptcode_rcode = "";
                string ptcode = "";
                if (temp == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
                }
                else if (temp == 2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择的数据包含未审核的记录,本次操作无效！');", true);
                }
                else if (temp == 3)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据包含有不同的供应商的记录,本次操作无效！');", true);
                }
                else if (temp == 4)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据包含已下订单的记录,本次操作无效！');", true);
                }
                else if (temp == 5)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择的数据包含已关闭的记录,本次操作无效！');", true);
                }
                else
                {
                    foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
                    {
                        if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
                        {
                            ptcode = ptcode + ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_lbpurid")).Text + ",";
                        }
                    }
                    ptcode_rcode = ptcode + Session["UserID"].ToString();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "mowinopen('" + ptcode_rcode + "');", true);
                }
            }
        }


    }
}
