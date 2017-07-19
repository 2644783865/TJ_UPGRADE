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
using System.Collections.Generic;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_PurOrder_close : BasicPage
    {
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
        public string gloabcode
        {
            get
            {
                object str = ViewState["gloabcode"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabcode"] = value;
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
                    gloabsheetno = Request.QueryString["orderno"].ToString();
                }
                else
                {
                    gloabsheetno = "";
                }
                InitInfo();
                gridatabind();
                //CheckUser(ControlFinder);    2013年5月20日11:09:24   因上层已控制，本页面不需要在添加权限控制  Meng
            }
        }

        

        private void InitInfo()
        {
            string orderno = gloabsheetno;
            string sqltext = "";
            sqltext = "SELECT orderno AS Code,supplierid,suppliernm AS Supplier,zdtime AS Date,abstract AS Abstract," +
                "versionno AS VersionNo,changdate AS ChangeDate,changmanid,changnm AS ChangeMan,changreason AS ChangeReason," +
                "zdrid AS Documentid,zdrnm AS Document,ywyid AS ClerkID,ywynm AS Clerk,depid AS DepID,depnm AS Dep, " +
                "zgid AS ManagerID,zgnm AS Manager,totalnote AS note,totalstate as state,totalcstate as tcstate  " +
                "FROM View_TBPC_PURORDERTOTAL WHERE orderno='" + orderno + "' ";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt1.Rows.Count > 0)
            {
                LabelCode.Text = dt1.Rows[0]["Code"].ToString();
                LabelDate.Text = dt1.Rows[0]["Date"].ToString();
                LabelSupplier.Text = dt1.Rows[0]["Supplier"].ToString();
                LabelAbstract.Text = dt1.Rows[0]["Abstract"].ToString();
                LabelVersionNo.Text = dt1.Rows[0]["VersionNo"].ToString();
                LabelChangeDate.Text = dt1.Rows[0]["ChangeDate"].ToString();
                LabelChangeMan.Text = dt1.Rows[0]["ChangeMan"].ToString();
                LabelChangeReason.Text = dt1.Rows[0]["ChangeReason"].ToString();
                LabelManager.Text = dt1.Rows[0]["Manager"].ToString();
                LabelDep.Text = dt1.Rows[0]["Dep"].ToString();
                LabelClerk.Text = dt1.Rows[0]["Clerk"].ToString();
                LabelDocument.Text = dt1.Rows[0]["Document"].ToString();
                LabelDocumentid.Text = dt1.Rows[0]["Documentid"].ToString();
                tb_state.Text = dt1.Rows[0]["state"].ToString();
                tb_cstate.Text = dt1.Rows[0]["tcstate"].ToString();
                if (Convert.ToInt32(tb_state.Text) >= 1)//审核通过
                {
                    ImageVerify.Visible = true;
                }
                else
                {
                    ImageVerify.Visible = false;
                }
            }
        }
        private void gridatabind()
        {
            string sqltext = "";
            string orderno = gloabsheetno;
            sqltext = "SELECT orderno as Code,picno as irqsheet,ptcode as PlanCode ," +
                     "marid as MaterialCode,marnm as MaterialName,margg as MaterialStandard," +
                     "marcz as MaterialTexture,margb as MaterialGb,marunit as Unit,num as Number," +
                     "zxnum as zxnum," +
                     "fznum,zxfznum as zxFznum,marfzunit as Fzunit,cgtimerq as Cgtimerq," +
                     "recgdnum as arrivedNumber,recdate as Rptime,price as UnitPrice,ctprice as CTUP," +
                     "amount as Amount,taxrate as TaxRate,ctamount as PricePlusTax," +
                     "detailnote as Comment,planmode as PlanMode,ptcode as ptcode,length as Length," +
                     "width as Width,keycoms as keycoms,detailstate as PO_STATE,detailcstate as pocstate,engid as engid  " +
                     "FROM View_TBPC_PURORDERDETAIL_PLAN WHERE  orderno='" + orderno + "' ";
            if (rad_guanbi.Checked)
            {
                sqltext = sqltext + " and detailcstate='1'";
            }
            if (rad_zc.Checked)
            {
                sqltext = sqltext + " and detailcstate='0'";
            }
            sqltext = sqltext + " order by marnm,margg,ptcode asc";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        protected void btn_confirm_Click(object sender, EventArgs e)
        {   
            string userid = Session["UserID"].ToString();
            if (userid == LabelDocumentid.Text)
            {
                string sqltext = "";
                List<string> sqltextlist = new List<string>();
                string orderno = gloabsheetno;
                string ptcode = "";
                string note = "";
                int temp = confsavemess();
                if (temp == 0 || temp == 1)
                {
                        for (int i = 0; i < GridView1.Rows.Count; i++)
                        {
                            GridViewRow gr = GridView1.Rows[i];
                            CheckBox cbk = (CheckBox)gr.FindControl("CheckBox1");
                            if (cbk.Checked)
                            {
                                ptcode = ((Label)gr.FindControl("PlanCode")).Text;
                                note = ((TextBox)gr.FindControl("tb_value")).Text;
                                if (note.ToString().Trim() == "")
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('关闭原因不能为空！');", true);
                                }
                                else
                                {
                                    sqltext = "update TBPC_PURORDERDETAIL set PO_CSTATE='1',PO_NOTE='" + note + "' WHERE PO_CODE='" + orderno + "' and PO_PTCODE='" + ptcode + "'";
                                    DBCallCommon.ExeSqlText(sqltext);
                                }
                            }
                        }
                        sqltext = "SELECT PO_ID FROM TBPC_PURORDERDETAIL WHERE PO_CODE='" + orderno + "' and PO_CSTATE='0'";//是否还存在未关闭的，如果都关闭则整单关闭
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt.Rows.Count == 0)
                        {
                            sqltext = "update TBPC_PURORDERTOTAL set PO_CSTATE='1',PO_ZJE='0'  WHERE PO_CODE='" + orderno + "'";//单号关闭
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                        else
                        {
                            double zje = 0;
                            sqltext = "select sum(ctamount) as zje from View_TBPC_PURORDERDETAIL_PLAN where orderno='" + LabelCode.Text + "' and  detailcstate!='1'";
                            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
                            if (dt1.Rows.Count > 0)
                            {
                                zje = Convert.ToDouble(dt1.Rows[0]["zje"].ToString());
                            }
                            sqltext = "UPDATE TBPC_PURORDERTOTAL SET PO_ZJE=" + zje + "  WHERE PO_CODE='" + LabelCode.Text.ToString() + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                        }

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');window.close();", true);
                
                }
                else if (temp == 2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您未选择数据,本次操作无效！');", true);
                }
                else if (temp == 3)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择了已入库的数据,本次操作无效！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行操作！');", true);
            }
        }
        protected void btn_concel_Click(object sender, EventArgs e)
        {
            string userid = Session["UserID"].ToString();
            if (userid == LabelDocumentid.Text)
            {
                string sqltext = "";
                List<string> sqltextlist = new List<string>();
                string orderno = gloabsheetno;
                string ptcode = "";
                string note = "";
                int temp = concsavemess();
                if (temp == 0 || temp == 1)
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        GridViewRow gr = GridView1.Rows[i];
                        CheckBox cbk = (CheckBox)gr.FindControl("CheckBox1");
                        if (cbk.Checked)
                        {
                            ptcode = ((Label)gr.FindControl("PlanCode")).Text;
                            note = ((TextBox)gr.FindControl("tb_value")).Text;
                            if (note.ToString().Trim() == "")
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('反关闭原因不能为空！');", true);
                            }
                            else
                            {
                                sqltext = "update TBPC_PURORDERDETAIL set PO_CSTATE='0',PO_NOTE='" + note + "' WHERE PO_CODE='" + orderno + "' " +
                                          "and PO_PTCODE='" + ptcode + "'";
                                DBCallCommon.ExeSqlText(sqltext);
                            }
                        }
                    }
                    sqltext = "SELECT PO_ID FROM TBPC_PURORDERDETAIL WHERE PO_CODE='" + orderno + "' and PO_CSTATE='0'";//是否还存在未关闭的，如果都存则整单未关闭
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dt.Rows.Count > 0)
                    {
                        sqltext = "update TBPC_PURORDERTOTAL set PO_CSTATE='0'  WHERE PO_CODE='" + orderno + "'";//单号反关闭
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');window.close();", true);
                }
                else if (temp == 2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您未选择数据，本次操作无效！');", true);
                }
                else if (temp == 3)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您选择了没有关闭的数据,本次操作无效！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行操作！');", true);
            }
        }
        protected void rad_stall_CheckedChanged(object sender, EventArgs e)
        {
            gridatabind();
        }
        protected void rad_guanbi_CheckedChanged(object sender, EventArgs e)
        {
            gridatabind();
        }
        protected void rad_zc_CheckedChanged(object sender, EventArgs e)
        {
            gridatabind();
        }
        protected void btn_close_Click(object sender, EventArgs e)//到货关闭
        {   
            string userid = Session["UserID"].ToString();
            if (userid == LabelDocumentid.Text)
            {
                string sqltext = "";
                List<string> sqltextlist = new List<string>();
                string orderno = gloabsheetno;
                string ptcode = "";
                string note = "";
                int temp = confsavemess();
                if (temp == 0 || temp == 1 || temp == 3)
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        GridViewRow gr = GridView1.Rows[i];
                        CheckBox cbk = (CheckBox)gr.FindControl("CheckBox1");
                        if (cbk.Checked)
                        {
                            ptcode = ((Label)gr.FindControl("PlanCode")).Text;
                            note = ((TextBox)gr.FindControl("tb_value")).Text;
                            sqltext = "update TBPC_PURORDERDETAIL set PO_STATE='2'  WHERE PO_CODE='" + orderno + "' and  PO_PTCODE='" + ptcode + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');window.close();", true);
                }
                else if (temp == 2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您未选择数据,本次操作无效！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行操作！');", true);
            }
        }
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close();</script>");
        }
        private int confsavemess()
        {
            int j = 0;
            int k = 0;
            int temp = 0;
            string userid = Session["UserID"].ToString();
            if (userid == LabelDocumentid.Text)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    CheckBox cbk = (CheckBox)gr.FindControl("CheckBox1");
                    //string lbstate = ((Label)gr.FindControl("state")).Text;
                    Label arrynum = (Label)gr.FindControl("lb_arry");
                    double num = Convert.ToDouble(arrynum.Text);
                    if (cbk.Checked)
                    {
                        j++;
                        if (num > 0)//存在已经入库的数据，不能关闭
                        {
                            k++;
                            break;
                        }

                    }
                }
                if (j == 0)
                {
                    temp = 2;//未选择数据
                }
                if (k>0)
                {
                    temp = 3;//选择了已入库的数据
                }
            }
            else
            {
                temp = 1; //不是制单人
            }
            return temp;
        }
        private int concsavemess()
        {
            int j = 0;
            int k = 0;
            int temp = 0;
            string userid = Session["UserID"].ToString();
            if (userid == LabelDocumentid.Text)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gr = GridView1.Rows[i];
                    CheckBox cbk = (CheckBox)gr.FindControl("CheckBox1");
                    Label lbcstate = (Label)gr.FindControl("cstate");
                    if (cbk.Checked)
                    {
                        j++;
                        if (lbcstate.Text != "1")//存在没有关闭的数据，不能反关闭
                        {
                            k++;
                            break;
                        }

                    }
                }
                if (j == 0)
                {
                    temp = 2;//未选择数据
                }
                if (k > 0)
                {
                    temp = 3;//存在没有关闭的数据
                }
            }
            else
            {
                temp = 1;//不是制单人
            }
            return temp;
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
                string cstate = ((System.Web.UI.WebControls.Label)e.Row.FindControl("cstate")).Text;
                if (Convert.ToDouble(cstate) == 1)
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                }
                string ptcode = ((Label)e.Row.FindControl("PlanCode")).Text;
                CheckBox cbk = (CheckBox)e.Row.FindControl("CheckBox1");
                string sql = "select * from TBPC_MARSTOUSEALL where PUR_PTCODE='" + ptcode + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    if (Convert.ToDouble(cstate) == 1)
                    {
                        cbk.Enabled = false;
                        e.Row.Cells[0].BackColor = System.Drawing.Color.Yellow;
                    }
                }
                sql = "select * from TBPC_MARREPLACEALL where MP_PTCODE='" + ptcode + "' and  SUBSTRING(MP_CODE, 5, 1) = '0'";
                System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt1.Rows.Count > 0)
                {
                    if (Convert.ToDouble(cstate) == 1)
                    {
                        cbk.Enabled = false;
                        e.Row.Cells[0].BackColor = System.Drawing.Color.Green;
                    }
                    
                }
            }
        }
        public string get_order_state(string i, string j)
        {
            string statestr = "";
            if (Convert.ToInt32(i)==0)
            {
                statestr += "是";
            }
            else
            {

                statestr += "否";

            }
            statestr += "/";
            if (Convert.ToInt32(i) == 2)
            {
                statestr += "是";
            }
            else
            {

                statestr += "否";
            }
            statestr += "/";
            if (Convert.ToInt32(j) == 1)
            {
                statestr += "是";
            }
            else
            {

                statestr += "否";
            }
            return statestr;
        }

        protected void btn_ZY_Click(object sender, EventArgs e)
        {   
          string userid = Session["UserID"].ToString();
          if (userid == LabelDocumentid.Text)
          {
              int temp = isselected();
              if (temp == 0)//是否选择数据
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
                  string beizhu = "";
                  string pjid = "";
                  string engid = "";
                  string zypihao = "";
                  string sql = "select PR_PJID,PR_ENGID,PUR_MASHAPE,PR_NOTE from TBPC_PCHSPLANRVW where PR_SHEETNO='" + gloabcode + "'";
                  System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                  if (dt.Rows.Count > 0)
                  {
                      pjid = dt.Rows[0]["PR_PJID"].ToString();
                      engid = dt.Rows[0]["PR_ENGID"].ToString();
                      shape = dt.Rows[0]["PUR_MASHAPE"].ToString();
                      note = dt.Rows[0]["PR_NOTE"].ToString();
                  }

                  //查询批号是否存在
                  sql = "select pr_pcode from TBPC_MARSTOUSETOTAL where pr_pcode like '" + gloabcode + "%' order by pr_pcode ASC";
                  dt = DBCallCommon.GetDTUsingSqlText(sql);
                  if (dt.Rows.Count > 0)
                  {
                      int number = dt.Rows.Count;
                      if (number == 1)
                      {
                          zypihao = gloabcode + "#1";
                      }
                      else
                      {
                          zypihao = gloabcode + "#" + Convert.ToString(Convert.ToDouble(dt.Rows[number - 1][0].ToString().Split('#')[1]) + 1);
                      }
                  }
                  else
                  {
                      zypihao = gloabcode;
                  }
                  globlempcode = zypihao;
                  for (int i = 0; i < GridView1.Rows.Count; i++)
                  {
                      GridViewRow gr = GridView1.Rows[i];
                      System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                      if (cbk.Checked)
                      {
                          ptcode = ptcode = ((System.Web.UI.WebControls.Label)gr.FindControl("PlanCode")).Text;
                          marid = ((System.Web.UI.WebControls.Label)gr.FindControl("MaterialCode")).Text;
                          length = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("Length")).Text);
                          width = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("Width")).Text);
                          num = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("Number")).Text);
                          fznum = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("fznum")).Text);
                          beizhu = ((System.Web.UI.WebControls.TextBox)gr.FindControl("tb_value")).Text;
                          sqltext = "insert into TBPC_MARSTOUSEALL(PUR_PCODE,PUR_PJID,PUR_ENGID,PUR_PTCODE, PUR_MARID, PUR_LENGTH,PUR_WIDTH,PUR_NUM, PUR_FZNUM,PUR_USTNUM,PUR_USTFZNUM,PUR_NOTE) values ('" + zypihao + "','" + pjid + "','" + engid + "','" + ptcode + "','" + marid + "'," + length + "," + width + "," + num + "," + fznum + ",'" + num + "','" + fznum + "','" + beizhu + "')";
                          DBCallCommon.ExeSqlText(sqltext);
                      }
                  }
                  string zdid = Session["UserID"].ToString();
                  string zdtm = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                  sqltext = "insert into TBPC_MARSTOUSETOTAL (PR_PCODE,PR_PJID,PR_ENGID,PUR_MASHAPE,PR_NOTE) values ('" + zypihao + "','" + pjid + "','" + engid + "','" + shape + "','" + note + "')";
                  DBCallCommon.ExeSqlText(sqltext);
                  sqltext = "update TBPC_MARSTOUSETOTAL set PR_REVIEWA='" + zdid + "', " +
                                "PR_REVIEWATIME='" + zdtm + "',PR_STATE='1' " +
                                "where  PR_PCODE='" + zypihao + "'";
                  DBCallCommon.ExeSqlText(sqltext);
                  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "openclosemodewin1();", true);

              }
              else if (temp == 1)
              {
                  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
              }
              else if (temp == 2)
              {
                  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('你选择了已经提交库存占用审核的数据！');", true);
              }
              else if (temp == 3)
              {
                  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('你选择了不是同一批计划的物料！');", true);
              }
              else if (temp == 4)
              {
                  ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择了未关闭的数据，需要先关闭，再占用！');", true);
              }
          }
          else
          {
              ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行操作！');", true);
          }
        }
        //判断是否选择数据
        protected int isselected()
        {
            gloabcode = "";
            int count = 0;
            int j = 0;
            int temp = 0;
            int k = 0;
            int a = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                if (cbk.Checked)
                {
                    count++;
                    string ptcode = ((System.Web.UI.WebControls.Label)gr.FindControl("PlanCode")).Text;
                    string sql = "select * from TBPC_MARSTOUSEALL where PUR_PTCODE='" + ptcode + "'";
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        j++;
                    }
                    sql = "select PUR_PCODE from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "'";
                    System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        if (gloabcode != dt1.Rows[0]["PUR_PCODE"].ToString())
                        {
                            k++;
                        }
                        gloabcode = dt1.Rows[0]["PUR_PCODE"].ToString();
                    }
                    string cstate = ((System.Web.UI.WebControls.Label)gr.FindControl("cstate")).Text;
                    if (cstate == "0")
                    {
                        a++;
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
            else if (k > 1)
            {
                temp = 3;
            }
            else if (a > 0)
            {
                temp = 4;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }

        protected void btn_XSDY_Click(object sender, EventArgs e)
        {
            string userid = Session["UserID"].ToString();
            if (userid == LabelDocumentid.Text)
            {
                int temp = isselected1();
                if (temp == 0)//是否选择数据
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

                    string sql = "select PR_PJID,PR_ENGID,PR_SQREID,PR_FZREID from TBPC_PCHSPLANRVW where PR_SHEETNO='" + gloabcode + "'";
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
                              "VALUES('" + mpcode + "','" + gloabcode + "','" + pjid + "','" + engid + "','" + fillid + "'," +
                              "'" + filltime + "','" + reviewaid + "','" + chargeid + "')";
                    DBCallCommon.ExeSqlText(sqltext);
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        GridViewRow gr = GridView1.Rows[i];
                        System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                        if (cbk.Checked)
                        {
                            ptcode = ((System.Web.UI.WebControls.Label)gr.FindControl("PlanCode")).Text;
                            marid = ((System.Web.UI.WebControls.Label)gr.FindControl("MaterialCode")).Text;
                            length = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("Length")).Text);
                            width = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("Width")).Text);
                            num = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("Number")).Text);
                            fznum = Convert.ToDouble(((System.Web.UI.WebControls.Label)gr.FindControl("fznum")).Text);
                            sqltext = "INSERT INTO TBPC_MARREPLACEALL(MP_CODE,MP_PTCODE,MP_MARID," +
                                              "MP_NUM,MP_FZNUM,MP_LENGTH,MP_WIDTH) " +
                                              "VALUES('" + mpcode + "','" + ptcode + "','" + marid + "'," +
                                               +num + "," + fznum + "," + length + "," + width + ")";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "openclosemodewin();", true);
                }
                else if (temp == 1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
                }
                else if (temp == 2)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('你选择了已经提交相似代用审核的数据！');", true);
                }
                else if (temp == 3)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('你选择了不是同一批计划的物料！');", true);
                }
                else if (temp == 4)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('选择了未关闭的数据，需要先关闭，再相似代用！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您无权进行操作！');", true);
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

        protected int isselected1()
        {
            int count = 0;
            int j = 0;
            int temp = 0;
            int k = 0;
            int a=0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                System.Web.UI.WebControls.CheckBox cbk = (System.Web.UI.WebControls.CheckBox)gr.FindControl("CheckBox1");
                if (cbk.Checked)
                {
                    count++;
                    string ptcode = ((System.Web.UI.WebControls.Label)gr.FindControl("PlanCode")).Text;
                    string sql = "select * from TBPC_MARREPLACEALL where MP_PTCODE='" + ptcode + "' and  SUBSTRING(MP_CODE, 5, 1) = '0'";
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        j++;
                    }
                    sql = "select PUR_PCODE from TBPC_PURCHASEPLAN where PUR_PTCODE='" + ptcode + "'";
                    System.Data.DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        if (gloabcode != dt1.Rows[0]["PUR_PCODE"].ToString())
                        {
                            k++;
                        }
                        gloabcode = dt1.Rows[0]["PUR_PCODE"].ToString();
                    }
                    string cstate = ((System.Web.UI.WebControls.Label)gr.FindControl("cstate")).Text;
                    if (cstate == "0")
                    {
                        a++;
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
            else if (k > 1)
            {
                temp = 3;
            }
            else if (a > 0)
            {
                temp = 4;
            }
            else
            {
                temp = 0;
            }
            return temp;
        }
    }
}
