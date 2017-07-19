using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_BGYP_IN_Detail : System.Web.UI.Page
    {
        int count = 0;

        string flag = string.Empty;

        string incode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["FLAG"] != null)
                flag = Request.QueryString["FLAG"];



            if (!IsPostBack)
            {
                //((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                if (flag == "IN")
                {
                    InitInfo();
                    InitGridview();
                    string sql_buy = "select WLBM,WLCODE,WLNAME,WLMODEL,WLUNIT,WLNUM,WLPRICE,WLJE,NOTE,CODE from View_TBOM_BGYPPCAPPLYINFO where STATE='2' and STATE_rk is NULL AND (HZSTATE is not null and HZSTATE<>'2')";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_buy);
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        ((TextBox)this.GridView1.Rows[i].FindControl("maId")).Text = dt.Rows[i][0].ToString();
                        ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("sId")).Value = dt.Rows[i][1].ToString();
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("name")).Value = dt.Rows[i][2].ToString();
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("canshu")).Value = dt.Rows[i][3].ToString();
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("unit")).Value = dt.Rows[i][4].ToString();
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("num")).Value = dt.Rows[i][5].ToString();
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("uprice_dj")).Value = dt.Rows[i][6].ToString();
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("price_je")).Value = dt.Rows[i][7].ToString();
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("note")).Value = dt.Rows[i][8].ToString();
                        ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("CODE")).Value = dt.Rows[i][9].ToString();
                        ((Label)this.GridView1.Rows[i].FindControl("lblCode")).Text = dt.Rows[i][9].ToString();
                        if (((TextBox)this.GridView1.Rows[i].FindControl("maId")).Text.ToString() != "")
                        {
                            ((TextBox)this.GridView1.Rows[i].FindControl("maId")).Enabled = false;
                            ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("sId")).Disabled = true;
                            ((HtmlInputText)this.GridView1.Rows[i].FindControl("name")).Disabled = true;
                            ((HtmlInputText)this.GridView1.Rows[i].FindControl("canshu")).Disabled = true;
                            ((HtmlInputText)this.GridView1.Rows[i].FindControl("unit")).Disabled = true;
                            //   ((HtmlInputText)this.GridView1.Rows[i].FindControl("num")).Disabled = true;
                            //((HtmlInputText)this.GridView1.Rows[i].FindControl("uprice_dj")).Disabled = true;
                            //((HtmlInputText)this.GridView1.Rows[i].FindControl("price_je")).Disabled = true;

                        }
                    }
                    btninsert.Visible = false;
                }
                if (flag == "LOOK")
                {
                    btnsave.Enabled = false;
                    btninsert.Enabled = false;
                    btndelete.Enabled = false;
                    string Id = Request.QueryString["Id"].ToString();

                    LabelCode.Text = Id;
                    //InitInfo();
                    string sql = "select * from View_OM_BGYP_IN where rkcode='" + Id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    TextBoxDate.Text = dt.Rows[0]["MakeTime"].ToString().Trim();
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
                if (flag == "Change")//删除掉了---已经去掉
                {
                    string Id = Request.QueryString["Id"].ToString();
                    //InitInfo();
                    //制单人
                    LabelCode.Text = Id;

                    LabelCode.Text = Id;
                    string sql = "select * from View_OM_BGYP_IN where rkcode='" + Id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    TextBoxDate.Text = dt.Rows[0]["MakeTime"].ToString().Trim();
                    GridView1.DataSource = dt;
                    GridView1.DataBind();
                }
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


        protected string generateTempCode()
        {

            List<string> lt = new List<string>();

            string sql = "SELECT Code FROM TBOM_BGYP_INCODE WHERE len(Code)=10";

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
                return "B000000001";
            }
            else
            {
                string tempstr = wsidlist.Max<string>();
                int tempnum = Convert.ToInt32((tempstr.Substring(1, 9)));
                tempnum++;
                tempstr = "B" + tempnum.ToString().PadLeft(9, '0');
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

            int check_no = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    check_no++;
                }
            }
            if (check_no == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要插入的行！！！');", true);
                return;
            }

            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];

                CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");

                if (chk.Checked)
                {
                    DataRow newRow = dt.NewRow();

                    newRow[5] = 0;
                    newRow[7] = 0;
                    newRow[8] = 0;


                    dt.Rows.InsertAt(newRow, i + 1);

                    count++;
                }
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
            dt.Columns.Add("sId");
            dt.Columns.Add("maId");

            dt.Columns.Add("name");

            dt.Columns.Add("canshu");

            dt.Columns.Add("Unit");

            dt.Columns.Add("num");

            dt.Columns.Add("note");//

            dt.Columns.Add("uprice");

            dt.Columns.Add("price");
            dt.Columns.Add("CODE");



            #endregion



            if (!isInit)
            {

                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];

                    DataRow newRow = dt.NewRow();

                    newRow["sId"] = ((HtmlInputHidden)gRow.FindControl("sId")).Value;
                    newRow["maId"] = ((TextBox)gRow.FindControl("maId")).Text;
                    newRow["name"] = ((HtmlInputText)gRow.FindControl("name")).Value;
                    newRow["canshu"] = ((HtmlInputText)gRow.FindControl("canshu")).Value;
                    newRow["Unit"] = ((HtmlInputText)gRow.FindControl("Unit")).Value;
                    newRow["num"] = ((HtmlInputText)gRow.FindControl("num")).Value;
                    newRow["note"] = ((HtmlInputText)gRow.FindControl("note")).Value;
                    newRow["uprice"] = ((HtmlInputText)gRow.FindControl("uprice_dj")).Value;
                    newRow["price"] = ((HtmlInputText)gRow.FindControl("price_je")).Value;
                    newRow["CODE"] = ((HtmlInputHidden)gRow.FindControl("CODE")).Value;
                    dt.Rows.Add(newRow);
                }
            }

            string sql_buy = "select WLBM,WLCODE,WLNAME,WLMODEL,WLUNIT,WLNUM,WLPRICE,WLJE,NOTE from View_TBOM_BGYPPCAPPLYINFO where STATE='2' and STATE_rk is NULL and (HZSTATE is not null and HZSTATE<>'2')";
            DataTable dt_buy = DBCallCommon.GetDTUsingSqlText(sql_buy);
            if (isInit)//新增入库的时候，行数与有数据的行数相等
            {
                for (int i = 0; i < dt_buy.Rows.Count; i++)
                {
                    DataRow newRow = dt.NewRow();

                    newRow[5] = 0;
                    newRow[7] = 0;
                    newRow[8] = 0;

                    dt.Rows.Add(newRow);
                }
            }

            dt.AcceptChanges();

            return dt;
        }


        /********************删除行******************************/

        protected void btndelete_Click(object sender, EventArgs e)
        {
            int check_no = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    check_no++;
                }
            }
            if (check_no == 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请勾选要删除的行！！！');", true);
                return;
            }

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
                    //newRow[7] = 0;
                    //newRow[8] = 0;
                    //dt.Rows.InsertAt(newRow, dt.Rows.Count - 1);
                    count++;
                }
            }
            ////删除之后,如果少于10行，则需要增加行数
            //for (int i = dt.Rows.Count; i < 10; i++)
            //{
            //    DataRow newRow = dt.NewRow();

            //    newRow[5] = 0;
            //    newRow[7] = 0;
            //    newRow[8] = 0;
            //    dt.Rows.Add(newRow);
            //}

            this.GridView1.DataSource = dt;

            this.GridView1.DataBind();

        }

        //保存操作
        protected void Save_Click(object sender, EventArgs e)
        {
            //此处是保存操作
            List<string> sqllist = new List<string>();
            List<string> liststorege = new List<string>();//更新库存
            List<string> listinsert = new List<string>();//插入没有的数据
            string sql = "";
            string Code = LabelCode.Text;//单号
            string Date = TextBoxDate.Text;//日期
            string DocCode = LabelDocCode.Text;//制单人单号

            sql = "DELETE FROM TBOM_BGYP_IN WHERE Code='" + Code + "'";

            sqllist.Add(sql);

            //1为备库

            sql = "INSERT INTO TBOM_BGYP_IN (Code, Maker, MakerNM, MakeTime) values('" + Code + "','" + LabelDocCode.Text + "','" + LabelDoc.Text + "','" + TextBoxDate.Text + "')";

            sqllist.Add(sql);

            sql = "DELETE FROM TBOM_BGYP_IN_DETAIL WHERE wId='" + Code + "'";

            sqllist.Add(sql);

            int count_add = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {

                string sId = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("sId")).Value.Trim();//物料代码
                string code_CG = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("CODE")).Value.Trim();
                string txt_maid = ((TextBox)this.GridView1.Rows[i].FindControl("maId")).Text.ToString();
                string sql_CG = "update TBOM_BGYPPCAPPLYINFO set STATE_rk='1' where CODE='" + code_CG + "' and WLBM='" + txt_maid + "'";
                sqllist.Add(sql_CG);
                // DBCallCommon.ExeSqlText(sql_CG);
                string input_name = ((HtmlInputText)this.GridView1.Rows[i].FindControl("name")).Value.Trim();
                if (txt_maid != "" && input_name != "")
                {
                    count_add++;
                }

                if (sId != string.Empty)
                {
                    string num = ((HtmlInputText)this.GridView1.Rows[i].FindControl("num")).Value.Trim();//数量
                    string dj = ((HtmlInputText)this.GridView1.Rows[i].FindControl("uprice_dj")).Value.Trim() == "" ? "0" : ((HtmlInputText)this.GridView1.Rows[i].FindControl("uprice_dj")).Value.Trim();//单价
                    double djia = CommonFun.ComTryDouble(dj);
                    string je = ((HtmlInputText)this.GridView1.Rows[i].FindControl("price_je")).Value.Trim() == "" ? "0" : ((HtmlInputText)this.GridView1.Rows[i].FindControl("price_je")).Value.Trim();//金额
                    string note = ((HtmlInputText)this.GridView1.Rows[i].FindControl("note")).Value.Trim();//备注
                    string ordernum = ((Label)this.GridView1.Rows[i].FindControl("lblCode")).Text.Trim();//订单号
                    Regex regMoeny = new Regex(@"^-?(\d)*((.?)(\d){1,2})?$");
                    Regex regNum = new Regex(@"^-?(\d)*((.?)(\d){1,2})?$");
                    if (regNum.IsMatch(num) && regMoeny.IsMatch(dj) && regMoeny.IsMatch(je))
                    {
                        sql = "insert into TBOM_BGYP_IN_DETAIL(wId, sId, uprice, price, num, note,ordercode) values('" + Code + "'," + sId + "," + dj + "," + je + "," + num + ",'" + note + "','" + ordernum + "')";
                        sqllist.Add(sql);
                        sql = "select * from TBOM_BGYP_STORE where mId=" + sId;
                        //sql = "select num,price,Id from TBOM_BGYP_STORE where mId=" + sId + " and unPrice =" + dj + "";
                        //unPrice
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                        if (dt.Rows.Count > 0)
                        {
                            float zongnum = float.Parse(dt.Rows[0]["num"].ToString()) + float.Parse(num);
                            float zongje = float.Parse(dt.Rows[0]["price"].ToString()) + float.Parse(je);
                            string id = dt.Rows[0]["Id"].ToString();
                            if (zongnum > 0 || zongnum == 0)
                            {
                                if (zongnum > 0)
                                {
                                    sql = "update TBOM_BGYP_STORE set num=" + zongnum + ",unPrice='" + zongje / zongnum + "',price=" + zongje + " where Id=" + id;
                                }
                                else
                                {
                                    sql = "update TBOM_BGYP_STORE set num=" + zongnum + ",unPrice='" + dj + "',price=" + zongje + " where Id=" + id;
                                }
                                liststorege.Clear();
                                liststorege.Add(sql);
                                DBCallCommon.ExecuteTrans(liststorege);
                            }
                            else
                            {
                                Response.Write("<script>alert('第" + i + 1 + "行数据有误，库存无足够数量!')</script>");
                                return;
                            }
                        }
                        else
                        {
                            if (float.Parse(num) > 0)
                            {
                                sql = "insert into TBOM_BGYP_STORE(mId, price, num, unPrice) values('" + sId + "'," + (CommonFun.ComTryDouble(num) * CommonFun.ComTryDouble(dj)).ToString() + "," + num + "," + dj + ")";
                                listinsert.Clear();
                                listinsert.Add(sql);
                                DBCallCommon.ExecuteTrans(listinsert);
                            }
                            else
                            {
                                Response.Write("<script>alert('第" + i + 1 + "行数据有误，库存无足够数量!')</script>");
                                return;
                            }
                        }
                    }
                    else
                    {
                        Response.Write("<script>alert('第" + i + 1 + "行数据有误，请查证后在进行保存!')</script>");
                        return;
                    }

                }
            }
            DBCallCommon.ExecuteTrans(sqllist);

            if (count_add == 0)
            {
                string sql_delete = "DELETE FROM TBOM_BGYP_IN WHERE Code='" + Code + "'";
                DBCallCommon.ExeSqlText(sql_delete);

                string sql_delete1 = "DELETE FROM TBOM_BGYP_IN_DETAIL WHERE wId='" + Code + "'";
                DBCallCommon.ExeSqlText(sql_delete1);
            }
            Response.Redirect("OM_BGYP_In_List.aspx");
        }
    }
}

