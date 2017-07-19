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
using System.Data.SqlClient;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_ShipTime : System.Web.UI.Page
    {
        string engid;
        string sqltext;
        private double sum = 0;
        private double sum1 = 0;
        private double sum2 = 0;
        private double sum3 = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            engid = Request.QueryString["engid"];
            btnFinsh.Attributes.Add("OnClick", "javascript:return confirm('确定" + engid + "发运完成？');");
            if (!IsPostBack)
            {
                this.InitVar();
                this.InitData();
            }
        }
        /// <summary>
        /// 绑定基本信息
        /// </summary>
        private void InitVar()
        {
            sqltext = "select TSA_ID,TSA_PJID,TSA_ENGNAME,TSA_PJNAME,TSA_ENGTYPE from View_TM_TaskAssign where TSA_ID='" + engid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                tsaid.Text = dt.Rows[0]["TSA_ID"].ToString();
                engname.Text = dt.Rows[0]["TSA_ENGNAME"].ToString();
                proname.Text = dt.Rows[0]["TSA_PJNAME"].ToString();
                ddlengtype.Text = dt.Rows[0]["TSA_ENGTYPE"].ToString();
                lblpjid.Text = dt.Rows[0]["TSA_PJID"].ToString();
            }
        }
        /// <summary>
        /// 绑定船次详细信息
        /// </summary>
        private void InitData()
        {
            DataTable dt = this.formatTable(false);
            sqltext = "select TSA_NO,TSA_QUANTITY,TSA_TOTALWGHT,TSA_ID,TSA_FY,TSA_VOLUME,TSA_NETWGHT,TSA_GROSSWGHT,TSA_SHIPPLANTIME,TSA_SHIPTIME,TSA_SHIP,TSA_SHIPWGHT,TSA_DEVICENO from View_TM_WorkAmount where TSA_ID='" + engid + "'";
            DataTable newdt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (newdt.Rows.Count > 0)
            {
                num.Text = newdt.Rows[0]["TSA_QUANTITY"].ToString();//数量
                totweight.Text = newdt.Rows[0]["TSA_TOTALWGHT"].ToString();//总重

                foreach (DataRow dr in newdt.Rows)
                {
                    if (dr["TSA_SHIP"].ToString() == "")//如果是总的生产制号
                    {
                        lblkey.Text = newdt.Rows[0]["TSA_NO"].ToString();
                    }
                    else
                    {
                        DataRow newRow = dt.NewRow();
                        newRow[0] = dr["TSA_NO"].ToString();
                        newRow[1] = dr["TSA_SHIPTIME"].ToString();
                        newRow[2] = dr["TSA_DEVICENO"].ToString();
                        newRow[3] = dr["TSA_SHIPWGHT"].ToString();
                        newRow[4] = dr["TSA_SHIP"].ToString();
                        newRow[5] = dr["TSA_FY"].ToString();
                        newRow[6] = dr["TSA_VOLUME"].ToString();
                        newRow[7] = dr["TSA_NETWGHT"].ToString();
                        newRow[8] = dr["TSA_GROSSWGHT"].ToString();
                        newRow[9] = dr["TSA_SHIPPLANTIME"].ToString();
                        dt.Rows.Add(newRow);
                    }
                }
            }
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        /// <summary>
        /// 定义DataTable
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        private DataTable formatTable(bool state)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("TSA_NO");
            dt.Columns.Add("TSA_SHIPTIME");
            dt.Columns.Add("TSA_DEVICENO");
            dt.Columns.Add("TSA_SHIPWGHT");
            dt.Columns.Add("TSA_SHIP");
            dt.Columns.Add("TSA_FY");
            dt.Columns.Add("TSA_VOLUME");
            dt.Columns.Add("TSA_NETWGHT");
            dt.Columns.Add("TSA_GROSSWGHT");
            dt.Columns.Add("TSA_SHIPPLANTIME");
            if (state)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((Label)gRow.FindControl("lblid")).Text.ToString();
                    newRow[1] = ((TextBox)gRow.FindControl("txtshiptime")).Text.Trim();
                    newRow[2] = ((TextBox)gRow.FindControl("txtsbno")).Text.Trim();
                    newRow[3] = ((TextBox)gRow.FindControl("txtweight")).Text.Trim();
                    newRow[4] = ((TextBox)gRow.FindControl("txtship")).Text.Trim();
                    newRow[5] = gRow.Cells[2].Text.Trim();
                    newRow[6] = ((TextBox)gRow.FindControl("txtsbtj")).Text.Trim();
                    newRow[7] = ((TextBox)gRow.FindControl("txtsbjz")).Text.Trim();
                    newRow[8] = ((TextBox)gRow.FindControl("txtsbmz")).Text.Trim();
                    newRow[9] = ((TextBox)gRow.FindControl("txtshipplantime")).Text.Trim();
                    dt.Rows.Add(newRow);
                }
            }
            return dt;
        }
        /// <summary>
        /// 计算总重
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                if (e.Row.Cells[2].Text.Trim() == "Y")
                {
                    ((TextBox)e.Row.FindControl("txtship")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtsbno")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtweight")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtsbtj")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtsbmz")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtsbjz")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtshipplantime")).Enabled = false;
                    ((TextBox)e.Row.FindControl("txtshiptime")).Enabled = false;
                    ((HyperLink)e.Row.FindControl("hlSelect")).Visible = false;
                }
                else if (e.Row.Cells[2].Text.Trim() == "N")
                {
                    ((TextBox)e.Row.FindControl("txtship")).Enabled = false; ;
                }
                sum += Convert.ToDouble(((TextBox)e.Row.FindControl("txtweight")).Text.Trim() == "" ? "0" : ((TextBox)e.Row.FindControl("txtweight")).Text.Trim());
                sum1 += Convert.ToDouble(((TextBox)e.Row.FindControl("txtsbtj")).Text.Trim() == "" ? "0" : ((TextBox)e.Row.FindControl("txtsbtj")).Text.Trim());
                sum2 += Convert.ToDouble(((TextBox)e.Row.FindControl("txtsbmz")).Text.Trim() == "" ? "0" : ((TextBox)e.Row.FindControl("txtsbmz")).Text.Trim());
                sum3 += Convert.ToDouble(((TextBox)e.Row.FindControl("txtsbjz")).Text.Trim() == "" ? "0" : ((TextBox)e.Row.FindControl("txtsbjz")).Text.Trim());
            }
            else if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[4].Text = "合计：";
                e.Row.Cells[8].Text = sum.ToString();
                e.Row.Cells[5].Text = sum1.ToString();
                e.Row.Cells[6].Text = sum2.ToString();
                e.Row.Cells[7].Text = sum3.ToString();
                e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[8].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[5].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Left;
                e.Row.Cells[7].HorizontalAlign = HorizontalAlign.Left;
            }
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.CanAdd())
            {

                DataTable dt = this.formatTable(true);
                DataRow newRow = dt.NewRow();
                newRow[0] = "null";
                newRow[3] = "0";
                newRow[5] = "W";
                newRow[6]="0";
                newRow[7]="0";
                newRow[8]="0";
                dt.Rows.Add(newRow);
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {   
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法增加！！！\\r\\r提示:各分项总和已与总重相等或超过总重！');", true);
            }
        }
        /// <summary>
        /// 能否增加判断
        /// </summary>
        /// <returns></returns>
        protected bool CanAdd()
        {
            bool retvale = true;
            double totalweight = Convert.ToDouble(totweight.Text.Trim());
            double weight = 0;
            foreach (GridViewRow grow in GridView1.Rows)
            {
                weight += Convert.ToDouble(((TextBox)grow.FindControl("txtweight")).Text.Trim() == "" ? "0" : ((TextBox)grow.FindControl("txtweight")).Text.Trim());
            }

            if (totalweight <= weight)
            {
                retvale = false;
            }

            return retvale;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            string submit_flag = "Yes";
            int n = 0;
            DataTable dt = this.formatTable(true);
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                CheckBox cbcheck = (CheckBox)gRow.FindControl("cbcheck");
                if (cbcheck.Checked)
                {
                    //找出该勾选项的发运状态
                    string fy_state = gRow.Cells[2].Text.Trim();

                    if (fy_state != "W")
                    {
                        submit_flag = "No";
                        break;
                    }
                    else
                    {
                        dt.Rows.RemoveAt(i-n);
                        n++;
                        dt.AcceptChanges();
                        GridView1.DataSource = dt;
                        GridView1.DataBind();
                        Label lblid = (Label)gRow.FindControl("lblid");
                        if (lblid.Text != "null")
                        {
                            sqltext = "delete from TBPM_TCTSENROLL where TSA_NO='" + lblid.Text + "'";
                            list_sql.Add(sqltext);
                        }
                    }
                }
            }

            if (submit_flag == "Yes")
            {
                DBCallCommon.ExecuteTrans(list_sql);
            }
            else if(submit_flag=="No")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法删除！！！\\r\\r提示:发运状态为Y或N时无法删除');", true);
            }

        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string ret=this.CanSave();
            if (ret == "Yes")
            {
                List<string> list_sql = new List<string>();
                foreach (GridViewRow grow in GridView1.Rows)
                {
                    string ship_id = ((TextBox)grow.FindControl("txtship")).Text.Trim();
                    string sbno = ((TextBox)grow.FindControl("txtsbno")).Text.Trim();
                    double sbtj=Convert.ToDouble(((TextBox)grow.FindControl("txtsbtj")).Text.Trim() == "" ? "0" : ((TextBox)grow.FindControl("txtsbtj")).Text.Trim());
                    double sbmz=Convert.ToDouble(((TextBox)grow.FindControl("txtsbmz")).Text.Trim() == "" ? "0" : ((TextBox)grow.FindControl("txtsbmz")).Text.Trim());
                    double sbjz=Convert.ToDouble(((TextBox)grow.FindControl("txtsbjz")).Text.Trim() == "" ? "0" : ((TextBox)grow.FindControl("txtsbjz")).Text.Trim());
                    double weight = Convert.ToDouble(((TextBox)grow.FindControl("txtweight")).Text.Trim() == "" ? "0" : ((TextBox)grow.FindControl("txtweight")).Text.Trim());
                    string ship_plantime=((TextBox)grow.FindControl("txtshipplantime")).Text.Trim();
                    string ship_time = ((TextBox)grow.FindControl("txtshiptime")).Text.Trim();
                    string id = ((Label)grow.FindControl("lblid")).Text.Trim();

                    if (id != "null")
                    {
                        sqltext = "update TBPM_TCTSENROLL set TSA_SHIP='" + ship_id + "',TSA_VOLUME='" + sbtj + "',TSA_GROSSWGHT='" + sbmz + "',TSA_NETWGHT='"+sbjz+"',TSA_SHIPTIME='" + ship_time + "',TSA_SHIPPLANTIME='"+ship_plantime+"',TSA_SHIPWGHT='" + weight + "',TSA_DEVICENO='" + sbno + "' where TSA_NO='" + id + "'";
                        list_sql.Add(sqltext);
                    }
                    else
                    {
                        sqltext = "INSERT INTO [TBPM_TCTSENROLL]([TSA_ID],[TSA_QUANTITY],[TSA_TOTALWGHT],[TSA_FY],[TSA_SHIP],[TSA_SHIPPLANTIME],[TSA_SHIPTIME],[TSA_CNWXDLZL],[TSA_MNWKSHOP],[TSA_VOLUME],[TSA_NETWGHT],[TSA_GROSSWGHT],[TSA_MARPLAN],[TSA_WXWGJ],[TSA_MNDETAIL],[TSA_ZXSHEET],[TSA_TECHJD],[TSA_OILPLAN],[TSA_TIMES],[TSA_TECHMANID],[TSA_NOTE],[TSA_SHIPWGHT],[TSA_DEVICENO],[TSA_TOTALSHIP]) ";
                        sqltext += " SELECT [TSA_ID],[TSA_QUANTITY],[TSA_TOTALWGHT],[TSA_FY],'"+ship_id+"','"+ship_plantime+"','"+ship_time+"',[TSA_CNWXDLZL],[TSA_MNWKSHOP],'"+sbtj+"','"+sbjz+"','"+sbmz+"',[TSA_MARPLAN],[TSA_WXWGJ],[TSA_MNDETAIL],[TSA_ZXSHEET],[TSA_TECHJD],[TSA_OILPLAN],[TSA_TIMES],[TSA_TECHMANID],[TSA_NOTE],"+weight+",'"+sbno+"',[TSA_TOTALSHIP]";
                        sqltext += " FROM [TBPM_TCTSENROLL] where [TSA_NO]='" + lblkey.Text + "'";
                        list_sql.Add(sqltext);
                    }
                }
                list_sql.Add("EXEC PRO_TM_WorkAmount '" + engid + "'," + lblkey.Text + "");
                DBCallCommon.ExecuteTrans(list_sql);
                this.InitData();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已保存！！！');", true);
            }
            else if (ret == "Y/N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法保存！！！\\r\\r提示:发运状态为Y时所有项不能为空，为N时除【集港时间】外所有项不能为空，同时船次重量必须大于0！');", true);
            }
            else if (ret == "W")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法保存！！！\\r\\r提示:发运状态为W时,除【计划时间】【集港时间】外所有项不能为空，同时船次重量等必须大于0！');", true);
            }
            else if (ret.Contains("NotEqual"))
            {
                string[] a = ret.Split('-');
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法保存,存在重量差异！！！\\r\\r提示:总重-各分项重量差值为:"+a[1]+"！');", true);
            }
        }
        /// <summary>
        /// 能否保存检查
        /// </summary>
        /// <returns></returns>
        protected string CanSave()
        {
            double totalweight = Convert.ToDouble(totweight.Text.Trim());
            double sum_weight=0;
            string retValue="Yes";
            foreach (GridViewRow grow in GridView1.Rows)
            {
                string fy=grow.Cells[2].Text.Trim();
                string ship_id = ((TextBox)grow.FindControl("txtship")).Text.Trim();
                string sbno = ((TextBox)grow.FindControl("txtsbno")).Text.Trim();
                double sbtj = Convert.ToDouble(((TextBox)grow.FindControl("txtsbtj")).Text.Trim() == "" ? "0" : ((TextBox)grow.FindControl("txtsbtj")).Text.Trim());
                double sbmz = Convert.ToDouble(((TextBox)grow.FindControl("txtsbmz")).Text.Trim() == "" ? "0" : ((TextBox)grow.FindControl("txtsbmz")).Text.Trim());
                double sbjz = Convert.ToDouble(((TextBox)grow.FindControl("txtsbjz")).Text.Trim() == "" ? "0" : ((TextBox)grow.FindControl("txtsbjz")).Text.Trim());
                double weight = Convert.ToDouble(((TextBox)grow.FindControl("txtweight")).Text.Trim() == "" ? "0" : ((TextBox)grow.FindControl("txtweight")).Text.Trim());
                string ship_plantime = ((TextBox)grow.FindControl("txtshipplantime")).Text.Trim();
                string ship_time = ((TextBox)grow.FindControl("txtshiptime")).Text.Trim();
                string id = ((Label)grow.FindControl("lblid")).Text.Trim();

                sum_weight+=weight;
                if(ship_plantime==""&&ship_time!="")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('【集港时间】不为空时，【计划时间】不能为空！');", true);
                    break;
                }

                if (fy != "W")
                {
                    if (ship_id == "" || sbno == "" || weight <=0 || ship_time == ""|| sbtj<=0 || sbmz<=0 || sbjz<=0 ||ship_plantime== "")
                    {
                        retValue="Y/N";
                        break;
                    }
                }
                else if(fy=="W")
                {
                    if (ship_id == "" || sbno == "" || weight <= 0 )/////////|| sbtj <= 0 || sbmz <= 0 || sbjz <= 0
                    {
                        retValue="W";
                        break;
                    }
                }
                
            }

            if (retValue == "Yes")
            {
                if (totalweight - sum_weight != 0)
                {
                    double D_value = totalweight - sum_weight;
                    retValue = "NotEqual-" + D_value.ToString();
                }
            }

            return retValue;
             
        }

        /// <summary>
        /// 返回
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("TM_WorkAmount.aspx");
        }
        /// <summary>
        /// 发运完成确认
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnFinsh_Click(object sender, EventArgs e)
        {
            string ret = this.CanFinish();
            if (ret=="Ok")
            {
                List<string> list_sql = new List<string>();
                //更新任务分工中工程的完成状态
                sqltext = "update TBPM_TCTSASSGN set TSA_STATUS='1',TSA_STATE='2',TSA_REALFSDATE='"+System.DateTime.Now.ToString("yyyy-MM-dd")+"' where TSA_ID='" + tsaid.Text + "' or TSA_ID like '" + tsaid.Text + "-%'";
                list_sql.Add(sqltext);

                ///以下暂时取消，由生产修改
                ////////////////////更新主计划的项目工程的完成状态，这里表示完成
                //////////////////sqltext = "update TBMP_MAINPLANDETAIL set MP_STATE='1' where MP_MARK='1' and MP_ENGID='" + tsaid.Text + "'";
                //////////////////list_sql.Add(sqltext);

                ////////////////////更新其他计划工程的完成状态。
                //////////////////sqltext = "update TBMP_PDCTPLANMTH set PPM_STATE='1' where PPM_MARK='1' and PPM_ENGID='" + tsaid.Text + "'";
                //////////////////list_sql.Add(sqltext);

                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已完成确认！！！');", true);
            }
            else if(ret=="Y/N")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法进行完工确认！！！\\r\\r提示:请确认船次已添加并且发运状态为\"Y\"');", true);
            }
            else if(ret=="Confirmed")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该生产制号已确认过，无法再进行确认！！！');", true);
            }
            else if (ret == "WeightError")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法确认！！！\\r\\r提示:总重与各船次分项之和不等！');", true);
            }
        }
        /// <summary>
        /// 能否发运完成
        /// </summary>
        /// <returns></returns>
        protected string CanFinish()
        {
            double totalweight = Convert.ToDouble(totweight.Text.Trim());
            double sum_weight = 0;


            if (GridView1.Rows.Count == 0)
            {
                return "Y/N";
            }

            foreach (GridViewRow grow in GridView1.Rows)
            {
                if (grow.Cells[2].Text.Trim() == "N" || grow.Cells[2].Text.Trim() == "W")
                {
                    return "Y/N";
                }
                else
                {
                    double weight = Convert.ToDouble(((TextBox)grow.FindControl("txtweight")).Text.Trim() == "" ? "0" : ((TextBox)grow.FindControl("txtweight")).Text.Trim());
                    sum_weight+=weight;
                }
            }
            //总量是否正确
            if (sum_weight != totalweight)
            {
                return "WeightError";
            }

            //是否已确认
            string sql = "select TSA_STATUS from  TBPM_TCTSASSGN where TSA_ID='" + tsaid.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.HasRows)
            {
                dr.Read();
                if (dr["TSA_STATUS"].ToString()=="1")
                {
                    dr.Close();
                    return "Confirmed";
                }
                dr.Close();
            }

            return "Ok";
        }
        protected void txt_textchanged(object sender, EventArgs e)
        {
            DataTable dt = this.formatTable(true);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
    }
}
