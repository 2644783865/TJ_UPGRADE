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
    public partial class PM_Finished_INBILL : System.Web.UI.Page
    {
        List<string> list = new List<string>();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initinfo();
                GetCode();
            }
        }
        /// <summary>
        /// 绑定制单人等信息
        /// </summary>
        private void initinfo()
        {
            if (Request.QueryString["action"].ToString() == "add")
            {
                lblInDate.Text = DateTime.Now.ToString(); 
                string sqltext = "select ST_ID,ST_NAME from TBDS_STAFFINFO WHERE ST_DEPID='" + Session["UserDeptID"].ToString() + "'and ST_PD='0'order by ST_ID DESC";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                cob_fuziren.DataSource = dt;
                cob_fuziren.DataTextField = "ST_NAME";
                cob_fuziren.DataValueField = "ST_ID";
                cob_fuziren.DataBind();
                cob_fuziren.SelectedValue = Session["UserID"].ToString();
                cob_sqren.DataSource = dt;
                cob_sqren.DataTextField = "ST_NAME";
                cob_sqren.DataValueField = "ST_ID";
                cob_sqren.DataBind();
                cob_sqren.SelectedValue = Session["UserID"].ToString();
                TextBoxexecutor.Text = Session["UserName"].ToString();
                TextBoxexecutorid.Text = Session["UserID"].ToString();
            }
        }
        /// <summary>
        /// 获取入库单号
        /// </summary>
        private void GetCode()
        {
            string sqltext;
            sqltext = "select TOP 1 TFI_DOCNUM AS TopIndex from TBMP_FINISHED_IN ORDER BY TFI_DOCNUM DESC";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            int index;
            if (dt.Rows.Count > 0)
            {
                index = Convert.ToInt16(dt.Rows[0]["TopIndex"].ToString());
            }
            else
            {
                index = 0;
            }
            string code = (index + 1).ToString();
           lbi_docnum.Text =code.PadLeft(8, '0');
        }
        //2016.6.23提交审批OnClientClick
        public void sfczbhgx(object sender, EventArgs e)
        {
            string tsaid = "";
            string resulttsaid = "";
            List<string> afiid = new List<string>();
            string resultafiid = "";
            string resultvalid = "";
            // TextBox txt_tsa = (TextBox)this.FindControl("txt_tsa");
            if (txt_tsa.Text == null || txt_tsa.Text == "")
            {
                resultvalid = txt_tsa.Text.ToString().Trim();
            }
            else
            {
                string sfczgrwh = "SELECT distinct TSA_ID  as Expr1 from  View_CM_Task  WHERE  CM_SPSTATUS='2' AND TSA_ID ='" + txt_tsa.Text.Trim() + "'";
                DataTable dtsfczgrwh = DBCallCommon.GetDTUsingSqlText(sfczgrwh);
                if (dtsfczgrwh.Rows.Count > 0)
                {
                    tsaid = txt_tsa.Text;
                    string sqlquality = "select distinct AFI_ID,PTC,AFI_ENDDATE,AFI_QCMANNM from TBQM_APLYFORINSPCT as a left join TBQM_APLYFORITEM as b on a.UNIQUEID=b.UNIQUEID where PTC like '%" + tsaid + "%'and RESULT not  in ('合格','让步接收') order by AFI_ENDDATE desc";
                    DataTable dtquality = DBCallCommon.GetDTUsingSqlText(sqlquality);
                    if (dtquality.Rows.Count > 0)
                    {
                        string sqlselect = "";
                        DataTable dtselect;
                        DataView dvselect;
                        for (int i = 0; i < dtquality.Rows.Count; i++)
                        {
                            sqlselect = "select ISAGAIN,AFI_ID,PTC,isnull(RESULT,'')RESULT,AFI_ENDRESLUT,AFI_ENDDATE,AFI_QCMANNM from TBQM_APLYFORINSPCT as a left join TBQM_APLYFORITEM as b on a.UNIQUEID=b.UNIQUEID where PTC='" + dtquality.Rows[i]["PTC"] + "'and isnull(AFI_ENDDATE,'') =isnull((select top 1 AFI_ENDDATE from TBQM_APLYFORINSPCT as a left join TBQM_APLYFORITEM as b on a.UNIQUEID=b.UNIQUEID where PTC='" + dtquality.Rows[i]["PTC"] + "' order by AFI_ENDDATE desc),'') order by ISAGAIN desc";
                            dtselect = DBCallCommon.GetDTUsingSqlText(sqlselect);
                            if (dtselect.Rows[0]["RESULT"].ToString() != "合格" && dtselect.Rows[0]["RESULT"].ToString() != "让步接收")
                            {
                                resulttsaid = tsaid;
                                dvselect = dtselect.DefaultView;
                                dvselect.RowFilter = "RESULT not in ('合格','让步接收')";
                                for (int j = 0; j < dvselect.Count; j++)
                                {
                                    if (!afiid.Contains(dvselect[j]["AFI_ID"].ToString().PadLeft(5, '0')))
                                        afiid.Add(dvselect[j]["AFI_ID"].ToString().PadLeft(5, '0'));
                                }
                            }
                        }
                        for (int k = 0; k < afiid.Count; k++)
                        {
                            resultafiid += afiid[k] + "/";
                        }
                        resultvalid = resulttsaid != "" ? "任务号【" + resulttsaid + "】下存在未经质检或检验不合格的报检子项,质检编号为【" + resultafiid.TrimEnd('/') + "】,仍需提交请点击【提交】按钮" : "";
                    }
                }
                else
                {
                    tsaid = txt_tsa.Text;
                    resultvalid = "任务号【" + tsaid + "】不存在";
                }
            }
            bhgts.Text = resultvalid;
        }
        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btn_save_Click(object sender, EventArgs e)
        //{
        //    List<string> list = new List<string>();
        //    int i = 0;
        //    foreach (GridViewRow gr in GridView1.Rows)
        //    {
        //        CheckBox cb = (CheckBox)gr.FindControl("cb_check");
        //        TextBox txt = (TextBox)gr.FindControl("txt_rknum");
        //        if (cb.Checked)
        //        {
        //            string patten = @"^\d*$";
        //            Regex rex = new Regex(patten);
        //            if (rex.IsMatch(txt.Text.ToString())&&txt.Text.Trim()!="")//判断输入的是否是纯数字且不为空
        //            {
        //                i++;
        //                int rknum1 = Convert.ToInt32(txt.Text.ToString());//输入的入库数量
        //                string tfi_index = gr.Cells[1].Text.ToString();//总序号
        //                string tfi_name = gr.Cells[3].Text.ToString();//产品名称
        //                string tfi_map = gr.Cells[2].Text.ToString();//图号
        //                string tfi_number = gr.Cells[5].Text.ToString();//总数
        //                string tfi_wght = gr.Cells[6].Text.ToString();//单重
        //                string tfi_fatherid=(gr.FindControl("lblid") as Label).Text;
        //                string tfi_singnumber = (gr.FindControl("BM_SINGNUMBER") as Label).Text;
        //                string tfi_docnum = lbi_docnum.Text.ToString();
        //                string sqltext = "insert into TBMP_FINISHED_IN (TFI_PROJ,TFI_SINGNUMBER,TFI_DOCNUM,TFI_FATHERID,TFI_ZONGXU,TSA_ID,TFI_NAME,TFI_MAP,TFI_RKNUM,TFI_NUMBER,TFI_WGHT,INDATE,NOTE,DocuPersonID,REVIEWA,SQRID,SPZT) VALUES ('"+lab_proj.Text.ToString()+"','"+tfi_singnumber+"','" + tfi_docnum + "','"+tfi_fatherid+"','"+tfi_index+"','" + txt_tsa.Text.ToString() + "','" + tfi_name + "','" + tfi_map + "'," + rknum1 + ",'" + tfi_number + "','" + tfi_wght + "','" + lblInDate.Text.ToString() + "','" + txt_note.Text.ToString().Trim() + "','" + TextBoxexecutorid.Text.ToString() + "','" + cob_fuziren.SelectedValue.ToString() + "','" + cob_sqren.SelectedValue.ToString() + "','0')";
        //              //DBCallCommon.ExeSqlText(sqltext);
        //                list.Add(sqltext);
        //                //sqltext = "update dbo.View_TM_DQO set BM_YRKNUM="+rknum1+" where BM_PJID not like 'JSB.%' and BM_MSSTATUS<>'1' and dbo.Splitnum(BM_ZONGXU,'.')<2 and BM_MARID='' and BM_ENGID='" + txt_tsa.Text.ToString() + "' and BM_ZONGXU = '" + tfi_index + "'";
        //                //list.Add(sqltext);
        //            }
        //        }
        //    }
        //    if (i > 0)
        //    {
        //        DBCallCommon.ExecuteTrans(list);
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提交成功！！！点击确定跳转到审核页面!');window.location.href='PM_Finished_look.aspx?action=view&docnum=" + lbi_docnum.Text + "&id=" + txt_tsa.Text.Trim().ToString() + "'", true);
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('没有选择数据！！');", true);       
        //    }

        //}
        //protected void btn_confirm_OnClick(object sender, EventArgs e)
        //{
        //    string engid = txt_tsa.Text.Trim();
        //    string sqltext = "select * from dbo.View_TM_DQO where BM_PJID not like 'JSB.%' and BM_MSSTATUS<>'1' and dbo.Splitnum(BM_ZONGXU,'.')=0  and BM_ENGID='" + engid + "'ORDER BY dbo.f_formatstr(BM_ZONGXU, '.')";//一级BOM
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //    string sqltext2="SELECT CM_PROJ from  View_CM_Task  WHERE TSA_ID='"+engid+"'";
        //    DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);
        //    lab_proj.Text = dt2.Rows[0]["CM_PROJ"].ToString();
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        //}
        //protected void btn_confirm2_OnClick(object sender, EventArgs e)
        //{
        //    string engid = txt_tsa.Text.Trim();
        //    string sqltext = "select * from dbo.View_TM_DQO where BM_PJID not like 'JSB.%' and BM_MSSTATUS<>'1' and dbo.Splitnum(BM_ZONGXU,'.')=1  and BM_ENGID='" + engid + "'ORDER BY dbo.f_formatstr(BM_ZONGXU, '.')";//二级BOM
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //    string sqltext2 = "SELECT CM_PROJ from  View_CM_Task  WHERE TSA_ID='" + engid + "'";
        //    DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltext2);
        //    lab_proj.Text = dt2.Rows[0]["CM_PROJ"].ToString();
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        //}

       
        //protected void GridView1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    foreach (GridViewRow gridrow in GridView1.Rows)
        //    {
        //        //HiddenField hdf_status = (HiddenField)gridrow.FindControl("scstatus");
        //        Label lblstatus = (Label)gridrow.FindControl("lblstatus");
        //        CheckBox cb = (CheckBox)gridrow.FindControl("cb_check");
        //        TextBox txt_rknum = (TextBox)gridrow.FindControl("txt_rknum");
        //        Label lab_index = (Label)gridrow.FindControl("lblIndex");
        //        switch (lblstatus.Text.ToString())
        //        {
        //            //case "0":  gridrow.Cells[0].BackColor = System.Drawing.Color.Green;
        //            //    break;
        //            case "1": gridrow.Cells[0].BackColor = System.Drawing.Color.Yellow;
        //                break;
        //            case "2": gridrow.Cells[0].BackColor = System.Drawing.Color.Red;
        //                cb.Visible = false;
        //                txt_rknum.Enabled = false;
        //                break;
        //        }
        //    }
        //}
        /// <summary>
        /// 输入单号直接出相关信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void txt_tsa_Textchanged(object sender, EventArgs e)
        //{
        //    string tsaid = "";
        //    string sqltext = "";
        //    DataTable glotb = new DataTable();
        //    TextBox TSA_ID = (TextBox)sender;
        //    if (TSA_ID.Text.ToString() != null)
        //    {
        //        tsaid = TSA_ID.Text.ToString();
        //        sqltext = "SELECT TSA_ID,CM_CONTR,CM_PROJ from  View_CM_Task  WHERE TSA_ID ='" + tsaid + "'";
        //        glotb = DBCallCommon.GetDTUsingSqlText(sqltext);
        //        //txt_contr.Text = glotb.Rows[0]["CM_CONTR"].ToString();
        //        //txt_proj.Text = glotb.Rows[0]["CM_PROJ"].ToString();
        //    }
        //    else
        //    {
        //        showerrormessage(TSA_ID, "输入的任务单号不存在，请重新输入！");
        //        return;
        //    }
        //}
        //protected void showerrormessage(TextBox tbx, string errormessage)
        //{
        //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('" + errormessage + "');", true);
        //    txt_tsa.Text = "";

        //    txt_tsa.Focus();
        //}
    }
}
