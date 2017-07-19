using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_BGYP_PCHZ_Detail : System.Web.UI.Page
    {
        string flag = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            flag = Request.QueryString["action"].ToString();
            if (!IsPostBack)
            {
                if (flag == "add")//添加采购申请
                {
                    InitInfo();

                    Binddata_add();
                }
                if (flag == "view")
                {
                    txt_note.Enabled = false;
                    btnsave.Visible = false;
                    TextBoxDate.Enabled = false;
                    btnsave.Visible = false;

                    string Id = Request.QueryString["id"].ToString();
                    string sql = "select * from TBOM_BGYPPCHZ where PCCODE='" + Id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {

                        LabelCode.Text = Id.ToString();
                        TextBoxDate.Text = dt.Rows[0]["DATE"].ToString();
                        LabelDoc.Text = dt.Rows[0]["JBR"].ToString();
                        txt_first.Text = dt.Rows[0]["SHRF"].ToString();
                        //txt_second.Text = dt.Rows[0]["SHRS"].ToString();
                        firstid.Value = dt.Rows[0]["SHRFID"].ToString();
                        //secondid.Value = dt.Rows[0]["SHRSID"].ToString();
                        first_opinion.Text = dt.Rows[0]["SHRFNOTE"].ToString();
                        rblfirst.SelectedValue = dt.Rows[0]["SHRFRESULT"].ToString();
                        first_time.Text = dt.Rows[0]["SHRFDATE"].ToString();
                        txt_note.Text = dt.Rows[0]["NOTE"].ToString();
                    }
                    string sqltxt = "select * from View_TBOM_BGYPPCAPPLYINFO WHERE HZCODE='" + Id + "'";
                    DataTable DT = DBCallCommon.GetDTUsingSqlText(sqltxt);
                    GridView1.DataSource = DT;
                    GridView1.DataBind();
                }
                if (flag == "audit")
                {
                    txt_note.Enabled = false;
                    TextBoxDate.Enabled = false;
                    btnsave.Visible = true;
                    rblfirst.Enabled = true;
                    first_opinion.Enabled = true;
                    string Id = Request.QueryString["id"].ToString();
                    string sql = "select * from TBOM_BGYPPCHZ where PCCODE='" + Id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        state.Text = dt.Rows[0]["STATE"].ToString();
                        LabelCode.Text = Id.ToString();
                        TextBoxDate.Text = dt.Rows[0]["DATE"].ToString();
                        LabelDoc.Text = dt.Rows[0]["JBR"].ToString();
                        txt_first.Text = dt.Rows[0]["SHRF"].ToString();
                        //txt_second.Text = dt.Rows[0]["SHRS"].ToString();
                        firstid.Value = dt.Rows[0]["SHRFID"].ToString();
                        //secondid.Value = dt.Rows[0]["SHRSID"].ToString();
                        first_opinion.Text = dt.Rows[0]["SHRFNOTE"].ToString();
                        rblfirst.SelectedValue = dt.Rows[0]["SHRFRESULT"].ToString();
                        first_time.Text = dt.Rows[0]["SHRFDATE"].ToString();
                        txt_note.Text = dt.Rows[0]["NOTE"].ToString();
                    }
                    string sqltxt = "select * from View_TBOM_BGYPPCAPPLYINFO WHERE HZCODE='" + Id + "'";
                    DataTable DT = DBCallCommon.GetDTUsingSqlText(sqltxt);
                    GridView1.DataSource = DT;
                    GridView1.DataBind();
                }
                double jine = 0;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    jine += Convert.ToDouble(GridView1.Rows[i].Cells[8].Text.Trim());
                }
                lbljine.Text = jine.ToString();
            }
        }

        private void InitInfo()
        {
            //初始化单号

            LabelCode.Text = generateTempCode();

            string sql = "INSERT INTO TBOM_BGYP_INCODE(Code) VALUES ('" + LabelCode.Text + "')";

            DBCallCommon.ExeSqlText(sql);

            //制单人
            LabelDoc.Text = Session["UserName"].ToString();

            LabelDocCode.Text = Session["UserID"].ToString();

            TextBoxDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

        }

        protected void close(object sender, EventArgs e)
        {
            Response.Redirect("OM_BGYP_PCHZ.aspx");
        }

        private void Binddata_add()
        {
            string sql_use = "select * from View_TBOM_BGYPPCAPPLYINFO where (HZSTATE='' or HZSTATE is null) and STATE='2'";
            DataTable DT = DBCallCommon.GetDTUsingSqlText(sql_use);
            GridView1.DataSource = DT;
            GridView1.DataBind();
            double jine = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                jine += Convert.ToDouble(GridView1.Rows[i].Cells[8].Text.Trim());
            }
            lbljine.Text = jine.ToString();
        }


        protected string generateTempCode()
        {

            List<string> lt = new List<string>();

            string sql = "SELECT Code FROM TBOM_BGYP_INCODE WHERE len(Code)=11 and left(Code,4)='HZPC'";

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    lt.Add(sdr["Code"].ToString());
                }
            }

            sdr.Close();

            string[] wsidlist = lt.ToArray();

            if (wsidlist.Count<string>() == 0)
            {
                return "HZPC0000001";
            }
            else
            {
                string tempstr = wsidlist.Max<string>();
                int tempnum = Convert.ToInt32((tempstr.Substring(4, 7)));
                tempnum++;
                tempstr = "HZPC" + tempnum.ToString().PadLeft(7, '0');
                return tempstr;
            }
        }




        //保存操作

        protected void Save_Click(object sender, EventArgs e)
        {
            //此处是保存操作
            List<string> sqllist = new List<string>();

            string sql = "";
            string Code = LabelCode.Text;//单号
            string Date = TextBoxDate.Text;//日期
            string jbrid = LabelDocCode.Text;//制单人
            string jbr = LabelDoc.Text;
            //float jine = 0;
            string shrf = txt_first.Text;
            string shrfid = firstid.Value.Trim();
            string shrfnote = first_opinion.Text;
            string shrfdate = first_time.Text;
            string firstR = rblfirst.SelectedValue;
            string note = txt_note.Text;

            if (flag == "add")
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {

                    string pccode = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("hidPCCode")).Value.Trim();//物料代码
                    sql = "update TBOM_BGYPPCAPPLY  set HZSTATE='1',HZCODE='" + Code + "' where  PCCODE='" + pccode + "'";
                    sqllist.Add(sql);
                }
                sql = "insert into TBOM_BGYPPCHZ(PCCODE,JBR,JBRID,SHRF,SHRFID,SHRFRESULT,SHRFDATE,SHRFNOTE,DATE,STATE,NOTE) VALUES ('" + Code + "','" + jbr + "','" + jbrid + "','" + shrf + "','" + shrfid + "','" + firstR + "','" + shrfdate + "','" + shrfnote + "','" + Date + "','1','" + note.Trim() + "')";
                sqllist.Add(sql);
                DBCallCommon.ExecuteTrans(sqllist);

                //邮件提醒
                string _emailto = DBCallCommon.GetEmailAddressByUserID(shrfid);
                string _body = "办公用品汇总审批任务:"
                      + "\r\n编    号：" + Code
                      + "\r\n制单人：" + jbr
                      + "\r\n制单日期：" + Date
                      + "\r\n总额: " + lbljine.Text.Trim();

                string _subject = "您有新的【办公用品汇总】需要审批，请及时处理:" + Code;
                DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);

                Response.Redirect("OM_BGYP_PCHZ.aspx");
            }
            if (flag == "audit")
            {
                string zongfirst = "";
                if (Session["UserID"].ToString() == firstid.Value.ToString() && state.Text == "1")
                {
                    if (rblfirst.SelectedIndex == -1)
                    {
                        Response.Write("<script>alert('请选择审核结果!')</script>");
                        return;
                    }
                    else if (rblfirst.SelectedValue == "0")
                    {
                        zongfirst = "2";
                    }
                    else if (rblfirst.SelectedValue == "1")
                    {
                        zongfirst = "3";
                    }

                    sql = "update TBOM_BGYPPCHZ set STATE='" + zongfirst + "', SHRFDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "',SHRFNOTE='" + first_opinion.Text.ToString() + "',SHRFRESULT='" + rblfirst.SelectedValue + "' WHERE PCCODE='" + Code + "'";
                    DBCallCommon.ExeSqlText(sql);
                    if (zongfirst == "3")
                    {
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            string pccode = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("hidPCCode")).Value.Trim();//物料代码
                            sql = "update TBOM_BGYPPCAPPLY  set HZSTATE='2',HZCODE='" + Code + "' where  PCCODE='" + pccode + "'";
                            sqllist.Add(sql);
                        }
                        DBCallCommon.ExecuteTrans(sqllist);
                    }
                    Response.Redirect("OM_BGYP_PCHZ.aspx");
                }

            }
        }
        protected void btndaochu_click(object sender, EventArgs e)
        {

            if (flag == "view" || flag == "audit")
            {
                string filename = "办公用品采购申请单" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
                HttpContext.Current.Response.Clear();
                string sqltext = ""; ;
                string Id = Request.QueryString["id"].ToString();
                sqltext += "select * from View_TBOM_BGYPPCAPPLYINFO WHERE HZCODE='" + Id + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                //1.读取Excel到FileStream 
                using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("办公用品采购申请单.xls")))
                {
                    IWorkbook wk = new HSSFWorkbook(fs);
                    ISheet sheet0 = wk.GetSheetAt(0);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet0.CreateRow(i + 1);
                        row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                        row.CreateCell(1).SetCellValue("" + dt.Rows[i]["WLBM"].ToString());
                        row.CreateCell(2).SetCellValue("" + dt.Rows[i]["WLNAME"].ToString());
                        row.CreateCell(3).SetCellValue("" + dt.Rows[i]["WLMODEL"].ToString());
                        row.CreateCell(4).SetCellValue("" + dt.Rows[i]["WLUNIT"].ToString());
                        row.CreateCell(5).SetCellValue("" + dt.Rows[i]["WLNUM"].ToString());
                        row.CreateCell(6).SetCellValue("" + dt.Rows[i]["WLPRICE"].ToString());
                        row.CreateCell(7).SetCellValue("" + dt.Rows[i]["WLJE"].ToString());
                        row.CreateCell(8).SetCellValue("" + dt.Rows[i]["num"].ToString());
                        row.CreateCell(9).SetCellValue("" + dt.Rows[i]["DEPNAME"].ToString());
                        row.CreateCell(10).SetCellValue("" + dt.Rows[i]["Note"].ToString());

                    }

                    for (int i = 0; i <= dt.Columns.Count; i++)
                    {
                        sheet0.AutoSizeColumn(i);
                    }
                    sheet0.ForceFormulaRecalculation = true;

                    MemoryStream file = new MemoryStream();
                    wk.Write(file);
                    HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                    HttpContext.Current.Response.End();
                }
            }
        }

    }
}
