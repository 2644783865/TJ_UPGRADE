using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Paint_Scheme_Create : System.Web.UI.Page
    {
        string sqlText;
        string ps_no;
        string ps_pici;
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
            }
        }
        //初始化页面
        private void InitInfo()
        {
            string pihao = "";
            //第一次提油漆方案
            if (Request.QueryString["id"] == null)
            {
                if (Request.QueryString["pseditid"] == null) //油漆方案查看
                {
                    if (Request.QueryString["add"] != null)
                    {
                        tsaid.Text = Request.QueryString["add"];
                    }
                    InitGrid();

                }
                else //油漆方案编辑(驳回时才能编辑)
                {
                    pihao = Request.QueryString["pseditid"].ToString();
                    string[] fields = pihao.Split('.');
                    tsaid.Text = Request.QueryString["edit"];
                    ps_no = fields[0].ToString() + '.' + fields[1].ToString();
                    string status = fields[2].ToString();
                    if (status == "3" || status == "5" || status == "7")
                    {
                        sqlText = "select * from View_TM_PAINTSCHEMEDETAIL where PS_PID='" + ps_no + "' ";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                        for (int i = 0; i < 10; i++)
                        {
                            DataRow newRow = dt.NewRow();
                            dt.Rows.Add(newRow);
                        }
                        Repeater1.DataSource = dt;
                        Repeater1.DataBind();


                    }
                }

            }
            else
            {
                pihao = Request.QueryString["id"].ToString();
                string[] fields = pihao.Split('.');
                tsaid.Text = Request.QueryString["edit"];
                ps_no = fields[0].ToString() + '.' + fields[1].ToString();
                string status = fields[2].ToString();
                if (int.Parse(status) > 1)
                {
                    Response.Redirect("TM_Paint_Scheme_Audit.aspx?id=" + ps_no);
                }
                sqlText = "select * from View_TM_PAINTSCHEMEDETAIL where PS_PID='" + ps_no + "' ";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
                for (int i = 0; i < 10; i++)
                {
                    DataRow newRow = dt.NewRow();
                    dt.Rows.Add(newRow);
                }
                Repeater1.DataSource = dt;
                Repeater1.DataBind();

            }


            sqlText = "select CM_PROJ,TSA_ENGNAME,TSA_PJID ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsaid.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                lab_proname.Text = dr[0].ToString();
                paint_contract.Text = dr[2].ToString();
                lab_engname.Text = dr[1].ToString();

            }
            dr.Close();
            GetSheBei();

            sqlText = "select PS_MAP,PS_PAINTBRAND from TBPM_PAINTSCHEME where PS_ID='" + ps_no + "'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlText);
            if (dt1.Rows.Count > 0)
            {
                ddlSheBei.SelectedValue = dt1.Rows[0][0].ToString();
                txtBrand.Text = dt1.Rows[0][1].ToString();
            }
        }



        private void GetSheBei()
        {
            sqlText = "select TSA_MAP+'|'+TSA_ENGNAME as ENGNAME,TSA_MAP from View_CM_Task where CM_CONTR='" + paint_contract.Text + "'";
            string dataText = "ENGNAME";
            string dataValue = "TSA_MAP";
            DBCallCommon.BindDdl(ddlSheBei, sqlText, dataText, dataValue);
        }

        /// <summary>
        /// 初始化GridView
        /// </summary>
        private void InitGrid()
        {
            string columns = "PS_ID,PS_PID,PS_NAME,PS_PJID,PS_ENGID,PS_LEVEL,PS_STATE,PS_TUHAO,PS_MIANJI,PS_BOTMARID,PS_BOTSHAPE,PS_BOTYONGLIANG,PS_BOTHOUDU,PS_BOTXISHIJI,PS_BOTMWEIGHT,PS_MIDMARID,PS_MIDSHAPE,PS_MIDYONGLIANG,PS_MIDHOUDU,PS_MIDXISHIJI,PS_MIDMWEIGHT,PS_TOPMARID,PS_TOPSHAPE,PS_TOPYONGLIANG,PS_TOPHOUDU,PS_TOPXISHIJI,PS_TOPMWEIGHT,PS_TOPCOLOR,PS_TOPCOLORLABEL,PS_TOTALHOUDU,PS_BEIZHU,PS_BGBEIZHU";
            DataTable dt = new DataTable();
            for (int i = 0; i < columns.Split(',').Length; i++)
            {
                dt.Columns.Add(columns.Split(',')[i]);
            }
            for (int i = Repeater1.Items.Count; i < 20; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
        }

        //导入
        protected void btnInput_Click(object sender, EventArgs e)
        {
            if (txtInputNum.Text == "" || txtInputPihao.Text.ToString() == "")
            {
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:请输入导入倍数及源数据');", true);
                Response.Write("<script>alert('提示:请输入导入倍数及源数据');</script>");

            }
            else
            {
                string num = txtInputNum.Text.Trim();
                System.Data.DataTable dt = GetDataFromGrid("import");
                sqlText = "select  PS_ENGID,PS_TUHAO,PS_NAME,PS_LEVEL,cast(cast(PS_MIANJI as float)*" + num + " as varchar(50)) as PS_MIANJI,PS_BOTMARID,PS_BOTSHAPE,PS_BOTHOUDU,cast(cast(PS_BOTYONGLIANG as float)*" + num + " as varchar(50)) as PS_BOTYONGLIANG,cast(cast(PS_BOTXISHIJI as float)*" + num + " as varchar(50)) as PS_BOTXISHIJI,cast(PS_BOTMWEIGHT as varchar(50)) as PS_BOTMWEIGHT,PS_MIDMARID,PS_MIDSHAPE,PS_MIDHOUDU,cast(cast(PS_MIDYONGLIANG as float)*" + num + " as varchar(50)) as PS_MIDYONGLIANG,cast(cast(PS_MIDXISHIJI as float)*" + num + " as varchar(50)) as PS_MIDXISHIJI,cast(PS_MIDMWEIGHT as varchar(50)) as PS_MIDMWEIGHT,PS_TOPMARID,PS_TOPSHAPE,PS_TOPHOUDU,cast(cast(PS_TOPYONGLIANG as float)*" + num + " as varchar(50)) as PS_TOPYONGLIANG,cast(cast(PS_TOPXISHIJI as float)*" + num + " as varchar(50)) as PS_TOPXISHIJI,cast(PS_TOPMWEIGHT as varchar(50)) as PS_TOPMWEIGHT,PS_TOPCOLOR,PS_TOPCOLORLABEL,PS_TOTALHOUDU,PS_BEIZHU,PS_BGBEIZHU ";
                sqlText += "from View_TM_PAINTSCHEMEDETAIL where PS_PID='" + txtInputPihao.Text + "'";
                try
                {
                    DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqlText);
                    dt.Merge(dt1, true);
                    for (int i = 0; i < 10; i++)
                    {
                        DataRow newRow = dt.NewRow();
                        dt.Rows.Add(newRow);
                    }
                    this.Repeater1.DataSource = dt;
                    this.Repeater1.DataBind();
                }
                catch (Exception)
                {

                    //  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:导入数据有误，存在类型转换错误！');", true);
                    Response.Write("<script>alert('提示:导入数据有误，存在类型转换错误！');</script>");
                }

            }
        }

        private DataTable GetDataFromGrid(string type)
        {
            string columns = "PS_ENGID,PS_TUHAO,PS_NAME,PS_LEVEL,PS_MIANJI,PS_BOTMARID,PS_BOTSHAPE,PS_BOTHOUDU,PS_BOTYONGLIANG,PS_BOTXISHIJI,PS_BOTMWEIGHT,PS_MIDMARID,PS_MIDSHAPE,PS_MIDHOUDU,PS_MIDYONGLIANG,PS_MIDXISHIJI,PS_MIDMWEIGHT,PS_TOPMARID,PS_TOPSHAPE,PS_TOPHOUDU,PS_TOPYONGLIANG,PS_TOPXISHIJI,PS_TOPMWEIGHT,PS_TOPCOLOR,PS_TOPCOLORLABEL,PS_TOTALHOUDU,PS_BEIZHU,PS_BGBEIZHU";
            DataTable dt = new DataTable();
            for (int i = 0; i < columns.Split(',').Length; i++)
            {
                dt.Columns.Add(columns.Split(',')[i]);
            }
            foreach (RepeaterItem repeat in Repeater1.Items)
            {
                string partName = ((HtmlInputText)repeat.FindControl("partName")).Value;
                if (type == "insert" || (partName != "" && type == "import"))
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((HtmlInputText)repeat.FindControl("txtTaskID")).Value;
                    newRow[1] = ((HtmlInputText)repeat.FindControl("tuhao")).Value;
                    newRow[2] = ((HtmlInputText)repeat.FindControl("partName")).Value;
                    newRow[3] = ((HtmlInputText)repeat.FindControl("level")).Value;
                    newRow[4] = ((HtmlInputText)repeat.FindControl("txtMianJi")).Value;
                    newRow[5] = ((TextBox)repeat.FindControl("txtBotMarid")).Text;
                    newRow[6] = ((HtmlInputText)repeat.FindControl("txtBotShape")).Value;
                    newRow[7] = ((HtmlInputText)repeat.FindControl("txtBotHouDu")).Value;
                    newRow[8] = ((HtmlInputText)repeat.FindControl("txtBotYongLiang")).Value;
                    newRow[9] = ((HtmlInputText)repeat.FindControl("txtBotXiShiJi")).Value;
                    newRow[10] = ((HtmlInputText)repeat.FindControl("txtBotTu")).Value;
                    newRow[11] = ((TextBox)repeat.FindControl("txtMidMarid")).Text;
                    newRow[12] = ((HtmlInputText)repeat.FindControl("txtMidShape")).Value;
                    newRow[13] = ((HtmlInputText)repeat.FindControl("txtMidHouDu")).Value;
                    newRow[14] = ((HtmlInputText)repeat.FindControl("txtMidYongLiang")).Value;
                    newRow[15] = ((HtmlInputText)repeat.FindControl("txtMidXiShiJi")).Value;
                    newRow[16] = ((HtmlInputText)repeat.FindControl("txtMidTu")).Value;
                    newRow[17] = ((TextBox)repeat.FindControl("txtTopMarid")).Text;
                    newRow[18] = ((HtmlInputText)repeat.FindControl("txtTopShape")).Value;
                    newRow[19] = ((HtmlInputText)repeat.FindControl("txtTopHouDu")).Value;
                    newRow[20] = ((HtmlInputText)repeat.FindControl("txtTopYongLiang")).Value;
                    newRow[21] = ((HtmlInputText)repeat.FindControl("txtTopXiShiJi")).Value;
                    newRow[22] = ((HtmlInputText)repeat.FindControl("txtTopTu")).Value;
                    newRow[23] = ((HtmlInputText)repeat.FindControl("txtTopColor")).Value;
                    newRow[24] = ((HtmlInputText)repeat.FindControl("txtTopColorLabel")).Value;
                    newRow[25] = ((HtmlInputText)repeat.FindControl("txtTotalHouDu")).Value;
                    newRow[26] = ((HtmlInputText)repeat.FindControl("txtBeiZhu")).Value;
                    newRow[27] = ((HtmlInputText)repeat.FindControl("txtBGBeiZhu")).Value;
                    dt.Rows.Add(newRow);
                }
            }
            return dt;
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnsave_Click(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            if (Request.QueryString["pseditid"] == null)
            {
                if (ddlSheBei.SelectedIndex == 0)
                {
                    // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:请选择图号');", true);
                    Response.Write("<script>alert('提示:请选择设备名称');</script>");
                    return;
                }

                #region
                psNum();//油漆涂装方案批号
                sqlText = "select count(*) from TBPM_PAINTSCHEME where PS_ID='" + ps_no + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                if (dr.Read())
                {
                    count = int.Parse(dr[0].ToString());
                }
                dr.Close();
                sqlText = "select TSA_YQREVIEWERID from TBPM_TCTSASSGN where TSA_ID='" + tsaid.Text + "'";
                DataTable ReviewA = DBCallCommon.GetDTUsingSqlText(sqlText);
                string reviewA = "";
                if (ReviewA.Rows.Count > 0)
                {
                    reviewA = ReviewA.Rows[0][0].ToString();
                }
                if (count == 0)
                {
                    sqlText = "insert into TBPM_PAINTSCHEME ";
                    sqlText += "(PS_ID,PS_PJID,PS_ENGID,";
                    sqlText += "PS_SUBMITID,PS_STATE,PS_CHECKLEVEL,PS_REVIEWA,PS_MAP,PS_PAINTBRAND) VALUES ('" + ps_no + "','" + paint_contract.Text + "',";
                    sqlText += "'" + tsaid.Text + "',";
                    sqlText += "'" + Session["UserID"] + "','1','2','" + reviewA + "','" + ddlSheBei.SelectedValue + "','" + txtBrand.Text.Trim() + "')";
                    list_sql.Add(sqlText);
                }
                else
                {
                    sqlText = "update TBPM_PAINTSCHEME set PS_REVIEWA='" + reviewA + "', PS_MAP='" + ddlSheBei.SelectedValue + "',PS_PAINTBRAND='" + txtBrand.Text.Trim() + "' where PS_ID='" + ps_no + "'";
                    list_sql.Add(sqlText);
                }
                //删除明细表中该批号记录
                sqlText = "delete from  TBPM_PAINTSCHEMELIST where PS_PID='" + ps_no + "'";
                list_sql.Add(sqlText);
                //重新插入明细
                foreach (RepeaterItem repeat in Repeater1.Items)
                {
                    string partName = ((HtmlInputText)repeat.FindControl("partName")).Value;
                    if (partName != "")
                    {


                        string tuhao = ((HtmlInputText)repeat.FindControl("tuhao")).Value;
                        string taskId = ((HtmlInputText)repeat.FindControl("txtTaskID")).Value;
                        string level = ((HtmlInputText)repeat.FindControl("level")).Value;
                        string txtMianJi = ((HtmlInputText)repeat.FindControl("txtMianJi")).Value;
                        // string txtBotShape = ((TextBox)repeat.FindControl("txtBotShape")).Text;
                        string txtBotMarid = ((TextBox)repeat.FindControl("txtBotMarid")).Text;
                        string txtBotHouDu = ((HtmlInputText)repeat.FindControl("txtBotHouDu")).Value;
                        string txtBotYongLiang = ((HtmlInputText)repeat.FindControl("txtBotYongLiang")).Value;
                        string txtBotXiShiJi = ((HtmlInputText)repeat.FindControl("txtBotXiShiJi")).Value;
                        //  string txtMidShape = ((HtmlInputText)repeat.FindControl("txtMidShape")).Value;
                        string txtMidMarid = ((TextBox)repeat.FindControl("txtMidMarid")).Text;
                        string txtMidHouDu = ((HtmlInputText)repeat.FindControl("txtMidHouDu")).Value;
                        string txtMidYongLiang = ((HtmlInputText)repeat.FindControl("txtMidYongLiang")).Value;
                        string txtMidXiShiJi = ((HtmlInputText)repeat.FindControl("txtMidXiShiJi")).Value;
                        //  string txtTopShape = ((HtmlInputText)repeat.FindControl("txtTopShape")).Value;
                        string txtTopMarid = ((TextBox)repeat.FindControl("txtTopMarid")).Text;
                        string txtTopHouDu = ((HtmlInputText)repeat.FindControl("txtTopHouDu")).Value;
                        string txtTopYongLiang = ((HtmlInputText)repeat.FindControl("txtTopYongLiang")).Value;
                        string txtTopXiShiJi = ((HtmlInputText)repeat.FindControl("txtTopXiShiJi")).Value;
                        string txtTopColor = ((HtmlInputText)repeat.FindControl("txtTopColor")).Value;
                        string txtTopColorLabel = ((HtmlInputText)repeat.FindControl("txtTopColorLabel")).Value;
                        string txtTotalHouDu = ((HtmlInputText)repeat.FindControl("txtTotalHouDu")).Value;
                        string txtBeiZhu = ((HtmlInputText)repeat.FindControl("txtBeiZhu")).Value;
                        string BGBeiZhu = ((HtmlInputText)repeat.FindControl("txtBGBeiZhu")).Value;
                        sqlText = "insert into TBPM_PAINTSCHEMELIST values ('" + ps_no + "','" + partName + "','" + paint_contract.Text + "','" + taskId + "','" + level + "','1','" + tuhao + "','" + txtMianJi + "','" + txtBotMarid + "','" + txtBotYongLiang + "','" + txtBotHouDu + "','" + txtBotXiShiJi + "','" + txtMidMarid + "','" + txtMidYongLiang + "','" + txtMidHouDu + "','" + txtMidXiShiJi + "','" + txtTopMarid + "','" + txtTopYongLiang + "','" + txtTopHouDu + "','" + txtTopXiShiJi + "','" + txtTopColor + "','" + txtTopColorLabel + "','" + txtTotalHouDu + "','" + txtBeiZhu + "','" + BGBeiZhu + "')";
                        list_sql.Add(sqlText);
                    }
                }
                DBCallCommon.ExecuteTrans(list_sql);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:油漆计划保存成功!');location.href='" + "TM_Paint_Scheme_Audit.aspx?id=" + ps_no + "'", true);
                Response.Write("<script>alert('提示:油漆计划保存成功!');location.href='" + "TM_Paint_Scheme_Audit.aspx?id=" + ps_no + "'</script>");
                #endregion
            }
            else//驳回修改
            {
                #region
                string rejectid = Request.QueryString["pseditid"].ToString().Substring(0, Request.QueryString["pseditid"].ToString().LastIndexOf('.'));

                //删除明细表中该批号记录
                sqlText = "delete from  TBPM_PAINTSCHEMELIST where PS_PID='" + rejectid + "'";
                list_sql.Add(sqlText);

                foreach (RepeaterItem repeat in Repeater1.Items)
                {
                    string partName = ((HtmlInputText)repeat.FindControl("partName")).Value;
                    if (partName != "")
                    {
                        string taskId = ((HtmlInputText)repeat.FindControl("txtTaskID")).Value;
                        string tuhao = ((HtmlInputText)repeat.FindControl("tuhao")).Value;
                        string level = ((HtmlInputText)repeat.FindControl("level")).Value;
                        string txtMianJi = ((HtmlInputText)repeat.FindControl("txtMianJi")).Value;
                        string txtBotMarid = ((TextBox)repeat.FindControl("txtBotMarid")).Text;
                        string txtBotHouDu = ((HtmlInputText)repeat.FindControl("txtBotHouDu")).Value;
                        string txtBotYongLiang = ((HtmlInputText)repeat.FindControl("txtBotYongLiang")).Value;
                        string txtBotXiShiJi = ((HtmlInputText)repeat.FindControl("txtBotXiShiJi")).Value;
                        string txtMidMarid = ((TextBox)repeat.FindControl("txtMidMarid")).Text;
                        string txtMidHouDu = ((HtmlInputText)repeat.FindControl("txtMidHouDu")).Value;
                        string txtMidYongLiang = ((HtmlInputText)repeat.FindControl("txtMidYongLiang")).Value;
                        string txtMidXiShiJi = ((HtmlInputText)repeat.FindControl("txtMidXiShiJi")).Value;
                        string txtTopMarid = ((TextBox)repeat.FindControl("txtTopMarid")).Text;
                        string txtTopHouDu = ((HtmlInputText)repeat.FindControl("txtTopHouDu")).Value;
                        string txtTopYongLiang = ((HtmlInputText)repeat.FindControl("txtTopYongLiang")).Value;
                        string txtTopXiShiJi = ((HtmlInputText)repeat.FindControl("txtTopXiShiJi")).Value;
                        string txtTopColor = ((HtmlInputText)repeat.FindControl("txtTopColor")).Value;
                        string txtTopColorLabel = ((HtmlInputText)repeat.FindControl("txtTopColorLabel")).Value;
                        string txtTotalHouDu = ((HtmlInputText)repeat.FindControl("txtTotalHouDu")).Value;
                        string txtBeiZhu = ((HtmlInputText)repeat.FindControl("txtBeiZhu")).Value;
                        string BGBeiZhu = ((HtmlInputText)repeat.FindControl("txtBGBeiZhu")).Value;
                        sqlText = "insert into TBPM_PAINTSCHEMELIST values ('" + rejectid + "','" + partName + "','" + paint_contract.Text + "','" + taskId + "','" + level + "','1','" + tuhao + "','" + txtMianJi + "','" + txtBotMarid + "','" + txtBotYongLiang + "','" + txtBotHouDu + "','" + txtBotXiShiJi + "','" + txtMidMarid + "','" + txtMidYongLiang + "','" + txtMidHouDu + "','" + txtMidXiShiJi + "','" + txtTopMarid + "','" + txtTopYongLiang + "','" + txtTopHouDu + "','" + txtTopXiShiJi + "','" + txtTopColor + "','" + txtTopColorLabel + "','" + txtTotalHouDu + "','" + txtBeiZhu + "','" + BGBeiZhu + "')";
                        list_sql.Add(sqlText);
                    }
                }

                sqlText = "update TBPM_PAINTSCHEME set PS_MAP='" + ddlSheBei.SelectedValue + "',PS_PAINTBRAND='" + txtBrand.Text.Trim() + "',PS_STATE='1' where PS_ID='" + rejectid + "'";
                list_sql.Add(sqlText);

                DBCallCommon.ExecuteTrans(list_sql);
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:油漆计划保存成功!');location.href='" + "TM_Paint_Scheme_Audit.aspx?id=" + rejectid + "'", true);
                Response.Write("<script>alert('提示:油漆计划保存成功!');location.href='" + "TM_Paint_Scheme_Audit.aspx?id=" + rejectid + "'</script>");
                #endregion
            }
        }

        /// <summary>
        /// 油漆计划批次
        /// </summary>
        private void psNum()
        {
            sqlText = "select PS_ID from TBPM_PAINTSCHEME ";
            sqlText += "where PS_PJID='" + paint_contract.Text + "'order by PS_ID desc";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

            if (Request.QueryString["id"] == null)
            {
                if (dt.Rows.Count > 0)
                {
                    ps_pici = (int.Parse(dt.Rows[0][0].ToString().Split('/')[1]) + 1).ToString();
                }
                else
                {
                    ps_pici = "0";
                }
                ps_no = paint_contract.Text + "." + "PS" + "/" + ps_pici.PadLeft(2, '0');
            }
            else
            {
                string pihao = Request.QueryString["id"].ToString();
                string[] fields = pihao.Split('.');
                ps_no = fields[0].ToString() + '.' + fields[1].ToString();
            }

            //SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            //if (dr.Read())
            //{
            //    ps_pici = (int.Parse(dr[0].ToString()) + 1).ToString();
            //}
            //dr.Close();

        }

        /// <summary>
        /// 油漆方案中的插入
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btninsert_Click(object sender, EventArgs e)
        {
            // if (istid.Value == "1")//相当于确定
            // {
            System.Data.DataTable dt = this.GetDataFromGrid("insert");
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {

                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)Repeater1.Items[i].FindControl("cbx");
                if (chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    dt.Rows.InsertAt(newRow, i + 1 + count);
                    ///////////dt.Rows.RemoveAt(dt.Rows.Count-1);
                    count++;
                }
            }

            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();

        }

        /// <summary>
        /// 原始数据中的删除,不对数据库操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btndelete_Click(object sender, EventArgs e)
        {
            //***********删除数据不对数据库操作************
            //  if (txtid.Value != "0")
            //  {
            System.Data.DataTable dt = this.GetDataFromGrid("insert");
            for (int i = 0; i < Repeater1.Items.Count; i++)
            {
                RepeaterItem item = Repeater1.Items[i];
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)item.FindControl("cbx");
                if (chk.Checked)
                {
                    dt.Rows.RemoveAt(i - count);
                    count++;
                }
            }
            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();
            ////////InitGridview();
            //   }
        }
    }
}
