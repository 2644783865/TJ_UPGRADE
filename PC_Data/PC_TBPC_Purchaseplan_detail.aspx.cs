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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchaseplan_detail : System.Web.UI.Page
    {
        string sf = "";
        PagerQueryParamGroupBy pager = new PagerQueryParamGroupBy();
        ////页数
        public int ObjPageSize
        {
            get
            {
                if (ViewState["ObjPageSize"] == null)
                {
                    //默认是升序
                    ViewState["ObjPageSize"] = 50;
                }

                return Convert.ToInt32(ViewState["ObjPageSize"]);
            }
            set
            {
                ViewState["ObjPageSize"] = value;
            }
        }
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
        public System.Data.DataTable gloabt//变更表
        {
            get
            {
                object dt = ViewState["gloabt"];
                return dt == null ? null : (System.Data.DataTable)dt;
            }
            set
            {
                ViewState["gloabt"] = value;
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
            if (Request.QueryString["mp_id"] != null)
            {
                sf = Request.QueryString["mp_id"].ToString();
            }
            if (!IsPostBack)
            {
                if (Request.QueryString["mp_id"] != null)
                {
                    gloabsheetno = Server.UrlDecode(Request.QueryString["mp_id"].ToString());
                }
                else
                {
                    gloabsheetno = "";
                }
                if (Request.QueryString["shape"] != null)
                {
                    gloabshape = Server.UrlDecode(Request.QueryString["shape"].ToString());
                }
                else
                {
                    gloabshape = "";
                }
                getArticle(true);
                this.BindCllx();
                this.BindFgr();
                this.BindCgy();
                this.BindSqr();
                this.BindZxr();
            }
        }
      
        private void initglotb()
        {
            string sqltext = "SELECT MP_OLDPTCODE FROM TBPC_MPCHANGEDETAIL WHERE  MP_STATE='0'";
            gloabt = DBCallCommon.GetDTUsingSqlText(sqltext);//变更表

        }
        private void BindPage(string tableName, string PrimaryKey, string ShowFields, string OrderField, string GroupField, int OrderType, string StrWhere, int PageSize, bool isFristPage)
        {

            InitVar(tableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFristPage);

        }
        //材料类型
        private void BindCllx()
        {
            string sqltext = "SELECT PUR_MASHAPE FROM View_TBPC_PURCHASEPLAN_IRQ_ORDER WHERE planno ='" + sf + "' GROUP BY PUR_MASHAPE";
            string datatext = "PUR_MASHAPE";
            string datavalue = "PUR_MASHAPE";
            DBCallCommon.BindDdl(cllx, sqltext, datatext, datavalue);
        }
        //分工人绑定
        private void BindFgr()
        {
            string sqltext = "SELECT fgrnm FROM View_TBPC_PURCHASEPLAN_IRQ_ORDER WHERE planno ='" + sf + "' GROUP BY fgrnm";
            string datatext="fgrnm";
            string datavalue="fgrnm";
            DBCallCommon.BindDdl(fgr, sqltext, datatext, datavalue);
        }
        //采购员
        private void BindCgy()
        {
            string sqltext = "SELECT cgrnm FROM View_TBPC_PURCHASEPLAN_IRQ_ORDER WHERE planno ='" + sf + "' GROUP BY cgrnm";
            string datatext = "cgrnm";
            string datavalue = "cgrnm";
            DBCallCommon.BindDdl(cgy, sqltext, datatext, datavalue);
        }
        //申请人
        private void BindSqr()
        {
            string sqltext = "SELECT sqrnm FROM View_TBPC_PURCHASEPLAN_IRQ_ORDER WHERE planno ='" + sf + "' GROUP BY sqrnm";
            string datatext = "sqrnm";
            string datavalue = "sqrnm";
            DBCallCommon.BindDdl(sqr, sqltext, datatext, datavalue);
        }
        //执行人
        private void BindZxr()
        {
            string sqltext = "SELECT piczdnm FROM View_TBPC_PURCHASEPLAN_IRQ_ORDER WHERE planno ='" + sf + "' GROUP BY piczdnm";
            string datatext = "piczdnm";
            string datavalue = "piczdnm";
            DBCallCommon.BindDdl(zxr, sqltext, datatext, datavalue);
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
            System.Data.DataTable dt = CommonFun.GetDataByPagerQueryParamGroupBy(pager);
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
            
        }

        void Pager_PageChanged(int pageNumber)
        {
            getArticle(false);
        }

        private void getArticle(bool isFirstPage)      //取得Article数据
        {
            string TableName = "View_TBPC_PURCHASEPLAN_IRQ_ORDER";

            string PrimaryKey = "";

            string ShowFields = "planno AS PUR_PCODE, pjid as PUR_PJID, pjnm as PUR_PJNAME,engid as PUR_ENGID,PUR_ID,sqrid,sqrnm," +
                         "engnm as PUR_ENGNAME,ptcode as PUR_PTCODE,marid as PUR_MARID,marnm as PUR_MARNAME," +
                         "margg as PUR_MARNORM,marcz as PUR_MARTERIAL,margb as PUR_GUOBIAO,marunit as PUR_NUNIT," +
                         "marfzunit as FZUNIT,length as PUR_LENGTH,width as PUR_WIDTH,rpnum as PUR_RPNUM," +
                         "rpfznum as PUR_RPFZNUM,jstimerq as PUR_TIMEQ,fgrnm AS PUR_PTASMAN,fgtime as PUR_PTASTIME, " +
                        "cgrid as PUR_CGMAN,cgrnm as PUR_CGMANNM,keycoms as PUR_KEYCOMS,purstate as PUR_STATE," +
                        "purnote as PUR_NOTE,picno as PIC_SHEETNO,CONVERT(varchar(12) , irqdata, 102 ) as ICL_IQRDATE," +
                        "piczdid as ICL_REVIEWA,piczdnm as ICL_REVIEWANM,marzxnum as ZXNUM,marzxfznum as ZXFZNUM," +
                        "orderno as PO_SHEETNO,PUR_TUHAO,PUR_MASHAPE ";

            //数据库中的主键
            string OrderField = "planno desc,ptcode";

            string GroupField = "";

            int OrderType = 0;
            /**/
            string StrWhere = "planno='" + sf + "' ";

            int PageSize = ObjPageSize;

            BindPage(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, isFirstPage);

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
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        string ptcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PTCODE")).Text;
                        string sql = "select PIC_PTCODE from TBPC_IQRCMPPRICE where PIC_PTCODE='" + ptcode + "'";
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        if (dt.Rows.Count > 0 || Convert.ToDouble(((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_STATE")).Text)>=6)
                        {
                            j++;
                        }
                        sql = "select PUR_STATE from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='0'";
                        System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                        if (dt1.Rows.Count > 0)
                        {
                            string state = dt1.Rows[0]["PUR_STATE"].ToString();
                            if (state == "5")
                            {
                                k++;
                            }
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
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_assign_list.aspx?ptc=" + gloabptc);
        }
        private bool is_sameeng()//判断是否同一批同一工程
        {
            string temppcode = "";
            bool temp = true;
            int i = 0;
            foreach (RepeaterItem Reitem in tbpc_purshaseplanassignRepeater.Items)
            {
                System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        if (i == 1)
                        {
                            temppcode = ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PCODE")).Text.ToString();
                        }
                        else
                        {
                            if (temppcode != ((System.Web.UI.WebControls.Label)Reitem.FindControl("PUR_PCODE")).Text.ToString())
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
            string bjdsheetno = "";
            string ddsheetno = "";
            string ptcode = "";
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlTableCell ph = (HtmlTableCell)e.Item.FindControl("ph");
                HtmlTableCell xm = (HtmlTableCell)e.Item.FindControl("xm");
                HtmlTableCell gc = (HtmlTableCell)e.Item.FindControl("gc");
                HtmlTableCell jhh = (HtmlTableCell)e.Item.FindControl("jhh");
                HtmlTableCell th = (HtmlTableCell)e.Item.FindControl("th");
                HtmlTableCell clbm = (HtmlTableCell)e.Item.FindControl("clbm");
                HtmlTableCell cz = (HtmlTableCell)e.Item.FindControl("cz");
                HtmlTableCell gb = (HtmlTableCell)e.Item.FindControl("gb");
                HtmlTableCell fgr = (HtmlTableCell)e.Item.FindControl("fgr");
                HtmlTableCell fgrq = (HtmlTableCell)e.Item.FindControl("fgrq");
                HtmlTableCell cgy = (HtmlTableCell)e.Item.FindControl("cgy");
                HtmlTableCell sqr = (HtmlTableCell)e.Item.FindControl("sqr");
                HtmlTableCell cd = (HtmlTableCell)e.Item.FindControl("cd");
                HtmlTableCell kd = (HtmlTableCell)e.Item.FindControl("kd");
                HtmlTableCell zxr = (HtmlTableCell)e.Item.FindControl("zxr");
                HtmlTableCell zxrq = (HtmlTableCell)e.Item.FindControl("zxrq");
                
            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableCell ph1 = (HtmlTableCell)e.Item.FindControl("ph1");
                HtmlTableCell xm1 = (HtmlTableCell)e.Item.FindControl("xm1");
                HtmlTableCell gc1 = (HtmlTableCell)e.Item.FindControl("gc1");
                HtmlTableCell jhh1 = (HtmlTableCell)e.Item.FindControl("jhh1");
                HtmlTableCell th1 = (HtmlTableCell)e.Item.FindControl("th1");
                HtmlTableCell clbm1 = (HtmlTableCell)e.Item.FindControl("clbm1");
                HtmlTableCell cz1 = (HtmlTableCell)e.Item.FindControl("cz1");
                HtmlTableCell gb1 = (HtmlTableCell)e.Item.FindControl("gb1");
                HtmlTableCell fgr1 = (HtmlTableCell)e.Item.FindControl("fgr1");
                HtmlTableCell fgrq1 = (HtmlTableCell)e.Item.FindControl("fgrq1");
                HtmlTableCell cgy1 = (HtmlTableCell)e.Item.FindControl("cgy1");
                HtmlTableCell sqr1 = (HtmlTableCell)e.Item.FindControl("sqr1");
                HtmlTableCell cd1 = (HtmlTableCell)e.Item.FindControl("cd1");
                HtmlTableCell kd1 = (HtmlTableCell)e.Item.FindControl("kd1");
                HtmlTableCell zxr1 = (HtmlTableCell)e.Item.FindControl("zxr1");
                HtmlTableCell zxrq1 = (HtmlTableCell)e.Item.FindControl("zxrq1");
             
                //订单、比价单处理
                #region
                state = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_STATE")).Text;
                ptcode = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_PTCODE")).Text;
                if (Convert.ToInt32(state) >= 6)
                {
                    bjdsheetno = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PIC_SHEETNO")).Text;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_BJD")).ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)e.Item.FindControl("Hypbjd")).NavigateUrl = "TBPC_IQRCMPPRCLST_checked_detail.aspx?sheetno=" + bjdsheetno + "&ptc=" + ptcode + "";
                }
                if (Convert.ToInt32(state) >= 7)
                {
                    ddsheetno = ((System.Web.UI.WebControls.Label)e.Item.FindControl("PO_SHEETNO")).Text;
                    ((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_DD")).ForeColor = System.Drawing.Color.Red;
                    ((HyperLink)e.Item.FindControl("Hypdd")).NavigateUrl = "PC_TBPC_PurOrder.aspx?orderno=" + ddsheetno + "&ptc=" + ptcode + "";
                }
                #endregion
                //单击一行变色
                ((HtmlTableRow)e.Item.FindControl("row")).Attributes.Add("onclick", "MouseClick1(this)");
                if (Convert.ToInt32(state) == 5)
                {
                    ((HtmlTableCell)e.Item.FindControl("ch")).BgColor = "Red";
                    ((System.Web.UI.WebControls.CheckBox)e.Item.FindControl("CKBOX_SELECT")).Enabled = false;
                }
                string sql = "select * from TBPC_MPTEMPCHANGE where MP_CHPTCODE='" + ptcode + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    ((HtmlTableCell)e.Item.FindControl("ch1")).BgColor = "Green";
                }
                double num = Convert.ToDouble(((System.Web.UI.WebControls.Label)e.Item.FindControl("PUR_RPNUM")).Text);
                if (num == 0)
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#FFFF00";
                }
                
            }
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
        //重置查询条件
        protected void clear_Click(object sender, EventArgs e)
        {
            planid.Text = "";
            mc.Text = "";
            gb.Text = "";
            clbm.Text = "";
            gg.Text = "";
            cz.Text = "";
            tuhao.Text = "";
            cllx.SelectedIndex=0;
            fgr.SelectedIndex = 0;
            fgrq.Value = "";
            cgy.SelectedIndex = 0;
            sqr.SelectedIndex = 0;
            zxr.SelectedIndex = 0;
            zxrq.Value = "";
            sfbjd.SelectedIndex = 0;
            sfdd.SelectedIndex = 0;
            getArticle(true);
        }
        //查询条件
        protected void search_Click(object sender, EventArgs e)
        {   
            string TableName = "View_TBPC_PURCHASEPLAN_IRQ_ORDER";

            string PrimaryKey = "";

            string ShowFields = "planno AS PUR_PCODE, pjid as PUR_PJID, pjnm as PUR_PJNAME,engid as PUR_ENGID,PUR_ID,sqrid,sqrnm," +
                         "engnm as PUR_ENGNAME,ptcode as PUR_PTCODE,marid as PUR_MARID,marnm as PUR_MARNAME," +
                         "margg as PUR_MARNORM,marcz as PUR_MARTERIAL,margb as PUR_GUOBIAO,marunit as PUR_NUNIT," +
                         "marfzunit as FZUNIT,length as PUR_LENGTH,width as PUR_WIDTH,rpnum as PUR_RPNUM," +
                         "rpfznum as PUR_RPFZNUM,jstimerq as PUR_TIMEQ,fgrnm AS PUR_PTASMAN,fgtime as PUR_PTASTIME, " +
                        "cgrid as PUR_CGMAN,cgrnm as PUR_CGMANNM,keycoms as PUR_KEYCOMS,purstate as PUR_STATE," +
                        "purnote as PUR_NOTE,picno as PIC_SHEETNO,CONVERT(varchar(12) , irqdata, 102 ) as ICL_IQRDATE," +
                        "piczdid as ICL_REVIEWA,piczdnm as ICL_REVIEWANM,marzxnum as ZXNUM,marzxfznum as ZXFZNUM," +
                        "orderno as PO_SHEETNO,PUR_TUHAO,PUR_MASHAPE ";

            //数据库中的主键
            string OrderField = "planno desc,ptcode";

            string GroupField = "";

            int OrderType = 0;
            /**/
            string StrWhere = "planno='" + sf + "' ";
            if (planid.Text != "")
            {
                StrWhere += " and ptcode like '%" + planid.Text.ToString().Trim() + "%'";
            }
            if(mc.Text!="")
            {
                StrWhere += " and marnm like '%"+mc.Text.ToString().Trim()+"%'";
            }
            if(gb.Text!="")
            {
                StrWhere += " and margb like '%"+gb.Text.ToString().Trim()+"%'";
            }
            if (clbm.Text != "")
            {
                StrWhere += " and marid like '%"+clbm.Text.ToString().Trim()+"%'";
            }
            if (gg.Text != "")
            {
                StrWhere += " and margg like '%"+gg.Text.ToString().Trim()+"%'";
            }
            if(cz.Text!="")
            {
                StrWhere+=" and marcz like '%"+cz.Text.ToString().Trim()+"%'";
            }
            if (tuhao.Text != "")
            {
                StrWhere += " and PUR_TUHAO like '%"+tuhao.Text.ToString().Trim()+"%'";
            }
            if (cllx.SelectedIndex != 0)
            {
                StrWhere += " and PUR_MASHAPE='"+cllx.SelectedValue.ToString().Trim()+"'";
            }
            if(fgr.SelectedIndex!=0)
            {
                if (fgr.SelectedValue.ToString().Trim() == "")
                {
                    StrWhere += " and fgrnm is Null";
                }
                else
                {
                    StrWhere += " and fgrnm='" + fgr.SelectedValue.ToString().Trim() + "'";
                }
            }
            if(fgrq.Value!="")
            {
                StrWhere += " and fgtime='"+fgrq.Value.ToString().Trim()+"'";
            }
            if(cgy.SelectedIndex!=0)
            {
                if (cgy.SelectedValue.ToString().Trim() == "")
                {
                    StrWhere += " and cgrnm is null";
                }
                else
                {
                    StrWhere += " and cgrnm='" + cgy.SelectedValue.ToString().Trim() + "'";
                }
            }
            if (sqr.SelectedIndex != 0)
            {
                if (sqr.SelectedValue.ToString().Trim() == "")
                {
                    StrWhere += " and sqrnm is null";
                }
                else
                {
                    StrWhere += " and sqrnm='"+sqr.SelectedValue.ToString().Trim()+"'";
                }
            }
            if(zxr.SelectedIndex!=0)
            {
                if (zxr.SelectedValue.ToString().Trim() == "")
                {
                    StrWhere += " and piczdnm is null";
                }
                else
                {
                    StrWhere += " and piczdnm='"+zxr.SelectedValue.ToString().Trim()+"'";
                }
            }
            if (zxrq.Value != "")
            {
                StrWhere += " and piczdtime='"+zxrq.Value.ToString().Trim()+"'";
            }
            if (sfbjd.SelectedIndex != 0)
            {
                if (sfbjd.SelectedIndex == 1)
                {
                    StrWhere += " and PURSTATE>=6";
                }
                else
                {
                    StrWhere += " and PURSTATE<6";
                }
            }
            if (sfdd.SelectedIndex != 0)
            {
                if (sfdd.SelectedIndex == 1)
                {
                    StrWhere += " and PURSTATE>=7";
                }
                else
                {
                    StrWhere += " and PURSTATE<7";
                }
            }
            int PageSize = ObjPageSize;

            InitVar(TableName, PrimaryKey, ShowFields, OrderField, GroupField, OrderType, StrWhere, PageSize, true);
        }
     
    }
}
