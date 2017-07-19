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
using System.IO;
using System.Collections.Generic;

namespace ZCZJ_DPF.CM_Data
{
    public partial class PD_DocManage : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        string sqltext;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnDelete.Click += new EventHandler(btnDelete_Click);
            btnDelete.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
            InitVar();
            if (!IsPostBack)
            {
                this.DataPJname();
                this.DataENGname();
                this.DataYeZhu();
                this.DataZhiDanRen();
                doPost(rbl_mytask);
                doPost(rbl_status);      
                bindGrid();
            }
            CheckUser(ControlFinder);
        }
        private void DataPJname()
        {
            sqltext = "select distinct PROJECT from TBBS_CONREVIEW ";
            string DataText = "PROJECT";
            string DataValue = "PROJECT";
            DBCallCommon.BindDdl(ddlpjname, sqltext, DataText, DataValue);
        }
        private void DataENGname()
        {
            sqltext = "select distinct ENGINEER from TBBS_CONREVIEW where PROJECT='" + ddlpjname.SelectedValue + "'";
            string DataText = "ENGINEER";
            string DataValue = "ENGINEER";
            DBCallCommon.BindDdl(ddlengname, sqltext, DataText, DataValue);
        }
        private void DataYeZhu()
        {
            sqltext = "select distinct YEZHU from TBBS_CONREVIEW ";
            string DataText = "YEZHU";
            string DataValue = "YEZHU";
            DBCallCommon.BindDdl(ddlyezhu, sqltext, DataText, DataValue);
        }
        private void DataZhiDanRen()
        {
            sqltext = "select  distinct BC_DRAFTER,ST_NAME from TBBS_CONREVIEW left outer join TBDS_STAFFINFO on TBBS_CONREVIEW.BC_DRAFTER=TBDS_STAFFINFO.ST_CODE ";
            string DataText = "ST_NAME";
            string DataValue = "BC_DRAFTER";
            DBCallCommon.BindDdl(ddlzhidanren, sqltext, DataText, DataValue);
        }
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数      
        }

        //初始化分布信息
        private void InitPager()
        {
            pager.TableName = "TBBS_CONREVIEW";
            pager.PrimaryKey = "BC_ID";
            pager.ShowFields = "BC_ID*1 as BC_ID,BC_CONID,PROJECT,ENGINEER,YEZHU,BC_NUMBER,JINE,BC_DRAFTER, BP_ACPDATE ,BP_STATUS,BP_NOTE,BP_SPSTATUS,BP_YESORNO";
            pager.OrderField = "BP_ACPDATE";
            pager.StrWhere = CreateConStr();
            pager.OrderType = 1;
            pager.PageSize = 10;
        }

        private string CreateConStr()
        {
            string status = rbl_status.SelectedValue.ToString();//状态，0，N，1
            string strWhere = string.Empty;
            if (rbl_mytask.SelectedItem.Text == "全部")
            {
                //审核状态
                if (rbl_status.SelectedItem.Text == "已驳回")
                {
                    strWhere = "BP_YESORNO='N'";
                }
                else if (rbl_status.SelectedItem.Text == "待审核")
                {
                    strWhere = "BP_STATUS ='0' AND BP_YESORNO='Y'";
                }
                else
                {
                    strWhere = "BP_STATUS ='1' AND BP_YESORNO='Y'";
                }
            }
            else
            {
                string userID = Session["UserID"].ToString();
                //我的任务，显示我的未审核的任务，隐藏那个状态选择的按钮

                if (rbl_status.SelectedItem.Text == "已驳回")
                {
                    strWhere = "BP_YESORNO ='N' AND ((BC_REVIEWERA='" + userID + "' AND  BP_RVIEWA!='') or (BC_DRAFTER='" + userID + "') or (BC_REVIEWERB='" + userID + "' AND  BP_RVIEWB!='') or (BC_REVIEWERC='" + userID + "' AND  BP_RVIEWC!='') or (BC_REVIEWERD='" + userID + "' AND  BP_RVIEWD!='') or (BC_REVIEWERE='" + userID + "' AND  BP_RVIEWE!='') or (BC_REVIEWERF='" + userID + "' AND  BP_RVIEWF!='') or (BC_REVIEWERG='" + userID + "' AND  BP_RVIEWG!='') OR (BC_REVIEWERH='" + userID + "' AND  BP_RVIEWH!='') OR (BC_REVIEWERI='" + userID + "' AND  BP_RVIEWI!='') OR (BC_REVIEWERJ='" + userID + "' AND  BP_RVIEWJ!=''))";
                    rbl_status.Visible = true;
                }
                else if (rbl_status.SelectedItem.Text == "待审核")
                {
                    string mydep = Session["UserDeptID"].ToString();
                    if (mydep.Equals("01"))
                    {
                        strWhere = "BP_LEADPS='1' AND BP_STATUS ='0' AND BP_YESORNO ='Y' AND ((BC_REVIEWERA='" + userID + "' AND  (BP_RVIEWA='' or BP_RVIEWA is null)) or (BC_DRAFTER='" + userID + "') or (BC_REVIEWERB='" + userID + "' AND  (BP_RVIEWB='' or BP_RVIEWB is null)) or (BC_REVIEWERC='" + userID + "' AND  (BP_RVIEWC='' or BP_RVIEWC is null)) or (BC_REVIEWERD='" + userID + "' AND (BP_RVIEWD='' or BP_RVIEWD is null)) or (BC_REVIEWERE='" + userID + "' AND  (BP_RVIEWE='' or BP_RVIEWE is null)) or (BC_REVIEWERF='" + userID + "' AND  (BP_RVIEWF='' or BP_RVIEWF is null)) or (BC_REVIEWERG='" + userID + "' AND  (BP_RVIEWG='' or BP_RVIEWG is null)) OR (BC_REVIEWERH='" + userID + "' AND  (BP_RVIEWH='' or BP_RVIEWH is null)) OR (BC_REVIEWERI='" + userID + "' AND  (BP_RVIEWI='' or BP_RVIEWI is null)) OR (BC_REVIEWERJ='" + userID + "' AND  (BP_RVIEWJ='' or BP_RVIEWJ is null)))";
                    }
                    else
                    {
                        strWhere = "BP_LEADPS!='1' AND BP_STATUS ='0' AND BP_YESORNO ='Y' AND ((BC_REVIEWERA='" + userID + "' AND  (BP_RVIEWA='' or BP_RVIEWA is null)) or (BC_DRAFTER='" + userID + "') or (BC_REVIEWERB='" + userID + "' AND  (BP_RVIEWB='' or BP_RVIEWB is null)) or (BC_REVIEWERC='" + userID + "' AND  (BP_RVIEWC='' or BP_RVIEWC is null)) or (BC_REVIEWERD='" + userID + "' AND (BP_RVIEWD='' or BP_RVIEWD is null)) or (BC_REVIEWERE='" + userID + "' AND  (BP_RVIEWE='' or BP_RVIEWE is null)) or (BC_REVIEWERF='" + userID + "' AND  (BP_RVIEWF='' or BP_RVIEWF is null)) or (BC_REVIEWERG='" + userID + "' AND  (BP_RVIEWG='' or BP_RVIEWG is null)) OR (BC_REVIEWERH='" + userID + "' AND  (BP_RVIEWH='' or BP_RVIEWH is null)) OR (BC_REVIEWERI='" + userID + "' AND  (BP_RVIEWI='' or BP_RVIEWI is null)) OR (BC_REVIEWERJ='" + userID + "' AND  (BP_RVIEWJ='' or BP_RVIEWJ is null)))";
                    }
                }
                else
                {
                    strWhere = "BP_YESORNO!='N' AND ((BC_REVIEWERA='" + userID + "' AND  BP_RVIEWA!='') or (BC_DRAFTER='" + userID + "' and BP_STATUS='1') or (BC_REVIEWERB='" + userID + "' AND  BP_RVIEWB!='') or (BC_REVIEWERC='" + userID + "' AND  BP_RVIEWC!='') or (BC_REVIEWERD='" + userID + "' AND  BP_RVIEWD!='') or (BC_REVIEWERE='" + userID + "' AND  BP_RVIEWE!='') or (BC_REVIEWERF='" + userID + "' AND  BP_RVIEWF!='') or (BC_REVIEWERG='" + userID + "' AND  BP_RVIEWG!='') OR (BC_REVIEWERH='" + userID + "' AND  BP_RVIEWH!='') OR (BC_REVIEWERI='" + userID + "' AND  BP_RVIEWI!='') OR (BC_REVIEWERJ='" + userID + "' AND  BP_RVIEWJ!=''))";
                }
            }

            if (ddlpjname.SelectedIndex != 0)
            {
                strWhere += " AND PROJECT='" + ddlpjname.SelectedValue + "'";
            }
            if (ddlengname.SelectedIndex != 0)
            {
                strWhere += " and ENGINEER='" + ddlengname.SelectedValue + "' ";
            }
            if (ddlyezhu.SelectedIndex != 0)
            {
                strWhere += " and YEZHU='" + ddlyezhu.SelectedValue + "'";
            }
            if (ddlzhidanren.SelectedIndex != 0)
            {
                strWhere += " and BC_DRAFTER='" + ddlzhidanren.SelectedValue + "'";
            }
            return strWhere;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }

        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
                btnDelete.Visible = false;
                btnAgain.Visible = false;
            }
            else
            {
                btnDelete.Visible = true;
                btnAgain.Visible = true;
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            if (YesOrNo())
            {
                List<string> tb_id = new List<string>();
                string bcid = "";
                foreach (GridViewRow gr in GridView1.Rows)
                {
                    CheckBox chk = (CheckBox)gr.FindControl("CKBOX_SELECT");
                    if (chk.Checked)
                    {
                        bcid = ((Label)gr.FindControl("lbid")).Text;
                        tb_id.Add(bcid);
                    }
                }
                foreach (string id in tb_id)
                {
                    string sqlSelect = "select BC_CONTEXT from TBBS_CONREVIEW where BC_ID='" + id + "'";
                    using (DataSet ds = DBCallCommon.FillDataSet(sqlSelect))
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            string context = ds.Tables[0].Rows[0]["BC_CONTEXT"].ToString();
                            DeleteTFN(context);//删除文件
                            string sqldelete1 = "DELETE FROM tb_files WHERE BC_CONTEXT = '" + context + "'";
                            string sqldelete2 = "DELETE FROM TBBS_CONREVIEW WHERE BC_ID = '" + id + "'";
                            List<string> sqlTexts = new List<string>();
                            sqlTexts.Add(sqldelete1);
                            sqlTexts.Add(sqldelete2);
                            DBCallCommon.ExecuteTrans(sqlTexts);//执行存储过程
                        }
                    }
                }
                Response.Redirect("PD_DocManage.aspx");
            }
            else
            {
                string script = @"alert('已进出评审中流程或者您不是制单人,无法删除');";
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", script, true);
            }
        }


        private bool YesOrNo()
        {
            Label lb = new Label();
            Label yorn = new Label();
            Label zdr = new Label();
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox cbox = (CheckBox)gr.FindControl("CKBOX_SELECT");
                if (cbox.Checked == true)
                {
                    lb = (Label)gr.FindControl("lb_status");
                    yorn = (Label)gr.FindControl("lb_yesorno");
                    zdr = (Label)gr.FindControl("lb_zdr");
                    break;
                }

            }
            if (zdr.Text == Session["UserID"].ToString())
            {
                if (lb.Text.Trim().Contains("1") && yorn.Text != "N")//已经评审，且评审不是不通过的时候不能删除
                {
                    //表示有评审
                    return false;
                }
                else//还没开始评审，或者评审不通过
                {
                    //表示未评审
                    return true;
                }
            }
            else//不是制单人不能删除
            {
                return false;
            }

        }

        //删除文件
        protected void DeleteTFN(string context)
        {
            string sqlStr = "select fileName from tb_files where BC_CONTEXT='" + context + "'";
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取指定文件的路径
            //Response.Write(ds.Tables[0].Rows.Count);
            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
            {
                string strFilePath = Server.MapPath("../Contract/") + ds.Tables[0].Rows[i][0].ToString();
                File.Delete(strFilePath);//文件不存在也不会引发异常
            }
        }


        protected void rbl_status_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DataPJname();
            this.DataENGname();
            this.DataYeZhu();
            this.DataZhiDanRen();
            UCPaging1.CurrentPage = 1;
            RebindGrid();

        }

        protected void rbl_mytask_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.DataPJname();
            this.DataENGname();
            this.DataYeZhu();
            this.DataZhiDanRen();
            UCPaging1.CurrentPage = 1;
            RebindGrid();

        }
        protected void ddlpjname_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataENGname();
            //DataYeZhu();
            //DataZhiDanRen();
            UCPaging1.CurrentPage = 1;
            RebindGrid();
        }

        protected void ddlengname_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            RebindGrid();
        }
        protected void ddlyezhu_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            RebindGrid();
        }
        protected void ddlzhidanren_SelectedIndexChanged(object sender, EventArgs e)
        {

            UCPaging1.CurrentPage = 1;
            RebindGrid();
        }

        private void RebindGrid()
        {
            InitPager();
            bindGrid();
            //CheckUser(ControlFinder);
        }

        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
                Label lb = (Label)gr.FindControl("lb_status");
                Label yorn = (Label)gr.FindControl("lb_yesorno");
                Label zdr = (Label)gr.FindControl("lb_zdr");
                if (lb.Text.Trim().Contains("0"))
                {
                    //说明还有未审核的
                    ((Label)gr.FindControl("lb_yesorno")).Visible = false;
                }
                else
                {
                    //表示审核完毕
                    ((Label)gr.FindControl("lb_yesorno")).Visible = true;
                }

                if (zdr.Text == Session["UserID"].ToString())//登录人为制单人
                {
                    if (yorn.Text == "N")//被驳回了能够修改
                    {
                        ((HyperLink)gr.FindControl("hlTask_xg")).Visible = true;
                    }
                    else
                    {
                        if (lb.Text.Contains("1"))//在审核中不能修改
                        {
                            ((HyperLink)gr.FindControl("hlTask_xg")).Visible = false;
                        }
                        else//提交还未开始审核才能修改
                        {
                            ((HyperLink)gr.FindControl("hlTask_xg")).Visible = true;
                        }
                    }
                }
                else//登录人不是制单人不能修改
                {
                    ((HyperLink)gr.FindControl("hlTask_xg")).Visible = false;
                }

                if (rbl_mytask.SelectedValue == "0")
                {
                    ((HyperLink)gr.FindControl("hlTask_ps")).Visible = false;
                }
            }
        }

        protected void btnAgain_Click(object sender, EventArgs e)
        {
            List<string> tb_id = new List<string>();
            string bcid = "";
            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gr.FindControl("CKBOX_SELECT");
                if (chk.Checked)
                {
                    bcid = ((Label)gr.FindControl("lbid")).Text;
                    tb_id.Add(bcid);
                }
            }
            if (tb_id.Count == 1)
            {
                string sqlSelect = "select * from TBBS_CONREVIEW where BC_ID='" + bcid + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlSelect);
                string project = dt.Rows[0]["PROJECT"].ToString();
                string engineer = dt.Rows[0]["ENGINEER"].ToString();
                string yezhu = dt.Rows[0]["YEZHU"].ToString();
                Response.Redirect("PD_DocTypeIn.aspx?action=add&conName=" + project + "&txtengnm=" + engineer + "&txtyz=" + yezhu + "");
            }
            else
            {
                string script = @"alert('必须选择一项，请重新选择');";
                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", script, true);
            }
        }

        protected void doPost(RadioButtonList rbList)
        {
            foreach (ListItem item in rbList.Items)
            {
                //为预设项添加doPostBack JS  
                if (item.Selected)
                {
                    item.Attributes.Add("onclick", String.Format("javascript:setTimeout('__doPostBack(\\'{0}${1}\\',\\'\\')', 0)", rbList.UniqueID, rbList.Items.IndexOf(item)));
                }
            }
        }
    }
}
