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
using System.Data.SqlClient;
using System.Drawing;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_Warning : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                bindData();
            }
            CheckUser(ControlFinder);
        }

        private void bindData()
        {
            string sql = "select MARID,MNAME,GUIGE,CAIZHI,GB,PURCUNIT,cast(WARNNUM as float) as WARNNUM,cast(REASONABLENUM as float) as REASONABLENUM,CAST(isnull(storagenum,0) AS FLOAT) AS STORAGENUM,CAST(isnull(NUM,0) AS FLOAT) AS NUM,ISNULL(TJRNM,'') AS TJRNM,ISNULL(left(TJDATE,10),'') AS TJDATE,ISNULL(TOTALSTATE,'') AS TOTALSTATE,ISNULL(MP_SPZT,'') AS MP_SPZT,Type,BZJSBZ from View_STORAGE_WARNING where " + StrWhere();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        private string StrWhere()
        {
            string sql = "1=1";
            if (ddlType.SelectedValue != "0")
            {
                sql += " and Type='" + ddlType.SelectedValue + "'";
            }
            return sql;
        }

        public string get_pr_state(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "未下推";
            }
            else if (i == "1")
            {
                state = "已下推";
            }


            return state;
        }

        public string get_spzt(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "初始化";
            }
            else if (i == "1")
            {
                state = "提交未审批";
            }
            else if (i == "2")
            {
                state = "审批中";
            }
            else if (i == "3")
            {
                state = "已通过";
            }
            else if (i == "4")
            {
                state = "已驳回";
            }
            return state;
        }
        protected void tb_pjinfo_Textchanged(object sender, EventArgs e)
        {
            string pjname = "";
            string pjid = "";
            if (tb_pjinfo.Text.ToString().Contains("|"))
            {
                string[] strs = tb_pjinfo.Text.Split('|');
                pjname = strs[0];
                pjid = strs[1];

                tb_enginfo.Text = strs[2];
                tb_pjinfo.Text = pjid;
                tb_htid.Text = pjid;

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请正确填写任务号！');", true);
            }
        }

        protected void ddlType_Search(object sender, EventArgs e)
        {
            bindData();
        }
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            List<string> sqllt = new List<string>();

            string sqltext;
            string shape;
            string pcode;
            if (isselected())
            {
                string end_pi_id = "";
                if (tb_pjinfo.Text != "")
                {
                    string sqltext1 = "select top 1 * from (select * from  View_TM_BOMAllLotNum where INDEX_ID like '%" + tb_pjinfo.Text.ToString() + "'+'.XZ%')t order by INDEX_ID desc";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext1);
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            int index = Convert.ToInt32(dr["INDEX_ID"].ToString().Split('/')[1]) + 1;

                            end_pi_id = index.ToString();
                            end_pi_id = end_pi_id.PadLeft(3, '0');
                        }
                        dr.Close();
                    }
                    else
                    {
                        end_pi_id = "001";
                    }
                    pcode = tb_pjinfo.Text + "." + "XZ/" + end_pi_id;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('任务号不能为空！');", true);
                    return;
                }
                if (DropDownList1.SelectedIndex == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择物料类型！');", true);
                    return;
                }
                else
                {
                    shape = DropDownList1.SelectedItem.Text.ToString();
                }
                sqltext = "INSERT INTO TBPC_OTPURRVW(MP_PCODE,MP_SUBMITID,MP_SUBMITTM,MP_USEDEPID,MP_PJID,MP_ENGID,MP_SHAPE,MP_SQRENID,MP_REVIEWA,MP_NOTE) " +
                                          "VALUES('" + pcode + "' ,'" + Session["UserID"].ToString() + "'," +
                                          "'" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'," +
                                          "'" + Session["UserDeptID"].ToString() + "','" + tb_pjinfo.Text + "','" + tb_enginfo.Text + "','" + shape + "','" + Session["UserID"].ToString() + "','" + Session["UserID"].ToString() + "','')";

                sqllt.Add(sqltext);

                foreach (GridViewRow gr in GridView1.Rows)
                {
                    string marid = gr.Cells[2].Text.Trim();
                    double length = 0;
                    double width = 0;
                    string tuhao = "";
                    double num = 0;
                    try
                    {
                        num = Convert.ToDouble(gr.Cells[7].Text.Trim()) - Convert.ToDouble(gr.Cells[11].Text.Trim());
                    }
                    catch
                    {
                        num = 0;
                    }

                    double fznum = 0;
                    string note = "";
                    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    string tag_pi_id_1 = "";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText("select * from TBPM_TCTSASSGN where TSA_ID='" + tb_htid.Text + "'");
                    string eng = "无";
                    if (dt.Rows.Count > 0)
                    {
                        eng = dt.Rows[0]["TSA_ENGNAME"].ToString();
                    }
                    string TSA_PJID = dt.Rows[0]["TSA_PJID"].ToString();
                    string P = pcode.Replace('.', '_');
                    tag_pi_id_1 = string.Format("{0}({1})_{2}_{3}", tb_enginfo.Text, TSA_PJID, eng, P);
                    string ptcode = tag_pi_id_1 + "_" + (gr.RowIndex + 1).ToString().PadLeft(4, '0');
                    CheckBox cbx = gr.FindControl("CheckBox1") as CheckBox;
                    if (cbx.Checked)
                    {
                        sqltext = "INSERT INTO TBPC_OTPURPLAN(MP_PCODE,MP_PTCODE,MP_MARID,MP_WIDTH,MP_LENGTH,MP_TUHAO,MP_NUMBER,MP_FZNUM,MP_NOTE,MP_TIMERQ) " +
                                  "VALUES('" + pcode + "','" + ptcode + "','" + marid + "'," + width + "," +
                                   "" + length + ",'" + tuhao + "'," + num + "," + fznum + ",'" + note + "','" + time + "')";
                        sqllt.Add(sqltext);

                        sqltext = "update TBWS_STORAGE_WARN set PTC='" + ptcode + "' where MARID='" + marid + "'";
                        sqllt.Add(sqltext);

                    }
                }

                DBCallCommon.ExecuteTrans(sqllt);

                Response.Redirect("~/PC_Data/PC_TBPC_Otherpur_Bill_List.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要复制的数据！');", true);
            }
        }
        //判断是否选择数据
        protected bool isselected()
        {
            int count = 0;
            bool temp = false;
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox cbx = gr.FindControl("CheckBox1") as CheckBox;//定义checkbox
                if (cbx.Checked)
                {
                    count++;
                }
            }
            if (count > 0)
            {
                temp = true;
            }
            return temp;
        }


        protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                double warning = CommonFun.ComTryDouble(e.Row.Cells[8].Text.ToString());
                double storageNum = CommonFun.ComTryDouble(e.Row.Cells[11].Text.ToString());

                if (storageNum < warning)
                {
                    e.Row.BackColor = Color.Yellow;
                }
               
            }

            //for (int i = 1; i < 15; i++)
            //{
            //    e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
            //    e.Row.Cells[i].Attributes.Add("title", "双击修改原始数据");


            //}
        }

    }
}
