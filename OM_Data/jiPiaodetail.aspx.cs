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

namespace ZCZJ_DPF.OM_Data
{
    public partial class jiPiaodetail : System.Web.UI.Page
    {
        string action;
        string auditno;
        protected void Page_Load(object sender, EventArgs e)
        {
            //操作：包括添加add,修改edit,删除delete,查看view,审核audit
            action = Request.QueryString["action"].ToString().Trim();
            //审核编号：识别当前审批单号
            auditno = Request.QueryString["auditno"].ToString().Trim();

            //获取当前单据状态
            string sqlgetstate = "select * from AuditNew where auditno='" + auditno + "'";
            DataTable dtgetstate = DBCallCommon.GetDTUsingSqlText(sqlgetstate);
            if (dtgetstate.Rows.Count > 0)
            {
                hidstate.Value = dtgetstate.Rows[0]["totalstate"].ToString().Trim();
            }
            else
            {
                txt_dep.Text = Session["UserDept"].ToString().Trim();
                hiddepid.Value = Session["UserDeptID"].ToString().Trim();
            }
            contrlkjx();//控件可见性和可用性
            if (!IsPostBack)
            {
                //子函数，绑定审批数据
                if (action != "add")
                {
                    bindmxdata();//绑定数据
                }
                else
                {
                    CreateNewRow(5);
                }
            }
        }
        //绑定数据
        private void bindmxdata()
        {
            string sqlmx = "select * from JipiaoContentDetail where detailno='" + auditno + "'";
            DataTable dtmx = DBCallCommon.GetDTUsingSqlText(sqlmx);
            if (dtmx.Rows.Count > 0)
            {
                rpt1.DataSource = dtmx;
                rpt1.DataBind();
            }
            string sql = "select * from JipiaoContent where jipiaocontentno='" + auditno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                txt_jipiaocontentno.Text = dt.Rows[0]["jipiaocontentno"].ToString().Trim();
                txt_dep.Text = dt.Rows[0]["sqdepartmentname"].ToString().Trim();
                hiddepid.Value = dt.Rows[0]["sqdepartmentid"].ToString().Trim();
                if (dt.Rows[0]["fankui"].ToString().Trim() == "是")
                {
                    iffankui.Checked = true;
                    lbfankuitime.Text = dt.Rows[0]["fankuitime"].ToString().Trim();
                    txtfaikuinote.Text = dt.Rows[0]["fankuinote"].ToString().Trim();
                }
            }
        }
        //控件可见性和可用性
        private void contrlkjx()
        {
            //添加
            if (action == "add")
            {
                iffankui.Disabled = false;
                btnsave.Visible = true;
                btnadd.Visible = true;
                delete.Visible = true;
            }
            //修改
            if (action == "edit")
            {
                iffankui.Disabled = false;
                btnsave.Visible = true;
                btnadd.Visible = true;
                delete.Visible = true;
            }
            //查看和审核
            if (action == "view" || action == "audit")
            {
                iffankui.Disabled = true;
                btnsave.Visible = false;
                btnadd.Visible = false;
                delete.Visible = false;
            }
            //反馈
            if (action == "fankui")
            {
                iffankui.Disabled = true;
                btnsave.Visible = true;
                btnadd.Visible = false;
                delete.Visible = false;

                foreach (RepeaterItem item in rpt1.Items)
                {
                    TextBox detailpername = item.FindControl("detailpername") as TextBox;
                    TextBox startlocation = item.FindControl("startlocation") as TextBox;
                    TextBox endlocation = item.FindControl("endlocation") as TextBox;
                    HtmlInputText planstartdate = item.FindControl("planstartdate") as HtmlInputText;
                    HtmlInputText planenddate = item.FindControl("planenddate") as HtmlInputText;
                    detailpername.Enabled = false;
                    startlocation.Enabled = false;
                    endlocation.Enabled = false;
                    planstartdate.Disabled = true;
                    planenddate.Disabled = false;
                }
            }
        }
        #region 增加删除行

        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            List<string> col = new List<string>();
            this.rpt1.DataSource = dt;
            this.rpt1.DataBind();
            InitVar(col);
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("detailID");
            dt.Columns.Add("detailpername");
            dt.Columns.Add("detailperid");
            dt.Columns.Add("startlocation");
            dt.Columns.Add("endlocation");
            dt.Columns.Add("money");
            dt.Columns.Add("planstartdate");
            dt.Columns.Add("planenddate");
            dt.Columns.Add("realstartdate");
            dt.Columns.Add("realenddate");
            dt.Columns.Add("detailnote");
            foreach (RepeaterItem retItem in rpt1.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((Label)retItem.FindControl("detailID")).Text;
                newRow[1] = ((TextBox)retItem.FindControl("detailpername")).Text;
                newRow[2] = ((Label)retItem.FindControl("detailperid")).Text;
                newRow[3] = ((TextBox)retItem.FindControl("startlocation")).Text;
                newRow[4] = ((TextBox)retItem.FindControl("endlocation")).Text;

                newRow[5] = ((TextBox)retItem.FindControl("money")).Text;
                newRow[6] = ((HtmlInputText)retItem.FindControl("planstartdate")).Value;

                newRow[7] = ((HtmlInputText)retItem.FindControl("planenddate")).Value;
                newRow[8] = ((HtmlInputText)retItem.FindControl("realstartdate")).Value;
                newRow[9] = ((HtmlInputText)retItem.FindControl("realenddate")).Value;
                newRow[10] = ((TextBox)retItem.FindControl("detailnote")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        private void InitVar(List<string> col)
        {
            if (rpt1.Items.Count == 0)
            {
                delete.Visible = false;
            }
            else
            {
                delete.Visible = true;
            }
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            List<string> col = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("detailID");
            dt.Columns.Add("detailpername");
            dt.Columns.Add("detailperid");
            dt.Columns.Add("startlocation");
            dt.Columns.Add("endlocation");
            dt.Columns.Add("money");
            dt.Columns.Add("planstartdate");
            dt.Columns.Add("planenddate");
            dt.Columns.Add("realstartdate");
            dt.Columns.Add("realenddate");
            dt.Columns.Add("detailnote");
            foreach (RepeaterItem retItem in rpt1.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((Label)retItem.FindControl("detailID")).Text;
                    newRow[1] = ((TextBox)retItem.FindControl("detailpername")).Text;
                    newRow[2] = ((Label)retItem.FindControl("detailperid")).Text;
                    newRow[3] = ((TextBox)retItem.FindControl("startlocation")).Text;
                    newRow[4] = ((TextBox)retItem.FindControl("endlocation")).Text;

                    newRow[5] = ((TextBox)retItem.FindControl("money")).Text;
                    newRow[6] = ((HtmlInputText)retItem.FindControl("planstartdate")).Value;

                    newRow[7] = ((HtmlInputText)retItem.FindControl("planenddate")).Value;
                    newRow[8] = ((HtmlInputText)retItem.FindControl("realstartdate")).Value;
                    newRow[9] = ((HtmlInputText)retItem.FindControl("realenddate")).Value;
                    newRow[10] = ((TextBox)retItem.FindControl("detailnote")).Text;
                    dt.Rows.Add(newRow);
                }
            }
            this.rpt1.DataSource = dt;
            this.rpt1.DataBind();
            InitVar(col);
        }

        #endregion
        //添加行
        protected void btnadd_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (int.TryParse(txtNum.Text, out a))
            {
                CreateNewRow(a);
            }
            else
            {
                Response.Write("<script>alert('请输入数字！')</script>");
            }
        }
        protected void Textname_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            TextBox Tb_newstid = (TextBox)sender;
            RepeaterItem Reitem = (RepeaterItem)Tb_newstid.Parent;

            if (num > 0)
            {
                string stid = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + stid + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    ((Label)Reitem.FindControl("detailperid")).Text = stid;
                    ((TextBox)Reitem.FindControl("detailpername")).Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员不存在，请重新输入！');", true);
                }
            }
        }
        //保存
        protected void btnsave_Click(object sender, EventArgs e)
        {
            List<string> listsql = new List<string>();
            string sqltext = "";
            string shenpibh="";
            if (action == "add")
            {
                shenpibh = "JP" + DateTime.Now.ToString("yyyyMMddHHmmss").Trim() + "" + Session["UserID"].ToString().Trim() + "";
            }
            else
            {
                shenpibh = auditno;
            }
            listsql.Add("delete from JipiaoContentDetail where detailno='" + auditno + "'");
            foreach (RepeaterItem item in rpt1.Items)
            {
                TextBox detailpername = item.FindControl("detailpername") as TextBox;
                Label detailperid = item.FindControl("detailperid") as Label;
                TextBox startlocation = item.FindControl("startlocation") as TextBox;
                TextBox endlocation = item.FindControl("endlocation") as TextBox;
                TextBox money = item.FindControl("money") as TextBox;
                HtmlInputText planstartdate = item.FindControl("planstartdate") as HtmlInputText;
                HtmlInputText planenddate = item.FindControl("planenddate") as HtmlInputText;
                HtmlInputText realstartdate = item.FindControl("realstartdate") as HtmlInputText;
                HtmlInputText realenddate = item.FindControl("realenddate") as HtmlInputText;
                TextBox detailnote = item.FindControl("detailnote") as TextBox;
                if (detailpername.Text.Trim() != "" & detailperid.Text.Trim() != "")
                {
                    sqltext = "insert into JipiaoContentDetail(detailno, detailpername, detailperid, startlocation, endlocation, money, planstartdate, planenddate, realstartdate, realenddate,detailnote) values('" + shenpibh + "','" + detailpername.Text.Trim() + "','" + detailperid.Text.Trim() + "','" + startlocation.Text.Trim() + "','" + endlocation.Text.Trim() + "'," + CommonFun.ComTryDouble(money.Text.Trim()) + ",'" + planstartdate.Value.Trim() + "','" + planenddate.Value.Trim() + "','" + realstartdate.Value.Trim() + "','" + realenddate.Value.Trim() + "','" + detailnote.Text.Trim() + "')";
                    listsql.Add(sqltext);
                }
            }
            if (action == "add")
            {
                if (iffankui.Checked)
                {
                    sqltext = "insert into JipiaoContent(jipiaocontentno,sqdepartmentid,sqdepartmentname,fankui) values('" + shenpibh + "','" + Session["UserDeptID"].ToString().Trim() + "','" + Session["UserDept"].ToString().Trim() + "','是')";
                    listsql.Add(sqltext);
                }
                else
                {
                    sqltext = "insert into JipiaoContent(jipiaocontentno,sqdepartmentid,sqdepartmentname,fankui) values('" + shenpibh + "','" + Session["UserDeptID"].ToString().Trim() + "','" + Session["UserDept"].ToString().Trim() + "','否')";
                    listsql.Add(sqltext);
                }
                sqltext = "insert into AuditNew(auditno,audittype,addpername,addperid,addtime) values('" + shenpibh + "','" + audittitle.Text.Trim() + "','" + Session["UserName"].ToString().Trim() + "','" + Session["UserID"].ToString().Trim() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "')";//不需要修改
                listsql.Add(sqltext);
            }
            else if (action == "edit")
            {
                if (iffankui.Checked)
                {
                    sqltext = "update JipiaoContent set fankui='是' where jipiaocontentno='" + shenpibh + "'";
                    listsql.Add(sqltext);
                }
                else
                {
                    sqltext = "update JipiaoContent set fankui='否' where jipiaocontentno='" + shenpibh + "'";
                    listsql.Add(sqltext);
                }
            }
            else if (action == "fankui")
            {
                sqltext = "update JipiaoContent set fankuistate='1',fankuitime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',fankuinote='" + txtfaikuinote.Text.Trim() + "' where jipiaocontentno='" + shenpibh + "'";
                listsql.Add(sqltext);
            }
            DBCallCommon.ExecuteTrans(listsql);
            if (action == "fankui")
            {
                Response.Redirect("~/OM_Data/jiPiaodetail.aspx?action=view&auditno=" + shenpibh);
            }
            else
            {
                Response.Redirect("~/OM_Data/jiPiaodetail.aspx?action=edit&auditno=" + shenpibh);
            }
        }
    }
}
