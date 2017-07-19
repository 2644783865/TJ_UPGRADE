using System;
using System.Collections;
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
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_GSFPDETAIL : System.Web.UI.Page
    {
        string action = "";
        string bh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                action = Request.QueryString["action"].ToString().Trim();
                if (action == "add")
                {
                    defaultdata();
                }
                else if (action == "edit")
                {
                    lbusername.Text = Session["UserName"].ToString().Trim();
                    lbuserid.Text = Session["UserID"].ToString().Trim();
                    binddata();
                }
                else if (action == "account")
                {
                    lbusername.Text = Session["UserName"].ToString().Trim();
                    binddata();
                }
                else if (action == "view")
                {
                    binddata();
                }
                controlpower();
            }
        }
        //默认数据
        private void defaultdata()
        {
            lbzdrname.Text = Session["UserName"].ToString().Trim();
            lbzdrid.Text = Session["UserID"].ToString().Trim();
            lbusername.Text = Session["UserName"].ToString().Trim();
            lbuserid.Text = Session["UserID"].ToString().Trim();
            lbzdtime.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim();
        }

        private void binddata()
        {
            bh = Request.QueryString["gs_bh"].ToString().Trim();
            string sql0 = "select * from TBTM_GSMANAGEMENT where gs_bh='" + bh + "'";
            DataTable dt0=DBCallCommon.GetDTUsingSqlText(sql0);
            if (dt0.Rows.Count>0)
            {
                lbzdrname.Text = dt0.Rows[0]["gs_zdrname"].ToString().Trim();
                lbzdrid.Text = dt0.Rows[0]["gs_zdrid"].ToString().Trim();
                lbzdtime.Text = dt0.Rows[0]["gs_zdtime"].ToString().Trim();
                rad_state.SelectedValue = dt0.Rows[0]["gs_state"].ToString().Trim();
                lbdjbh.Text = dt0.Rows[0]["gs_bh"].ToString().Trim();
                txtjcbh.Text = dt0.Rows[0]["gs_jcbh"].ToString().Trim();
                txtjctype.Text = dt0.Rows[0]["gs_jctype"].ToString().Trim();
                yearmonth.Value = dt0.Rows[0]["gs_yearmonth"].ToString().Trim();
                txtcpname.Text = dt0.Rows[0]["gs_cpname"].ToString().Trim();
                txtcpguige.Text = dt0.Rows[0]["gs_cpguige"].ToString().Trim();
                txtzongmap.Text = dt0.Rows[0]["gs_zongmap"].ToString().Trim();
                txtbjname.Text = dt0.Rows[0]["gs_bjname"].ToString().Trim();
                txtbjmap.Text = dt0.Rows[0]["gs_bjth"].ToString().Trim();
                txtbjpergs.Text = dt0.Rows[0]["gs_bjpergs"].ToString().Trim();
                txtbjnum.Text = dt0.Rows[0]["gs_bjnum"].ToString().Trim();
                txtbjtolgs.Text = dt0.Rows[0]["gs_bjtotalgs"].ToString().Trim();
                txtrealbjpergs.Text = dt0.Rows[0]["gs_realbjpergs"].ToString().Trim();
                txtrealbjnum.Text = dt0.Rows[0]["gs_realbjnum"].ToString().Trim();
                txtrealbjtotalgs.Text = dt0.Rows[0]["gs_realbjtotalgs"].ToString().Trim();
                jsrname.Text = dt0.Rows[0]["gs_jsrname"].ToString().Trim();
                jsrid.Text = dt0.Rows[0]["gs_jsrid"].ToString().Trim();
                lbjstime.Text = dt0.Rows[0]["gs_jstime"].ToString().Trim();
                txtperinfo.Text = dt0.Rows[0]["gs_perinfo"].ToString().Trim();
                txtnote.Text = dt0.Rows[0]["gs_note"].ToString().Trim();
            }
            string sql1 = "select * from TBTM_GSDETAIL where gs_detailbh='" + bh + "'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            if (dt1.Rows.Count > 0)
            {
                Det_Repeater.DataSource = dt1;
                Det_Repeater.DataBind();
            }
        }

        //增加行
        protected void btnadd_Click(object sender, EventArgs e)
        {
            int a = 0;
            if (int.TryParse(txtNum.Text, out a))
            {
                CreateNewRow(a);
            }
            else
            {
                Response.Write("<script>alert('请输入数字！')</script>");
            }
            controlpower();
        }
        private void CreateNewRow(int num) // 生成输入行函数
        {
            DataTable dt = this.GetDataTable();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            List<string> col = new List<string>();
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
        }

        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("detailid");
            dt.Columns.Add("gs_ljmap");
            dt.Columns.Add("gs_ljname");
            dt.Columns.Add("gs_ljnum");
            dt.Columns.Add("cfj_mapbh");
            dt.Columns.Add("cfj_name");
            dt.Columns.Add("cfj_num");
            dt.Columns.Add("gs_gxdetal");
            dt.Columns.Add("gs_cfjpergs");
            dt.Columns.Add("gs_cfjtolgs");
            dt.Columns.Add("gs_notedetail");
            dt.Columns.Add("realljnum");
            dt.Columns.Add("realcfjnum");
            dt.Columns.Add("realcfjpergs");
            dt.Columns.Add("realcfjtolgs");
            dt.Columns.Add("realnote");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                DataRow newRow = dt.NewRow();
                newRow[0] = ((HiddenField)retItem.FindControl("detailid")).Value.Trim();
                newRow[1] = ((TextBox)retItem.FindControl("gs_ljmap")).Text.Trim();
                newRow[2] = ((TextBox)retItem.FindControl("gs_ljname")).Text.Trim();
                newRow[3] = ((TextBox)retItem.FindControl("gs_ljnum")).Text.Trim();
                newRow[4] = ((TextBox)retItem.FindControl("cfj_mapbh")).Text.Trim();
                newRow[5] = ((TextBox)retItem.FindControl("cfj_name")).Text.Trim();
                newRow[6] = ((TextBox)retItem.FindControl("cfj_num")).Text.Trim();
                newRow[7] = ((TextBox)retItem.FindControl("gs_gxdetal")).Text.Trim();
                newRow[8] = ((TextBox)retItem.FindControl("gs_cfjpergs")).Text.Trim();
                newRow[9] = ((TextBox)retItem.FindControl("gs_cfjtolgs")).Text.Trim();
                newRow[10] = ((TextBox)retItem.FindControl("gs_notedetail")).Text.Trim();
                newRow[11] = ((TextBox)retItem.FindControl("realljnum")).Text.Trim();
                newRow[12] = ((TextBox)retItem.FindControl("realcfjnum")).Text.Trim();
                newRow[13] = ((TextBox)retItem.FindControl("realcfjpergs")).Text.Trim();
                newRow[14] = ((TextBox)retItem.FindControl("realcfjtolgs")).Text.Trim();
                newRow[15] = ((TextBox)retItem.FindControl("realnote")).Text.Trim();
                dt.Rows.Add(newRow);
            }
            dt.AcceptChanges();
            return dt;
        }

        //删除行
        protected void delete_Click(object sender, EventArgs e)
        {
            List<string> col = new List<string>();
            DataTable dt = new DataTable();
            dt.Columns.Add("detailid");
            dt.Columns.Add("gs_ljmap");
            dt.Columns.Add("gs_ljname");
            dt.Columns.Add("gs_ljnum");
            dt.Columns.Add("cfj_mapbh");
            dt.Columns.Add("cfj_name");
            dt.Columns.Add("cfj_num");
            dt.Columns.Add("gs_gxdetal");
            dt.Columns.Add("gs_cfjpergs");
            dt.Columns.Add("gs_cfjtolgs");
            dt.Columns.Add("gs_notedetail");
            dt.Columns.Add("realljnum");
            dt.Columns.Add("realcfjnum");
            dt.Columns.Add("realcfjpergs");
            dt.Columns.Add("realcfjtolgs");
            dt.Columns.Add("realnote");
            foreach (RepeaterItem retItem in Det_Repeater.Items)
            {
                CheckBox chk = (CheckBox)retItem.FindControl("chk");
                if (!chk.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    newRow[0] = ((HiddenField)retItem.FindControl("detailid")).Value.Trim();
                    newRow[1] = ((TextBox)retItem.FindControl("gs_ljmap")).Text.Trim();
                    newRow[2] = ((TextBox)retItem.FindControl("gs_ljname")).Text.Trim();
                    newRow[3] = ((TextBox)retItem.FindControl("gs_ljnum")).Text.Trim();
                    newRow[4] = ((TextBox)retItem.FindControl("cfj_mapbh")).Text.Trim();
                    newRow[5] = ((TextBox)retItem.FindControl("cfj_name")).Text.Trim();
                    newRow[6] = ((TextBox)retItem.FindControl("cfj_num")).Text.Trim();
                    newRow[7] = ((TextBox)retItem.FindControl("gs_gxdetal")).Text.Trim();
                    newRow[8] = ((TextBox)retItem.FindControl("gs_cfjpergs")).Text.Trim();
                    newRow[9] = ((TextBox)retItem.FindControl("gs_cfjtolgs")).Text.Trim();
                    newRow[10] = ((TextBox)retItem.FindControl("gs_notedetail")).Text.Trim();
                    newRow[11] = ((TextBox)retItem.FindControl("realljnum")).Text.Trim();
                    newRow[12] = ((TextBox)retItem.FindControl("realcfjnum")).Text.Trim();
                    newRow[13] = ((TextBox)retItem.FindControl("realcfjpergs")).Text.Trim();
                    newRow[14] = ((TextBox)retItem.FindControl("realcfjtolgs")).Text.Trim();
                    newRow[15] = ((TextBox)retItem.FindControl("realnote")).Text.Trim();
                    dt.Rows.Add(newRow);
                }
            }
            for (int i = 0; i < Det_Repeater.Items.Count; i++)
            {
                CheckBox chkdel = (CheckBox)Det_Repeater.Items[i].FindControl("chk");
                if (chkdel.Checked)
                {
                    string gs_cfjtolgs = ((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjtolgs")).Text.Trim();
                    string gs_ljnum = ((TextBox)Det_Repeater.Items[i].FindControl("gs_ljnum")).Text.Trim();
                    if (gs_cfjtolgs != "" && gs_ljnum != "" && txtbjpergs.Text.Trim() != "" && txtbjpergs.Text.Trim() != "0")
                    {
                        string bjpergs = txtbjpergs.Text.Trim();
                        string bjnum=txtbjnum.Text.Trim();
                        string bjpergsnew = ((CommonFun.ComTryDecimal(bjpergs)) - (CommonFun.ComTryDecimal(gs_cfjtolgs)) * (CommonFun.ComTryDecimal(gs_ljnum))).ToString().Trim();
                        txtbjpergs.Text = ((CommonFun.ComTryDecimal(bjpergs)) - (CommonFun.ComTryDecimal(gs_cfjtolgs)) * (CommonFun.ComTryDecimal(gs_ljnum))).ToString().Trim();

                        txtbjtolgs.Text = ((CommonFun.ComTryDecimal(bjpergsnew)) * (CommonFun.ComTryDecimal(bjnum))).ToString().Trim();
                    }


                    string realcfjtolgs = ((TextBox)Det_Repeater.Items[i].FindControl("realcfjtolgs")).Text.Trim();
                    string realljnum = ((TextBox)Det_Repeater.Items[i].FindControl("realljnum")).Text.Trim();
                    if (realcfjtolgs != "" && realljnum != "" && txtrealbjpergs.Text.Trim() != "" && txtrealbjpergs.Text.Trim() != "0")
                    {
                        string bjpergsreal = txtrealbjpergs.Text.Trim();
                        string bjnumreal = txtrealbjnum.Text.Trim();
                        string bjpergsrealnew = ((CommonFun.ComTryDecimal(bjpergsreal)) - (CommonFun.ComTryDecimal(realcfjtolgs)) * (CommonFun.ComTryDecimal(realljnum))).ToString().Trim();
                        txtrealbjpergs.Text = ((CommonFun.ComTryDecimal(bjpergsreal)) - (CommonFun.ComTryDecimal(realcfjtolgs)) * (CommonFun.ComTryDecimal(realljnum))).ToString().Trim();

                        txtrealbjtotalgs.Text = ((CommonFun.ComTryDecimal(bjpergsrealnew)) * (CommonFun.ComTryDecimal(bjnumreal))).ToString().Trim();
                    }
                }
            }
            this.Det_Repeater.DataSource = dt;
            this.Det_Repeater.DataBind();
            controlpower();
        }


        //加载基础数据
        protected void btnloadbase_click(object sender, EventArgs e)
        {
            string loadwhere = txtbjmapload.Text.Trim();
            string sql = "select sum(ljnum*cfjtolgs) as bjpergs from TBTM_GONGSBASE as a left join TBTM_GONGSBASEDETAIL as b on a.context=b.detailcontext where bjmap in('" + loadwhere + "') and state='0' and gxdetal like '%" + txtgx.Text.Trim() + "%'";
            DataTable dtpergs = DBCallCommon.GetDTUsingSqlText(sql);
            if (dtpergs.Rows.Count > 0)
            {
                txtbjpergs.Text = dtpergs.Rows[0]["bjpergs"].ToString().Trim();
                txtbjnum.Text = "1";
                txtbjtolgs.Text = dtpergs.Rows[0]["bjpergs"].ToString().Trim();

                txtrealbjpergs.Text = dtpergs.Rows[0]["bjpergs"].ToString().Trim();
                txtrealbjnum.Text = "1";
                txtrealbjtotalgs.Text = dtpergs.Rows[0]["bjpergs"].ToString().Trim();
            }
            string sqltext = "select '' as detailid,ljmap as gs_ljmap,ljname as gs_ljname,ljnum as gs_ljnum,cfjmapbh as cfj_mapbh,cfjname as cfj_name,cfjnum as cfj_num,gxdetal as gs_gxdetal,cfjpergs as gs_cfjpergs,cfjtolgs as gs_cfjtolgs,notedetail as gs_notedetail,ljnum as realljnum,cfjnum as realcfjnum,cfjpergs as realcfjpergs,cfjtolgs as realcfjtolgs,'' as realnote from TBTM_GONGSBASE as a left join TBTM_GONGSBASEDETAIL as b on a.context=b.detailcontext where bjmap in('" + loadwhere + "') and state='0' and gxdetal like '%" + txtgx.Text.Trim() + "%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                this.Det_Repeater.DataSource = dt;
                this.Det_Repeater.DataBind();
            }
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('加载完毕!');", true);
            controlpower();
        }

        //保存
        protected void btnsave_click(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            string sqltext = "";
            string gs_ljmap = "";
            string gs_ljname = "";
            action = Request.QueryString["action"].ToString().Trim();
            //增加
            if (action == "add")
            {
                string newbh="GS_"+DateTime.Now.ToString("yyyyMMddHHmmss").Trim()+"_"+lbuserid.Text.Trim();
                sqltext = "insert into TBTM_GSMANAGEMENT(gs_bh,gs_zdrid,gs_zdrname,gs_zdtime,gs_jcbh,gs_jctype,gs_yearmonth,gs_cpname,gs_cpguige,gs_zongmap,gs_bjth,gs_bjname,gs_bjnum,gs_bjpergs,gs_bjtotalgs,gs_realbjnum,gs_realbjpergs,gs_realbjtotalgs,gs_state,gs_note,gs_perinfo) values('" + newbh + "','" + lbuserid.Text.Trim() + "','" + lbusername.Text.Trim() + "','" + lbzdtime.Text.Trim() + "','" + txtjcbh.Text.Trim() + "','" + txtjctype.Text.Trim() + "','" + yearmonth.Value.Trim() + "','" + txtcpname.Text.Trim() + "','" + txtcpguige.Text.Trim() + "','" + txtzongmap.Text.Trim() + "','" + txtbjmap.Text.Trim() + "','" + txtbjname.Text.Trim() + "'," + CommonFun.ComTryDecimal(txtbjnum.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtbjpergs.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtbjtolgs.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtbjnum.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtbjpergs.Text.Trim()) + "," + CommonFun.ComTryDecimal(txtbjtolgs.Text.Trim()) + ",'0','" + txtnote.Text.Trim() + "','" + txtperinfo.Text.Trim() + "')";
                list_sql.Add(sqltext);
                for (int i = 0; i < Det_Repeater.Items.Count; i++)
                {
                    if (((TextBox)Det_Repeater.Items[i].FindControl("gs_ljmap")).Text.Trim() != "")
                    {
                        gs_ljmap = ((TextBox)Det_Repeater.Items[i].FindControl("gs_ljmap")).Text.Trim();
                        gs_ljname = ((TextBox)Det_Repeater.Items[i].FindControl("gs_ljname")).Text.Trim();
                    }

                    if (((TextBox)Det_Repeater.Items[i].FindControl("cfj_mapbh")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("cfj_name")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("cfj_num")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_gxdetal")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjpergs")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjtolgs")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_notedetail")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_ljnum")).Text.Trim() != "")
                    {
                        sqltext = "insert into TBTM_GSDETAIL(gs_detailbh,gs_ljmap,gs_ljname,gs_ljnum,cfj_mapbh,cfj_name,cfj_num,gs_gxdetal,gs_cfjpergs,gs_cfjtolgs,gs_notedetail,realljnum,realcfjnum,realcfjpergs,realcfjtolgs,realnote) values('" + newbh + "','" + gs_ljmap + "','" + gs_ljname + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_ljnum")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("cfj_mapbh")).Text.Trim() + "','" + ((TextBox)Det_Repeater.Items[i].FindControl("cfj_name")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("cfj_num")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("gs_gxdetal")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjpergs")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjtolgs")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("gs_notedetail")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_ljnum")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("cfj_num")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjpergs")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjtolgs")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("realnote")).Text.Trim() + "')";
                        list_sql.Add(sqltext);
                    }
                }
                DBCallCommon.ExecuteTrans(list_sql);
                Response.Redirect("TM_GSFPDETAIL.aspx?action=edit&gs_bh=" + newbh);
            }
            //编辑
            else if (action == "edit")
            {
                bh = Request.QueryString["gs_bh"].ToString().Trim();
                sqltext = "update TBTM_GSMANAGEMENT set gs_zdrid='" + lbuserid.Text.Trim() + "',gs_zdrname='" + lbusername.Text.Trim() + "',gs_zdtime='" + lbzdtime.Text.Trim() + "',gs_jcbh='" + txtjcbh.Text.Trim() + "',gs_jctype='" + txtjctype.Text.Trim() + "',gs_yearmonth='" + yearmonth.Value.Trim() + "',gs_cpname='" + txtcpname.Text.Trim() + "',gs_cpguige='" + txtcpguige.Text.Trim() + "',gs_zongmap='" + txtzongmap.Text.Trim() + "',gs_bjth='" + txtbjmap.Text.Trim() + "',gs_bjname='" + txtbjname.Text.Trim() + "',gs_bjnum=" + CommonFun.ComTryDecimal(txtbjnum.Text.Trim()) + ",gs_bjpergs=" + CommonFun.ComTryDecimal(txtbjpergs.Text.Trim()) + ",gs_bjtotalgs=" + CommonFun.ComTryDecimal(txtbjtolgs.Text.Trim()) + ",gs_realbjnum=" + CommonFun.ComTryDecimal(txtbjnum.Text.Trim()) + ",gs_realbjpergs=" + CommonFun.ComTryDecimal(txtbjpergs.Text.Trim()) + ",gs_realbjtotalgs=" + CommonFun.ComTryDecimal(txtbjtolgs.Text.Trim()) + ",gs_state='0',gs_note='" + txtnote.Text.Trim() + "',gs_perinfo='" + txtperinfo.Text.Trim() + "' where gs_bh='" + bh + "'"; 
                list_sql.Add(sqltext);
                string sqldelete = "delete from TBTM_GSDETAIL where gs_detailbh='" + bh + "'";
                DBCallCommon.ExeSqlText(sqldelete);
                for (int i = 0; i < Det_Repeater.Items.Count; i++)
                {
                    if (((TextBox)Det_Repeater.Items[i].FindControl("gs_ljmap")).Text.Trim() != "")
                    {
                        gs_ljmap = ((TextBox)Det_Repeater.Items[i].FindControl("gs_ljmap")).Text.Trim();
                        gs_ljname = ((TextBox)Det_Repeater.Items[i].FindControl("gs_ljname")).Text.Trim();
                    }

                    if (((TextBox)Det_Repeater.Items[i].FindControl("cfj_mapbh")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("cfj_name")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("cfj_num")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_gxdetal")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjpergs")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjtolgs")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_notedetail")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_ljnum")).Text.Trim() != "")
                    {
                        sqltext = "insert into TBTM_GSDETAIL(gs_detailbh,gs_ljmap,gs_ljname,gs_ljnum,cfj_mapbh,cfj_name,cfj_num,gs_gxdetal,gs_cfjpergs,gs_cfjtolgs,gs_notedetail,realljnum,realcfjnum,realcfjpergs,realcfjtolgs,realnote) values('" + bh + "','" + gs_ljmap + "','" + gs_ljname + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_ljnum")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("cfj_mapbh")).Text.Trim() + "','" + ((TextBox)Det_Repeater.Items[i].FindControl("cfj_name")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("cfj_num")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("gs_gxdetal")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjpergs")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjtolgs")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("gs_notedetail")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_ljnum")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("cfj_num")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjpergs")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjtolgs")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("realnote")).Text.Trim() + "')";
                        list_sql.Add(sqltext);
                    }
                }

                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功!');", true);
                binddata();
                controlpower();
            }
            //结算
            else if (action == "account")
            {
                bh = Request.QueryString["gs_bh"].ToString().Trim();
                sqltext = "update TBTM_GSMANAGEMENT set gs_realbjnum=" + CommonFun.ComTryDecimal(txtbjnum.Text.Trim()) + ",gs_realbjpergs=" + CommonFun.ComTryDecimal(txtbjpergs.Text.Trim()) + ",gs_realbjtotalgs=" + CommonFun.ComTryDecimal(txtbjtolgs.Text.Trim()) + ",gs_state='0',gs_note='" + txtnote.Text.Trim() + "',gs_jsrname='" + jsusername.Text.Trim() + "',gs_jsrid='" + lbjsuserid.Text.Trim() + "',gs_jstime='" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").Trim() + "',gs_perinfo='" + txtperinfo.Text.Trim() + "' where gs_bh='" + bh + "'";
                list_sql.Add(sqltext);
                string sqldelete = "delete from TBTM_GSDETAIL where gs_detailbh='" + bh + "'";
                DBCallCommon.ExeSqlText(sqldelete);
                for (int i = 0; i < Det_Repeater.Items.Count; i++)
                {
                    if (((TextBox)Det_Repeater.Items[i].FindControl("gs_ljmap")).Text.Trim() != "")
                    {
                        gs_ljmap = ((TextBox)Det_Repeater.Items[i].FindControl("gs_ljmap")).Text.Trim();
                        gs_ljname = ((TextBox)Det_Repeater.Items[i].FindControl("gs_ljname")).Text.Trim();
                    }
                    if (((TextBox)Det_Repeater.Items[i].FindControl("cfj_mapbh")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("cfj_name")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("cfj_num")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_gxdetal")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjpergs")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjtolgs")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_notedetail")).Text.Trim() != "" || ((TextBox)Det_Repeater.Items[i].FindControl("gs_ljnum")).Text.Trim() != "")
                    {
                        sqltext = "insert into TBTM_GSDETAIL(gs_detailbh,gs_ljmap,gs_ljname,gs_ljnum,cfj_mapbh,cfj_name,cfj_num,gs_gxdetal,gs_cfjpergs,gs_cfjtolgs,gs_notedetail,realljnum,realcfjnum,realcfjpergs,realcfjtolgs,realnote) values('" + bh + "','" + gs_ljmap + "','" + gs_ljname + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_ljnum")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("cfj_mapbh")).Text.Trim() + "','" + ((TextBox)Det_Repeater.Items[i].FindControl("cfj_name")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("cfj_num")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("gs_gxdetal")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjpergs")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjtolgs")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("gs_notedetail")).Text.Trim() + "'," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("realljnum")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("realcfjnum")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("realcfjpergs")).Text.Trim()) + "," + CommonFun.ComTryDecimal(((TextBox)Det_Repeater.Items[i].FindControl("realcfjtolgs")).Text.Trim()) + ",'" + ((TextBox)Det_Repeater.Items[i].FindControl("realnote")).Text.Trim() + "')";
                        list_sql.Add(sqltext);
                    }
                }
                DBCallCommon.ExecuteTrans(list_sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功!');", true);
                binddata();
                controlpower();
            }
        }
        //确认结算
        protected void btnaccount_click(object sender, EventArgs e)
        {
            bh = Request.QueryString["gs_bh"].ToString().Trim();
            string sql = "update TBTM_GSMANAGEMENT set gs_state='1' where gs_bh='" + bh + "'";
            DBCallCommon.ExeSqlText(sql);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功!');", true);
            binddata();
            controlpower();
        }


        //控件的可用性设置
        private void controlpower()
        {
            action = Request.QueryString["action"].ToString().Trim();
            if (action == "add")
            {
                btnaccount.Visible = false;
                txtrealbjpergs.Enabled = false;
                txtrealbjnum.Enabled = false;
                txtrealbjtotalgs.Enabled = false;
                for (int i = 0; i < Det_Repeater.Items.Count; i++)
                {
                    ((TextBox)Det_Repeater.Items[i].FindControl("realljnum")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("realcfjnum")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("realcfjpergs")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("realcfjtolgs")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("realnote")).Enabled = false;
                }
            }
            else if (action == "edit")
            {
                btnaccount.Visible = false;
                btnloadbase.Visible = false;

                txtrealbjpergs.Enabled = false;
                txtrealbjnum.Enabled = false;
                txtrealbjtotalgs.Enabled = false;
                for (int i = 0; i < Det_Repeater.Items.Count; i++)
                {
                    ((TextBox)Det_Repeater.Items[i].FindControl("realljnum")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("realcfjnum")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("realcfjpergs")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("realcfjtolgs")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("realnote")).Enabled = false;
                }
            }
            else if (action == "account")
            {
                btnsave.Visible = false;
                btnadd.Visible = false;
                delete.Visible = false;
                btnloadbase.Visible = false;

                txtjcbh.Enabled = false;
                txtjctype.Enabled = false;
                yearmonth.Disabled = true;
                txtcpname.Enabled = false;
                txtcpguige.Enabled = false;
                txtzongmap.Enabled = false;
                txtbjname.Enabled = false;
                txtbjmap.Enabled = false;
                txtbjpergs.Enabled = false;
                txtbjnum.Enabled = false;
                txtbjtolgs.Enabled = false;
                txtperinfo.Enabled = false;


                for (int i = 0; i < Det_Repeater.Items.Count; i++)
                {
                    ((TextBox)Det_Repeater.Items[i].FindControl("gs_ljmap")).Attributes["readonly"] = "true";
                    ((TextBox)Det_Repeater.Items[i].FindControl("gs_ljname")).Attributes["readonly"] = "true";
                    ((TextBox)Det_Repeater.Items[i].FindControl("gs_ljnum")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("cfj_mapbh")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("cfj_name")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("cfj_num")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("gs_gxdetal")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjpergs")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("gs_cfjtolgs")).Enabled = false;
                    ((TextBox)Det_Repeater.Items[i].FindControl("gs_notedetail")).Enabled = false;
                }
            }
            else if (action == "view")
            {
                btnaccount.Visible = false;
                btnsave.Visible = false;
                btnadd.Visible = false;
                delete.Visible = false;
                btnloadbase.Visible = false;
            }
        }


        protected void txtjcbh_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            TextBox Tb_meccode = (TextBox)sender;
            if (num > 0)
            {
                string meccode = (sender as TextBox).Text.Trim().Substring(0, num);
                string sqltext = "select * from TBTM_MEC where jc_bh='" + meccode + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (dt.Rows.Count > 0)
                {
                    txtjcbh.Text = dt.Rows[0]["jc_bh"].ToString().Trim();
                    txtjctype.Text = dt.Rows[0]["jc_type"].ToString().Trim();
                    txtperinfo.Text = dt.Rows[0]["jc_containper"].ToString().Trim();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('不存在该编号信息，请重新输入！');", true);
                }
            }
        }

        protected void txtbjmap_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            TextBox Tb_bjid = (TextBox)sender;
            if (num > 0)
            {
                string bjmap = (sender as TextBox).Text.Trim().Substring(0, num);
                string sql = "select * from TBTM_GONGSBASE where bjmap='" + bjmap + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    txtcpname.Text = dt.Rows[0]["cpname"].ToString().Trim();
                    txtcpguige.Text = dt.Rows[0]["cpguige"].ToString().Trim();
                    txtzongmap.Text = dt.Rows[0]["zongmap"].ToString().Trim();
                    txtbjmap.Text = dt.Rows[0]["bjmap"].ToString().Trim();
                    txtbjname.Text = dt.Rows[0]["bjname"].ToString().Trim();
                    txtnote.Text = dt.Rows[0]["notetotal"].ToString().Trim();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('不存在该图号信息，请重新输入！');", true);
                }
            }
        }


        protected void txtbjmapload_TextChanged(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            TextBox Tb_bjidload = (TextBox)sender;
            if (num > 0)
            {
                string bjmapload = (sender as TextBox).Text.Trim().Substring(0, num);
                string sql = "select * from TBTM_GONGSBASE where bjmap='" + bjmapload + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    txtbjmapload.Text = dt.Rows[0]["bjmap"].ToString().Trim();
                }
            }
        }
    }
}
