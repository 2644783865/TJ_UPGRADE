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

namespace ZCZJ_DPF.Basic_Data
{
    public partial class WL_Material_List : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            btnDelete.Click += new EventHandler(btnDelete_Click);
            btnDelete.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            NoDataPanel.Visible = false;
            if (!IsPostBack)
            {
                txt_px.SelectedIndex = 0;
                txt_pxl.SelectedIndex = 0;
                getClassOne();
                getClassTwo();
                this.BindWFName();
                InitVar();
                GetData();
            }
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);

            CheckUser(ControlFinder);
        }

        private void InitVar()
        {
            string sql = "";
            sql = " * ";
            InitPager("VIEW_MATERIAL", "ID", sql, "ID",0," TYPEID IN (SELECT TY_ID FROM TBMA_TYPEINFO WHERE TY_NAME='" + DDLname.SelectedItem.Text + "')");
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数      
        }

        //初始化分布信息
        private void InitPager(string tablename, string key, string showField, string orderField,int orderType, string where)
        {
            pager.TableName = tablename;
            pager.PrimaryKey = key;
            pager.ShowFields = showField;
            pager.OrderField = orderField;
            pager.OrderType = orderType;
            pager.StrWhere = where;
            pager.PageSize = Convert.ToInt16(ddl_pageno.SelectedValue);
        }

        protected void Pager_PageChanged(int pageNumber)
        {
            GetData();
        }

        protected void getClassOne()
        {
            string sql = "SELECT DISTINCT TY_NAME,TY_ID FROM TBMA_TYPEINFO WHERE TY_FATHERID='ROOT' ORDER BY TY_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DDLclass.DataSource = dt;
            DDLclass.DataTextField = "TY_NAME";
            DDLclass.DataValueField = "TY_ID";
            DDLclass.DataBind();
            DDLclass.Items.Insert(0, new ListItem("全部", "%"));
            DDLclass.SelectedIndex = 2;
        }
        

        protected void getClassTwo()
        {
            string sql = "SELECT DISTINCT TY_NAME,TY_ID FROM TBMA_TYPEINFO WHERE TY_FATHERID='" + DDLclass.SelectedValue + "'  ORDER BY TY_NAME";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DDLname.DataSource = dt;
            DDLname.DataTextField = "TY_NAME";
            DDLname.DataValueField = "TY_ID";
            DDLname.DataBind();          
            DDLname.Items.Insert(0, new ListItem("全部", ""));
            DDLname.SelectedIndex = 0;
            
           
        }

        //根据条件获取数据
        protected void GetData()
        {
            string sql = "";

            string sqlwhere = "";

            sqlwhere += " STATE='" + rblZT.SelectedValue + "'";

            if (DDLname.SelectedItem.Value != "")
            {
                sqlwhere += " AND TYPEID='" + DDLname.SelectedValue + "'";
            }

            if (ddlWFName.SelectedIndex != 0)
            {
                sqlwhere += " AND CLERK='" + ddlWFName.SelectedValue + "'";
            }

            sqlwhere += " and TYPEID like '" + DDLclass.SelectedValue + ".%'";

                    if (txt_ID2.Text != "")
                    {
                        sqlwhere=sqlwhere+"and ID like '%"+txt_ID2.Text+"%'";
                    }
                    if(date_ID.Value!="")
                    {
                        sqlwhere = sqlwhere + "and filldate ='"+date_ID.Value+"'";
                    }
                    if (txt_ID.Text != "")
                    {
                        sqlwhere = sqlwhere + "and ID >= '" + txt_ID.Text + "'";
                    }
                    if (txt_ID1.Text != "")
                    {
                        sqlwhere = sqlwhere + "and ID <= '" + txt_ID1.Text + "'";
                    }
                    if (txt_NAME.Text != "")
                    {
                        sqlwhere = sqlwhere + "and MNAME LIKE '%" + txt_NAME.Text + "%'";
                    }
                    if (txt_GG.Text != "")
                    {
                        sqlwhere = sqlwhere + "and GUIGE LIKE '%" + txt_GG.Text + "%'";
                    }
                    if (txt_ZJM.Text != "")
                    {
                        sqlwhere = sqlwhere + "and HMCODE LIKE '%" + txt_ZJM.Text + "%'";
                    }
                    if (txt_CZ.Text != "")
                    {
                        sqlwhere = sqlwhere + "and CAIZHI LIKE '%" + txt_CZ.Text + "%'";
                    }
                    if (txt_GB.Text != "")
                    {
                        sqlwhere = sqlwhere + "and GB LIKE '%" + txt_GB.Text + "%'";
                    }
                    if (txt_px.SelectedValue.ToString().Trim() =="1") //降序
                    {
                        if (txt_pxl.SelectedValue.ToString() == "0")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "ID",1, sqlwhere);
                        }

                        if (txt_pxl.SelectedValue.ToString() == "1")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "ID",1, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "2")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "MNAME",1, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "3")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "MENGNAME",1, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "4")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "HMCODE",1, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "5")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "GUIGE",1, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "6")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "CAIZHI",1, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "7")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "GB",1, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "8")
                        {
                            sql = "*";
                            InitPager("VIEW_MATERIAL", "ID", sql, "FILLDATE", 1, sqlwhere);
                        }
                    }
                     else //升序
                    {
                        if (txt_pxl.SelectedValue.ToString() == "0")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "ID", 0, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "1")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "ID",0, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "2")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "MNAME",0, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "3")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "MENGNAME",0, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "4")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "HMCODE",0, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "5")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "GUIGE",0, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "6")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "CAIZHI",0, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "7")
                        {
                            sql = " * ";
                            InitPager("VIEW_MATERIAL", "ID", sql, "GB",0, sqlwhere);
                        }
                        if (txt_pxl.SelectedValue.ToString() == "8")
                        {
                            sql = "*";
                            InitPager("VIEW_MATERIAL", "ID", sql, "FILLDATE", 0, sqlwhere);
                        }
                }

                pager.PageIndex = UCPaging1.CurrentPage;
                DataTable tb = CommonFun.GetDataByPagerQueryParam(pager);
                CommonFun.Paging(tb, Repeater1, UCPaging1, NoDataPanel);
                if (NoDataPanel.Visible)
                {
                    UCPaging1.Visible = false;
                }
                else
                {
                    UCPaging1.Visible = true;
                    UCPaging1.InitPageInfo();
                    CheckUser(ControlFinder);
                }
        }
        protected void rblZT_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetData();
        }

        //分类物料大类查询
        protected void DDLclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            getClassTwo();
            GetData();
        }

        //分物料种类查询
        protected void DDLname_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetData();
        }

       //执行查找
        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            GetData();
        }

        protected void btn_clear_Click(object sender, EventArgs e)
        {
            txt_ID.Text = "";
            txt_ID1.Text = "";
            txt_ID2.Text = "";
            txt_NAME.Text = "";
            txt_GG.Text = "";
            txt_ZJM.Text = "";
            txt_CZ.Text = "";
            txt_GB.Text = "";
            date_ID.Value = "";
            ddlWFName.SelectedIndex = 0;
            txt_px.SelectedIndex = 0;
            txt_pxl.SelectedIndex = 0;
            btn_confirm_Click(null,null);
        }
        
        //删除记录,建议在使用时隐藏改功能
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            System.Data.SqlClient.SqlConnection sqlConn = new SqlConnection();
            string strID = "";
            foreach (RepeaterItem labID in Repeater1.Items)
            {
                CheckBox chk = (CheckBox)labID.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    strID += "'" + ((Label)labID.FindControl("labID")).Text + "',";
                }
            }
            if (strID.Length > 1)
            {
                strID = strID.Substring(0, strID.Length - 1);
                sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");

                System.Data.SqlClient.SqlCommand sqlCmd = new System.Data.SqlClient.SqlCommand("DELETE FROM TBMA_MATERIAL WHERE ID IN (" + strID + ")", sqlConn);
                sqlConn.Open();
                SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
                dr.Close();
            }
            GetData();
        }

        protected string convertStatus(string status)
        {
            if (status == "1")
                return "在用";
            else
                return "停用";
        }
        /// <summary>
        /// 绑定维护人姓名
        /// </summary>
        protected void BindWFName()
        {
            string sqltext = "select distinct ST_NAME collate  Chinese_PRC_CS_AS_KS_WS AS ST_NAME,CLERK FROM VIEW_MATERIAL where ST_NAME IS NOT NULL order by ST_NAME collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "ST_NAME";
            string dataValue = "CLERK";
            DBCallCommon.BindDdl(ddlWFName, sqltext, dataText, dataValue);
            
        }

        protected string editWl(string WlId)
        {
            return "javascript:void window.open('WL_Material_Edit.aspx?id=" + WlId + "','修改物料信息','')";
        }
        protected void btn_search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.BindWFName();
            this.getClassOne();
            this.getClassTwo();
            this.InitVar();
            this.GetData();
        }
        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            string sqlwhere = "";
            sqlwhere += " STATE='" + rblZT.SelectedValue + "'";
            if (DDLclass.SelectedValue != "")
            {
                sqlwhere += " and TYPEID like '" + DDLclass.SelectedValue + ".%'";
            }
            if (DDLname.SelectedValue != "")
            {
                sqlwhere += " AND TYPEID='" + DDLname.SelectedValue + "'";
            }
            if (ddlWFName.SelectedValue != "-请选择-")
            {
                sqlwhere += " AND CLERK='" + ddlWFName.SelectedValue + "'";
            }
            if (date_ID.Value != "")
            {
                sqlwhere = sqlwhere + "and filldate ='" + date_ID.Value + "'";
            }
            if (txt_ID2.Text != "")
            {
                sqlwhere = sqlwhere + "and ID like '%" + txt_ID2.Text + "%'";
            }
            if (txt_ID.Text != "")
            {
                sqlwhere = sqlwhere + "and ID >= '" + txt_ID.Text + "'";
            }
            if (txt_ID1.Text != "")
            {
                sqlwhere = sqlwhere + "and ID <= '" + txt_ID1.Text + "'";
            }
            if (txt_NAME.Text != "")
            {
                sqlwhere = sqlwhere + "and MNAME LIKE '%" + txt_NAME.Text + "%'";
            }
            if (txt_GG.Text != "")
            {
                sqlwhere = sqlwhere + "and GUIGE LIKE '%" + txt_GG.Text + "%'";
            }
            if (txt_ZJM.Text != "")
            {
                sqlwhere = sqlwhere + "and HMCODE LIKE '%" + txt_ZJM.Text + "%'";
            }
            if (txt_CZ.Text != "")
            {
                sqlwhere = sqlwhere + "and CAIZHI LIKE '%" + txt_CZ.Text + "%'";
            }
            if (txt_GB.Text != "")
            {
                sqlwhere = sqlwhere + "and GB LIKE '%" + txt_GB.Text + "%'";
            }
            string sqltext = "select ID,MNAME,MENGNAME,HMCODE,GUIGE,CAIZHI,GB,MWEIGHT,MAREA,TECHUNIT,CONVERTRATE,PURCUNIT,FUZHUUNIT,ST_NAME,FILLDATE from View_Material where " + sqlwhere + "";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 50000)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数量大于50000，请选择类别后或者筛选后再导出');", true); return;
            }
            else
            {
                TM_Data.ExportTMDataFromDB.ExportMaterial(sqltext);
            }
        }
    }

}
