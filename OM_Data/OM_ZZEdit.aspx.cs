using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_ZZEdit : System.Web.UI.Page
    {
        string Id;
        string action;
        string psId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["Action"] != null)
                action = Request.QueryString["action"].ToString();
            if (Request.QueryString["ST_ID"] != null)
                Id = Request.QueryString["ST_ID"].ToString();
            if (Request.QueryString["ID"] != null)
                psId = Request.QueryString["ID"].ToString();
            if (!IsPostBack)
            {
                string sql = "select * from View_TBDS_STAFFINFO where ST_ID='" + Id + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                DataRow dr = dt.Rows[0];
                ST_NAME.Text = dr["ST_NAME"].ToString();
                DEP_NAME.Text = dr["DEP_NAME"].ToString();
                DEP_POSITION.Text = dr["DEP_POSITION"].ToString();
                ST_INTIME.Text = dr["ST_INTIME"].ToString();
                ST_ZHENG.Text = dr["ST_ZHENG"].ToString();
                ST_TELE.Text = dr["ST_TELE"].ToString();
            }
            GetLeaderInfo();
            if (action == "Edit")
            {
                string sql = "select * from TBDS_PSDETAIL as a left join TBDS_PSVIEW as b on a.ST_ID=b.ST_ID where a.ST_ID='" + psId + "' and a.ST_TYPE='3' and b.ST_PIDTYPE='0'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                DataRow dr = dt.Rows[0];
                ST_REMARK.Text = dr["ST_REMARK"].ToString();
                ST_ZHENG.Text = dr["ST_CONT"].ToString();
                txt_zdrYJ.Text = dr["ST_NOTE"].ToString();
                BindSelectReviewer();
            }
        }

        #region 评审信息

        Table t = new Table();
        private void GetLeaderInfo()
        {
            /******绑定人员信息*****/
            GetStaffInfo("02", "综合办公室", 0);
            GetStaffInfo("03", "技术质量部", 1);
            GetStaffInfo("04", "生产管理部", 2);
            GetStaffInfo("05", "采购部", 3);
            GetStaffInfo("06", "财务部", 4);
            GetStaffInfo("07", "市场部", 5);
            GetStaffInfo("01", "公司领导", 6);
            //得到领导信息，根据金额
            Panel1.Controls.Add(t);
        }

        protected void GetStaffInfo(string st_id, string DEP_NAME, int i)
        {
            string sql = string.Format("select ST_NAME,ST_ID,ST_DEPID from TBDS_STAFFINFO as a inner join TBCM_HT_SETTING as b on a.ST_ID=b.per_id where a.ST_PD='0'and b.dep_id='{0}' and per_sfjy='0' and per_type='3'", st_id);
            //bindInfo(sql, i, DEP_NAME, st_id);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (st_id == "01")
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    DataTable ld = new DataTable();
                    ld.Columns.Add("ST_NAME");
                    ld.Columns.Add("ST_ID");
                    ld.Columns.Add("ST_DEPID");
                    ld.Rows.Add(dt.Rows[j][0].ToString(), dt.Rows[j][1].ToString(), dt.Rows[j][2].ToString());
                    bindInfo(ld, st_id + j.ToString(), DEP_NAME, i);
                    i++;
                }
            }
            else
            {
                bindInfo(dt, st_id, DEP_NAME, i);
            }
        }

        protected void bindInfo(DataTable dt, string st_id, string DEP_NAME, int i)
        {
            if (dt.Rows.Count != 0)
            {
                TableRow tr = new TableRow();
                TableCell td1 = new TableCell();//第一列为部门名称
                td1.Width = 85;
                Label lab = new Label();
                lab.Text = DEP_NAME + ":";
                Label lab1 = new Label();
                lab1.ID = "dep" + i.ToString();
                lab1.Text = st_id;
                lab1.Visible = false;
                td1.Controls.Add(lab);
                td1.Controls.Add(lab1);
                tr.Cells.Add(td1);

                CheckBoxList cki = new CheckBoxList();//第二列为领导的姓名
                cki.ID = "cki" + i.ToString();
                cki.DataSource = dt;
                cki.DataTextField = "ST_NAME";//领导的姓名
                cki.DataValueField = "ST_ID";//部门的编号
                cki.DataBind();
                for (int k = 0; k < cki.Items.Count; k++)
                {
                    cki.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个
                }
                cki.RepeatColumns = 5;//获取显示的列数
                TableCell td2 = new TableCell();
                td2.Controls.Add(cki);
                tr.Cells.Add(td2);
                t.Controls.Add(tr);
            }
        }

        #endregion

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (Check())
            {
                BindReviewer();//读出评审人员
                List<string> str_sql = new List<string>();
                string sql = "";
                string code = DateTime.Now.ToString("yyyyMMddhhmmss");
                //删除信息
                if (!string.IsNullOrEmpty(psId))
                {
                    sql = "delete from TBDS_DETAIL where ST_ID='" + psId + "'";
                    str_sql.Add(sql);
                    sql = "delete from TBDS_VIEW where ST_ID='" + psId + "'";
                    str_sql.Add(sql);
                }
                //信息总表
                sql = string.Format("insert into TBDS_PSDETAIL(ST_ID,ST_PER,ST_CONT,ST_REMARK,ST_ZDR,ST_ZDSJ,ST_TYPE)  values('{0}','{1}','{2}','{3}','{4}','{5}','0')", code, Id, ST_ZHENG.Text, ST_REMARK.Text, Session["UserID"].ToString(), DateTime.Now.ToString("yyyy-MM-dd"));
                str_sql.Add(sql);
                //合同评审信息详细表(TBDS_PSVIEW)
                sql = "insert into TBDS_PSVIEW values('" + code + "','" + Session["UserID"].ToString() + "','2','" + txt_zdrYJ.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + Session["UserDeptID"].ToString() + "','0')";
                str_sql.Add(sql);  //制单人意见
                for (int i = 0; i < reviewer.Count; i++)
                {
                    /******************通过键来找值******************************************************/
                    /**为了兼容领导同时为部门负责人的情况，评审人部门编号要以评审人员设置表中为准，而不以当前登录人部门编号为依据**/
                    string sql_dep = "select dep_id from TBCM_HT_SETTING where per_id='" + reviewer.Values.ElementAt(i) + "' and per_sfjy=0 and per_type='3'";
                    DataTable dt_dep = DBCallCommon.GetDTUsingSqlText(sql_dep);
                    if (dt_dep.Rows.Count > 0)
                    {
                        sql = "insert into TBDS_PSVIEW(ST_ID,ST_PID,ST_DEP,ST_PIDTYPE) values('" + code + "','" + reviewer.Values.ElementAt(i) + "','" + dt_dep.Rows[0]["dep_id"].ToString().Substring(0, 2) + "','1')";
                        str_sql.Add(sql);//其他人
                        if (dt_dep.Rows[0]["dep_id"].ToString().Substring(0, 2) != "01")
                        {
                            DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(reviewer.Values.ElementAt(i)), new List<string>(), new List<string>(), "转正审批", "您有转正信息需要审批，请登录系统进行查看。");
                        }
                    }
                }
                DBCallCommon.ExecuteTrans(str_sql);
                ////Response.Write("<script>window.opener.location.reload();window.close();</script>");//刷新
                Response.Write("<script>alert('添加审批成功！');window.close();</script>");
            }
        }

        private bool Check()
        {
            bool check = false;
            int count = 0;
            for (int i = 0; i < 10; i++)
            {
                CheckBoxList ck = (CheckBoxList)Panel1.FindControl("cki" + i.ToString());
                if (ck != null)
                {
                    for (int j = 0; j < ck.Items.Count; j++)
                    {
                        if (ck.Items[j].Selected == true)
                        {
                            count++;
                        }
                    }
                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('没有选择任何审批人！！');", true); return check;
            }
            if (txt_zdrYJ.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('没有填写制单人意见！！');", true); return check;
            }
            return true;
        }

        //对评审人进行勾选登记
        Dictionary<string, string> reviewer = new Dictionary<string, string>();//用于存储审核部门负责人的名单
        private void BindReviewer()
        {
            int count = 0;
            string sql = string.Format("select ST_NAME,ST_ID,ST_DEPID from TBDS_STAFFINFO as a inner join TBCM_HT_SETTING as b on a.ST_ID=b.per_id where a.ST_PD='0'and b.dep_id='{0}' and per_sfjy='0' and b.per_type='0'", "01");
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            int num = dt.Rows.Count;
            for (int i = 0; i < 5 + num; i++)
            {
                CheckBoxList ck = (CheckBoxList)Panel1.FindControl("cki" + i.ToString());
                Label lb = (Label)Panel1.FindControl("dep" + i.ToString());
                if (ck != null)
                {
                    for (int j = 0; j < ck.Items.Count; j++)
                    {
                        if (ck.Items[j].Selected == true)
                        {
                            reviewer.Add(lb.Text, ck.Items[j].Value.ToString());//字典，绑定部门领导的编号
                            count++;
                        }
                    }
                }
            }
        }

        //对绑定已经勾选的评审人
        private void BindSelectReviewer()
        {
            string st_id = psId;//	评审单号
            string check_select = "select ST_PID from TBDS_PSVIEW where ST_ID='" + st_id + "' and ST_PIDTYPE!='0'";
            DataTable sele = DBCallCommon.GetDTUsingSqlText(check_select);
            string sql = string.Format("select ST_NAME,ST_ID,ST_DEPID from TBDS_STAFFINFO as a inner join TBCM_HT_SETTING as b on a.ST_ID=b.per_id where a.ST_PD='0'and b.dep_id='{0}' and per_sfjy='0' and b.per_type='0'", "01");
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            int num = dt.Rows.Count;
            for (int i = 0; i < 5 + num; i++)
            {
                CheckBoxList ck = (CheckBoxList)Panel1.FindControl("cki" + i.ToString());
                for (int j = 0; j < sele.Rows.Count; j++)
                {
                    for (int k = 0; k < ck.Items.Count; k++)
                    {
                        if (ck.Items[k].Value == sele.Rows[j][0].ToString())
                        {
                            ck.Items[k].Selected = true;
                        }
                    }
                }
            }
        }

    }
}
