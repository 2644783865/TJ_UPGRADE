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

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Reject_Product_Add : System.Web.UI.Page
    {
        string orderid = "";
        string action = "";
        public string declare;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
            {

                action = Request.QueryString["action"].ToString();
            }
            if (Request.QueryString["ID"] != null)
            {
                orderid = Request.QueryString["ID"].ToString();
            }
            if (!IsPostBack)
            {
                //添加时先创建一个全球唯一标识，存储在一个lable中，用来关联合同与附件，避免因合同号占用而导致附件关联错误
                if (action == "add")
                {
                    Guid tempid = Guid.NewGuid();
                    lbl_UNID.Text = tempid.ToString();
                }
                BindDep();
                BindPSR();
                InitPage();

                //如果是生产部，则显示是否已阅的按钮
                if (Session["UserDeptID"].ToString() == "04")
                {
                    LbtnDeal.Visible = true;
                }
            }
            InitUpload();

        }



        //初始附件上传控件
        private void InitUpload()
        {
            if (lb_orderid.Text.Trim() != "")
            {
                UploadAttachments1.Visible = true;
                UploadAttachments1.uf_code = lbl_UNID.Text;
                UploadAttachments1.uf_type = 1;
            }
            else
            {
                UploadAttachments1.Visible = false;
            }
            //******************************
            //2017.1.10修改
            if (action == "add" || action == "edit")
            {
                UploadAttachments1.uf_upload_del = true;
            }
            if (action == "view" || action == "audit" || action == "change")
            {
                UploadAttachments1.uf_upload_del = false;
            }
        }

        public string Rev_id
        {
            get
            {
                return lb_orderid.Text;
            }
        }


        //判断载入状态，查看审核添加or浏览
        private void InitPage()
        {
            if (action == "add")//添加
            {
                lb_orderid.Text = Create_orderid();   //创建不合格通知单单号

                Panel2.Visible = false;   //评审
                Panel3.Visible = false;   //验证



                tb_bzr.Text = Session["UserName"].ToString();   //编制人

                /*先删除多余的附件，即还没有保存单据就已经点了上传的附件,此处只删除了记录，并没有删除文件，待解决*/
                string sqldel = "delete from TBQC_FILEUPLOAD where UF_CODE='" + lb_orderid.Text.Trim() + "'";   //删除ID号下附件记录
                DBCallCommon.ExeSqlText(sqldel);   //执行删除语句
                /*********************************************/

            }
            else if (action == "view")//查看
            {
                lb_orderid.Text = orderid;   //单号
                cblMainDep.Enabled = false;
                cblCopyDep.Enabled = false;
                Panel1.Enabled = false;
                Panel2.Enabled = false;
                Panel3.Enabled = false;


                LbtnSubmit.Visible = false;
                tr_psr.Visible = false;    //审核人选择名单
                tr_spr.Visible = false;
                tr_zlb.Visible = false;//质量部张陈永秀
                tr_zgld.Visible = false;
                BindBasicData();           //绑定不合格通知单的基本信息
                BindRevInfo();             //绑定审核信息
                ControlEnable();           //控制输入，审核阶段
            }
            else if (action == "audit" || action == "change")//审核或审批人在审批后要修改审批意见
            {

                lb_orderid.Text = orderid;
                Panel1.Enabled = false;    //基本信息不可更改
                cblCopyDep.Enabled = false;
                cblMainDep.Enabled = false;
                tr_psr.Visible = false;    //审核人选择名单不可见
                tr_spr.Visible = false;
                tr_zlb.Visible = false;//质量部张陈永秀
                tr_zgld.Visible = false;
                BindBasicData();           //绑定不合格通知单的基本信息 
                BindRevInfo();             //绑定审核信息

                Pal_qf.Enabled = false;    //评审人不可输入
                Pal_jsfzr.Enabled = false; //技术负责人不可输入

         

                Pal_zlfzr.Enabled = false;//质量负责人不可输入


                Pal_zgld.Enabled = false;
                Pal_yzr.Enabled = false;   //验证人

                ControlEnable();           //控制输入，审核阶段

            }
            else if (action == "edit")     //编制人修改单据内容（不能修改审批人意见）
            {
                lb_orderid.Text = orderid;
                Panel2.Enabled = false;
                Panel3.Enabled = false;
                tr_psr.Visible = false;
                tr_spr.Visible = false;
                tr_zlb.Visible = false;//质量部张陈永秀
                tr_zgld.Visible = false;
                BindBasicData();
                BindRevInfo();
            }

        }






        //控制输入，审核阶段
        private void ControlEnable()
        {
            string time = DateTime.Now.ToString("yy-MM-dd");

            if (hidPSRID.Value == Session["UserID"].ToString() && hidState.Value == "1")
            {
                Pal_qf.Enabled = true;
                tb_opinion1.BorderColor = System.Drawing.Color.Orange;
                lb_psr.Text = Session["UserName"].ToString();
                lb_psrsj.Text = time;
            }

            if (hidSPR_ZL_ID.Value == Session["UserID"].ToString() && hidState.Value == "7")
            {
                Pal_zlfzr.Enabled = true;
                tb_opinion7.BorderColor = System.Drawing.Color.Orange;
                lb_zlfzr.Text = Session["UserName"].ToString();
                //审核阶段，又技术部长制定抄送部门
                cblCopyDep.Enabled = true;
                lb_zlfzrsj.Text = time;
            }

            if (hidSPRID.Value == Session["UserID"].ToString() && hidState.Value == "2")
            {
                Pal_jsfzr.Enabled = true;
                tb_opinion2.BorderColor = System.Drawing.Color.Orange;
                lb_jsfzr.Text = Session["UserName"].ToString();
                //审核阶段，又技术部长制定抄送部门
                cblCopyDep.Enabled = true;
                lb_jsfzrsj.Text = time;
            }
            if (hidZGLDID.Value == Session["UserID"].ToString() && hidState.Value == "6")
            {
                Pal_zgld.Enabled = true;
                tb_opinion3.BorderColor = System.Drawing.Color.Orange;
                lblZgld.Text = Session["UserName"].ToString();
                //审核阶段，又技术部长制定抄送部门

                lb_zgldsj.Text = time;
            }

            if (hidYZRID.Value == Session["UserID"].ToString() && hidState.Value == "3")
            {
                Pal_yzr.Enabled = true;
                tb_opinion4.BorderColor = System.Drawing.Color.Orange;
                lb_yzr.Text = Session["UserName"].ToString();
                lb_yzrsj.Text = time;
            }

            if (Cbxl_rank.SelectedValue == "0")
            {
                Pal_zgld.Visible = false;
            }
            else
            {
                Pal_zgld.Visible = true;
            }

        }




        //绑定不合格通知单的基本信息
        private void BindBasicData()
        {
            string sql = "select * from View_TBQC_RejectPro_Info where Order_id='" + orderid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                //唯一编号
                lbl_UNID.Text = dt.Rows[0]["GUID"].ToString();
                tb_pjinfo.Text = dt.Rows[0]["PJ_NAME"].ToString();
                tb_conId.Text = dt.Rows[0]["CON_ID"].ToString();
                tb_engId.Text = dt.Rows[0]["ENGID"].ToString();
                tb_cpmc.Text = dt.Rows[0]["BJMC"].ToString();
                //绑定主送部门
                for (int i = 0; i < cblMainDep.Items.Count; i++)
                {
                    if (cblMainDep.Items[i].Value == dt.Rows[0]["Main_dep"].ToString())
                    {
                        cblMainDep.Items[i].Selected = true;
                    }
                }
                //绑定抄送部门
                string[] copeDep = dt.Rows[0]["Copy_dep"].ToString().Split('|');
                for (int i = 0; i < copeDep.Length; i++)
                {
                    for (int j = 0; j < cblCopyDep.Items.Count; j++)
                    {
                        if (cblCopyDep.Items[j].Value == copeDep[i])
                        {
                            cblCopyDep.Items[j].Selected = true;
                        }
                    }
                }

                tb_th.Text = dt.Rows[0]["TH"].ToString();
                tb_bzr.Text = dt.Rows[0]["ST_NAME"].ToString();
                //   Cbxl_rank.SelectedValue = dt.Rows[0]["Rank"].ToString();
                //  txt_declare.Text = dt.Rows[0]["QKMS"].ToString();
                //    hidDeclare.Value = dt.Rows[0]["QKMS"].ToString();
                txtZRF.Text = dt.Rows[0]["ZRF"].ToString();
                declare = dt.Rows[0]["QKMS"].ToString();
                num.Text = dt.Rows[0]["NUM"].ToString();
                txtZrbz.Text = dt.Rows[0]["DUTY_DEP"].ToString();
                hidState.Value = dt.Rows[0]["STATE"].ToString();
                hidYZRID.Value = dt.Rows[0]["BZR"].ToString();
                //不合格类型1
                string[] type = dt.Rows[0]["Reject_Type"].ToString().Split('|');
                for (int i = 0; i < type.Length; i++)
                {
                    for (int j = 0; j < Cbxl_reason1.Items.Count; j++)
                    {
                        if (Cbxl_reason1.Items[j].Value == type[i])
                        {
                            Cbxl_reason1.Items[j].Selected = true;
                        }
                    }
                }

                //不合格类型2
                string[] type2 = dt.Rows[0]["Reject_Type2"].ToString().Split('|');
                for (int i = 0; i < type2.Length; i++)
                {
                    for (int j = 0; j < Cbxl_reason2.Items.Count; j++)
                    {
                        if (Cbxl_reason2.Items[j].Value == type2[i])
                        {
                            Cbxl_reason2.Items[j].Selected = true;
                        }
                    }
                }

            }
        }


        //绑定【审核】信息
        private void BindRevInfo()
        {
            string sqltext = "select * from View_TBQC_RejectPro_Info_Detail where Order_id='" + orderid + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {

                lb_psr.Text = dt.Rows[0]["PSR_NAME"].ToString();
                lb_jsfzr.Text = dt.Rows[0]["SPR_NAME"].ToString();
                lblZgld.Text = dt.Rows[0]["ZGLD_NAME"].ToString();
                hidPSRID.Value = dt.Rows[0]["PSR_ID"].ToString();
                hidSPRID.Value = dt.Rows[0]["SPR_ID"].ToString();

                //技术部
                lb_zlfzr.Text = dt.Rows[0]["SPR_ZL_NAME"].ToString();
                hidSPR_ZL_ID.Value = dt.Rows[0]["SPR_ZL_ID"].ToString();

                lb_zlfzrsj.Text = dt.Rows[0]["SPR_ZL_TIME"].ToString();
                tb_opinion7.Text = dt.Rows[0]["SPR_ZL_NOTE"].ToString();
                zlb_result.SelectedValue = dt.Rows[0]["SPR_ZL_RESULT"].ToString();

                Cbxl_rank.SelectedValue = dt.Rows[0]["RANK"].ToString();
                DealMethods.SelectedValue = dt.Rows[0]["CZFS"].ToString();
                lb_psrsj.Text = dt.Rows[0]["PSR_TIME"].ToString();
                tb_opinion1.Text = dt.Rows[0]["PSR_NOTE"].ToString();



                lb_jsfzrsj.Text = dt.Rows[0]["SPR_TIME"].ToString();
                tb_opinion2.Text = dt.Rows[0]["SPR_NOTE"].ToString();
                jsb_result.SelectedValue = dt.Rows[0]["SPR_RESULT"].ToString();


                lb_yzr.Text = dt.Rows[0]["ST_NAME"].ToString();
                lb_yzrsj.Text = dt.Rows[0]["YZ_TIME"].ToString();
                tb_opinion4.Text = dt.Rows[0]["YZ_NOTE"].ToString();
                check_result.SelectedValue = dt.Rows[0]["YZ_RESULT"].ToString();



                hidZGLDID.Value = dt.Rows[0]["ZGLD_ID"].ToString();
                tb_opinion3.Text = dt.Rows[0]["ZGLD_NOTE"].ToString();
                lb_zgldsj.Text = dt.Rows[0]["ZGLD_TIME"].ToString();
                zgld_result.SelectedValue = dt.Rows[0]["ZGLD_RESULT"].ToString();

            }

        }

        //创建不合格评审单号
        private string Create_orderid()
        {
            string id = "";
            id = "ZCZJ.BHGPPSD";

            id += DateTime.Now.Year.ToString() + ".";
            string sqlText = "select Top 1 Order_id from TBQC_RejectPro_Info where Order_id like '" + id + "%'  Order by Order_id desc";//找出当天的最大号
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.HasRows)
            {
                dr.Read();
                string[] bh = dr["Order_id"].ToString().Split('.');
                int maxID = Convert.ToInt16(bh[bh.Length - 1]) + 1;//取数组最后一个
                dr.Close();
                id += maxID.ToString().PadLeft(4, '0');
            }
            else
            {
                id += "0001";
            }
            return id;
        }
        /// <summary>
        /// 绑定主送、抄送部门
        /// </summary>
        private void BindDep()
        {
            string sql = "select DEP_CODE,DEP_NAME from dbo.TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            BindCheckboxDep(cblMainDep, sql);
            for (int k = 0; k < cblMainDep.Items.Count; k++)
            {
                cblMainDep.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个

            }
            BindCheckboxDep(cblCopyDep, sql);

        }
        //为主送部门、抄送部门绑定
        private void BindCheckboxDep(CheckBoxList cbl, string sqltext)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            cbl.DataSource = dt;
            cbl.DataTextField = "DEP_NAME";//部门名称
            cbl.DataValueField = "DEP_CODE";//部门编号 
            cbl.DataBind();


        }
        //绑定评审人
        private void BindPSR()
        {

            string sql = "";

            //技术部李小婷
            sql = "select * from TBDS_STAFFINFO where  ST_DEPID='12' and ST_SEQUEN='管理' and ST_PD='0' order by ST_WORKNO";
            BindCheckbox(cbl_zlb, sql);


            sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID='03' and ST_POSITION like '03%' and ST_PD='0'";
            BindCheckbox(cbl_psr, sql);

            sql = "select * from TBDS_STAFFINFO where  ST_DEPID='03' and ST_SEQUEN='管理' and ST_PD='0'";
            BindCheckbox(cbl_spr, sql);


            sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID='01' and ST_POSITION like '01%' and ST_PD='0' order by ST_WORKNO";

            BindCheckbox(cbl_zgjl, sql);
        }


        //为评审人列表赋值，填充评审人
        private void BindCheckbox(CheckBoxList cbl, string sqltext)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            cbl.DataSource = dt;
            cbl.DataTextField = "ST_NAME";//领导的姓名
            cbl.DataValueField = "ST_ID";//领导的编号

            cbl.DataBind();

            for (int k = 0; k < cbl.Items.Count; k++)
            {
                cbl.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个

            }
        }




        //提交事件 （添加、编辑、审核、变更）
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == "add" || action == "edit")
            {
                Write_Info();
            }

            else if (action == "audit")
            {
                Submit_Rev();
            }

            else if (action == "change")
            {
                Change_Rev();
            }


        }



        private void Write_Info()
        {
            //全球唯一标识符
            string guid = lbl_UNID.Text.Trim();
            string orderid = lb_orderid.Text.Trim();//单号
            string pjid = tb_conId.Text.Trim();// 合同号
            string pjname = tb_pjinfo.Text.Trim();
            string engId = tb_engId.Text.Trim();//任务号
            string bjmc = tb_cpmc.Text.Trim(); // 部件名称
            string maindep = cblMainDep.SelectedValue; // 主送部门
            string copydep = "";//  抄送部门
            for (int i = 0; i < cblCopyDep.Items.Count; i++)
            {
                if (cblCopyDep.Items[i].Selected == true)
                {
                    copydep += cblCopyDep.Items[i].Value + "|";
                }
            }
            if (copydep != "")
            {
                copydep = copydep.Trim('|');
            }
            string tuhao = tb_th.Text.Trim(); // 图号
            string bzr = Session["UserID"].ToString();//编制人

            // string qkms = txt_declare.Text.Trim(); // 情况描述
            string qkms = txtDeclare.Text;
            string jianshu = num.Text.Trim();
            string dutydep = txtZrbz.Text.Trim();
            string zrf = txtZRF.Text.Trim();

            /*********************************/
            //不合格品类型1 多选则用“|”隔开 
            string rejecttype1 = "";
            for (int j = 0; j < Cbxl_reason1.Items.Count; j++)
            {
                if (Cbxl_reason1.Items[j].Selected == true)
                {
                    rejecttype1 += Cbxl_reason2.Items[j].Value + "|";
                }
            }
            if (rejecttype1 != "")
            {
                rejecttype1 = rejecttype1.Substring(0, rejecttype1.Length - 1);
            }
            //不合格品类型2 多选则用“|”隔开 
            string rejecttype2 = "";
            for (int j = 0; j < Cbxl_reason2.Items.Count; j++)
            {
                if (Cbxl_reason2.Items[j].Selected == true)
                {
                    rejecttype2 += Cbxl_reason2.Items[j].Value + "|";
                }
            }
            if (rejecttype2 != "")
            {
                rejecttype2 = rejecttype2.Substring(0, rejecttype2.Length - 1);
            }
            /**********************************/


            if (Check_Input())
            {
                List<string> sqlstr = new List<string>();

                if (action == "add")
                {
                    //重新检查单号，防止多人同时添加造成单号重复
                    lb_orderid.Text = orderid = Create_orderid();
                    //插入不合格品通知单主表
                    string sql1 = "insert into TBQC_RejectPro_Info (ORDER_ID,MAIN_DEP,COPY_DEP,CON_ID,PJ_NAME,BJMC,TH,BZR,QKMS," +
                        " REJECT_TYPE,REJECT_TYPE2,DUTY_DEP,GUID,NUM,STATE,ZRF,Inform_time,ENGID) VALUES('" + orderid + "','" + maindep + "','" + copydep + "','" + pjid + "','" + pjname + "'," + " '" + bjmc + "','" + tuhao + "','" + bzr + "','" + qkms + "','" + rejecttype1 + "','" + rejecttype2 + "'," +
                        " '" + dutydep + "','" + guid + "','" + jianshu + "','7','" + zrf + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + engId + "')";
                    sqlstr.Add(sql1);
                    //插入不合格品通知单评审表

                    //查找质量部负责人
                    //string sql_zlfzr = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_ID='69'  and ST_PD='0'";
                    string sql_zlfzr = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_POSITION='1205' and ST_PD='0'";
                    DataTable dt_zlfzr = DBCallCommon.GetDTUsingSqlText(sql_zlfzr);
                    string sql2 = "insert into TBQC_RejectPro_Rev (Rev_id,SPR_ZL_NAME,SPR_ZL_ID,SPR_ZL_NOTE,SPR_ZL_RESULT,SPR_ZL_TIME, PSR_ID, PSR_NOTE, PSR_RESULT, PSR_TIME, SPR_ID, SPR_NOTE, SPR_RESULT, SPR_TIME, YZ_NOTE, YZ_TIME, ZGLD_ID, ZGLD_NOTE, ZGLD_RESULT, ZGLD_TIME) Values('" + orderid + "','" + dt_zlfzr.Rows[0]["ST_NAME"].ToString() + "','" + cbl_zlb.SelectedValue + "','','','','" + cbl_psr.SelectedValue + "','','','','" + cbl_spr.SelectedValue + "','','','','','','" + cbl_zgjl.SelectedValue + "','','','')";
                    sqlstr.Add(sql2);

                }
                else if (action == "edit")
                {
                    string sql6 = "update TBQC_RejectPro_Info set MAIN_DEP='" + maindep + "',COPY_DEP='" + copydep + "',CON_ID='" + pjid + "'," +
                        "PJ_NAME='" + pjname + "',BJMC='" + bjmc + "',TH='" + tuhao + "',BZR='" + bzr + "',QKMS='" + qkms + "'," +
                        " REJECT_TYPE='" + rejecttype1 + "',REJECT_TYPE2='" + rejecttype2 + "',DUTY_DEP='" + dutydep + "',NUM='" + jianshu + "',ZRF='" + zrf + "',ENGID='" + engId + "'" +
                        " where ORDER_ID='" + orderid + "'";
                    sqlstr.Add(sql6);
                }
                DBCallCommon.ExecuteTrans(sqlstr);
                if (action == "add")
                {
                    //制单人提交后向签发人发送邮件通知
                    this.MailSentTo(1);
                }
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('提交成功！');window.location.href='QC_Reject_Product.aspx';", true);
            }
        }

        private void Submit_Rev()
        {
            List<string> sqlstr = new List<string>();
            string time = DateTime.Now.ToString("yyyy-MM-dd");



            if (hidPSRID.Value == Session["UserID"].ToString() && hidState.Value == "1")    //评审人
            {
                if (Cbxl_rank.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择不合格分级！');", true); return;
                }
                else if (DealMethods.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择处置方式！');", true); return;
                }
                else if (tb_opinion1.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写意见再提交！');", true); return;
                }
                else
                {
                    string sql1 = "update TBQC_RejectPro_Rev set PSR_NOTE='" + tb_opinion1.Text + "',PSR_TIME='" + time + "' where Rev_id='" + orderid + "'";
                    sqlstr.Add(sql1);
                    sql1 = "update TBQC_RejectPro_Info set STATE='2',RANK='" + Cbxl_rank.SelectedValue + "',CZFS='" + DealMethods.SelectedValue + "' where  Order_id='" + orderid + "'";
                    sqlstr.Add(sql1);

                    //签发人审完后向技术部发送邮件通知
                    this.MailSentTo(2);
                }
            }
            if (hidSPR_ZL_ID.Value == Session["UserID"].ToString() && hidState.Value == "7")      //技术部负责人
            {
                if (zlb_result.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择同意或者不同意！');", true); return;
                }
                else
                {
                    if (zlb_result.SelectedValue == "1")
                    {
                        if (tb_opinion7.Text.Trim() == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写意见再提交！');", true); return;
                        }
                        else
                        {
                            string update_czfs = "update TBQC_RejectPro_Rev set SPR_ZL_RESULT='" + zlb_result.SelectedValue + "',SPR_ZL_NOTE='" + tb_opinion7.Text + "',SPR_ZL_TIME='" + time + "'" +
                                " where Rev_id='" + orderid + "'";

                            sqlstr.Add(update_czfs);
                            update_czfs = "update TBQC_RejectPro_Info set STATE='4' where  Order_id='" + orderid + "'";
                            sqlstr.Add(update_czfs);
                        }

                    }
                    else
                    {
                        if (cblCopyDep.SelectedIndex == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择抄送部门！');", true); return;
                        }
                        if (tb_opinion7.Text.Trim() == "")
                        {
                            tb_opinion7.Text = "同意";
                        }
                        string state = "";
                        string sql2 = "update TBQC_RejectPro_Rev set SPR_ZL_NOTE='" + tb_opinion7.Text + "'" +
                                                         " ,SPR_ZL_RESULT='" + zlb_result.SelectedValue + "',SPR_ZL_TIME='" + time + "' where Rev_id='" + orderid + "'";
                        sqlstr.Add(sql2);
                        //质量部负责人可以自行选择抄送部门
                        string copyDep = "";
                        for (int i = 0; i < cblCopyDep.Items.Count; i++)
                        {
                            if (cblCopyDep.Items[i].Selected == true)
                            {
                                copyDep += cblCopyDep.Items[i].Value + "|";
                            }
                        }
                        if (copyDep != "")
                        {
                            copyDep = copyDep.Trim('|');
                        }

                        string sqlchangestate = "update TBQC_RejectPro_Info set STATE='1',Copy_dep='" + copyDep + "' where Order_id='" + orderid + "'";
                        sqlstr.Add(sqlchangestate);


                    }


                }

            }
            if (hidSPRID.Value == Session["UserID"].ToString() && hidState.Value == "2")      //技术部负责人
            {
                if (jsb_result.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择同意或者不同意！');", true); return;
                }
                else
                {
                    if (jsb_result.SelectedValue == "1")
                    {
                        if (tb_opinion2.Text.Trim() == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写意见再提交！');", true); return;
                        }
                        else
                        {
                            string update_czfs = "update TBQC_RejectPro_Rev set SPR_RESULT='" + jsb_result.SelectedValue + "',SPR_NOTE='" + tb_opinion2.Text + "',SPR_TIME='" + time + "'" +
                                " where Rev_id='" + orderid + "'";

                            sqlstr.Add(update_czfs);
                            update_czfs = "update TBQC_RejectPro_Info set STATE='4' where  Order_id='" + orderid + "'";
                            sqlstr.Add(update_czfs);
                        }

                    }
                    else
                    {
                        if (cblCopyDep.SelectedIndex == -1)
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择抄送部门！');", true); return;
                        }
                        if (tb_opinion2.Text.Trim() == "")
                        {
                            tb_opinion2.Text = "同意";
                        }
                        string state = "";
                        if (Cbxl_rank.SelectedValue == "0")
                        {
                            state = "3";
                        }
                        else
                        {
                            state = "6";
                        }

                        string sql2 = "update TBQC_RejectPro_Rev set SPR_NOTE='" + tb_opinion2.Text + "'" +
                                                         " ,SPR_RESULT='" + jsb_result.SelectedValue + "',SPR_TIME='" + time + "' where Rev_id='" + orderid + "'";
                        sqlstr.Add(sql2);
                        //技术部负责人可以自行选择抄送部门
                        string copyDep = "";
                        for (int i = 0; i < cblCopyDep.Items.Count; i++)
                        {
                            if (cblCopyDep.Items[i].Selected == true)
                            {
                                copyDep += cblCopyDep.Items[i].Value + "|";
                            }
                        }
                        if (copyDep != "")
                        {
                            copyDep = copyDep.Trim('|');
                        }

                        string sqlchangestate = "update TBQC_RejectPro_Info set STATE='" + state + "',Copy_dep='" + copyDep + "' where Order_id='" + orderid + "'";
                        sqlstr.Add(sqlchangestate);


                    }


                }

            }

            if (hidZGLDID.Value == Session["UserID"].ToString() && hidState.Value == "6")
            {
                if (zgld_result.SelectedValue == "1")
                {
                    if (tb_opinion3.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写意见再提交！');", true); return;
                    }
                    else
                    {
                        string update_czfs = "update TBQC_RejectPro_Rev set ZGLD_RESULT='" + zgld_result.SelectedValue + "',ZGLD_NOTE='" + tb_opinion3.Text + "',ZGLD_TIME='" + time + "'" +
                            " where Rev_id='" + orderid + "'";

                        sqlstr.Add(update_czfs);
                        update_czfs = "update TBQC_RejectPro_Info set STATE='4' where  Order_id='" + orderid + "'";
                        sqlstr.Add(update_czfs);
                    }

                }
                else
                {

                    if (tb_opinion3.Text.Trim() == "")
                    {
                        tb_opinion3.Text = "同意";
                    }

                    string sql2 = "update TBQC_RejectPro_Rev set ZGLD_NOTE='" + tb_opinion3.Text + "'" +
                                                     " ,ZGLD_RESULT='" + zgld_result.SelectedValue + "',ZGLD_TIME='" + time + "' where Rev_id='" + orderid + "'";
                    sqlstr.Add(sql2);


                    string sqlchangestate = "update TBQC_RejectPro_Info set STATE='3'  where Order_id='" + orderid + "'";
                    sqlstr.Add(sqlchangestate);


                }
            }

            if (hidYZRID.Value == Session["UserID"].ToString() && hidState.Value == "3")     //验证人，即制单人
            {
                if (check_result.SelectedIndex != -1)
                {
                    string sql5 = "update TBQC_RejectPro_Rev set YZ_NOTE='" + tb_opinion4.Text + "'" +
                                       " ,YZ_TIME='" + time + "',YZ_RESULT='" + check_result.SelectedValue + "' where Rev_id='" + orderid + "'";
                    sqlstr.Add(sql5);
                    string sql6 = "update TBQC_RejectPro_Info set STATE='5' where Order_id='" + orderid + "'";
                    sqlstr.Add(sql6);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写验证结果再提交！');", true); return;
                }

            }
            DBCallCommon.ExecuteTrans(sqlstr);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('提交成功！');window.location.href='QC_Reject_Product.aspx';", true);
        }

        private void Change_Rev()
        {
            List<string> sqlstr = new List<string>();
            string time = DateTime.Now.ToString("yyyy-MM-dd");



            if (hidPSRID.Value == Session["UserID"].ToString() && hidState.Value == "1")    //评审人
            {
                if (Cbxl_rank.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择不合格分级！');", true); return;
                }
                else if (DealMethods.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择处置方式！');", true); return;
                }
                else if (tb_opinion1.Text.Trim() == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写意见再提交！');", true); return;
                }
                else
                {
                    string sql1 = "update TBQC_RejectPro_Rev set PSR_NOTE='" + tb_opinion1.Text + "',PSR_TIME='" + time + "' where Rev_id='" + orderid + "'";
                    sqlstr.Add(sql1);
                    sql1 = "update TBQC_RejectPro_Info set STATE='2',RANK='" + Cbxl_rank.SelectedValue + "',CZFS='" + DealMethods.SelectedValue + "' where Order_id='" + orderid + "'";
                    sqlstr.Add(sql1);

                    //签发人审完后向技术部发送邮件通知
                    //   this.MailSentTo(1);
                }
            }
            if (hidSPR_ZL_ID.Value == Session["UserID"].ToString() && (hidState.ToString() == "7"))    //技术部负责人
            {
                if (zlb_result.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择同意或者不同意！');", true); return;
                }
                else
                {
                    if (zlb_result.SelectedValue == "1")
                    {
                        if (tb_opinion7.Text.Trim() == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写意见再提交！');", true); return;
                        }
                        else
                        {
                            string update_czfs = "update TBQC_RejectPro_Rev set SPR_RESULT='" + zlb_result.SelectedValue + "',SPR_NOTE='" + tb_opinion7.Text + "'SPR_TIME='" + time + "'" +
                                " where Rev_id='" + orderid + "'";

                            sqlstr.Add(update_czfs);
                            update_czfs = "update TBQC_RejectPro_Info set STATE='4' where Order_id='" + orderid + "' ";
                            sqlstr.Add(update_czfs);
                            string to = DBCallCommon.GetEmailAddressByUserID(hidPSRID.Value);
                            string body = "您有一条被驳回的不合格品通知单记录，请尽快处理！" + "\n" + "任 务 号:" + tb_engId.Text + "\n" + "项 目 为：" + tb_pjinfo.Text + "\n" + "产 品 为：" + tb_cpmc.Text + "\n";
                            DBCallCommon.SendEmail(to, null, null, tb_engId.Text + "-" + tb_pjinfo.Text + "数字平台不合格品通知单", body);

                            to = DBCallCommon.GetEmailAddressByUserID(hidYZRID.Value);
                            body = "您有一条被驳回的不合格品通知单记录，请尽快处理！" + "\n" + "任 务 号:" + tb_engId.Text + "\n" + "项 目 为：" + tb_pjinfo.Text + "\n" + "产 品 为：" + tb_cpmc.Text + "\n";
                            DBCallCommon.SendEmail(to, null, null, tb_engId.Text + "-" + tb_pjinfo.Text + "数字平台不合格品通知单", body);
                        }

                    }
                    else
                    {
                        if (tb_opinion7.Text.Trim() == "")
                        {
                            tb_opinion7.Text = "同意";
                        }

                        string sql2 = "update TBQC_RejectPro_Rev set SPR_NOTE='" + tb_opinion7.Text + "'" +
                                                         " ,SPR_RESULT='" + zlb_result.SelectedValue + "',SPR_TIME='" + time + "' where Rev_id='" + orderid + "'";
                        sqlstr.Add(sql2);

                        //检查处置方式，当不为报废或返工时，如果领导已经审了，就直接把状态改为2，视为审批完，不再经过验证人审批
                        if (DealMethods.SelectedValue != "1")
                        {

                            string sqlchangestate = "update TBQC_RejectPro_Info set STATE='5' where Order_id='" + orderid + "'";
                            sqlstr.Add(sqlchangestate);
                        }
                        else
                        {
                            string sqlchangestate = "update TBQC_RejectPro_Info set STATE='3' where Order_id='" + orderid + "'";
                            sqlstr.Add(sqlchangestate);
                        }

                    }


                }

            }
            if (hidSPRID.Value == Session["UserID"].ToString() && (hidState.ToString() == "2"))    //技术部负责人
            {
                if (jsb_result.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择同意或者不同意！');", true); return;
                }
                else
                {
                    if (jsb_result.SelectedValue == "1")
                    {
                        if (tb_opinion2.Text.Trim() == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写意见再提交！');", true); return;
                        }
                        else
                        {
                            string update_czfs = "update TBQC_RejectPro_Rev set SPR_RESULT='" + jsb_result.SelectedValue + "',SPR_NOTE='" + tb_opinion2.Text + "'SPR_TIME='" + time + "'" +
                                " where Rev_id='" + orderid + "'";

                            sqlstr.Add(update_czfs);
                            update_czfs = "update TBQC_RejectPro_Info set STATE='4' where Order_id='" + orderid + "' ";
                            sqlstr.Add(update_czfs);
                            string to = DBCallCommon.GetEmailAddressByUserID(hidPSRID.Value);
                            string body = "您有一条被驳回的不合格品通知单记录，请尽快处理！" + "\n" + "任 务 号:" + tb_engId.Text + "\n" + "项 目 为：" + tb_pjinfo.Text + "\n" + "产 品 为：" + tb_cpmc.Text + "\n";
                            DBCallCommon.SendEmail(to, null, null, tb_engId.Text + "-" + tb_pjinfo.Text + "数字平台不合格品通知单", body);

                             to = DBCallCommon.GetEmailAddressByUserID(hidYZRID.Value);
                             body = "您有一条被驳回的不合格品通知单记录，请尽快处理！" + "\n" + "任 务 号:" + tb_engId.Text + "\n" + "项 目 为：" + tb_pjinfo.Text + "\n" + "产 品 为：" + tb_cpmc.Text + "\n";
                            DBCallCommon.SendEmail(to, null, null, tb_engId.Text + "-" + tb_pjinfo.Text + "数字平台不合格品通知单", body);
                        }

                    }
                    else
                    {
                        if (tb_opinion2.Text.Trim() == "")
                        {
                            tb_opinion2.Text = "同意";
                        }

                        string sql2 = "update TBQC_RejectPro_Rev set SPR_NOTE='" + tb_opinion2.Text + "'" +
                                                         " ,SPR_RESULT='" + jsb_result.SelectedValue + "',SPR_TIME='" + time + "' where Rev_id='" + orderid + "'";
                        sqlstr.Add(sql2);

                        //检查处置方式，当不为报废或返工时，如果领导已经审了，就直接把状态改为2，视为审批完，不再经过验证人审批
                        if (DealMethods.SelectedValue != "1")
                        {

                            string sqlchangestate = "update TBQC_RejectPro_Info set STATE='5' where Order_id='" + orderid + "'";
                            sqlstr.Add(sqlchangestate);
                        }
                        else
                        {
                            string sqlchangestate = "update TBQC_RejectPro_Info set STATE='3' where Order_id='" + orderid + "'";
                            sqlstr.Add(sqlchangestate);
                        }

                    }


                }

            }
            if (hidZGLDID.Value == Session["UserID"].ToString() && hidState.Value == "6")
            {
                if (zgld_result.SelectedValue == "1")
                {
                    if (tb_opinion3.Text.Trim() == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写意见再提交！');", true); return;
                    }
                    else
                    {
                        string update_czfs = "update TBQC_RejectPro_Rev set ZGLD_RESULT='" + zgld_result.SelectedValue + "',ZGLD_NOTE='" + tb_opinion3.Text + "',ZGLD_TIME='" + time + "'" +
                            " where Rev_id='" + orderid + "'";

                        sqlstr.Add(update_czfs);
                        update_czfs = "update TBQC_RejectPro_Info set STATE='4' where  Order_id='" + orderid + "'";
                        sqlstr.Add(update_czfs);
                    }

                }
                else
                {

                    if (tb_opinion3.Text.Trim() == "")
                    {
                        tb_opinion3.Text = "同意";
                    }

                    string sql2 = "update TBQC_RejectPro_Rev set ZGLD_NOTE='" + tb_opinion3.Text + "'" +
                                                     " ,ZGLD_RESULT='" + zgld_result.SelectedValue + "',ZGLD_TIME='" + time + "' where Rev_id='" + orderid + "'";
                    sqlstr.Add(sql2);
                    string sqlchangestate = "update TBQC_RejectPro_Info set STATE='3'  where Order_id='" + orderid + "'";
                    sqlstr.Add(sqlchangestate);


                }
            }

            if (hidYZRID.Value == Session["UserID"].ToString() && hidState.Value == "3")     //验证人，即制单人
            {
                if (check_result.SelectedIndex != -1)
                {
                    string sql5 = "update TBQC_RejectPro_Rev set YZ_NOTE='" + tb_opinion4.Text + "'" +
                                       " ,YZ_TIME='" + time + "',YZ_RESULT='" + check_result.SelectedValue + "' where Rev_id='" + orderid + "'";
                    sqlstr.Add(sql5);
                    string sql6 = "update TBQC_RejectPro_Info set STATE='5' where Order_id='" + orderid + "'";
                    sqlstr.Add(sql6);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写验证结果再提交！');", true); return;
                }

            }


            DBCallCommon.ExecuteTrans(sqlstr);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('提交成功！');window.location.href='QC_Reject_Product.aspx';", true);
        }

        //检查输入
        private bool Check_Input()
        {
            bool yesno = true;
            if (tb_pjinfo.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('项目不能为空！');", true);
                return yesno = false;
            }
            if (tb_conId.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('合同号不能为空！');", true);
                return yesno = false;
            }

            if (tb_cpmc.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('部件名称不能为空！');", true);
                return yesno = false;
            }
            if (cblMainDep.SelectedValue == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('主送部门不能为空！');", true);
                return yesno = false;
            }
            //if (cblCopyDep.SelectedValue == "")
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('抄送部门不能为空！');", true);
            //    return yesno = false;
            //}
            if (tb_th.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('图号不能为空！');", true);
                return yesno = false;
            }
            //if (Cbxl_rank.SelectedIndex == -1)
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择判定等级！');", true);
            //    return yesno = false;
            //}
            //if (txt_declare.Text == "")
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('情况描述不能为空！');", true);
            //    return yesno = false;
            //}
            //if (hidDeclare.Value=="")
            //{
            //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('情况描述不能为空！');", true);
            //    return yesno = false;
            //}
            if (Cbxl_reason1.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择不合格品类型！');", true);
                return yesno = false;
            }
            if (txtZrbz.Text == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('责任班组不能为空！');", true);
                return yesno = false;
            }

            if (Cbxl_reason2.SelectedIndex == -1)
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择不合格品类型！');", true);
                return yesno = false;
            }

            if (action == "add")
            {
                if (cbl_zlb.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择质量部负责人！');", true);
                    return yesno = false;
                }

                if (cbl_psr.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择签发人！');", true);
                    return yesno = false;
                }
                if (cbl_spr.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择技术部负责人！');", true);
                    return yesno = false;
                }

                if (cbl_zgjl.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择主管经理负责人！');", true);
                    return yesno = false;
                }

            }
            return yesno;
        }

        //批准人意见改变
        protected void pzr_result_Changed(object sender, EventArgs e)
        {
            //自动带出同意或不同意，且不同意时将技术部意见加删除线
            //if (pzr_result.SelectedIndex == 0)
            //{
            //    tb_opinion3.Text = "同意";
            //    tb_opinion2.Font.Strikeout = false;
            //    Pal_jsfzr.Enabled = false;
            //}
            //else if (pzr_result.SelectedIndex == 1)
            //{
            //    tb_opinion3.Text = "采用以下处理方式：";
            //    tb_opinion2.Font.Strikeout = true;
            //    Pal_jsfzr.Enabled = true;
            //    tb_opinion2.Enabled = false ;//不同意时可修改处置方式
            //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请重新勾选处置方式，否则以技术部意见为准!');", true); return;
            //}
        }

        //返回（查看--关闭窗口；其他--回到管理界面）
        protected void LbtnBack_Click(object sender, EventArgs e)
        {
            if (action == "view")
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "window.close();", true);

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "window.location.href='QC_Reject_Product.aspx';", true);
            }

        }

        protected void LbtnDeal_Click(object sender, EventArgs e)
        {
            string sql = "update TBQC_RejectPro_Info set MPSTATE='1' where Order_id='" + orderid + "'";
            DBCallCommon.ExeSqlText(sql);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "window.location.href='QC_Reject_Product.aspx';", true);

        }


        #region  "发送邮件部分"

        //邮件发送列表
        private void MailSentTo(int submittype)
        {
            string sendto = "";
            List<string> copyto = new List<string>();
            string sqltext = "";

            switch (submittype)
            {
                case 1:  //制单人发送给签发人
                    sqltext = "select DISTINCT [EMAIL] from TBQC_RejectPro_Rev as a,TBDS_STAFFINFO as b where a.Rev_id='" + lb_orderid.Text.Trim() + "'  and a.PSR_ID=b.ST_ID";
                    break;
                case 2:  //签发人 to 技术
                    sqltext = "select DISTINCT [EMAIL] from TBQC_RejectPro_Rev as a,TBDS_STAFFINFO as b where a.Rev_id='" + lb_orderid.Text.Trim() + "'  and a.SPR_ID=b.ST_ID";
                    break;
                case 3:  //技术部负责人 to 各部门

                    break;
            }
            //主送

            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                sendto = dt.Rows[0][0].ToString();

            }
            this.SendMail(submittype, sendto, copyto);
        }


        //发送邮件
        private void SendMail(int type, string sendto, List<string> copyto)
        {
            string subject_type = "";
            string body_bype = "";

            switch (type)
            {
                case 1:
                    subject_type = "您有新的不合格品通知单需要审批";
                    body_bype = "基本信息如下：";
                    break;
                case 2:
                    subject_type = "您有新的不合格品通知单需要审批";
                    body_bype = "基本信息如下：";
                    break;
                //case 2:
                //    subject_type = "您有新的不合格品通知单需要审批";
                //    body_bype = "基本信息如下：";
                //    break;
                //case 3:
                //    subject_type = "已审批完不合格品通知单";
                //    body_bype = "您提交的不合格品通知单于" + DateTime.Now.ToString() + "由" + Session["UserName"].ToString() + "审批完";
                //    break;
                //case 5:
                //    subject_type = "【复审】您有新的不合格品通知单需要复审";
                //    body_bype = "基本信息如下：";
                //    break;
                //case 6:
                //    subject_type = "【复审】您有新的不合格品通知单需要复审";
                //    body_bype = "基本信息如下：";
                //    break;
                //case 7:
                //    subject_type = "【复审】已审批完不合格品通知单";
                //    body_bype = "您提交的不合格品通知单于" + DateTime.Now.ToString() + "由" + Session["UserName"].ToString() + "复审完";
                //    break;
            }

            string subject = subject_type + "——" + lb_orderid.Text.ToString();
            string body = body_bype +
                        "\r\n单据编号：" + lb_orderid.Text.ToString() +
                        "\r\n项目名称：" + tb_pjinfo.Text.Trim() +
                        "\r\n合同号：" + tb_conId.Text.Trim() +
                        "\r\n部件名称：" + tb_cpmc.Text.Trim() +
                        "\r\n零件名称：" + tb_th.Text.Trim() +
                //   "\r\n情况描述：" + txtDeclare.Text +
                //  "\r\n产生原因：" + txt_reason.Text.Trim() +
                // "\r\n责任方：" + txt_duty_per.Text.Trim() +
                        "\r\n******************************************\r\n" +
                        "*提示：此邮件将发送给相应审批人及制单人，如需发送其他人员（如技术员，采购员），请自行转发";

            DBCallCommon.SendEmail(sendto, copyto, null, subject, body);

        }
        #endregion
    }
}
