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
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MarReplaceBulk : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ///////DBCallCommon.SessionLostToLogIn(ViewState["UserID"]);
            if (!IsPostBack)
            {
                this.PageInit();
            }
        }
        /// <summary>
        /// 页面信息初始化
        /// </summary>
        protected void PageInit()
        {
            ViewState["TaskID"] =Request.QueryString["TaskID"].ToString();
            string sql = "select  TSA_ID, TSA_PJID, TSA_ENGNAME,CM_PROJ  from View_TM_TaskAssign where TSA_ID='" + ViewState["TaskID"].ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
               // lblProjName.Text = dt.Rows[0]["TSA_PJNAME"].ToString() + "(" + dt.Rows[0]["TSA_PJID"].ToString() + ")";
                lblEngName.Text = dt.Rows[0]["TSA_ENGNAME"].ToString() + "_" + dt.Rows[0]["TSA_ID"].ToString();
                lblContratId.Text = dt.Rows[0]["TSA_PJID"].ToString();
                lblProjName.Text = dt.Rows[0]["CM_PROJ"].ToString();


            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
      
            string strWhere = " BM_ENGID='" + ViewState["TaskID"].ToString() + "' AND BM_MARID!='' AND BM_MSSTATE='0' AND BM_MPSTATE='0' AND BM_OSSTATE='0' ";

            string sql = "select DISTINCT [BM_MARID],[BM_MANAME],[BM_MAGUIGE],[BM_MAQUALITY],[BM_STANDARD],[BM_MASHAPE],[BM_MASTATE] from View_TM_DQO where ";
            if (txtGuiGe.Text.Trim() != "")
            {
                strWhere += " AND [BM_MAGUIGE] like '%" + txtGuiGe.Text.Trim() + "%'";
            }

            if (txtMarCZ.Text.Trim() != "")
            {
                strWhere += " AND [BM_MAQUALITY] like '%" + txtMarCZ.Text.Trim() + "%'";
            }

            if (txtMarid.Text.Trim() != "")
            {
                strWhere += " AND [BM_MARID] like '%" + txtMarid.Text.Trim() + "%'";
            }

            if (txtMarName.Text.Trim() != "")
            {
                strWhere += " AND [BM_MANAME] like '%" + txtMarName.Text.Trim() + "%'";
            }
           
            if (txtZongXu.Text.Trim() != "")
            {
                strWhere += " AND [BM_ZONGXU]='" + txtZongXu.Text.Trim() + "' or [BM_ZONGXU] like '" + txtZongXu.Text.Trim() + ".%'";
            }
           

            udqOrg.ExistedConditions = strWhere;
            strWhere += UserDefinedQueryConditions.ReturnQueryString((GridView)udqOrg.FindControl("GridView1"), (Label)udqOrg.FindControl("Label1"));

            sql += strWhere+" ORDER BY BM_MARID";

            ViewState["strWhere"] = strWhere.Replace('\'','^');

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView2.DataSource = dt;
            GridView2.DataBind();
            if (GridView2.Rows.Count > 0)
            {
                NoDataPanel1.Visible = false;
            }
            else
            {
                NoDataPanel1.Visible = true;
            }
        }

         /// <summary>
        /// 清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
            NoDataPanel1.Visible = true;

            txtGuiGe.Text = "";
            txtMarCZ.Text = "";
            txtMarid.Text = "";
            txtMarName.Text = "";
            
            txtZongXu.Text = "";
           
        }
        protected void btnClearUser_OnClick(object sender, EventArgs e)
        {
            UserDefinedQueryConditions.UserDefinedExternalCallForInitControl((GridView)udqOrg.FindControl("GridView1"));
        }
        /// <summary>
        /// GirdView行绑定时，查看明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["style"] = "Cursor:hand";

                e.Row.Attributes.Add("title", "双击查看BOM中可替换与不可替换数据明细");

                string marid=e.Row.Cells[1].Text.Trim();
                e.Row.Attributes.Add("ondblclick", "ShowReplaceDetail('" + hdfEngid.Value.Trim() + "','" + marid + "')");
            }
        }
        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReplace_OnClick(object sender, EventArgs e)
        {
            string oldmarshape = "";
            string newmarshape = "";
            string oldmarstate = "";
            string newmarstate = "";
            string marid="";
            string oldmarid = "";
            double xishu = 1;

            List<String> list_insert = new List<string>();
            string sql_insert = "DELETE FROM [dbo].[TBPM_TEMPMARREPLACE] WHERE TaskID='"+hdfEngid.Value+"'";
            list_insert.Add(sql_insert);

            foreach(GridViewRow grow in GridView2.Rows)
            {
                marid = ((TextBox)grow.FindControl("txtNewMarid")).Text.Trim();
                oldmarid = ((TableCell)grow.Cells[1]).Text.Trim() == "&nbsp;" ? "" : ((TableCell)grow.Cells[1]).Text.Trim();
                if (marid == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【"+grow.RowIndex.ToString()+"】行请输入【物料编码】!!!');", true);
                    return;
                }

                oldmarshape = ((TableCell)grow.Cells[6]).Text.Trim()=="&nbsp;"?"":((TableCell)grow.Cells[6]).Text.Trim();
                oldmarstate = ((TableCell)grow.Cells[7]).Text.Trim() == "&nbsp;" ? "" : ((TableCell)grow.Cells[7]).Text.Trim();

                newmarshape = ((TextBox)grow.FindControl("txtNewMarShape")).Text.Trim();
                newmarstate = ((TextBox)grow.FindControl("txtNewMarState")).Text.Trim();

                xishu = Convert.ToDouble(((TextBox)grow.FindControl("txtMarNewXishu")).Text.Trim());

                if (newmarshape == ""||oldmarshape=="")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【" + grow.RowIndex.ToString() + "】行请输入【毛坯形状】!!!');", true);
                    return;
                }

                if (oldmarshape != newmarshape)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法替换!!!\\r\\r提示:第【" + grow.RowIndex.ToString() + "】行替换前【毛坯形状:"+oldmarshape+"】与替换后【毛坯形状"+newmarshape+"】不同!!!');", true);
                    return;
                }

                if(newmarshape=="采"&&newmarstate=="")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('第【" + grow.RowIndex.ToString() + "】行【毛坯形状:" + newmarshape + "】,必须输入【毛坯状态】!!!');", true);
                    return;
                }

                sql_insert = "INSERT INTO [dbo].[TBPM_TEMPMARREPLACE]([TaskID],[OldMarid],[NewMarid],[NewMarShpae],[NewMarState],[ReplaceCeft],[StrWhere]) VALUES('" + hdfEngid.Value + "','" + oldmarid + "','" + marid + "','" + newmarshape + "','" + newmarstate + "','" + xishu + "','" + ViewState["strWhere"] .ToString()+ "')";

                list_insert.Add(sql_insert);
            }

            list_insert.Add("exec PRO_TM_MarReplace '"+hdfEngid.Value+"'");

            try
            {
                DBCallCommon.ExecuteTrans(list_insert);
                this.btnQuery_OnClick(null, null);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('替换成功!!!');", true);
            }
            catch(Exception)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('程序出错，请与管理员联系!!!');", true);
                return;
            }
        }
    }
}
