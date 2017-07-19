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

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Md_Detail_List : System.Web.UI.Page
    {
        #region
        string ms_id;
        string detail_id;
        string edit_id;
        string sqlText;
        string[] fields;
        string tablename;
        string auditlist;
        //string strtable;
        //string index;
        //string tuhao;
        //string zongxu;
        //string name;
        //string guige;
        //string caizhi;
        //string num;
        //string uweight;
        //string totalweight;
        //string xinzhuang;
        //string zhuangtai;
        //string biaozhun;
        //string process;
        //string delivery;
        //string casenum;
        //string remark;
        //int count = 0;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
            }
        }

        protected void InitInfo()
        {
            ms_id = Request.QueryString["id"];
            detail_id = Request.QueryString["msdetail_id"];
            edit_id = Request.QueryString["msedit_id"];
            if (ms_id != null)
            {
                fields = ms_id.Split('.');
                ms_no.Value = ms_id;
                tsa_id.Text = fields[0].ToString();
                status.Value = "0";
            }
            #region
            else
            {
                if (detail_id != null)
                {
                    fields = detail_id.Split('.');
                    tsa_id.Text = fields[0].ToString();
                    ms_no.Value = fields[0].ToString() +'.'+ fields[1].ToString();
                    status.Value = fields[2].ToString();
                    if (int.Parse(status.Value) > 1)
                    {
                        Response.Redirect("TM_MS_Detail_Audit.aspx?id=" + ms_no.Value);
                    }
                }
                else
                {
                    fields = edit_id.Split('.');
                    tsa_id.Text = fields[0].ToString();
                    ms_no.Value = fields[0].ToString() +'.'+ fields[1].ToString();
                    status.Value = fields[2].ToString();
                }
            }
            #endregion
            sqlText = "select TSA_PJID,TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE ";
            sqlText += "from TBPM_TCTSASSGN where TSA_ID='" + tsa_id.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                pro_id.Value = dr[0].ToString();
                lab_proname.Text = dr[1].ToString();
                lab_engname.Text = dr[2].ToString();
                ms_list.Value = dr[3].ToString();
            }
            dr.Close();
            GetMSData();   
        }

        //初始化表名
        private void InitList()
        {
            #region
            fields = ms_no.Value.Split('.');
            string[] cols = fields[1].ToString().Split('/');
            if (cols[0].ToString().Length > 6)
            {
                //tablename = "TBPM_MSCHANGE";
                auditlist = "TBPM_MSCHANGERVW";
            }
            else
            {
                auditlist = "TBPM_MSFORALLRVW";
            }
            switch (ms_list.Value)
            {
                case "回转窑":
                    //strtable = "TBPM_STRINFOHZY";
                    tablename = "TBPM_MSOFHZY";
                    break;
                case "球、立磨":
                    //strtable = "TBPM_STRINFOQLM";
                    tablename = "TBPM_MSOFQLM";
                    break;
                case "篦冷机":
                    //strtable = "TBPM_STRINFOBLJ";
                    tablename = "TBPM_MSOFBLJ";
                    break;
                case "堆取料机":
                    //strtable = "TBPM_STRINFODQLJ";
                    tablename = "TBPM_MSOFDQJ";
                    break;
                case "钢结构及非标":
                    //strtable = "TBPM_STRINFOGFB";
                    tablename = "TBPM_MSOFGFB";
                    break;
                case "电气及其他":
                    //strtable = "TBPM_STRINFODQO";
                    tablename = "TBPM_MSOFDQO";
                    break;
                default:
                    break;
            }
            #endregion
        }

        //查询满足条件的制作明细
        private void GetMSData()
        {
            InitList();
            sqlText = "select count(MS_PID) from " + tablename + " where MS_PID='" + ms_no.Value + "' ";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                if (int.Parse(dr[0].ToString()) == 0)
                {
                    Response.Redirect("TM_MS_TEMP_InitData.aspx?bgid=" + ms_no.Value);
                }
            }
            dr.Close();
            sqlText = "select MS_INDEX,MS_TUHAO,MS_ZONGXU,MS_NAME,MS_GUIGE,MS_CAIZHI,MS_UNUM,MS_UWGHT,";
            sqlText += "MS_TLWGHT,MS_MASHAPE,MS_MASTATE,MS_STANDARD,MS_PROCESS,MS_NOTE ";
            sqlText += "from " + tablename + " where MS_PID='" + ms_no.Value + "' ";
            if (status.Value == "3" || status.Value == "5" || status.Value == "7")
            {
                sqlText += "and MS_STATE='4' ";//驳回
            }
            else
            {
                sqlText += "and MS_STATE in ('0','1','2','3','5') ";
            }
            sqlText += "order by dbo.f_formatstr(MS_INDEX, '.')";
            DBCallCommon.BindGridView(GridView1, sqlText);
        }

        #region

        //protected DataTable GetDataFrom()
        //{
        //    DataTable dt1 = new DataTable("Table1");
        //    dt1.Columns.Add("MS_ID");
        //    dt1.Columns.Add("MS_INDEX");
        //    dt1.Columns.Add("MS_TUHAO");
        //    dt1.Columns.Add("MS_ZONGXU");
        //    dt1.Columns.Add("MS_NAME");
        //    dt1.Columns.Add("MS_GUIGE");
        //    dt1.Columns.Add("MS_CAIZHI");
        //    dt1.Columns.Add("MS_UNUM");
        //    dt1.Columns.Add("MS_UWGHT");
        //    dt1.Columns.Add("MS_TLWGHT");
        //    dt1.Columns.Add("MS_MASHAPE");
        //    dt1.Columns.Add("MS_MASTATE");
        //    dt1.Columns.Add("MS_STANDARD");
        //    dt1.Columns.Add("MS_PROCESS");
        //    dt1.Columns.Add("MS_TIMERQ");
        //    dt1.Columns.Add("MS_NOTE");
        //    //dt1.Columns.Add("MS_DELIVERY");
        //    for (int i = 0; i < GridView1.Rows.Count; i++)
        //    {
        //        GridViewRow gRow = GridView1.Rows[i];
        //        DataRow newRow = dt1.NewRow();
        //        newRow[0] = ((Label)gRow.FindControl("ID")).Text;
        //        newRow[1] = ((HtmlInputText)gRow.FindControl("Index")).Value;
        //        newRow[2] = ((HtmlInputText)gRow.FindControl("tuhao")).Value;
        //        newRow[3] = ((HtmlInputText)gRow.FindControl("zongxu")).Value;
        //        newRow[4] = ((HtmlInputText)gRow.FindControl("Name")).Value;
        //        newRow[5] = ((HtmlInputText)gRow.FindControl("Guige")).Value;
        //        newRow[6] = ((HtmlInputText)gRow.FindControl("Caizhi")).Value;
        //        newRow[7] = ((HtmlInputText)gRow.FindControl("Num")).Value;
        //        newRow[8] = ((HtmlInputText)gRow.FindControl("danzhong")).Value;
        //        newRow[9] = ((HtmlInputText)gRow.FindControl("zongzhong")).Value;
        //        newRow[10] = ((HtmlInputText)gRow.FindControl("xinzhuang")).Value;
        //        newRow[11] = ((HtmlInputText)gRow.FindControl("zhuangtai")).Value;
        //        newRow[12] = ((HtmlInputText)gRow.FindControl("biaozhun")).Value;
        //        newRow[13] = ((HtmlInputText)gRow.FindControl("gongyiliucheng")).Value;
        //        newRow[14] = ((HtmlInputText)gRow.FindControl("xianghao")).Value;
        //        newRow[15] = ((HtmlInputText)gRow.FindControl("remark")).Value;
        //        //newRow[16] = ((Label)gRow.FindControl("delivery")).Text;
        //        dt1.Rows.Add(newRow);
        //    }
        //    dt1.AcceptChanges();
        //    return dt1;
        //}

        ////生成输入行函数
        //private void CreateNewRow(int num)
        //{
        //    DataTable dt = this.GetDataFrom();
        //    for (int i = 0; i < num; i++)
        //    {
        //        DataRow newRow = dt.NewRow();
        //        dt.Rows.Add(newRow);
        //    }
        //    this.GridView1.DataSource = dt;
        //    this.GridView1.DataBind();
        //}

        //protected void Deletebind()
        //{
        //    InitList();
        //    //删除制作明细表中该批号下的数据
        //    sqlText = "delete from " + tablename + " ";
        //    sqlText += "where MS_PID='" + ms_no.Value + "' and MS_STATE in ('0','1','4')";
        //    DBCallCommon.ExeSqlText(sqlText);
        //    //删除工程下制作明细该批号下的数据
        //    sqlText = "delete from TBPM_MSOFNEW where MS_PID='" + ms_no.Value + "' ";
        //    DBCallCommon.ExeSqlText(sqlText);
        //}

        #endregion

        protected void btnreview_Click(object sender, EventArgs e)
        {
            #region
            //InitList();
            //Deletebind();
            //for(int i=0;i<GridView1.Rows.Count;i++)
            //{
            //    GridViewRow gr=GridView1.Rows[i];
            //    index = ((HtmlInputText)gr.FindControl("Index")).Value;
            //    tuhao = ((HtmlInputText)gr.FindControl("tuhao")).Value;
            //    zongxu = ((HtmlInputText)gr.FindControl("zongxu")).Value;
            //    name = ((HtmlInputText)gr.FindControl("Name")).Value;
            //    guige = ((HtmlInputText)gr.FindControl("Guige")).Value;
            //    caizhi = ((HtmlInputText)gr.FindControl("Caizhi")).Value;
            //    num = ((HtmlInputText)gr.FindControl("Num")).Value;
            //    uweight = ((HtmlInputText)gr.FindControl("danzhong")).Value;
            //    totalweight = ((HtmlInputText)gr.FindControl("zongzhong")).Value;
            //    xinzhuang = ((HtmlInputText)gr.FindControl("xinzhuang")).Value;
            //    zhuangtai = ((HtmlInputText)gr.FindControl("zhuangtai")).Value;
            //    biaozhun = ((HtmlInputText)gr.FindControl("biaozhun")).Value;
            //    process = ((HtmlInputText)gr.FindControl("gongyiliucheng")).Value;
            //    casenum = ((HtmlInputText)gr.FindControl("xianghao")).Value;
            //    remark = ((HtmlInputText)gr.FindControl("remark")).Value;
            //    //插入制作明细表中该批号下的数据
            //    sqlText = "insert into " + tablename + " ";
            //    sqlText += "(MS_PID,MS_INDEX,MS_TUHAO,MS_ZONGXU,MS_PJID,MS_PJNAME,MS_ENGID,";
            //    sqlText += "MS_ENGNAME,MS_NAME,MS_GUIGE,MS_CAIZHI,MS_UNUM,MS_UWGHT,MS_TLWGHT,";
            //    sqlText += "MS_MASHAPE,MS_MASTATE,MS_STANDARD,MS_PROCESS,MS_TIMERQ,MS_NOTE,MS_STATE) ";
            //    sqlText += "values ('" + ms_no.Value + "','"+index+"','" + tuhao + "','" + zongxu + "','" + pro_id.Value + "',";
            //    sqlText += "'" + lab_proname.Text + "','" + tsa_id.Text + "','" + lab_engname.Text + "','" + name + "',";
            //    sqlText += "'" + guige + "','" + caizhi + "','" + num + "','" + uweight + "','" + totalweight + "','"+xinzhuang+"',";
            //    sqlText += "'" + zhuangtai + "','" + biaozhun + "','" + process + "','" + casenum + "','" + remark + "','1')";
            //    DBCallCommon.ExeSqlText(sqlText);
            //    //插入工程制作明细表中该批号下的数据
            //    sqlText = "insert into TBPM_MSOFNEW ";
            //    sqlText += "(MS_PID,MS_INDEX,MS_TUHAO,MS_ZONGXU,MS_PJID,MS_PJNAME,MS_ENGID,";
            //    sqlText += "MS_ENGNAME,MS_NAME,MS_GUIGE,MS_CAIZHI,MS_UNUM,MS_UWGHT,MS_TLWGHT,";
            //    sqlText += "MS_MASHAPE,MS_MASTATE,MS_STANDARD,MS_PROCESS,MS_TIMERQ,MS_NOTE,MS_STATE) ";
            //    sqlText += "values ('" + ms_no.Value + "','" + index + "','" + tuhao + "','" + zongxu + "','" + pro_id.Value + "',";
            //    sqlText += "'" + lab_proname.Text + "','" + tsa_id.Text + "','" + lab_engname.Text + "','" + name + "',";
            //    sqlText += "'" + guige + "','" + caizhi + "','" + num + "','" + uweight + "','" + totalweight + "','" + xinzhuang + "',";
            //    sqlText += "'" + zhuangtai + "','" + biaozhun + "','" + process + "','" + casenum + "','" + remark + "','1')";
            //    DBCallCommon.ExeSqlText(sqlText);
            //}
            #endregion
            InitList();
            //明细下推审核时该批号下的制作明细状态置为1
            sqlText = "update " + tablename + " set MS_STATE='1' where MS_PID='" + ms_no.Value + "' ";
            DBCallCommon.ExeSqlText(sqlText);
            sqlText = "update TBPM_MSOFNEW set MS_STATE='1' where MS_PID='" + ms_no.Value + "' ";
            DBCallCommon.ExeSqlText(sqlText);
            if (status.Value == "0" || status.Value == "3" || status.Value == "5" || status.Value == "7")
            {
                sqlText = "update "+auditlist+" set MS_STATE='1',";
                sqlText += "MS_SUBMITTM='',MS_REVIEWA='',MS_REVIEWANAME='',";
                sqlText += "MS_REVIEWAADVC='',MS_REVIEWATIME='',MS_REVIEWB='',";
                sqlText += "MS_REVIEWBNAME='',MS_REVIEWBADVC='',MS_REVIEWBTIME='',";
                sqlText += "MS_REVIEWC='',MS_REVIEWCNAME='',MS_REVIEWCADVC='',";
                sqlText += "MS_REVIEWCTIME='',MS_ADATE='' ";
                sqlText += "where MS_ID='" + ms_no.Value + "' and MS_STATE='"+status.Value+"'";
                DBCallCommon.ExeSqlText(sqlText);
            }
            Response.Redirect("TM_MS_Detail_Audit.aspx?id=" + ms_no.Value);
        }

        #region

        //protected void btnadd_Click(object sender, EventArgs e)
        //{
        //    if (addId.Value=="1")
        //    {
        //        CreateNewRow(Convert.ToInt32(txtnum.Text));
        //        txtnum.Text = "";
        //    }  
        //}

        //protected void btndelete_Click(object sender, EventArgs e)
        //{
        //    DataTable dt = this.GetDataFrom();
        //    if (txtid.Value == "1")
        //    {
        //        for (int i = 0; i < GridView1.Rows.Count; i++)
        //        {
        //            GridViewRow gRow = GridView1.Rows[i];
        //            CheckBox chk = (CheckBox)gRow.FindControl("chk");
        //            if (chk.Checked)
        //            {
        //                dt.Rows.RemoveAt(i - count);
        //                count++;
        //            }
        //        }
        //    }
        //    this.GridView1.DataSource = dt;
        //    this.GridView1.DataBind();
        //}

        //protected void btninsert_Click(object sender, EventArgs e)
        //{
        //    if (istid.Value == "1")
        //    {
        //        DataTable dt = this.GetDataFrom();
        //        for (int i = 0; i < GridView1.Rows.Count; i++)
        //        {
        //            GridViewRow gRow = GridView1.Rows[i];
        //            CheckBox chk = (CheckBox)gRow.FindControl("chk");
        //            if (chk.Checked)
        //            {
        //                DataRow newRow = dt.NewRow();
        //                dt.Rows.InsertAt(newRow, i + 1 + count);
        //                count++;
        //            }
        //        }
        //        istid.Value = "0";
        //        this.GridView1.DataSource = dt;
        //        this.GridView1.DataBind();
        //    }
        //}

        #endregion

        //生成装箱单
        //protected void packlist_Click(object sender, EventArgs e)
        //{
        //    string id = "";
        //    string now = DateTime.Now.ToString();
        //    now = now.Replace("-", "");
        //    now = now.Replace(" ", "");
        //    string pack_no = "PK" + " " + tsa_id.Text + now.Replace(":", "");
        //    InitList();
        //    for (int i = 0; i < GridView1.Rows.Count; i++)
        //    {
        //        GridViewRow gr = GridView1.Rows[i];
        //        id = ((Label)gr.FindControl("ID")).Text;
        //        name = ((TextBox)gr.FindControl("Name")).Text;
        //        tuhao = ((TextBox)gr.FindControl("tuhao")).Text;
        //        num = ((TextBox)gr.FindControl("Num")).Text;
        //        uweight = ((TextBox)gr.FindControl("danzhong")).Text;
        //        delivery = ((Label)gr.FindControl("delivery")).Text.Trim();
        //        casenum = ((TextBox)gr.FindControl("xianghao")).Text;
        //        if (casenum!=""&&delivery != "1")
        //        {
        //            sqlText = "update " + tablename + " set MS_DELIVERY='1' ";
        //            sqlText += "where MS_ID='"+id+"'";
        //            DBCallCommon.ExeSqlText(sqlText);
        //            sqlText = "insert into TBPM_PACKINGLIST ";
        //            sqlText += "(PL_PACKLISTNO,PL_PACKAGENO,PL_PACKNAME,PL_MARKINGNO,PL_PACKQLTY,PL_SINGLENETWGHT,PL_STATE)";
        //            sqlText += " values ('" + pack_no + "','" + casenum + "','" + name + "','" + tuhao + "','" + num + "','" + uweight + "','0')";
        //            DBCallCommon.ExeSqlText(sqlText);
        //            count++;
        //        }
        //    }
        //    if (count != 0)
        //    {
        //        sqlText = "insert into TBPM_PACKLISTTOTAL ";
        //        sqlText += "(PLT_PACKLISTNO,PLT_PJID,PLT_PJNAME,PLT_ENGID,PLT_ENGNAME,PLT_SUBMITID,PLT_SUBMITNM,PLT_STATE)";
        //        sqlText += " values ('" + pack_no + "','" + pro_id.Value + "','" + lab_proname.Text + "','" + tsa_id.Text + "',";
        //        sqlText += "'" + lab_engname.Text + "','" + Session["UserID"] + "','" + Session["UserName"] + "','0')";
        //        DBCallCommon.ExeSqlText(sqlText);
        //        Response.Redirect("TM_Packing_List.aspx?id=" + pack_no);
        //    }
        //    else
        //    {
        //        Response.Write("<script>alert('提示:装箱单已提交!')</script>");
        //        return;
        //    }
        //}

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            //Response.Write("<script language=javascript>history.go(-2);</script>");
            Response.Redirect("TM_MS_Detail_View.aspx?detail_id=" + tsa_id.Text);
        }
    }
}
