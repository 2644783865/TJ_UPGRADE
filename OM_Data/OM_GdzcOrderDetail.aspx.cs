using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_GdzcOrderDetail : System.Web.UI.Page
    {
        string sqltext = "";
        string flag = string.Empty;
        string id = string.Empty;
        string code = "";
        string bianhao = "";
        string place = "";
        string bumen = "";
        string syr = "";
        string syrid = "";
        string name = "";
        string model = "";
        string note = "";
        string nx = "";
        string jiazhi = "";

        int n;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if (Request.QueryString["FLAG"] != null)
            flag = Request.QueryString["FLAG"].ToString();
            //if (Request.QueryString["id"] != null)
            id = Request.QueryString["id"].ToString();
            if (!IsPostBack)
            {
                asd.userid = Session["UserID"].ToString();
                BindData();
                //if (flag == "add")
                //{
                //    lbl_creater.Text = Session["UserName"].ToString();

                //    lblInDate.Text = DateTime.Now.ToString();
                //}
                if (flag=="add")
                {
                    CreateNewRow(1);
                }
                if (flag == "addbh")
                {
                    InitGridView();
                    bind_info();
                    hlSelect0.Visible = false;
                    xinzeng.Visible = false;
                    txtLines.Visible = false;
                    btnAdd.Visible = false;
                    btnDelRow.Visible = false;
                }
            }
        }

        private void BindData()
        {
            if (flag == "add")
            {
                lbl_creater.Text = Session["UserName"].ToString();
                lblInDate.Text = DateTime.Now.ToString();
                txtZDR.Text = Session["UserName"].ToString();
                lbZDR_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                hidZDRID.Value = Session["UserID"].ToString();
            }
            else if (flag == "check")
            {
                string sql = string.Format("select * from TBOM_GDZCIN as a left join OM_SP as b on a.INCODE=b.SPFATHERID where SPID is not null and INTYPE='0' and INCODE='{0}'", id);
                asd.dts = DBCallCommon.GetDTUsingSqlText(sql);
                InitGridView();
                bind_info();
                BindPanel(panSP);
                if (asd.userid == asd.dts.Rows[0]["SPR1ID"].ToString())
                {
                    lbSPR1_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (asd.userid == asd.dts.Rows[0]["SPR2ID"].ToString())
                {
                    lbSPR2_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
                if (asd.userid == asd.dts.Rows[0]["SPR3ID"].ToString())
                {
                    lbSPR3_SJ.Text = DateTime.Now.ToString("yyyy-MM-dd");
                }
            }
            else if (flag == "read")
            {
                string sql = string.Format("select * from TBOM_GDZCIN as a left join OM_SP as b on a.INCODE=b.SPFATHERID where SPID is not null and INTYPE='0' and INCODE='{0}'", id);
                asd.dts = DBCallCommon.GetDTUsingSqlText(sql);
                InitGridView();
                bind_info();
                BindPanel(panSP);
            }
            PowerControl();
        }

        private void PowerControl()
        {
            if (flag == "check")
            {
                hlSelect0.Visible = false;
                xinzeng.Visible = false;
                txtLines.Visible = false;
                btnAdd.Visible = false;
                btnDelRow.Visible = false;

                panJBXX.Enabled = false;
                panZDR.Enabled = false;
                panSPR1.Enabled = false;
                panSPR2.Enabled = false;
                panSPR3.Enabled = false;
                if (asd.userid == asd.dts.Rows[0]["SPR1ID"].ToString())
                {
                    panSPR1.Enabled = true;
                }
                if (asd.userid == asd.dts.Rows[0]["SPR2ID"].ToString())
                {
                    panSPR2.Enabled = true;
                }
                if (asd.userid == asd.dts.Rows[0]["SPR3ID"].ToString())
                {
                    panSPR3.Enabled = true;
                }
            }
            else if (flag == "read")
            {
                hlSelect0.Visible = false;
                xinzeng.Visible = false;
                txtLines.Visible = false;
                btnAdd.Visible = false;
                btnDelRow.Visible = false;
                btnSave.Visible = false;
                btnReturn.Visible = false;


                panZDR.Enabled = false;
                panSPR1.Enabled = false;
                panSPR2.Enabled = false;
                panSPR3.Enabled = false;
            }
        }

        private void BindPanel(Panel panel)//绑定panel
        {
            DataTable dt = asd.dts;
            List<string> list_dc = new List<string>();
            foreach (DataColumn dc in dt.Columns)
            {
                list_dc.Add(dc.ColumnName);
            }
            DataRow dr = dt.Rows[0];
            foreach (Control ctr in panel.Controls)
            {
                if (ctr is TextBox)
                {
                    TextBox txt = (TextBox)ctr;
                    if (list_dc.Contains(txt.ID.Substring(3)))
                    {
                        txt.Text = dr[txt.ID.Substring(3)].ToString();
                    }
                }
                else if (ctr is Label)
                {
                    Label lb = (Label)ctr;
                    if (list_dc.Contains(lb.ID.Substring(2)))
                    {
                        lb.Text = dr[lb.ID.Substring(2)].ToString();
                    }
                }
                else if (ctr is DropDownList)
                {
                    DropDownList ddl = (DropDownList)ctr;
                    if (list_dc.Contains(ddl.ID.Substring(3)))
                    {
                        ddl.SelectedValue = dr[ddl.ID.Substring(3) + "ID"].ToString();
                    }

                }
                else if (ctr is RadioButtonList)
                {
                    RadioButtonList rbl = (RadioButtonList)ctr;
                    if (list_dc.Contains(rbl.ID.Substring(3)))
                    {
                        if (dr[rbl.ID.Substring(3)].ToString() != "0")
                        {
                            rbl.SelectedValue = dr[rbl.ID.Substring(3)].ToString();
                        }
                    }
                }
                else if (ctr is HiddenField)
                {
                    HiddenField hid = (HiddenField)ctr;
                    if (list_dc.Contains(hid.ID.Substring(3)))
                    {
                        hid.Value = dr[hid.ID.Substring(3)].ToString();
                    }
                }
                else if (ctr is Panel)
                {
                    Panel pan = (Panel)ctr;
                    BindPanel(pan);
                }
                else if (ctr is CheckBox)
                {
                    CheckBox cbx = (CheckBox)ctr;
                    if (list_dc.Contains(cbx.ID.Substring(3)))
                    {
                        if (dr[cbx.ID.Substring(3)].ToString() != "")
                        {
                            cbx.Checked = true;
                        }
                        else
                        {
                            cbx.Checked = false;
                        }
                    }
                }
            }
        }

        private class asd
        {
            public static string userid;
            public static string bh;
            public static DataTable dts;
        }

        private void InitGridView()
        {
            string sql = "select ID*1 AS ID,CODE,NAME,MODEL,PLACE,BIANHAO,SYBUMEN,SYBUMENID,SYR,SYRID,NOTE,NX,JIAZHI,TYPE,TYPE2 from TBOM_GDZCIN WHERE INCODE='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }

        public void bind_info()
        {
            string sql = "select distinct(INCODE) as INCODE,CREATER,CREATERID,INDATE,JLR,JLRID from TBOM_GDZCIN WHERE INCODE='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                lblInCode.Text = dr["INCODE"].ToString();
                lblInDate.Text = dr["INDATE"].ToString();
                lbl_creater.Text = dr["CREATER"].ToString();
                txtshr.Text = dr["JLR"].ToString();
            }
            dr.Close();
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (flag == "add")
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    DropDownList ddl_code = (DropDownList)e.Row.FindControl("ddl_code");
                    DropDownList ddl_bumen = (DropDownList)e.Row.FindControl("ddl_bumen");
                    //DropDownList ddl_user = (DropDownList)e.Row.FindControl("ddl_user");
                    HiddenField hfCODE = (HiddenField)e.Row.FindControl("hfCODE");
                    //HiddenField hfSYR = (HiddenField)e.Row.FindControl("hfSYR");
                    HiddenField hfbumen = (HiddenField)e.Row.FindControl("hfbumen");
                    GetBUMEN(ddl_bumen, hfbumen.Value);
                    //GetSYR(ddl_user, hfSYR.Value);
                    GetCODE(ddl_code, hfCODE.Value);
                }
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string code = ((Label)e.Row.FindControl("lbl_CODE")).Text;
                    ListItem item = new ListItem();
                    item.Text = code;
                    item.Value = code;
                    DropDownList ddl = ((DropDownList)e.Row.FindControl("ddl_code"));
                    ddl.Items.Add(item);

                    string hfbumen = ((HiddenField)e.Row.FindControl("hfbumen")).Value;
                    ListItem item1 = new ListItem();
                    item1.Text = hfbumen;
                    item1.Value = hfbumen;
                    DropDownList ddl1 = ((DropDownList)e.Row.FindControl("ddl_bumen"));
                    ddl1.Items.Add(item1);


                    string hfSYR = ((HiddenField)e.Row.FindControl("hfSYR")).Value;
                    ListItem item2 = new ListItem();
                    item2.Text = hfSYR;
                    item2.Value = hfSYR;
                    DropDownList ddl2 = ((DropDownList)e.Row.FindControl("ddl_user"));
                    ddl2.Items.Add(item2);

                }
                if (flag == "addbh")
                {
                    if (e.Row.RowType == DataControlRowType.DataRow)
                    {
                        ((Label)e.Row.FindControl("lbl_CODE")).Visible = true;
                        ((DropDownList)e.Row.FindControl("ddl_bumen")).Visible = false;
                        ((Label)e.Row.FindControl("lbl_BUMEN")).Visible = true;
                        ((DropDownList)e.Row.FindControl("ddl_user")).Visible = false;
                        ((Label)e.Row.FindControl("lbl_user")).Visible = true;
                        ((DropDownList)e.Row.FindControl("ddl_code")).Visible = false;
                        ((CheckBox)e.Row.FindControl("CHK")).Visible = false;
                        ((TextBox)e.Row.FindControl("txt_Name")).Enabled = false;
                        ((TextBox)e.Row.FindControl("txt_Model")).Enabled = false;
                        ((TextBox)e.Row.FindControl("txt_Note")).Enabled = false;
                        ((TextBox)e.Row.FindControl("txt_place")).Enabled = false;
                        ((TextBox)e.Row.FindControl("txt_bianhao")).Visible = true;
                        ((TextBox)e.Row.FindControl("txt_nx")).Visible = true;
                        ((TextBox)e.Row.FindControl("txt_jiazhi")).Visible = true;
                        ((TextBox)e.Row.FindControl("txtType")).Enabled = false;
                    }
                }
            }
        
        }
        private void GetCODE(DropDownList ddlobject, string value)
        {
            ddlobject.Items.Clear();
            string sql = "select DISTINCT(a.CODE) CODE FROM View_TBOM_GDZCAPPLY as a left join TBOM_GDZCIN as b on a.CODE=b.CODE WHERE a.STATUS='6' and b.ID is null and PCTYPE='0'";
            DBCallCommon.BindDdl(ddlobject, sql, "CODE", "CODE");
            ddlobject.SelectedValue = value;
        }

        //private void code_selectchange()
        //{
        //    int j = 0;
        //    DataTable dt_code_fill;
        //    for (int i = 0; i < GridView1.Rows.Count; i++)
        //    {
        //        GridViewRow gr = GridView1.Rows[i];
        //        if (i==0)
        //        {
        //            string sql_code_fill = "select * from View_TBOM_GDZCAPPLY where CODE='" + ((DropDownList)gr.FindControl("ddl_code")).SelectedValue.ToString() + "' and STATE='6'";
        //            dt_code_fill = DBCallCommon.GetDTUsingSqlText(sql_code_fill);
        //            int rows_count = dt_code_fill.Rows.Count;
        //        }
        //        for (j; j < dt_code_fill.Rows.Count;j++ )
        //        {
        //            ((DropDownList)gr.FindControl("ddl_code")).SelectedValue = dt_code_fill.Rows[i]["CODE"].ToString();
        //            ((HiddenField)e.FindControl("hfCODE")).Value = dt_code_fill.Rows[i]["CODE"].ToString();
        //            ((Label)e.FindControl("lbl_CODE")).Text = dt_code_fill.Rows[i]["CODE"].ToString();
        //            ((TextBox)e.FindControl("txt_Name")).Text = dt_code_fill.Rows[i]["NAME"].ToString();
        //            ((Label)e.FindControl("only_id")).Text = dt_code_fill.Rows[i]["ID"].ToString();
        //            ((TextBox)e.FindControl("txt_Model")).Text = dt_code_fill.Rows[i]["MODEL"].ToString();
        //            ((TextBox)e.FindControl("txt_Note")).Text = dt_code_fill.Rows[i]["NOTE"].ToString();
        //        }
        //        if (j<dt_code_fill.Rows.Count)
        //        {
        //            continue;
        //        }
        //    }
        //}

        private void GetBUMEN(DropDownList ddlobject, string value)
        {
            ddlobject.Items.Clear();
            string sql2 = "select DEP_NAME,DEP_CODE FROM TBDS_DEPINFO WHERE DEP_CODE LIKE '[0-9][0-9]'";
            DBCallCommon.BindDdl(ddlobject, sql2, "DEP_NAME", "DEP_CODE");
            ddlobject.SelectedValue = value;
        }
        private void GetSYR(DropDownList ddlobject, string value)
        {
            ddlobject.Items.Clear();
            string sql2 = "select ST_ID,ST_NAME FROM from TBDS_STAFFINFO";
            DBCallCommon.BindDdl(ddlobject, sql2, "ST_NAME", "ST_ID");
            ddlobject.SelectedValue = value;
        }
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("CODE");
            dt.Columns.Add("BIANHAO");
            dt.Columns.Add("NX");
            dt.Columns.Add("JIAZHI");
            dt.Columns.Add("NAME");
            dt.Columns.Add("MODEL");
            dt.Columns.Add("NOTE");
            dt.Columns.Add("SYBUMEN");
            dt.Columns.Add("SYR");
            dt.Columns.Add("PLACE");
            dt.Columns.Add("ID");
            dt.Columns.Add("TYPE");
            dt.Columns.Add("TYPE2");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((DropDownList)gr.FindControl("ddl_code")).SelectedValue;
                newRow[1] = ((TextBox)gr.FindControl("txt_bianhao")).Text;
                newRow[2] = ((TextBox)gr.FindControl("txt_nx")).Text;
                newRow[3] = ((TextBox)gr.FindControl("txt_jiazhi")).Text;
                newRow[4] = ((TextBox)gr.FindControl("txt_Name")).Text;
                newRow[5] = ((TextBox)gr.FindControl("txt_Model")).Text;
                newRow[6] = ((TextBox)gr.FindControl("txt_Note")).Text;
                newRow[7] = ((DropDownList)gr.FindControl("ddl_bumen")).SelectedValue;
                newRow[8] = ((DropDownList)gr.FindControl("ddl_user")).SelectedValue;
                newRow[9] = ((TextBox)gr.FindControl("txt_place")).Text;
                newRow[10] = ((Label)gr.FindControl("only_id")).Text;
                newRow[11] = ((TextBox)gr.FindControl("txtType")).Text;
                newRow[12] = ((TextBox)gr.FindControl("txtType2")).Text;
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }
        protected void btnAdd_OnClick(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(txtLines.Text.Trim());
            //string llr = txtshr.Text;
            //string llrid = shrid.Value;
            CreateNewRow(num);
            //txtshr.Text = llr;
            //shrid.Value = llrid;

        }
        private void CreateNewRow(int num)
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();

        }

        protected void btnDelRow_OnClick(object sender, EventArgs e)
        {
            int count = 0;
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gr.FindControl("CHK");
                if (chk.Checked)
                {
                    dt.Rows.RemoveAt(i - count);
                    count++;
                }
            }
            if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要删除的行！');", true);
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }

        private void ddlcode()
        {
            string sql = "select DISTINCT(CODE) AS CODE FROM TBOM_GDZCPCAPPLY";
        }

        private void GetCode()
        {
            sqltext = "select TOP 1 dbo.GetIndex(INCODE) AS TopIndex from TBOM_GDZCIN where INTYPE='0' ORDER BY dbo.GetIndex(INCODE) DESC ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int index;
            if (dt.Rows.Count > 0)
            {
                index = Convert.ToInt16(dt.Rows[0]["TopIndex"].ToString());
            }
            else
            {
                index = 0;
            }
            string code = (index + 1).ToString();
            lblInCode.Text = "GDZCIN" + code.PadLeft(4, '0');
        }

        private void writedata()
        {
            List<string> list_sql = new List<string>();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                if (((TextBox)gr.FindControl("txt_Name")).Text != "")
                {
                    code = ((DropDownList)gr.FindControl("ddl_code")).SelectedValue;
                    bianhao = ((TextBox)gr.FindControl("txt_bianhao")).Text;
                    nx = ((TextBox)gr.FindControl("txt_nx")).Text;
                    jiazhi = ((TextBox)gr.FindControl("txt_jiazhi")).Text;
                    name = ((TextBox)gr.FindControl("txt_Name")).Text;
                    model = ((TextBox)gr.FindControl("txt_Model")).Text;
                    note = ((TextBox)gr.FindControl("txt_Note")).Text;
                    DropDownList ss = (DropDownList)gr.FindControl("ddl_user");
                    bumen = ((DropDownList)gr.FindControl("ddl_bumen")).SelectedItem.Text;
                    place = ((TextBox)gr.FindControl("txt_place")).Text;
                    syrid = ((HtmlInputHidden)gr.FindControl("hidSyrId")).Value;
                    string bumenid = ((DropDownList)gr.FindControl("ddl_bumen")).SelectedValue;
                    syr = ((HtmlInputHidden)gr.FindControl("hidSyr")).Value;
                    string type = ((TextBox)gr.FindControl("txtType")).Text;
                    string type2 = ((TextBox)gr.FindControl("txtType2")).Text;

                    //if (innum == 0.0)
                    //{
                    //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！没有入库数据！！！');", true);
                    //    return;
                    //}
                    //else
                    //{
                    sqltext = "insert into TBOM_GDZCIN(INCODE,CODE,NAME,MODEL,PLACE,BIANHAO,SYBUMEN,SYBUMENID,SYR,SYRID,CREATER,CREATERID,INDATE,NOTE,JLR,JLRID,SYDATE,NX,JIAZHI,INTYPE,TYPE,TYPE2)";
                    sqltext += "values('" + lblInCode.Text + "','" + code + "','" + name + "','" + model + "','" + place + "','" + bianhao + "','" + bumen + "','" + bumenid + "','" + syr + "','" + syrid + "','" + lbl_creater.Text + "','" + Session["UserID"].ToString() + "','" + lblInDate.Text + "','" + note + "','" + txtshr.Text + "','" + shrid.Value + "','" + lblInDate.Text + "','" + nx + "','" + jiazhi + "','0','" + type + "','" + type2 + "')";
                    list_sql.Add(sqltext);
                    //sqltext = "update TBOM_GDZCSTORE set NUMSTORE=" + numstore + " where NAME='" + name + "'and MODEL='" + model + "'";
                    //list_sql.Add(sqltext);
                    //} 
                }
            }
            sqltext = string.Format("insert into OM_SP (SPFATHERID,SPLX,SPJB,ZDR,ZDRID,ZDR_SJ,ZDR_JY,SPR1,SPR1ID,SPR1_JL,SPR1_SJ,SPR1_JY,SPR2,SPR2ID,SPR2_JL,SPR2_SJ,SPR2_JY,SPR3,SPR3ID,SPR3_JL,SPR3_SJ,SPR3_JY,SPZT) values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}')", lblInCode.Text, "GDZC", rblSPJB.SelectedValue, txtZDR.Text.Trim(), hidZDRID.Value, lbZDR_SJ.Text, txtZDR_JY.Text.Trim(), txtSPR1.Text.Trim(), hidSPR1ID.Value, "", "", "", txtSPR2.Text.Trim(), hidSPR2ID.Value, "", "", "", txtSPR3.Text.Trim(), hidSPR3ID.Value, "", "", "", "0");
            list_sql.Add(sqltext);
            DBCallCommon.ExecuteTrans(list_sql);
            string _emailto = DBCallCommon.GetEmailAddressByUserID(hidSPR1ID.Value);
            string _body = "固定资产入库审批任务:"
                  + "\r\n制单人：" + lbl_creater.Text.Trim()
                  + "\r\n制单日期：" + lblInDate.Text.Trim();

            string _subject = "您有新的【固定资产入库】需要审批，请及时处理";
            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            string sql = string.Format("update OM_SP set SPJB='{0}',ZDR_JY='{1}',SPR1='{2}',SPR1ID='{3}',SPR2='{4}',SPR2ID='{5}',SPR3='{6}',SPR3ID='{7}',SPZT='{8}' where SPFATHERID='{9}'", rblSPJB.SelectedValue, txtZDR.Text.Trim(), txtSPR1.Text.Trim(), hidSPR1ID.Value, txtSPR2.Text.Trim(), hidSPR2ID.Value, txtSPR3.Text.Trim(), hidSPR3ID.Value, "0", lblInCode.Text);
            try
            {
                DBCallCommon.ExeSqlText(sql);
            }
            catch
            {
                Response.Write("alert('提交审批的语句出现问题，请联系管理员！！！')");
                return;
            }

        }

        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            string st = "OK";
            if (GridView1.Rows.Count == 0)
            {
                st = "NoData";
            }
            if (st == "OK")
            {
                if (flag == "add")
                {
                    GetCode();
                    writedata();
                    Response.Write("<script>alert('保存成功！入库人单号为:" + lblInCode.Text + "');window.location.href='OM_GdzcIn.aspx';</script>");
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！入库人单号为:'" + lblInCode.Text + "'');window.location.href='OM_GdzcIn.aspx';", true);
                }
                if (flag == "addbh")
                {
                    List<string> list_sql = new List<string>();
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        GridViewRow gr = GridView1.Rows[i];
                        bianhao = ((TextBox)gr.FindControl("txt_bianhao")).Text;
                        nx = ((TextBox)gr.FindControl("txt_nx")).Text;
                        jiazhi = ((TextBox)gr.FindControl("txt_jiazhi")).Text;
                        string only_id = ((Label)gr.FindControl("only_id")).Text.ToString();
                        sqltext = "update TBOM_GDZCIN set BIANHAO='" + bianhao + "',NX='" + nx + "',JIAZHI='" + jiazhi + "' WHERE ID='" + only_id + "'";
                        list_sql.Add(sqltext);
                    }
                    DBCallCommon.ExecuteTrans(list_sql);
                    Response.Write("<script>alert('添加编号成功');window.location.href='OM_GdzcIn.aspx';</script>");
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('添加编号成功');window.location.href='OM_GdzcIn.aspx';", true);
                }
                if (flag == "check")
                {
                    if (asd.userid == asd.dts.Rows[0]["SPR1ID"].ToString())
                    {
                        if (rblSPR1_JL.SelectedValue != "y" && rblSPR1_JL.SelectedValue != "n")
                        {
                            Response.Write("<script>alert('请选择“同意”或“不同意”后再提交！！！')</script>");
                            return;
                        }
                    }
                    else if (asd.userid == asd.dts.Rows[0]["SPR2ID"].ToString())
                    {
                        if (rblSPR2_JL.SelectedValue != "y" && rblSPR2_JL.SelectedValue != "n")
                        {
                            Response.Write("<script>alert('请选择“同意”或“不同意”后再提交！！！')</script>");
                            return;
                        }
                    }
                    else if (asd.userid == asd.dts.Rows[0]["SPR3ID"].ToString())
                    {
                        if (rblSPR3_JL.SelectedValue != "y" && rblSPR3_JL.SelectedValue != "n")
                        {
                            Response.Write("<script>alert('请选择“同意”或“不同意”后再提交！！！')</script>");
                            return;
                        }
                    }
                    List<string> list = checklist();
                    try
                    {
                        DBCallCommon.ExecuteTrans(list);
                    }
                    catch
                    {
                        Response.Write("<script>alert('审批语句出现错误，请与管理员联系！！！')</script>");
                        return;
                    }
                    Response.Redirect("OM_GDZCRK_SP.aspx");
                }

            }
            else if (st == "NoData")
            {
                Response.Write("<script>alert('提示:无法保存！！！没有入库数据！！！')</script>");
                return;
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存！！！没有入库数据！！！');", true);
            }
        }

        private List<string> checklist()
        {
            List<string> list = new List<string>();
            string sql = string.Format("update OM_SP set SPR1_JL='{0}',SPR1_SJ='{1}',SPR1_JY='{2}',SPR2_JL='{3}',SPR2_SJ='{4}',SPR2_JY='{5}',SPR3_JL='{6}',SPR3_SJ='{7}',SPR3_JY='{8}' where SPFATHERID='{9}'", rblSPR1_JL.SelectedValue, lbSPR1_SJ.Text, txtSPR1_JY.Text.Trim(), rblSPR2_JL.SelectedValue, lbSPR2_SJ.Text, txtSPR2_JY.Text.Trim(), rblSPR3_JL.SelectedValue, lbSPR3_SJ.Text, txtSPR3_JY.Text, id);
            list.Add(sql);
            if (asd.dts.Rows[0]["SPJB"].ToString() == "1")
            {
                if (rblSPR1_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='y' where SPFATHERID='{0}'", id);
                    list.Add(sql);
                }
            }
            else if (asd.dts.Rows[0]["SPJB"].ToString() == "2")
            {
                if (rblSPR1_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='1y' where SPFATHERID='{0}'", id);
                    list.Add(sql);
                    if (asd.userid == hidSPR1ID.Value)
                    {
                        string _emailto = DBCallCommon.GetEmailAddressByUserID(hidSPR2ID.Value);
                        string _body = "固定资产入库审批任务:"
                              + "\r\n制单人：" + lbl_creater.Text.Trim()
                              + "\r\n制单日期：" + lblInDate.Text.Trim();

                        string _subject = "您有新的【固定资产入库】需要审批，请及时处理";
                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                    }
                }
                if (rblSPR2_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='y' where SPFATHERID='{0}'", id);
                    list.Add(sql);
                }
            }
            else if (asd.dts.Rows[0]["SPJB"].ToString() == "3")
            {
                if (rblSPR1_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='1y' where SPFATHERID='{0}'", id);
                    list.Add(sql);
                    if (asd.userid == hidSPR1ID.Value)
                    {
                        string _emailto = DBCallCommon.GetEmailAddressByUserID(hidSPR2ID.Value);
                        string _body = "固定资产入库审批任务:"
                              + "\r\n制单人：" + lbl_creater.Text.Trim()
                              + "\r\n制单日期：" + lblInDate.Text.Trim();

                        string _subject = "您有新的【固定资产入库】需要审批，请及时处理";
                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                    }
                }
                if (rblSPR2_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='2y' where SPFATHERID='{0}'", id);
                    list.Add(sql);
                    if (asd.userid == hidSPR2ID.Value)
                    {
                        string _emailto = DBCallCommon.GetEmailAddressByUserID(hidSPR3ID.Value);
                        string _body = "固定资产入库审批任务:"
                              + "\r\n制单人：" + lbl_creater.Text.Trim()
                              + "\r\n制单日期：" + lblInDate.Text.Trim();

                        string _subject = "您有新的【固定资产入库】需要审批，请及时处理";
                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                    }
                }
                if (rblSPR3_JL.SelectedValue == "y")
                {
                    sql = string.Format("update OM_SP set SPZT='y' where SPFATHERID='{0}'", id);
                    list.Add(sql);
                }
            }
            if (rblSPR1_JL.SelectedValue == "n" || rblSPR2_JL.SelectedValue == "n" || rblSPR3_JL.SelectedValue == "n")
            {
                sql = string.Format("update OM_SP set SPZT='n' where SPFATHERID='{0}'", id);
                list.Add(sql);
            }
            return list;
        }

        protected void btnReturn_OnClick(object sender, EventArgs e)
        {
            Response.Redirect("OM_GdzcIn.aspx");
        }


    }
}
