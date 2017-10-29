using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.HtmlControls;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_BGYP_PCHZ_Detail : System.Web.UI.Page
    {
        string flag = string.Empty;
        DataTable HZdt = new DataTable();
        static int SHlevel = 2;   //默认为二级审核
        static int MYlevel = 0;   //我的审核等级

        protected void Page_Load(object sender, EventArgs e)
        {
            flag = Request.QueryString["action"].ToString();
            if (!IsPostBack)
            {
                if (flag == "add")//添加采购申请
                {
                    InitInfo();

                    Binddata_add();

                    //控件不可用
                    rbl_HT_SHR1_JL.Enabled = false;
                    txt_HT_SHR1_JY.Enabled = false;
                    rbl_HT_SHR2_JL.Enabled = false;
                    txt_HT_SHR2_JY.Enabled = false;
                    rbl_HT_SHR3_JL.Enabled = false;
                    txt_HT_SHR3_JY.Enabled = false;

                    GridView1.Columns[0].Visible = false;  //隐藏checkbox
                }
                if (flag == "view") //这是"查看"办公用品采购汇总审批
                {
                    //控制Panel不可编辑
                    FirstSHPanel.Enabled = false;
                    SecondSHPanel.Enabled = false;
                    ThirdSHPanel.Enabled = false;

                    imgSHR1.Visible = false;
                    imgSHR2.Visible = false;
                    imgSHR3.Visible = false;

                    txt_note.Enabled = false;
                    btnsave.Visible = false;
                    TextBoxDate.Enabled = false;
                    rblShdj.Visible = false;  //RadioButtonList隐藏 

                    string Id = Request.QueryString["id"].ToString();
                    string sql = "select * from TBOM_BGYPPCHZ where PCCODE='" + Id + "'";
                    HZdt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (HZdt.Rows.Count > 0)
                    {
                        //汇总清单
                        LabelCode.Text = Id.ToString();
                        TextBoxDate.Text = HZdt.Rows[0]["DATE"].ToString();
                        LabelDoc.Text = HZdt.Rows[0]["JBR"].ToString();
                        txt_note.Text = HZdt.Rows[0]["NOTE"].ToString();
                        SHlevel = Convert.ToInt32(HZdt.Rows[0]["SHRNUM"]);

                        //审核列表，先看审核人的数量再获取界面
                        LoadSHRDetail(SHlevel);
                    }

                    string sqltxt = "select * from View_TBOM_BGYPPCAPPLYINFO WHERE HZCODE='" + Id + "'";
                    DataTable DT = DBCallCommon.GetDTUsingSqlText(sqltxt);
                    GridView1.DataSource = DT;
                    GridView1.DataBind();

                    GridView1.Columns[0].Visible = false;  //隐藏checkbox
                }
                if (flag == "audit")
                {
                    //控制Panel不可编辑
                    FirstSHPanel.Enabled = false;
                    SecondSHPanel.Enabled = false;
                    ThirdSHPanel.Enabled = false;

                    imgSHR1.Visible = false;
                    imgSHR2.Visible = false;
                    imgSHR3.Visible = false;

                    txt_note.Enabled = false;
                    TextBoxDate.Enabled = false;
                    btnsave.Visible = true;
                    rblShdj.Visible = false;  //RadioButtonList隐藏 

                    string Id = Request.QueryString["id"].ToString();
                    string sql = "select * from TBOM_BGYPPCHZ where PCCODE='" + Id + "'";
                    HZdt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (HZdt.Rows.Count > 0)
                    {
                        LabelCode.Text = Id.ToString();
                        state.Text = HZdt.Rows[0]["STATE"].ToString();
                        TextBoxDate.Text = HZdt.Rows[0]["DATE"].ToString();
                        LabelDoc.Text = HZdt.Rows[0]["JBR"].ToString();
                        txt_note.Text = HZdt.Rows[0]["NOTE"].ToString();

                        //确定我的审核等级
                        if (HZdt.Rows[0]["SHRFID"].ToString() == Session["UserID"].ToString())
                        {
                            MYlevel = 1;
                            FirstSHPanel.Enabled = true;
                        }
                        else if (HZdt.Rows[0]["SHRSID"].ToString() == Session["UserID"].ToString())
                        {
                            MYlevel = 2;
                            SecondSHPanel.Enabled = true;
                        }
                        else if (HZdt.Rows[0]["SHRTID"].ToString() == Session["UserID"].ToString())
                        {
                            MYlevel = 3;
                            ThirdSHPanel.Enabled = true;
                        }

                        //审核列表，先看审核人的数量再获取界面
                        SHlevel = Convert.ToInt32(HZdt.Rows[0]["SHRNUM"]);
                        LoadSHRDetail(SHlevel);
                    }

                    string sqltxt = "select * from View_TBOM_BGYPPCAPPLYINFO WHERE HZCODE='" + Id + "'";
                    DataTable DT = DBCallCommon.GetDTUsingSqlText(sqltxt);
                    GridView1.DataSource = DT;
                    GridView1.DataBind();
                    GridView1.Columns[0].Visible = false;   //隐藏checkbox
                }

                //金额汇总计算
                double jine = 0;
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    jine += Convert.ToDouble(GridView1.Rows[i].Cells[8].Text.Trim());
                }
                lbljine.Text = jine.ToString();
            }
        }

        //加载汇总信息
        private void LoadSHRDetail(int num)
        {
            switch (num)
            {
                case 1:   //一级审核
                    FirstSHPanel.Visible = true;
                    SecondSHPanel.Visible = false;
                    ThirdSHPanel.Visible = false;

                    //一级审核信息
                    txt_HT_SHR1.Text = HZdt.Rows[0]["SHRF"].ToString();
                    SHR1id.Text = HZdt.Rows[0]["SHRFID"].ToString();
                    if (HZdt.Rows[0]["SHRFRESULT"].ToString() == "0")  //同意
                    {
                        rbl_HT_SHR1_JL.SelectedValue = "0";
                    }
                    if (HZdt.Rows[0]["SHRFRESULT"].ToString() == "1")
                    {
                        rbl_HT_SHR1_JL.SelectedValue = "1";
                    }
                    lb_HT_SHR1_SJ.Text = HZdt.Rows[0]["SHRFDATE"].ToString();
                    txt_HT_SHR1_JY.Text = HZdt.Rows[0]["SHRFNOTE"].ToString();

                    break;

                case 2:   //二级审核
                    FirstSHPanel.Visible = true;
                    SecondSHPanel.Visible = true;
                    ThirdSHPanel.Visible = false;

                    //一级审核信息
                    txt_HT_SHR1.Text = HZdt.Rows[0]["SHRF"].ToString();
                    SHR1id.Text = HZdt.Rows[0]["SHRFID"].ToString();
                    if (HZdt.Rows[0]["SHRFRESULT"].ToString() == "0")  //同意
                    {
                        rbl_HT_SHR1_JL.SelectedValue = "0";
                    }
                    if (HZdt.Rows[0]["SHRFRESULT"].ToString() == "1")
                    {
                        rbl_HT_SHR1_JL.SelectedValue = "1";
                    }
                    lb_HT_SHR1_SJ.Text = HZdt.Rows[0]["SHRFDATE"].ToString();
                    txt_HT_SHR1_JY.Text = HZdt.Rows[0]["SHRFNOTE"].ToString();


                    //二级审核信息
                    txt_HT_SHR2.Text = HZdt.Rows[0]["SHRS"].ToString();
                    SHR2id.Text = HZdt.Rows[0]["SHRSID"].ToString();
                    if (HZdt.Rows[0]["SHRSRESULT"].ToString() == "0")  //同意
                    {
                        rbl_HT_SHR2_JL.SelectedValue = "0";
                    }
                    if (HZdt.Rows[0]["SHRSRESULT"].ToString() == "1")
                    {
                        rbl_HT_SHR2_JL.SelectedValue = "1";
                    }
                    lb_HT_SHR2_SJ.Text = HZdt.Rows[0]["SHRSDATE"].ToString();
                    txt_HT_SHR2_JY.Text = HZdt.Rows[0]["SHRSNOTE"].ToString();

                    break;

                case 3:   //三级审核
                    FirstSHPanel.Visible = true;
                    SecondSHPanel.Visible = true;
                    ThirdSHPanel.Visible = true;

                    //一级审核信息
                    txt_HT_SHR1.Text = HZdt.Rows[0]["SHRF"].ToString();
                    SHR1id.Text = HZdt.Rows[0]["SHRFID"].ToString();
                    if (HZdt.Rows[0]["SHRFRESULT"].ToString() == "0")  //同意
                    {
                        rbl_HT_SHR1_JL.SelectedValue = "0";
                    }
                    if (HZdt.Rows[0]["SHRFRESULT"].ToString() == "1")
                    {
                        rbl_HT_SHR1_JL.SelectedValue = "1";
                    }
                    lb_HT_SHR1_SJ.Text = HZdt.Rows[0]["SHRFDATE"].ToString();
                    txt_HT_SHR1_JY.Text = HZdt.Rows[0]["SHRFNOTE"].ToString();


                    //二级审核信息
                    txt_HT_SHR2.Text = HZdt.Rows[0]["SHRS"].ToString();
                    SHR2id.Text = HZdt.Rows[0]["SHRSID"].ToString();
                    if (HZdt.Rows[0]["SHRSRESULT"].ToString() == "0")  //同意
                    {
                        rbl_HT_SHR2_JL.SelectedValue = "0";
                    }
                    if (HZdt.Rows[0]["SHRSRESULT"].ToString() == "1")
                    {
                        rbl_HT_SHR2_JL.SelectedValue = "1";
                    }
                    lb_HT_SHR2_SJ.Text = HZdt.Rows[0]["SHRSDATE"].ToString();
                    txt_HT_SHR2_JY.Text = HZdt.Rows[0]["SHRSNOTE"].ToString();

                    //三级审核信息
                    txt_HT_SHR3.Text = HZdt.Rows[0]["SHRT"].ToString();
                    SHR3id.Text = HZdt.Rows[0]["SHRTID"].ToString();
                    if (HZdt.Rows[0]["SHRTRESULT"].ToString() == "0")  //同意
                    {
                        rbl_HT_SHR3_JL.SelectedValue = "0";
                    }
                    if (HZdt.Rows[0]["SHRTRESULT"].ToString() == "1")
                    {
                        rbl_HT_SHR3_JL.SelectedValue = "1";
                    }
                    lb_HT_SHR3_SJ.Text = HZdt.Rows[0]["SHRTDATE"].ToString();
                    txt_HT_SHR3_JY.Text = HZdt.Rows[0]["SHRTNOTE"].ToString();

                    break;
            }
        }

        private void InitInfo()
        {
            //初始化单号

            LabelCode.Text = generateTempCode();

            string sql = "INSERT INTO TBOM_BGYP_INCODE(Code) VALUES ('" + LabelCode.Text + "')";

            DBCallCommon.ExeSqlText(sql);

            //制单人
            LabelDoc.Text = Session["UserName"].ToString();

            LabelDocCode.Text = Session["UserID"].ToString();

            TextBoxDate.Text = System.DateTime.Now.ToString("yyyy-MM-dd");

        }

        protected void close(object sender, EventArgs e)
        {
            Response.Redirect("OM_BGYP_PCHZ.aspx");
        }

        private void Binddata_add()
        {
            string sql_use = "select * from View_TBOM_BGYPPCAPPLYINFO where (HZSTATE='' or HZSTATE is null) and STATE='2'";
            DataTable DT = DBCallCommon.GetDTUsingSqlText(sql_use);
            GridView1.DataSource = DT;
            GridView1.DataBind();
            double jine = 0;
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                jine += Convert.ToDouble(GridView1.Rows[i].Cells[8].Text.Trim());
            }
            lbljine.Text = jine.ToString();
        }


        protected string generateTempCode()
        {

            List<string> lt = new List<string>();

            string sql = "SELECT Code FROM TBOM_BGYP_INCODE WHERE len(Code)=11 and left(Code,4)='HZPC'";

            SqlDataReader sdr = DBCallCommon.GetDRUsingSqlText(sql);
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    lt.Add(sdr["Code"].ToString());
                }
            }

            sdr.Close();

            string[] wsidlist = lt.ToArray();

            if (wsidlist.Count<string>() == 0)
            {
                return "HZPC0000001";
            }
            else
            {
                string tempstr = wsidlist.Max<string>();
                int tempnum = Convert.ToInt32((tempstr.Substring(4, 7)));
                tempnum++;
                tempstr = "HZPC" + tempnum.ToString().PadLeft(7, '0');
                return tempstr;
            }
        }




        //保存操作

        protected void Save_Click(object sender, EventArgs e)
        {
            //此处是保存操作
            List<string> sqllist = new List<string>();
            bool isupdate = false;     //是否更新了TBOM_BGYPPCAPPLY

            string sql = "";
            string Code = LabelCode.Text;//单号
            string Date = TextBoxDate.Text;//日期
            string jbrid = LabelDocCode.Text;//制单人id
            string jbr = LabelDoc.Text;  //制单人
            string note = txt_note.Text;  //制单人备注

            string totalprice = lbljine.Text.Trim();  //总的金额大小

            if (flag == "add")
            {
                //判断审核人有没有填写正确
                switch (rblShdj.SelectedIndex)
                {
                    case 0:
                        if (String.IsNullOrEmpty(txt_HT_SHR1.Text.ToString()))
                        {
                            Response.Write("<script>alert('请正确填写审核人!')</script>");
                            return;
                        }
                        break;
                    case 1:
                        if (String.IsNullOrEmpty(txt_HT_SHR1.Text.ToString()) || String.IsNullOrEmpty(txt_HT_SHR2.Text.ToString()))
                        {
                            Response.Write("<script>alert('请正确填写审核人!')</script>");
                            return;
                        }
                        break;
                    case 2:
                        if (String.IsNullOrEmpty(txt_HT_SHR1.Text.ToString()) || String.IsNullOrEmpty(txt_HT_SHR2.Text.ToString()) || String.IsNullOrEmpty(txt_HT_SHR3.Text.ToString()))
                        {
                            Response.Write("<script>alert('请正确填写审核人!')</script>");
                            return;
                        }
                        break;
                }

                //审核人填写正确之后开始对需要采购的物料单、采购申请汇总表插入数据
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    string pccode = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("hidPCCode")).Value.Trim();//物料代码
                    sql = "update TBOM_BGYPPCAPPLY  set HZSTATE='1',HZCODE='" + Code + "' where  PCCODE='" + pccode + "'";    //状态为1代表待审核
                    sqllist.Add(sql);
                    isupdate = true;
                }

                if (isupdate)  //如果插入了数据就加入审批汇总
                {
                    switch (rblShdj.SelectedIndex)
                    {
                        //一级审核
                        case 0:
                            sql = "INSERT INTO TBOM_BGYPPCHZ(PCCODE,JBR,JBRID,SHRF,SHRFID,DATE,STATE,NOTE,SHRNUM,TOTALPRICE) VALUES ('" +
                            Code + "','" + jbr + "','" + jbrid + "','" + txt_HT_SHR1.Text.ToString() + "','" + SHR1id.Text.ToString().Trim() + "','" + Date + "','1','" + note.Trim() + "',1,'" + totalprice + "')";
                            break;

                        //二级审核
                        case 1:
                            sql = "INSERT INTO TBOM_BGYPPCHZ(PCCODE,JBR,JBRID,SHRF,SHRFID,SHRS,SHRSID,DATE,STATE,NOTE,SHRNUM,TOTALPRICE) VALUES ('" +
                            Code + "','" + jbr + "','" + jbrid + "','" + txt_HT_SHR1.Text.ToString() + "','" + SHR1id.Text.ToString().Trim() + "','" + txt_HT_SHR2.Text.ToString() + "','" + SHR2id.Text.ToString().Trim() + "','" + Date + "','1','" + note.Trim() + "',2,'" + totalprice + "')";
                            break;

                        //三级审核
                        case 2:
                            sql = "INSERT INTO TBOM_BGYPPCHZ(PCCODE,JBR,JBRID,SHRF,SHRFID,SHRS,SHRSID,SHRT,SHRTID,DATE,STATE,NOTE,SHRNUM,TOTALPRICE) VALUES ('" +
                            Code + "','" + jbr + "','" + jbrid + "','" + txt_HT_SHR1.Text.ToString() + "','" + SHR1id.Text.ToString().Trim() + "','" + txt_HT_SHR2.Text.ToString() + "','" + SHR2id.Text.ToString().Trim() + "','" + txt_HT_SHR3.Text.ToString() + "','" + SHR3id.Text.ToString().Trim() + "','" + Date + "','1','" + note.Trim() + "',3,'" + totalprice + "')";
                            break;
                    }
                    sqllist.Add(sql);
                    DBCallCommon.ExecuteTrans(sqllist);

                    //发送邮件
                    SendEmailToSHR(SHR1id.Text.Trim());
                    Response.Redirect("OM_BGYP_PCHZ.aspx");
                }
            }

            if (flag == "audit")
            {
                string zongfirst = "";

                switch (MYlevel)
                {
                    //我的审核等级
                    case 1:
                        //审核结论
                        if (rbl_HT_SHR1_JL.SelectedIndex==-1)
                        {
                            Response.Write("<script>alert('请选择审核结果!')</script>");
                            return;
                        }
                        else if (rbl_HT_SHR1_JL.SelectedValue == "0")
                        {
                            zongfirst = "2";
                        }
                        else if (rbl_HT_SHR1_JL.SelectedValue == "1")
                        {
                            zongfirst = "3";
                        }

                        if (zongfirst == "3")   //不同意
                        {
                            sql = "update TBOM_BGYPPCHZ set STATE='" + zongfirst + "', SHRFDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "',SHRFNOTE='" + txt_HT_SHR1_JY.Text.ToString() + "',SHRFRESULT='" + rbl_HT_SHR1_JL.SelectedValue + "' WHERE PCCODE='" + Code + "'";
                            DBCallCommon.ExeSqlText(sql);

                            for (int i = 0; i < GridView1.Rows.Count; i++)
                            {
                                //将汇总状态设置为2即为驳回状态
                                string pccode = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("hidPCCode")).Value.Trim();//物料代码
                                sql = "update TBOM_BGYPPCAPPLY  set HZSTATE='2',HZCODE='" + Code + "' where  PCCODE='" + pccode + "'";
                                sqllist.Add(sql);
                            }
                            DBCallCommon.ExecuteTrans(sqllist);
                        }
                        else if (zongfirst == "2")   //同意
                        {
                            //如果我的审核级别为最终审核人，则可以赋值state=2（通过）
                            if (MYlevel == SHlevel)
                            {
                                sql = "update TBOM_BGYPPCHZ set STATE='" + zongfirst + "', SHRFDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "',SHRFNOTE='" + txt_HT_SHR1_JY.Text.ToString() + "',SHRFRESULT='" + rbl_HT_SHR1_JL.SelectedValue + "' WHERE PCCODE='" + Code + "'";
                                DBCallCommon.ExeSqlText(sql);
                            }
                            else if (MYlevel < SHlevel)
                            {
                                sql = "update TBOM_BGYPPCHZ set SHRFDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "',SHRFNOTE='" + txt_HT_SHR1_JY.Text.ToString() + "',SHRFRESULT='" + rbl_HT_SHR1_JL.SelectedValue + "' WHERE PCCODE='" + Code + "'";
                                DBCallCommon.ExeSqlText(sql);
                                SendEmailToSHR(SHR2id.Text.ToString().Trim());
                            }
                        }
                        Response.Redirect("OM_BGYP_PCHZ.aspx");
                        break;

                    case 2:
                        //审核结论
                        if (rbl_HT_SHR2_JL.SelectedIndex == -1)
                        {
                            Response.Write("<script>alert('请选择审核结果!')</script>");
                            return;
                        }
                        else if (rbl_HT_SHR2_JL.SelectedValue == "0")
                        {
                            zongfirst = "2";
                        }
                        else if (rbl_HT_SHR2_JL.SelectedValue == "1")
                        {
                            zongfirst = "3";
                        }

                        if (zongfirst == "3")   //不同意
                        {
                            sql = "update TBOM_BGYPPCHZ set STATE='" + zongfirst + "', SHRSDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "',SHRSNOTE='" + txt_HT_SHR2_JY.Text.ToString() + "',SHRSRESULT='" + rbl_HT_SHR2_JL.SelectedValue + "' WHERE PCCODE='" + Code + "'";
                            DBCallCommon.ExeSqlText(sql);

                            for (int i = 0; i < GridView1.Rows.Count; i++)
                            {
                                //将汇总状态设置为2即为驳回状态
                                string pccode = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("hidPCCode")).Value.Trim();//物料代码
                                sql = "update TBOM_BGYPPCAPPLY  set HZSTATE='2',HZCODE='" + Code + "' where  PCCODE='" + pccode + "'";
                                sqllist.Add(sql);
                            }
                            DBCallCommon.ExecuteTrans(sqllist);
                        }
                        else if (zongfirst == "2")   //同意
                        {
                            //如果我的审核级别为最终审核人，则可以赋值state=2（通过）
                            if (MYlevel == SHlevel)
                            {
                                sql = "update TBOM_BGYPPCHZ set STATE='" + zongfirst + "', SHRSDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "',SHRSNOTE='" + txt_HT_SHR2_JY.Text.ToString() + "',SHRSRESULT='" + rbl_HT_SHR2_JL.SelectedValue + "' WHERE PCCODE='" + Code + "'";
                                DBCallCommon.ExeSqlText(sql);
                            }
                            else if (MYlevel < SHlevel)
                            {
                                sql = "update TBOM_BGYPPCHZ set SHRSDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "',SHRSNOTE='" + txt_HT_SHR2_JY.Text.ToString() + "',SHRSRESULT='" + rbl_HT_SHR2_JL.SelectedValue + "' WHERE PCCODE='" + Code + "'";
                                DBCallCommon.ExeSqlText(sql);
                                SendEmailToSHR(SHR3id.Text.ToString().Trim());
                            }
                        }
                        Response.Redirect("OM_BGYP_PCHZ.aspx");
                        break;

                    case 3:
                        //审核结论
                        if (rbl_HT_SHR3_JL.SelectedIndex == -1)
                        {
                            Response.Write("<script>alert('请选择审核结果!')</script>");
                            return;
                        }
                        else if (rbl_HT_SHR3_JL.SelectedValue == "0")
                        {
                            zongfirst = "2";
                        }
                        else if (rbl_HT_SHR3_JL.SelectedValue == "1")
                        {
                            zongfirst = "3";
                        }

                        if (zongfirst == "3")   //不同意
                        {
                            sql = "update TBOM_BGYPPCHZ set STATE='" + zongfirst + "', SHRTDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "',SHRTNOTE='" + txt_HT_SHR3_JY.Text.ToString() + "',SHRTRESULT='" + rbl_HT_SHR3_JL.SelectedValue + "' WHERE PCCODE='" + Code + "'";
                            DBCallCommon.ExeSqlText(sql);

                            for (int i = 0; i < GridView1.Rows.Count; i++)
                            {
                                //将汇总状态设置为2即为驳回状态
                                string pccode = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("hidPCCode")).Value.Trim();//物料代码
                                sql = "update TBOM_BGYPPCAPPLY  set HZSTATE='2',HZCODE='" + Code + "' where  PCCODE='" + pccode + "'";
                                sqllist.Add(sql);
                            }
                            DBCallCommon.ExecuteTrans(sqllist);
                        }
                        else if (zongfirst == "2")   //同意
                        {
                            //如果我的审核级别为最终审核人，则可以赋值state=2（通过）
                            if (MYlevel == SHlevel)
                            {
                                sql = "update TBOM_BGYPPCHZ set STATE='" + zongfirst + "', SHRTDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "',SHRTNOTE='" + txt_HT_SHR3_JY.Text.ToString() + "',SHRTRESULT='" + rbl_HT_SHR3_JL.SelectedValue + "' WHERE PCCODE='" + Code + "'";
                                DBCallCommon.ExeSqlText(sql);
                            }
                            else if (MYlevel < SHlevel)
                            {
                                sql = "update TBOM_BGYPPCHZ set SHRTDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "',SHRTNOTE='" + txt_HT_SHR3_JY.Text.ToString() + "',SHRTRESULT='" + rbl_HT_SHR3_JL.SelectedValue + "' WHERE PCCODE='" + Code + "'";
                                DBCallCommon.ExeSqlText(sql);
                            }
                        }
                        Response.Redirect("OM_BGYP_PCHZ.aspx");
                        break;
                }
            }
        }

        private void SendEmailToSHR(string ID)
        {
            //邮件提醒
            string _emailto = DBCallCommon.GetEmailAddressByUserID(ID);
            string _body = "办公用品汇总审批任务:"
                  + "\r\n编    号：" + LabelCode.Text
                  + "\r\n制单人：" + LabelDoc.Text
                  + "\r\n制单日期：" + TextBoxDate.Text
                  + "\r\n总额: " + lbljine.Text.Trim();

            string _subject = "您有新的【办公用品汇总】需要审批，请及时处理:" + LabelCode.Text;
            DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
            
        }
        protected void btndaochu_click(object sender, EventArgs e)
        {

            if (flag == "view" || flag == "audit")
            {
                string filename = "办公用品采购申请单" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
                HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
                HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
                HttpContext.Current.Response.Clear();
                string sqltext = ""; ;
                string Id = Request.QueryString["id"].ToString();
                sqltext += "select * from View_TBOM_BGYPPCAPPLYINFO WHERE HZCODE='" + Id + "'";
                System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                //1.读取Excel到FileStream 
                using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("办公用品采购申请单.xls")))
                {
                    IWorkbook wk = new HSSFWorkbook(fs);
                    ISheet sheet0 = wk.GetSheetAt(0);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        IRow row = sheet0.CreateRow(i + 1);
                        row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                        row.CreateCell(1).SetCellValue("" + dt.Rows[i]["WLBM"].ToString());
                        row.CreateCell(2).SetCellValue("" + dt.Rows[i]["WLNAME"].ToString());
                        row.CreateCell(3).SetCellValue("" + dt.Rows[i]["WLMODEL"].ToString());
                        row.CreateCell(4).SetCellValue("" + dt.Rows[i]["WLUNIT"].ToString());
                        row.CreateCell(5).SetCellValue("" + dt.Rows[i]["WLNUM"].ToString());
                        row.CreateCell(6).SetCellValue("" + dt.Rows[i]["WLPRICE"].ToString());
                        row.CreateCell(7).SetCellValue("" + dt.Rows[i]["WLJE"].ToString());
                        row.CreateCell(8).SetCellValue("" + dt.Rows[i]["num"].ToString());
                        row.CreateCell(9).SetCellValue("" + dt.Rows[i]["DEPNAME"].ToString());
                        row.CreateCell(10).SetCellValue("" + dt.Rows[i]["Note"].ToString());

                    }

                    for (int i = 0; i <= dt.Columns.Count; i++)
                    {
                        sheet0.AutoSizeColumn(i);
                    }
                    sheet0.ForceFormulaRecalculation = true;

                    MemoryStream file = new MemoryStream();
                    wk.Write(file);
                    HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                    HttpContext.Current.Response.End();
                }
            }
        }

        //一级审核/二级审核/三级审核 
        protected void rblShdj_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (rblShdj.SelectedIndex)
            {
                case 0:
                    FirstSHPanel.Visible = true;
                    SecondSHPanel.Visible = false;
                    ThirdSHPanel.Visible = false;

                    //清空审核人
                    txt_HT_SHR2.Text = "";
                    txt_HT_SHR3.Text = "";

                    break;
                case 1:
                    FirstSHPanel.Visible = true;
                    SecondSHPanel.Visible = true;
                    ThirdSHPanel.Visible = false;

                    //清空审核人
                    txt_HT_SHR3.Text = "";
                    break;
                case 2:
                    FirstSHPanel.Visible = true;
                    SecondSHPanel.Visible = true;
                    ThirdSHPanel.Visible = true;
                    break;
            }
        }
    }
}
