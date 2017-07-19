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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Md_Detail_Search_1 : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        string engid;
        string sqltext;
        protected void Page_Load(object sender, EventArgs e)
        {
            engid = Request.QueryString["engid"];
            InitVarPager(engid);
            InitVar();
            if (!IsPostBack)
            {
                Delt.Click += new EventHandler(Delt_Click);
                Delt.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
                UCPaging1.CurrentPage = 1;
                GetManutAssignData(); //数据绑定
            }
        }

        private void Pager_PageChanged(int pageNumber)
        {
            InitPager();
            GetManutAssignData();
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
                //绑定负责人
                DropDownList ddldiaoduyuan = (DropDownList)gr.FindControl("ddl_fuzeren");

                ddldiaoduyuan.Items.Clear();
                ddldiaoduyuan.Items.Add(new ListItem("--请选择--", ""));
                string sqltxt = "select ST_CODE,ST_NAME from TBDS_STAFFINFO where ST_POSITION='采购员'";
                DataSet ds = DBCallCommon.FillDataSet(sqltxt);
                foreach (DataRow dRow in ds.Tables[0].Rows)
                {
                    ListItem listitem = new ListItem(dRow["ST_NAME"].ToString(), dRow["ST_CODE"].ToString());
                    ddldiaoduyuan.Items.Add(listitem);
                }
            }
        }
        private void GetManutAssignData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
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

        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }

        private void InitPager()
        {
            pager.TableName = ViewState["tcmxtable"].ToString();
            pager.PrimaryKey = "MS_ID";
            pager.ShowFields = "MS_ID,MS_NEWINDEX,MS_NAME,MS_MARID,MS_KEYCOMS,MS_PDSTATE,MS_CHGPID,MS_GUIGE";
            pager.OrderField = "dbo.f_formatstr(MS_NEWINDEX, '.')";
            pager.OrderType = 0;//按任务名称升序排列
            pager.PageSize = 15;
            pager.StrWhere = "MS_PDSTATE='3'and dbo.GetNoEngid(MS_ENGID,'-')='" + engid + "' ";
        }

       
       protected void InitVarPager(string str)
        {
            string sqlselect = "select TSA_ID,TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE from View_TM_TaskAssign where TSA_ID='" + engid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlselect);
            if (dt.Rows.Count > 0)
            {
                tsa_id.Text = dt.Rows[0]["TSA_ID"].ToString();
                lab_proname.Text = dt.Rows[0]["TSA_PJNAME"].ToString();
                lab_engname.Text = dt.Rows[0]["TSA_ENGNAME"].ToString();
                eng_type.Value = dt.Rows[0]["TSA_ENGSTRTYPE"].ToString();
                //bomtime.Text = dt.Rows[0]["MS_ADATE"].ToString();
                switch (eng_type.Value)
                {
                    case "回转窑":
                        ViewState["tcmxtable"] = "View_TM_MSOFHZY";
                        ViewState["tctable"] = "TBPM_MSOFHZY";
                        break;
                    case "球、立磨":
                        ViewState["tcmxtable"] = "View_TM_MSOFQLM";
                        ViewState["tctable"] = "TBPM_MSOFQLM";
                        break;
                    case "篦冷机":
                        ViewState["tcmxtable"] = "View_TM_MSOFBLJ";
                        ViewState["tctable"] = "TBPM_MSOFBLJ";
                        break;
                    case "堆取料机":
                        ViewState["tcmxtable"] = "View_TM_MSOFDQJ";
                        ViewState["tctable"] = "TBPM_MSOFDQJ";
                        break;
                    case "钢结构及非标":
                        ViewState["tcmxtable"] = "View_TM_MSOFGFB";
                        ViewState["tctable"] = "TBPM_MSOFGFB";
                        break;
                    case "电气及其他":
                        ViewState["tcmxtable"] = "View_TM_MSOFDQO";
                        ViewState["tctable"] = "TBPM_MSOFDQO";
                        break;
                    default:
                        break;
                }
            }
        }

       protected void btn_save_Click(object sender, EventArgs e)
       {
           string sqltext = "";
           string fzrid;
           string fzrname;
           string bzname;
           string pstime;
           string pftime;
           List<string> list_sql = new List<string>();
           int n = 0;
           foreach (GridViewRow gr in GridView1.Rows)
           {
               Label lblzhujian = (Label)gr.FindControl("lblmsid");
               Label lblmarid = (Label)gr.FindControl("lblmarid");
               Label lblzzindex = (Label)gr.FindControl("lblzzindex");

               DropDownList fzrddy = (DropDownList)gr.FindControl("ddl_fuzeren");
               if (fzrddy.SelectedIndex == 0 || fzrddy.SelectedIndex == -1)
               {
                   fzrid = "";
                   fzrname = "";
               }
               else
               {
                   fzrid = fzrddy.SelectedValue;
                   fzrname = fzrddy.SelectedItem.Text;
               }
               bzname = ((HtmlInputText)gr.FindControl("ttbz")).Value.Trim();
               pstime = ((HtmlInputText)gr.FindControl("ttpstime")).Value.Trim();
               pftime = ((HtmlInputText)gr.FindControl("ttpftime")).Value.Trim();
               n++;
               string nid = System.DateTime.Now.ToString("yyyyMMddHHmmss") + Session["UserID"] + n.ToString();
               sqltext = "exec FJNodeAdjust '" + ViewState["tcmxtable"] + "','" + engid + "','" + lblzzindex.Text + "','" + lblzhujian.Text + "','" + lblmarid.Text.ToString() + "','" + nid + "',";
               sqltext += " '" + fzrid + "','" + fzrname + "','" + bzname + "','" + pstime + "','" + pftime + "'";
               list_sql.Add(sqltext);

               //插入的任务使父级任务的时间发生改变
               sqltext = "exec PM_MaxTimeToFather '" + engid + "','" + lblzzindex.Text + "'";
               list_sql.Add(sqltext);

               sqltext = "update " + ViewState["tctable"] + " set MS_PDSTATE='1' where MS_ID='" + lblzhujian.Text + "'";
               list_sql.Add(sqltext);
           }
           //string datetime = DateTime.Now.ToString("yyyy-MM-dd");
           //sqltext = "update TBMP_MANUTSASSGN set MTA_MAINDATE='" + datetime + "',MTA_BOMDATE='" + bomtime.Text.Substring(0, 10) + "',MTA_BOMSTATE='0' where MTA_ID='" + tsa_id.Text + "' and MTA_MAINDATE is null";
           //list_sql.Add(sqltext);
           DBCallCommon.ExecuteTrans(list_sql);
           Response.Redirect("TM_Md_Detail_Search_1.aspx?engid=" + engid);
       }

       protected void Delt_Click(object sender, EventArgs e)
       {
           string strID = "";
           int n = 0;
           List<string> list_sql = new List<string>();
           foreach (GridViewRow gr in GridView1.Rows)
           {
               CheckBox cb = (CheckBox)gr.FindControl("chtask");
               if (cb.Checked)
               {
                   n++;
                   strID = ((Label)gr.FindControl("lblmsid")).Text;
                   sqltext = "update " + ViewState["tctable"] + " set MS_PDSTATE='0' where MS_ID='" + strID + "'";
                   list_sql.Add(sqltext);
               }
           }
           if (n == 0)
           {
               Response.Write("alert('请勾选需要删除的行！')");
           }
           else
           {
               DBCallCommon.ExecuteTrans(list_sql);
               Response.Redirect("TM_Md_Detail_Search_1.aspx?engid=" + engid);
           }
       }
       protected void Reset_Click(object sender, EventArgs e)
       {
           Response.Redirect("TM_Md_Detail_Search.aspx");
       }
        
    }

}
