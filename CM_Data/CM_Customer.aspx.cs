using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_Customer : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        string dep = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (Request.QueryString["dep"] != null)
            {
                dep = Request.QueryString["dep"];
            }
            if (!IsPostBack)
            {
                if (dep == "03")
                {
                    btnIn.Visible = false;
                    hpTask.Visible = false;
                    lbshow.Text = "是否质检";
                    sf_getin.Visible = false;
                    sf_zj.Visible = true;

                }
                doPost(sf_getin);
                doPost(sf_zj);
                GetDepartment();
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
            pager.TableName = "TBCM_CUSTOMER as a left join TBDS_STAFFINFO as b on a.CM_MANCLERK=b.ST_ID left join TBDS_STAFFINFO as c on a.CM_INKEEP=c.ST_ID";
            pager.PrimaryKey = "CM_ID";
            pager.ShowFields = "a.*,b.ST_NAME,(CASE WHEN a.CM_INOROUT='0' THEN '厂内' WHEN a.CM_INOROUT='1' THEN '厂外' END) as INOROUT,c.ST_NAME as KEEPER";
            pager.OrderField = "CM_ID";
            pager.StrWhere = ConStr();
            pager.OrderType = 1;
            pager.PageSize = 20;
            UCPaging1.PageSize = pager.PageSize;
        }

        private string ConStr()
        {
            string StrWhere = "";
            string ColName = ddlBz.SelectedValue;
            if (txtBox.Text != "")
            {
                if (ddl_place.SelectedValue == "00")
                {
                    StrWhere = string.Format("{0} like '%{1}%' AND ", ColName, txtBox.Text);
                }
                else
                {
                    StrWhere = string.Format("{0} like '%{1}%' AND CM_PLACE='{2}' AND ", ColName, txtBox.Text, ddl_place.SelectedItem.Text);
                }
            }
            else
            {
                if (ddl_place.SelectedValue != "00")
                {
                    StrWhere = string.Format("CM_PLACE='{0}' AND ", ddl_place.SelectedItem.Text);
                }
            }
            if (ddl_inout.SelectedValue != "a")
            {
                StrWhere += "CM_INOROUT='" + ddl_inout.SelectedValue + "' AND ";
            }
            switch (sf_getin.SelectedValue)
            {
                case "0":
                    StrWhere += "CM_BTIN='0'";
                    break;
                case "1":
                    StrWhere += "CM_BTIN='1'";
                    break;
                case "2":
                    StrWhere += "CM_BTOUT='1'";
                    break;
                default:
                    StrWhere += "1=1";
                    break;
            }
            switch (sf_zj.SelectedValue)
            {
                case "0":
                    StrWhere += " and CM_CHECK is null or CM_CHECK=''";
                    break;
                case "1":
                    StrWhere += " and (CM_CHECK='0' or CM_CHECK='1')";
                    break;
                case "2":
                    StrWhere += " and (CM_CHECK is null or CM_CHECK='') and CM_ZJYUAN='" + Session["UserID"].ToString() + "'";
                    break;
                default:
                    break;
            }
            return StrWhere;
        }

        private void GetDepartment()//绑定部门
        {
            string sqlText = "select distinct CM_PLACE from TBCM_CUSTOMER";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddl_place.DataSource = dt;
            ddl_place.DataTextField = "CM_PLACE";
            ddl_place.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddl_place.Items.Insert(0, item);
            ddl_place.SelectedValue = "00";
        }

        protected void btn_del_Click(object sender, EventArgs e)
        {
            string strId = "";
            foreach (RepeaterItem rptItem in Repeater1.Items)
            {
                CheckBox chk = (CheckBox)rptItem.FindControl("chk");
                if (chk.Checked)
                {
                    strId += "'" + ((Label)rptItem.FindControl("CM_ID")).Text + "'" + ",";
                }
            }
            strId = strId.Substring(0, strId.Length - 1);
            string sqlTxt = "delete from TBCM_CUSTOMER where CM_ID in (" + strId + ")";
            DBCallCommon.ExeSqlText(sqlTxt);
            bindGrid();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
            ScriptManager.RegisterStartupScript(this.Page, GetType(), DateTime.Now.ToString("ssffff"), "sTable()", true);
        }

        protected void ddl_place_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindGrid();
            ScriptManager.RegisterStartupScript(this.Page, GetType(), DateTime.Now.ToString("ssffff"), "sTable()", true);
        }

        protected void Show_Click(object sender, EventArgs e)
        {
            Response.Redirect("CM_Customer.aspx");
        }

        protected string ShowOut(string id)
        {
            return "javascript:window.showModalDialog('CM_EditOut.aspx?CM_ID=" + id + "','','dialogWidth=700px;dialogHeight=300px')";
        }

        protected string Edit(string id)
        {
            return "javascript:window.showModalDialog('CM_CustomerEdit.aspx?CM_ID=" + id + "','','dialogWidth=700px;dialogHeight=350px')";
        }

        List<string> str = new List<string>();
        protected void Repeater1_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType.ToString() != "Header")
            {
                if (dep == "03")
                {
                    e.Item.FindControl("Edit").Visible = false;
                    e.Item.FindControl("Image2").Visible = false;
                }
                else
                {
                    if (((DataRowView)e.Item.DataItem).Row["CM_BTIN"].ToString() == "0")
                    {
                        e.Item.FindControl("EditOut").Visible = false;//未入库，不得出库
                    }
                    if (((DataRowView)e.Item.DataItem).Row["CM_BTIN"].ToString() == "1")
                    {
                        ((DataRowView)e.Item.DataItem).Row["CM_BTIN"] = "yello";
                        e.Item.FindControl("Edit").Visible = false;
                        e.Item.FindControl("Image2").Visible = false;
                    }
                    if (((DataRowView)e.Item.DataItem).Row["CM_BTOUT"].ToString() == "1")
                    {
                        ((DataRowView)e.Item.DataItem).Row["CM_BTOUT"] = "red";
                        //e.Item.FindControl("EditOut").Visible = false;
                        e.Item.FindControl("Edit").Visible = false;
                        //e.Item.FindControl("Image1").Visible = false;
                        e.Item.FindControl("Image2").Visible = false;
                    }
                    if (e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "white";
                    }
                    else
                    {
                        ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "#EFF3FB";
                    }
                    //相同合同号只显示一条的信息，下面的为空
                    //string contr = ((DataRowView)e.Item.DataItem).Row["CM_CONTR"].ToString();
                    //if (str.Count < 1)
                    //{
                    //    str.Add(contr);
                    //}
                    //else
                    //{
                    //    if (str.Contains(contr))
                    //    {
                    //        ((DataRowView)e.Item.DataItem).Row["CM_PJNAME"] = "";
                    //        ((DataRowView)e.Item.DataItem).Row["INOROUT"] = "";
                    //        ((DataRowView)e.Item.DataItem).Row["CM_CONTR"] = "";
                    //        ((DataRowView)e.Item.DataItem).Row["CM_COSTERM"] = "";
                    //        ((DataRowView)e.Item.DataItem).Row["CM_INKEEP"] = "";
                    //    }
                    //    else
                    //    {
                    //        str.Add(contr);
                    //    }
                    //}
                    e.Item.DataBind();
                }
            }
        }

        protected void doPost(RadioButtonList rbList)
        {
            foreach (ListItem item in rbList.Items)
            {
                //为预设项添加doPostBack JS  
                if (item.Selected)
                {
                    item.Attributes.Add("onclick", String.Format("javascript:setTimeout('__doPostBack(\\'{0}${1}\\',\\'\\')', 0)", rbList.UniqueID, rbList.Items.IndexOf(item)));
                }
            }
        }

        protected void btnIn_Click(object sender, EventArgs e)
        {
            List<string> list = new List<string>();
            List<string> sql = new List<string>();
            foreach (RepeaterItem rptItem in Repeater1.Items)
            {
                CheckBox chk = (CheckBox)rptItem.FindControl("chk");
                if (chk.Checked)
                {
                    list.Add(((Label)rptItem.FindControl("CM_ID")).Text);
                    string sql1 = "select * from TBCM_CUSTOMER where CM_ID='" + ((Label)rptItem.FindControl("CM_ID")).Text + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql1);
                    DataRow dr = dt.Rows[0];
                    DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID("174"), new List<string>(), new List<string>(), "顾客财产已入库", "项目为" + dr["CM_PJNAME"].ToString() + "请登录系统进行查看。");//刘学的ST_ID
                }
            }
            for (int i = 0; i < list.Count; i++)
            {
                string sqlTxt = "update TBCM_CUSTOMER set CM_BTIN='1' where CM_ID='" + list[i] + "'";
                sql.Add(sqlTxt);
            }
            DBCallCommon.ExecuteTrans(sql);
            bindGrid();
            ScriptManager.RegisterStartupScript(this.Page, GetType(), DateTime.Now.ToString("ssffff"), "sTable()", true);

        }

        #region 导出功能

        protected void btnExport_Click(object sender, EventArgs e) //导出
        {
            string sqltext = "select a.CM_PJNAME,(CASE WHEN a.CM_INOROUT='0' THEN '厂内' WHEN a.CM_INOROUT='1' THEN '厂外' END) as INOROUT,a.CM_CONTR,a.CM_COSTERM,a.CM_EQUIP,a.CM_PIC,a.CM_NUM,a.CM_PLACE,a.CM_APPNAME,a.CM_NOTE,c.ST_NAME as KEEPER,a.CM_INDATE,a.CM_OUTNUM,a.CM_OUT,a.CM_OUTDATE,b.ST_NAME,(CASE WHEN a.CM_CHECK='1' THEN '合格' WHEN a.CM_CHECK='0' THEN '不合格' ELSE '未质检' END) as CM_CHECK,(CASE WHEN a.CM_SFHG='1' THEN '合格' WHEN a.CM_SFHG='0' THEN '不合格' ELSE '' END) as CM_SFHG,(CASE WHEN a.CM_BTIN='1' THEN '入' ELSE '' END) as CM_BTIN,(CASE WHEN a.CM_BTOUT='1' THEN '出'   ELSE '' END) as CM_BTOUT  from TBCM_CUSTOMER as a left join TBDS_STAFFINFO as b on a.CM_MANCLERK=b.ST_ID left join TBDS_STAFFINFO as c on a.CM_INKEEP=c.ST_ID  where " + ConStr() + "order by CM_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }

        private void ExportDataItem(System.Data.DataTable dt)
        {
            string filename = "顾客财产台账表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("顾客财产台账表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion
    }
}
