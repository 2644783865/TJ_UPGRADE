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
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Out_Source : System.Web.UI.Page
    {
        string sqlText;
        string viewouttable;
        string[] fields;
        string tablename;
        string tableRV;
        string strutablenm;
        string viewtable;
        string auditlist;
        string code;
        string codeRV;
        protected void Page_Load(object sender, EventArgs e)
        {
            btn_concel.Attributes.Add("OnClick", "Javascript:return confirm('确定取消,执行此操作将取消选中的材料计划?');");
            btn_concel.Click += new EventHandler(btn_concel_Click);
            if (!IsPostBack)
            {
                btn_gosh.Attributes.Add("style", "display:none");
                InitInfo();
            }
        }

        private void InitInfo()
        {
            btn_concel.Visible = true;
            if (Request.QueryString["id"] != null)    //第一次提外协
            {
                outsource_no.Value = Request.QueryString["id"];
                fields = Request.QueryString["id"].ToString().Split('.');
                tsa_id.Text = fields[0].ToString();
                status.Value = "0";
            }
            else                      
            {
                if (Request.QueryString["OSTdetail_id"] != null)   //外协查看
                {
                    fields = Request.QueryString["OSTdetail_id"].ToString().Split('.');
                    tsa_id.Text = fields[0].ToString();
                    outsource_no.Value=fields[0].ToString()+'.'+fields[1].ToString();
                    status.Value = fields[2].ToString();
                    if (int.Parse(status.Value) > 1)  // 表示提交过审核申请
                    {
                        Response.Redirect("TM_Out_Source_Audit.aspx?id=" + outsource_no.Value+"&lk=1");
                    }
                }
                else                                     //外协编辑
                {
                    fields = Request.QueryString["OSTedit_id"].ToString().Split('.');
                    tsa_id.Text = fields[0].ToString();
                    outsource_no.Value = fields[0].ToString() + '.' + fields[1].ToString();
                    status.Value = fields[2].ToString();
                }
            }
            sqlText = "select TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE from View_TM_TaskAssign";
            sqlText += " where TSA_ID='" + tsa_id.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                lab_proname.Text = dr[0].ToString();
                lab_engname.Text = dr[1].ToString();
                wx_list.Value = dr[2].ToString();//工程类型
            }
            dr.Close();
            GetOutList();
        }

        private void InitList()
        {
            fields = outsource_no.Value.Split('.');
            string[] cols = fields[1].ToString().Split('/');
            if (cols[0].ToString().Length > 6)
            {
                tablename = "TBPM_OUTSCHANGE";
                tableRV = "TBPM_OUTSCHANGERVW";
                codeRV = "OST_CHANGECODE";
                viewtable = "View_TM_OUTSCHANGE";
                code = "OST_CHANGECODE";
            }
            else
            {
                tablename = "TBPM_OUTSOURCELIST";
                tableRV = "TBPM_OUTSOURCETOTAL";
                codeRV = "OST_OUTSOURCENO";
                viewtable = "View_TM_OUTSOURCELIST";
                code = "OSL_OUTSOURCENO";
            }
            #region
            switch (wx_list.Value)
            {

                case "回转窑":
                    strutablenm = "TBPM_STRINFOHZY";
                    break;
                case "球、立磨":
                    strutablenm = "TBPM_STRINFOQLM";
                    break;
                case "篦冷机":
                    strutablenm = "TBPM_STRINFOBLJ";
                    break;
                case "堆取料机":
                    strutablenm = "TBPM_STRINFODQLJ";
                    break;
                case "钢结构及非标":
                    strutablenm = "TBPM_STRINFOGFB";
                    break;
                case "电气及其他":
                    strutablenm = "TBPM_STRINFODQO";
                    break;
            }
            #endregion
        }
        /// <summary>
        /// DataPanel
        /// </summary>
        private void InitDataPanel(GridView grv)
        {
            if (grv.Rows.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
            }
        }

        /// <summary>
        /// 绑定数据 
        /// </summary>
        private void GetOutList()
        {
            InitList();
            sqlText = "select * from "+viewtable +" ";
            sqlText += "where OSL_OUTSOURCENO='" + outsource_no.Value + "' ";

            if (ckbShowZero.Checked)
            {
                sqlText += "and (case when  LOWER(OSL_PURCUNIT)='吨' or  LOWER(OSL_PURCUNIT)='t' or  LOWER(OSL_PURCUNIT)='kg' or  LOWER(OSL_PURCUNIT)='千克' or  LOWER(OSL_PURCUNIT)='公斤' THEN OSL_TOTALWGHTL ELSE OSL_NUMBER END)=0 ";
            }

            sqlText += " order by dbo.f_FormatSTR(OSL_OLDXUHAO,'.')";
            DBCallCommon.BindGridView(GridView1, sqlText);
            InitDataPanel(GridView1);
        }

        protected void ckbShowZero_OnCheckedChanged(object sender, EventArgs e)
        {
            this.GetOutList();
        }


        protected void btn_concel_Click(object sender, EventArgs e)
        {
            this.InitList();
            string xuhao = "";
            int temp = 0;
            temp = checkselect();
            List<string> sqltextlist = new List<string>();
            if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择任何数据,本次操作无效！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择了已提交的数据,不能再做修改,本次操作无效！');", true);
            }
            else
            {
                for (int j = 0; j <= GridView1.Rows.Count - 1; j++)
                {
                    GridViewRow grow = GridView1.Rows[j];
                    CheckBox cbox = (CheckBox)grow.FindControl("CheckBox1");
                    if (cbox.Checked == true)
                    {
                        xuhao = ((Label)grow.FindControl("lblxuhao")).Text.ToString();//序号
                        if (!outsource_no.Value.Contains(" WXQX/"))  //对于取消的批号，不进行原始数据操作
                        {
                            sqlText = "update  " + strutablenm + "  set BM_OSSTATE='0',BM_OSREVIEW='0'";
                            sqlText += "where BM_ENGID='" + tsa_id.Text + "' and BM_XUHAO='" + xuhao + "'";
                            sqltextlist.Add(sqlText);
                        }
                        sqlText = "delete from  " + tablename + "  " +
                                  "where "+code+"='" + outsource_no.Value + "' and OSL_ENGID='" + tsa_id.Text + "' and  OSL_NEWXUHAO='" + xuhao + "'";
                        sqltextlist.Add(sqlText);
                        //判断此批号中的任务是否都取消，是：删除总表批号
                        sqlText = "exec verify '" + tablename + "','" + code + "','" + tableRV + "','" + codeRV + "','" + outsource_no.Value + "'";
                        sqltextlist.Add(sqlText);
                    }
                }
                DBCallCommon.ExecuteTrans(sqltextlist);
                GetOutList();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');", true);
            }
        }

        private int checkselect()
        {
            int temp = 0;
            int i = 0;
            int k = 0;
            for (int j = 0; j <= GridView1.Rows.Count - 1; j++)
            {
                GridViewRow grow = GridView1.Rows[j];
                CheckBox cbox = (CheckBox)grow.FindControl("CheckBox1");
                if (cbox.Checked == true)
                {
                    i++;
                    if (status.Value=="2"||status.Value=="4"||status.Value=="6"||status.Value=="8")
                    {
                        k++;
                        break;
                    }
                }
            }
            if (i == 0)
            {
                temp = 1;
            }
            else if (k > 0)
            {
                temp = 2;
            }
            return temp;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsave_Click(object sender, EventArgs e)
        {

            string index;
            string md_request;
            string time_request;
            string place;
            string lab_ID;
            string tracknum;
            string pici;
            string pjEng = this.GetPorjEngName(tsa_id.Text);
            List<string> list_sql = new List<string>();
            fields = outsource_no.Value.Split('.');
            string[] cols = fields[1].ToString().Split('/');
            if (cols[0].ToString().Length > 6)     //变更
            {
                tablename = "TBPM_OUTSCHANGE";
                auditlist = "TBPM_OUTSCHANGERVW";
                pici = "WXBG" + cols[2].ToString().PadLeft(2, '0');
                code = "OST_CHANGECODE";
                viewouttable = "View_TM_OUTSCHANGE";
            }
            else
            {
                tablename = "TBPM_OUTSOURCELIST";
                auditlist = "TBPM_OUTSOURCETOTAL";
                pici = "WX" + cols[2].ToString().PadLeft(2, '0');
                code = "OST_OUTSOURCENO";
                viewouttable = "View_TM_OUTSOURCELIST";
            }



            string sql_check_num = "select * from " + viewouttable + " where OSL_OUTSOURCENO='" + outsource_no.Value + "' AND  (case when  LOWER(OSL_PURCUNIT)='吨' or  LOWER(OSL_PURCUNIT)='t' or  LOWER(OSL_PURCUNIT)='kg' or  LOWER(OSL_PURCUNIT)='千克' or  LOWER(OSL_PURCUNIT)='公斤' THEN OSL_TOTALWGHTL ELSE OSL_NUMBER END)=0";
            System.Data.SqlClient.SqlDataReader dr_sql_check_num = DBCallCommon.GetDRUsingSqlText(sql_check_num);

            if (dr_sql_check_num.HasRows)
            {
                dr_sql_check_num.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法保存！\\r\\r存在数量或重量为零的项，无法采购！！！\\r\\r请选择【不可提交计划项】，取消后修改计划！！！');", true);
                return;
            }
            dr_sql_check_num.Close();

            if (GridView1.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法保存！\\r\\r没有数据！！！');", true);
                return;
            }

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                index = ((Label)gr.FindControl("Index")).Text.Trim().PadLeft(4, '0');
                tracknum = pjEng + '_' + tsa_id.Text + '_' + outsource_no.Value.Split('.')[1].Split('_')[1] + '_' + index;   //计划跟踪号
                lab_ID = ((Label)gr.FindControl("lab_ID")).Text;
                md_request = ((HtmlInputText)gr.FindControl("txt_request")).Value;
                time_request = ((HtmlInputText)gr.FindControl("txt_time")).Value;
                place = ((HtmlInputText)gr.FindControl("txt_place")).Value;

                sqlText = "update " + tablename + " set OSL_REQUEST='" + md_request + "',";
                sqlText += "OSL_REQDATE='" + time_request + "',OSL_DELSITE='" + place + "',";
                sqlText += "OSL_TRACKNUM='" + tracknum + "' where OSL_ID='" + lab_ID + "'";
                list_sql.Add(sqlText);
            }

            if (status.Value == "0" || status.Value == "1" || status.Value == "3" || status.Value == "5" || status.Value == "7")
            {
                sqlText = "update " + auditlist + " set OST_STATE='1',OST_CHECKLEVEL='3',";
                sqlText += "OST_MDATE='',OST_REVIEWERA='',";
                sqlText += "OST_REVIEWAADVC='',OST_REVIEWADATE='',OST_REVIEWERB='',";
                sqlText += "OST_REVIEWBADVC='',OST_REVIEWBDATE='',";
                sqlText += "OST_REVIEWERC='',OST_REVIEWCADVC='',";
                sqlText += "OST_REVIEWCDATE='',OST_ADATE='' ";
                sqlText += "where " + code + "='" + outsource_no.Value + "' and OST_STATE='" + status.Value + "'";
                list_sql.Add(sqlText);
            }

            DBCallCommon.ExecuteTrans(list_sql);
            if (status.Value == "0" || status.Value == "1" || status.Value == "3" || status.Value == "5" || status.Value == "7")
            {
                btnCheck.Visible = true;
            }
            Page.ClientScript.RegisterStartupScript(this.GetType(), "test", "<script language=javascript>if(confirm('提示:保存成功!\\r\\r是否进入审核界面?'))document.getElementById('" + btn_gosh.ClientID + "').click();</script>");
        }

        protected void btn_gosh_Click(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法下推审核！\\r\\r没有数据！！！');", true);
                return;
            }
            Response.Redirect("TM_Out_Source_Audit.aspx?id=" + outsource_no.Value);
        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            if (GridView1.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('无法下推审核！\\r\\r没有数据！！！');", true);
                return;
            }
            Response.Redirect("TM_Out_Source_Audit.aspx?id=" + outsource_no.Value);
        }
        protected void btnreturn_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }
        /// <summary>
        /// 保存按钮和下推审核按钮的可见性控制
        /// </summary>
        /// <param name="checkstate"></param>
        private void btnSaveCheckVisible(string checkstate)
        {
            // 审核状态 审核状态 初始化为0，1为保存，2为提交，3为一级驳回，4为一级通过，5为二级驳回，6为二级通过，7为三级驳回，8为三级通过
            switch (checkstate)
            {
                //未提交状态\驳回状态
                case "0":
                case "1":
                case "3":
                case "5":
                case "7":
                    break;
                //审核中级审核通过状态
                default:
                    btnsave.Visible = false;
                    btnCheck.Visible = false;
                    break;
            }
        }

        /// <summary>
        /// 根据生产制号查询项目工程，返回：项目(项目简称)_工程名称
        /// </summary>
        /// <param name="taskid"></param>
        /// <returns></returns>
        private string GetPorjEngName(string taskid)
        {
            string retVal = "";
            string sql = "select TSA_PJNAME+'('+TSA_PJID+')_'+TSA_ENGNAME AS ProjEng from View_TM_TaskAssign where TSA_ID='" + taskid + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            dr.Read();
            retVal = dr["ProjEng"].ToString();
            dr.Close();
            return retVal;
        }



    }
}
