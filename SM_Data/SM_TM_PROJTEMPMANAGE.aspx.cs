using System;
using System.Collections;
using System.Collections.Generic;
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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_TM_PROJTEMPMANAGE : System.Web.UI.Page
    {
        

        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {

            InitVar();
            if (!IsPostBack)
            {
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;               
                bindData();
                this.Form.DefaultButton = QueryButton.UniqueID;

            }
        }


        private void InitVar()
        {
            if (MyTask.Checked)
            {
               
                string userid = Session["UserID"].ToString();
                string dep = userid.Substring(0,2);
                if (dep == "03")
                {
                    
                    string sqltext = "select st_code from TBDS_STAFFINFO where st_position='技术部部长' and st_state='0' and st_depid='03'";
                    string sqluserid = DBCallCommon.GetDTUsingSqlText(sqltext).Rows[0][0].ToString();
                    
                    if (sqluserid==userid)
                    {
                        TextBoxManager.Text = Session["UserName"].ToString();
                        RadioButtonListState.SelectedValue = "2";
                    }
                    else
                    {
                        TextBoxVerifier.Text = Session["UserName"].ToString();
                        RadioButtonListState.SelectedValue = "1";

                    }

                }
               else if (dep == "09")
                {

                    string sqltext1 = "select st_code from TBDS_STAFFINFO where st_position='电气制造部部长' and st_state='0' and st_depid='09'";
                    string sqluserid1 = DBCallCommon.GetDTUsingSqlText(sqltext1).Rows[0][0].ToString();

                    if (sqluserid1 == userid)
                    {
                        TextBoxManager.Text = Session["UserName"].ToString();
                        RadioButtonListState.SelectedValue = "2";
                    }
                    else
                    {
                        TextBoxVerifier.Text = Session["UserName"].ToString();
                        RadioButtonListState.SelectedValue = "1";

                    }

                } 
                else
                {
                    MyTask.Checked = false;
                    RadioButtonListState.SelectedValue = "";
                }

               
            }
           
            getInfo();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;
        }

        //初始化分页信息
        private void InitPager(string TableName, string PrimaryKey, string ShowFields, string OrderField, int OrderType, string StrWhere, int PageSize)
        {
            pager.TableName = TableName;

            pager.PrimaryKey = PrimaryKey;

            pager.ShowFields = ShowFields;

            pager.OrderField = OrderField;

            pager.OrderType = OrderType;

            pager.StrWhere = StrWhere;

            pager.PageSize = PageSize;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindData();
        }

        protected void bindData()
        {
            pager.PageIndex = UCPaging1.CurrentPage;

            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);

            CommonFun.Paging(dt, Repeater1, UCPaging1, NoDataPanel);

            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            if (!CheckBoxShow.Checked)
            {
                BindItem();
            }

            SetRadioButtonTip();
        }
    

        protected void SetRadioButtonTip()
        {
            
            int dsh = 0;    //待提交
            int mydsh = 0;   //我的待待提交
            int dsp = 0;    //待审批
            int mydsp = 0;  //我的待审批


           
            string sqltext_dsh = "select count(*) from tbws_projtemp where  pt_state='1'"; //待提交
            string sqltext_mydsh = "select count(*) from tbws_projtemp where  pt_state='1' and pt_verifier='" + Session["UserId"].ToString() + "'";

            string sqltext_dsp = "select count(*) from tbws_projtemp where  pt_state='2'"; //待审批
            string sqltext_mydsp = "select count(*) from tbws_projtemp where pt_state='2' and pt_manager='" + Session["UserId"].ToString() + "'";

           
            dsh = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_dsh).Rows[0][0].ToString());
            mydsh = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_mydsh).Rows[0][0].ToString());

            dsp = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_dsp).Rows[0][0].ToString());
            mydsp = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(sqltext_mydsp).Rows[0][0].ToString());
           
            RadioButtonListState.Items.FindByValue("1").Text = "未提交<font color=red>(" + mydsh + "/" + dsh + ")</font>"; //技术员未提交
            RadioButtonListState.Items.FindByValue("2").Text = "待审批<font color=red>(" + mydsp + "/" + dsp + ")</font>"; //待部长审核

        }

        protected string GetStrCondition()
        {
            string state = RadioButtonListState.SelectedValue.ToString();

            string condition = "";
            //单号条件
            if (TextBoxPTCode.Text != "")
            {
                condition = " PT_CODE LIKE '%" + TextBoxPTCode.Text.Trim() + "%'";
            }
            //制单时间条件
            if ((TextBoxDate.Text != "") && (condition != ""))
            {
                condition += " AND " + " ZHDTIME LIKE '%" + TextBoxDate.Text.Trim() + "%'";
            }
            else if ((TextBoxDate.Text != "") && (condition == ""))
            {
                condition += " ZHDTIME LIKE '%" + TextBoxDate.Text.Trim() + "%'";
            }
              
            //制单人
            if ((TextBoxDoc.Text != "") && (condition != ""))
            {
                condition += " AND " + " DOC LIKE '%" + TextBoxDoc.Text.Trim() + "%'";
            }
            else if ((TextBoxDoc.Text != "") && (condition == ""))
            {
                condition += " DOC LIKE '%" + TextBoxDoc.Text.Trim() + "%'";
            }
            //技术员是审核人
            if ((TextBoxVerifier.Text != "") && (condition != ""))
            {
                condition += " AND " + " JSHY LIKE '%" + TextBoxVerifier.Text.Trim() + "%'";
            }
            else if ((TextBoxVerifier.Text != "") && (condition == ""))
            {
                condition += " JSHY LIKE '%" + TextBoxVerifier.Text.Trim() + "%'";
            }
            //审批人为部门领导
            if ((TextBoxManager.Text != "") && (condition != ""))
            {
                condition += " AND " + " MANGER LIKE '%" + TextBoxManager.Text.Trim() + "%'";
            }
            else if ((TextBoxManager.Text != "") && (condition == ""))
            {
                condition += " MANGER LIKE '%" + TextBoxManager.Text.Trim() + "%'";
            }
            //技术提交日期，审核日期
            if ((TextBoxVerifyDate.Text != "") && (condition != ""))
            {
                condition += " AND " + " JSHYTIME LIKE '%" + TextBoxVerifyDate.Text.Trim() + "%'";
            }
            else if ((TextBoxVerifyDate.Text != "") && (condition == ""))
            {
                condition += " JSHYTIME LIKE '%" + TextBoxVerifyDate.Text.Trim() + "%'";
            }
            //项目名称
            if ((TextBoxProjname.Text != "") && (condition != ""))
            {
                condition += " AND " + " PROJNAME LIKE '%" + TextBoxProjname.Text.Trim() + "%'";
            }
            else if ((TextBoxProjname.Text != "") && (condition == ""))
            {
                condition += " PROJNAME LIKE '%" + TextBoxProjname.Text.Trim() + "%'";
            }
            //工程名称
            if ((TextBoxEngname.Text != "") && (condition != ""))
            {
                condition += " AND " + " ENGNAME LIKE '%" + TextBoxEngname.Text.Trim() + "%'";
            }
            else if ((TextBoxEngname.Text != "") && (condition == ""))
            {
                condition += " ENGNAME LIKE '%" + TextBoxEngname.Text.Trim() + "%'";
            }
            //生产制号
            if ((TextBoxSCZH.Text != "") && (condition != ""))
            {
                condition += " AND " + " SCZH LIKE '%" + TextBoxSCZH.Text.Trim().ToUpper() + "%'";
            }
            else if ((TextBoxSCZH.Text != "") && (condition == ""))
            {
                condition += " SCZH LIKE '%" + TextBoxSCZH.Text.Trim().ToUpper() + "%'";
            }  
            //审批日期
            if ((TextBoxShPiDate.Text != "") && (condition != ""))
            {
                condition += " AND " + " MANAGERTIME LIKE '%" + TextBoxShPiDate.Text.Trim() + "%'";
            }
            else if ((TextBoxShPiDate.Text != "") && (condition == ""))
            {
                condition += " MANAGERTIME LIKE '%" + TextBoxShPiDate.Text.Trim() + "%'";
            }  
            

            switch (state)
            {
                case "":

                    break;
                
                case "1":  //储运已提交，技术待审核

                    if (condition != "") { condition += " AND PT_STATE='1'"; }
                    else { condition += " PT_STATE='1' "; }
                    break;
                case "2": //技术员已提交，待部长审核

                    if (condition != "") { condition += " AND PT_STATE='2'"; }
                    else { condition += " PT_STATE='2'"; }
                    break;
                case "3":   //已审批

                    if (condition != "") { condition += " AND PT_STATE='3'"; } 
                    else { condition += " PT_STATE='3'"; }
                    break;
                
                default: break;
            }
            return condition;
        }

        protected void getInfo()
        {
            string condition = GetStrCondition();
            if (condition == "")
            {
                string TableName = "View_SM_TM_PROJTEMP";
                string PrimaryKey = "PT_CODE";
                string ShowFields = "PT_CODE AS Code,ZHDTIME as DOCDATE,DOC AS DOC,JSHY as JSHY,left(JSHYTIME,10) as JSHYDATE,PT_STATE AS State,MANGER AS MANAGER,PROJNAME AS PROJNAME,ENGNAME AS ENGNAME,SCZH AS SCZH,LEFT(MANAGERTIME,10) AS MANAGERDATE";
                string OrderField = "PT_CODE DESC,SCZH";
                int OrderType = 0;
                string StrWhere = "";
                int PageSize = 15;

                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);


            }
            else
            {

                string TableName = "View_SM_TM_PROJTEMP";
                string PrimaryKey = "PT_CODE";
                string ShowFields = "PT_CODE AS Code,ZHDTIME as DOCDATE,DOC AS DOC,JSHY as JSHY,left(JSHYTIME,10) as JSHYDATE,PT_STATE AS State,MANGER AS MANAGER,PROJNAME AS PROJNAME,ENGNAME AS ENGNAME,SCZH AS SCZH,LEFT(MANAGERTIME,10) AS MANAGERDATE";
                string OrderField = "PT_CODE DESC,SCZH";                
                int OrderType = 0;
                string StrWhere = condition;
                int PageSize = 15;

                InitPager(TableName, PrimaryKey, ShowFields, OrderField, OrderType, StrWhere, PageSize);

            }

        }

        protected void RadioButtonListState_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;

            bindData();

        }
        protected void MyTask_CheckedChanged(object sender, EventArgs e)
        {
            if (MyTask.Checked)
            {
                string userid = Session["UserID"].ToString();
                string jshytask = "select count(*) from tbws_projtemp where PT_STATE='1'and PT_VERIFIER='"+userid+"' ";
                string managertask="select count(*) from tbws_projtemp where PT_STATE='2'and PT_MANAGER='"+userid+"' ";
                int jshynum = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(jshytask).Rows[0][0].ToString());
                int managernum = Convert.ToInt16(DBCallCommon.GetDTUsingSqlText(managertask).Rows[0][0].ToString());
                if(jshynum > 0)
                {
                    TextBoxVerifier.Text = Session["UserName"].ToString();

                }
                if (managernum > 0)
                {
                    TextBoxManager.Text = Session["UserName"].ToString();
                }

               
            }
            else
            {
                TextBoxVerifier.Text = string.Empty;
                TextBoxManager.Text = string.Empty;
            }
            UCPaging1.CurrentPage = 1;
            getInfo();
            bindData();

        }

        protected string convertState(string state)
        {
            switch (state)
            {               
                case "1": return "待提交";
                case "2": return "待审批";
                case "3": return "已审批";               
                default: return state;
            }
        }

        //查询
        protected void QueryButton_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;

            bindData();

            ModalPopupExtenderSearch.Hide();

            UpdatePanelBody.Update();
        }

        //关闭
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }
        //重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearCondition();
        }

        //单据头显示
        protected void CheckBoxShow_CheckedChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            bindData();
            UpdatePanelBody.Update();
        }



        //清除条件
        private void clearCondition()
        {

            //审核状态
            foreach (ListItem lt in RadioButtonListState.Items)
            {
                if (lt.Selected)
                {
                    lt.Selected = false;
                }
            }
            RadioButtonListState.Items[0].Selected = true;
            TextBoxVerifier.Text = string.Empty;
            TextBoxDate.Text = string.Empty;
            TextBoxSCZH.Text = string.Empty;
            TextBoxPTCode.Text = string.Empty;
            TextBoxEngname.Text = string.Empty;
            TextBoxProjname.Text = string.Empty;
            TextBoxShPiDate.Text = string.Empty;
            TextBoxDoc.Text = string.Empty;
            if (MyTask.Checked == false)
            {
                TextBoxVerifier.Text = string.Empty;
            }

            TextBoxVerifyDate.Text = string.Empty;

        }

        private void BindItem()
        {
            for (int i = 0; i < (Repeater1.Items.Count - 1); i++)
            {

                Label lbCode = (Repeater1.Items[i].FindControl("LabelCode") as Label);

                string NextCode = lbCode.Text;

                if (lbCode.Visible)
                {
                    for (int j = i + 1; j < Repeater1.Items.Count; j++)
                    {
                        string Code = (Repeater1.Items[j].FindControl("LabelCode") as Label).Text;

                        if (NextCode == Code)
                        {
                            (Repeater1.Items[j].FindControl("LabelCode") as Label).Style.Add("display", "none");
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
        }
    }
}
