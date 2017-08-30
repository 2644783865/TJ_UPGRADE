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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Marreplace_edit : System.Web.UI.Page
    {
        public string gloabstate
        {
            get
            {
                object state = ViewState["gloabstate"];
                return state == null ? null : (string)state;
            }
            set
            {
                ViewState["gloabstate"] = value;
            }
        }
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
        public string gloabptc
        {
            get
            {
                object str = ViewState["gloabptc"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabptc"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mpcode"] != null)
                {
                    gloabsheetno = Request.QueryString["mpcode"].ToString();
                }
                else
                {
                    gloabsheetno = "";
                }
                if (Request.QueryString["ptc"] != null)
                {
                    gloabptc = Request.QueryString["ptc"].ToString();
                }
                else
                {
                    gloabptc = "";
                }


                Tb_Code.Text = gloabsheetno;//单号
                initpager();
                //initpower();
            }
        }
        //private void initpower()
        //{
        //    string sqltext = "";
        //    sqltext = "select ST_NAME,ST_CODE from TBDS_STAFFINFO WHERE ST_DEPID='05'";
        //    string dataText = "ST_NAME";
        //    string dataValue = "ST_CODE";
        //    DBCallCommon.BindDdl(DropDownList1, sqltext, dataText, dataValue);
        //    DropDownList1.SelectedIndex = 0;
        //}
        private void initpager()
        {
            string sqltext = "";
            string pcode = Tb_Code.Text;
            sqltext = "SELECT mpcode, plancode, zdreson, zdrid, zdtime, zdwctime, shraid," +
                     "shratime, shrayj, shrbid, shrbtime, shrbyj, totalstate,totalnote, " +
                     "zdrnm, shranm, shrbnm, engid, pjid,pjnm, engnm,ST_NAME  " +
                     "FROM View_TBPC_MARREPLACE_total_planrvw where mpcode='" + pcode + "'";
            DataTable dt = new DataTable();
            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                Tb_pjid.Text = dt.Rows[0]["pjid"].ToString();
                Tb_pjname.Text = dt.Rows[0]["pjnm"].ToString();
                Tb_engname.Text = dt.Rows[0]["engnm"].ToString();
                Tb_Date.Text = dt.Rows[0]["zdtime"].ToString();
                Tb_Abstract.Text = dt.Rows[0]["totalnote"].ToString();
                tb_Document.Text = dt.Rows[0]["zdrnm"].ToString();
                lb_DocumentID.Text = dt.Rows[0]["zdrid"].ToString();
                tb_shenhe.Text = dt.Rows[0]["shranm"].ToString();
                lb_shenheID.Text = dt.Rows[0]["shraid"].ToString();
                tb_Manager.Text = dt.Rows[0]["shrbnm"].ToString();
                lb_ManagerID.Text = dt.Rows[0]["shrbid"].ToString();
                //if (dt.Rows[0]["MP_CKSHRID"].ToString() != "")
                //{
                //    DropDownList1.SelectedValue = dt.Rows[0]["MP_CKSHRID"].ToString();
                //}
                gloabstate = dt.Rows[0]["totalstate"].ToString();
                //if ((gloabstate == "000" && Tb_Code.Text.Substring(4, 1).ToString() == "0"))
                //{
                //    ckshr.Visible = true;
                //}
                //else
                //{
                //    ckshr.Visible = false;
                //}
            }
            Marreplace_edit_repeaterbind();
        }
        private void Marreplace_edit_repeaterbind()
        {
            string pcode = Tb_Code.Text;
            string sqltext = "";
            sqltext = "SELECT marid, num, ptcode, usenum, allstate, allshstate, alloption, allnote, marnm, " +
                    "marguige, marguobiao, marcaizhi, marcgunit, mpcode, plancode, zdreson, zdrid, " +
                    "zdtime, shraid, shratime, shrayj, shrbid, shrbtime, shrbyj, totalstate, totalnote, zdrnm, " +
                    "shranm, shrbnm, engid, pjnm, engnm, detailmarnm, detailmarguige, " +
                    "detailmarguobiao, detailmarcaizhi, detailmarunit, detailmpcode, detailmarid, " +
                    "detailmarnuma, detailmarnumb, detailnote, detailoldsqcode, detailnewsqcode, " +
                    "detailstate, fzunit, length, width, detaillength, detailwidth, detailfzunit, fznum, " +
                    "usefznum, pjid, zdwctime " +
                    "FROM View_TBPC_MARREPLACE_total_all_detail " +
                    "WHERE mpcode='" + pcode + "' order by ptcode asc";
            DBCallCommon.BindRepeater(Marreplace_edit_repeater, sqltext);
            if (Marreplace_edit_repeater.Items.Count == 0)
            {
                NoDataPane.Visible = true;
            }
            else
            {
                NoDataPane.Visible = false;
            }
        }
        protected void btn_save_Click(object sender, EventArgs e)//保存
        {
            foreach (RepeaterItem reitem in Marreplace_edit_repeater.Items)
            {
                string newmarid = ((TextBox)reitem.FindControl("MP_NEWMARID")).Text.ToString().Trim();
                if (newmarid == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入代用的物料编码！');", true);
                    return;
                }
                else
                {
                    string sqlnewmarid="select * from TBMA_MATERIAL where ID='"+newmarid+"'";
                    DataTable dtnewmarid=DBCallCommon.GetDTUsingSqlText(sqlnewmarid);
                    if (dtnewmarid.Rows.Count == 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('物料编码不存在！');", true);
                        return;
                    }
                    else
                    {
                        savedate();
                    }
                }
            }
            
            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功');", true);
        }
        //protected void btn_back_Click(object sender, EventArgs e)//返回
        //{

        //}
        private void savedate()
        {
            string sqltext = "";
            int i = 0;
            List<string> sqltextlist = new List<string>();
            string mpcode = "";
            string ptcode = "";
            string newmarid = "";
            double newnuma = 0;
            double newnumb = 0;
            double length = 0;
            double width = 0;
            string newnote = "";
            foreach (RepeaterItem Retem in Marreplace_edit_repeater.Items)
            {
                newnuma = Convert.ToDouble(((TextBox)Retem.FindControl("TMP_NEWNUMA")).Text == "" ? "0" : ((TextBox)Retem.FindControl("TMP_NEWNUMA")).Text);
                if (newnuma == 0)
                {
                    i++;
                    break;                   
                }
            }
            if (i > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入代用数量！');", true);
            }
            else
            {
                //2017.5.12修改，修改发给部长，没部长发给人本人通知
                string str = "select ST_ID from TBDS_STAFFINFO where ST_POSITION='0501' and ST_PD='0'";
                System.Data.DataTable leader = DBCallCommon.GetDTUsingSqlText(str);
                string lead = "";
                if (leader.Rows.Count > 0)
                {
                    lead = leader.Rows[0][0].ToString();
                }
                if (!string.IsNullOrEmpty(lead))
                {
                    //2017.8.29修改，MP_CHARGEID修改为技术部长ID，原为写死'曹卫亮'ID。
                    sqltext = "update TBPC_MARREPLACETOTAL set MP_STATE='000',MP_LEADER="+lead+",MP_REASON='',MP_FILLFMTIME='" + Tb_Date.Text + "'," +
                                  "MP_REVIEWAADVC='',MP_REVIEWATIME='',MP_CHARGEID=(SELECT TOP 1 st_id FROM dbo.TBDS_STAFFINFO WHERE ST_POSITION='0301' and ST_PD='0'),MP_CHARGETIME='',MP_CHARGEADVC='',MP_NOTE='" + Tb_Abstract.Text + "'  where MP_CODE='" + Tb_Code.Text + "'";
                    sqltextlist.Add(sqltext);

                    sqltext = "delete from TBPC_MARREPLACEDETAIL where MP_CODE='" + Tb_Code.Text + "'";
                    sqltextlist.Add(sqltext);
                    foreach (RepeaterItem Retem in Marreplace_edit_repeater.Items)
                    {
                        mpcode = Tb_Code.Text;
                        ptcode = ((Label)Retem.FindControl("MP_PTCODE")).Text.ToString();
                        newmarid = ((TextBox)Retem.FindControl("MP_NEWMARID")).Text.ToString();
                        newnuma = Convert.ToDouble(((TextBox)Retem.FindControl("TMP_NEWNUMA")).Text == "" ? "0" : ((TextBox)Retem.FindControl("TMP_NEWNUMA")).Text);
                        newnumb = Convert.ToDouble(((TextBox)Retem.FindControl("TMP_NEWNUMB")).Text == "" ? "0" : ((TextBox)Retem.FindControl("TMP_NEWNUMB")).Text);
                        length = Convert.ToDouble(((TextBox)Retem.FindControl("MP_NEWLENGTH")).Text == "" ? "0" : ((TextBox)Retem.FindControl("MP_NEWLENGTH")).Text);
                        width = Convert.ToDouble(((TextBox)Retem.FindControl("MP_NEWWIDTH")).Text == "" ? "0" : ((TextBox)Retem.FindControl("MP_NEWWIDTH")).Text);
                        newnote = ((TextBox)Retem.FindControl("MP_NEWNOTE")).Text.ToString();
                        sqltext = "insert into TBPC_MARREPLACEDETAIL(MP_CODE,MP_PTCODE,MP_NEWMARID," +
                               "MP_NEWNUMA,MP_NEWNUMB,MP_LENGTH,MP_WIDTH,MP_NEWNOTE) values  " +
                               "('" + mpcode + "','" + ptcode + "','" + newmarid + "'," + newnuma + "," +
                               newnumb + "," + length + "," + width + ",'" + newnote + "')";
                        sqltextlist.Add(sqltext);
                    }

                    DBCallCommon.ExecuteTrans(sqltextlist);
                    Response.Redirect("~/PC_Data/PC_TBPC_Marreplace_panel.aspx?mpno=" + Tb_Code.Text);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('采购部暂时无部长，无法进行审批操作，请及时与负责人沟通！');", true);
                    return;
                }
            }
        }


        //数量变化时
        protected void Tb_newmarnum_Textchanged(object sender, EventArgs e)
        {
            string marid = "";
            string marname = "";
            string sqltext = "";
            DataTable glotb = new DataTable();
            TextBox tb_newnum = (TextBox)sender;//定义TextBox
            RepeaterItem Reitem = (RepeaterItem)tb_newnum.Parent;//repeater中的item
            TextBox Tb_newmarid = (TextBox)Reitem.FindControl("MP_NEWMARID");//定义TextBox


            if (Tb_newmarid.Text.ToString().Contains("|"))
            {
                marid = Tb_newmarid.Text.Substring(0, Tb_newmarid.Text.ToString().IndexOf("|"));
                marname = Tb_newmarid.Text.Substring(Tb_newmarid.Text.ToString().IndexOf("|") + 1);
                sqltext = "SELECT MNAME,GUIGE,CAIZHI,GB,PURCUNIT,FUZHUUNIT,MWEIGHT FROM TBMA_MATERIAL WHERE ID='" + marid + "' ORDER BY ID";
                glotb = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (glotb.Rows.Count > 0)
                {
                    Tb_newmarid.Text = marid;
                    ((Label)Reitem.FindControl("MP_NEWMARNAME")).Text = glotb.Rows[0]["MNAME"].ToString();
                    ((Label)Reitem.FindControl("MP_NEWGUIGE")).Text = glotb.Rows[0]["GUIGE"].ToString();
                    ((Label)Reitem.FindControl("MP_NEWCAIZHI")).Text = glotb.Rows[0]["CAIZHI"].ToString();
                    ((Label)Reitem.FindControl("MP_NEWGUOBIAO")).Text = glotb.Rows[0]["GB"].ToString();
                    ((Label)Reitem.FindControl("MP_NEWUNIT")).Text = glotb.Rows[0]["PURCUNIT"].ToString();
                    ((Label)Reitem.FindControl("MP_NEWFZUNIT")).Text = glotb.Rows[0]["FUZHUUNIT"].ToString();
                    //单位换算
                    //主单位千克，理论重量不为0，数量与辅助数量均不为0
                    if (glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "kg" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                    {
                        if ((glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim()))).ToString("0.0000").Trim();//板
                        }
                        else
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim()))).ToString("0.0000").Trim();
                        }
                    }


                    //主单位吨，理论重量不为0，数量与辅助数量均不为0
                    else if (glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "T" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                    {
                        if ((glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) / 1000).ToString("0.0000").Trim();//板
                        }
                        else
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) / 1000).ToString("0.0000").Trim();
                        }
                    }


                    //辅助单位千克，理论重量不为0，数量与辅助数量均不为0
                    else if (glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "kg" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                    {
                        if ((glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim()))).ToString("0.0000").Trim();//板
                        }
                        else
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim()))).ToString("0.0000").Trim();
                        }
                    }


                    //辅助单位吨，理论重量不为0，数量与辅助数量均不为0
                    else if (glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "T" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                    {
                        if ((glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) / 1000).ToString("0.0000").Trim();//板
                        }
                        else
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) / 1000).ToString("0.0000").Trim();
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入的物料编码不存在，请重新输入！');", true);
                }
            }
            else
            {
                if (!(Tb_newmarid.Text == "" || Tb_newmarid.Text == DBNull.Value.ToString()))
                {
                    sqltext = "SELECT ID,MNAME,GUIGE,CAIZHI,GB,PURCUNIT,FUZHUUNIT,MWEIGHT FROM TBMA_MATERIAL WHERE ID='" + Tb_newmarid.Text.Replace(" ", "") + "' OR HMCODE='" + Tb_newmarid.Text.Replace(" ", "") + "' ORDER BY ID";
                    glotb = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (glotb.Rows.Count == 1)
                    {
                        ((TextBox)Reitem.FindControl("MP_NEWMARID")).Text = glotb.Rows[0]["ID"].ToString();
                        ((Label)Reitem.FindControl("MP_NEWMARNAME")).Text = glotb.Rows[0]["MNAME"].ToString();
                        ((Label)Reitem.FindControl("MP_NEWGUIGE")).Text = glotb.Rows[0]["GUIGE"].ToString();
                        ((Label)Reitem.FindControl("MP_NEWCAIZHI")).Text = glotb.Rows[0]["CAIZHI"].ToString();
                        ((Label)Reitem.FindControl("MP_NEWGUOBIAO")).Text = glotb.Rows[0]["GB"].ToString();
                        ((Label)Reitem.FindControl("MP_NEWUNIT")).Text = glotb.Rows[0]["PURCUNIT"].ToString();
                        ((Label)Reitem.FindControl("MP_NEWFZUNIT")).Text = glotb.Rows[0]["FUZHUUNIT"].ToString();


                        //单位换算
                        //主单位千克，理论重量不为0，数量与辅助数量均不为0
                        if (glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "kg" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                        {
                            if ((glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim()))).ToString("0.0000").Trim();//板
                            }
                            else
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim()))).ToString("0.0000").Trim();
                            }
                        }


                        //主单位吨，理论重量不为0，数量与辅助数量均不为0
                        else if (glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "T" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                        {
                            if ((glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) / 1000).ToString("0.0000").Trim();//板
                            }
                            else
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) / 1000).ToString("0.0000").Trim();
                            }
                        }


                        //辅助单位千克，理论重量不为0，数量与辅助数量均不为0
                        else if (glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "kg" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                        {
                            if ((glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim()))).ToString("0.0000").Trim();//板
                            }
                            else
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim()))).ToString("0.0000").Trim();
                            }
                        }


                        //辅助单位吨，理论重量不为0，数量与辅助数量均不为0
                        else if (glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "T" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                        {
                            if ((glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) / 1000).ToString("0.0000").Trim();//板
                            }
                            else
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) / 1000).ToString("0.0000").Trim();
                            }
                        }

                    }
                    else
                    {
                        if (glotb.Rows.Count > 1)
                        {
                            Tb_newmarid.Text = "";
                            Tb_newmarid.Focus();
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入的物料编码不完整，请重新输入！');", true);
                            return;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入的物料编码不存在，请重新输入！');", true);

                        }
                    }
                }
                else
                {
                    Tb_newmarid.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入的物料编码为空，请重新输入！');", true);

                }
            }
        }



        protected void Tb_newmarid_Textchanged(object sender, EventArgs e)
        {
            string marid = "";
            string marname = "";
            string sqltext = "";
            DataTable glotb = new DataTable();
            TextBox Tb_newmarid = (TextBox)sender;//定义TextBox
            RepeaterItem Reitem = (RepeaterItem)Tb_newmarid.Parent;//repeater中的item
            if (Tb_newmarid.Text.ToString().Contains("|"))
            {
                marid = Tb_newmarid.Text.Substring(0, Tb_newmarid.Text.ToString().IndexOf("|"));
                marname = Tb_newmarid.Text.Substring(Tb_newmarid.Text.ToString().IndexOf("|") + 1);
                sqltext = "SELECT MNAME,GUIGE,CAIZHI,GB,PURCUNIT,FUZHUUNIT,MWEIGHT FROM TBMA_MATERIAL WHERE ID='" + marid + "' ORDER BY ID";
                glotb = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (glotb.Rows.Count > 0)
                {
                    Tb_newmarid.Text = marid;
                    ((Label)Reitem.FindControl("MP_NEWMARNAME")).Text = glotb.Rows[0]["MNAME"].ToString();
                    ((Label)Reitem.FindControl("MP_NEWGUIGE")).Text = glotb.Rows[0]["GUIGE"].ToString();
                    ((Label)Reitem.FindControl("MP_NEWCAIZHI")).Text = glotb.Rows[0]["CAIZHI"].ToString();
                    ((Label)Reitem.FindControl("MP_NEWGUOBIAO")).Text = glotb.Rows[0]["GB"].ToString();
                    ((Label)Reitem.FindControl("MP_NEWUNIT")).Text = glotb.Rows[0]["PURCUNIT"].ToString();
                    ((Label)Reitem.FindControl("MP_NEWFZUNIT")).Text = glotb.Rows[0]["FUZHUUNIT"].ToString();
                    //单位换算
                    //主单位千克，理论重量不为0，数量与辅助数量均不为0
                    if (glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "kg" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim()!="张")
                    {
                        if ((glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim()))>0)
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim()))).ToString("0.0000").Trim();//板
                        }
                        else
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim()))).ToString("0.0000").Trim();
                        }
                    }


                    //主单位吨，理论重量不为0，数量与辅助数量均不为0
                    else if (glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "T" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                    {
                        if ((glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) / 1000).ToString("0.0000").Trim();//板
                        }
                        else
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) / 1000).ToString("0.0000").Trim();
                        }
                    }


                    //辅助单位千克，理论重量不为0，数量与辅助数量均不为0
                    else if (glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "kg" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                    {
                        if ((glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim()))).ToString("0.0000").Trim();//板
                        }
                        else
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim()))).ToString("0.0000").Trim();
                        }
                    }


                    //辅助单位吨，理论重量不为0，数量与辅助数量均不为0
                    else if (glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "T" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                    {
                        if ((glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) / 1000).ToString("0.0000").Trim();//板
                        }
                        else
                        {
                            ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) / 1000).ToString("0.0000").Trim();
                        }
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入的物料编码不存在，请重新输入！');", true);
                }
            }
            else
            {
                if (!(Tb_newmarid.Text == "" || Tb_newmarid.Text == DBNull.Value.ToString()))
                {
                    sqltext = "SELECT ID,MNAME,GUIGE,CAIZHI,GB,PURCUNIT,FUZHUUNIT,MWEIGHT FROM TBMA_MATERIAL WHERE ID='" + Tb_newmarid.Text.Replace(" ", "") + "' OR HMCODE='" + Tb_newmarid.Text.Replace(" ", "") + "' ORDER BY ID";
                    glotb = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (glotb.Rows.Count == 1)
                    {
                        ((TextBox)Reitem.FindControl("MP_NEWMARID")).Text = glotb.Rows[0]["ID"].ToString();
                        ((Label)Reitem.FindControl("MP_NEWMARNAME")).Text = glotb.Rows[0]["MNAME"].ToString();
                        ((Label)Reitem.FindControl("MP_NEWGUIGE")).Text = glotb.Rows[0]["GUIGE"].ToString();
                        ((Label)Reitem.FindControl("MP_NEWCAIZHI")).Text = glotb.Rows[0]["CAIZHI"].ToString();
                        ((Label)Reitem.FindControl("MP_NEWGUOBIAO")).Text = glotb.Rows[0]["GB"].ToString();
                        ((Label)Reitem.FindControl("MP_NEWUNIT")).Text = glotb.Rows[0]["PURCUNIT"].ToString();
                        ((Label)Reitem.FindControl("MP_NEWFZUNIT")).Text = glotb.Rows[0]["FUZHUUNIT"].ToString();


                        //单位换算
                        //主单位千克，理论重量不为0，数量与辅助数量均不为0
                        if (glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "kg" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                        {
                            if ((glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim()))).ToString("0.0000").Trim();//板
                            }
                            else
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim()))).ToString("0.0000").Trim();
                            }
                        }


                        //主单位吨，理论重量不为0，数量与辅助数量均不为0
                        else if (glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "T" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                        {
                            if ((glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) / 1000).ToString("0.0000").Trim();//板
                            }
                            else
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) / 1000).ToString("0.0000").Trim();
                            }
                        }


                        //辅助单位千克，理论重量不为0，数量与辅助数量均不为0
                        else if (glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "kg" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                        {
                            if ((glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim()))).ToString("0.0000").Trim();//板
                            }
                            else
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim()))).ToString("0.0000").Trim();
                            }
                        }


                        //辅助单位吨，理论重量不为0，数量与辅助数量均不为0
                        else if (glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() == "T" && glotb.Rows[0]["MWEIGHT"].ToString().Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "0" && ((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim() != "" && ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text.Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "" && glotb.Rows[0]["PURCUNIT"].ToString().Trim() != "张" && glotb.Rows[0]["FUZHUUNIT"].ToString().Trim() != "张")
                        {
                            if ((glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "平米" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "m2" || glotb.Rows[0]["PURCUNIT"].ToString().Trim() == "M2") && (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) > 0)
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["GUIGE"].ToString().Trim())) / 1000).ToString("0.0000").Trim();//板
                            }
                            else
                            {
                                ((TextBox)Reitem.FindControl("TMP_NEWNUMB")).Text = ((CommonFun.ComTryDouble(((TextBox)Reitem.FindControl("TMP_NEWNUMA")).Text.Trim())) * (CommonFun.ComTryDouble(glotb.Rows[0]["MWEIGHT"].ToString().Trim())) / 1000).ToString("0.0000").Trim();
                            }
                        }

                    }
                    else
                    {
                        if (glotb.Rows.Count > 1)
                        {
                            Tb_newmarid.Text = "";
                            Tb_newmarid.Focus();
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入的物料编码不完整，请重新输入！');", true);
                            return;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入的物料编码不存在，请重新输入！');", true);

                        }
                    }
                }
                else
                {
                    Tb_newmarid.Focus();
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入的物料编码为空，请重新输入！');", true);

                }
            }
        }
        protected void Marreplace_edit_repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {

            }
        }
    }
}
