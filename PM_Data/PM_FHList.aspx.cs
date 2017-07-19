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
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_FHList : System.Web.UI.Page
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
       
       
            //if (rbl_status.SelectedValue == "1")
            //{
            //    btnfayun.Visible = false;
            //}
            //else if (rbl_status.SelectedValue == "0" || rbl_status.SelectedValue == "3")
            //{
            //    btnfayun.Visible = true;
            //}
            //if (Session["UserID"].ToString() == "257")
            //{
            //    btn_bbj.Enabled = true;
            //}
            //else { btn_bbj.Enabled = false; }
            //if (Session["UserID"].ToString() == "85")
            //{
            //    btnfayun.Enabled = true;
            //}
            //else
            //{
            //    btnfayun.Enabled = false;
            //}
        }

     


 

        protected void btnfayun_Click(object sender, EventArgs e)
        {
            //List<string> sqlstr = new List<string>();
            //int temp = isselected();
            //if (temp == 1)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');window.location.reload();", true);
            //}
            //else if (temp == 2)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您的比价表中包含已经比价的记录,本次操作无效！');window.location.reload();", true);
            //}
            //else
            //{
            //    string sheetcode = encodesheetno();//生成比价单号
            //    string sqltext1;
            //    string sqltext2;
            //    string time = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //    string manid = Session["UserID"].ToString();
            //    sqltext1 = "insert into TBMP_FAYUNPRCRVW(ICL_SHEETNO,ICL_IQRDATE,ICL_REVIEWA,ICL_CSTATE,ICL_STATE) VALUES('" + sheetcode + "','" + time + "','" + manid + "','0','0')";
            //    DBCallCommon.ExeSqlText(sqltext1);
            //    foreach (RepeaterItem Reitem in Repeater1.Items)
            //    {
            //        if (((System.Web.UI.WebControls.CheckBox)Reitem.FindControl("CKBOX_SELECT")).Checked)
            //        {
            //            string tsaid = ((System.Web.UI.WebControls.Label)Reitem.FindControl("TSA_ID")).Text;//任务号
            //            string engname = ((Label)Reitem.FindControl("lab_name")).Text;//名称
            //            string map = ((Label)Reitem.FindControl("lab_map")).Text;//图号
            //            string fid = ((Label)Reitem.FindControl("CM_FID")).Text;
            //            string cmid = ((Label)Reitem.FindControl("CM_ID")).Text;//发货通知的唯一编码
            //            string bianhao = ((Label)Reitem.FindControl("CM_BIANHAO")).Text;//编号号
            //            string pm_zongxu = ((Label)Reitem.FindControl("KC_ZONGXU")).Text;//总序
            //            int fynum = Convert.ToInt32(((Label)Reitem.FindControl("lab_number")).Text);
            //            sqlstr.Clear();
            //            sqltext2 = "insert into TBMP_FAYUNPRICE(PM_ZONGXU,PM_FATHERID,PM_FID,PM_BIANHAO,PM_SHEETNO,TSA_ID,PM_ENGNAME,PM_MAP,PM_FYNUM) VALUES('" + pm_zongxu + "','" + cmid + "','" + fid + "','" + bianhao + "','" + sheetcode + "','" + tsaid + "','" + engname + "','" + map + "'," + fynum + ")";
            //            sqlstr.Add(sqltext2);

            //            string sqltext3 = "update TBCM_FHBASIC set CM_STATUS='1' where CM_ID='" + cmid + "' and CM_FID='" + fid + "'";

            //            sqlstr.Add(sqltext3);

            //            DBCallCommon.ExecuteTrans(sqlstr);
            //        }
            //    }
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.open('PM_fayun_check_detail.aspx?sheetno=" + sheetcode + "');", true);
            //}
        }
        protected string encodesheetno()
        {
            string sheetcode = "";
            string sqltext = "select top 1 ICL_SHEETNO FROM TBMP_FAYUNPRCRVW ORDER BY ICL_SHEETNO DESC";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                sheetcode = dt.Rows[0][0].ToString();
                sheetcode = Convert.ToString(Convert.ToInt32(sheetcode) + 1);
                sheetcode = sheetcode.PadLeft(8, '0');
            }
            else
            {
                sheetcode = "00000001";
            }
            return sheetcode;
        }
        protected int isselected()
        {
            int temp = 0;
            //int i = 0;//是否选中数据
            //int j = 0;//选择的数据中是否包含有已询价的数据
            //foreach (RepeaterItem Reitem in Repeater1.Items)
            //{
            //    System.Web.UI.WebControls.CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as System.Web.UI.WebControls.CheckBox;
            //    if (cbx != null)
            //    {
            //        if (cbx.Checked)
            //        {
            //            i++;
            //            string cmfid = ((Label)Reitem.FindControl("CM_FID")).Text;
            //            string tsaid = ((Label)Reitem.FindControl("TSA_ID")).Text;
            //            string pm_zongxu = ((Label)Reitem.FindControl("KC_ZONGXU")).Text;//总序
            //            string sql = "select * from TBMP_FAYUNPRICE where PM_FID='" + cmfid + "' and TSA_ID='" + tsaid + "' and PM_ZONGXU='" + pm_zongxu + "'";
            //            if (DBCallCommon.GetDTUsingSqlText(sql).Rows.Count > 0)
            //            {
            //                j++;
            //            }
            //        }
            //    }
            //}
            //if (i == 0)//未选择数据
            //{
            //    temp = 1;
            //}
            //else
            //{
            //    if (j > 0)
            //    {

            //        temp = 2;
            //    }
            //    else
            //    {
            //        temp = 0;//可以下推
            //    }                //lab_bianhao.Text = t;
            //}
            return temp;
        }
        protected void btn_bbj_Click(object sender, EventArgs e)
        {
            //int i = 0;
            //List<string> list = new List<string>();
            //string sqltxt;
            //foreach (RepeaterItem Reitem in Repeater1.Items)
            //{
            //    CheckBox cb = (CheckBox)Reitem.FindControl("CKBOX_SELECT");
            //    if (cb.Checked)
            //    {
            //        i++;
            //        string fid = ((Label)Reitem.FindControl("CM_FID")).Text;
            //        // string fynum = ((Label)Reitem.FindControl("lab_number")).Text;
            //        //string tsaid = ((Label)Reitem.FindControl("TSA_ID")).Text;
            //        //string engname = ((Label)Reitem.FindControl("lab_name")).Text;
            //        //string map = ((Label)Reitem.FindControl("lab_map")).Text;
            //        string cmid = ((Label)Reitem.FindControl("CM_ID")).Text;//发货通知的唯一编码
            //        sqltxt = "update TBCM_FHBASIC set CM_STATUS='2' where  CM_FID='" + fid + "' and CM_ID='" + cmid + "'";
            //        list.Add(sqltxt);
            //    }
            //}
            //if (i == 0)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有选择数据,本次操作无效！');", true);
            //}
            //else if (i > 0)
            //{
            //    DBCallCommon.ExecuteTrans(list);
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功！');window.location.reload();", true);
            //}
        }
  

    }
}
