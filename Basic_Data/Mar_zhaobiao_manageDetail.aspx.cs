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
using System.Text;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class Mar_zhaobiao_manageDetail : System.Web.UI.Page
    {
        static string mar_ID = string.Empty;//全局变量id
        static string ibID = string.Empty;//全局变量id
        static string action = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "招标物料信息管理";
                if (Request.QueryString["id"] == null && Request.QueryString["ibid"] == null && Request.QueryString["action"] == null)
                    throw new NoNullAllowedException("不允许空值传递！");
                else
                {
                    if (Request.QueryString["ibid"] != null && Request.QueryString["ibid"] != "")
                    {
                        ibID = Request.QueryString["ibid"].ToString();
                    }
                    if (Request.QueryString["action"] != null && Request.QueryString["action"] != "")
                    {
                        action = Request.QueryString["action"].ToString();
                    }
                    
                    if (action == "update")  //更新项目
                    {
                        ib_marid.Enabled=false;
                        mar_ID = Request.QueryString["id"].ToString();
                        showcurrentPJ_INFO(ibID);
                    }
                }
            }
        }

        void showcurrentPJ_INFO(string ibID)
        {
            string cmdStr = " SELECT IB_MARID,IB_SUPPLY, IB_PRICE, IB_DATE, IB_FIDATE, IB_SUBMIT, IB_NOTE, IB_TAXRATE, IB_LENGTH,IB_WIDTH, ISNULL(IB_STATE, 0) AS IB_STATE FROM TBPC_INVITEBID  WHERE IB_ID='" + ibID + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(cmdStr);
            while (dr.Read())
            {
                ib_marid.Text = dr["IB_MARID"].ToString();
                ib_supply.Text = dr["IB_SUPPLY"].ToString();
                ib_price.Text = dr["IB_PRICE"].ToString();           
                ib_taxrate.Text = dr["IB_TAXRATE"].ToString();
                ib_date.Value = dr["IB_DATE"].ToString();
                ib_fidate.Value =  dr["IB_FIDATE"].ToString();
                //ib_length.Text = dr["IB_LENGTH"].ToString();
                //ib_width.Text = dr["IB_WIDTH"].ToString();              
                ib_note.Text = dr["IB_NOTE"].ToString();
                rblstatus.SelectedValue = dr["IB_STATE"].ToString();
            }
            dr.Close();
        }

        protected void btn_Submit_Click(object sender, EventArgs e)
        {

            //构建cmd语句
            string cmdText = string.Empty;
            if (action == "update")  //表示更新项目
            {
                cmdText = "UPDATE TBPC_INVITEBID SET " +
                          "IB_MARID='" + ib_marid.Text.Trim() + "', " +
                          "IB_SUPPLY='" + ib_supply.Text.Trim() + "', " +
                          "IB_PRICE='" + ib_price.Text + "', " +
                          "IB_TAXRATE='" + ib_taxrate.Text + "'," +
                          "IB_DATE='" + ib_date.Value + "', " +
                          "IB_FIDATE='" + ib_fidate.Value + "', " +
                          "IB_NOTE='" + ib_note.Text + "' ," +
                          " IB_STATE='" + rblstatus.SelectedValue+ "'" +
                          " WHERE(IB_ID = '" + ibID + "')";
            }
            else if (action == "add") //表示添加项目
            {
                if (vefrify(ib_marid.Text.Trim()))
                {
                    cmdText = "INSERT INTO TBPC_INVITEBID(IB_MARID,IB_SUPPLY,IB_PRICE,IB_TAXRATE,IB_DATE,IB_FIDATE,IB_NOTE,IB_SUBMIT,IB_STATE)  VALUES('"
                                                               + ib_marid.Text.Trim() + "'," +
                                                            "'" + ib_supply.Text.Trim() + "'," +
                                                            "'" + ib_price.Text + "'," +
                                                            "'" + ib_taxrate.Text + "'," +
                                                            "'" + ib_date.Value + "'," +
                                                            "'" + ib_fidate.Value + "'," +
                                                            "'" + ib_note.Text + "'," +
                                                            "'" + rblstatus.SelectedValue + "'," +
                                                            "'" + Session["UserID"].ToString() + "')";
                }
                else
                {
                    
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该招标物料正在使用，不能重复添加！');", true);
                    return;
                }
            }

            try
            {
                //lbl_Info.Text = cmdText;
                DBCallCommon.ExeSqlText(cmdText);
            }
            catch (Exception sqlEx)
            {
                lbl_Info.Text = cmdText + "<br />抱歉，出现错误,请重试。错误详情：" + sqlEx.Message;
            }
            finally
            {
                lbl_Info.Text = "完成提交，正在返回……";
                Response.Write("<script>javascript:window.close();</script>");
            }

        }

        protected void ib_marid_Textchanged(object sender, EventArgs e)
        {
            DataTable glotb = new DataTable();
            if (ib_marid.Text.ToString().Contains("|"))
            {
                string marid = ib_marid.Text.Substring(0, ib_marid.Text.ToString().IndexOf("|"));
                ib_marid.Text = marid;
            }
            else
            {
                if (!(ib_marid.Text == "" || ib_marid.Text == DBNull.Value.ToString()))
                {
                    string sqltext = "SELECT ID  FROM TBMA_MATERIAL WHERE ID='" + ib_marid.Text + "' ORDER BY ID";
                    glotb = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (glotb.Rows.Count != 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入的物料编码不正确，请重新输入！');", true);
                    }
                }
            }
        }


        protected void ib_supply_Textchanged(object sender, EventArgs e)
        {
            DataTable glotb = new DataTable();
            if (ib_supply.Text.ToString().Contains("|"))
            {
                string supplyid = ib_supply.Text.Substring(0, ib_supply.Text.ToString().IndexOf("|"));
                ib_supply.Text = supplyid;
            }
            else
            {
                if (!(ib_supply.Text == "" || ib_supply.Text == DBNull.Value.ToString()))
                {
                    string sqltext = "SELECT CS_CODE  FROM TBCS_CUSUPINFO WHERE CS_CODE='" + ib_supply.Text + "' AND CS_State='0'";
                    glotb = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (glotb.Rows.Count != 1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入的供应商不正确，请重新输入！');", true);
                    }
                }
            }
        }


        bool vefrify(string marid)
        {
            string sqlserver1 = " select IB_MARID from TBPC_INVITEBID where IB_STATE='0' and  IB_MARID='"+marid+"' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlserver1);
            if (dt.Rows.Count > 0)
            {
                return false;
              
            }
            else
            {
                return true;
            }

        }
    }
}

