using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_External_Audit_Edit : System.Web.UI.Page
    {
        string action = "";
        string orderid = "";
        string Id = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
            {

                action = Request.QueryString["action"].ToString();
            }
            if (Request.QueryString["Id"] != null)
            {
                Id = Request.QueryString["Id"].ToString();
            }



            //添加时先创建一个全球唯一标识，存储在一个lable中，用来关联合同与附件，避免因合同号占用而导致附件关联错误
            if (action == "add")
            {
                Guid tempid = Guid.NewGuid();
                lbl_UNID.Text = tempid.ToString();
            }


            if (!IsPostBack)
            {
                BindDep();
                InitPage();
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

        //判断载入状态，查看审核添加or浏览
        private void InitPage()
        {
            if (action == "add")//添加
            {
                lb_orderid.Text = Create_orderid();   //创建不合格通知单单号

                lblSHY.Text = Session["UserName"].ToString();   //编制人        
                /*********************************************/
                Panel2.Enabled = false;
                Panel3.Enabled = false;
                Panel4.Enabled = false;
                Panel5.Enabled = false;

            }
            else if (action == "view")//查看
            {
                lb_orderid.Text = orderid;   //单号
                Panel1.Enabled = false;
                Panel2.Enabled = false;
                Panel3.Enabled = false;
                Panel4.Enabled = false;
                Panel5.Enabled = false;
                LbtnSubmit.Visible = false;
                BindBasicData();           //绑定基本信息

            }
            else if (action == "audit" || action == "change")//审核或审批人在审批后要修改审批意见
            {



                BindBasicData();           //绑定基本信息 

                if (hidState.Value == "1")
                {
                    Panel1.Enabled = false;    //基本信息不可更改
                    Panel2.Enabled = false;
                    Panel3.Enabled = false;
                    Panel4.Enabled = false;   //验证人

                }
                else if (hidState.Value == "2")
                {
                    Panel1.Enabled = false;    //基本信息不可更改
                    Panel2.Enabled = true;
                    Panel3.Enabled = false;
                    Panel4.Enabled = false;   //验证人 
                    Panel5.Enabled = false;
                }
                else if (hidState.Value == "4")
                {
                    Panel1.Enabled = false;    //基本信息不可更改
                    Panel2.Enabled = false;
                    Panel3.Enabled = true;
                    Panel4.Enabled = false;
                    Panel5.Enabled = false;
                }
                else if (hidState.Value == "5")
                {
                    Panel1.Enabled = false;    //基本信息不可更改
                    Panel2.Enabled = false;
                    Panel3.Enabled = false;
                    Panel4.Enabled = true;
                    Panel5.Enabled = false;
                }
                else
                {
                    Panel1.Enabled = false;    //基本信息不可更改
                    Panel2.Enabled = false;
                    Panel3.Enabled = false;
                    Panel4.Enabled = false;
                    Panel5.Enabled = false;
                }



            }
            else if (action == "edit")     //编制人修改单据内容（不能修改审批人意见）
            {
                lb_orderid.Text = orderid;
                Panel2.Enabled = false;
                Panel3.Enabled = false;
                BindBasicData();
            }

        }



        //绑定基本信息
        private void BindBasicData()
        {
            string sql = "select * from View_TBQC_EXTERNAL_AUDIT where Id='" + Id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                lb_orderid.Text = dt.Rows[0]["PRO_ID"].ToString();
                string dep = dt.Rows[0]["PRO_DEP"].ToString();
                cblMainDep.SelectedValue = dep;
                txtTheme.Text = dt.Rows[0]["PRO_THEME"].ToString();
                txtKaiDanTime.Text = dt.Rows[0]["PRO_KDTIME"].ToString();
                lblSHY.Text = dt.Rows[0]["PRO_SHYNM"].ToString();

                txtProblem.Text = dt.Rows[0]["PRO_WTMS"].ToString();
                lblZGR.Text = dt.Rows[0]["PRO_ZGRNM"].ToString();
                lblZGData.Text = dt.Rows[0]["PRO_ZGDATA"].ToString();
                txtYYFX.Text = dt.Rows[0]["PRO_YYFX"].ToString();
                txtJS.Text = dt.Rows[0]["PRO_JIUZHENGCUOSHI"].ToString();

                txtCorrect.Text = dt.Rows[0]["PRO_YUFANGCUOSHI"].ToString();
                lb_fzr.Text = dt.Rows[0]["PRO_SPRNM"].ToString();
                lb_jsfzrsj.Text = dt.Rows[0]["PRO_SPTIME"].ToString();
                jsb_result.SelectedValue = dt.Rows[0]["PRO_SPRESULT"].ToString();
                tb_opinion2.Text = dt.Rows[0]["PRO_SPNOTE"].ToString();

                lb_fzr1.Text = dt.Rows[0]["PRO_SPRNM"].ToString();
                lb_jsfzrsj1.Text = dt.Rows[0]["PRO_SPTIME1"].ToString();
                jsb_result1.SelectedValue = dt.Rows[0]["PRO_SPRESULT1"].ToString();
                tb_opinion21.Text = dt.Rows[0]["PRO_SPNOTE1"].ToString();

                lb_yzr.Text = dt.Rows[0]["PRO_SHYNM"].ToString();
                lb_yzrsj.Text = dt.Rows[0]["PRO_YZTIME"].ToString();
                check_result.SelectedValue = dt.Rows[0]["PRO_YZRESULT"].ToString();
                tb_opinion4.Text = dt.Rows[0]["PRO_YZNOTE"].ToString();
                txtComment.Text = dt.Rows[0]["PRO_ZONGTIPINGJIA"].ToString();
                hidState.Value = dt.Rows[0]["PRO_STATE"].ToString();
                hidZGRID.Value = dt.Rows[0]["PRO_ZGR"].ToString();
                hidSPRID.Value = dt.Rows[0]["PRO_SPR"].ToString();
                hidYZRID.Value = dt.Rows[0]["PRO_SHY"].ToString();
                string zgr = dt.Rows[0]["PRO_ZGR"].ToString();
                //绑定抄送部门
                string[] copeDep = dt.Rows[0]["PRO_DEP2"].ToString().Split('|');
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

                if (dep != "")
                {

                    sql = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_DEPID='" + dep + "' and ST_POSITION like '" + dep + "%' and ST_PD='0' ";
                    BindCheckbox(cblZGR, sql);
                    cblZGR.SelectedValue = zgr;
                }

            }
        }



        //创建单号
        private string Create_orderid()
        {
            string id = "";
            id = "TJZJ-R-QM-";


            string sqlText = "select Top 1 PRO_ID from TBQC_EXTERNAL_AUDIT where PRO_ID like '" + id + "%'  Order by PRO_ID desc";//找出的最大号
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.HasRows)
            {
                dr.Read();
                string[] bh = dr["PRO_ID"].ToString().Split('-');
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

        /// <summary>
        /// 绑定主送部门
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
        //为主送部门绑定
        private void BindCheckboxDep(CheckBoxList cbl, string sqltext)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            cbl.DataSource = dt;
            cbl.DataTextField = "DEP_NAME";//部门名称
            cbl.DataValueField = "DEP_CODE";//部门编号 
            cbl.DataBind();


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
        }


        private void Submit_Rev()
        {
            List<string> sqlstr = new List<string>();
            string time = DateTime.Now.ToString("yyyy-MM-dd");
            if (hidSPRID.Value == Session["UserID"].ToString() && hidState.Value == "1")      //部门负责人
            {
                if (jsb_result1.SelectedIndex == -1)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择同意或者不同意！');", true); return;
                }
                else
                {
                    if (jsb_result1.SelectedValue == "1")
                    {

                        string update_czfs = "update TBQC_EXTERNAL_AUDIT set PRO_SPRESULT1='" + jsb_result1.SelectedValue + "',PRO_SPNOTE1='" + tb_opinion21.Text + "',PRO_SPTIME1='" + time + "',PRO_STATE='3' " +
                            " where Id='" + Id + "'";

                        sqlstr.Add(update_czfs);



                    }
                    else
                    {
                        string zgr = cblZGR.SelectedValue;
                        if (zgr != "")
                        {
                            string update_czfs = "update TBQC_EXTERNAL_AUDIT set PRO_SPRESULT1='" + jsb_result1.SelectedValue + "',PRO_SPNOTE1='" + tb_opinion21.Text + "',PRO_SPTIME1='" + time + "',PRO_STATE='2',PRO_ZGR='" + zgr + "' " +
                               " where Id='" + Id + "'";
                            sqlstr.Add(update_czfs);

                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请选择整改人！');", true); return;
                        }

                    }
                }

            }
            if (hidZGRID.Value == Session["UserID"].ToString() && hidState.Value == "2")    //整改人
            {

                string sql1 = "update TBQC_EXTERNAL_AUDIT set PRO_YYFX='" + txtYYFX.Text + "',PRO_JIUZHENGCUOSHI='" + txtJS.Text + "',PRO_YUFANGCUOSHI='" + txtCorrect.Text + "',PRO_ZGDATA='" + time + "',PRO_STATE='4' where Id='" + Id + "'";
                sqlstr.Add(sql1);

            }
            if (hidSPRID.Value == Session["UserID"].ToString() && hidState.Value == "4")      //技术部负责人
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
                            string update_czfs = "update TBQC_EXTERNAL_AUDIT set PRO_SPRESULT='" + jsb_result.SelectedValue + "',PRO_SPNOTE='" + tb_opinion2.Text + "',PRO_SPTIME='" + time + "',PRO_STATE='6' " +
                                " where Id='" + Id + "'";

                            sqlstr.Add(update_czfs);

                        }

                    }
                    else
                    {

                        if (tb_opinion2.Text.Trim() == "")
                        {
                            tb_opinion2.Text = "同意";
                        }

                        string update_czfs = "update TBQC_EXTERNAL_AUDIT set PRO_SPRESULT='" + jsb_result.SelectedValue + "',PRO_SPNOTE='" + tb_opinion2.Text + "',PRO_SPTIME='" + time + "',PRO_STATE='5' " +
                               " where Id='" + Id + "'";
                        sqlstr.Add(update_czfs);

                    }
                }

            }
            if (hidYZRID.Value == Session["UserID"].ToString() && hidState.Value == "5")     //验证人，即制单人
            {
                if (check_result.SelectedIndex != -1)
                {
                    string sql5 = "update TBQC_EXTERNAL_AUDIT set PRO_YZNOTE='" + tb_opinion4.Text + "'" +
                                       " ,PRO_ZONGTIPINGJIA='" + txtComment.Text.Trim() + "',PRO_YZTIME='" + time + "',PRO_YZRESULT='" + check_result.SelectedValue + "',PRO_STATE='7' where Id='" + Id + "'";
                    sqlstr.Add(sql5);

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请填写验证结果再提交！');", true); return;
                }

            }
            DBCallCommon.ExecuteTrans(sqlstr);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('提交成功！');window.location.href='QC_External_Audit.aspx';", true);
        }

        private void Write_Info()
        {

            string orderid = lb_orderid.Text.Trim();//单号
            string maindep = cblMainDep.SelectedValue; // 主送部门         
            string wtms = txtProblem.Text.Trim();
            string theme = txtTheme.Text.Trim();
            string kaidanTime = txtKaiDanTime.Text.Trim();
            string spr = "";

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
            List<string> sqlstr = new List<string>();
            string sql = "select ST_ID from dbo.TBDS_STAFFINFO where ST_POSITION='" + maindep + "01' ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count == 0 && maindep == "12")
            {
                spr = "69";//如果是质量部，并且质量部没部长，陈永秀代替
            }
            else if (maindep == "03")
            {
                spr = "67";//技术部，直接李小婷代替
            }
            else if (dt.Rows.Count > 0)
            {
                spr = dt.Rows[0][0].ToString();
            }

            if (action == "add")
            {
                //重新检查单号，防止多人同时添加造成单号重复
                lb_orderid.Text = orderid = Create_orderid();

                string sql1 = "insert into TBQC_EXTERNAL_AUDIT (PRO_DEP, PRO_KDTIME, PRO_ID,PRO_SHY,PRO_YEAR,PRO_STATE,PRO_WTMS,PRO_SPR,PRO_FUJIAN,PRO_THEME,PRO_DEP2) VALUES('" + maindep + "','" + kaidanTime + "'," + " '" + orderid + "','" + Session["UserID"].ToString() + "','" + Convert.ToDateTime(kaidanTime).Year.ToString() + "','1','" + wtms + "','" + spr + "','" + lbl_UNID.Text.ToString() + "','" + theme + "','" + copydep + "')";
                sqlstr.Add(sql1);
            }
            else if (action == "edit")
            {
                string sql6 = "update TBQC_EXTERNAL_AUDIT set PRO_DEP='" + maindep + "',PRO_DEP2='" + copydep + "',PRO_THEME='" + theme + "' where ORDER_ID='" + orderid + "'";
                sqlstr.Add(sql6);
            }
            DBCallCommon.ExecuteTrans(sqlstr);
            ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('提交成功！');window.location.href='QC_External_Audit.aspx';", true);
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

    }
}
