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
using System.IO;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_ServiceFile : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                UserID.Value = Session["UserID"].ToString();
                bindGrid();
            }
            CheckUser(ControlFinder);
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            InitPager();
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

        private void InitPager()
        {
            pager.TableName = "TBCM_RECORD as a left join TBDS_STAFFINFO as c on a.CM_MANCLERK=c.ST_ID";
            pager.PrimaryKey = "a.CM_ID";
            pager.ShowFields = "a.*,c.ST_NAME";
            pager.OrderField = "a.CM_ID";
            pager.StrWhere = ConWhere();
            pager.OrderType = 0;
            pager.PageSize = 10;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string ConWhere()
        {
            string StrWhere = string.Format("{0} like '%{1}%'", ddlBz.SelectedValue, txtBox.Text);
            return StrWhere;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
        }

        protected void btn_del_Click(object sender, EventArgs e)
        {
            string strId = "";
            string context = "";
            List<string> list = new List<string>() { };
            foreach (RepeaterItem rptItem in Repeater1.Items)
            {
                CheckBox chk = (CheckBox)rptItem.FindControl("chk");
                if (chk.Checked)
                {
                    strId += "'" + ((Label)rptItem.FindControl("CM_ID")).Text + "'" + ",";
                    context += "'" + ((Label)rptItem.FindControl("CM_CONTEXT")).Text + "'" + ",";
                }
            }
            strId = strId.Substring(0, strId.Length - 1);
            context = context.Substring(0, context.Length - 1);
            #region 删除文件
            string sqlfile = "select fileload,fileName from tb_files where BC_CONTEXT in (" + context + ")";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlfile);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string strFilePath = dt.Rows[i]["fileload"].ToString() + @"\" + dt.Rows[i]["fileName"].ToString();
                    if (Directory.Exists(strFilePath))
                    {
                        File.Delete(strFilePath);
                    }
                }
            }
            #endregion

            #region 删除数据库
            string sqlTxt = "delete from TBCM_RECORD where CM_ID in (" + strId + ")";
            list.Add(sqlTxt);
            string sqlTxt1 = "delete from tb_files where BC_CONTEXT in (" + context + ")";
            list.Add(sqlTxt1);
            DBCallCommon.ExecuteTrans(list);
            #endregion
            bindGrid();
        }

        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            string person = UserID.Value;
            if (e.Item.ItemType.ToString() != "Header")
            {
                DataTable dt = GetPerson((DataRowView)e.Item.DataItem);
                DataRow dr = dt.Rows[0];
                if (dr[0].ToString() == person)
                {
                    e.Item.FindControl("hyperlink1").Visible = true;
                }
            }
        }

        private DataTable GetPerson(DataRowView drv)
        {
            string id = drv.Row["CM_ID"].ToString();
            string sql = "select CM_MANCLERK from TBCM_RECORD where CM_ID='" + id + "'";
            DataTable table = DBCallCommon.GetDTUsingSqlText(sql);
            return table;
        }
    }
}
