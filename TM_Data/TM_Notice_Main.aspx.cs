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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Notice_Main : System.Web.UI.Page
    {
        string sqlText;
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                bindGrid();
                comboBoxPro();//绑定主查询--项目查询
                comboBoxEng();//绑定查询--工程查询
                BindPersons();//绑定查询--按人员查询
            }
        }

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数      
        }


        //初始化分布信息
        private void InitPager()
        {
            pager.TableName = "TBPM_DEPCONSHEET";
            pager.PrimaryKey = "DCS_ID";
            pager.ShowFields = "DCS_ID,DCS_PROJECT,DCS_ENGNAME,DCS_TYPE,DCS_EDITOR,DCS_DATE";
            pager.OrderField = "cast(DCS_DATE as datetime)";
            pager.StrWhere = "DCS_DEPID in ('03','01') and DCS_EDITORID='" + Session["UserID"] + "' ";//默认显示登录人员的联系单
            pager.OrderType = 1;//按时间降序排列
            pager.PageSize = 16;
            //pager.PageIndex = 1;
        }


        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
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

            foreach (RepeaterItem items in Repeater1.Items)
            {
                Label lb = (Label)items.FindControl("BCSTYPE");
                if (lb.Text != "")
                {
                    switch (Convert.ToInt32(lb.Text))
                    {
                        case 1:
                            lb.Text = "合同信息";
                            break;
                        case 2:
                            lb.Text = "任务单信息";
                            break;
                        case 3:
                            lb.Text = "其他信息";
                            break;
                    }
                }
            }
        }

        private void RebindGrid()
        {
            InitPager();
            pager.StrWhere = CreateConStr();
            bindGrid();
        }

        private string CreateConStr()
        {
            string strWhere;
            if (ComboBox2.SelectedValue != "0")
            {
                strWhere = "DCS_PROJECTID ='" + ComboBox2.SelectedValue + "' and ";
                if (ComboBox1.SelectedValue != "0")
                {
                    strWhere += "DCS_ENGID='" + ComboBox1.SelectedValue + "' and ";
                    if (ddlstatus.SelectedValue != "0" && ddlpersons.SelectedValue != "-请选择-")
                    {
                        strWhere += "DCS_TYPE='" + ddlstatus.SelectedValue + "' and ";
                        strWhere += "DCS_EDITORID='" + ddlpersons.SelectedValue + "' and ";
                    }
                    else if (ddlstatus.SelectedValue != "0" && ddlpersons.SelectedValue == "-请选择-")
                    {
                        strWhere += "DCS_TYPE='" + ddlstatus.SelectedValue + "' and ";
                    }
                    else if (ddlstatus.SelectedValue == "0" && ddlpersons.SelectedValue != "-请选择-")
                    {
                        strWhere += "DCS_EDITORID='" + ddlpersons.SelectedValue + "' and ";
                    }
                }
                else
                {
                    if (ddlstatus.SelectedValue != "0" && ddlpersons.SelectedValue != "-请选择-")
                    {
                        strWhere += "DCS_TYPE='" + ddlstatus.SelectedValue + "' and ";
                        strWhere += "DCS_EDITORID='" + ddlpersons.SelectedValue + "' and ";
                    }
                    else if (ddlstatus.SelectedValue != "0" && ddlpersons.SelectedValue == "-请选择-")
                    {
                        strWhere += "DCS_TYPE='" + ddlstatus.SelectedValue + "' and ";
                    }
                    else if (ddlstatus.SelectedValue == "0" && ddlpersons.SelectedValue != "-请选择-")
                    {
                        strWhere += "DCS_EDITORID='" + ddlpersons.SelectedValue + "' and ";
                    }
                }
            }
            else
            {
                if (ddlstatus.SelectedValue != "0" && ddlpersons.SelectedValue != "-请选择-")
                {
                    strWhere = "DCS_TYPE='" + ddlstatus.SelectedValue + "' and ";
                    strWhere += "DCS_EDITORID='" + ddlpersons.SelectedValue + "' and ";
                }
                else if (ddlstatus.SelectedValue != "0" && ddlpersons.SelectedValue == "-请选择-")
                {
                    strWhere = "DCS_TYPE='" + ddlstatus.SelectedValue + "' and ";
                }
                else if (ddlstatus.SelectedValue == "0" && ddlpersons.SelectedValue != "-请选择-")
                {
                    strWhere = "DCS_EDITORID='" + ddlpersons.SelectedValue + "' and ";
                }
                else
                {
                    strWhere = " ";
                }
            }
            strWhere += "DCS_DEPID in ('03','01') ";
            return strWhere;
        }

        //初始化绑定项目
        private void comboBoxPro()
        {
            sqlText = "select distinct DCS_PROJECTID, DCS_PROJECTID+'‖' +DCS_PROJECT as DCS_PROJECT ";
            sqlText += "from TBPM_DEPCONSHEET where DCS_DEPID in ('03','01') ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ComboBox2.DataSource = dt;
            ComboBox2.DataTextField = "DCS_PROJECT";
            ComboBox2.DataValueField = "DCS_PROJECTID";
            ComboBox2.DataBind();
            ComboBox2.Items.Insert(0,new ListItem("全部","0"));
            ComboBox2.SelectedIndex = 0;
        }

        //初始化绑定工程
        private void comboBoxEng()
        {
            sqlText = "select distinct DCS_ENGID, DCS_ENGID+'‖' +DCS_ENGNAME as DCS_ENGNAME ";
            sqlText += "from TBPM_DEPCONSHEET where DCS_DEPID in ('03','01') ";
            sqlText += "and DCS_PROJECTID='" + ComboBox2.SelectedValue + "' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ComboBox1.DataSource = dt;
            ComboBox1.DataTextField = "DCS_ENGNAME";
            ComboBox1.DataValueField = "DCS_ENGID";
            ComboBox1.DataBind();
            ComboBox1.Items.Insert(0, new ListItem("全部", "0"));
            ComboBox1.SelectedIndex = 0;
        }

        //初始化公司领导与技术部人员
        private void BindPersons()
        {
            sqlText = "select distinct DCS_EDITORID,DCS_EDITOR from TBPM_DEPCONSHEET ";
            sqlText += "where DCS_DEPID in ('03','01') ";
            string dataText = "DCS_EDITOR";
            string dataValue = "DCS_EDITORID";
            DBCallCommon.BindDdl(ddlpersons, sqlText, dataText, dataValue);
        }

        //约束绑定项目
        private void restrainBindPro()
        {
            sqlText = "select distinct DCS_PROJECTID, DCS_PROJECTID+'‖' +DCS_PROJECT as DCS_PROJECT ";
            sqlText += "from TBPM_DEPCONSHEET where DCS_DEPID in ('03','01') ";
            if (ddlstatus.SelectedValue != "0" && ddlpersons.SelectedValue != "-请选择-")
            {
                sqlText += "and DCS_TYPE='" + ddlstatus.SelectedValue + "' ";
                sqlText += "and DCS_EDITORID='" + ddlpersons.SelectedValue + "' ";
            }
            else if (ddlstatus.SelectedValue != "0" && ddlpersons.SelectedValue == "-请选择-")
            {
                sqlText += "and DCS_TYPE='" + ddlstatus.SelectedValue + "' ";
            }
            else if (ddlstatus.SelectedValue == "0" && ddlpersons.SelectedValue != "-请选择-")
            {
                sqlText += "and DCS_EDITORID='" + ddlpersons.SelectedValue + "' ";
            }
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ComboBox2.DataSource = dt;
            ComboBox2.DataTextField = "DCS_PROJECT";
            ComboBox2.DataValueField = "DCS_PROJECTID";
            ComboBox2.DataBind();
            ComboBox2.Items.Insert(0, new ListItem("全部", "0"));
            ComboBox2.SelectedIndex = 0;
        }

        //约束绑定部门人员
        private void restrainBindPerson()
        {
            sqlText = "select distinct DCS_EDITORID,DCS_EDITOR from TBPM_DEPCONSHEET ";
            sqlText += "where DCS_DEPID in ('03','01') ";
            if (ddlstatus.SelectedValue != "0" &&ComboBox2.SelectedValue!="0")
            {
                sqlText += "and DCS_TYPE='" + ddlstatus.SelectedValue + "' ";
                sqlText += "and DCS_PROJECTID ='" + ComboBox2.SelectedValue + "' ";
            }
            else if (ddlstatus.SelectedValue != "0" && ComboBox2.SelectedValue == "0")
            {
                sqlText += "and DCS_TYPE='" + ddlstatus.SelectedValue + "' ";
            }
            else if (ddlstatus.SelectedValue == "0" && ComboBox2.SelectedValue != "0")
            {
                sqlText += "and DCS_PROJECTID ='" + ComboBox2.SelectedValue + "' ";
            }
            string dataText = "DCS_EDITOR";
            string dataValue = "DCS_EDITORID";
            DBCallCommon.BindDdl(ddlpersons, sqlText, dataText, dataValue);
        }

        protected void ComboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox2.SelectedValue!="0")
            {
                if (ddlpersons.SelectedValue != "0")
                {
                    comboBoxEng();
                    restrainBindPerson();
                }
            }
            else
            {
                if (ddlpersons.SelectedValue == "0")
                {
                    BindPersons();
                }
                comboBoxEng();
            }
            RebindGrid();
        }

        protected void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBox1.SelectedValue != "0")
            {
                if (ddlpersons.SelectedValue != "0")
                {
                    restrainBindPerson();
                }
            }
            else
            {
                if (ddlpersons.SelectedValue == "0")
                {
                    BindPersons();
                }
            }
            RebindGrid();
        }

        protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            RebindGrid();
        }

        protected void ddlpersons_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlpersons.SelectedValue != "0")
            {
                if (ComboBox2.SelectedValue == "0")
                {
                    restrainBindPro();
                }
            }
            else
            {
                if (ComboBox2.SelectedValue == "0")
                {
                    comboBoxPro();
                    BindPersons();
                }
            }
            RebindGrid();
        }
    }
}
