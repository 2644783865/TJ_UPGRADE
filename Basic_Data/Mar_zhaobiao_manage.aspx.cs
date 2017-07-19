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
using ZCZJ_DPF;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class Mar_zhaobiao_manage : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            InitVar();
            if (!IsPostBack)
            {
                bindGrid();
                this.btn_search_Click(null, null);
            }
            CheckUser(ControlFinder);
        }

        private void InitVar()
        {
            delete.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数      
        }

        //初始化分布信息
        private void InitPager()
        {
            pager.TableName = "View_DB_Marzhaobiao";
            pager.PrimaryKey = "IB_ID";
            //在pager.ShowFields中加一个ENG_PJID
            pager.ShowFields = "IB_ID,IB_MARID,MNAME,CS_NAME,IB_PRICE,IB_LENGTH,IB_WIDTH,substring(IB_DATE,1,10) as IB_DATE,substring(IB_FIDATE,1,10) as IB_FIDATE,IB_NOTE,CAIZHI,GUIGE,GB";
            pager.OrderField = "IB_MARID";
            pager.StrWhere = "IB_STATE='" + rblZT.SelectedValue + "' ";
            pager.OrderType = 1;//按时间降序排列
            pager.PageSize = Convert.ToInt16(ddl_pageno.SelectedValue);
        }

        void Pager_PageChanged(int pageNumber)
        {
            pager.TableName = "View_DB_Marzhaobiao";
            pager.PrimaryKey = "IB_ID";
            pager.ShowFields = "IB_ID,IB_MARID,MNAME,CS_NAME,IB_PRICE,IB_LENGTH,IB_WIDTH,substring(IB_DATE,1,10) as IB_DATE,substring(IB_FIDATE,1,10) as IB_FIDATE,IB_NOTE,CAIZHI,GUIGE,GB";
            pager.OrderField = "IB_MARID";
            string sqlwhere = "IB_STATE='" + rblZT.SelectedValue+ "' ";
            if (txt_ID.Text != "")
            {
                sqlwhere = sqlwhere + " and IB_MARID LIKE '%" + txt_ID.Text + "%'";
            }
            if (txt_NAME.Text != "")
            {
                sqlwhere = sqlwhere + " and MNAME LIKE '%" + txt_NAME.Text + "%'";
            }
            if (txt_GG.Text != "")
            {
                sqlwhere = sqlwhere + " and GUIGE LIKE '%" + txt_GG.Text + "%'";
            }
            if (txt_ZJM.Text != "")
            {
                sqlwhere = sqlwhere + " and CAIZHI LIKE '%" + txt_ZJM.Text + "%'";
            }
            if (txt_CZ.Text != "")
            {
                sqlwhere = sqlwhere + " and CS_NAME LIKE '%" + txt_CZ.Text + "%'";
            }
            if (txt_GB.Text != "")
            {
                sqlwhere = sqlwhere + " and IB_PRICE = '" + txt_GB.Text + "'";
            }            
            pager.StrWhere = sqlwhere;
            pager.OrderType = 1;//按时间降序排列
            pager.PageSize = Convert.ToInt16(ddl_pageno.SelectedValue);
            bindGrid();
        }

        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, Reproject1, UCPaging1, NoDataPanel);
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

        protected void delete_Click(object sender, EventArgs e)
        {
            List<string> mar_ID = new List<string>();
            string strID = "";

            foreach (RepeaterItem labID in Reproject1.Items)
            {
                CheckBox chk = (CheckBox)labID.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    //查找该CheckBox所对应纪录的id号,在labID中,按唯一ID号添加
                    strID = ((Label)labID.FindControl("IB_ID")).Text;
                    mar_ID.Add(strID);
                }
            }
            lbl_Info.Text += strID;
            foreach (string id in mar_ID)
            {
                string sql = "delete from TBPC_INVITEBID where IB_ID='" + id + "'";
                DBCallCommon.ExeSqlText(sql);
            }
            //防止刷新
            Response.Redirect("Mar_zhaobiao_manage.aspx?q=1");
        }


        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            pager.TableName = "View_DB_Marzhaobiao";
            pager.PrimaryKey = "IB_ID";
            pager.ShowFields = "IB_ID,IB_MARID,MNAME,CS_NAME,IB_PRICE,IB_LENGTH,IB_WIDTH,substring(IB_DATE,1,10) as IB_DATE,substring(IB_FIDATE,1,10) as IB_FIDATE,IB_NOTE,CAIZHI,GUIGE,GB";
            pager.OrderField = "IB_MARID";
            string sqlwhere = "1=1";
            if (txt_ID.Text != "")
            {
                sqlwhere = sqlwhere + " and IB_MARID LIKE '%" + txt_ID.Text + "%'";
            }
            if (txt_NAME.Text != "")
            {
                sqlwhere = sqlwhere + " and MNAME LIKE '%" + txt_NAME.Text + "%'";
            }
            if (txt_GG.Text != "")
            {
                sqlwhere = sqlwhere + " and GUIGE LIKE '%" + txt_GG.Text + "%'";
            }
            if (txt_ZJM.Text != "")
            {
                sqlwhere = sqlwhere + " and CAIZHI LIKE '%" + txt_ZJM.Text + "%'";
            }
            if (txt_CZ.Text != "")
            {
                sqlwhere = sqlwhere + " and CS_NAME LIKE '%" + txt_CZ.Text + "%'";
            }
            if (txt_GB.Text != "")
            {
                sqlwhere = sqlwhere + " and IB_PRICE = '" + txt_GB.Text + "'";
            }
            pager.StrWhere = sqlwhere;
            pager.OrderType = 1;//按时间降序排列
            pager.PageSize = Convert.ToInt16(ddl_pageno.SelectedValue);
            //pager.PageIndex = 1;
            bindGrid();
        }

        protected void btn_clear_Click(object sender, EventArgs e)
        {
            txt_ID.Text = "";
            txt_NAME.Text = "";
            txt_GG.Text = "";
            txt_ZJM.Text = "";
            txt_CZ.Text = "";
            txt_GB.Text = "";
            btn_confirm_Click(null, null);
        }
        protected string editZb(string ZbId,string Ibid)
        {
            return "javascript:void window.open('Mar_zhaobiao_manageDetail.aspx?action=update&id=" + ZbId + "&ibid=" + Ibid + " ','','')";
        }
        protected void btn_search_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
            btn_confirm_Click(null,null);
        }

        protected void rblZT_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }

    }

}
