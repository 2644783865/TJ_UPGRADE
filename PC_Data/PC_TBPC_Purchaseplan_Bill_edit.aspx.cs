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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Purchaseplan_Bill_edit : System.Web.UI.Page
    {
        public DataTable gloabt
        {
            get
            {
                object dt = ViewState["gloabt"];
                return dt == null ? null : (DataTable)dt;
            }
            set
            {
                ViewState["gloabt"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {

                TextBox_pid.Text = Request.QueryString["sheetno"].ToString();
                Label_view.Text = Request.QueryString["state"].ToString();
                inidroplist_view();//初始化视图
                initpage();//初始化页面
                tbpc_purshaseplanbillRepeaterdatabind();//数据绑定
                hesejinshuRepeaterdatabind();
            }
        }
        private DataTable GetDataTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MARID");
            dt.Columns.Add("MARNAME");
            dt.Columns.Add("MARNORM");
            dt.Columns.Add("MARTERIAL");
            dt.Columns.Add("GUOBIAO");
            dt.Columns.Add("NUNIT");
            dt.Columns.Add("NUM", typeof(double));
            return dt;
        }
        private DataTable SetDataTable()
        {
            string sqltext = "";
            DataTable gdt = GetDataTable();
            // DataTable dt = GetDataTable();
            // sqltext = "SELECT SQ_MARID AS MARID,SQ_NAME AS MARNAME,SQ_GUIGE AS MARNORM,SQ_CAIZHI AS MARTERIAL,SQ_GUOBIAO AS GUOBIAO,"+
            //           "SQ_PCODE AS PCODE,SQ_LENGTH AS LENGTH,SQ_WIDTH AS WIDTH,SQ_STID AS STID,SQ_STNAME AS STNAME,SQ_CWID AS CWID,"+
            //           "SQ_CWNAME AS CWNAME,SQ_UNIT AS NUNIT,SQ_NUM AS NUM,SQ_SPRICE AS SPRICE,SQ_INPRICE AS INPRICE FROM TBWS_SQASM WHERE SQ_PTCODE=''";
            // dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            // if (dt.Rows.Count > 0)
            // {
            //     gdt.Merge(dt);
            //     gdt.AcceptChanges();
            // }
            // sqltext = "SELECT SQ_MARID AS MARID,SQ_NAME AS MARNAME,SQ_GUIGE AS MARNORM,SQ_CAIZHI AS MARTERIAL,SQ_GUOBIAO AS GUOBIAO," +
            //          "SQ_PCODE AS PCODE,SQ_LENGTH AS LENGTH,SQ_WIDTH AS WIDTH,SQ_STID AS STID,SQ_STNAME AS STNAME,SQ_CWID AS CWID,"+
            //          "SQ_CWNAME AS CWNAME,SQ_UNIT AS NUNIT,SQ_NUM AS NUM,SQ_SPRICE AS SPRICE,SQ_INPRICE AS INPRICE FROM TBWS_SQBZJ WHERE SQ_PTCODE=''";
            // dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            // if (dt.Rows.Count > 0)
            // {
            //     gdt.Merge(dt);
            //     gdt.AcceptChanges();
            // }
            // sqltext = "SELECT SQ_MARID AS MARID,SQ_NAME AS MARNAME,SQ_GUIGE AS MARNORM,SQ_CAIZHI AS MARTERIAL,SQ_GUOBIAO AS GUOBIAO," +
            //           "SQ_PCODE AS PCODE,SQ_LENGTH AS LENGTH,SQ_WIDTH AS WIDTH,SQ_STID AS STID,SQ_STNAME AS STNAME,SQ_CWID AS CWID,"+
            //           "SQ_CWNAME AS CWNAME,SQ_UNIT AS NUNIT,SQ_NUM AS NUM,SQ_SPRICE AS SPRICE,SQ_INPRICE AS INPRICE FROM TBWS_SQELC WHERE SQ_PTCODE=''";
            // dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            // if (dt.Rows.Count > 0)
            // {
            //     gdt.Merge(dt);
            //     gdt.AcceptChanges();
            // }
            // sqltext = "SELECT SQ_MARID AS MARID,SQ_NAME AS MARNAME,SQ_GUIGE AS MARNORM,SQ_CAIZHI AS MARTERIAL,SQ_GUOBIAO AS GUOBIAO," +
            //           "SQ_PCODE AS PCODE,SQ_LENGTH AS LENGTH,SQ_WIDTH AS WIDTH,SQ_STID AS STID,SQ_STNAME AS STNAME,SQ_CWID AS CWID,"+
            //           "SQ_CWNAME AS CWNAME,SQ_UNIT AS NUNIT,SQ_NUM AS NUM,SQ_SPRICE AS SPRICE,SQ_INPRICE AS INPRICE FROM TBWS_SQHST WHERE SQ_PTCODE=''";
            // dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            // if (dt.Rows.Count > 0)
            // {
            //     gdt.Merge(dt);
            //     gdt.AcceptChanges();
            // }
            //sqltext = "SELECT SQ_MARID AS MARID,SQ_NAME AS MARNAME,SQ_GUIGE AS MARNORM,SQ_CAIZHI AS MARTERIAL,SQ_GUOBIAO AS GUOBIAO," +
            //           "SQ_PCODE AS PCODE,SQ_LENGTH AS LENGTH,SQ_WIDTH AS WIDTH,SQ_STID AS STID,SQ_STNAME AS STNAME,SQ_CWID AS CWID,"+
            //           "SQ_CWNAME AS CWNAME,SQ_UNIT AS NUNIT,SQ_NUM AS NUM,SQ_SPRICE AS SPRICE,SQ_INPRICE AS INPRICE FROM TBWS_SQPTO WHERE SQ_PTCODE=''";
            // dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            // if (dt.Rows.Count > 0)
            // {
            //     gdt.Merge(dt);
            //     gdt.AcceptChanges();
            // }
            // sqltext = "SELECT SQ_MARID AS MARID,SQ_NAME AS MARNAME,SQ_GUIGE AS MARNORM,SQ_CAIZHI AS MARTERIAL,SQ_GUOBIAO AS GUOBIAO," +
            //           "SQ_PCODE AS PCODE,SQ_LENGTH AS LENGTH,SQ_WIDTH AS WIDTH,SQ_STID AS STID,SQ_STNAME AS STNAME,SQ_CWID AS CWID,"+
            //           "SQ_CWNAME AS CWNAME,SQ_UNIT AS NUNIT,SQ_NUM AS NUM,SQ_SPRICE AS SPRICE,SQ_INPRICE AS INPRICE FROM TBWS_SQSTL WHERE SQ_PTCODE=''";
            // dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            // if (dt.Rows.Count > 0)
            // {
            //     gdt.Merge(dt);
            //     gdt.AcceptChanges();
            // }
            //sqltext = "SELECT SQ_MARID AS MARID,SQ_NAME AS MARNAME,SQ_GUIGE AS MARNORM,SQ_CAIZHI AS MARTERIAL,SQ_GUOBIAO AS GUOBIAO," +
            //           "SQ_PCODE AS PCODE,SQ_LENGTH AS LENGTH,SQ_WIDTH AS WIDTH,SQ_STID AS STID,SQ_STNAME AS STNAME,SQ_CWID AS CWID,"+
            //           "SQ_CWNAME AS CWNAME,SQ_UNIT AS NUNIT,SQ_NUM AS NUM,SQ_SPRICE AS SPRICE,SQ_INPRICE AS INPRICE FROM TBWS_SQSVN WHERE SQ_PTCODE=''";
            // dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            // if (dt.Rows.Count > 0)
            // {
            //     gdt.Merge(dt);
            //     gdt.AcceptChanges();
            // }
            // sqltext = "SELECT SQ_MARID AS MARID,SQ_NAME AS MARNAME,SQ_GUIGE AS MARNORM,SQ_CAIZHI AS MARTERIAL,SQ_GUOBIAO AS GUOBIAO," +
            //           "SQ_PCODE AS PCODE,SQ_LENGTH AS LENGTH,SQ_WIDTH AS WIDTH,SQ_STID AS STID,SQ_STNAME AS STNAME,SQ_CWID AS CWID,"+
            //           "SQ_CWNAME AS CWNAME,SQ_UNIT AS NUNIT,SQ_NUM AS NUM,SQ_SPRICE AS SPRICE,SQ_INPRICE AS INPRICE FROM TBWS_SQYMI WHERE SQ_PTCODE=''";
            // dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            // if (dt.Rows.Count > 0)
            // {
            //     gdt.Merge(dt);
            //     gdt.AcceptChanges();
            // }
            //sqltext = "select  SQ_CODE AS , SQ_MARID, SQ_MARNAME, SQ_ATTRIBUTE, SQ_GB, SQ_STANDARD, SQ_LENGTH, SQ_WIDTH, " +
            //          "SQ_LOTNUM, SQ_PMODE, SQ_PTC, SQ_WAREHOUSEID, SQ_WAREHOUSENAME, SQ_POSITIONID, " +
            //          "SQ_POSITIONNAME, SQ_UNIT, SQ_NUM, SQ_UNITPRICE, SQ_AMOUNT, SQ_INCODE, SQ_OUTCODE, SQ_NOTE " +
            //          "from TBWS_STORAGE where SQ_PTCODE=''";
            sqltext = "select  SQ_MARID AS MARID, SQ_MARNAME AS MARNAME, SQ_STANDARD AS MARNORM, SQ_GB AS GUOBIAO,SQ_ATTRIBUTE  AS MARTERIAL," +
                      "SQ_UNIT AS NUNIT, SQ_NUM AS NUM from TBWS_STORAGE where SQ_PTC=''";
            gdt = DBCallCommon.GetDTUsingSqlText(sqltext);
            return gdt;
        }
        private void initpage()
        {
            //Tb_shijian.Text = DateTime.Now.ToShortDateString();
            gloabt = SetDataTable();
            if (Label_view.Text.ToString() == "000")
            {
                Panel_view.Visible = false;
                Panel_baocun.Visible = true;
            }
            else
            {
                Panel_view.Visible = true;
                Panel_baocun.Visible = false;
            }
            string sqltext = "SELECT distinct PUR_PJID,PUR_PJNAME,PUR_ENGID,PUR_ENGNAME FROM TBPC_PURCHASEPLAN WHERE PUR_PCODE='" + TextBox_pid.Text.ToString() + "'";
            DataTable gdt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (gdt.Rows.Count > 0)//有项目数据需要汇总
            {
                tb_pj.Text = gdt.Rows[0]["PUR_PJNAME"].ToString();
                tb_pjid.Text = gdt.Rows[0]["PUR_PJID"].ToString();
                tb_eng.Text = gdt.Rows[0]["PUR_ENGNAME"].ToString();
                tb_engid.Text = gdt.Rows[0]["PUR_ENGID"].ToString();
            }
            sqltext = "SELECT PR_REVIEWA,PR_REVIEWANM,PR_REVIEWATIME,PR_NOTE,PR_REVIEWB,PR_REVIEWBNM,PR_REVIEWBTIME,PR_REVIEWBADVC FROM TBPC_PCHSPLANRVW WHERE PR_SHEETNO='" + TextBox_pid.Text.ToString() + "'";
            gdt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (gdt.Rows.Count > 0)//有项目数据需要汇总
            {
                Tb_shijian.Text = gdt.Rows[0]["PR_REVIEWATIME"].ToString();
                tb_note.Text = gdt.Rows[0]["PR_NOTE"].ToString();
                Tb_shenpiren.Text = gdt.Rows[0]["PR_REVIEWBNM"].ToString();
                Tb_shenpirenid.Text = gdt.Rows[0]["PR_REVIEWB"].ToString();
                TextBox_shenheren.Text = gdt.Rows[0]["PR_REVIEWBNM"].ToString();
                if (Label_view.Text == "100")
                {
                    TextBox_shenhesj.Text = gdt.Rows[0]["PR_REVIEWBTIME"].ToString();
                    suggestion.Text = gdt.Rows[0]["PR_REVIEWBADVC"].ToString();
                    radcon_disagree.Checked = true;
                }
            }
        }
        protected void inidroplist_view()//初始化视图为“待保存”
        {
            DropDownListview.Items[0].Selected = true;
        }
        private void Initcheckboxandnum()//待保存，自动扣减
        {
            for (int i = 0; i < tbpc_purshaseplanbillRepeater.Items.Count; i++)
            {
                RepeaterItem Reitem = tbpc_purshaseplanbillRepeater.Items[i];
                CheckBox cbx = Reitem.FindControl("CKBOX_USEKCALTER") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    double NEDDNUM = Convert.ToDouble(((Label)Reitem.FindControl("PUR_NEDDNUM")).Text);
                    double KCNUM = Convert.ToDouble(((Label)Reitem.FindControl("PUR_KCNUM")).Text);
                    if (KCNUM <= 0)//不存在库存量不可编辑
                    {
                        cbx.Enabled = false;
                        cbx.Checked = false;
                        ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Enabled = false;
                        ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text = "0";
                    }
                    else//存在库存量可编辑
                    {
                        cbx.Enabled = true;
                        if ((KCNUM - NEDDNUM) >= 0)
                        {
                            ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text = Convert.ToString(NEDDNUM);
                        }
                        else
                        {
                            ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text = Convert.ToString(KCNUM);
                        }
                    }
                    double USEKCNUM = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text);
                    double PLANNUM = NEDDNUM - USEKCNUM;
                    ((TextBox)Reitem.FindControl("PUR_PLANNUM")).Text = Convert.ToString(PLANNUM);
                }
            }
        }
        void Pager_PageChanged(int pageNumber)
        {
            tbpc_purshaseplanbillRepeaterdatabind();
            hesejinshuRepeaterdatabind();
        }

        private void tbpc_purshaseplanbillRepeaterdatabind()
        {
            string sqltext1 = "";
            sqltext1 = "SELECT a.PUR_USTNUM AS PUR_USEKCNUM,PUR_PLANNUM=a.PUR_NUM-a.PUR_USTNUM,a.PUR_PTCODE,a.PUR_PJID,a.PUR_ENGID,a.PUR_MARID,a.PUR_MARNAME,a.PUR_MARNORM,a.PUR_MARTERIAL,a.PUR_GUOBIAO,a.PUR_FIXEDSIZE,a.PUR_NUM AS PUR_NEDDNUM ,a.PUR_NUNIT ,a.PUR_STATE,a.PUR_NOTE FROM TBPC_PURCHASEPLAN as a WHERE a.PUR_PRONODE='0' AND a.PUR_PCODE='" + TextBox_pid.Text.ToString() + "' AND  PUR_MARID not like '01.07%' ORDER BY a.PUR_PTCODE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext1);
            DataView dv = dt.DefaultView;
            if (Label_view.Text == "000")//未提交
            {
                if (DropDownListview.SelectedValue.ToString() == "0")
                {
                    //sqltext1 = "SELECT a.PUR_USTNUM AS PUR_USEKCNUM,PUR_PLANNUM=a.PUR_NUM-a.PUR_USTNUM,a.PUR_PTCODE,a.PUR_PJID,a.PUR_ENGID,a.PUR_MARID,a.PUR_MARNAME,a.PUR_MARNORM,a.PUR_MARTERIAL,a.PUR_GUOBIAO,a.PUR_FIXEDSIZE,a.PUR_NUM AS PUR_NEDDNUM ,a.PUR_NUNIT ,a.PUR_STATE,a.PUR_NOTE FROM TBPC_PURCHASEPLAN as a WHERE a.PUR_PRONODE='0' AND a.PUR_PCODE='" + TextBox_pid.Text.ToString() + "'AND a.PUR_STATE='" + DropDownListview.SelectedValue.ToString() + "' ORDER BY a.PUR_PTCODE";
                    dv.RowFilter = "PUR_STATE='" + DropDownListview.SelectedValue.ToString() + "'";
                    tbpc_purshaseplanbillRepeater.DataSource = dv;
                    tbpc_purshaseplanbillRepeater.DataBind();
                    chechnullVCK();
                    Initcheckboxandnum();//第一次加载数据，初始化页面数据 
                }
                else
                {
                    //sqltext1 = "SELECT a.PUR_USTNUM as PUR_USEKCNUM,PUR_PLANNUM=a.PUR_NUM-a.PUR_USTNUM,a.PUR_PTCODE,a.PUR_PJID,a.PUR_ENGID,a.PUR_MARID,a.PUR_MARNAME,a.PUR_MARNORM,a.PUR_MARTERIAL,a.PUR_GUOBIAO,a.PUR_FIXEDSIZE,a.PUR_NUM AS PUR_NEDDNUM ,a.PUR_NUNIT ,a.PUR_STATE,a.PUR_NOTE FROM TBPC_PURCHASEPLAN as a WHERE a.PUR_PRONODE='0' AND a.PUR_PCODE='" + TextBox_pid.Text.ToString() + "'AND a.PUR_STATE='" + DropDownListview.SelectedValue.ToString() + "' ORDER BY a.PUR_PTCODE";
                    dv.RowFilter = "PUR_STATE='" + DropDownListview.SelectedValue.ToString() + "'";
                    tbpc_purshaseplanbillRepeater.DataSource = dv;
                    tbpc_purshaseplanbillRepeater.DataBind();
                    chechnullVCK();
                    Initcheckbox();//初始化checkbox
                }
            }
            else//驳回100
            {
                if (RadioButton_disagree.Checked)
                {
                    //sqltext1 = "SELECT a.PUR_USTNUM as PUR_USEKCNUM,PUR_PLANNUM=a.PUR_NUM-a.PUR_USTNUM,a.PUR_PTCODE,a.PUR_PJID,a.PUR_ENGID,a.PUR_MARID,a.PUR_MARNAME,a.PUR_MARNORM,a.PUR_MARTERIAL,a.PUR_GUOBIAO,a.PUR_FIXEDSIZE,a.PUR_NUM AS PUR_NEDDNUM ,a.PUR_NUNIT ,a.PUR_STATE,a.PUR_NOTE FROM TBPC_PURCHASEPLAN as a WHERE a.PUR_PRONODE='0' AND a.PUR_PCODE='" + TextBox_pid.Text.ToString() + "'AND a.PUR_STATE='1' ORDER BY a.PUR_PTCODE";
                    dv.RowFilter = "PUR_STATE='1'";

                }
                else
                {
                    if (RadioButton_agree.Checked)
                    {
                        //sqltext1 = "SELECT a.PUR_USTNUM as PUR_USEKCNUM,PUR_PLANNUM=a.PUR_NUM-a.PUR_USTNUM,a.PUR_PTCODE,a.PUR_PJID,a.PUR_ENGID,a.PUR_MARID,a.PUR_MARNAME,a.PUR_MARNORM,a.PUR_MARTERIAL,a.PUR_GUOBIAO,a.PUR_FIXEDSIZE,a.PUR_NUM AS PUR_NEDDNUM ,a.PUR_NUNIT ,a.PUR_STATE,a.PUR_NOTE FROM TBPC_PURCHASEPLAN as a  WHERE a.PUR_PRONODE='0' AND a.PUR_PCODE='" + TextBox_pid.Text.ToString() + "'AND a.PUR_STATE='2' ORDER BY a.PUR_PTCODE";
                        dv.RowFilter = "PUR_STATE='2'";
                    }
                    else
                    {
                        //sqltext1 = "SELECT a.PUR_USTNUM as PUR_USEKCNUM,PUR_PLANNUM=a.PUR_NUM-a.PUR_USTNUM,a.PUR_PTCODE,a.PUR_PJID,a.PUR_ENGID,a.PUR_MARID,a.PUR_MARNAME,a.PUR_MARNORM,a.PUR_MARTERIAL,a.PUR_GUOBIAO,a.PUR_FIXEDSIZE,a.PUR_NUM AS PUR_NEDDNUM ,a.PUR_NUNIT ,a.PUR_STATE,a.PUR_NOTE FROM TBPC_PURCHASEPLAN as a  WHERE a.PUR_PRONODE='0' AND a.PUR_PCODE='" + TextBox_pid.Text.ToString() + "'AND (a.PUR_STATE='1' OR a.PUR_STATE='2') ORDER BY a.PUR_PTCODE";
                        dv.RowFilter = "PUR_STATE='1' OR PUR_STATE='2'";
                    }
                }
                tbpc_purshaseplanbillRepeater.DataSource = dv;
                tbpc_purshaseplanbillRepeater.DataBind();
                chechnullVCK();
                Initcheckbox();//初始化checkbox
            }
        }
        private void hesejinshuRepeaterdatabind()
        {
            string sqltext = "";
            sqltext="SELECT  PUR_PTCODE, PUR_MARID, PUR_MARNAME, PUR_MARNORM, "+
                            "PUR_MARTERIAL, PUR_GUOBIAO,PUR_NUM,PUR_WEIGHT, PUR_NUNIT, PUR_USTNUM,"+
                            "PUR_USTWEIGHT, PUR_RPNUM,PUR_RPWEIGHT,PUR_PRONODE, PUR_STATE, PUR_NOTE "+
                    "FROM  TBPC_PURCHASEPLAN  "+
                    "where PUR_PCODE='" + TextBox_pid.Text.ToString() + "' and PUR_MARID like '01.07%' ORDER BY PUR_PTCODE";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DataView dv = dt.DefaultView;
            if (Label_view.Text == "000")
            {
                dv.RowFilter = "PUR_PRONODE='0' and PUR_STATE='" + DropDownListview.SelectedValue.ToString() + "'";
                //if (DropDownListview.SelectedValue.ToString() == "0")
                //{
                //    dv.RowFilter = "PUR_PRONODE='0' and PUR_STATE='" + DropDownListview.SelectedValue.ToString() + "'";
                //}
                //else
                //{
                //    sqltext1 = "SELECT a.PUR_USTNUM as PUR_USEKCNUM,PUR_PLANNUM=a.PUR_NUM-a.PUR_USTNUM,a.PUR_PTCODE,a.PUR_PJID,a.PUR_ENGID,a.PUR_MARID,a.PUR_MARNAME,a.PUR_MARNORM,a.PUR_MARTERIAL,a.PUR_GUOBIAO,a.PUR_FIXEDSIZE,a.PUR_NUM AS PUR_NEDDNUM ,a.PUR_NUNIT ,a.PUR_STATE,a.PUR_NOTE FROM TBPC_PURCHASEPLAN as a WHERE a.PUR_PRONODE='0' AND a.PUR_PCODE='" + TextBox_pid.Text.ToString() + "'AND a.PUR_STATE='" + DropDownListview.SelectedValue.ToString() + "' ORDER BY a.PUR_PTCODE";
                //    dv.RowFilter = "PUR_PRONODE='0' and PUR_STATE='" + DropDownListview.SelectedValue.ToString() + "'";
                //}
            }
            else//驳回100
            {
                if (RadioButton_disagree.Checked)
                {
                    dv.RowFilter = "PUR_PRONODE='0' AND PUR_PCODE='" + TextBox_pid.Text.ToString() + "'AND PUR_STATE='1'";

                }
                else
                {
                    if (RadioButton_agree.Checked)
                    {
                        dv.RowFilter = "PUR_PRONODE='0' AND PUR_PCODE='" + TextBox_pid.Text.ToString() + "'AND PUR_STATE='2'";
                    }
                    else
                    {
                        dv.RowFilter = "a.PUR_PRONODE='0' AND a.PUR_PCODE='" + TextBox_pid.Text.ToString() + "'AND (a.PUR_STATE='1' OR a.PUR_STATE='2') ";
                    }
                }
            }
            dt = dv.ToTable();
            hesejinshuRepeater.DataSource = dv;
            hesejinshuRepeater.DataBind();
        }
        private void Initcheckbox()//初始化checkbox状态，最大可用量信息为0，则本行数据不可更改
        {
            for (int i = 0; i < tbpc_purshaseplanbillRepeater.Items.Count; i++)
            {
                RepeaterItem Reitem = tbpc_purshaseplanbillRepeater.Items[i];
                CheckBox cbx = Reitem.FindControl("CKBOX_USEKCALTER") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    double PUR_MAXUSENUM = Convert.ToDouble(((Label)Reitem.FindControl("PUR_MAXUSENUM")).Text);
                    if (PUR_MAXUSENUM <= 0)//不存在库存量不可编辑
                    {
                        cbx.Enabled = false;
                        cbx.Checked = false;
                    }
                    else//存在库存量可编辑
                    {
                        cbx.Enabled = true;
                    }
                }
            }
        }
        private void chechnullVCK()//库存中不存在该材料是将“null”改为“0”
        {
            RepeaterItem Reitem;
            for (int i = 0; i < tbpc_purshaseplanbillRepeater.Items.Count; i++)
            {
                Reitem = tbpc_purshaseplanbillRepeater.Items[i];
                if (((Label)Reitem.FindControl("PUR_KCNUM")).Text.Replace(" ", "") == "" || ((Label)Reitem.FindControl("PUR_KCNUM")).Text.Replace(" ", "") == DBNull.Value.ToString())
                {
                    ((Label)Reitem.FindControl("PUR_KCNUM")).Text = "0";
                    ((Label)Reitem.FindControl("PUR_MAXUSENUM")).Text = "0";
                }
            }
        }
        protected void Textchanged(object sender, EventArgs e)
        {
            TextBox tb = (TextBox)sender;//定义TextBox
            RepeaterItem Reitem = (RepeaterItem)tb.Parent;
            if (((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text.Replace(" ", "") == "")
            {
                ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text = "0";
            }
            double nowmaxusenumstr = Convert.ToDouble(((Label)Reitem.FindControl("PUR_MAXUSENUM")).Text);
            double NEDDNUM = Convert.ToDouble(((Label)Reitem.FindControl("PUR_NEDDNUM")).Text);
            double KCNUM = Convert.ToDouble(((Label)Reitem.FindControl("PUR_KCNUM")).Text);//提交以后才变化
            double USEKCNUM = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text);
            if (USEKCNUM > nowmaxusenumstr)//输入使用库存量大于最大可使用量
            {
                rightornot.Text = "0";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('使用库存量大于最大可使用量，请更正！');", true);
                ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).BackColor = System.Drawing.Color.Red;
                ((Label)Reitem.FindControl("PUR_NEDDNUM")).BackColor = System.Drawing.Color.FromArgb(255, 255, 204);
                ((Label)Reitem.FindControl("PUR_MAXUSENUM")).BackColor = System.Drawing.Color.Red;
                ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Focus();
            }
            else
            {
                if (USEKCNUM > NEDDNUM)//输入使用库存量大于实际需要
                {
                    rightornot.Text = "0";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('使用库存量大于实际需求量，请更正！');", true);
                    ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).BackColor = System.Drawing.Color.Red;
                    ((Label)Reitem.FindControl("PUR_NEDDNUM")).BackColor = System.Drawing.Color.Red;
                    ((Label)Reitem.FindControl("PUR_MAXUSENUM")).BackColor = System.Drawing.Color.FromArgb(255, 255, 204);
                    ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Focus();
                }
                else//输入正确 
                {
                    double PLANNUM = NEDDNUM - USEKCNUM;
                    ((TextBox)Reitem.FindControl("PUR_PLANNUM")).Text = Convert.ToString(PLANNUM);
                    ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).BackColor = System.Drawing.Color.Yellow;
                    ((Label)Reitem.FindControl("PUR_MAXUSENUM")).BackColor = System.Drawing.Color.FromArgb(255, 255, 204);
                    ((Label)Reitem.FindControl("PUR_NEDDNUM")).BackColor = System.Drawing.Color.FromArgb(255, 255, 204);
                    rightornot.Text = "1";
                    Labelerror.Visible = false;
                }
            }
        }
        protected void CheckedChanged(object sender, EventArgs e)//当checkbox选中状态改变时，使用库存单元格状态改变
        {
            Labelerror.Visible = false;
            CheckBox cbx = (CheckBox)sender;//定义checkbox
            RepeaterItem Reitem = (RepeaterItem)cbx.Parent;
            if (cbx.Checked)//选中时
            {
                ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Enabled = true;
                ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).BackColor = System.Drawing.Color.Yellow;
                ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Focus();
            }
            else//没有选中时
            {
                ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Enabled = false;
                ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).BackColor = System.Drawing.Color.FromArgb(255, 255, 204);
                ((Label)Reitem.FindControl("PUR_KCNUM")).BackColor = System.Drawing.Color.FromArgb(255, 255, 204);
                ((Label)Reitem.FindControl("PUR_NEDDNUM")).BackColor = System.Drawing.Color.FromArgb(255, 255, 204);
                double NEDDNUM = Convert.ToDouble(((Label)Reitem.FindControl("PUR_NEDDNUM")).Text);
                double KCNUM = Convert.ToDouble(((Label)Reitem.FindControl("PUR_KCNUM")).Text);
                if ((KCNUM - NEDDNUM) >= 0)
                {
                    ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text = Convert.ToString(NEDDNUM);
                }
                else
                {
                    ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text = Convert.ToString(KCNUM);
                }
                double USEKCNUM = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text);
                double PLANNUM = NEDDNUM - USEKCNUM;
                ((TextBox)Reitem.FindControl("PUR_PLANNUM")).Text = Convert.ToString(PLANNUM);
            }
        }

        protected void DropDownListview_SelectedIndexChanged(object sender, EventArgs e)//视图下拉框绑定
        {
            tbpc_purshaseplanbillRepeaterdatabind();
            hesejinshuRepeaterdatabind();

        }
        protected void RadioButton_disagree_CheckedChanged(object sender, EventArgs e)
        {
            tbpc_purshaseplanbillRepeaterdatabind();
            hesejinshuRepeaterdatabind();

        }
        protected void RadioButton_agree_CheckedChanged(object sender, EventArgs e)
        {
            tbpc_purshaseplanbillRepeaterdatabind();
            hesejinshuRepeaterdatabind();

        }
        protected void RadioButton_all_CheckedChanged(object sender, EventArgs e)
        {
            tbpc_purshaseplanbillRepeaterdatabind();
            hesejinshuRepeaterdatabind();

        }
        //protected void AlterVirtualKC(RepeaterItem Reitem)//改变该行对应材料的虚拟库存数量
        //{
        //    double virtualKC;
        //    string sqltext;
        //    double KCNUM = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_KCNUM")).Text);
        //    double USEKCNUM = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text);
        //    virtualKC = KCNUM + USEKCNUM;
        //    sqltext = "update TBWS_SQYMI SET SQ_VNUM='" + virtualKC + "'WHERE SQ_MARID='" + Convert.ToString(((Label)Reitem.FindControl("PUR_MARID")).Text).Trim() + "'";
        //    DBCallCommon.ExeSqlText(sqltext);//执行操作
        //    repeaterdatabind();
        //}
        protected void tijiao_Click(object sender, EventArgs e)
        {
            Labelerror.Visible = true;
            if (rightornot.Text == "0")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据错误，保存失败，请更正！');", true);
                return;
            }
            else
            {
                Updatetbpc_purchaseplanandtbpc();//保存数据
                changevirtualKC1();//更新虚拟库存量
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);
                //finish.Enabled = true;
            }
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_check_list.aspx");
            //repeaterdatabind();//数据绑定
        }
       
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_check_list.aspx");
        }
        //提交
        protected void finish_Click(object sender, EventArgs e)
        {
            string sqltext1;
            sqltext1 = "UPDATE TBPC_PCHSPLANRVW SET PR_STATE='001' WHERE PR_SHEETNO='" + TextBox_pid.Text.ToString() + "'";
            DBCallCommon.ExeSqlText(sqltext1);
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_check_list.aspx");//返回到主件面
        }
        //保存数据
        private void Updatetbpc_purchaseplanandtbpc()
        {
            Updatetbpc_purchaseplan();//更新采购计划表
        }
        private void Updatetbpc_purchaseplan()
        {
            string sqltext;
            SqlCommand sqlCmd = new SqlCommand();
            SqlConnection sqlConn = new SqlConnection();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            sqlConn.Open();
            sqlCmd.Connection = sqlConn;
            if (Label_view.Text == "000")
            {
                foreach (RepeaterItem Reitem in tbpc_purshaseplanbillRepeater.Items)
                {
                    sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_USTNUM=@PUR_USTNUM,PUR_RPNUM=@PUR_PLANNUM ,PUR_VICLERK=@PUR_VICLERK,PUR_STATE=@PUR_STATE WHERE PUR_PTCODE=@PUR_PTCODE AND PUR_PRONODE='0' AND PUR_PCODE=@PUR_PCODE";
                    sqlCmd.CommandText = sqltext;
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddWithValue("@PUR_PTCODE", ((Label)Reitem.FindControl("PUR_PTCODE")).Text.Replace(" ", ""));
                    sqlCmd.Parameters.AddWithValue("@PUR_USTNUM", ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text.Replace(" ", ""));
                    sqlCmd.Parameters.AddWithValue("@PUR_VICLERK", TextBoxexecutor.Text.ToString());
                    sqlCmd.Parameters.AddWithValue("@PUR_PLANNUM", ((TextBox)Reitem.FindControl("PUR_PLANNUM")).Text.Replace(" ", ""));
                    sqlCmd.Parameters.AddWithValue("@PUR_PCODE", TextBox_pid.Text.ToString());
                    sqlCmd.Parameters.AddWithValue("@PUR_STATE", '1');
                    int rowsnum = sqlCmd.ExecuteNonQuery();
                }
            }
            else//100
            {
                foreach (RepeaterItem Reitem in tbpc_purshaseplanbillRepeater.Items)
                {
                    sqltext = "UPDATE TBPC_PURCHASEPLAN SET PUR_USTNUM=@PUR_USTNUM,PUR_RPNUM=@PUR_PLANNUM WHERE PUR_PTCODE=@PUR_PTCODE AND PUR_PRONODE='0'";
                    sqlCmd.CommandText = sqltext;
                    sqlCmd.Parameters.Clear();
                    sqlCmd.Parameters.AddWithValue("@PUR_PTCODE", ((Label)Reitem.FindControl("PUR_PTCODE")).Text.Replace(" ", ""));
                    sqlCmd.Parameters.AddWithValue("@PUR_USTNUM", ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text.Replace(" ", ""));
                    sqlCmd.Parameters.AddWithValue("@PUR_PLANNUM", ((TextBox)Reitem.FindControl("PUR_PLANNUM")).Text.Replace(" ", ""));
                    int rowsnum = sqlCmd.ExecuteNonQuery();
                }
            }
            DBCallCommon.closeConn(sqlConn);
        }
        //protected void changevirtualKC1()
        //{
        //    RepeaterItem Reitem;
        //    DataTable dt = new DataTable();
        //    dt = DBCallCommon.GetDTUsingSqlText("SELECT * FROM TBWS_WAREHOUSEINFO");
        //    DataView dvtab = dt.DefaultView;
        //    DataView dvglob = gloabt.DefaultView;
        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < tbpc_purshaseplanbillRepeater.Items.Count; i++)
        //        {
        //            Reitem = tbpc_purshaseplanbillRepeater.Items[i];
        //            double virtualKC;
        //            string tablename = "";
        //            string sqltext1 = "";
        //            string sqltext2 = "";
        //            string sqltext3 = "";
        //            double PUR_MAXUSENUM = Convert.ToDouble(((Label)Reitem.FindControl("PUR_MAXUSENUM")).Text);
        //            double USEKCNUM = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text);
        //            string marid = ((Label)Reitem.FindControl("PUR_MARID")).Text;
        //            string marname = ((Label)Reitem.FindControl("PUR_MARNAME")).Text;
        //            string guige = ((Label)Reitem.FindControl("PUR_MARNORM")).Text;
        //            string caizhi = ((Label)Reitem.FindControl("PUR_MARTERIAL")).Text;
        //            string guobiao = ((Label)Reitem.FindControl("PUR_GUOBIAO")).Text;
        //            string kcnum = ((Label)Reitem.FindControl("PUR_KCNUM")).Text;
        //            string unit = ((Label)Reitem.FindControl("PUR_NUNIT")).Text;
        //            string ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text;
        //            string stid = ((Label)Reitem.FindControl("PUR_STID")).Text;
        //            string state = "0";
        //            string usenum = ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text;
        //            string stdate = DateTime.Now.ToString("yyyy-MM-dd");
        //            string pcode = "";
        //            string length = "";
        //            string width = "";
        //            string stname = "";
        //            string cwid = "";
        //            string cwname = "";
        //            string sprice = "";
        //            string inprice = "";
        //            virtualKC = PUR_MAXUSENUM - USEKCNUM;
        //            //sqltext = "update TBWS_SQYMI SET SQ_VNUM='" + virtualKC + "'WHERE SQ_MARID='" + Convert.ToString(((Label)Reitem.FindControl("PUR_MARID")).Text).Trim() + "'";
        //            //DBCallCommon.ExeSqlText(sqltext);//执行操作
        //            if (Label_view.Text == "000" && DropDownListview.SelectedValue.ToString() == "0")
        //            {
        //                if (USEKCNUM > 0)
        //                {
        //                    sqltext1 = "insert into TBPC_YUMARUSAGE(PYU_PJID,PYU_PJNAME,PYU_ENGID,PYU_ENGNAME,PYU_PTCODE,PYU_STID,PYU_STATE,PYU_NUM,PYU_STDATE) values('" + tb_pjid.Text + "','" + tb_pj.Text + "','" + tb_engid.Text + "','" + tb_eng.Text + "','" + ptcode + "','" + stid + "','" + state + "','" + usenum + "','" + stdate + "')";
        //                    DBCallCommon.ExeSqlText(sqltext1);
        //                    dvtab.RowFilter = "WS_ID='" + ((Label)Reitem.FindControl("PUR_STID")).Text + "'";
        //                    tablename = dvtab.ToTable().Rows[0]["WS_TABLENAME"].ToString();
        //                    dvglob.RowFilter = "MARID='" + marid + "' AND MARNAME='" + marname + "' AND MARNORM='" + guige + "' AND MARTERIAL='" + caizhi + "' AND GUOBIAO='" + guobiao + "' AND STID='" + stid + "' AND NUNIT='" + unit + "'";
        //                    pcode = dvglob.ToTable().Rows[0]["PCODE"].ToString();
        //                    length = dvglob.ToTable().Rows[0]["LENGTH"].ToString();
        //                    width = dvglob.ToTable().Rows[0]["WIDTH"].ToString();
        //                    stname = dvglob.ToTable().Rows[0]["STNAME"].ToString();
        //                    cwid = dvglob.ToTable().Rows[0]["CWID"].ToString();
        //                    cwname = dvglob.ToTable().Rows[0]["CWNAME"].ToString();
        //                    sprice = dvglob.ToTable().Rows[0]["SPRICE"].ToString();
        //                    inprice = dvglob.ToTable().Rows[0]["INPRICE"].ToString();
        //                    sqltext2 = "insert into " + tablename +
        //                              "(SQ_MARID,SQ_NAME,SQ_GUIGE,SQ_CAIZHI,SQ_GUOBIAO,SQ_PCODE,SQ_LENGTH,SQ_WIDTH,SQ_PTCODE,SQ_STID,SQ_STNAME,SQ_CWID,SQ_CWNAME,SQ_UNIT,SQ_NUM,SQ_SPRICE,SQ_INPRICE,SQ_NOTE)" +
        //                              " values('" + marid + "','" + marname + "','" + guige + "','" + caizhi + "','" + guobiao + "','" + pcode + "','" + length + "','" + width + "','" + ptcode + "','" + stid + "','" + stname + "','" + cwid + "','" + cwname + "','" + unit + "','" + usenum + "','" + sprice + "','" + inprice + "','')";
        //                    DBCallCommon.ExeSqlText(sqltext2);
        //                    sqltext3 = "update " + tablename + " set SQ_NUM='" + virtualKC.ToString() + "' where SQ_MARID='" + marid + "' AND SQ_NAME='" + marname + "' AND SQ_GUIGE='" + guige + "' AND SQ_CAIZHI='" + caizhi + "' AND SQ_GUOBIAO='" + guobiao + "' AND SQ_PCODE='" + pcode + "' AND SQ_STID='" + stid + "' AND SQ_CWID='" + cwid + "' AND SQ_UNIT='" + unit + "' AND SQ_PTCODE=''";
        //                    DBCallCommon.ExeSqlText(sqltext3);
        //                }
        //            }
        //            else
        //            {
        //                if (stid != "")
        //                {
        //                    dvtab.RowFilter = "WS_ID='" + ((Label)Reitem.FindControl("PUR_STID")).Text + "'";
        //                    tablename = dvtab.ToTable().Rows[0]["WS_TABLENAME"].ToString();
        //                    dvglob.RowFilter = "MARID='" + marid + "' AND MARNAME='" + marname + "' AND MARNORM='" + guige + "' AND MARTERIAL='" + caizhi + "' AND GUOBIAO='" + guobiao + "' AND STID='" + stid + "' AND NUNIT='" + unit + "'";
        //                    pcode = dvglob.ToTable().Rows[0]["PCODE"].ToString();
        //                    length = dvglob.ToTable().Rows[0]["LENGTH"].ToString();
        //                    width = dvglob.ToTable().Rows[0]["WIDTH"].ToString();
        //                    stname = dvglob.ToTable().Rows[0]["STNAME"].ToString();
        //                    cwid = dvglob.ToTable().Rows[0]["CWID"].ToString();
        //                    cwname = dvglob.ToTable().Rows[0]["CWNAME"].ToString();
        //                    sprice = dvglob.ToTable().Rows[0]["SPRICE"].ToString();
        //                    inprice = dvglob.ToTable().Rows[0]["INPRICE"].ToString();
        //                    sqltext1 = "update TBPC_YUMARUSAGE set  PYU_NUM='" + usenum + "' where PYU_PTCODE='" + ptcode + "'";
        //                    sqltext2 = "update  " + tablename + " set  SQ_NUM='" + usenum + "' where SQ_PTCODE='" + ptcode + "'";
        //                    sqltext3 = "update  " + tablename + " set  SQ_NUM='" + virtualKC + "' where SQ_MARID='" + marid + "' AND SQ_NAME='" + marname + "' AND SQ_GUIGE='" + guige + "' AND SQ_CAIZHI='" + caizhi + "' AND SQ_GUOBIAO='" + guobiao + "' AND SQ_PCODE='" + pcode + "' AND SQ_STID='" + stid + "' AND SQ_CWID='" + cwid + "' AND SQ_UNIT='" + unit + "' AND SQ_PTCODE=''";
        //                    DBCallCommon.ExeSqlText(sqltext1);
        //                    DBCallCommon.ExeSqlText(sqltext2);
        //                    DBCallCommon.ExeSqlText(sqltext3);
        //                }
        //            }
        //        }
        //    }
        //}
        protected void changevirtualKC1()
        {
            RepeaterItem Reitem;

            for (int i = 0; i < tbpc_purshaseplanbillRepeater.Items.Count; i++)
            {
                Reitem = tbpc_purshaseplanbillRepeater.Items[i];
                double virtualKC;
                string tablename = "";
                string sqltext1 = "";
                string sqltext2 = "";
                string sqltext3 = "";
                double PUR_MAXUSENUM = Convert.ToDouble(((Label)Reitem.FindControl("PUR_MAXUSENUM")).Text);
                double USEKCNUM = Convert.ToDouble(((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text);
                string marid = ((Label)Reitem.FindControl("PUR_MARID")).Text;
                string marname = ((Label)Reitem.FindControl("PUR_MARNAME")).Text;
                string guige = ((Label)Reitem.FindControl("PUR_MARNORM")).Text;
                string caizhi = ((Label)Reitem.FindControl("PUR_MARTERIAL")).Text;
                string guobiao = ((Label)Reitem.FindControl("PUR_GUOBIAO")).Text;
                string kcnum = ((Label)Reitem.FindControl("PUR_KCNUM")).Text;
                string unit = ((Label)Reitem.FindControl("PUR_NUNIT")).Text;
                string ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text;
                string mtoptcode = ptcode.Substring(0, ptcode.Length - 4) + "MTO" + ptcode.Substring(ptcode.Length - 4);
                string state = "0";
                string usenum = ((TextBox)Reitem.FindControl("PUR_USEKCNUM")).Text;
                virtualKC = PUR_MAXUSENUM - USEKCNUM;
                //sqltext = "update TBWS_SQYMI SET SQ_VNUM='" + virtualKC + "'WHERE SQ_MARID='" + Convert.ToString(((Label)Reitem.FindControl("PUR_MARID")).Text).Trim() + "'";
                //DBCallCommon.ExeSqlText(sqltext);//执行操作
                if (PUR_MAXUSENUM > 0)
                {
                    if (Label_view.Text == "000" && DropDownListview.SelectedValue.ToString() == "0")
                    {

                        sqltext1 = "insert into TBWS_STORAGE(SQ_CODE, SQ_MARID, SQ_MARNAME, SQ_ATTRIBUTE, SQ_GB, SQ_STANDARD, SQ_LENGTH, SQ_WIDTH, " +
                                   "SQ_LOTNUM, SQ_PMODE, SQ_PTC, SQ_WAREHOUSEID, SQ_WAREHOUSENAME, SQ_POSITIONID, " +
                                   "SQ_POSITIONNAME, SQ_UNIT, SQ_NUM, SQ_UNITPRICE, SQ_AMOUNT, SQ_INCODE, SQ_OUTCODE, SQ_NOTE) " +
                                   "select SQ_CODE='" + mtoptcode + "', SQ_MARID, SQ_MARNAME, SQ_ATTRIBUTE, SQ_GB, SQ_STANDARD, SQ_LENGTH, SQ_WIDTH, " +
                                   "SQ_LOTNUM, SQ_PMODE, SQ_PTC='" + mtoptcode + "', SQ_WAREHOUSEID, SQ_WAREHOUSENAME, SQ_POSITIONID, " +
                                   "SQ_POSITIONNAME, SQ_UNIT, SQ_NUM='" + usenum + "', SQ_UNITPRICE, SQ_AMOUNT, SQ_INCODE, SQ_OUTCODE, SQ_NOTE=''" +
                                   "from TBWS_STORAGE where SQ_PTC='' and SQ_MARID='" + marid + "' and SQ_MARNAME='" + marname + "' and SQ_ATTRIBUTE='" + caizhi + "' and SQ_GB='" + guobiao + "' and SQ_STANDARD='" + guige + "'";
                        DBCallCommon.ExeSqlText(sqltext1);
                        sqltext2 = "update TBWS_STORAGE set SQ_NUM='" + virtualKC.ToString() + "' where SQ_MARID='" + marid + "' AND SQ_MARNAME='" + marname + "' AND SQ_STANDARD='" + guige + "' AND SQ_ATTRIBUTE='" + caizhi + "' AND SQ_GB='" + guobiao + "' AND SQ_UNIT='" + unit + "' AND SQ_PTC=''";
                        DBCallCommon.ExeSqlText(sqltext2);

                    }
                    else
                    {
                        sqltext1 = "update  TBWS_STORAGE set  SQ_NUM='" + usenum + "' where SQ_PTC='" + mtoptcode + "'";
                        sqltext2 = "update  TBWS_STORAGE set  SQ_NUM='" + virtualKC + "' where SQ_MARID='" + marid + "' AND SQ_MARNAME='" + marname + "' AND SQ_STANDARD='" + guige + "' AND SQ_ATTRIBUTE='" + caizhi + "' AND SQ_GB='" + guobiao + "' AND SQ_UNIT='" + unit + "' AND SQ_PTC=''";
                        DBCallCommon.ExeSqlText(sqltext1);
                        DBCallCommon.ExeSqlText(sqltext2);
                    }
                }
            }
        }
        protected void tbpc_purshaseplanbillRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            DataView dataView = gloabt.DefaultView;//定义一个DataView为gloabt的默认视图 
            if (Label_view.Text == "100" || DropDownListview.SelectedValue.ToString() == "1")
            {
                if (e.Item.ItemIndex >= 0)
                {
                    if (((Label)e.Item.FindControl("PUR_STATE")).Text.ToString() == "2")//审核已通过，不能再修改
                    {
                        ((CheckBox)e.Item.FindControl("CKBOX_USEKCALTER")).Enabled = false;
                    }
                    dataView.RowFilter = "MARID='" + ((Label)e.Item.FindControl("PUR_MARID")).Text + "' AND  MARNAME='" + ((Label)e.Item.FindControl("PUR_MARNAME")).Text + "' AND  MARNORM='" + ((Label)e.Item.FindControl("PUR_MARNORM")).Text + "' AND  MARTERIAL='" + ((Label)e.Item.FindControl("PUR_MARTERIAL")).Text + "' AND GUOBIAO='" + ((Label)e.Item.FindControl("PUR_GUOBIAO")).Text + "' AND NUNIT='" + ((Label)e.Item.FindControl("PUR_NUNIT")).Text + "'"; //对dataView进行筛选 
                    dataView.Sort = "NUM ASC";
                    if (dataView.Count > 0)
                    {
                        ((Label)e.Item.FindControl("PUR_KCNUM")).Text = dataView.ToTable().Rows[0]["NUM"].ToString();
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("PUR_KCNUM")).Text = "0";
                    }
                    ((Label)e.Item.FindControl("PUR_MAXUSENUM")).Text = (Convert.ToDouble(((Label)e.Item.FindControl("PUR_KCNUM")).Text) + Convert.ToDouble(((TextBox)e.Item.FindControl("PUR_USEKCNUM")).Text)).ToString();
                }
            }
            else
            {
                if (e.Item.ItemIndex >= 0)
                {
                    dataView.RowFilter = "MARID='" + ((Label)e.Item.FindControl("PUR_MARID")).Text + "' AND  MARNAME='" + ((Label)e.Item.FindControl("PUR_MARNAME")).Text + "' AND  MARNORM='" + ((Label)e.Item.FindControl("PUR_MARNORM")).Text + "' AND  MARTERIAL='" + ((Label)e.Item.FindControl("PUR_MARTERIAL")).Text + "' AND GUOBIAO='" + ((Label)e.Item.FindControl("PUR_GUOBIAO")).Text + "' AND NUNIT='" + ((Label)e.Item.FindControl("PUR_NUNIT")).Text + "' AND NUM>'0'"; //对dataView进行筛选 
                    dataView.Sort = "NUM ASC";
                    if (dataView.Count > 0)
                    {
                        ((Label)e.Item.FindControl("PUR_KCNUM")).Text = dataView.ToTable().Rows[0]["NUM"].ToString();
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("PUR_KCNUM")).Text = "0";
                    }
                    //((Label)e.Item.FindControl("PUR_MAXUSENUM")).Text = (Convert.ToDouble(((Label)e.Item.FindControl("PUR_KCNUM")).Text) + Convert.ToDouble(((Label)e.Item.FindControl("PUR_KCNUM")).Text)).ToString();
                    ((Label)e.Item.FindControl("PUR_MAXUSENUM")).Text = ((Label)e.Item.FindControl("PUR_KCNUM")).Text;
                }
            }
        }
        public string get_pr_state(string i)
        {
            string statestr = "";
            string liststate = Label_view.Text;
            if (liststate == "000")//未提交
            {
                if (i == "0")
                {
                    statestr = "未保存";
                }
                else
                {
                    statestr = "已保存";
                }

            }
            else//已审核
            {
                if (liststate == "100")
                {
                    statestr = "驳回";
                }
            }
            return statestr;
        }
        public string get_pur_state(string i)
        {
            string statestr = "";
            if (Label_view.Text == "100")
            {
                if (i == "1")
                {
                    statestr = "拒绝";
                }
                else
                {
                    if (i == "2")
                    {
                        statestr = "同意";
                    }
                }
            }
            return statestr;
        }
        public string get_pur_fixed(string i)
        {
            string statestr = "";
            if (i == "0")
            {
                statestr = "不定尺";
            }
            else
            {
                if (i == "1")
                {
                    statestr = "定尺";
                }
            }
            return statestr;
        }
    }
}
