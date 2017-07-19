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
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_Engineering_Reg : System.Web.UI.Page
    {
        string register = "";
        string sqlText = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitInfo();
                IniVar(); //用于控制页面的编辑性
            }
        }

        //初始化页面
        private void InitInfo()
        {
            register = Request.QueryString["register"];
            GetAssInfo();
        }

        private void IniVar()
        {
            string fag = Request.QueryString["fag"];
            if (fag == "look")  //查看
            {
                btnUpdate.Visible = false;
                quantity.Disabled = true;
                totalwght.Disabled = true;
                volume.Disabled = true;
                cnwxdlzl.Disabled = true;
                netwght.Disabled = true;
                grosswght.Disabled = true;
                caildate.Disabled = true;
                wgdate.Disabled = true;
                mxdate.Disabled = true;
                zxdate.Disabled = true;
                jsdate.Disabled = true;
                oildate.Disabled = true;

                record.Enabled = false;
                btnConfirm.Visible = false;
                btnCancel.Visible = false;
            }
            else
            {
                caildate.Attributes.Add("onclick", "setday(this)");
                wgdate.Attributes.Add("onclick", "setday(this)");
                mxdate.Attributes.Add("onclick", "setday(this)");
                zxdate.Attributes.Add("onclick", "setday(this)");
                jsdate.Attributes.Add("onclick", "setday(this)");
                oildate.Attributes.Add("onclick", "setday(this)");
                btnConfirm.Visible = true;
                btnCancel.Visible = true;
            }
        }

        /// <summary>
        /// 技术负责人任务分工详细信息
        /// </summary>
        private void GetAssInfo()
        {
            if (register.Contains("(O)") || register.Contains("(DQO)"))
            {
                sqlText = "select * from  View_TM_TaskAssign where TSA_ID='"+register+"'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                if (dr.HasRows)
                {
                    dr.Read();
                    tsaid.Text = dr["TSA_ID"].ToString();
                    proname.Text = dr["TSA_PJNAME"].ToString();
                    engname.Text = dr["TSA_ENGNAME"].ToString();
                    ddlengtype.Text = dr["TSA_ENGSTRTYPE"].ToString();
                    drawcode.Text = dr["TSA_DRAWCODE"].ToString();
                    devicecode.Text = dr["TSA_DEVICECODE"].ToString();
                    designcom.Text = dr["TSA_DESIGNCOM"].ToString();
                    modelcode.Text = dr["TSA_MODELCODE"].ToString();
                    recvdate.Text = dr["TSA_RECVDATE"].ToString();
                    confishdate.Text = dr["TSA_CONFNSHDATE"].ToString();
                    contype.Text = dr["TSA_CONTYPE"].ToString();
                    drawstate.Text = dr["TSA_DRAWSTATE"].ToString();
                    client.Text = dr["TSA_CLIENT"].ToString();
                    labperson.Text = dr["TSA_TCCLERKNM"].ToString();
                    startdate.Text = dr["TSA_STARTDATE"].ToString();
                    planfishdate.Text = dr["TSA_PLANFSDATE"].ToString();
                    realfishdate.Text = dr["TSA_REALFSDATE"].ToString();
                    planingplan.Text = dr["TSA_PAINTINGPLAN"].ToString();
                    techsharing.Text = dr["TSA_TECHSHARING"].ToString();
                    plpreschedule.Text = dr["TSA_PLPRESCHEDULE"].ToString();
                    thirdpart.Text = dr["TSA_THIRDPART"].ToString();
                    rblstatus.Text = dr["TSA_STATE"].ToString().Trim() == "1" ? "进行中" : dr["TSA_STATE"].ToString().Trim() == "2" ? "完工" : "停工";

                    rblJState.SelectedValue = dr["TSA_STATE"].ToString().Trim();
                    txtWCSJ.Text = dr["TSA_REALFSDATE"].ToString();

                    note.Text = dr["TSA_NOTE"].ToString();
                }
                dr.Close();
            }
            else
            {
                sqlText = "select TSA_ID,TSA_PJID,TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE,";
                sqlText += "TSA_DRAWCODE,TSA_DEVICECODE,TSA_DESIGNCOM,TSA_MODELCODE,";
                sqlText += "TSA_RECVDATE,TSA_CONFNSHDATE,TSA_CONTYPE,TSA_DRAWSTATE,TSA_CLIENT,TSA_TCCLERKNM,TSA_STARTDATE,";
                sqlText += "TSA_PLANFSDATE,TSA_REALFSDATE,TSA_PAINTINGPLAN,TSA_TECHSHARING,TSA_PLPRESCHEDULE,";
                sqlText += "TSA_THIRDPART,TSA_STATE,TSA_NOTE,TSA_QUANTITY,TSA_TOTALWGHT,TSA_CNWXDLZL,TSA_VOLUME,";
                sqlText += "TSA_NETWGHT,TSA_GROSSWGHT,TSA_NOTE1,TSA_MARPLAN,TSA_WXWGJ,TSA_MNDETAIL,TSA_ZXSHEET,";
                sqlText += "TSA_TECHJD,TSA_OILPLAN from View_TM_QUANT_LIST where TSA_ID='" + register + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
                while (dr.Read())
                {
                    #region
                    tsaid.Text = dr["TSA_ID"].ToString();
                    proname.Text = dr["TSA_PJNAME"].ToString();
                    engname.Text = dr["TSA_ENGNAME"].ToString();
                    ddlengtype.Text = dr["TSA_ENGSTRTYPE"].ToString();
                    drawcode.Text = dr["TSA_DRAWCODE"].ToString();
                    devicecode.Text = dr["TSA_DEVICECODE"].ToString();
                    designcom.Text = dr["TSA_DESIGNCOM"].ToString();
                    modelcode.Text = dr["TSA_MODELCODE"].ToString();
                    recvdate.Text = dr["TSA_RECVDATE"].ToString();
                    confishdate.Text = dr["TSA_CONFNSHDATE"].ToString();
                    contype.Text = dr["TSA_CONTYPE"].ToString();
                    drawstate.Text = dr["TSA_DRAWSTATE"].ToString();
                    client.Text = dr["TSA_CLIENT"].ToString();
                    labperson.Text = dr["TSA_TCCLERKNM"].ToString();
                    startdate.Text = dr["TSA_STARTDATE"].ToString();
                    planfishdate.Text = dr["TSA_PLANFSDATE"].ToString();
                    realfishdate.Text = dr["TSA_REALFSDATE"].ToString();
                    planingplan.Text = dr["TSA_PAINTINGPLAN"].ToString();
                    techsharing.Text = dr["TSA_TECHSHARING"].ToString();
                    plpreschedule.Text = dr["TSA_PLPRESCHEDULE"].ToString();
                    thirdpart.Text = dr["TSA_THIRDPART"].ToString();
                    rblstatus.Text = dr["TSA_STATE"].ToString().Trim() == "1" ? "进行中" : dr["TSA_STATE"].ToString().Trim() == "2" ? "完工" : "停工";

                    rblJState.SelectedValue = dr["TSA_STATE"].ToString().Trim();
                    txtWCSJ.Text = dr["TSA_REALFSDATE"].ToString();

                    note.Text = dr["TSA_NOTE"].ToString();
                    quantity.Value = dr["TSA_QUANTITY"].ToString();
                    totalwght.Value = dr["TSA_TOTALWGHT"].ToString();
                    volume.Value = dr["TSA_VOLUME"].ToString();
                    cnwxdlzl.Value = dr["TSA_CNWXDLZL"].ToString();
                    netwght.Value = dr["TSA_NETWGHT"].ToString();
                    grosswght.Value = dr["TSA_GROSSWGHT"].ToString();

                    caildate.Value = dr["TSA_MARPLAN"].ToString();
                    wgdate.Value = dr["TSA_WXWGJ"].ToString();
                    mxdate.Value = dr["TSA_MNDETAIL"].ToString();
                    zxdate.Value = dr["TSA_ZXSHEET"].ToString();
                    jsdate.Value = dr["TSA_TECHJD"].ToString();
                    oildate.Value = dr["TSA_OILPLAN"].ToString();
                    record.Text = dr["TSA_NOTE1"].ToString();
                    #endregion
                }
                dr.Close();
            }
        }

        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            if (rblJState.SelectedValue == "2"&&txtWCSJ.Text.Trim()=="")
            {
                Response.Write("<script>javascript:alert('【完工】,请输入【技术准备完成时间】！！！');</script>");
                return;
            }
            if (this.CanSubmit())
            {
                List<string> list_sql = new List<string>();
                if (tsaid.Text.Trim().Contains("(O)") || tsaid.Text.Trim().Contains("(DQO)"))
                {
                    ;
                }
                else
                {
                    sqlText = "update TBPM_TCTSENROLL set TSA_QUANTITY='" + quantity.Value.Trim() + "',";
                    sqlText += "TSA_TOTALWGHT='" + totalwght.Value.Trim() + "',TSA_VOLUME='" + volume.Value.Trim() + "',";
                    sqlText += "TSA_CNWXDLZL='" + cnwxdlzl.Value.Trim() + "',TSA_NETWGHT='" + netwght.Value.Trim() + "',";
                    sqlText += "TSA_GROSSWGHT='" + grosswght.Value.Trim() + "',TSA_NOTE='" + record.Text.Trim() + "', ";
                    sqlText += "TSA_MARPLAN='" + caildate.Value.Trim() + "',TSA_WXWGJ='" + wgdate.Value.Trim() + "',";
                    sqlText += "TSA_MNDETAIL='" + mxdate.Value.Trim() + "',TSA_ZXSHEET='" + zxdate.Value.Trim() + "',";
                    sqlText += "TSA_TECHJD='" + jsdate.Value.Trim() + "',TSA_OILPLAN='" + oildate.Value.Trim() + "' ";
                    sqlText += "where TSA_ID='" + tsaid.Text + "'";
                    list_sql.Add(sqlText);
                }

                sqlText = "update TBPM_TCTSASSGN set TSA_STATE='" + rblJState.SelectedValue + "',TSA_REALFSDATE='"+txtWCSJ.Text.Trim()+"' where TSA_ID='" + tsaid.Text.Trim() + "' or TSA_ID like '" + tsaid.Text.Trim() + "-%'";
                list_sql.Add(sqlText);

                DBCallCommon.ExecuteTrans(list_sql);


                Response.Write("<script>javascript:alert('保存成功！');history.go(-2);</script>");
            }
            else
            {
                Response.Write("<script>javascript:alert('无法保存！\\r\\r提示:修改导致【总重】小于各分项之和！！！');</script>");
            }
        }

        protected bool CanSubmit()
        {

            string sql = "select isnull(sum(TSA_SHIPWGHT),0) Weight from  TBPM_TCTSENROLL where TSA_ID='" + tsaid.Text + "' and (TSA_SHIP is not null OR TSA_SHIP<>'')";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            double totalweight = 0;
            if (dr.HasRows)
            {
                dr.Read();
                totalweight = Convert.ToDouble(dr["Weight"].ToString());
                dr.Close();
            }

            double total=0;
            if (totalwght.Value.Trim() != "")
            {
                total = Convert.ToDouble(totalwght.Value.Trim());
            }

            if (total < totalweight)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Write("<script language=javascript>history.go(-2);</script>");
        }

        protected void btnUpdate_OnClick(object sender, EventArgs e)
        {
            string sql_tb="select dbo.TM_GetTableNameByTaskID('"+tsaid.Text.Trim()+"')";
            string tablename;
            DataTable dt_tb=DBCallCommon.GetDTUsingSqlText(sql_tb);
            tablename = dt_tb.Rows[0][0].ToString();

            string sql_getsum = "Select isnull(sum([BM_MATOTALWGHT]),0) from " + tablename + " Where BM_TASKID='" + tsaid.Text.Trim() + "' AND BM_MPSTATE='1' AND [BM_MASHAPE] in('板','型','圆钢') AND [BM_MARID]!=''";

            string cldawx =Convert.ToDouble(DBCallCommon.GetDTUsingSqlText(sql_getsum).Rows[0][0].ToString()).ToString("0");

            string sql = "update TBPM_TCTSENROLL set TSA_CNWXDLZL=" + cldawx + " where TSA_ID='" + tsaid.Text + "' ";

            DBCallCommon.ExeSqlText(sql);

            cnwxdlzl.Value = cldawx;


        }
    }
}
