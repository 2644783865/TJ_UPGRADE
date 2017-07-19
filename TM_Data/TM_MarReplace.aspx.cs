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
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MarReplace : System.Web.UI.Page
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
            ViewState["TaskID"] = Request.QueryString["TaskID"].ToString();
            string sql = "select  TSA_ID, TSA_PJID, TSA_ENGNAME, TSA_ENGSTRTYPE,TSA_PJNAME from View_TM_TaskAssign where TSA_ID='" + ViewState["TaskID"].ToString() + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lblProjName.Text = dt.Rows[0]["TSA_PJNAME"].ToString() + "(" + dt.Rows[0]["TSA_PJID"].ToString() + ")";
                lblEngName.Text = dt.Rows[0]["TSA_ENGNAME"].ToString() + "_" + dt.Rows[0]["TSA_ID"].ToString();
                hdfType.Value = dt.Rows[0]["TSA_ENGSTRTYPE"].ToString();
                hdfEngid.Value = ViewState["TaskID"].ToString();
                hdfProid.Value = dt.Rows[0]["TSA_PJID"].ToString();

                switch (dt.Rows[0]["TSA_ENGSTRTYPE"].ToString())
                {
                    case "回转窑":
                        ViewState["tablename"] = "TBPM_STRINFOHZY";
                        ViewState["viewtablename"] = "View_TM_HZY";
                        break;
                    case "球、立磨":
                        ViewState["tablename"] = "TBPM_STRINFOQLM";
                        ViewState["viewtablename"] = "View_TM_QLM";
                        break;
                    case "篦冷机":
                        ViewState["tablename"] = "TBPM_STRINFOBLJ";
                        ViewState["viewtablename"] = "View_TM_BLJ";
                        break;
                    case "堆取料机":
                        ViewState["tablename"] = "TBPM_STRINFODQLJ";
                        ViewState["viewtablename"] = "View_TM_DQLJ";
                        break;
                    case "钢结构及非标":
                        ViewState["tablename"] = "TBPM_STRINFOGFB";
                        ViewState["viewtablename"] = "View_TM_GFB";
                        break;
                    case "电气及其他":
                        ViewState["tablename"] = "TBPM_STRINFODQO";
                        ViewState["viewtablename"] = "View_TM_DQO";
                        break;
                    default: break;
                }
            }
        }
        /// <summary>
        /// 显示物料信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnViewMar_OnClick(object sender, EventArgs e)
        {
            if (txtMarOld.Text.Trim() != "" && txtMarNew.Text.Trim() != "")
            {

                string retvalue = this.BindOldNewMarDetail();
                if (retvalue == "OK")
                {
                    this.BindAllCanReplaceMar();
                    this.VisibleOfNoDataPanel();
                    this.TipforUnableReplace();
                }
                else if (retvalue == "MarOldError")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('替换前物料编码不存在或格式错误！！！');", true);/////window.location.reload();
                }
                else if (retvalue == "MarNewError")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('替换后的物料编码不存在、已停用或格式错误！！！');", true);/////window.location.reload();
                }
            }
            else
            {
                lblTip.Text = "物料编码为空或输入错误！！！";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法显示物料信息，请输入替换前后物料编码！！！');", true);/////window.location.reload();
            }
        }
        /// <summary>
        /// 绑定替换前后的物料信息
        /// </summary>
        protected string BindOldNewMarDetail()
        {
            string retvalue = "OK";
            DataTable dt=new DataTable();
            dt.Columns.Add("TYPE");
            dt.Columns.Add("MARID");
            dt.Columns.Add("MARNAME");
            dt.Columns.Add("MARGUIGE");
            dt.Columns.Add("MARCAIZHI");
            dt.Columns.Add("MARWEIGHT");
            dt.Columns.Add("MARSTAR");
            dt.Columns.Add("MARUNIT");

            string mar_old = txtMarOld.Text.Trim();
            string mar_new = txtMarNew.Text.Trim();

            string pattern = @"^\d{2}\.\d{2}\.\d{6}$";
            Regex rgx = new Regex(pattern);

            if (mar_old != "")
            {
                if (!rgx.IsMatch(mar_old))
                {
                    retvalue = "MarOldError";
                    DataRow newRow = dt.NewRow();
                    newRow[0] = "替换前";
                    dt.Rows.Add(newRow);
                }
                else
                {
                    DataRow newRow = dt.NewRow();
                    string sql_mar_old = "SELECT [ID],[MNAME],[GUIGE],[MWEIGHT],[GB],[CAIZHI],'('+[TECHUNIT]+')-('+[PURCUNIT]+'-'+[FUZHUUNIT]+')' as Unit,[STATE] FROM [TBMA_MATERIAL] where [ID]='" + mar_old + "'";
                    DataTable dt_mar_old = DBCallCommon.GetDTUsingSqlText(sql_mar_old);
                    newRow[0] = "替换前";
                    if (dt_mar_old.Rows.Count > 0)
                    {
                        newRow[1] = dt_mar_old.Rows[0]["ID"].ToString();
                        newRow[2] = dt_mar_old.Rows[0]["MNAME"].ToString();
                        newRow[3] = dt_mar_old.Rows[0]["GUIGE"].ToString();
                        newRow[4] = dt_mar_old.Rows[0]["CAIZHI"].ToString();
                        newRow[5] = dt_mar_old.Rows[0]["MWEIGHT"].ToString();
                        newRow[6] = dt_mar_old.Rows[0]["GB"].ToString();
                        newRow[7] = dt_mar_old.Rows[0]["Unit"].ToString();
                    }
                    dt.Rows.Add(newRow);
                }
            }
            else
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = "替换前";
                dt.Rows.Add(newRow);
            }

            if (mar_new != "")
            {
                if (!rgx.IsMatch(mar_new))
                {
                    retvalue = "MarNewError";
                    DataRow newRow = dt.NewRow();
                    newRow[0] = "替换后";
                    dt.Rows.Add(newRow);
                }
                else
                {
                    DataRow newRow = dt.NewRow();
                    string sql_mar_new = "SELECT [ID],[MNAME],[GUIGE],[MWEIGHT],[GB],[CAIZHI],'('+[TECHUNIT]+')-('+[PURCUNIT]+'-'+[FUZHUUNIT]+')' as Unit,[STATE] FROM [TBMA_MATERIAL] where [ID]='" + mar_new + "'  and [STATE]='1'";
                    DataTable dt_mar_new = DBCallCommon.GetDTUsingSqlText(sql_mar_new);
                    newRow[0] = "替换后";
                    if (dt_mar_new.Rows.Count > 0)
                    {
                        newRow[1] = dt_mar_new.Rows[0]["ID"].ToString();
                        newRow[2] = dt_mar_new.Rows[0]["MNAME"].ToString();
                        newRow[3] = dt_mar_new.Rows[0]["GUIGE"].ToString();
                        newRow[4] = dt_mar_new.Rows[0]["CAIZHI"].ToString();
                        newRow[5] = dt_mar_new.Rows[0]["MWEIGHT"].ToString();
                        newRow[6] = dt_mar_new.Rows[0]["GB"].ToString();
                        newRow[7] = dt_mar_new.Rows[0]["Unit"].ToString();
                    }
                    dt.Rows.Add(newRow);
                }
            }
            else
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = "替换后";
                dt.Rows.Add(newRow);
            }

            dt.AcceptChanges();
            GridView1.DataSource = dt;
            GridView1.DataBind();
            return retvalue;
        }
        /// <summary>
        /// 绑定生产制号可替换的物料
        /// </summary>
        protected void BindAllCanReplaceMar()
        {
            if (txtMarOld.Text.Trim() != "")
            {
                string sql = "select * from " + ViewState["viewtablename"] + " where BM_ENGID='" + ViewState["TaskID"] + "' AND BM_MARID='" + txtMarOld.Text.Trim() + "' AND BM_MARID!='' AND (BM_MPSTATE='0' or BM_OSSTATE='0') AND BM_MSSTATE='0'";
                GridView2.DataSource = DBCallCommon.GetDTUsingSqlText(sql);
                GridView2.DataBind();
            }
        }
        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnClear_OnClick(object sender, EventArgs e)
        {
            txtMarNew.Text = "";
            txtMarOld.Text = "";

            lblTip.Text = "";

            GridView1.DataSource = null;
            GridView1.DataBind();

            GridView2.DataSource = null;
            GridView2.DataBind();

            this.VisibleOfNoDataPanel();
        }
        /// <summary>
        /// 能否替换提示
        /// </summary>
        protected void TipforUnableReplace()
        {
            string[] array_marid = new string[2];//物料编码
            string[] array_marname = new string[2];//名称
            string[] array_guige = new string[2];//规格
            string[] array_weight = new string[2];//理论重量
            string[] array_unit = new string[2];//单位
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow grow=GridView1.Rows[i];
                array_marid[i]=grow.Cells[1].Text=="&nbsp;"?"":grow.Cells[1].Text;
                array_marname[i] = grow.Cells[2].Text == "&nbsp;" ? "" : grow.Cells[2].Text;
                array_guige[i] = grow.Cells[3].Text == "&nbsp;" ? "" : grow.Cells[3].Text;
                array_weight[i] = grow.Cells[5].Text == "&nbsp;" ? "" : grow.Cells[5].Text;
                array_unit[i] = grow.Cells[7].Text == "&nbsp;" ? "" : grow.Cells[7].Text;
            }

            if (array_marid[0] == "")
            {
                lblTip.Text = "待替换物料编码为空或不存在！！！";
                return;
            }

            if (array_marid[1] == "")
            {
                lblTip.Text = "待替换物料编码为空或不存在！！！";
                return;
            }

            if (array_marid[0].Substring(0,5) != array_marid[1].Substring(0,5))
            {
                lblTip.Text = "物料不属于同一类，无法替换！！！";
                return;
            }


            if (array_guige[0] != array_guige[1])
            {
                lblTip.Text = "物料规格不同，无法替换！！！";
                return;
            }

            if (array_marname[0] != array_marname[1])
            {
                lblTip.Text = "物料名称不同，无法替换！！！";
                return;
            }

            if (array_weight[0] != array_weight[1])
            {
                lblTip.Text = "物料理论重量，无法替换！！！";
                return;
            }

            if (array_unit[0] != array_unit[1])
            {
                lblTip.Text = "物料单位不同，无法替换！！！";
                return;
            }

            lblTip.Text = "";
        }
        /// <summary>
        /// 替换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnReplace_OnClick(object sender, EventArgs e)
        {
            this.btnViewMar_OnClick(null, null);
            if (lblTip.Text != "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法完成替换，请检查替换物料是否正确！！！');", true);
            }
            else if (GridView2.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有可替换记录！！！');", true);
            }
            else
            {
                string maridold=GridView1.Rows[0].Cells[1].Text;
                string maridnew=GridView1.Rows[1].Cells[1].Text;
                string sql = "update " + ViewState["tablename"] + " set BM_MARID='" + maridnew + "' where BM_ENGID='" + ViewState["TaskID"] + "' AND BM_MARID='" + maridold + "' AND BM_MARID!='' AND (BM_MPSTATE='0' or BM_OSSTATE='0') AND BM_MSSTATE='0'";
                DBCallCommon.ExeSqlText(sql);
                this.btnClear_OnClick(null, null);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('替换成功！！！');window.close();", true);
            }
        }

        protected void VisibleOfNoDataPanel()
        {
            if (GridView1.Rows.Count > 0)
            {
                NoDataPanel1.Visible = false;
            }
            else
            {
                NoDataPanel1.Visible = true;
            }

            if (GridView2.Rows.Count > 0)
            {
                NoDataPanel2.Visible = false;
            }
            else
            {
                NoDataPanel2.Visible = true;
            }
        }

    }
}
