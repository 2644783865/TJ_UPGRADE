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

namespace ZCZJ_DPF.QR_Interface.FInished_QRInOutManage
{
    public partial class QRIn_Finished_List : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
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
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);

            if (!IsPostBack)
            {
                getOrderInfo(true, false);
            }
        }
        protected void getOrderInfo(bool isFristPage, bool isUpdateCondition)
        {
            string condition = GetCondition();

            string TableName = "(select s.*,CM_PROJ,BM_CHANAME,BM_TUHAO,BM_NUMBER,BM_KU from midTable_Finished_QRIn as s left join View_TM_DQO as a on s.CPQRIn_TaskID=a.BM_ENGID and s.CPQRIn_Zongxu=a.BM_ZONGXU left join View_CM_TSAJOINPROJ as b on a.BM_ENGID=b.TSA_ID)t";
            string PrimaryKey = "CPQRIn_ID";
            string ShowFields = "*";
            string OrderField = "CPQRIn_TaskID,CPQRIn_Zongxu";
            int OrderType = 0;
            string StrWhere = condition;
            int PageSize = ObjPageSize;

            InitVar(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize, isFristPage);
        }
        private string GetCondition()
        {
            //总表，从表都不等于0
            string condition = "1=1";
            //入库状态
            if (rblInState.SelectedValue.ToString().Trim() != "")
            {
                condition += " and CPQRIn_State='" + rblInState.SelectedValue.ToString().Trim() + "'";
            }
            //起始时间
            if (txtStartYearMonth.Text.Trim() != "")
            {
                condition += " and CPQRIn_Time>='" + txtStartYearMonth.Text.Trim() + "'";
            }
            //终止时间
            if (txtEndYearMonth.Text.Trim() != "")
            {
                condition += " and CPQRIn_Time<='" + txtEndYearMonth.Text.Trim() + "'";
            }
            //任务号
            if (tbCPQRIn_TaskID.Text.Trim() != "")
            {
                condition += " and CPQRIn_TaskID like '%" + tbCPQRIn_TaskID.Text.Trim() + "%'";
            }
            //总序
            if (tbCPQRIn_Zongxu.Text.Trim() != "")
            {
                condition += " and CPQRIn_Zongxu like '%" + tbCPQRIn_Zongxu.Text.Trim() + "%'";
            }
            //项目名称
            if (tbEngName.Text.Trim() != "")
            {
                condition += " and CM_PROJ like '%" + tbEngName.Text.Trim() + "%'";
            }
            //产品名称
            if (tbProdName.Text.Trim() != "")
            {
                condition += " and BM_CHANAME like '%" + tbProdName.Text.Trim() + "%'";
            }
            return condition;
        }
        private void InitVar(string TableName, string PrimaryKey, string ShowFields, string OrderField, int OrderType, string StrWhere, int PageSize, bool isFristPage)
        {
            InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);

            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数

            if (isFristPage)
            {
                UCPaging1.CurrentPage = 1;
            }

            //否则即为当前页

            bindData();
        }
        //初始化分页信息
        private void InitPager(string TableName, string PrimaryKey, string ShowFields, string OrderField, int OrderType, string StrWhere, int PageSize)
        {
            pager.TableName = TableName;

            pager.PrimaryKey = PrimaryKey;

            pager.ShowFields = ShowFields;

            pager.OrderField = OrderField;

            pager.OrderType = OrderType;

            pager.StrWhere = StrWhere;

            pager.PageSize = PageSize;
        }
        void Pager_PageChanged(int pageNumber)
        {
            getOrderInfo(false, false);
        }

        protected void bindData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;

            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);

            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);

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
        protected void Query_Click(object sender, EventArgs e)
        {
            getOrderInfo(true, true);
        }
        protected void rblInState_SelectedIndexChanged(object sender, EventArgs e)
        {
            getOrderInfo(true, true);
        }
        //下推物料
        protected void Push_Click(object sender, EventArgs e)
        {
            List<string> sqllist = new List<string>();
            string sql = "";
            string sql2 = "";
            int TaNum = 0;
            int uniqID = 0;
            string CurTa = "";
            string ZongXu = "";
            string sqlcheckrk = "";
            int count = 0;
            int RkNum = 0;

            string QRTime = DateTime.Now.ToString("yyyy-MM-dd");

            string DocCode = GetCode();

            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                if (((CheckBox)Repeater1.Items[i].FindControl("CheckBox1")).Checked == true)
                {
                    count += 1;
                    ZongXu = ((Label)Repeater1.Items[i].FindControl("lbCPQRIn_Zongxu")).Text.Trim();
                    //验证是否只包含一个任务号
                    if (CurTa == "")
                    {
                        TaNum += 1;
                        CurTa = ((Label)Repeater1.Items[i].FindControl("lbCPQRIn_TaskID")).Text.Trim();
                    }
                    else
                    {
                        if (CurTa != ((Label)Repeater1.Items[i].FindControl("lbCPQRIn_TaskID")).Text.Trim())
                        {
                            TaNum += 1;
                            CurTa = ((Label)Repeater1.Items[i].FindControl("lbCPQRIn_TaskID")).Text.Trim();
                        }
                    }
                    if (TaNum > 1)
                    {
                        string alertTa = "<script>alert('勾选项涉及多个任务号，无法完成下推！')</script>";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alertTa, false);
                        return;
                    }
                    if (((Label)Repeater1.Items[i].FindControl("lbCPQRIn_State")).Text == "已入库" || ((Label)Repeater1.Items[i].FindControl("lbCPQRIn_State")).Text == "其它")
                    {
                        string alert = "<script>alert('存在已入库条目或问题数据，下推被阻止！')</script>";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                        return;
                    }

                    sqlcheckrk = "select BM_ZONGXU,BM_TUHAO,BM_CHANAME,BM_NUMBER,BM_TUUNITWGHT,BM_YRKNUM,BM_KU,case when  BM_MARID<>'' then 'open' when BM_MARID='' then 'closed' end as state from dbo.View_TM_DQO where  BM_MSSTATUS<>'1'  and BM_ENGID='" + CurTa + "' and BM_ZONGXU='" + ZongXu + "' and (BM_KU like '%S%' or BM_MARID='' )";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlcheckrk);
                    if (dt.Rows.Count > 0)
                    {
                        if (dt.Rows[0]["BM_NUMBER"].ToString().Trim() == dt.Rows[0]["BM_YRKNUM"].ToString().Trim())
                        {
                            string alert = "<script>alert('该成品已入库或正在审核，请勿重复入库！')</script>";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                            return;
                        }
                    }

                    //添加SQL语句
                    if (((Label)Repeater1.Items[i].FindControl("lbCPQRIn_Num")).Text.Trim() == "")
                    {
                        RkNum = 0;
                    }
                    else
                    {
                        RkNum = Convert.ToInt32(((Label)Repeater1.Items[i].FindControl("lbCPQRIn_Num")).Text.Trim());
                    }
                    uniqID = Convert.ToInt32(((Label)Repeater1.Items[i].FindControl("lbCPQRIn_ID")).Text.ToString());
                    sql = "insert into TBMP_FINISHED_IN (TFI_PROJ,TFI_SINGNUMBER,TFI_DOCNUM,TFI_ZONGXU,TSA_ID,TFI_NAME,TFI_MAP,TFI_RKNUM,TFI_NUMBER,TFI_WGHT,KU,INDATE,DocuPersonID,REVIEWA,SQRID,SPZT,QRInUniqCode) select CM_PROJ,BM_SINGNUMBER,'" + DocCode + "','" + ZongXu + "',BM_ENGID,BM_CHANAME,BM_TUHAO," + RkNum + ",BM_NUMBER,BM_TUUNITWGHT,BM_KU,'" + QRTime + "','" + Session["UserID"] + "','" + Session["UserID"] + "','" + Session["UserID"] + "','0'," + uniqID + " from View_TM_DQO as a left join View_CM_TSAJOINPROJ as b on a.BM_ENGID=b.TSA_ID  where BM_ENGID='" + CurTa + "' and BM_ZONGXU='" + ZongXu + "'";
                    sqllist.Add(sql);
                    sql2 = "update midTable_Finished_QRIn set CPQRIn_State='1' where CPQRIn_ID=" + Convert.ToInt32(((Label)Repeater1.Items[i].FindControl("lbCPQRIn_ID")).Text.ToString()) + "";
                    sqllist.Add(sql2);
                }
            }

            if (count == 0)
            {
                string alert = "<script>alert('请勾选要入库的数据！')</script>";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "", alert, false);
                return;
            }
            else
            {
                DBCallCommon.ExecuteTrans(sqllist);
                Response.Redirect("~/PM_Data/PM_Finished_look.aspx?action=view&docnum=" + DocCode + "&id=" + CurTa);
            }
        }

        /// <summary>
        /// 生成入库单号
        /// </summary>
        private string GetCode()
        {
            string sqltext;
            sqltext = "select TOP 1 TFI_DOCNUM AS TopIndex from TBMP_FINISHED_IN ORDER BY TFI_DOCNUM DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int index;
            if (dt.Rows.Count > 0)
            {
                index = Convert.ToInt16(dt.Rows[0]["TopIndex"].ToString());
            }
            else
            {
                index = 0;
            }
            string code = (index + 1).ToString();
            return code.PadLeft(8, '0');
        }
    }
}
