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
    public partial class PC_TBPC_Marreplace_all : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initpagemess();
            }
        }

        private void initpagemess()
        {
            string sqltext = "";
            TextBox_pid.Text = Request.QueryString["mpcode"].ToString();//相似代用单号
            sqltext = "select  pjid AS MP_PJID,pjnm AS MP_PJNAME,engid AS MP_ENGID,engnm AS MP_ENGNAME,"+
                      "zdrid AS MP_FILLFMID,zdrnm as MP_FILLFMNM,zdtime as MP_FILLFMTIME,zdwctime  " +
                      "from View_TBPC_MARREPLACE_total_planrvw where mpcode='" + TextBox_pid.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                tb_pjid.Text = dr["MP_PJID"].ToString();
                tb_pjname.Text = dr["MP_PJNAME"].ToString();
                tb_pjinfo.Text = tb_pjid.Text + tb_pjname.Text;
                tb_engid.Text = dr["MP_ENGID"].ToString();
                tb_engname.Text = dr["MP_ENGNAME"].ToString();
                tb_enginfo.Text = tb_engid.Text + tb_engname.Text;
                tb_peoid.Text = dr["MP_FILLFMID"].ToString();
                tb_peoname.Text = dr["MP_FILLFMNM"].ToString();
                Tb_shijian.Text = dr["MP_FILLFMTIME"].ToString();
                tb_zh.Text = dr["MP_ENGID"].ToString(); 
            }
            dr.Close();
            tbpc_marrepallRepeaterdatabind();
        }

        private void tbpc_marrepallRepeaterdatabind()
        {
            string pcode = TextBox_pid.Text;
            string sqltext = "";
            sqltext = "select ptcode as YPTCODE, marid as YMARID, marnm as YMARNAME, marguige as YGUIGE,"+
                      " marcaizhi as YCAIZHI, marguobiao as YGUOBIAO, num as YNUM,fznum as YFZNUM, "+
                      "marcgunit as YNUNIT,length as YLENGTH,width as YWIDTH,fzunit as YFZUNIT, allstate as STATE,allshstate,alloption,allnote as NOTE  "+
                      "from View_TBPC_MARREPLACE_all "+
                      "where mpcode='" + pcode + "'";
            DBCallCommon.BindRepeater(tbpc_marrepallRepeater, sqltext);
            if (tbpc_marrepallRepeater.Items.Count == 0)
            {
                NoDataPane.Visible = true;
            }
            else
            {
                NoDataPane.Visible = false;
            }
        }
        //protected void btn_concel_Click(object sender, EventArgs e)
        //{

        //}
        //protected void btn_confirm_Click(object sender, EventArgs e)
        //{

        //}
        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PC_Data/PC_TBPC_Marreplace_panel.aspx?mpno=" + TextBox_pid.Text + "");
        }
        //protected void btn_edit_Click(object sender, EventArgs e)
        //{
        //    Button lbtn = (Button)sender;
        //    RepeaterItem Reitem = (RepeaterItem)lbtn.Parent;
        //    string ptcode = ((Label)Reitem.FindControl("YPTCODE")).Text;
        //    string tempstr = ptcode;
        //    Response.Redirect("~/PC_Data/PC_TBPC_Marreplace_alldetail.aspx?ptcode=" + tempstr + "");
        //}
        protected void tbpc_marrepallRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Header)
            {

            }
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

            }

        }
    }
}
