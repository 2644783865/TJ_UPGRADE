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
using System.IO;
using Microsoft.Office.Interop.Excel;
using ExcelApplication = Microsoft.Office.Interop.Excel.ApplicationClass;
using System.Runtime.InteropServices;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_Date_closemarshow : System.Web.UI.Page
    {
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
        public string gloabsheetno
        {
            get
            {
                object str = ViewState["gloabsheetno"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabsheetno"] = value;
            }
        }
        public string globlempcode
        {
            get
            {
                object str = ViewState["globlempcode"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["globlempcode"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["orderno"] != null)
                {
                    orderno.Text = Request.QueryString["orderno"].ToString();
                    gloabsheetno = Request.QueryString["orderno"].ToString();
                }
                if (Request.QueryString["shape"] != null)
                {
                    tb_shape.Text = Request.QueryString["shape"].ToString();
                    gloabshape = Request.QueryString["shape"].ToString();
                    if (tb_shape.Text == "非定尺板" || tb_shape.Text == "标(发运)" || tb_shape.Text == "标(组装)")
                    {
                        GridView1.Columns[14].Visible = false;
                        GridView1.Columns[15].Visible = false;
                    }
                }
                bind();
            }
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            //取得显示分页界面的那一行
            GridViewRow pagerRow = GridView1.BottomPagerRow;
            if (pagerRow != null)
            {
                //取得第一页。上一页。下一页。最后一页的超级链接
                LinkButton lnkBtnFirst = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnFirst");
                LinkButton lnkBtnPrev = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnPrev");
                LinkButton lnkBtnNext = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnNext");
                LinkButton lnkBtnLast = (LinkButton)pagerRow.Cells[0].FindControl("lnkBtnLast");

                //设置何时应该禁用第一页。上一页。下一页。最后一页的超级链接
                if (GridView1.PageIndex == 0)
                {
                    lnkBtnFirst.Enabled = false;
                    lnkBtnPrev.Enabled = false;
                }
                else if (GridView1.PageIndex == GridView1.PageCount - 1)
                {
                    lnkBtnNext.Enabled = false;
                    lnkBtnLast.Enabled = false;
                }
                else if (GridView1.PageCount <= 0)
                {
                    lnkBtnFirst.Enabled = false;
                    lnkBtnPrev.Enabled = false;
                    lnkBtnNext.Enabled = false;
                    lnkBtnLast.Enabled = false;
                }
                //从显示分页的行中取得用来显示页次与切换分页的DropDownList控件
                DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("page_DropDownList");

                //根据欲显示的数据源的总页数，创建DropDownList控件的下拉菜单内容
                if (pageList != null)
                {
                    int intPage;
                    for (intPage = 0; intPage <= GridView1.PageCount - 1; intPage++)
                    {
                        //创建一个ListItem对象来存放分页列表
                        int pageNumber = intPage + 1;
                        ListItem item = new ListItem(pageNumber.ToString());

                        //交替显示背景颜色
                        switch (pageNumber % 2)
                        {
                            case 0: item.Attributes.Add("style", "background:#CDC9C2;");
                                break;
                            case 1: item.Attributes.Add("style", "color:red; background:white;");
                                break;
                        }
                        if (intPage == GridView1.PageIndex)
                        {
                            item.Selected = true;
                        }
                        pageList.Items.Add(item);
                    }
                }
                //显示当前所在页数与总页数
                System.Web.UI.WebControls.Label pagerLabel = (System.Web.UI.WebControls.Label)pagerRow.Cells[0].FindControl("lblCurrrentPage");

                if (pagerLabel != null)
                {

                    int currentPage = GridView1.PageIndex + 1;
                    pagerLabel.Text = "第" + currentPage.ToString() + "页（共" + GridView1.PageCount.ToString() + " 页）";

                }
            }

        }
        protected void page_DropDownList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //取得显示分页界面的那一行
            GridViewRow pagerRow = GridView1.BottomPagerRow;
            //从显示页数的行中取得显示页数的DropDownList控件
            DropDownList pageList = (DropDownList)pagerRow.Cells[0].FindControl("page_DropDownList");
            //将GridView移至用户所选择的页数
            GridView1.PageIndex = pageList.SelectedIndex;
            bind();
            //getData();不用数据源需要绑定
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                //鼠标经过时，行背景色变 
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
                //鼠标移出时，行背景色变 
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#FFFFFF'");
                //点击返回当前行第6列的值
                //e.Row.Attributes.Add("onclick", "window.returnValue=\"" + e.Row.Cells[0].Text.Trim() + "\";window.close();");
                string state = ((System.Web.UI.WebControls.Label)e.Row.FindControl("purstate")).Text;
                if (Convert.ToDouble(state) != 0 && Convert.ToDouble(state) != 3 && Convert.ToDouble(state) != 4)
                {
                    ((System.Web.UI.WebControls.CheckBox)e.Row.FindControl("CheckBox1")).Enabled = false;
                }
                string cstate = ((System.Web.UI.WebControls.Label)e.Row.FindControl("PUR_CSTATE")).Text;
                if (cstate == "2")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                }
            }
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            bind();
        }

        protected void bind()
        {
            string sqltext = "";
            sqltext = "SELECT planno, pjid, pjnm, engid, engnm, ptcode, " +
                           "marid, marnm, margg, marcz, margb, marunit,marfzunit,isnull(length,0) as length ,isnull(width,0) as width,isnull(num,0) as num,isnull(fznum,0) as fznum, " +
                           "isnull(rpnum,0) as rpnum, isnull(rpfznum,0) as rpfznum,jstimerq,cgrid, cgrnm, isnull(purstate,0) as purstate, purnote, picno,orderno,PUR_MASHAPE,PUR_CSTATE,PUR_ZYDY,PUR_TUHAO  " +
                           "FROM  View_TBPC_PURCHASEPLAN_IRQ_ORDER   where planno='" + orderno.Text + "' and (PUR_CSTATE='1' or PUR_CSTATE='2') order by ptcode asc";
            DBCallCommon.BindGridView(GridView1, sqltext);

        }

        protected void btn_daochu_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "SELECT planno, pjid, pjnm, engid, engnm, ptcode, " +
                            "marid, marnm, margg, marcz, margb, marunit,marfzunit,isnull(length,0),isnull(width,0),isnull(num,0),isnull(fznum,0), " +
                            "isnull(rpnum,0), isnull(rpfznum,0),jstimerq,cgrid, cgrnm, isnull(purstate,0) as purstate, purnote, picno,orderno,PUR_MASHAPE,PUR_CSTATE,PUR_ZYDY,PUR_TUHAO  " +
                            "FROM  View_TBPC_PURCHASEPLAN_IRQ_ORDER   where planno='" + orderno.Text + "' and (PUR_CSTATE='1' or PUR_CSTATE='2')";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }

        private void ExportDataItem(System.Data.DataTable objdt)
        {
            Application m_xlApp = new Application();
            Workbooks workbooks = m_xlApp.Workbooks;
            Workbook workbook;// = workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet wksheet;
            workbook = m_xlApp.Workbooks.Open(System.Web.HttpContext.Current.Server.MapPath("占用代用物料表") + ".xls", Type.Missing, false, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);

            m_xlApp.Visible = false;    // Excel不显示  
            m_xlApp.DisplayAlerts = false;        // 关闭提示，采用默认的方案执行（合并单元格的时候，如果两个单元格都有数据，会出现一个确认提示）  

            wksheet = (Worksheet)workbook.Sheets.get_Item(1);

            System.Data.DataTable dt = objdt;

            // 填充数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                wksheet.Cells[i + 3, 1] = Convert.ToString(i + 1);//序号

                wksheet.Cells[i + 3, 2] = "'" + dt.Rows[i]["planno"].ToString();//批号

                wksheet.Cells[i + 3, 3] = "'" + dt.Rows[i]["pjnm"].ToString();//项目

                wksheet.Cells[i + 3, 4] = "'" + dt.Rows[i]["engnm"].ToString();//工程

                wksheet.Cells[i + 3, 5] = "'" + dt.Rows[i]["ptcode"].ToString();//计划跟踪号

                wksheet.Cells[i + 3, 6] = "'" + dt.Rows[i]["PUR_TUHAO"].ToString();//图号/标识号

                wksheet.Cells[i + 3, 7] = "'" + dt.Rows[i]["marid"].ToString();//物料编码

                wksheet.Cells[i + 3, 8] = "'" + dt.Rows[i]["marnm"].ToString();//名称

                wksheet.Cells[i + 3, 9] = "'" + dt.Rows[i]["margg"].ToString();//规格

                wksheet.Cells[i + 3, 10] = "'" + dt.Rows[i]["marcz"].ToString();//材质

                wksheet.Cells[i + 3, 11] = "'" + dt.Rows[i]["margb"].ToString();//国标

                wksheet.Cells[i + 3, 12] = "'" + dt.Rows[i]["length"].ToString();//长度

                wksheet.Cells[i + 3, 13] = "'" + dt.Rows[i]["width"].ToString();//宽度

                wksheet.Cells[i + 3, 14] = "'" + dt.Rows[i]["num"].ToString();//需用数量

                wksheet.Cells[i + 3, 15] = "'" + dt.Rows[i]["marunit"].ToString();//单位

                wksheet.Cells[i + 3, 16] = "'" + dt.Rows[i]["fznum"].ToString();//需用辅助数量

                wksheet.Cells[i + 3, 17] = "'" + dt.Rows[i]["marfzunit"].ToString();//辅助单位

                wksheet.Cells[i + 3, 18] = "'" + dt.Rows[i]["PUR_ZYDY"].ToString();//备注

                wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 18]).HorizontalAlignment = XlHAlign.xlHAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 18]).VerticalAlignment = XlVAlign.xlVAlignCenter;
                wksheet.get_Range(wksheet.Cells[i + 3, 1], wksheet.Cells[i + 3, 18]).Borders.LineStyle = Microsoft.Office.Interop.Excel.XlLineStyle.xlContinuous;
            }
            //设置列宽
            wksheet.Columns.EntireColumn.AutoFit();//列宽自适应

            string filename = Server.MapPath("/PC_Data/ExportFile/" + "采购占用代用物料表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls");

            ExportExcel_Exit(filename, workbook, m_xlApp, wksheet);
        }

        /// <summary>
        /// 输出Excel文件并退出
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="workbook"></param>
        private void ExportExcel_Exit(string filename, Workbook workbook, Application m_xlApp, Worksheet wksheet)
        {
            try
            {

                workbook.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                workbook.Close(Type.Missing, Type.Missing, Type.Missing);
                m_xlApp.Workbooks.Close();
                m_xlApp.Quit();
                m_xlApp.Application.Quit();

                System.Runtime.InteropServices.Marshal.ReleaseComObject(wksheet);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                System.Runtime.InteropServices.Marshal.ReleaseComObject(m_xlApp);

                wksheet = null;
                workbook = null;
                m_xlApp = null;
                GC.Collect();

                //下载

                System.IO.FileInfo path = new System.IO.FileInfo(filename);

                //同步，异步都支持
                HttpResponse contextResponse = HttpContext.Current.Response;
                contextResponse.Redirect(string.Format("~/PC_Data/ExportFile/{0}", path.Name), false);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close();</script>");
        }
        protected void btn_ZY_Click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            int temp = isselected();
            if (temp==0)//是否选择数据
            {
                string sqltext = "";
                string ptcode = "";
                string marid = "";
                string shape = "";
                string note = "";
                double length = 0;
                double width = 0;
                double num = 0;
                double fznum = 0;
                string beizhu="";
                string pjid = "";
                string engid = "";
                string zypihao = "";
                string sql = "select PR_PJID,PR_ENGID,PUR_MASHAPE,PR_NOTE from TBPC_PCHSPLANRVW where PR_SHEETNO='" + orderno.Text + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    pjid = dt.Rows[0]["PR_PJID"].ToString();
                    engid = dt.Rows[0]["PR_ENGID"].ToString();
                    shape = dt.Rows[0]["PUR_MASHAPE"].ToString();
                    note = dt.Rows[0]["PR_NOTE"].ToString();
                }

                //查询批号是否存在
                sql = "select pr_pcode from TBPC_MARSTOUSETOTAL where pr_pcode like '" + orderno.Text + "%' order by pr_pcode ASC";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    int number = dt.Rows.Count;
                    if (number == 1)
                    {
                        if (dt.Rows[0][0].ToString().Contains("#"))
                        {
                            zypihao = orderno.Text + "#" + Convert.ToString(Convert.ToDouble(dt.Rows[number - 1][0].ToString().Split('#')[1]) + 1);
                        }
                        else
                        {
                            zypihao = orderno.Text + "#1";
                        }
                    }
                    else
                    {
                        zypihao = orderno.Text + "#" + Convert.ToString(Convert.ToDouble(dt.Rows[number - 1][0].ToString().Split('#')[1]) + 1);
                    }
                }
                else
                {
                    zypihao = orderno.Text;
                }
                globlempcode = zypihao;

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                    if (cbk.Checked)
                    {
                        ptcode = ptcode = ((System.Web.UI.WebControls.Label)gr.FindControl("ptcode")).Text;
                        marid = ((System.Web.UI.WebControls.Label)gr.FindControl("marid")).Text;
                        length = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("length")).Text);
                        width = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("width")).Text);
                        num = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("num")).Text);
                        fznum = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("fznum")).Text);
                        beizhu = ((System.Web.UI.WebControls.Label)gr.FindControl("PUR_ZYDY")).Text;
                        sqltext = "insert into TBPC_MARSTOUSEALL(PUR_PCODE,PUR_PJID,PUR_ENGID,PUR_PTCODE, PUR_MARID, PUR_LENGTH,PUR_WIDTH,PUR_NUM, PUR_FZNUM,PUR_USTNUM,PUR_USTFZNUM,PUR_NOTE) values ('" + zypihao + "','" + pjid + "','" + engid + "','" + ptcode + "','" + marid + "'," + length + "," + width + "," + num + "," + fznum + ",'" + num + "','" + fznum + "','" + beizhu + "')";
                        sqltextlist.Add(sqltext);
                        sqltext = "update TBPC_PURCHASEPLAN set PUR_STATE='1' where PUR_PTCODE='" + ptcode + "' and (PUR_CSTATE='1'or PUR_CSTATE='2')";
                        sqltextlist.Add(sqltext);
                    }
                }
                string zdid = Session["UserID"].ToString();
                string zdtm = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                sqltext = "insert into TBPC_MARSTOUSETOTAL (PR_PCODE,PR_PJID,PR_ENGID,PUR_MASHAPE,PR_NOTE) values ('" + zypihao + "','" + pjid + "','" + engid + "','" + shape + "','" + note + "')";
                sqltextlist.Add(sqltext);
                sqltext = "update TBPC_MARSTOUSETOTAL set PR_REVIEWA='" + zdid + "', " +
                              "PR_REVIEWATIME='" + zdtm + "',PR_STATE='1' " +
                              "where  PR_PCODE='" + zypihao + "'";
                sqltextlist.Add(sqltext);
                DBCallCommon.ExecuteTrans(sqltextlist);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "openclosemodewin1();", true);  
                
            }
            else if(temp==1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('你选择了已经提交库存占用审核的数据！');", true);
            }
            else if (temp == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有提交库存占用审核的权限！');", true);
            }
           
        }

        //判断是否选择数据
        protected int isselected()
        {
            int count = 0;
            int j = 0;
            int temp = 0;
            int k = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                if (cbk.Checked)
                {
                    count++;
                    string ptcode = ((System.Web.UI.WebControls.Label)gr.FindControl("ptcode")).Text;
                    string sql = "select * from TBPC_MARSTOUSEALL where PUR_PTCODE='" + ptcode + "' and PUR_PCODE='" + orderno.Text + "'";
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        j++;
                    }
                    if (Session["UserDeptID"].ToString() != "06" && Session["UserDeptID"].ToString() != "07")
                    {
                        k++;
                    }
                }
            }
            if (count == 0)//没选择数据
            {
                temp = 1;
            }
            else if (j > 0)//已经插入的数据
            {
                temp = 2;
            }
            else if (k > 0)
            {
                //暂时去掉部门的权限
                //temp = 3;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }

        protected int isselected1()
        {
            int count = 0;
            int j = 0;
            int temp = 0;
            int k = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                if (cbk.Checked)
                {
                    count++;
                    string ptcode = ((System.Web.UI.WebControls.Label)gr.FindControl("ptcode")).Text;
                    string sql = "select * from TBPC_MARREPLACEALL where MP_PTCODE='" + ptcode + "' and charindex(MP_CODE,5,1)='1'";
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        j++;
                    }
                    if (Session["UserDeptID"].ToString() != "06")
                    {
                        k++;
                    }
                }
            }
            if (count == 0)//没选择数据
            {
                temp = 1;
            }
            else if (j > 0)//已经插入的数据
            {
                temp = 2;
            }
            else if (k > 0)//采购部权利
            {
                //temp = 3;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }

        protected void btn_XSDY_Click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            int temp = isselected1();
            if (temp==0)//是否选择数据
            {
                string sqltext = "";
                string ptcode = "";
                string marid = "";
                double length = 0;
                double width = 0;
                double num = 0;
                double fznum = 0;
                string pjid = "";
                string engid = "";
                string mpcode = generatecode();
                globlempcode = mpcode;
                string fillid = Session["UserID"].ToString();
                string filltime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                string reviewaid = "";
                string chargeid = "";

                string sql = "select PR_PJID,PR_ENGID,PR_SQREID,PR_FZREID from TBPC_PCHSPLANRVW where PR_SHEETNO='" + orderno.Text + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    pjid = dt.Rows[0]["PR_PJID"].ToString();
                    engid = dt.Rows[0]["PR_ENGID"].ToString();
                    reviewaid = dt.Rows[0]["PR_SQREID"].ToString();
                    chargeid = dt.Rows[0]["PR_FZREID"].ToString();
                }
                sqltext = "INSERT INTO TBPC_MARREPLACETOTAL(MP_CODE,MP_PLANPCODE,MP_PJID," +
                          "MP_ENGID,MP_FILLFMID,MP_FILLFMTIME,MP_REVIEWAID,MP_CHARGEID)  " +
                          "VALUES('" + mpcode + "','" + orderno.Text + "','" + pjid + "','" + engid + "','" + fillid + "'," +
                          "'" + filltime + "','" + reviewaid + "','" + chargeid + "')";
                sqltextlist.Add(sqltext);
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                    if (cbk.Checked)
                    {
                        ptcode = ((System.Web.UI.WebControls.Label)gr.FindControl("ptcode")).Text;
                        marid = ((System.Web.UI.WebControls.Label)gr.FindControl("marid")).Text;
                        length = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("length")).Text);
                        width = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("width")).Text);
                        num = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("num")).Text);
                        fznum = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("fznum")).Text);
                        sqltext = "INSERT INTO TBPC_MARREPLACEALL(MP_CODE,MP_PTCODE,MP_MARID," +
                                          "MP_NUM,MP_FZNUM,MP_LENGTH,MP_WIDTH) " +
                                          "VALUES('" + mpcode + "','" + ptcode + "','" + marid + "'," +
                                           +num + "," + fznum + "," + length + "," + width + ")";
                        sqltextlist.Add(sqltext);
                        sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_STATE='2' where PUR_PTCODE='" + ptcode + "' and PUR_CSTATE='1'";
                        sqltextlist.Add(sqltext);
                    }
                }
                DBCallCommon.ExecuteTrans(sqltextlist);
                //Response.Redirect("~/PC_Data/PC_TBPC_Marreplace_edit.aspx?mpcode=" + mpcode);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "openclosemodewin();", true);
                //Response.Write("<script>javascript:window.close();</script>");
            }
            else if(temp==1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('你选择了已经提交相似代用审核的数据！');", true);
            }
            else if (temp == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('你没有提交相似待用的权限！');", true);
            }
        }

        //代用单号
        private string generatecode()
        {
            string repcode = "";
            string subcode = "";
            string sqltext = "SELECT TOP 1 MP_CODE FROM TBPC_MARREPLACETOTAL WHERE MP_CODE LIKE '" + DateTime.Now.Year.ToString() + "0" + "%' ORDER BY MP_CODE DESC";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                subcode = Convert.ToString(dt.Rows[0][0]).Substring(Convert.ToString(dt.Rows[0][0]).ToString().Length - 5, 5);//后五位流水号
                subcode = Convert.ToString(Convert.ToInt32(subcode) + 1);
                subcode = subcode.PadLeft(5, '0');
            }
            else
            {
                subcode = "00001";
            }
            repcode = DateTime.Now.Year.ToString() + "0" + subcode;
            return repcode;
        }

        protected void btn_FClose_Click(object sender, EventArgs e)
        {
            int temp = isselected2();
            if (temp==0)//是否选择数据
            {
                string ptcode = "";
                string sql = "";
                double num = 0;
                double fznum = 0;
                int l = 0;
                int m = 0;
                List<string> number=new List<string>();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    
                    GridViewRow gr = GridView1.Rows[i];
                    System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                    if (cbk.Checked)
                    {
                        ptcode = ((System.Web.UI.WebControls.Label)gr.FindControl("ptcode")).Text;
                        number.Add(ptcode);

                        sql = "select PUR_NUM,PUR_FZNUM from TBPC_PURCHASEPLAN where PUR_PTCODE = '" + ptcode + "'";
                        System.Data.DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql);
                        num += Convert.ToDouble(dt2.Rows[0]["PUR_NUM"].ToString());
                        fznum += Convert.ToDouble(dt2.Rows[0]["PUR_FZNUM"].ToString());
                        l++;  
                    }
                }
                if (l == 1)
                {
                    sql = "update TBPC_PURCHASEPLAN set PUR_ZYDY='',PUR_CSTATE='0',Pue_Closetype=NULL  where PUR_PTCODE= '" + number[0] + "' and PUR_CSTATE='1'";
                    DBCallCommon.ExeSqlText(sql);
                    sql = "select PUR_CSTATE from TBPC_PURCHASEPLAN where PUR_PCODE='" + orderno.Text + "' and PUR_CSTATE='0' and PUR_STATE<'3'";
                    System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        string sql2 = "update TBPC_PCHSPLANRVW set PR_STATE='0' where PR_SHEETNO='" + orderno.Text + "'";
                        DBCallCommon.ExeSqlText(sql2);
                    }
                    Response.Write("<script>alert('反关闭成功！');location.href='PC_TBPC_Purchaseplan_startcontent.aspx?mp_id=" + orderno.Text + "&shape=" + tb_shape.Text + "';</script>");

                }
                else
                {
                    for (int j = 0; j < number.Count - 1; j++)
                    {
                        ptcode = number[j];
                        string ptcode1 = number[j + 1];
                        if (ptcode.Contains("&"))
                        {
                            ptcode = ptcode.Substring(0, ptcode.IndexOf("&")).ToString();
                        }
                        if (ptcode1.Contains("&"))
                        {
                            ptcode1 = ptcode1.Substring(0, ptcode1.IndexOf("&")).ToString();
                        }
                        if (ptcode.ToString() == ptcode1.ToString())
                        {
                        }
                        else
                        {
                            m++;
                        }
                    }
                    if (m > 0)
                    {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('两条数据不是同种物料，不能合并！');", true);
                    }
                    else
                    {
                        for (int k = 0; k < number.Count; k++)
                        {
                            string sql3 = "";
                            if (k == 0)
                            {
                                sql3 = "update TBPC_PURCHASEPLAN set PUR_NUM=" + num + ",PUR_FZNUM=" + fznum + ",PUR_RPNUM=" + num + ",PUR_RPFZNUM=" + fznum + ",PUR_ZYDY='',PUR_CSTATE='0',Pue_Closetype=NULL  where PUR_PTCODE= '" + number[k] + "' and PUR_CSTATE='1'";
                                DBCallCommon.ExeSqlText(sql3);
                            }
                            else
                            {
                                sql3 = "delete TBPC_PURCHASEPLAN where PUR_PTCODE= '" + number[k] + "' and PUR_CSTATE='1'";
                                DBCallCommon.ExeSqlText(sql3);
                            }

                        }
                        sql = "select PUR_CSTATE from TBPC_PURCHASEPLAN where PUR_PCODE='" + orderno.Text + "' and PUR_CSTATE='0' and PUR_STATE<'3'";
                        System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                        if (dt1.Rows.Count > 0)
                        {
                            string sql2 = "update TBPC_PCHSPLANRVW set PR_STATE='0' where PR_SHEETNO='" + orderno.Text + "'";
                            DBCallCommon.ExeSqlText(sql2);
                        }
                        Response.Write("<script>alert('合并反关闭成功！');location.href='PC_TBPC_Purchaseplan_startcontent.aspx?mp_id=" + orderno.Text + "&shape=" + tb_shape.Text + "';</script>");

                    }
                }                   
            }
            else if(temp==1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有反关闭的权限！');", true);
            }
            else if (temp == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已经下推比价单或者订单，无法进行合并反关闭！');", true);
            } 
        }
        protected int isselected2()
        {
            int count = 0;
            int temp = 0;
            int k = 0;
            int j = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                string ptcode = ((System.Web.UI.WebControls.Label)gr.FindControl("ptcode")).Text;
                if (ptcode.Contains("&"))
                {
                    ptcode = ptcode.Substring(0, ptcode.IndexOf("&")).ToString();
                }
                if (cbk.Checked)
                {
                    count++;
                    string sql = "select * from TBPC_IQRCMPPRICE where PIC_PTCODE='" + ptcode + "'";//已经下推比价单无法进行反关闭
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    string sql1 = "select * from TBPC_PURORDERDETAIL where PO_PTCODE='" + ptcode + "'";//已经下推订单无法进行反关闭
                    System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                    if (dt.Rows.Count > 0||dt1.Rows.Count>0)
                    {
                        j++;
                    }
                }
                if (Session["UserDeptID"].ToString() != "06")
                {
                    k++;
                }
            }
            if (count == 0)//没选择数据
            {
                temp = 1;
            }
            else if(k>0)
            {
                //temp = 2;
            }
            else if (j > 0)
            {
                temp = 3;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }

        protected void selectall_CheckedChanged(object sender, EventArgs e)
        {
            if (selectall.Checked)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                    if (cbk.Enabled != false)//存在行
                    {
                        cbk.Checked = true;
                    }
                }
                
            }
            else
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                    if (cbk != null)//存在行
                    {
                        cbk.Checked = false;
                    }
                }
            }
        }

        protected void btn_LX_click(object sender, EventArgs e)//连选
        {
            int i = 0;
            int j = 0;
            int start = 0;
            int finish = 0;
            int k = 0;
            for (int a = 0; a < GridView1.Rows.Count; a++)
            {
                j++;
                GridViewRow gr = GridView1.Rows[a];
                System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                if (cbk.Checked)
                {
                    i++;
                    if (start == 0)
                    {
                        start = j;
                    }
                    else
                    {
                        finish = j;
                    }
                }
            }
            if (i == 2)
            {
                for (int a = 0; a < GridView1.Rows.Count; a++)
                {
                    k++;
                    GridViewRow gr = GridView1.Rows[a];
                    System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                    if (k >= start && k <= finish)
                    {
                        if (cbk.Enabled != false)
                        {
                            cbk.Checked = true;
                        } 
                    }
                    if (k > finish)
                    {
                        cbk.Checked = false;
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择连续的区间！');", true);
            }
        }

        protected void btn_QX_click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                if (cbk != null)//存在行
                {
                    cbk.Checked = false;
                }
            }
        }

        protected void btn_DTFclose_Click(object sender, EventArgs e)
        {
            int temp = isselected3();
            if (temp == 0)//是否选择数据
            {
                string ptcode = "";
                string sql = "";
                double num = 0;
                double fznum = 0;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                    if (cbk.Checked)
                    {
                        ptcode = ((System.Web.UI.WebControls.Label)gr.FindControl("ptcode")).Text;
                        sql = "update TBPC_PURCHASEPLAN set PUR_CSTATE='0',PUR_ZYDY='',Pue_Closetype=NULL  where PUR_PTCODE='" + ptcode + "' and (PUR_CSTATE='1' or PUR_CSTATE='2')";
                        DBCallCommon.ExeSqlText(sql);                        
                    }
                }
                sql = "select PUR_CSTATE from TBPC_PURCHASEPLAN where PUR_PCODE='" + orderno.Text + "' and PUR_CSTATE='0' and PUR_STATE<'3'";
                System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt1.Rows.Count > 0)
                {
                    string sql2 = "update TBPC_PCHSPLANRVW set PR_STATE='0' where PR_SHEETNO='" + orderno.Text + "'";
                    DBCallCommon.ExeSqlText(sql2);
                }
                Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_startcontent.aspx?mp_id=" + orderno.Text + "&shape=" + tb_shape.Text + "");
                //Response.Write("<script>javascript:window.close();</script>");
            }
            else if (temp == 1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
            }
            else if (temp == 2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有反关闭的权限！');", true);
            }
            else if (temp == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('现在拆分之后的计划跟踪号上加了&1，这条记录为以前拆分，不能进行单独反关闭！');", true);
            }
        }

        protected int isselected3()
        {
            int count = 0;
            int temp = 0;
            int j = 0;
            int k = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                if (cbk.Checked)
                {
                    count++;
                    string ptcode = ((System.Web.UI.WebControls.Label)gr.FindControl("ptcode")).Text;
                    if (ptcode.Contains("&"))
                    {

                    }
                    else
                    {
                        j++;
                    }
                }
                if (Session["UserDeptID"].ToString() != "06")
                {
                    k++;
                }
            }
            if (count == 0)//没选择数据
            {
                temp = 1;
            }
            else if (k > 0)
            {
                //temp = 2;
            }
            else if (j > 0)
            {
                temp = 3;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
    }
}
