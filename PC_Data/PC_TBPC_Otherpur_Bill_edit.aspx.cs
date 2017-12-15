using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Otherpur_Bill_edit : System.Web.UI.Page
    {
        public string gloabtype
        {
            get
            {
                object str = ViewState["gloabtype"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabtype"] = value;
            }
        }
        public string gloabshape
        {
            get
            {
                object str = ViewState["gloabshape"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabshape"] = value;
            }
        }
        public string gloabnum
        {
            get
            {
                object str = ViewState["gloabnum"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabnum"] = value;
            }
        }
        public string gloabcsnum
        {
            get
            {
                object str = ViewState["gloabcsnum"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabcsnum"] = value;
            }
        }

        string pid = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initinfo();
            }
        }
        private void initinfo()
        {

            string sqltext = "";

            sqltext = "select ST_NAME as ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_DEPID='" + Session["UserDeptID"].ToString() + "' and ST_PD='0'";

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            cob_fuziren.DataSource = dt;
            cob_fuziren.DataTextField = "ST_NAME";
            cob_fuziren.DataValueField = "ST_ID";
            cob_fuziren.DataBind();
            cob_fuziren.SelectedIndex = 0;
            if (Session["UserDeptID"].ToString()=="12")
            {
                ListItem list1 = new ListItem("陈永秀", "69");
                cob_fuziren.Items.Add(list1);
            }                                                           
            cob_sqren.DataSource = dt;
            cob_sqren.DataTextField = "ST_NAME";
            cob_sqren.DataValueField = "ST_ID";
            cob_sqren.DataBind();
            cob_sqren.SelectedValue = Session["UserID"].ToString();
            if (Request.QueryString["action"].ToString() == "add")
            {
                gloabnum = "0";
                gloabcsnum = "0";
                Tb_shijian.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                tb_dep.Text = Session["UserDept"].ToString();
                tb_depid.Text = Session["UserDeptID"].ToString();
                TextBoxexecutor.Text = Session["UserName"].ToString();
                TextBoxexecutorid.Text = Session["UserID"].ToString();
                //TextBox_pid.Enabled = true;
                tb_pjinfo.Enabled = true;
                tb_enginfo.Enabled = true;
                tb_note.Enabled = true;
                CreateNewRow(10);

                //如果从TM_Paint_Collect按钮没有传入pId数值则按原方式添加采购,否则直接导入油漆物品采购件
                if (String.IsNullOrEmpty(Request.QueryString["pId"].ToString()))
                {
                    if (Request.QueryString["Type"].ToString() != null)
                    {
                        gloabtype = Request.QueryString["Type"].ToString();
                    }
                    else
                    {
                        gloabtype = "";
                    }
                    if (Request.QueryString["shape"].ToString() != null)
                    {
                        gloabshape = Request.QueryString["shape"].ToString();
                    }
                    else
                    {
                        gloabshape = "";
                    }
                    lb_shape.Text = gloabshape;
                }
                else
                {
                    gloabtype = "ZC";
                    gloabshape = "油漆";
                    lb_shape.Text = gloabshape;

                    string pId = Request.QueryString["pId"].ToString();
                    string sql = "select PS_ENGID,PS_BOTMARID,sumYL from (select PS_BOTMARID,sum(cast(PS_BOTYONGLIANG as float)) as sumYL,PS_ENGID from (select PS_BOTMARID,PS_BOTYONGLIANG,PS_ENGID from dbo.TBPM_PAINTSCHEMELIST where PS_PID='" + pId + "' and PS_BOTMARID<>''union all select PS_MIDMARID,PS_MIDYONGLIANG,PS_ENGID from dbo.TBPM_PAINTSCHEMELIST where PS_PID='" + pId + "' and PS_MIDMARID<>''union all select PS_TOPMARID,PS_TOPYONGLIANG,PS_ENGID from dbo.TBPM_PAINTSCHEMELIST where PS_PID='" + pId + "' and PS_TOPMARID<>'')a group by PS_BOTMARID,PS_ENGID)b left join dbo.TBMA_MATERIAL as c on b.PS_BOTMARID=c.ID";
                    SqlDataReader ZCdtrd = DBCallCommon.GetDRUsingSqlText(sql);
                    int i = 0;
                    while (ZCdtrd.Read())
                    {   
                        tb_pjinfo.Text = ZCdtrd["PS_ENGID"].ToString().Trim();
                        RepeaterItem Reitem = tbpc_otherpurbillRepeater.Items[i];
                        ((TextBox)Reitem.FindControl("MP_MARID")).Text = ZCdtrd["PS_BOTMARID"].ToString().Trim();
                        ((TextBox)Reitem.FindControl("MP_NUMBER")).Text = ZCdtrd["sumYL"].ToString().Trim();
                        i++;
                    }
                }
            }
            else
            {
                tb_pjinfo.Enabled = false;
                tb_enginfo.Enabled = false;
                TextBox_pid.Text = Request.QueryString["mp_id"].ToString();

                sqltext = "select PCODE,PJID,PJNM,ENGID,ENGNM,SQRID,SQRNM,TJRID,TJRNM," +
                             "TJDATE,SHRID,SHRNM,DEPID,DEPNM,NOTE,MP_SHAPE,MP_YFBG,MP_IFFAST  "
                             + "from View_TBPC_OTPURRVW where PCODE='" + TextBox_pid.Text + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    tb_dep.Text = dr["DEPNM"].ToString();
                    tb_depid.Text = dr["DEPID"].ToString();
                    Tb_shijian.Text = dr["TJDATE"].ToString();
                    tb_enginfo.Text = dr["ENGID"].ToString();
                    tb_pj.Text = dr["PJNM"].ToString();
                    tb_pjid.Text = dr["PJID"].ToString();
                    tb_pjinfo.Text = tb_pjid.Text;
                    cob_fuziren.SelectedValue = dr["SHRID"].ToString();
                    cob_sqren.SelectedValue = dr["SQRID"].ToString();
                    TextBoxexecutor.Text = dr["TJRNM"].ToString();
                    TextBoxexecutorid.Text = dr["TJRID"].ToString();
                    lb_shape.Text = dr["MP_SHAPE"].ToString();
                    if (dr["MP_YFBG"].ToString() == "1")
                    {
                        cb_bg.Checked = true;
                    }
                    if (dr["MP_IFFAST"].ToString().Trim() == "1")
                    {
                        chkiffast.Checked = true;
                    }
                    else
                    {
                        chkiffast.Checked = false;
                    }
                }
                dr.Close();
                
                    sqltext = "SELECT PTCODE AS MP_PTCODE,MARID AS MP_MARID,MARNM AS MP_MARNAME,MARGG AS MP_MARNORM,"
                                + "MARCZ AS MP_MARTERIAL,MARGB AS MP_MARGUOBIAO,WIDTH AS MP_WIDTH,LENGTH AS MP_LENGTH,"
                                + "NUM AS MP_NUMBER,FZNUM AS MP_FZNUM,TIMERQ AS MP_TIMERQ,UNIT AS MP_NUNIT,FZUNIT AS MP_FZNUNIT,"
                                + " NOTE AS MP_NOTE,DETAILSTATE AS MP_STATE ,MP_TUHAO,MWEIGHT "
                                + "FROM View_TBPC_OTPURPLAN WHERE PCODE='" + TextBox_pid.Text + "'";


                    DBCallCommon.BindRepeater(tbpc_otherpurbillRepeater, sqltext);
             
                
            }
        }
        //定义DataTable
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MP_PTCODE");
            dt.Columns.Add("MP_MARID");
            dt.Columns.Add("MP_MARNAME");
            dt.Columns.Add("MP_MARNORM");
            dt.Columns.Add("MP_MARTERIAL");
            dt.Columns.Add("MP_MARGUOBIAO");
            //dt.Columns.Add("MP_FIXEDSIZE");
            dt.Columns.Add("MP_TUHAO");
            dt.Columns.Add("MP_WIDTH");
            dt.Columns.Add("MP_LENGTH");
            dt.Columns.Add("MP_NUMBER");
            dt.Columns.Add("MP_NUNIT");
            dt.Columns.Add("MP_FZNUM");
            dt.Columns.Add("MP_FZNUNIT");
            dt.Columns.Add("MP_TIMERQ");
            dt.Columns.Add("MP_NOTE");
            dt.Columns.Add("MP_STATE");
            dt.Columns.Add("MWEIGHT");
            for (int i = 0; i < tbpc_otherpurbillRepeater.Items.Count; i++)
            {
                RepeaterItem Reitem = tbpc_otherpurbillRepeater.Items[i];
                DataRow newRow = dt.NewRow();
                newRow[0] = ((Label)Reitem.FindControl("MP_PTCODE")).Text;
                newRow[1] = ((TextBox)Reitem.FindControl("MP_MARID")).Text;
                newRow[2] = ((TextBox)Reitem.FindControl("MP_MARNAME")).Text;
                newRow[3] = ((TextBox)Reitem.FindControl("MP_MARNORM")).Text;
                newRow[4] = ((TextBox)Reitem.FindControl("MP_MARTERIAL")).Text;
                newRow[5] = ((TextBox)Reitem.FindControl("MP_MARGUOBIAO")).Text;
                //newRow[6] = ((RadioButtonList)Reitem.FindControl("MP_FIXEDSIZE")).SelectedValue.ToString();
                newRow[6] = ((TextBox)Reitem.FindControl("MP_TUHAO")).Text;
                newRow[7] = ((TextBox)Reitem.FindControl("MP_WIDTH")).Text;
                newRow[8] = ((TextBox)Reitem.FindControl("MP_LENGTH")).Text;
                newRow[9] = ((TextBox)Reitem.FindControl("MP_NUMBER")).Text;
                newRow[10] = ((TextBox)Reitem.FindControl("MP_NUNIT")).Text;
                newRow[11] = ((TextBox)Reitem.FindControl("MP_FZNUM")).Text;
                newRow[12] = ((TextBox)Reitem.FindControl("MP_FZNUNIT")).Text;
                newRow[13] = ((TextBox)Reitem.FindControl("MP_TIMERQ")).Text;
                newRow[14] = ((TextBox)Reitem.FindControl("MP_NOTE")).Text;
                newRow[15] = ((Label)Reitem.FindControl("MP_STATE")).Text;
                newRow[16] = ((HtmlInputHidden)Reitem.FindControl("MWEIGHT")).Value;

                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }
        //生成输入1行函数
        private void CreateNewRow()
        {
            DataTable dt = this.GetDataTable();
            DataRow newRow = dt.NewRow();
            dt.Rows.Add(newRow);
            this.tbpc_otherpurbillRepeater.DataSource = dt;
            this.tbpc_otherpurbillRepeater.DataBind();
        }
        //生成输入n行函数
        private void CreateNewRow(int num)
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.tbpc_otherpurbillRepeater.DataSource = dt;
            this.tbpc_otherpurbillRepeater.DataBind();
        }
        protected void btn_add_Click(object sender, EventArgs e)
        {
            int num = Convert.ToInt32(TextBox1.Text.Trim());
            CreateNewRow(num);
        }

        protected void btn_addrow_Click(object sender, EventArgs e)
        {
            CreateNewRow();
        }
        protected void btn_delectrow_Click(object sender, EventArgs e)
        {
            int count = 0;
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < tbpc_otherpurbillRepeater.Items.Count; i++)
            {
                RepeaterItem Reitem = tbpc_otherpurbillRepeater.Items[i];
                CheckBox cbk = (CheckBox)Reitem.FindControl("CHK");
                Label jhgzh=(Label)Reitem.FindControl("MP_PTCODE"); 
                if (cbk.Checked)
                {
                    string sql000 = "select * from TBPC_OTPURPLAN where MP_PTCODE='" + jhgzh.Text.ToString().Trim() + "' and MP_PCODE='" + TextBox_pid.Text.ToString().Trim() + "'";
                    DataTable dt000 = DBCallCommon.GetDTUsingSqlText(sql000);
                    if (dt000.Rows.Count > 0)
                    {
                        string sqldelete = "delete from TBPC_OTPURPLAN where MP_PTCODE='" + jhgzh.Text.ToString().Trim() + "' and MP_PCODE='" + TextBox_pid.Text.ToString().Trim() + "'";
                        DBCallCommon.ExeSqlText(sqldelete);
                    }
                }
            }
            for (int i = 0; i < tbpc_otherpurbillRepeater.Items.Count; i++)
            {
                RepeaterItem Reitem = tbpc_otherpurbillRepeater.Items[i];
                CheckBox chk = (CheckBox)Reitem.FindControl("CHK");
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
            else if (count > 0)
            {
                string sql001 = "select * from TBPC_OTPURPLAN where MP_PCODE='" + TextBox_pid.Text.ToString().Trim() + "'";
                DataTable dt001 = DBCallCommon.GetDTUsingSqlText(sql001);
                if (dt001.Rows.Count == 0)
                {
                    string sqltextdel1 = "delete from TBPC_OTPURRVW where MP_PCODE='" + TextBox_pid.Text.ToString().Trim() + "'";
                    string sqltextdel2 = "delete from TBPC_OTPUR_AUDIT where PA_CODE='" + TextBox_pid.Text.ToString().Trim() + "'";
                    DBCallCommon.ExeSqlText(sqltextdel1);
                    DBCallCommon.ExeSqlText(sqltextdel2);
                }
            }
            this.tbpc_otherpurbillRepeater.DataSource = dt;
            this.tbpc_otherpurbillRepeater.DataBind();
        }

        protected void btn_insert_Click(object sender, EventArgs e)
        {
            int count = 0;
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < tbpc_otherpurbillRepeater.Items.Count; i++)
            {
                RepeaterItem Reitem = tbpc_otherpurbillRepeater.Items[i];
                CheckBox chk = (CheckBox)Reitem.FindControl("CHK");
                if (chk.Checked)
                {
                    count++;
                }
            }
            if (count > 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('只能选择一行！');", true);
            }
            else if (count == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要插入行的位置！');", true);
            }
            else if (count == 1)
            {
                for (int i = 0; i < tbpc_otherpurbillRepeater.Items.Count; i++)
                {
                    RepeaterItem Reitem = tbpc_otherpurbillRepeater.Items[i];
                    CheckBox chk = (CheckBox)Reitem.FindControl("CHK");
                    if (chk.Checked)
                    {
                        DataRow newRow = dt.NewRow();
                        dt.Rows.InsertAt(newRow, i + 1);

                        count++;
                    }
                }
            }
            this.tbpc_otherpurbillRepeater.DataSource = dt;
            this.tbpc_otherpurbillRepeater.DataBind();
        }

        protected void btn_save_Click(object sender, EventArgs e)
        {
            savedata();
        }


        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("PC_TBPC_Otherpur_Bill_List.aspx");
        }
        protected void Tb_marid_Textchanged(object sender, EventArgs e)
        {
            string marid = "";
            string sqltext = "";
            DataTable glotb = new DataTable();
            TextBox Tb_newmarid = (TextBox)sender;//定义TextBox
            HtmlTableRow Reitem = ((HtmlTableRow)Tb_newmarid.Parent.Parent);
            if (Tb_newmarid.Text.ToString().Contains("|"))
            {
                marid = Tb_newmarid.Text.Substring(0, Tb_newmarid.Text.ToString().IndexOf("|"));
                sqltext = "SELECT ID,MNAME,GUIGE,CAIZHI,GB AS GUOBIAO,PURCUNIT AS UNIT,FUZHUUNIT AS FZNUNIT,MWEIGHT FROM TBMA_MATERIAL WHERE ID='" + marid + "' ORDER BY ID";

                glotb = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (glotb.Rows.Count > 0)
                {
                    Tb_newmarid.Text = marid;
                    ((TextBox)Reitem.FindControl("MP_MARID")).Text = glotb.Rows[0]["ID"].ToString();
                    ((TextBox)Reitem.FindControl("MP_MARNAME")).Text = glotb.Rows[0]["MNAME"].ToString();
                    ((TextBox)Reitem.FindControl("MP_MARNORM")).Text = glotb.Rows[0]["GUIGE"].ToString();
                    ((TextBox)Reitem.FindControl("MP_MARTERIAL")).Text = glotb.Rows[0]["CAIZHI"].ToString();
                    ((TextBox)Reitem.FindControl("MP_MARGUOBIAO")).Text = glotb.Rows[0]["GUOBIAO"].ToString();
                    ((TextBox)Reitem.FindControl("MP_NUNIT")).Text = glotb.Rows[0]["UNIT"].ToString();                 
                        ((TextBox)Reitem.FindControl("MP_FZNUNIT")).Text = glotb.Rows[0]["FZNUNIT"].ToString();
                        ((HtmlInputHidden)Reitem.FindControl("MWEIGHT")).Value = glotb.Rows[0]["MWEIGHT"].ToString();

                }
                else
                {
                    showerrormessage(Tb_newmarid, "输入的物料编码不存在，请重新输入！");
                    return;
                }
            }
        }
        protected void showerrormessage(TextBox tbx, string errormessage)
        {
            RepeaterItem Reitem = (RepeaterItem)tbx.Parent;
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + errormessage + "');", true);

            ((TextBox)Reitem.FindControl("MP_MARID")).Text = "";
            ((HtmlInputText)Reitem.FindControl("MP_MARNAME")).Value = "";
            ((HtmlInputText)Reitem.FindControl("MP_MARNORM")).Value = "";
            //((TextBox)Reitem.FindControl("MP_RGUIGE")).Text = "";
            ((HtmlInputText)Reitem.FindControl("MP_MARTERIAL")).Value = "";
            ((HtmlInputText)Reitem.FindControl("MP_MARGUOBIAO")).Value = "";
            ((HtmlInputText)Reitem.FindControl("MP_NUNIT")).Value = "";
            ((HtmlInputText)Reitem.FindControl("MP_FZNUNIT")).Value = "";
            ((TextBox)Reitem.FindControl("MP_WIDTH")).Text = "";
            ((TextBox)Reitem.FindControl("MP_LENGTH")).Text = "";
            tbx.Focus();
        }
        protected void tbpc_otherpurbillRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

        }
        protected void savedata()
        {
            if (Request.QueryString["action"].ToString() == "add")
            {
                if (gloabcsnum == "0")
                {
                    if (gloabtype == "ZC")
                    {
                        string end_pi_id = "";                        
                        if (tb_pjinfo.Text != "")
                        {
                            string sqltext = "select top 1 * from (select * from  View_TM_BOMAllLotNum where INDEX_ID like '%" + tb_pjinfo.Text.ToString() + "'+'.XZ%')t order by INDEX_ID desc";
                            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
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
                            TextBox_pid.Text = tb_pjinfo.Text + "." + "XZ/" + end_pi_id;
                           
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('任务号不能为空！');", true);
                            return;
                        }
                    }

                }
                writedate();

                if (cb_bg.Checked == true)
                {
                    string sqltet = "update TBPC_OTPURRVW set MP_YFBG='1' where MP_PCODE='" + TextBox_pid.Text + "'  ";
                    DBCallCommon.GetDRUsingSqlText(sqltet);
                }
                //Response.Redirect("PC_TBPC_Otherpur_Bill_List.aspx");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功,点击查看提交审核！');window.location.href='PC_TBPC_Otherpur_Bill_List.aspx'", true);
            }
            else
            {
                updatedate();
                if (cb_bg.Checked == true)
                {
                    string sqltet = "update TBPC_OTPURRVW set MP_YFBG='1' where MP_PCODE='" + TextBox_pid.Text + "'  ";
                    DBCallCommon.GetDRUsingSqlText(sqltet);
                }
                //Response.Redirect("PC_TBPC_Otherpur_Bill_List.aspx");
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功,点击查看提交审核！');window.location.href='PC_TBPC_Otherpur_Bill_List.aspx'", true);
            }
        }
        private void writedate()
        {
            gloabcsnum = Convert.ToString(Convert.ToInt32(gloabcsnum) + 1);
            string sqltext = "";
            string tag_pi_id_1 = "";
            DataTable dt = DBCallCommon.GetDTUsingSqlText("select * from TBPM_TCTSASSGN where TSA_ID='" + tb_htid.Text + "'");
            string eng = "无";
            if (dt.Rows.Count > 0)
            {
                eng = dt.Rows[0]["TSA_ENGNAME"].ToString();
            }
            string TSA_PJID = dt.Rows[0]["TSA_PJID"].ToString();
            string P = TextBox_pid.Text.Replace('.','_');
            tag_pi_id_1 = string.Format("{0}({1})_{2}_{3}", tb_enginfo.Text, TSA_PJID, eng, P);
            foreach (RepeaterItem Reitem in tbpc_otherpurbillRepeater.Items)
            {
                string MP_MARID = ((TextBox)Reitem.FindControl("MP_MARID")).Text.Trim();
                if (MP_MARID != "")
                {
                    gloabnum = Convert.ToString(Convert.ToInt32(gloabnum) + 1);
                    string MP_PCODE = TextBox_pid.Text.ToString().Trim();
                    double MP_NUMBER = Convert.ToDouble(((TextBox)Reitem.FindControl("MP_NUMBER")).Text.Trim() == "" ? "0" : ((TextBox)Reitem.FindControl("MP_NUMBER")).Text.Trim());
                    double MP_FZNUM = Convert.ToDouble(((TextBox)Reitem.FindControl("MP_FZNUM")).Text.Trim() == "" ? "0" : ((TextBox)Reitem.FindControl("MP_FZNUM")).Text.Trim());
                    double MP_WIDTH = Convert.ToDouble(((TextBox)Reitem.FindControl("MP_WIDTH")).Text.Trim() == "" ? "0" : ((TextBox)Reitem.FindControl("MP_WIDTH")).Text.Trim());
                    double MP_LENGTH = Convert.ToDouble(((TextBox)Reitem.FindControl("MP_LENGTH")).Text.Trim() == "" ? "0" : ((TextBox)Reitem.FindControl("MP_LENGTH")).Text.Trim());
                    string MP_TIMERQ = ((TextBox)Reitem.FindControl("MP_TIMERQ")).Text.Trim();
                    string MP_NOTE = ((TextBox)Reitem.FindControl("MP_NOTE")).Text.Trim();
                    char MP_STATE = '0';
                    string MP_PTCODE = tag_pi_id_1 + "_" + gloabnum.PadLeft(4, '0');
                    //增加图号/标识号
                    string MP_TUHAO = ((TextBox)Reitem.FindControl("MP_TUHAO")).Text.Trim();
                    
                    sqltext = "INSERT INTO TBPC_OTPURPLAN(MP_PCODE,MP_MARID,MP_NUMBER,MP_FZNUM,MP_WIDTH,MP_LENGTH,MP_TIMERQ,MP_NOTE,MP_STATE,MP_SHAPE,MP_TUHAO,MP_PTCODE) " +
                              "VALUES('" + MP_PCODE + "','" + MP_MARID + "','" + MP_NUMBER + "','" + MP_FZNUM + "','" + MP_WIDTH + "'," +
                                    "'" + MP_LENGTH + "','" + MP_TIMERQ + "','" + MP_NOTE + "','" + MP_STATE + "','" + lb_shape.Text + "','" + MP_TUHAO + "','" + MP_PTCODE + "')";

                    DBCallCommon.ExeSqlText(sqltext);
                }
            }
            //插入主表
            if (gloabcsnum == "1")
            {
                if (chkiffast.Checked)
                {
                    sqltext = "INSERT INTO TBPC_OTPURRVW(MP_PCODE,MP_PJID,MP_ENGID,MP_SUBMITID,MP_SUBMITTM,MP_SQRENID,MP_REVIEWA,MP_USEDEPID,MP_NOTE,MP_SHAPE,MP_IFFAST) " +
                                                "VALUES('" + TextBox_pid.Text.ToString() + "' ,'" + tb_pjinfo.Text + "','" + tb_enginfo.Text + "','" + TextBoxexecutorid.Text.ToString() + "'," +
                                                "'" + Tb_shijian.Text + "','" + cob_sqren.SelectedValue.ToString() + "'," +
                                                "'" + cob_fuziren.SelectedValue.ToString() + "','" + tb_depid.Text + "','" + tb_note.Text + "','" + lb_shape.Text + "','1')";
                }
                else
                {
                    sqltext = "INSERT INTO TBPC_OTPURRVW(MP_PCODE,MP_PJID,MP_ENGID,MP_SUBMITID,MP_SUBMITTM,MP_SQRENID,MP_REVIEWA,MP_USEDEPID,MP_NOTE,MP_SHAPE,MP_IFFAST) " +
                                                                   "VALUES('" + TextBox_pid.Text.ToString() + "' ,'" + tb_pjinfo.Text + "','" + tb_enginfo.Text + "','" + TextBoxexecutorid.Text.ToString() + "'," +
                                                                   "'" + Tb_shijian.Text + "','" + cob_sqren.SelectedValue.ToString() + "'," +
                                                                   "'" + cob_fuziren.SelectedValue.ToString() + "','" + tb_depid.Text + "','" + tb_note.Text + "','" + lb_shape.Text + "',NULL)";
                }
                DBCallCommon.ExeSqlText(sqltext);
            }
            foreach (RepeaterItem Reitem in tbpc_otherpurbillRepeater.Items)
            {
                ((TextBox)Reitem.FindControl("MP_MARID")).Text = "";
                ((TextBox)Reitem.FindControl("MP_MARNAME")).Text = "";
                ((TextBox)Reitem.FindControl("MP_MARNORM")).Text = "";
                ((TextBox)Reitem.FindControl("MP_MARTERIAL")).Text = "";
                ((TextBox)Reitem.FindControl("MP_MARGUOBIAO")).Text = "";
                ((TextBox)Reitem.FindControl("MP_TUHAO")).Text = "";
                ((TextBox)Reitem.FindControl("MP_LENGTH")).Text = "";
                ((TextBox)Reitem.FindControl("MP_WIDTH")).Text = "";
                ((TextBox)Reitem.FindControl("MP_NUMBER")).Text = "";
                ((TextBox)Reitem.FindControl("MP_NUNIT")).Text = "";
                ((TextBox)Reitem.FindControl("MP_FZNUM")).Text = "";
                ((TextBox)Reitem.FindControl("MP_FZNUNIT")).Text = "";
                ((TextBox)Reitem.FindControl("MP_TIMERQ")).Text = "";
                ((TextBox)Reitem.FindControl("MP_NOTE")).Text = "";
            }
        }
        private void updatedate()
        {
            string sqltext = "";
            List<string> sqlar = new List<string>();
            foreach (RepeaterItem Reitem in tbpc_otherpurbillRepeater.Items)
            {
                string MP_MARID = ((TextBox)Reitem.FindControl("MP_MARID")).Text.Trim();
                if (MP_MARID != "")
                {
                    string MP_PCODE = TextBox_pid.Text.ToString().Trim();
                    double MP_NUMBER = Convert.ToDouble(((TextBox)Reitem.FindControl("MP_NUMBER")).Text.Trim() == "" ? "0" : ((TextBox)Reitem.FindControl("MP_NUMBER")).Text.Trim());
                    double MP_FZNUM = Convert.ToDouble(((TextBox)Reitem.FindControl("MP_FZNUM")).Text.Trim() == "" ? "0" : ((TextBox)Reitem.FindControl("MP_FZNUM")).Text.Trim());
                    double MP_WIDTH = Convert.ToDouble(((TextBox)Reitem.FindControl("MP_WIDTH")).Text.Trim() == "" ? "0" : ((TextBox)Reitem.FindControl("MP_WIDTH")).Text.Trim());
                    double MP_LENGTH = Convert.ToDouble(((TextBox)Reitem.FindControl("MP_LENGTH")).Text.Trim() == "" ? "0" : ((TextBox)Reitem.FindControl("MP_LENGTH")).Text.Trim());
                    string MP_TIMERQ = ((TextBox)Reitem.FindControl("MP_TIMERQ")).Text.Trim();
                    string MP_NOTE = ((TextBox)Reitem.FindControl("MP_NOTE")).Text.Trim();
                    string MP_TUHAO = ((TextBox)Reitem.FindControl("MP_TUHAO")).Text.Trim();
                    string MP_PTCODE =((Label)Reitem.FindControl("MP_PTCODE")).Text.Trim();

                   
                    char MP_STATE = '0';
                    if (MP_PTCODE != "")
                    {
                        sqltext = "UPDATE TBPC_OTPURPLAN set MP_PCODE='" + MP_PCODE + "',MP_MARID='" + MP_MARID + "',MP_NUMBER='" + MP_NUMBER + "',MP_FZNUM='" + MP_FZNUM + "',MP_WIDTH='" + MP_WIDTH + "',MP_LENGTH='" + MP_LENGTH + "',MP_TIMERQ='" + MP_TIMERQ + "',MP_NOTE='" + MP_NOTE + "',MP_STATE='" + MP_STATE + "',MP_SHAPE='" + lb_shape.Text + "',MP_TUHAO='" + MP_TUHAO + "' where MP_PTCODE='" + MP_PTCODE + "' ";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                    //增加图号/标识号
                    else
                    {
                        string sql = "select top 1 * from (select distinct MP_PTCODE from  TBPC_OTPURPLAN where MP_PTCODE like '%" + TextBox_pid.Text.ToString().Trim().Replace('.','_') + "%')t order by MP_PTCODE desc";
                        SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                        if (dr.HasRows)
                        {
                            while (dr.Read())
                            {
                                int index = Convert.ToInt32(dr["MP_PTCODE"].ToString().Split('_')[4]) + 1;
                                MP_PTCODE = index.ToString();
                                MP_PTCODE = MP_PTCODE.PadLeft(4, '0');
                                MP_PTCODE = dr["MP_PTCODE"].ToString().Split('_')[0] + '_' + dr["MP_PTCODE"].ToString().Split('_')[1] + '_' + dr["MP_PTCODE"].ToString().Split('_')[2] + '_' + dr["MP_PTCODE"].ToString().Split('_')[3] + '_' + MP_PTCODE;
                            }
                           
                            dr.Close();
                        }
                        sqltext = "INSERT INTO TBPC_OTPURPLAN(MP_PCODE,MP_MARID,MP_NUMBER,MP_FZNUM,MP_WIDTH,MP_LENGTH,MP_TIMERQ,MP_NOTE,MP_STATE,MP_PTCODE,MP_SHAPE,MP_TUHAO) " +
                                   "VALUES('" + MP_PCODE + "','" + MP_MARID + "','" + MP_NUMBER + "','" + MP_FZNUM + "','" + MP_WIDTH + "'," +
                                         "'" + MP_LENGTH + "','" + MP_TIMERQ + "','" + MP_NOTE + "','" + MP_STATE + "','" + MP_PTCODE + "','" + lb_shape.Text + "','" + MP_TUHAO + "')";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            if (chkiffast.Checked)
            {
                sqltext = "update TBPC_OTPURRVW set MP_SQRENID='" + cob_sqren.SelectedValue.ToString() + "'," +
                          "MP_REVIEWA='" + cob_fuziren.SelectedValue.ToString() + "',MP_PJID='" + tb_pjid.Text + "',MP_NOTE='" + tb_note.Text + "',MP_IFFAST='1' " +
                          "where MP_PCODE='" + TextBox_pid.Text.ToString() + "'";
            }
            else
            {
                sqltext = "update TBPC_OTPURRVW set MP_SQRENID='" + cob_sqren.SelectedValue.ToString() + "'," +
                                         "MP_REVIEWA='" + cob_fuziren.SelectedValue.ToString() + "',MP_PJID='" + tb_pjid.Text + "',MP_NOTE='" + tb_note.Text + "',MP_IFFAST=NULL " +
                                         "where MP_PCODE='" + TextBox_pid.Text.ToString() + "'";
            }

            DBCallCommon.ExeSqlText(sqltext);
        }
        public string get_pr_state(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "初始化";
            }
            else if (i == "1")
            {
                state = "提交";
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
        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {
            CheckBox1.Checked = false;
            CheckBox2.Checked = false;
            CheckBox3.Checked = false;
            CheckBox4.Checked = false;
            CheckBox5.Checked = false;
            CheckBox6.Checked = false;
            CheckBox7.Checked = false;
            CheckBox8.Checked = false;
            QueryButton_Click(null, null);
        }

        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }

        protected void QueryButton_Click(object sender, EventArgs e)
        {
            #region 遍历判断隐藏行
            foreach (RepeaterItem item in this.tbpc_otherpurbillRepeater.Controls)
            {
                if (CheckBox1.Checked)
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td1").Visible = false;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td9").Visible = false;
                    }
                }
                else
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td1").Visible = true;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td9").Visible = true;
                    }
                }
                if (CheckBox2.Checked)
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td2").Visible = false;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td10").Visible = false;
                    }
                }
                else
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td2").Visible = true;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td10").Visible = true;
                    }
                }
                if (CheckBox3.Checked)
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td3").Visible = false;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td11").Visible = false;
                    }
                }
                else
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td3").Visible = true;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td11").Visible = true;
                    }
                }
                if (CheckBox4.Checked)
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td4").Visible = false;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td12").Visible = false;
                    }
                }
                else
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td4").Visible = true;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td12").Visible = true;
                    }
                }
                if (CheckBox5.Checked)
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td5").Visible = false;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td13").Visible = false;
                    }
                }
                else
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td5").Visible = true;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td13").Visible = true;
                    }
                }
                if (CheckBox6.Checked)
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td6").Visible = false;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td14").Visible = false;
                    }
                }
                else
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td6").Visible = true;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td14").Visible = true;
                    }
                }
                if (CheckBox7.Checked)
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td7").Visible = false;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td15").Visible = false;
                    }
                }
                else
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td7").Visible = true;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td15").Visible = true;
                    }
                }
                if (CheckBox8.Checked)
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td8").Visible = false;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td16").Visible = false;
                    }
                }
                else
                {
                    if (item.ItemType == ListItemType.Header)
                    {
                        item.FindControl("td8").Visible = true;
                    }

                    if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                    {
                        item.FindControl("td16").Visible = true;
                    }
                }
            }
            #endregion
        }
    }
}
