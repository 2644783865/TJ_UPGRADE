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
using System.Collections.Generic;
using System.Text;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_FHNOTICE : System.Web.UI.Page
    {
        List<string> str = new List<string>();
        string action = string.Empty;
        string id = string.Empty;
        string confirm = string.Empty;
        string manclerk = string.Empty;
        string bmzg = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            if (Request.QueryString["id"] != null)
                id = Request.QueryString["id"];
            if (Request.QueryString["confirm"] != null)
                confirm = Request.QueryString["confirm"];
            this.Title = "发货通知";
            Get_Shr();
            getLeaderInfo();
            if (!IsPostBack)
            {
                UserID.Value = Session["UserID"].ToString();
                HidAction.Value = action;
                if (action == "add")
                {
                    CM_MANCLERK.Text = Session["UserName"].ToString();
                    CM_ZDTIME.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    rblShdj.Enabled = true;
                    Hidden.Value = DateTime.Now.ToString("yyyyMMddHHmmssff");
                    BindSelectReviewer();
                }
                else
                {
                    ShowData();
                    if (action == "look")
                    {
                        btnsubmit.Visible = false;
                        hlSelect1.Visible = false;
                        hlSelect2.Visible = false;
                        ReadOnly();
                        SetReadOnly();
                        TabContainer1.Tabs[1].Visible = false;
                    }
                    if (action == "edit")
                    {
                        Panel1.Visible = true;
                        rblShdj.Enabled = true;
                        if (manclerk == Session["UserID"].ToString())
                        {
                            Button1.Visible = true;
                            btnsubmit.Visible = false;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
                        }
                        BindSelectReviewer();
                    }
                    if (action == "sure")
                    {
                        TabContainer1.Tabs[1].Visible = false;
                        if (firstid.Value == Session["UserID"].ToString() || secondid.Value == Session["UserID"].ToString())
                        {
                            Button2.Visible = true;
                            btnsubmit.Visible = false;
                            hlSelect1.Visible = false;
                            hlSelect2.Visible = false;
                            rbl_first.Enabled = true;
                            rbl_second.Enabled = true;
                            ReadOnly();
                            SetReadOnly();
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
                        }
                    }
                }
                //txt_first.ReadOnly = true;
                //txt_second.ReadOnly = true;
                rblShdj_Changed(null, null);
            }
        }

        private void ShowData()
        {
            Panel1.Visible = false;
            string sql = "select a.*,b.ST_NAME as name1,d.ST_NAME as name2,e.ST_NAME as name3 from TBCM_FHNOTICE as a left join TBDS_STAFFINFO as b on a.CM_MANCLERK=b.ST_ID left join TBDS_STAFFINFO as d on a.CM_BMZG=d.ST_ID left join TBDS_STAFFINFO as e on a.CM_GSLD=e.ST_ID where CM_FID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow dr = dt.Rows[0];
                CM_BIANHAO.Text = dr["CM_BIANHAO"].ToString();
                hid_BianHao.Value = dr["CM_BIANHAO"].ToString();
                CM_CUSNAME.Text = dr["CM_CUSNAME"].ToString();
                CM_SH.Text = dr["CM_SH"].ToString();
                CM_JH.Text = dr["CM_JH"].ToString();
                CM_LXR.Text = dr["CM_LXR"].ToString();
                CM_LXFS.Text = dr["CM_LXFS"].ToString();
                CM_JHTIME.Text = dr["CM_JHTIME"].ToString();
                CM_YQDHSJ.Text = dr["CM_YQDHSJ"].ToString();
                CM_BEIZHU.Text = dr["CM_BEIZHU"].ToString();
                CM_MANCLERK.Text = dr["name1"].ToString();
                CM_ZDTIME.Text = dr["CM_ZDTIME"].ToString();
                Hidden.Value = dr["CM_ATTACH"].ToString();
                HidCSR.Value = dr["CM_CSR"].ToString();


                if (CM_ZDTIME.Text == "")
                {
                    CM_ZDTIME.Text = "未填写";
                }
                manclerk = dr["CM_MANCLERK"].ToString();

                rblShdj.SelectedValue = dr["CM_PSJB"].ToString();
                if (dr["CM_PSJB"].ToString() == "1")
                {
                    tb2.Visible = false;
                }

                firstid.Value = dr["CM_BMZG"].ToString();
                txt_first.Text = dr["name2"].ToString();
                rbl_first.Text = dt.Rows[0]["CM_YJ1"].ToString();
                first_time.Text = dt.Rows[0]["CM_SJ1"].ToString();
                first_opinion.Text = dt.Rows[0]["CM_NOTE1"].ToString();
                first_opinion.ReadOnly = true;

                secondid.Value = dt.Rows[0]["CM_GSLD"].ToString();
                txt_second.Text = dt.Rows[0]["name3"].ToString();
                rbl_second.Text = dt.Rows[0]["CM_YJ2"].ToString();
                second_time.Text = dt.Rows[0]["CM_SJ2"].ToString();
                second_opinion.Text = dt.Rows[0]["CM_NOTE2"].ToString();
                second_opinion.ReadOnly = true;

                if (firstid.Value == Session["UserID"].ToString())
                {
                    tb2.Enabled = false;
                }

                if (secondid.Value == Session["UserID"].ToString())
                {
                    tb1.Enabled = false;
                }

                sql = "select a.*,(case when b.CM_YFSM is null then '0' else CM_YFSM end) as TSA_YFSM from View_CM_FaHuo as a left join (select sum(convert(float,CM_FHNUM)) as CM_YFSM,CM_ID from VIEW_CM_FaHuo where CM_CONFIRM!=3 group by CM_ID) as b on a.CM_ID=b.CM_ID where CM_FID='" + id + "'";
                dt = DBCallCommon.GetDTUsingSqlText(sql);
                Det_Repeater.DataSource = dt;
                Det_Repeater.DataBind();
                InitVar();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('请刷新原页面！');window.opener=null;window.open('','_self');window.close();", true);
            }
        }

        private void ReadOnly()
        {
            foreach (Control item in TabPanel1.Controls[1].Controls)
            {
                if (item is TextBox)
                {
                    ((TextBox)item).ReadOnly = true;
                }
            }
            if (action == "sure")
            {
                if (txt_first.Text == Session["UserName"].ToString())
                {
                    first_opinion.ReadOnly = false;
                }
                if (txt_second.Text == Session["UserName"].ToString())
                {
                    second_opinion.ReadOnly = false;
                }
            }
        }//使发货通知单不可编辑

        private void SetReadOnly()//使填入发货数目文本框不可编辑
        {
            foreach (RepeaterItem item in Det_Repeater.Items)
            {
                TextBox tb = (TextBox)item.FindControl("CM_FHNUM");
                tb.ReadOnly = true;
                tb.Attributes.Remove("onblur");
            }
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (CONTR.Text.Trim() != "")
            {
                //string sql = "";
                StringBuilder sqlBuilder = new StringBuilder();
                if (action == "add")
                {
                    sqlBuilder.Append(@" SELECT  a.BM_XUHAO AS ID ,
                                                a.BM_ID AS CM_ID ,
                                                BM_PJID AS CM_CONTR ,
                                                b.CM_PROJ ,
                                                a.BM_ENGID AS TSA_ID ,
                                                a.BM_CHANAME AS TSA_ENGNAME ,
                                                a.BM_TUHAO AS TSA_MAP ,
                                                a.BM_PNUMBER AS TSA_NUMBER ,
                                                a.BM_TECHUNIT AS TSA_UNIT ,
                                                b.TSA_ENGNAME AS TSA_IDNOTE ,
                                                ( CAST(a.BM_PNUMBER AS INT)
                                                  - CAST(( CASE WHEN c.CM_YFSM IS NULL THEN '0'
                                                                ELSE c.CM_YFSM
                                                           END ) AS INT) ) AS CM_FHNUM ,
                                                ( CASE WHEN c.CM_YFSM IS NULL THEN '0'
                                                       ELSE c.CM_YFSM
                                                  END ) AS TSA_YFSM ");
                    sqlBuilder.Append(@" FROM    dbo.TBPM_STRINFODQO AS a
                                                    LEFT JOIN View_CM_TSAJOINPROJ AS b ON a.BM_ENGID = b.TSA_ID
                                                    LEFT JOIN ( SELECT  SUM(CM_FHNUM) AS CM_YFSM ,
                                                                        CM_ID
                                                                FROM    View_CM_FaHuo
                                                                WHERE   CM_CONFIRM != 3
                                                                GROUP BY CM_ID
                                                              ) AS c ON CONVERT(VARCHAR(20), a.BM_ID) = c.CM_ID ");
                    sqlBuilder.Append(@" WHERE   ( ( a.BM_MARID IS NULL
                                                    OR a.BM_MARID = ''
                                                  )
                                                  OR ( a.BM_MARID IS NOT NULL
                                                       AND a.BM_MARID <> ''
                                                       AND a.BM_KU IS NOT NULL
                                                       AND ( a.BM_KU <> ''
                                                             OR ( ( a.BM_KU = ''
                                                                    OR a.BM_KU IS NULL
                                                                  )
                                                                  AND a.BM_MARID LIKE '%.%'
                                                                )
                                                           )
                                                     )
                                                )
                                                AND BM_PJID NOT LIKE '%JSB.BOM%'
                                                AND BM_MSSTATUS <> '1'
                                                AND dbo.Splitnum(BM_ZONGXU, '.') < 3 ");
                    if (!string.IsNullOrEmpty(CONTR.Text))
                    {
                        sqlBuilder.Append(" and a.BM_PJID like '%" + CONTR.Text.Trim() + "%' ");
                    }                    
                    if (!string.IsNullOrEmpty(txtENGNAME.Text))
                    {
                        sqlBuilder.Append(" and a.BM_CHANAME like '%" + txtENGNAME.Text.Trim() + "%' ");
                    }
                    if (!string.IsNullOrEmpty(txtMap.Text))
                    {
                        sqlBuilder.Append(" and a.BM_TUHAO like '%" + txtMap.Text.Trim() + "%' ");
                    }
                    sqlBuilder.Append(" order by TSA_ID,ID asc ");
                }
                else if (action == "edit")
                {
                    //sql = "select a.BM_XUHAO as ID,a.BM_ID as CM_ID,BM_PJID as CM_CONTR,b.CM_PROJ,a.BM_ENGID as TSA_ID,a.BM_CHANAME as TSA_ENGNAME,a.BM_TUHAO as TSA_MAP,a.BM_PNUMBER as TSA_NUMBER,a.BM_TECHUNIT as TSA_UNIT,b.TSA_ENGNAME as TSA_IDNOTE,(CAST(a.BM_PNUMBER as int)-CAST((case when c.CM_YFSM is null then '0' else c.CM_YFSM end) as int)) as CM_FHNUM,(case when c.CM_YFSM is null then '0' else c.CM_YFSM end) as TSA_YFSM from dbo.TBPM_STRINFODQO as a left join View_CM_TSAJOINPROJ as b on a.BM_ENGID=b.TSA_ID  left join (select sum(CM_FHNUM) as CM_YFSM,CM_ID from VIEW_CM_FaHuo where CM_CONFIRM!=3 group by CM_ID) as c on convert(varchar(20),a.BM_ID)=c.CM_ID where ((a.BM_MARID is null or a.BM_MARID='') or (a.BM_MARID is not null and a.BM_MARID<>'' and a.BM_KU like '%S%' )) and a.BM_PJID like '%" + CONTR.Text.Trim() + "%'  and BM_PJID not like '%JSB.BOM%' and BM_MSSTATUS<>'1' and dbo.Splitnum(BM_ZONGXU,'.')<3 order by TSA_ID,ID asc";

                    sqlBuilder.Append(@" SELECT  a.BM_XUHAO AS ID ,
                                                a.BM_ID AS CM_ID ,
                                                BM_PJID AS CM_CONTR ,
                                                b.CM_PROJ ,
                                                a.BM_ENGID AS TSA_ID ,
                                                a.BM_CHANAME AS TSA_ENGNAME ,
                                                a.BM_TUHAO AS TSA_MAP ,
                                                a.BM_PNUMBER AS TSA_NUMBER ,
                                                a.BM_TECHUNIT AS TSA_UNIT ,
                                                b.TSA_ENGNAME AS TSA_IDNOTE ,
                                                ( CAST(a.BM_PNUMBER AS INT)
                                                  - CAST(( CASE WHEN c.CM_YFSM IS NULL THEN '0'
                                                                ELSE c.CM_YFSM
                                                           END ) AS INT) ) AS CM_FHNUM ,
                                                ( CASE WHEN c.CM_YFSM IS NULL THEN '0'
                                                       ELSE c.CM_YFSM
                                                  END ) AS TSA_YFSM ");
                    sqlBuilder.Append(@" FROM    dbo.TBPM_STRINFODQO AS a
                                            LEFT JOIN View_CM_TSAJOINPROJ AS b ON a.BM_ENGID = b.TSA_ID
                                            LEFT JOIN ( SELECT  SUM(CM_FHNUM) AS CM_YFSM ,
                                                                CM_ID
                                                        FROM    View_CM_FaHuo
                                                        WHERE   CM_CONFIRM != 3
                                                        GROUP BY CM_ID
                                                      ) AS c ON CONVERT(VARCHAR(20), a.BM_ID) = c.CM_ID ");
                    sqlBuilder.Append(@" WHERE   ( ( a.BM_MARID IS NULL
                                                    OR a.BM_MARID = ''
                                                  )
                                                  OR ( a.BM_MARID IS NOT NULL
                                                       AND a.BM_MARID <> ''
                                                       AND a.BM_KU LIKE '%S%'
                                                     )
                                                )
                                                AND BM_PJID NOT LIKE '%JSB.BOM%'
                                                AND BM_MSSTATUS <> '1'
                                                AND dbo.Splitnum(BM_ZONGXU, '.') < 3 ");
                    if (!string.IsNullOrEmpty(CONTR.Text))
                    {
                        sqlBuilder.Append(" AND a.BM_PJID LIKE '%" + CONTR.Text.Trim() + "%' ");
                    }                    
                    if (!string.IsNullOrEmpty(txtENGNAME.Text))
                    {
                        sqlBuilder.Append(" AND a.BM_CHANAME LIKE '%" + txtENGNAME.Text.Trim() + "%' ");
                    }
                    if (!string.IsNullOrEmpty(txtMap.Text))
                    {
                        sqlBuilder.Append(" AND a.BM_TUHAO LIKE '%" + txtMap.Text.Trim() + "%' ");
                    }
                    sqlBuilder.Append(" ORDER BY TSA_ID,ID ASC ");
                }
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlBuilder.ToString());
                Det_Repeater.DataSource = dt;
                Det_Repeater.DataBind();
                InitVar();
            }
        }

        private void InitVar()
        {
            if (Det_Repeater.Items.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
            }
        }

        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            List<string> list_sql = new List<string>();
            int err = CheckLev();
            if (err != 0)
            {
                switch (err)
                {
                    case 1:
                        Response.Write("<script>alert('请选择评审人！');</script>"); return;//后台验证
                    case 2:
                        Response.Write("<script>alert('您选择的是两级审批,请选择第二个评审人！');</script>"); return;//后台验证
                    case 3:
                        Response.Write("<script>alert('请勿重复选择评审人！');</script>"); return;//后台验证
                    default:
                        Response.Write("<script>alert('请选择评审人！');</script>"); return;//后台验证
                }
            }
            else
            {
                if (action == "add" || action == "edit")
                {
                    bool sf = true;
                    for (int i = 0; i < Det_Repeater.Items.Count; i++)
                    {
                        RepeaterItem retItem = Det_Repeater.Items[i];
                        if (((CheckBox)retItem.FindControl("chk")).Checked)
                        {
                            sf = false;
                        }
                    }
                    if (sf)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请添加并勾选内容！');", true); return;
                    }
                    string code = DateTime.Now.ToString("yyyyMMddHHmmssff");
                    if (action == "edit")//如果是编辑的话  先删除 然后再添加
                    {
                        string del1 = "delete from TBCM_FHNOTICE where CM_FID='" + id + "'";
                        string del2 = "delete from TBCM_FHBASIC where CM_FID='" + id + "'";
                        list_sql.Add(del1);
                        list_sql.Add(del2);
                    }
                    int n = 0;
                    if (CM_BIANHAO.Text.Trim() != hid_BianHao.Value)//验证编号是否重复
                    {
                        string str = "select * from TBCM_FHNOTICE where CM_BIANHAO='" + CM_BIANHAO.Text.Trim() + "'";
                        n = DBCallCommon.GetDTUsingSqlText(str).Rows.Count;
                    }
                    if (n != 0 || CM_BIANHAO.Text.Trim() == "")
                    {
                        Response.Write("<script>alert('已存在此编号或编号为空，请重新输入！');</script>"); return;
                    }
                    else
                    {
                        string yj2 = "";
                        if (secondid.Value != "")
                        {
                            yj2 = "1";
                        }
                        StringBuilder val = new StringBuilder();
                        string csr = "";
                        bindReviewer();
                        for (int i = 0; i < reviewer.Count; i++)
                        {
                            csr += "," + reviewer.ElementAt(i).Value;
                        }
                        if (csr != "")
                        {
                            csr = csr.Substring(1);
                        }

                        string sql = string.Format("insert into TBCM_FHNOTICE(CM_BIANHAO,CM_CUSNAME,CM_SH,CM_JH,CM_LXR,CM_LXFS,CM_JHTIME,CM_YQDHSJ,CM_MANCLERK,CM_BMZG,CM_GSLD,CM_PSJB,CM_ZDTIME,CM_FID,CM_YJ2,CM_BEIZHU,CM_ATTACH,CM_CSR) values('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}')", CM_BIANHAO.Text.Trim(), CM_CUSNAME.Text, CM_SH.Text, CM_JH.Text, CM_LXR.Text, CM_LXFS.Text, CM_JHTIME.Text, CM_YQDHSJ.Text.Trim(), UserID.Value, firstid.Value, secondid.Value, rblShdj.SelectedValue, DateTime.Now.ToString("yyyy-MM-dd"), code, yj2, CM_BEIZHU.Text, Hidden.Value, csr);
                        list_sql.Add(sql);
                        //添加发货信息
                        for (int i = 0; i < Det_Repeater.Items.Count; i++)
                        {
                            RepeaterItem retItem = Det_Repeater.Items[i];
                            if (((CheckBox)retItem.FindControl("chk")).Checked)
                            {
                                string tid = ((HiddenField)retItem.FindControl("tid")).Value;
                                string sid = ((HiddenField)retItem.FindControl("sid")).Value;
                                string fhnum = ((TextBox)retItem.FindControl("CM_FHNUM")).Text;
                                if (fhnum != "0")
                                {
                                    string sqlTxt = string.Format("insert into TBCM_FHBASIC(CM_FID,ID,CM_ID,CM_STATE,CM_FHNUM) values('{0}','{1}','{2}','0','{3}')", code, tid, sid, fhnum);
                                    list_sql.Add(sqlTxt);
                                    val = new StringBuilder();
                                }
                            }
                        }
                        DBCallCommon.ExecuteTrans(list_sql);//执行事务
                        DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(firstid.Value), new List<string>(), new List<string>(), "发货通知审批", "您有发货通知需要审批，请登录系统进行查看。");//DBCallCommon.GetEmailAddressByUserID(bmzgID.Value)
                        if (action == "add")
                        {
                            Response.Redirect("CM_FHList.aspx");
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
                        }
                    }
                }
                else if (action == "sure")
                {
                    string sql = "";
                    if (UserID.Value == firstid.Value)
                    {
                        if (rblShdj.SelectedValue == "1")
                        {
                            if (rbl_first.SelectedValue == "2")
                            {
                                sql = string.Format("update TBCM_FHNOTICE set CM_CONFIRM='{0}',CM_YJ1='{0}',CM_SJ1='{1}',CM_NOTE1='{2}' where CM_FID='{3}'", rbl_first.SelectedValue, DateTime.Now.ToString("yyyy-MM-dd"), first_opinion.Text == "" ? "同意" : first_opinion.Text, id);
                                SendEmail();
                            }
                            else if (rbl_first.SelectedValue == "3")
                            {
                                sql = string.Format("update TBCM_FHNOTICE set CM_CONFIRM='{0}',CM_YJ1='{0}',CM_SJ1='{1}',CM_NOTE1='{2}' where CM_FID='{3}'", rbl_first.SelectedValue, DateTime.Now.ToString("yyyy-MM-dd"), first_opinion.Text == "" ? "不同意" : first_opinion.Text, id);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择通过或者不通过！');", true); return;
                            }
                        }
                        if (rblShdj.SelectedValue == "2")
                        {
                            if (rbl_first.SelectedValue == "2")
                            {
                                sql = string.Format("update TBCM_FHNOTICE set CM_YJ1='2',CM_SJ1='{0}',CM_NOTE1='{1}' where CM_FID='{2}'", DateTime.Now.ToString("yyyy-MM-dd"), first_opinion.Text == "" ? "同意" : first_opinion.Text, id);
                                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(secondid.Value), new List<string>(), new List<string>(), "发货通知审批", "您有发货通知需要审批，请登录系统进行查看。");
                            }
                            else if (rbl_first.SelectedValue == "3")
                            {
                                sql = string.Format("update TBCM_FHNOTICE set CM_CONFIRM='3',CM_YJ1='3',CM_SJ1='{0}',CM_NOTE1='{1}' where CM_FID='{2}'", DateTime.Now.ToString("yyyy-MM-dd"), first_opinion.Text == "" ? "不同意" : first_opinion.Text, id);
                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择通过或者不通过！');", true); return;
                            }
                        }
                    }
                    if (UserID.Value == secondid.Value && rbl_first.SelectedValue == "2")
                    {
                        if (rbl_second.SelectedValue == "2")
                        {
                            sql = string.Format("update TBCM_FHNOTICE set CM_CONFIRM='2',CM_YJ2='2',CM_SJ2='{0}',CM_NOTE2='{1}' where CM_FID='{2}'", DateTime.Now.ToString("yyyy-MM-dd"), second_opinion.Text == "" ? "同意" : second_opinion.Text, id);
                            SendEmail();
                        }
                        else if (rbl_second.SelectedValue == "3")
                        {
                            sql = string.Format("update TBCM_FHNOTICE set CM_CONFIRM='3',CM_YJ2='3',CM_SJ2='{0}',CM_NOTE2='{1}' where CM_FID='{2}'", DateTime.Now.ToString("yyyy-MM-dd"), second_opinion.Text == "" ? "不同意" : second_opinion.Text, id);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择通过或者不通过！');", true); return;
                        }
                    }
                    DBCallCommon.ExeSqlText(sql);
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener.location.reload();window.opener=null;window.open('','_self');window.close();", true);
                }
            }
        }

        protected int CheckLev()
        {
            int b = 0;
            if (firstid.Value == "")
            {
                b = 1;
            }
            else
            {
                switch (rblShdj.SelectedValue)
                {
                    case "2":
                        if (secondid.Value == "")
                        {
                            b = 2;
                        }
                        else if (secondid.Value == firstid.Value)
                        {
                            b = 3;
                        }
                        break;
                    default:
                        break;
                }
            }
            return b;
        }

        protected void btnreturn_Click(object sender, EventArgs e)
        {
            if (action == "add")
            {
                Response.Redirect("CM_FHList.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.opener=null;window.open('','_self');window.close();", true);
            }
        }

        protected void Det_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType != ListItemType.Header)
            {
                if (action != "edit" && action != "add")
                {
                    e.Item.FindControl("chk").Visible = false;
                }
                DataRowView drv = (DataRowView)e.Item.DataItem;
                TextBox FHText = (TextBox)e.Item.FindControl("CM_FHNUM");

                string taskid = drv.Row["TSA_ID"].ToString();

                if (str.Count < 1)
                {
                    str.Add(taskid);
                }
                else
                {
                    if (str.Contains(taskid))
                    {
                        drv.Row["TSA_ID"] = "";
                        drv.Row["CM_PROJ"] = "";
                        drv.Row["CM_CONTR"] = "";
                    }
                    else
                    {
                        str.Add(taskid);
                    }
                    e.Item.DataBind();
                }
                drv.Row.AcceptChanges();

                Label lb1 = (Label)e.Item.FindControl("TSA_NUMBER");
                Label lb2 = (Label)e.Item.FindControl("TSA_YFSM");
                TextBox tb1 = (TextBox)e.Item.FindControl("CM_FHNUM");
                float a = float.Parse(lb1.Text == "" ? "0" : lb1.Text);
                float c = float.Parse(lb2.Text);
                float d = float.Parse(tb1.Text);

                if (action == "edit")
                {
                    if (confirm != "3")
                    {
                        c -= d;
                        lb2.Text = c.ToString();
                    }
                    //tb1.Text = "0";
                }
                if (action != "add" && action != "edit")
                {
                    if (drv.Row["CM_CONFIRM"].ToString() != "3")
                    {
                        c -= d;
                        lb2.Text = c.ToString();
                    }
                }
                if (c >= a)
                {
                    e.Item.FindControl("chk").Visible = false;
                    e.Item.FindControl("lbfh").Visible = true;
                    ((ITextControl)e.Item.FindControl("lbfh")).Text = "已发完";
                }

                string zongxu = drv.Row["ID"].ToString();
                FHText.Attributes.Add("group", taskid + zongxu.Substring(0, 1));
                if (zongxu.Length == 1)
                {
                    FHText.Attributes.Remove("onblur");
                    FHText.Attributes.Add("onblur", "dragVal(this,'" + taskid + zongxu.Substring(0, 1) + "')");
                }

                //string sql = string.Format("select CM_FID from TBCM_FHBASIC where ID='{0}' and CM_ID='{1}'", tid, ((HiddenField)e.Item.FindControl("sid")).Value);
                //DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                //if (dt.Rows.Count > 0)
                //{
                //    e.Item.FindControl("chk").Visible = false;
                //    e.Item.FindControl("lbfh").Visible = true;
                //    sql = "select CM_CONFIRM from TBCM_FHNOTICE where CM_FID='" + DBCallCommon.GetDTUsingSqlText(sql).Rows[0][0].ToString() + "'";
                //    string type = DBCallCommon.GetDTUsingSqlText(sql).Rows[0][0].ToString();
                //    string s = string.Empty;
                //    if (type == "1")
                //    {
                //        s = "签字中";
                //    }
                //    else if (type == "2")
                //    {
                //        s = "已通过";
                //    }
                //    else if (type == "3")
                //    {
                //        s = "被驳回";
                //    }
                //    ((ITextControl)e.Item.FindControl("lbfh")).Text = s;
                //}
            }
        }

        protected void rblShdj_Changed(object sender, EventArgs e)
        {
            switch (rblShdj.SelectedIndex)
            {
                case 0:
                    tb1.Visible = true;
                    tb2.Visible = false;
                    txt_second.Text = "";
                    secondid.Value = "";
                    break;
                case 1:
                    tb1.Visible = true;
                    tb2.Visible = true;
                    break;
            }
        }

        protected void btn_shr_Click(object sender, EventArgs e)
        {
            string i = ((Button)sender).ID.Substring(7);
            CheckBoxList ck = (CheckBoxList)Pan_ShenHe.FindControl("cki" + i);
            if (ck != null)
            {
                for (int j = 0; j < ck.Items.Count; j++)
                {
                    if (ck.Items[j].Selected == true)
                    {
                        if (i == "1")
                        {
                            firstid.Value = ck.Items[j].Value.ToString();
                            txt_first.Text = ck.Items[j].Text.ToString();
                        }
                        if (i == "2")
                        {
                            secondid.Value = ck.Items[j].Value.ToString();
                            txt_second.Text = ck.Items[j].Text.ToString();
                        }
                        return;
                    }
                }
                if (i == "1")
                {
                    firstid.Value = "";
                    txt_first.Text = "";
                }
                if (i == "2")
                {
                    secondid.Value = "";
                    txt_second.Text = "";
                }
            }
        }

        private void Get_Shr()
        {
            //审核人1
            pal_select1_inner.Controls.Add(ShrTable("1"));

            // 审核人2
            pal_select2_inner.Controls.Add(ShrTable("2"));
        }

        private Table ShrTable(string i)
        {
            string dep = "07";
            if (i == "2")
            {
                dep = "01";
            }
            string sql = string.Format("select st_ID,st_name from TBDS_STAFFINFO where st_depid='07' and ST_PD='0'", dep);
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            Table tctrl = new Table();
            if (dt.Rows.Count != 0)
            {
                TableRow tr = new TableRow();
                TableCell td = new TableCell();

                CheckBoxList cki = new CheckBoxList();
                cki.ID = "cki" + i;
                cki.DataSource = dt;
                cki.DataTextField = "ST_NAME";//领导的姓名
                cki.DataValueField = "ST_ID";//领导的编号
                cki.DataBind();
                for (int k = 0; k < cki.Items.Count; k++)
                {
                    cki.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个
                }
                cki.RepeatColumns = 5;//获取显示的列数
                td.Controls.Add(cki);
                tr.Cells.Add(td);
                tctrl.Controls.Add(tr);
            }
            return tctrl;
        }

        #region 得到领导信息
        Table t = new Table();
        int rowsum = 0;
        protected void getLeaderInfo()
        {
            /******绑定人员信息*****/
            getStaffInfo("07", "市场部", 4);
            getStaffInfo("03", "技术质量部", 0);
            getStaffInfo("05", "采购部", 1);
            getStaffInfo("04", "生产管理部", 2);
            getStaffInfo("06", "财务部", 3);
            getStaffInfo("01", "公司领导", 5);
            //得到领导信息，根据金额
            Panel2.Controls.Add(t);
        }

        protected void getStaffInfo(string st_id, string DEP_NAME, int i)
        {
            string sql = string.Format("select ST_NAME,ST_ID,ST_DEPID from TBDS_STAFFINFO as a inner join TBCM_HT_SETTING as b on a.ST_ID=b.per_id where a.ST_PD='0'and b.dep_id='{0}' and b.per_sfjy='0' and b.per_type='2'", st_id);
            bindInfo(sql, i, DEP_NAME, st_id);
        }
        /**********************动态的绑定评审人员的信息*************************************/
        private void bindInfo(string sql, int i, string DEP_NAME, string st_id)
        {
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count != 0)
            {
                TableRow tr = new TableRow();
                TableCell td = new TableCell();
                td.Width = 100;
                TableCell td1 = new TableCell();//第一列为部门名称
                if (i == 4)
                {
                    td.Text = "抄送至:";
                }
                td1.Width = 85;
                Label lab = new Label();
                lab.Text = DEP_NAME + ":";
                Label lab1 = new Label();
                lab1.ID = "dep" + i.ToString();
                lab1.Text = st_id;
                lab1.Visible = false;
                td1.Controls.Add(lab);
                td1.Controls.Add(lab1);
                tr.Cells.Add(td);
                tr.Cells.Add(td1);

                CheckBoxList cki = new CheckBoxList();//第二列为领导的姓名
                cki.ID = "cki" + i.ToString();
                cki.DataSource = dt;
                cki.DataTextField = "ST_NAME";//领导的姓名
                cki.DataValueField = "ST_ID";//部门的编号
                cki.DataBind();
                cki.CellSpacing = 10;
                if (i == 4)
                {
                    for (int k = 0; k < cki.Items.Count; k++)
                    {
                        //cki.Items[k].Attributes.Add("Onclick", "CheckBoxList_Click(this)");//使用了javascript使其只能勾选一个
                        cki.Items[k].Attributes.Add("width", "100px");
                    }
                }
                cki.RepeatColumns = 5;//获取显示的列数
                TableCell td2 = new TableCell();
                td2.Controls.Add(cki);
                tr.Cells.Add(td2);
                t.Controls.Add(tr);
                rowsum++;
            }
        }

        #endregion

        Dictionary<string, string> reviewer = new Dictionary<string, string>();//用于存储审核的名单
        private void bindReviewer()
        {
            List<string> list = new List<string>();
            foreach (Control item in Panel2.Controls)
            {
                list.Add(item.ID);
            }
            int count = 0;
            for (int i = 0; i < 6; i++)
            {
                CheckBoxList ck = (CheckBoxList)Panel2.FindControl("cki" + i.ToString());
                Label lb = (Label)Panel2.FindControl("dep" + i.ToString());
                if (ck != null)
                {
                    for (int j = 0; j < ck.Items.Count; j++)
                    {
                        if (ck.Items[j].Selected == true)
                        {
                            reviewer.Add(lb.Text + ck.Items[j].Value.ToString(), ck.Items[j].Value.ToString());//字典，绑定部门领导的编号
                            count++;
                        }
                    }
                }
            }
        }

        private void BindSelectReviewer()
        {
            string csr = HidCSR.Value;
            string[] str = csr.Split(',');
            for (int i = 0; i < 6; i++)
            {
                CheckBoxList ck = (CheckBoxList)Panel2.FindControl("cki" + i.ToString());
                for (int j = 0; j < str.Length; j++)
                {
                    for (int k = 0; k < ck.Items.Count; k++)
                    {
                        if (ck.Items[k].Value == str[j].ToString())
                        {
                            ck.Items[k].Selected = true;
                        }
                    }
                }
            }
        }

        private void SendEmail()
        {
            string csr = HidCSR.Value;
            string[] str = csr.Split(',');
            for (int i = 0; i < str.Length; i++)
            {
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(str[i]), new List<string>(), new List<string>(), "您有发货通知需要查看", "编号为 " + CM_BIANHAO.Text + ",顾客名称为 " + CM_CUSNAME.Text + " 的发货通知已审批通过，请登录系统查看。");
            }
        }
    }
}