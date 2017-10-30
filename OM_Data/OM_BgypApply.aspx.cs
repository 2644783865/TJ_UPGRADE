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
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_BgypApply : System.Web.UI.Page
    {
        int count = 0;

        string flag = string.Empty;

        string incode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            flag = Request.QueryString["action"].ToString();
            if (!IsPostBack)
            {
                //((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                if (flag == "add")
                {
                    InitInfo();
                    InitGridview();
                }
                if (flag == "view")
                {

                    btnsave.Visible = false;
                    TextBoxDate.Enabled = false;
                    txtshr.Enabled = false;
                    hlSelect0.Visible = false;
                    btnsave.Visible = false;
                    
                    //增加行不可见
                    addrow.Visible = false;
                    txtNum.Visible = false;
                    btnadd.Visible = false;
                    btndelete.Visible = false;

                    string Id = Request.QueryString["id"].ToString();
                    string sql = "select * from View_TBOM_BGYPAPPLY where CODE='" + Id + "'";
                    //string sql = "select * from View_TBOM_BGYPAPPLY where CODE='"+ Id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        LabelCode.Text = Id.ToString();
                        TextBoxDate.Text = dt.Rows[0]["DATE"].ToString();
                        LabelDoc.Text = dt.Rows[0]["APPLY"].ToString();
                        txtshr.Text = dt.Rows[0]["REVIEW"].ToString();
                    }
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        ((CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1")).Visible = false;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLBM")).Disabled = true;
                        ((TextBox)this.GridView1.Rows[i].FindControl("WLNAME")).Enabled = false;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLMODEL")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLUNIT")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNUM")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLPRICE")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLJE")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNOTE")).Disabled = true;
                        ((DropDownList)this.GridView1.Rows[i].FindControl("ddlIsCal")).Enabled = false;
                        ((DropDownList)this.GridView1.Rows[i].FindControl("ddlIsChange")).Enabled = false;
                    }
                }
                if (flag == "verify")
                {
                    TextBoxDate.Enabled = false;
                    txtshr.Enabled = false;
                    hlSelect0.Visible = false;
                    drop_view.Visible = true;

                    //增加行不可见
                    addrow.Visible = false;
                    txtNum.Visible = false;
                    btnadd.Visible = false;
                    btndelete.Visible = false;

                    string Id = Request.QueryString["id"].ToString();
                    string sql = "select * from View_TBOM_BGYPAPPLY where CODE='" + Id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        LabelCode.Text = Id.ToString();
                        TextBoxDate.Text = dt.Rows[0]["DATE"].ToString();
                        LabelDoc.Text = dt.Rows[0]["APPLY"].ToString();
                        txtshr.Text = dt.Rows[0]["REVIEW"].ToString();
                    }
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        ((CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1")).Visible = false;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLBM")).Disabled = true;
                        ((TextBox)this.GridView1.Rows[i].FindControl("WLNAME")).Enabled = false;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLMODEL")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLUNIT")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNUM")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLPRICE")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLJE")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNOTE")).Disabled = true;
                        ((DropDownList)this.GridView1.Rows[i].FindControl("ddlIsCal")).Enabled = false;
                        ((DropDownList)this.GridView1.Rows[i].FindControl("ddlIsChange")).Enabled = false;
                    }
                }
                if (flag == "mod")
                {
                    TextBoxDate.Enabled = false;
                    txtshr.Enabled = false;
                    hlSelect0.Visible = false;
                    string Id = Request.QueryString["id"].ToString();
                    string sql = "select * from View_TBOM_BGYPAPPLY where CODE='" + Id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        LabelCode.Text = Id.ToString();
                        TextBoxDate.Text = dt.Rows[0]["DATE"].ToString();
                        LabelDoc.Text = dt.Rows[0]["APPLY"].ToString();
                        LabelDocCode.Text = dt.Rows[0]["APPLYID"].ToString();
                        txtshr.Text = dt.Rows[0]["REVIEW"].ToString();
                        shrid.Value = dt.Rows[0]["REVIEWID"].ToString();
                    }
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                if (flag == "addsls")
                {
                    TextBoxDate.Enabled = false;
                    txtshr.Enabled = false;
                    hlSelect0.Visible = false;
                    btnadd.Visible = false;
                    btndelete.Visible = false;
                    drop_view.Visible = true;
                    drop_view.Enabled = false;

                    string Id = Request.QueryString["id"].ToString();
                    string sql = "select * from View_TBOM_BGYPAPPLY where CODE='" + Id + "' and (WLSLS is null or WLSLS='')";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        LabelCode.Text = Id.ToString();
                        TextBoxDate.Text = dt.Rows[0]["DATE"].ToString();
                        LabelDoc.Text = dt.Rows[0]["APPLY"].ToString();
                        txtshr.Text = dt.Rows[0]["REVIEW"].ToString();
                    }
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        ((CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1")).Visible = false;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLBM")).Disabled = true;
                        ((TextBox)this.GridView1.Rows[i].FindControl("WLNAME")).Enabled = false;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLMODEL")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLUNIT")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNUM")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLPRICE")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLJE")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNOTE")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLSLS")).Disabled = false;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("GET_MONEY")).Disabled = false;
                        ((DropDownList)this.GridView1.Rows[i].FindControl("ddlIsCal")).Enabled = true;
                        ((DropDownList)this.GridView1.Rows[i].FindControl("ddlIsChange")).Enabled = true;
                    }
                }
            }
        }
        protected void close(object sender, EventArgs e)
        {
            Response.Redirect("OM_BgypApplyMain.aspx");
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


        protected string generateTempCode()
        {

            List<string> lt = new List<string>();

            string sql = "SELECT Code FROM TBOM_BGYP_INCODE WHERE len(Code)=11 and left(Code,2)='SQ'";

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
                return "SQ000000001";
            }
            else
            {
                string tempstr = wsidlist.Max<string>();
                int tempnum = Convert.ToInt32((tempstr.Substring(2, 9)));
                tempnum++;
                tempstr = "SQ" + tempnum.ToString().PadLeft(9, '0');
                return tempstr;
            }
        }


        /// <summary>
        /// 初始化Gridview
        /// </summary>
        private void InitGridview()
        {
            DataTable dt = this.GetDataFromGrid(true);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btninsert_Click(object sender, EventArgs e)
        {

            DataTable dt = this.GetDataFromGrid(false);
            int a = CommonFun.ComTryInt(txtNum.Text.Trim());
            for (int i = 0; i < a; i++)
            {
                DataRow newRow = dt.NewRow();

                newRow[5] = 0;
                newRow[6] = 0;
                newRow[4] = 0;
                newRow["IsCalculate"] = 0;
                newRow["IsChange"] = 0;
                dt.Rows.Add(newRow);
            }




            this.GridView1.DataSource = dt;

            this.GridView1.DataBind();
        }


        /// <summary>
        /// 初始化表格
        /// </summary>
        /// <param name="isInit">是否是初始化</param>
        /// <returns></returns>
        protected DataTable GetDataFromGrid(bool isInit)
        {
            DataTable dt = new DataTable();

            #region
            dt.Columns.Add("WLCODE");
            dt.Columns.Add("WLNAME");

            dt.Columns.Add("WLMODEL");

            dt.Columns.Add("WLUNIT");

            dt.Columns.Add("WLNUM");

            dt.Columns.Add("WLPRICE");

            dt.Columns.Add("WLJE");//

            dt.Columns.Add("WLSLS");
            dt.Columns.Add("WLNOTE");

            dt.Columns.Add("WLBM");
            dt.Columns.Add("GET_MONEY");
            dt.Columns.Add("num");
            dt.Columns.Add("unPrice");
            dt.Columns.Add("StoreId");
            dt.Columns.Add("IsCalculate");
            dt.Columns.Add("IsChange");
            #endregion



            if (!isInit)
            {

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];

                    DataRow newRow = dt.NewRow();

                    newRow["WLCODE"] = ((HtmlInputHidden)gRow.FindControl("WLCODE")).Value;
                    newRow["WLBM"] = ((HtmlInputText)gRow.FindControl("WLBM")).Value;
                    newRow["WLNAME"] = ((TextBox)gRow.FindControl("WLNAME")).Text;
                    newRow["WLMODEL"] = ((HtmlInputText)gRow.FindControl("WLMODEL")).Value;
                    newRow["WLUNIT"] = ((HtmlInputText)gRow.FindControl("WLUNIT")).Value;
                    newRow["WLNUM"] = ((HtmlInputText)gRow.FindControl("WLNUM")).Value;
                    newRow["WLPRICE"] = ((HtmlInputText)gRow.FindControl("WLPRICE")).Value;
                    newRow["WLJE"] = ((HtmlInputText)gRow.FindControl("WLJE")).Value;
                    newRow["WLSLS"] = ((HtmlInputText)gRow.FindControl("WLSLS")).Value;
                    newRow["GET_MONEY"] = ((HtmlInputText)gRow.FindControl("GET_MONEY")).Value;
                    newRow["WLNOTE"] = ((HtmlInputText)gRow.FindControl("WLNOTE")).Value;
                    newRow["num"] = ((HtmlInputText)gRow.FindControl("num")).Value;
                    newRow["unPrice"] = ((HtmlInputText)gRow.FindControl("unPrice")).Value;
                    newRow["StoreId"] = ((HtmlInputHidden)gRow.FindControl("hidStoreId")).Value;
                    newRow["IsCalculate"] = ((DropDownList)gRow.FindControl("ddlIsCal")).SelectedValue;
                    newRow["IsChange"] = ((DropDownList)gRow.FindControl("ddlIsChange")).SelectedValue;
                    dt.Rows.Add(newRow);
                }
            }


            if (isInit)
            {
                for (int i = GridView1.Rows.Count; i < 10; i++)
                {
                    DataRow newRow = dt.NewRow();

                    newRow[5] = 0;
                    newRow[6] = 0;
                    newRow[4] = 0;
                    newRow["IsCalculate"] = 0;
                    newRow["IsChange"] = 0;
                    dt.Rows.Add(newRow);
                }
            }

            dt.AcceptChanges();

            return dt;
        }


        /********************删除行******************************/

        protected void btndelete_Click(object sender, EventArgs e)
        {
            int count = 0;

            DataTable dt = this.GetDataFromGrid(false);

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];

                CheckBox chk = (CheckBox)gr.FindControl("CheckBox1");

                if (chk.Checked)
                {
                    dt.Rows.RemoveAt(i - count);
                    //DataRow newRow = dt.NewRow();
                    //newRow[5] = 0;
                    //newRow[6] = 0;
                    //newRow[4] = 0;
                    //newRow["IsCalculate"] = 0;
                    //newRow["IsChange"] = 0;
                    //dt.Rows.InsertAt(newRow, dt.Rows.Count - 1);
                    count++;
                }
            }
            //删除之后,如果少于10行，则需要增加行数
            for (int i = dt.Rows.Count; i < 10; i++)
            {
                DataRow newRow = dt.NewRow();

                newRow[5] = 0;
                newRow[6] = 0;
                newRow[4] = 0;
                newRow["IsCalculate"] = 0;
                newRow["IsChange"] = 0;
                dt.Rows.Add(newRow);
            }

            this.GridView1.DataSource = dt;

            this.GridView1.DataBind();

        }

        //保存操作

        protected void Save_Click(object sender, EventArgs e)
        {
            //此处是保存操作
            List<string> sqllist = new List<string>();

            string sql = "";
            string Code = LabelCode.Text;//单号
            string Date = TextBoxDate.Text;//日期
            string APPLYID = LabelDocCode.Text;//制单人
            string APPLY = LabelDoc.Text;
            string SHR = txtshr.Text;
            string SHRID = shrid.Value.Trim();



            if (flag == "add")
            {
                string year = DateTime.Now.Year.ToString();
                string month = DateTime.Now.Month.ToString().PadLeft(2, '0');
                double benciZongJ = 0;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {

                    string sId = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("WLCODE")).Value.Trim();//物料代码



                    if (sId != string.Empty)
                    {
                        string bm = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLBM")).Value;
                        string name = ((TextBox)this.GridView1.Rows[i].FindControl("WLNAME")).Text.Trim();
                        string model = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLMODEL")).Value.Trim();
                        string unit = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLUNIT")).Value.Trim();
                        string num = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNUM")).Value.Trim();//申请数量
                        string dj = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLPRICE")).Value.Trim();//单价
                        string je = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLJE")).Value.Trim();//金额
                        string note = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNOTE")).Value.Trim();//备注
                        string ischange = ((DropDownList)this.GridView1.Rows[i].FindControl("ddlIsChange")).SelectedValue;
                        string kcNum = ((HtmlInputText)this.GridView1.Rows[i].FindControl("num")).Value;//库存

                        Regex regMoeny = new Regex(@"^-?(\d)*((.?)(\d){1,2})?$");
                        Regex regNum = new Regex(@"^-?(\d)*((.?)(\d){1,2})?$");
                        if (regNum.IsMatch(num) && regMoeny.IsMatch(dj) && regMoeny.IsMatch(je))
                        {
                            sql = "insert into TBOM_BGYPAPPLY(WLCODE,WLBM,WLNAME,WLMODEL,WLUNIT,WLPRICE,WLNUM,WLJE,WLNOTE,APPLY,APPLYID,REVIEW,REVIEWID,CODE,DATE,year,month,IsChange) values('" + sId + "','" + bm + "','" + name + "','" + model + "','" + unit + "','" + dj + "','" + num + "','" + je + "','" + note + "','" + APPLY + "','" + APPLYID + "','" + SHR + "','" + SHRID + "','" + Code + "','" + Date + "','" + year + "','" + month + "','" + ischange + "')";
                            sqllist.Add(sql);
                            if (bm.IndexOf("3-") != 0)
                            {
                                benciZongJ += CommonFun.ComTryDouble(je);
                            }
                        }
                        else
                        {
                            Response.Write("<script>alert('第" + i + 1 + "行数据有误，请查证后在进行保存!')</script>");
                            return;
                        }
                        if (CommonFun.ComTryDouble(num) > CommonFun.ComTryDouble(kcNum))
                        {
                            Response.Write("<script>alert('库存不足，无法申请!')</script>");
                            return;
                        }
                    }
                }

                if (txtshr.Text.ToString() == "")
                {
                    Response.Write("<script>alert('请选择审核人!')</script>");
                    return;
                }
                else
                {
                    DBCallCommon.ExecuteTrans(sqllist);
                    string _emailto = DBCallCommon.GetEmailAddressByUserID(shrid.Value);
                    string _body = "办公用品使用审批任务:"
                          + "\r\n编 号：" + LabelCode.Text.ToString().Trim()
                          + "\r\n制单日期：" + TextBoxDate.Text.ToString().Trim();

                    string _subject = "您有新的【办公用品使用】需要审批，请及时处理:" + LabelCode.Text.ToString().Trim();
                    DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                    double BenYueSLS = 0;
                    string sqlSum = "select sum(GET_MONEY) from View_TBOM_BGYPAPPLY where WLBM not like '3-%' and  year='" + year + "' and month='" + month + "' and ST_DEPID='" + Session["UserDeptID"] + "' ";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlSum);
                    if (dt.Rows.Count > 0)
                    {
                        BenYueSLS = CommonFun.ComTryDouble(dt.Rows[0][0].ToString());
                    }

                    sqlSum = "select MONTH_MAX from TBOM_BGYP_Month_max where type='1' and  year='" + year + "' and month='" + month + "' and DEP_CODE='" + Session["UserDeptID"] + "' ";
                    dt = DBCallCommon.GetDTUsingSqlText(sqlSum);
                    double byDE = 0;
                    if (dt.Rows.Count > 0)
                    {
                        byDE = CommonFun.ComTryDouble(dt.Rows[0][0].ToString());

                    }
                    else
                    {
                        sqlSum = "select MONTH_MAX from TBOM_BGYP_Month_max where type='0' and DEP_CODE='" + Session["UserDeptID"] + "' ";
                        dt = DBCallCommon.GetDTUsingSqlText(sqlSum);
                        if (dt.Rows.Count > 0)
                        {
                            byDE = CommonFun.ComTryDouble(dt.Rows[0][0].ToString());
                        }
                    }
                    if (benciZongJ + BenYueSLS > byDE)
                    {
                        Response.Write("<script>alert('提示：本月申请总计【" + (benciZongJ + BenYueSLS).ToString() + "】已超本月额度【" + byDE + "】,申请仍会通过，请注意！');window.location='OM_BgypApplyMain.aspx';</script>");
                    }
                    else
                    {

                        Response.Redirect("OM_BgypApplyMain.aspx");
                    }


                }
            }
            if (flag == "verify")
            {
                sql = "update TBOM_BGYPAPPLY set REVIEWSTATE='" + drop_view.SelectedValue.ToString() + "' WHERE CODE='" + Code + "'";
                DBCallCommon.ExeSqlText(sql);
                if (drop_view.SelectedValue.ToString() == "2")
                {
                    string _emailto = DBCallCommon.GetEmailAddressByUserID("181");
                    string _body = "办公用品使用审批任务:"
                          + "\r\n编 号：" + LabelCode.Text.ToString().Trim()
                          + "\r\n制单日期：" + TextBoxDate.Text.ToString().Trim();

                    string _subject = "您有新的【办公用品使用】需要添加实领数，请及时处理:" + LabelCode.Text.ToString().Trim();
                    DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                }
                Response.Redirect("OM_BgypApplyMain.aspx");
            }
            if (flag == "addsls")
            {

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {

                    string sId = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("WLCODE")).Value.Trim();//物料代码

                    if (sId != string.Empty)
                    {
                        string sls = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLSLS")).Value.Trim();
                        string get_money = ((HtmlInputText)this.GridView1.Rows[i].FindControl("GET_MONEY")).Value.Trim();
                        string kcNum = ((HtmlInputText)this.GridView1.Rows[i].FindControl("num")).Value;
                        string StoreId = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("hidStoreId")).Value;
                        string isCalculate = ((DropDownList)this.GridView1.Rows[i].FindControl("ddlIsCal")).SelectedValue;
                        string IsChange = ((DropDownList)this.GridView1.Rows[i].FindControl("ddlIsChange")).SelectedValue;
                        if (CommonFun.ComTryDouble(sls) <= CommonFun.ComTryDouble(kcNum))
                        {
                            if (sls == "")
                            {
                                sql = "update TBOM_BGYPAPPLY set WLSLS='',GET_MONEY='',IsCalculate='" + isCalculate + "',IsChange='" + IsChange + "' WHERE CODE='" + Code + "' AND WLCODE='" + sId + "'";
                            }
                            else
                            {
                                sql = "update TBOM_BGYPAPPLY set WLSLS='" + CommonFun.ComTryInt(sls) + "',GET_MONEY='" + CommonFun.ComTryDouble(get_money) + "',IsCalculate='" + isCalculate + "',IsChange='" + IsChange + "'  WHERE CODE='" + Code + "' AND WLCODE='" + sId + "'";
                            }

                            sqllist.Add(sql);
                            sql = "update TBOM_BGYP_STORE set num=num-" + CommonFun.ComTryInt(sls) + ",price=price-unPrice*" + CommonFun.ComTryInt(sls) + " WHERE  Id='" + StoreId + "'";
                            sqllist.Add(sql);

                        }
                        else
                        {
                            Response.Write("<script>alert('库存不足，无法领取!')</script>");
                            return;
                        }


                    }
                    else
                    {
                        Response.Write("<script>alert('物料编码不能为零!')</script>");
                        return;
                    }
                }
                DBCallCommon.ExecuteTrans(sqllist);
                Response.Redirect("OM_BgypApplyMain.aspx");
            }
            if (flag == "mod")
            {
                sql = "delete from TBOM_BGYPAPPLY where CODE='" + Code + "'";
                sqllist.Add(sql);
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {

                    string sId = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("WLCODE")).Value.Trim();//物料代码

                    if (sId != string.Empty)
                    {
                        string bm = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLBM")).Value;
                        string name = ((TextBox)this.GridView1.Rows[i].FindControl("WLNAME")).Text.Trim();
                        string model = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLMODEL")).Value.Trim();
                        string unit = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLUNIT")).Value.Trim();
                        string num = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNUM")).Value.Trim();//数量
                        string dj = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLPRICE")).Value.Trim();//数量
                        string je = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLJE")).Value.Trim();//金额
                        string note = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNOTE")).Value.Trim();//备注
                        string ischange = ((DropDownList)this.GridView1.Rows[i].FindControl("ddlIsChange")).SelectedValue;
                        Regex regMoeny = new Regex(@"^-?(\d)*((.?)(\d){1,2})?$");
                        Regex regNum = new Regex(@"^-?(\d)*((.?)(\d){1,2})?$");
                        if (regNum.IsMatch(num) && regMoeny.IsMatch(dj) && regMoeny.IsMatch(je))
                        {
                            sql = "insert into TBOM_BGYPAPPLY(WLCODE,WLBM,WLNAME,WLMODEL,WLUNIT,WLPRICE,WLNUM,WLJE,WLNOTE,APPLY,APPLYID,REVIEW,REVIEWID,CODE,DATE,IsChange) values('" + sId + "','" + bm + "','" + name + "','" + model + "','" + unit + "','" + dj + "','" + num + "','" + je + "','" + note + "','" + APPLY + "','" + APPLYID + "','" + SHR + "','" + SHRID + "','" + Code + "','" + Date + "','" + ischange + "')";
                            sqllist.Add(sql);

                        }
                        else
                        {
                            Response.Write("<script>alert('第" + i + 1 + "行数据有误，请查证后在进行保存!')</script>");
                            return;
                        }
                    }
                }

                DBCallCommon.ExecuteTrans(sqllist);
                Response.Redirect("OM_BgypApplyMain.aspx");
            }
        }
    }
}
