using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_KaoHe_JXGZ_Detail : System.Web.UI.Page
    {
        string action = string.Empty;
        string key = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["action"] != null)
                action = Request.QueryString["action"];
            if (Request.QueryString["key"] != null)
                key = Request.QueryString["key"];

            if (!IsPostBack)
            {
                hidAction.Value = action;
                BindData();
                if (action == "add")
                {
                    //获取上一周期年月份
                    string lastyear = "";
                    string lastmonth = "";
                    DateTime datenow = DateTime.Parse(DateTime.Now.ToString("yyyy-MM").Trim());
                    try
                    {
                        if (datenow.Month == 1)
                        {
                            lastyear = (datenow.Year - 1).ToString().Trim();
                            lastmonth = "12";
                        }
                        else
                        {
                            lastyear = datenow.Year.ToString().Trim();
                            lastmonth = (datenow.Month - 1).ToString("00").Trim();
                        }
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('获取年月份出错！');", true);
                        return;
                    }
                    txtKhNianYue.Text = lastyear + "-" + lastmonth;
                    lb1.Text = Session["UserName"].ToString();
                    txtTime.Text = DateTime.Now.ToString("yyyy-MM-dd").Trim();

                }
                else
                {

                    ShowData(key);
                    InitP();
                    // CaculateBzAverage();
                }
                ContralEnable();

            }
        }


        //绑定基本信息
        private void BindData()
        {
            string sql = "select DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE like '[0-9][0-9]'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            ddl_Depart.DataSource = dt;
            ddl_Depart.DataTextField = "DEP_NAME";
            ddl_Depart.DataValueField = "DEP_CODE";
            ddl_Depart.DataBind();
        }

        //0未提交 1审批中 2通过 3驳回
        private void ContralEnable()
        {
            btnCacuJX.Visible = false;
            btnCreatJx.Visible = false;
            btnDelete.Visible = false;

            if (action == "add")
            {

                btnAudit.Visible = false;
                rblResult1.Enabled = false;
                first_opinion.Enabled = false;
                btnCreatJx.Visible = true;
                btnDelete.Visible = true;
                spnBL.Visible = false;
                spnAddPer.Visible = true;
            }
            else
            {



                if (hidAction.Value == "view")
                {
                    hlSelect1.Visible = false;
                    btnAudit.Visible = false;
                    Panel2.Enabled = false;
                    Panel0.Enabled = false;
                    Panel1.Enabled = false;
                    spnBL.Visible = false;
                    spnAddPer.Visible = false;
                }
                else if (hidAction.Value == "edit")
                {
                    btnAudit.Visible = false;
                    btnCreatJx.Visible = true;
                    btnDelete.Visible = true;
                    spnBL.Visible = false;
                    spnAddPer.Visible = true;
                }
                else if (hidAction.Value == "audit")
                {
                    hlSelect1.Visible = false;
                    btnAudit.Visible = true;
                    Panel0.Enabled = false;
                    Panel2.Enabled = true;
                    btnCacuJX.Enabled = true;
                    btnCacuJX.Visible = true;
                    btnSave.Visible = false;
                    spnBL.Visible = true;
                    spnAddPer.Visible = false;

                }
            }

        }

        private void ShowData(string showContext)
        {

            string sql = " select * from TBDS_KaoHe_JXDetail as  a left join dbo.TBDS_KaoHeTotal as b on a.ST_ID=b.kh_Id and a.JxYear=b.kh_year and a.JxMonth=b.kh_month  where Context='" + showContext + "' order by GangWeiXiShu desc ";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            Det_Repeater.DataSource = dt;
            Det_Repeater.DataBind();


        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> sql = new List<string>();

            string sqltext = "";
            foreach (RepeaterItem LabelID in Det_Repeater.Items)
            {
                System.Web.UI.WebControls.CheckBox chk = (System.Web.UI.WebControls.CheckBox)LabelID.FindControl("CKBOX_SELECT");
                if (chk.Checked)
                {
                    string Id = ((HtmlInputHidden)LabelID.FindControl("hidId")).Value;


                    sqltext = "delete FROM TBDS_KaoHe_JXDetail WHERE Id='" + Id + "'";
                    sql.Add(sqltext);
                }
            }
            DBCallCommon.ExecuteTrans(sql);

            Response.Write("<script>alert('删除成功！')</script>");
            ShowData(hidConext.Value);
            CaculZongGZ(hidConext.Value);
        }


        private void InitP()
        {


            //Year, Month, State, SPRID, SPRNM, ZDRID, ZDRNM, RESULTA, ZDTIME, TIMEA, OPTIONA, MONEY, JXZong, PeoNum, ConText
            string sql = "select * from TBDS_KaoHe_JXList  where Context='" + key + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                ddl_Depart.SelectedValue = dt.Rows[0]["DepId"].ToString();
                txt_first.Text = dt.Rows[0]["SPRNM"].ToString();
                firstid.Value = dt.Rows[0]["SPRID"].ToString();
                first_time.Text = dt.Rows[0]["TIMEA"].ToString();
                first_opinion.Text = dt.Rows[0]["OPTIONA"].ToString();
                rblResult1.SelectedValue = dt.Rows[0]["RESULTA"].ToString();
                txtTime.Text = dt.Rows[0]["ZDTIME"].ToString();

                hidId.Value = dt.Rows[0]["Id"].ToString();
                hidConext.Value = key;
                txtZonghe.Text = dt.Rows[0]["Zonghe"].ToString();

                tb_yfpe.Text = dt.Rows[0]["Yingfpe"].ToString();
                tb_jye.Text = dt.Rows[0]["Jieye"].ToString();

                btnAudit.Visible = false;
                lb1.Text = dt.Rows[0]["ZDRNM"].ToString();
                hidState.Value = dt.Rows[0]["State"].ToString();
                txtKhNianYue.Text = dt.Rows[0]["Year"].ToString() + "-" + dt.Rows[0]["Month"].ToString();
                lblJS.Text = dt.Rows[0]["GongZiJS"].ToString();
                hidJS.Value = dt.Rows[0]["GongZiJS"].ToString();

            }


        }

        protected void btnCreatJx_Click(object sender, EventArgs e)
        {
            if (txtKhNianYue.Text == "")
            {
                Response.Write("<script>alert('请选择考核年月！')</script>");
                return;
            }
            string creatContext = DateTime.Now.ToString("yyyyMMddhhmmss");
            hidConext.Value = creatContext;
            string year = txtKhNianYue.Text.Split('-')[0].ToString();
            string month = txtKhNianYue.Text.Split('-')[1].PadLeft(2, '0');

            List<string> list = new List<string>();
            string sql = "select * from View_TBDS_KaoheDepartMonth where DepMonth_Year='" + year + "' and DepMonth_Month='" + month + "' and State='2'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count == 0)
            {
                Response.Write("<script>alert('未找到当月部门绩效考核数据！！！')</script>");
                return;
            }

            sql = "select * from TBDS_BZAVERAGE where Year='" + year + "' and Month='" + month + "' and State='4'";
            dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count == 0)
            {
                Response.Write("<script>alert('未找到一线班组平均工资数据！！！')</script>");
                return;
            }


            sql = "select  * from TBDS_KaoHeTotal as a left join TBDS_STAFFINFO as b on a.Kh_Id=b.ST_ID where Kh_Year='" + year + "' and Kh_Month='" + month + "' and   b.ST_DEPID='" + ddl_Depart.SelectedValue + "'";
            DataTable dtCount = DBCallCommon.GetDTUsingSqlText(sql);
            if (dtCount.Rows.Count > 0)
            {

            }
            else
            {
                sql = "insert into dbo.TBDS_KaoHeTotal select '" + year + "','" + month + "','0',case when kh_Score is null then '0' when kh_Score='' then '0' else kh_Score end ,'0','',ST_ID,'' from dbo.TBDS_STAFFINFO as a left join (select * from dbo.TBDS_KaoHeList where (Kh_Type='人员月度考核' or Kh_Type is null) and Kh_Year='" + year + "' and Kh_Month='" + month + "')b  on a.ST_ID=b.kh_Id where ST_PD in ('0','2') and ST_DEPID='" + ddl_Depart.SelectedValue + "'";
                list.Add(sql);

            }
            string lastYear = "";
            string lastMonth = "";

            if (month == "01")
            {
                lastMonth = "12";
                lastYear = (CommonFun.ComTryInt(year) - 1).ToString();
            }
            else
            {
                lastMonth = (CommonFun.ComTryInt(month) - 1).ToString().PadLeft(2, '0');
                lastYear = year;
            }

            //sql = "delete a  from TBDS_KaoHeTotal as a left join dbo.TBDS_STAFFINFO as b on a.Kh_Id=b.ST_ID where b.ST_DEPID='" + ddl_Depart.SelectedValue + "' and  a.Kh_Year='" + year + "' and a.Kh_Month='" + month + "'";
            //list.Add(sql);

            //sql = "delete from TBDS_KaoHe_JXDetail where DEP_ID='" + ddl_Depart.SelectedValue + "' and JxYear='" + year + "' and JxMonth='" + month + "'";
            //list.Add(sql);
            string sqlifexist = "select '" + creatContext + "','','0',d.Score,ST_GANGWEIXISHU,'0',DEP_POSITION,c.ST_ID,c.ST_NAME,ST_DEPID,c.DEP_NAME,DepMonth_Year,DepMonth_Month from dbo.TBDS_KaoHe_JXList as a left join dbo.TBDS_KaoHe_JXDetail as b on a.ConText=b.Context left join View_TBDS_STAFFINFO as c on b.ST_ID=c.ST_ID  inner join View_TBDS_KaoheDepartMonth as d on c.ST_DEPID=d.DepartId where d.state='2' and DepMonth_Year='" + year + "' and DepMonth_Month='" + month + "' and Year='" + lastYear + "' and Month='" + lastMonth + "' and ST_DEPID='" + ddl_Depart.SelectedValue + "' and ST_PD in ('0','2')";
            DataTable dtifexist = DBCallCommon.GetDTUsingSqlText(sqlifexist);

            sql = " insert into TBDS_KaoHe_JXDetail select ''";
            if (dtifexist.Rows.Count > 0)
            {
                sql = "insert into TBDS_KaoHe_JXDetail select '" + creatContext + "','','0',d.Score,ST_GANGWEIXISHU,'0',DEP_POSITION,c.ST_ID,c.ST_NAME,ST_DEPID,c.DEP_NAME,DepMonth_Year,DepMonth_Month from dbo.TBDS_KaoHe_JXList as a left join dbo.TBDS_KaoHe_JXDetail as b on a.ConText=b.Context left join View_TBDS_STAFFINFO as c on b.ST_ID=c.ST_ID  inner join View_TBDS_KaoheDepartMonth as d on c.ST_DEPID=d.DepartId where d.state='2' and DepMonth_Year='" + year + "' and DepMonth_Month='" + month + "' and Year='" + lastYear + "' and Month='" + lastMonth + "' and ST_DEPID='" + ddl_Depart.SelectedValue + "' and ST_PD in ('0','2')";
            }
            else
            {
                sql = "insert into TBDS_KaoHe_JXDetail select distinct '" + creatContext + "','','0',d.Score,ST_GANGWEIXISHU,'0',DEP_POSITION,c.ST_ID,c.ST_NAME,ST_DEPID,c.DEP_NAME,DepMonth_Year,DepMonth_Month from dbo.TBDS_KaoHe_JXList as a left join dbo.TBDS_KaoHe_JXDetail as b on a.ConText=b.Context right join View_TBDS_STAFFINFO as c on b.ST_ID=c.ST_ID  inner join View_TBDS_KaoheDepartMonth as d on c.ST_DEPID=d.DepartId where d.state='2' and DepMonth_Year='" + year + "' and DepMonth_Month='" + month + "' and ST_DEPID='" + ddl_Depart.SelectedValue + "' and Year+Month in (select max(Year+Month) from dbo.TBDS_KaoHe_JXList as a left join dbo.TBDS_KaoHe_JXDetail as b on a.ConText=b.Context right join View_TBDS_STAFFINFO as c on b.ST_ID=c.ST_ID  inner join View_TBDS_KaoheDepartMonth as d on c.ST_DEPID=d.DepartId where d.state='2' and DepMonth_Year='" + year + "' and DepMonth_Month='" + month + "' and ST_DEPID='" + ddl_Depart.SelectedValue + "' and ST_PD in ('0','2')) and ST_PD in ('0','2')";
            }
            list.Add(sql);

            DBCallCommon.ExecuteTrans(list);
            ShowData(creatContext);

            CaculZongGZ(creatContext);
        }




        private void CaculZongGZ(string context)
        {
            if (txtKhNianYue.Text == "")
            {
                Response.Write("<script>alert('请选择考核年月！')</script>");
                return;
            }
            string year = txtKhNianYue.Text.Split('-')[0].ToString();
            string month = txtKhNianYue.Text.Split('-')[1].PadLeft(2, '0');
            string js = CaculateBzAverage();

            string sql = "";
            if ( ddl_Depart.SelectedValue == "12")
            {
                 sql = " select sum(GangWeiXiShu) from TBDS_KaoHe_JXDetail where Context='" + context + "' and PosName <> '部长' and PosName <> '部长助理' and PosName not like '%总监%' and  PosName not like '%总经理助理兼部长%'";
            }
            else
            {
                 sql = " select sum(GangWeiXiShu) from TBDS_KaoHe_JXDetail where Context='" + context + "' and PosName <> '部长' and PosName not like '%总监%' and  PosName not like '%总经理助理兼部长%'";
            }
            DataTable dtGWXS;
            try
            {
                dtGWXS = DBCallCommon.GetDTUsingSqlText(sql);
            }
            catch (Exception)
            {
                Response.Write("<script>alert('该部门岗位系数有误，请核实人员信息表！！！')</script>");
                return;
            }
            sql = "select * from View_TBDS_KaoheDepartMonth where DepMonth_Year='" + year + "' and DepMonth_Month='" + month + "' and state='2' and DepartId='" + ddl_Depart.SelectedValue + "'";
            DataTable dtDepScore = DBCallCommon.GetDTUsingSqlText(sql);
            if (dtDepScore.Rows.Count > 0)
            {
                double zonggz = CommonFun.ComTryDouble(js) * CommonFun.ComTryDouble(dtGWXS.Rows[0][0].ToString()) * CommonFun.ComTryDouble(dtDepScore.Rows[0]["Score"].ToString()) / 100;

                sql = " select usermoney from OM_JXGZSYSP where yearmonth='" + year + "-" + month + "' and jxadddepartmentId='" + ddl_Depart.SelectedValue + "'";
                DataTable dtYe = DBCallCommon.GetDTUsingSqlText(sql);
                double jyeDouble = 0;
                if (dtYe.Rows.Count > 0)
                {
                    jyeDouble = CommonFun.ComTryDouble(dtYe.Rows[0][0].ToString());
                }

                tb_yfpe.Text = zonggz.ToString("0.00");
                tb_jye.Text = jyeDouble.ToString("0.00");
                txtZonghe.Text = (zonggz + jyeDouble).ToString("0.00");
            }
            else
            {
                Response.Write("<script>alert('未找到当月部门绩效考核数据！！！')</script>");
                return;
            }
        }


        //计算基数
        private string CaculateBzAverage()
        {
            string js = "0";
            if (txtKhNianYue.Text != "")
            {
                string year = txtKhNianYue.Text.Split('-')[0].ToString();
                string month = txtKhNianYue.Text.Split('-')[1].PadLeft(2, '0');


                string sql = "select * from TBDS_BZAVERAGE where Year='" + year + "' and Month='" + month + "' and State='4'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    string money = dt.Rows[0]["MONEY"].ToString();
                    js = (300 * CommonFun.ComTryDouble(money) / 3500).ToString("0.00");
                    lblJS.Text = js;
                    hidJS.Value = js;
                }
            }
            return js;
        }


        protected void btnCacuJX_Click(object sender, EventArgs e)
        {
            List<string> sql_list = new List<string>();

            if (txtKhNianYue.Text == "")
            {
                Response.Write("<script>alert('请选择考核年月！')</script>");
                return;
            }
            string year = txtKhNianYue.Text.Split('-')[0].ToString();
            string month = txtKhNianYue.Text.Split('-')[1].PadLeft(2, '0');


            string bl1 = txtHP.Text.Trim();
            string bl2 = txtLD.Text.Trim();
            string bl = bl1 + ":" + bl2;

            //if (CommonFun.ComTryInt(bl1) + CommonFun.ComTryInt(bl2) != 100)
            //{

            //    Response.Write("<script>alert('请输入正确的比例，使和为100！！！')</script>");
            //    return;
            //}



            for (int i = 0; i < Det_Repeater.Items.Count; i++)
            {

                string Id = ((HtmlInputHidden)Det_Repeater.Items[i].FindControl("hidId")).Value.Trim();
                string scoreHp = ((System.Web.UI.WebControls.TextBox)Det_Repeater.Items[i].FindControl("txtScoreHP")).Text.Trim();
                string txtlScoreLD = ((System.Web.UI.WebControls.TextBox)Det_Repeater.Items[i].FindControl("txtlScoreLD")).Text.Trim();
                string lblScoreZong = ((HtmlInputHidden)Det_Repeater.Items[i].FindControl("hidZong")).Value.Trim();
                string txtNote = ((System.Web.UI.WebControls.TextBox)Det_Repeater.Items[i].FindControl("txtNote")).Text.Trim();
                string stID = ((HtmlInputHidden)Det_Repeater.Items[i].FindControl("hidStId")).Value;
                //Id, Kh_Year, Kh_Month, Kh_ScoreHP, Kh_ScoreLD, Kh_ScoreTotal, Kh_BL, Kh_Id, Kh_BeiZhu, ST_NAME, ST_SEQUEN, ST_GENDER, DEP_NAME, POSITION, ST_WORKNO
                string sqlText = "";
                if (bl1 == "" || bl2 == "")
                {

                }
                else
                {
                     sqlText = "update TBDS_KaoHeTotal set Kh_ScoreHP='" + CommonFun.ComTryDecimal(scoreHp) + "',Kh_ScoreLD='" + CommonFun.ComTryDecimal(txtlScoreLD) + "',Kh_ScoreTotal='" + CommonFun.ComTryDecimal(lblScoreZong) + "',Kh_BL='" + bl + "' where Kh_Year='" + year + "' and Kh_Month='" + month + "' and Kh_Id='" + stID + "'";
                    sql_list.Add(sqlText);
                }
                sqlText = "update TBDS_KaoHe_JXDetail set Score='" + CommonFun.ComTryDecimal(lblScoreZong) + "', Note='" + txtNote + "' where JxYear='" + year + "' and JxMonth='" + month + "' and ST_ID='" + stID + "'";
                sql_list.Add(sqlText);

            }
            //更新
            DBCallCommon.ExecuteTrans(sql_list);




            CaculateBzAverage();

            string js = CaculateBzAverage();
            string sql = "select DepartId,Score from View_TBDS_KaoheDepartMonth where DepMonth_Year='" + year + "' and DepMonth_Month='" + month + "' and State='2' and DepartId='" + ddl_Depart.SelectedValue + "'";
            DataTable dtPart = DBCallCommon.GetDTUsingSqlText(sql);
            if (dtPart.Rows.Count > 0)
            {
                for (int i = 0; i < dtPart.Rows.Count; i++)
                {
                    string depId = dtPart.Rows[i]["DepartId"].ToString();
                    string score = dtPart.Rows[i]["Score"].ToString();
                    if (ddl_Depart.SelectedValue == "12")
                    {
                        sql = "select  sum((case when GangWeiXiShu='' or GangWeiXiShu is null then 1.00 else GangWeiXiShu end)*(case when  Score is null then 0.0 else Score end))  from TBDS_KaoHe_JXDetail where JxYear='" + year + "' and JxMonth='" + month + "'  and PosName <> '部长' and PosName <> '部长助理' and PosName not like '%总监%' and PosName not like '%总经理助理兼部长%' and Context='" + hidConext.Value + "'";
                    }
                    else
                    {
                        sql = "select  sum((case when GangWeiXiShu='' or GangWeiXiShu is null then 1.00 else GangWeiXiShu end)*(case when  Score is null then 0.0 else Score end))  from TBDS_KaoHe_JXDetail where JxYear='" + year + "' and JxMonth='" + month + "'  and PosName <> '部长' and PosName not like '%总监%' and PosName not like '%总经理助理兼部长%' and Context='" + hidConext.Value + "'";
                    }
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        double zongXishu = CommonFun.ComTryDouble(dt.Rows[0][0].ToString());
                        //double zongDeiFen = CommonFun.ComTryDouble(dt.Rows[0][1].ToString());
                        //if (zongDeiFen == 0)
                        //{
                        //    zongDeiFen = 1.0;
                        //}
                        double zonggongzi = CommonFun.ComTryDouble(txtZonghe.Text.Trim());
                        if (ddl_Depart.SelectedValue == "12")
                        {
                            sql = "update TBDS_KaoHe_JXDetail set  Money=case when PosName = '部长' or PosName = '部长助理' or PosName like '%总监%' or PosName like '%总经理助理兼部长%' then " + CommonFun.ComTryDouble(js) * CommonFun.ComTryDouble(score) / 100.0 + "*GangWeiXiShu  else " + zonggongzi / zongXishu + "*cast(Score as float)*GangWeiXiShu  end  where JxYear='" + year + "' and JxMonth='" + month + "' and Context='" + hidConext.Value + "' ";
                        }
                        else
                        {
                            sql = "update TBDS_KaoHe_JXDetail set  Money=case when PosName = '部长' or PosName like '%总监%' or PosName like '%总经理助理兼部长%' then " + CommonFun.ComTryDouble(js) * CommonFun.ComTryDouble(score) / 100.0 + "*GangWeiXiShu  else " + zonggongzi / zongXishu + "*cast(Score as float)*GangWeiXiShu  end  where JxYear='" + year + "' and JxMonth='" + month + "' and Context='" + hidConext.Value + "' ";
                        }
                            DBCallCommon.ExeSqlText(sql);
                    }

                }
            }
            ShowData(hidConext.Value);

        }



        protected void btnAddPer_Click(object sender, EventArgs e)
        {
            if (txtKhNianYue.Text == "")
            {
                Response.Write("<script>alert('请选择考核年月！')</script>");
                return;
            }
            string year = txtKhNianYue.Text.Split('-')[0].ToString();
            string month = txtKhNianYue.Text.Split('-')[1].PadLeft(2, '0');

            string sql = "insert into TBDS_KaoHe_JXDetail select  '" + hidConext.Value + "','','0',Score,ST_GANGWEIXISHU,'0',DEP_POSITION,ST_ID,ST_NAME,ST_DEPID,DEP_NAME,DepMonth_Year,DepMonth_Month from  (select * from dbo.View_TBDS_STAFFINFO as a inner join View_TBDS_KaoheDepartMonth as b on 1=1 where b.state='2' and DepMonth_Year='" + year + "' and DepMonth_Month='" + month + "' and DepartId='" + ddl_Depart.SelectedValue + "' and ST_ID='" + hidPerId.Value + "')c";
            DBCallCommon.ExeSqlText(sql);
            ShowData(hidConext.Value);
            CaculZongGZ(hidConext.Value);
        }



        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (firstid.Value == "")
            {
                Response.Write("<script>alert('请选择审核人！！')</script>");
                return;
            }
            if (txtKhNianYue.Text == "")
            {
                Response.Write("<script>alert('请选择考核年月！')</script>");
                return;
            }
            string year = txtKhNianYue.Text.Split('-')[0].ToString();
            string month = txtKhNianYue.Text.Split('-')[1].PadLeft(2, '0');
            if (action == "add")
            {
                string sql = "insert into TBDS_KaoHe_JXList(Year, Month, State, SPRID, SPRNM, ZDRID, ZDRNM,ZDTIME,ConText,DepId,Zonghe,Yingfpe,Jieye,GongZiJS) values('" + year + "','" + month + "','0','" + firstid.Value + "','" + txt_first.Text + "','" + Session["UserId"].ToString() + "','" + Session["UserName"].ToString() + "','" + txtTime.Text.Trim() + "','" + hidConext.Value + "','" + ddl_Depart.SelectedValue + "','" + txtZonghe.Text + "'," + CommonFun.ComTryDecimal(tb_yfpe.Text.Trim()) + "," + CommonFun.ComTryDecimal(tb_jye.Text.Trim()) + ",'" + hidJS.Value + "')";
                DBCallCommon.ExeSqlText(sql);
                Response.Write("<script>alert('保存成功！')</script>");
            }
            else if (action == "edit")
            {
                string sql = " update TBDS_KaoHe_JXList set Year='" + year + "',Month='" + month + "',SPRID='" + firstid.Value + "',SPRNM='" + txt_first.Text + "',DepID='" + ddl_Depart.SelectedValue + "',Zonghe='" + txtZonghe.Text + "',State='0',Yingfpe=" + CommonFun.ComTryDecimal(tb_yfpe.Text.Trim()) + ",Jieye=" + CommonFun.ComTryDecimal(tb_jye.Text.Trim()) + ",Context='" + hidConext.Value + "',GongZiJS='" + hidJS.Value + "' where Id='" + hidId.Value + "'";
                DBCallCommon.ExeSqlText(sql);
                Response.Write("<script>alert('保存成功！')</script>");
            }
            btnSave.Visible = false;
            btnAudit.Visible = true;

        }
        //审批
        protected void btnAudit_Click(object sender, EventArgs e)
        {
            string sql = "";
            string state = "";
            if (action == "add" || action == "edit")
            {
                sql = " update TBDS_KaoHe_JXList set  State='1' where Context='" + hidConext.Value + "'";
                DBCallCommon.ExeSqlText(sql);

                //邮件提醒
                string sprid = "";
                string sptitle = "";
                string spcontent = "";
                sprid = firstid.Value.Trim();
                sptitle = "职能部门绩效工资审批";
                spcontent = txtKhNianYue.Text.Trim() + "职能部门绩效工资需要您审批，请登录查看！";
                DBCallCommon.SendEmail(DBCallCommon.GetEmailAddressByUserID(sprid), new List<string>(), new List<string>(), sptitle, spcontent);

                Response.Write("<script>alert('保存成功！');window.location.href='OM_KaoHe_JXGZ_List.aspx';</script>");
            }
            else if (action == "audit")
            {
                //提示计算工资
                foreach (RepeaterItem item in Det_Repeater.Items)
                {
                    if (((TextBox)item.FindControl("txtMoney")).Text.Trim() == "0" || ((TextBox)item.FindControl("txtMoney")).Text.Trim() == "0.0" || ((TextBox)item.FindControl("txtMoney")).Text.Trim() == "0.00")
                    {
                        Response.Write("<script>alert('存在绩效工资为0的项，请先输入比例后计算绩效工资！')</script>");
                        return;
                    }
                }

                if (CommonFun.ComTryDouble(hidZongHeNew.Value) > CommonFun.ComTryDouble(txtZonghe.Text))
                {
                    Response.Write("<script>alert('调整后工资总和应小于等于原工资总和！')</script>");
                    return;
                }

                if (rblResult1.SelectedIndex == -1)
                {
                    Response.Write("<script>alert('请选择审核结果！')</script>");
                    return;
                }
                else
                {
                    List<string> list = new List<string>();


                    if (rblResult1.SelectedValue == "0")
                    {
                        state = "2";
                        foreach (RepeaterItem item in Det_Repeater.Items)
                        {

                            string Money = ((TextBox)item.FindControl("txtMoney")).Text;
                            string Id = ((HtmlInputHidden)item.FindControl("hidId")).Value;
                            sql = string.Format("update TBDS_KaoHe_JXDetail set Money='" + Money + "' where Id='" + Id + "'");
                            list.Add(sql);

                        }

                    }
                    else if (rblResult1.SelectedValue == "1")
                    {
                        state = "3";
                    }
                    sql = "update TBDS_KaoHe_JXList set state='" + state + "',RESULTA='" + rblResult1.SelectedValue + "',TIMEA='" + DateTime.Now.ToString() + "',OPTIONA='" + first_opinion.Text.Trim() + "',YuE='" + (CommonFun.ComTryDouble(txtZonghe.Text) - CommonFun.ComTryDouble(hidZongHeNew.Value)).ToString("0.00") + "'  where Context='" + hidConext.Value + "'";
                    list.Add(sql);
                    DBCallCommon.ExecuteTrans(list);
                    Response.Write("<script>alert('保存成功！');window.location.href='OM_KaoHe_JXGZ_List.aspx';</script>");

                }

            }


        }
    }
}
