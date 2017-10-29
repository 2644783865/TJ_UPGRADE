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
using System.Text.RegularExpressions;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_BgypPcApply : System.Web.UI.Page
    {
        int count = 0;

        string flag = string.Empty;

        string incode = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            flag = Request.QueryString["action"].ToString();
            if (!IsPostBack)
            {
                //((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                if (flag == "add")//添加采购申请
                {
                    InitInfo();   //初始化办公用品采购单号
                    InitGridview();     //初始化采购单信息
                    //Binddata_add();

                    //隐藏对应的采购部门的填写
                    this.GridView1.Columns[10].Visible = false;

                    //默认选择自己部门的部长进行审批
                    string sql = "select ST_NAME,ST_ID from  TBDS_STAFFINFO where ST_DEPID='" + Session["UserDeptID"].ToString() + "' and ST_PD='0' and ST_POSITION LIKE '%01'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count != 0)
                    {
                        txt_first.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                        SHR1id.Text = dt.Rows[0]["ST_ID"].ToString().Trim();
                    }
                }
                //((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
                if (flag == "safePC")  //安全库存采购申请
                {
                    InitInfo();
                    InitGridview();
                    Binddata_safePC();

                    //隐藏对应的采购部门的填写
                    this.GridView1.Columns[10].Visible = false;

                    //默认选择自己部门的部长进行审批
                    string sql = "select ST_NAME,ST_ID from  TBDS_STAFFINFO where ST_DEPID='" + Session["UserDeptID"].ToString() + "' and ST_PD='0' and ST_POSITION LIKE '%01'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count != 0)
                    {
                        txt_first.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                        SHR1id.Text = dt.Rows[0]["ST_ID"].ToString().Trim();
                    }

                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        //((CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1")).Visible = false;
                        ((TextBox)this.GridView1.Rows[i].FindControl("WLBM")).Enabled = false;
                        ((TextBox)this.GridView1.Rows[i].FindControl("WLNAME")).Enabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLMODEL")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLUNIT")).Disabled = true;
                        //((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNUM")).Disabled = true;
                        // ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLPRICE")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLJE")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("num")).Disabled = true;
                        //((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNOTE")).Disabled = true;
                    }
                }
                if (flag == "view")   //查看按钮
                {
                    txt_note.Enabled = false;
                    btnsave.Visible = false;
                    TextBoxDate.Enabled = false;

                    //增加行不可见
                    btnadd.Visible = false;
                    btndelete.Visible = false;
                    addrow.Visible = false;   
                    txtNum.Visible = false;  
 
                    //审核人选择图标
                    imgSHR1.Visible = false;

                    string Id = Request.QueryString["id"].ToString();
                    string sql = "select * from TBOM_BGYPPCAPPLY where PCCODE='" + Id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        //申请清单
                        lbljine.Text = dt.Rows[0]["JINE"].ToString();
                        LabelCode.Text = Id.ToString();
                        TextBoxDate.Text = dt.Rows[0]["DATE"].ToString();
                        LabelDoc.Text = dt.Rows[0]["JBR"].ToString();
                        txt_note.Text = dt.Rows[0]["NOTE"].ToString();


                        //审核人
                        txt_first.Text = dt.Rows[0]["SHRF"].ToString();
                        first_opinion.Text = dt.Rows[0]["SHRFNOTE"].ToString();
                        first_time.Text = dt.Rows[0]["SHRFDATE"].ToString();
                        SHR1id.Text = dt.Rows[0]["SHRFID"].ToString();

                        if (dt.Rows[0]["STATE"].ToString() == "2")
                        {
                            rblfirst.SelectedValue = "2";

                        }
                        if (dt.Rows[0]["STATE"].ToString() == "3")
                        {
                            rblfirst.SelectedValue = "3";
                        }
                    }
                    string sqltxt = "select * from View_TBOM_BGYPPCAPPLYINFO WHERE CODE='" + Id + "'";
                    DataTable DT = DBCallCommon.GetDTUsingSqlText(sqltxt);
                    GridView1.DataSource = DT;
                    GridView1.DataBind();

                    //设置物料清单为不可见状态
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        ((CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1")).Visible = false;
                        ((TextBox)this.GridView1.Rows[i].FindControl("WLBM")).Enabled = false;
                        ((TextBox)this.GridView1.Rows[i].FindControl("WLNAME")).Enabled = false;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLMODEL")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLUNIT")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNUM")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLPRICE")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLJE")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("num")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("txtDep")).Disabled = true;
                        ((TextBox)this.GridView1.Rows[i].FindControl("txtNote")).Enabled = false;
                    }
                }

                if (flag == "verify")  //审核处理
                {
                    btnsave.Visible = true;
                    txt_note.Enabled = false;
                    TextBoxDate.Enabled = false;

                    //增加行不可见
                    btnadd.Visible = false;
                    btndelete.Visible = false;
                    addrow.Visible = false;   
                    txtNum.Visible = false;

                    //审核人选择图标
                    imgSHR1.Visible = false;

                    string Id = Request.QueryString["id"].ToString();
                    string sql = "select * from TBOM_BGYPPCAPPLY where PCCODE='" + Id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        //申请清单
                        lbljine.Text = dt.Rows[0]["JINE"].ToString();
                        LabelCode.Text = Id.ToString();
                        TextBoxDate.Text = dt.Rows[0]["DATE"].ToString();
                        LabelDoc.Text = dt.Rows[0]["JBR"].ToString();
                        txt_note.Text = dt.Rows[0]["NOTE"].ToString();

                        state.Text = dt.Rows[0]["STATE"].ToString();

                        //审核人
                        txt_first.Text = dt.Rows[0]["SHRF"].ToString();
                        SHR1id.Text = dt.Rows[0]["SHRFID"].ToString();
                    }

                    if (Session["UserID"].ToString() == dt.Rows[0]["SHRFID"].ToString())
                    {
                        rblfirst.Enabled = true;
                        first_opinion.Enabled = true;
                        first_time.Text = DateTime.Now.ToString();
                    }
                    if (Session["UserID"].ToString() == dt.Rows[0]["SHRSID"].ToString())
                    {
                        rblfirst.SelectedValue = "2";
                        //hlSelect2.Enabled = true;

                    }

                    string sqltxt = "select * from View_TBOM_BGYPPCAPPLYINFO WHERE CODE='" + Id + "'";
                    DataTable DT = DBCallCommon.GetDTUsingSqlText(sqltxt);
                    GridView1.DataSource = DT;
                    GridView1.DataBind();

                    //设置物料清单为不可见状态
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        ((CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1")).Visible = false;
                        ((TextBox)this.GridView1.Rows[i].FindControl("WLBM")).Enabled = false;
                        ((TextBox)this.GridView1.Rows[i].FindControl("WLNAME")).Enabled = false;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLMODEL")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLUNIT")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNUM")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLPRICE")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLJE")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("num")).Disabled = true;
                        ((HtmlInputText)this.GridView1.Rows[i].FindControl("txtDep")).Disabled = true;
                        ((TextBox)this.GridView1.Rows[i].FindControl("txtNote")).Enabled = false;
                    }
                }

                if (flag == "mod")   //驳回处理
                {

                    TextBoxDate.Enabled = false;

                    //隐藏对应的采购部门的填写
                    this.GridView1.Columns[10].Visible = false;

                    string Id = Request.QueryString["id"].ToString();
                    string sql = "select * from TBOM_BGYPPCAPPLY where PCCODE='" + Id + "'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        //申请清单
                        lbljine.Text = dt.Rows[0]["JINE"].ToString();
                        LabelCode.Text = Id.ToString();
                        TextBoxDate.Text = dt.Rows[0]["DATE"].ToString();
                        LabelDoc.Text = dt.Rows[0]["JBR"].ToString();
                        txt_note.Text = dt.Rows[0]["NOTE"].ToString();
                        state.Text = dt.Rows[0]["STATE"].ToString();  //申请状态

                        //审核人
                        txt_first.Text = dt.Rows[0]["SHRF"].ToString();
                        SHR1id.Text = dt.Rows[0]["SHRFID"].ToString();
                        first_opinion.Text = dt.Rows[0]["SHRFNOTE"].ToString();
                        first_time.Text = dt.Rows[0]["SHRFDATE"].ToString();

                        if (dt.Rows[0]["STATE"].ToString() == "6" || dt.Rows[0]["STATE"].ToString() == "5")
                        {
                            rblfirst.SelectedValue = "2";   //同意
                        }

                    }
                    string sqltxt = "select * from View_TBOM_BGYPPCAPPLYINFO WHERE CODE='" + Id + "'";
                    DataTable DT = DBCallCommon.GetDTUsingSqlText(sqltxt);
                    GridView1.DataSource = DT;
                    GridView1.DataBind();
                    //for (int i = 0; i < GridView1.Rows.Count; i++)
                    //{
                    //    ((HtmlInputText)this.GridView1.Rows[i].FindControl("num")).Disabled = true;
                    //}
                }
            }
        }

        private void Binddata_safePC()
        {
            string maId = Request.QueryString["maId"].ToString();

            string sql_safe = "select *,kc-num as WLNUM,WLPRICE*(kc-num) as WLJE,'' as DEPNAME,'' as Note from (select a.maId as WLBM,'' as CODE,a.Id as WLCODE,name as WLNAME,canshu as WLMODEL,unit as WLUNIT,a.price as WLPRICE,case when num is null then '0' else num end as num,kc from TBMA_OFFICETH as a left join TBOM_BGYP_STORE as b on a.Id=b.mId where (a.kc>b.num) or ((a.kc is not null and a.kc<>'' and a.kc<>'0') and (num is null or num='0') and IsDel='0' and a.maId in " + maId + "))c";
            DataTable DT = DBCallCommon.GetDTUsingSqlText(sql_safe);
            GridView1.DataSource = DT;
            GridView1.DataBind();
        }

        protected void close(object sender, EventArgs e)
        {
            Response.Redirect("OM_BgypPcApplyMain.aspx");
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

        protected string generateTempCode()
        {

            List<string> lt = new List<string>();

            string sql = "SELECT Code FROM TBOM_BGYP_INCODE WHERE len(Code)=11 and left(Code,2)='PC'";

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
                return "PC000000001";
            }
            else
            {
                string tempstr = wsidlist.Max<string>();
                int tempnum = Convert.ToInt32((tempstr.Substring(2, 9)));
                tempnum++;
                tempstr = "PC" + tempnum.ToString().PadLeft(9, '0');
                return tempstr;
            }
        }

        /// <summary>
        /// 初始化Gridview
        /// </summary>
        private void InitGridview()
        {
            DataTable dt = this.GetDataFromGrid(true);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }


        /// <summary>
        /// 插入数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btninsert_Click(object sender, EventArgs e)
        {
            DataTable dt = this.GetDataFromGrid(false);
            int a = CommonFun.ComTryInt(txtNum.Text.Trim());
            for (int i = 0; i < a; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }

            this.GridView1.DataSource = dt;

            this.GridView1.DataBind();

            txtNum.Text = "";
        }


        /// <summary>
        /// 初始化表格
        /// </summary>
        /// <param name="isInit">是否是初始化</param>
        /// <returns></returns>
        protected DataTable GetDataFromGrid(bool isInit)
        {
            DataTable dt = new DataTable();

            #region
            dt.Columns.Add("WLCODE");
            dt.Columns.Add("WLNAME");

            dt.Columns.Add("WLMODEL");

            dt.Columns.Add("WLUNIT");

            dt.Columns.Add("WLNUM");

            dt.Columns.Add("WLPRICE");

            dt.Columns.Add("WLJE");

            dt.Columns.Add("WLBM");
            dt.Columns.Add("num");
            dt.Columns.Add("CODE");
            dt.Columns.Add("DEPNAME");
            dt.Columns.Add("Note");

            #endregion



            if (!isInit)
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];

                    DataRow newRow = dt.NewRow();

                    newRow["WLCODE"] = ((HtmlInputHidden)gRow.FindControl("WLCODE")).Value;
                    newRow["CODE"] = ((HtmlInputHidden)gRow.FindControl("CODE")).Value;
                    newRow["WLBM"] = ((TextBox)gRow.FindControl("WLBM")).Text;
                    newRow["WLNAME"] = ((TextBox)gRow.FindControl("WLNAME")).Text;
                    newRow["WLMODEL"] = ((HtmlInputText)gRow.FindControl("WLMODEL")).Value;
                    newRow["WLUNIT"] = ((HtmlInputText)gRow.FindControl("WLUNIT")).Value;
                    newRow["WLNUM"] = ((HtmlInputText)gRow.FindControl("WLNUM")).Value;
                    newRow["WLPRICE"] = ((HtmlInputText)gRow.FindControl("WLPRICE")).Value;
                    newRow["WLJE"] = ((HtmlInputText)gRow.FindControl("WLJE")).Value;
                    newRow["num"] = ((HtmlInputText)gRow.FindControl("num")).Value;
                    newRow["DEPNAME"] = ((HtmlInputText)gRow.FindControl("txtDep")).Value;
                    newRow["Note"] = ((TextBox)gRow.FindControl("txtNote")).Text;

                    dt.Rows.Add(newRow);
                }
            }

            //如果是初始化表格
            if (isInit)
            {
                for (int i = GridView1.Rows.Count; i < 1; i++)
                {
                    DataRow newRow = dt.NewRow();
                    dt.Rows.Add(newRow);
                }
            }

            dt.AcceptChanges();

            return dt;
        }


        /********************删除行******************************/

        protected void btndelete_Click(object sender, EventArgs e)
        {
            DataTable dt = this.GetDataFromGrid(false);

            //如果已经没有行了
            if (dt.Rows.Count == 0)
            {
                Response.Write("<script>alert('请选中删除行!')</script>");
            }
            else
            {
                for (int i = GridView1.Rows.Count - 1; i >= 0; i--)
                {
                    GridViewRow gr = GridView1.Rows[i];

                    CheckBox chk = (CheckBox)gr.FindControl("CheckBox1");

                    if (chk.Checked)
                    {
                        dt.Rows.RemoveAt(i);
                    }
                }

                this.GridView1.DataSource = dt;

                this.GridView1.DataBind();
            }
        }

        //保存操作

        protected void Save_Click(object sender, EventArgs e)
        {
            //此处是保存操作
            List<string> sqllist = new List<string>();

            bool isInsert = false;     //是否插入了TBOM_BGYPPCAPPLYINFO采购信息表

            string sql = "";
            string Code = LabelCode.Text;//单号
            string Date = TextBoxDate.Text;//日期
            string jbrid = LabelDocCode.Text;//制单人
            string jbr = LabelDoc.Text;

            float jine = 0;
            string shrf = txt_first.Text;
            string shrfid = SHR1id.Text.Trim();
            string shrfnote = first_opinion.Text;
            string shrfdate = first_time.Text;

            string note = txt_note.Text;

            //审核人填写正确之后开始对需要采购的物料单、采购申请表插入数据
            if (flag == "add" || flag == "safePC")
            {
                if (String.IsNullOrEmpty(shrf) || String.IsNullOrEmpty(shrfid))
                {
                    Response.Write("<script>alert('请正确填写审核人!')</script>");
                    return;
                }
                else
                {
                    for (int i = 0; i < GridView1.Rows.Count; i++)
                    {
                        CheckBox ckbx = ((CheckBox)this.GridView1.Rows[i].FindControl("CheckBox1"));

                        string code_SQ = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("CODE")).Value.Trim();
                        string sId = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("WLCODE")).Value.Trim();//物料代码

                        if (sId != string.Empty)    //如果物料代码不为空
                        {
                            string bm = ((TextBox)this.GridView1.Rows[i].FindControl("WLBM")).Text;
                            string name = ((TextBox)this.GridView1.Rows[i].FindControl("WLNAME")).Text.Trim();
                            string model = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLMODEL")).Value.Trim();
                            string unit = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLUNIT")).Value.Trim();
                            string num = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNUM")).Value.Trim();//数量
                            string dj = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLPRICE")).Value.Trim();//数量
                            string je = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLJE")).Value.Trim();//金额
                            string txtNote = ((TextBox)this.GridView1.Rows[i].FindControl("txtNote")).Text.Trim();

                            //将使用申请单中对应物料的CG(采购）设置为1，审核状态变为待审2
                            string sql_SQ = "update TBOM_BGYPAPPLY set CG='1' where  WLCODE='" + sId + "' and REVIEWSTATE='2'";
                            DBCallCommon.ExeSqlText(sql_SQ);

                            jine += float.Parse(je);
                            Regex regMoeny = new Regex(@"^-?(\d)*((.?)(\d){1,2})?$");
                            Regex regNum = new Regex(@"^-?(\d)*((.?)(\d){1,2})?$");
                            if (regNum.IsMatch(num) && regMoeny.IsMatch(dj) && regMoeny.IsMatch(je) && !String.IsNullOrEmpty(num) && !String.IsNullOrEmpty(dj))
                            {
                                sql = "insert into TBOM_BGYPPCAPPLYINFO(WLCODE,WLBM,WLNAME,WLMODEL,WLUNIT,WLPRICE,WLNUM,WLJE,CODE,DEPNAME,NOTE) values('" + sId + "','" + bm + "','" + name + "','" + model + "','" + unit + "','" + dj + "','" + num + "','" + je + "','" + Code + "','" + Session["UserDept"].ToString() + "','" + txtNote + "')";
                                sqllist.Add(sql);
                                isInsert = true;
                            }
                            else
                            {
                                Response.Write("<script>alert('第" + i + 1 + "行数据有误，请查证后在进行保存!')</script>");
                                return;
                            }
                        }
                    }

                    //如果插入了采购数据就加入审批任务
                    if (isInsert)
                    {
                        sql = "insert into TBOM_BGYPPCAPPLY(PCCODE,JBR,JBRID,JBRDEPID,SHRF,SHRFID,SHRFDATE,SHRFNOTE,SHRS,SHRSID,SHRSDATE,SHRSNOTE,NOTE,DATE,JINE,STATE) VALUES ('" + Code + "','" + jbr + "','" + jbrid + "','" + Session["UserDeptID"].ToString() + "','" + shrf + "','" + shrfid + "','" + shrfdate + "','" + shrfnote + "','','','','','" + note + "','" + Date + "','" + jine + "','1')";
                        sqllist.Add(sql);
                        DBCallCommon.ExecuteTrans(sqllist);
                        string _emailto = DBCallCommon.GetEmailAddressByUserID(shrfid);
                        string _body = "办公用品采购审批任务:"
                               + "\r\n编 号：" + LabelCode.Text.ToString().Trim()
                               + "\r\n制单日期：" + TextBoxDate.Text.ToString().Trim();

                        string _subject = "您有新的【办公用品采购】需要审批，请及时处理:" + LabelCode.Text.ToString().Trim();
                        DBCallCommon.SendEmail(_emailto, null, null, _subject, _body);
                        Response.Redirect("OM_BgypPcApplyMain.aspx");
                    }
                    else
                    {
                        Response.Write("<script>alert('请插入有效数据!')</script>");
                    }
                }
            }


            if (flag == "verify")
            {
                if (Session["UserID"].ToString() == shrfid && state.Text != "2")
                {
                    if (rblfirst.SelectedIndex == -1)
                    {
                        Response.Write("<script>alert('请选择审核结果!')</script>");
                        return;
                    }
                    sql = "update TBOM_BGYPPCAPPLY set STATE='" + rblfirst.SelectedValue.ToString() + "', SHRFDATE='" + DateTime.Now.ToString("yyyy-MM-dd") + "',SHRFNOTE='" + first_opinion.Text.ToString() + "' WHERE PCCODE='" + Code + "'";
                    DBCallCommon.ExeSqlText(sql);
                    Response.Redirect("OM_BgypPcApplyMain.aspx");
                }

            }


            if (flag == "mod")
            {
                sql = "delete from TBOM_BGYPPCAPPLYINFO where CODE='" + Code + "'";
                sqllist.Add(sql);
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {

                    string sId = ((HtmlInputHidden)this.GridView1.Rows[i].FindControl("WLCODE")).Value.Trim();//物料代码

                    if (sId != string.Empty)
                    {
                        string bm = ((TextBox)this.GridView1.Rows[i].FindControl("WLBM")).Text;
                        string name = ((TextBox)this.GridView1.Rows[i].FindControl("WLNAME")).Text.Trim();
                        string model = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLMODEL")).Value.Trim();
                        string unit = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLUNIT")).Value.Trim();
                        string num = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLNUM")).Value.Trim();//数量
                        string dj = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLPRICE")).Value.Trim();//数量
                        string je = ((HtmlInputText)this.GridView1.Rows[i].FindControl("WLJE")).Value.Trim();//金额
                        string txtNote = ((TextBox)this.GridView1.Rows[i].FindControl("txtNote")).Text.Trim();

                        jine += float.Parse(je);
                        Regex regMoeny = new Regex(@"^-?(\d)*((.?)(\d){1,2})?$");
                        Regex regNum = new Regex(@"^-?(\d)*((.?)(\d){1,2})?$");
                        if (regNum.IsMatch(num) && regMoeny.IsMatch(dj) && regMoeny.IsMatch(je))
                        {
                            sql = "insert into TBOM_BGYPPCAPPLYINFO(WLCODE,WLBM,WLNAME,WLMODEL,WLUNIT,WLPRICE,WLNUM,WLJE,CODE,DEPNAME,NOTE) values('" + sId + "','" + bm + "','" + name + "','" + model + "','" + unit + "','" + dj + "','" + num + "','" + je + "','" + Code + "','" + Session["UserDept"].ToString() + "','" + txtNote + "')";
                            sqllist.Add(sql);
                            isInsert = true;
                        }
                        else
                        {
                            Response.Write("<script>alert('第" + i + 1 + "行数据有误，请查证后在进行保存!')</script>");
                            return;
                        }
                    }
                }

                //如果插入了采购数据就加入审批任务
                if (isInsert)
                {
                    sql = "update TBOM_BGYPPCAPPLY SET STATE='1',JINE='" + jine + "',NOTE='" + txt_note.Text + "' WHERE PCCODE='" + Code + "'";
                    sqllist.Add(sql);
                    DBCallCommon.ExecuteTrans(sqllist);
                    Response.Redirect("OM_BgypPcApplyMain.aspx");
                }
                else
                {
                    Response.Write("<script>alert('请插入有效数据!')</script>");
                }
            }
        }
    }
}
