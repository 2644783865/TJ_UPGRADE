using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using ZCZJ_DPF;
using System.Collections.Generic;
using System.IO;

namespace ZCZJ_DPF.CM_Data
{
    public partial class TM_HZY_info : BasicPage
    {
       PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                bindGrid();
                comboBoxData();//绑定主查询--项目查询
            }
            CheckUser(ControlFinder);
        }

        private void InitVar()
        {
            //btnDelete.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数      
        }


        //初始化分布信息
        private void InitPager()
        {
            pager.TableName = "TBMP_PRDCTTASKTOTAL";
            pager.PrimaryKey = "PT_CODE";
            pager.ShowFields = "PT_CODE,PT_PJNAME,PT_ENGNAME,PT_PMCHARGERNM,PT_PDCHARGERNM,PT_DATE,PT_ICKCOMNM,PT_JSJE,PT_ISJS";
            pager.OrderField = "PT_CODE";
            pager.StrWhere = "";
            pager.OrderType = 0;
            pager.PageSize = 10;
            //pager.PageIndex = 1;
        }


        void Pager_PageChanged(int pageNumber)
        {

            bindGrid();

        }


        private void bindGrid()
        {
            //DataTable dt = DBCProcPageing.Projects_Select(UCPaging1.CurrentPage, UCPaging1.PageSize);
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Repeater1 ,UCPaging1,NoDataPanel);
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

        void comboBoxData()
        {         
            string sql = "select distinct PT_PJNAME from TBMP_PRDCTTASKTOTAL";//从任务单管理中读取所有的项目名称
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                ComboBox1.DataSource = dt;
                ComboBox1.DataTextField = "PT_PJNAME";
                ComboBox1.DataValueField = "PT_PJNAME";
                ComboBox1.DataBind();
            }
            ComboBox1.Items.Insert(0, new ListItem("-全部-", ""));

        }

        protected void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (ListItem lt in ddl_state.Items)
            {
                lt.Selected = false;
            }
            ddl_state.Items.FindByText("-全部-").Selected = true;
            foreach (ListItem lt in ddl_type.Items)
            {
                lt.Selected = false;
            }
            ddl_type.Items.FindByText("-全部-").Selected = true;

            UCPaging1.CurrentPage = 1;
            RebindGrid("prj");
        }

        private void RebindGrid(string type)
        {
            InitPager();

            pager.StrWhere = CreateConStr(type);

            bindGrid();
        }


        private string CreateConStr(string type)
        {
            string strWhere = string.Empty;

            string status = string.Empty;
            if (type == "prj")
            {
                status = ComboBox1.SelectedItem.Value;

                if (status != string.Empty)

                    strWhere = "PT_PJNAME ='" + status + "'";

            }
            else if(type=="type")
            {
                status = ComboBox1.SelectedItem.Value;

                if (status != string.Empty)
                {
                    if (ddl_type.SelectedItem.Value != string.Empty)

                        strWhere = "PT_PJNAME ='" + status + "' and PT_TYPE='" + ddl_type.SelectedItem.Value + "'";
                    else
                    {
                        strWhere = "PT_PJNAME ='" + status + "'";
                    }
                }
                else
                {
                    if (ddl_type.SelectedItem.Value != string.Empty)

                        strWhere = "PT_TYPE='" + ddl_type.SelectedItem.Value + "'";

                    else

                        strWhere = string.Empty;

                }
            }
            else if (type == "state")
            {
                status = ComboBox1.SelectedItem.Value;

                if (status != string.Empty)
                {
                    if (ddl_state.SelectedItem.Value!=string.Empty)

                        strWhere = "PT_PJNAME ='" + status + "' and PT_ISJS='" + ddl_state.SelectedItem.Value + "'";

                    else
                        strWhere = "PT_PJNAME ='" + status + "'";
                }
                else
                {
                    if (ddl_state.SelectedItem.Value != string.Empty)

                        strWhere = "PT_ISJS='" + ddl_state.SelectedItem.Value + "'";

                    else

                        strWhere = string.Empty;
                }
            }

            return strWhere;

        }

        //是否有结算按钮
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                 Label lb_js=(Label)e.Item.FindControl("lb_isjs");
                 if (lb_js.Text.Trim()==string.Empty)
                 {
                     ((HyperLink)e.Item.FindControl("HyperLink1")).Visible=false;
                 }
            }
        }

        protected void ddl_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(ListItem lt in ddl_state.Items)
            {
                lt.Selected=false;
            }
            ddl_state.Items.FindByText("-全部-").Selected = true;
            UCPaging1.CurrentPage = 1;
            RebindGrid("type");
        }

        protected void ddl_state_SelectedIndexChanged(object sender, EventArgs e)
        {

            foreach (ListItem lt in ddl_type.Items)
            {
                lt.Selected = false;
            }
            ddl_type.Items.FindByText("-全部-").Selected = true;
            UCPaging1.CurrentPage = 1;
            RebindGrid("state");
        }

        //删除没有加限制条件
        protected void btn_del_Click(object sender, EventArgs e)
        {

            List<string> lt = new List<string>();

            List<string> ltsql = new List<string>();

            for(int i=0;i<Repeater1.Items.Count;i++)
            {
                CheckBox cb = (CheckBox)Repeater1.Items[i].FindControl("CKBOX_SELECT");

                if (cb.Checked)
                {
                    string code =((Label)Repeater1.Items[i].FindControl("PT_CODE")).Text;

                    lt.Add(code);

                }
            }

            foreach (string code in lt)
            {
                string selsql = "select PT_FILE,PT_FILE1 from TBMP_PRDCTTASKTOTAL where PT_CODE='" + code + "'";

                DataSet ds = DBCallCommon.FillDataSet(selsql);

                if(ds.Tables[0].Rows.Count>0)
                {
                    string content = ds.Tables[0].Rows[0]["PT_FILE"].ToString();

                    string content1 = ds.Tables[0].Rows[0]["PT_FILE1"].ToString();

                    DeleteTFN(content);

                    DeleteTFN(content1);

                    string filesql = "DELETE FROM tb_files WHERE BC_CONTEXT = '" + content + "'";



                    string sql = "delete from TBMP_PRDCTTASKTOTAL where PT_CODE='" + code + "'";

                    string subsql = "delete from TBMP_PRDCTTASKDETAIL where PT_CODE='" + code + "'";

                    ltsql.Add(filesql);

                    ltsql.Add(sql);

                    ltsql.Add(subsql);
                }
            }

            DBCallCommon.ExecuteTrans(ltsql);

            Response.Redirect("TM_HZY_info.aspx");
        }

        //删除文件
        protected void DeleteTFN(string context)
        {
            string sqlStr = "select fileName from tb_files where BC_CONTEXT='" + context + "'";

            DataSet ds = DBCallCommon.FillDataSet(sqlStr);

            if (ds.Tables[0].Rows.Count>0)
            {

                for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                {
                    string strFilePath = Server.MapPath("../Contract/") + ds.Tables[0].Rows[i][0].ToString();
                    File.Delete(strFilePath);//文件不存在也不会引发异常
                }

            }
        }
    }
}
