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
using System.IO;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Inspection_Manage : BasicPage
    {
      
        PagerQueryParam pager = new PagerQueryParam();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            btnDelete.Click += new EventHandler(btnDelete_Click);
            btnDelete.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");

            InitVar();
            
            if (!IsPostBack)
            {
                bindGrid();
                BindDropDownListInspecMan();
            }
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
            pager.TableName = "TBQM_APLYFORINSPCT";
            pager.PrimaryKey = "AFI_ID";
            pager.ShowFields = "AFI_ISSH,AFI_SHJG,AFI_ID*1 AS AFI_ID,AFI_TSDEP,AFI_PJNAME,AFI_ENGID,AFI_ENGNAME,AFI_PARTNAME ,AFI_DATACLCT ,left(AFI_DATE,10) as AFI_DATE,AFI_RQSTCDATE,AFI_SUPPLERNM,AFI_ISPCTSITE,AFI_CONTACT,AFI_CONTEL,AFI_MAN,AFI_MANNM,AFI_NOTE,AFI_NUMBER,UNIQUEID,AFI_QCMANNM,isnull(AFI_ENDRESLUT,'') as AFI_ENDRESLUT,left(AFI_ENDDATE,10) as AFI_ENDDATE";
            pager.OrderField = "AFI_RQSTCDATE";//按报检时间排序
            pager.StrWhere = getStrWhere();
            pager.OrderType = 1;//按时间降序排列
            pager.PageSize = 10;
        }

        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        //报检人绑定
        protected void BindDropDownListInspecMan()
        {
            string sqltext="select AFI_MANNM from TBQM_APLYFORINSPCT where AFI_TSDEP='"+DropDownListDep.SelectedItem.ToString().Trim()+"' group by AFI_MANNM";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            DropDownListInspecMan.DataSource = dt;
            DropDownListInspecMan.DataTextField = "AFI_MANNM";
            DropDownListInspecMan.DataValueField = "AFI_MANNM";
            DropDownListInspecMan.DataBind();
            DropDownListInspecMan.Items.Insert(0,new ListItem("-请选择-","-请选择-"));
            DropDownListInspecMan.SelectedIndex=0;
        }
        protected void DropDownListDep_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindDropDownListInspecMan();
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
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }

      //  CheckUser(ControlFinder);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> AFI_ID = new List<string>();
            string strID = "";
            string sql = "";
            foreach (GridViewRow labID in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)labID.FindControl("CKBOX_SELECT");
                if(chk!=null)
                {
                    if (chk.Checked)
                    {
                        //查找该CheckBox所对应纪录的质检ID
                        strID = ((Label)labID.FindControl("lbafiid")).Text;

                        AFI_ID.Add(strID);
                    }
                }
            }
            if (AFI_ID.Count > 0)
            {
                foreach (string id in AFI_ID)
                {
                    string uniqueid = "";
                    sql = "select UNIQUEID FROM TBQM_APLYFORINSPCT WHERE AFI_ID = '" + id + "'";
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows.Count > 0)
                    {
                        uniqueid = dt.Rows[0][0].ToString();
                    }
                    sql = "DELETE FROM TBQM_APLYFORITEM WHERE UNIQUEID='" + uniqueid + "'";
                    DBCallCommon.ExeSqlText(sql);

                    
                    sql= "DELETE FROM TBQM_APLYFORINSPCT WHERE AFI_ID = '" + id + "'";

                    DBCallCommon.ExeSqlText(sql);
                }

                Response.Redirect("QC_Inspection_Manage.aspx");
            }
            else
            {
                string script = @"alert('未选择删除项!');";

                this.Page.ClientScript.RegisterStartupScript(this.GetType(), "error", script, true);
            }
        }


        /// <summary>
        /// 判断权限---修改，再次质检，删除，质检
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void GridView1_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridView1.Rows)
            {
                Label lb = (Label)gr.FindControl("lbafiid");
                //Label lbsj = (Label)gr.FindControl("lbafisj");
                Label lbid = (Label)gr.FindControl("lbzjid");
                //string sj = lbsj.Text.Substring(0, 4);
                string id = lb.Text.ToString();
                lbid.Text = id.PadLeft(5, '0');

                string sql = "select AFI_STATE,AFI_NUMBER,AFI_MAN,AFI_QCMAN,UNIQUEID from TBQM_APLYFORINSPCT where AFI_ID='" + lb.Text.Trim() + "'";

                DataSet ds = DBCallCommon.FillDataSet(sql);
                string uniqueid = ds.Tables[0].Rows[0]["UNIQUEID"].ToString();

                if (ds.Tables[0].Rows.Count > 0)
                {
                    /***************************对于报检的权限*****************************************/
                    //表示已质检
                    if (ds.Tables[0].Rows[0]["AFI_STATE"].ToString() == "1")
                    {
                        //质检状态为已质检
                        HyperLink hl1 = (HyperLink)gr.FindControl("hlupdate1");
                        hl1.Parent.Visible = true;//可以重新报检
                        HyperLink hl = (HyperLink)gr.FindControl("hlupdate");
                        hl.Parent.Visible = false;//修改隐藏

                    }
                    else
                    {
                        //未质检
                        HyperLink hl1 = (HyperLink)gr.FindControl("hlupdate");
                        hl1.Parent.Visible = true;//修改可见
                        HyperLink hl = (HyperLink)gr.FindControl("hlupdate1");
                        hl.Parent.Visible = false;//重新报检不可见
                    }
                    /***************************对于质检的权限*****************************************/
                    if (ds.Tables[0].Rows[0]["AFI_STATE"].ToString() == "0" && ds.Tables[0].Rows[0]["AFI_NUMBER"].ToString() != "1")
                    {
                        //质检状态为未质检，报检状态为不是第一次报检
                        HyperLink hl1 = (HyperLink)gr.FindControl("hykzhijian2");
                        hl1.Parent.Visible = true;//可以重新质检
                        HyperLink hl = (HyperLink)gr.FindControl("hykzhijian1");
                        hl.Parent.Visible = false;//报检隐藏
                        HyperLink hlupdate = (HyperLink)gr.FindControl("hykupdate");
                        hlupdate.Parent.Visible = false;//修改报检隐藏
                    }
                    else if (ds.Tables[0].Rows[0]["AFI_STATE"].ToString() == "1")
                    {
                        //质检状态为已质检，报检状态为多次报检或者第一次报检
                        HyperLink hl1 = (HyperLink)gr.FindControl("hykzhijian2");
                        hl1.Parent.Visible = false;//重新质检隐藏
                        HyperLink hl = (HyperLink)gr.FindControl("hykzhijian1");
                        hl.Parent.Visible = false;//质检隐藏
                        HyperLink hlupdate = (HyperLink)gr.FindControl("hykupdate");
                        hlupdate.Parent.Visible = true;//可以修改报检

                    }
                    else if (ds.Tables[0].Rows[0]["AFI_STATE"].ToString() == "0" && ds.Tables[0].Rows[0]["AFI_NUMBER"].ToString() == "1")
                    {
                        //质检状态为未质检，是第一次质检
                        HyperLink hl1 = (HyperLink)gr.FindControl("hykzhijian2");
                        hl1.Parent.Visible = false;//重新质检隐藏
                        HyperLink hl = (HyperLink)gr.FindControl("hykzhijian1");
                        hl.Parent.Visible = true;//可以质检
                        HyperLink hlupdate = (HyperLink)gr.FindControl("hykupdate");
                        hlupdate.Parent.Visible = false;//修改报检隐藏
                    }
                    /*************************如果不是报检人**********************************/
                    if (ds.Tables[0].Rows[0]["AFI_MAN"].ToString() != Session["UserID"].ToString())
                    {
                        //不是自己报检
                        HyperLink hl = (HyperLink)gr.FindControl("hlupdate");//修改
                        HyperLink hl1 = (HyperLink)gr.FindControl("hlupdate1");//重新报检
                        CheckBox cb = (CheckBox)gr.FindControl("CKBOX_SELECT");//删除
                        cb.Enabled = false;
                        hl.Enabled = false;
                        hl1.Enabled = false;
                    }
                    else
                    {
                        //是自己报检
                        /*
                         *当已质检，不能删除
                         * 多次报检不能删除
                         * * *********************/
                        if (ds.Tables[0].Rows[0]["AFI_STATE"].ToString() == "1" || ds.Tables[0].Rows[0]["AFI_NUMBER"].ToString() != "1")
                        {
                            //已质检、或是重新报检的不可以删除
                            CheckBox cb = (CheckBox)gr.FindControl("CKBOX_SELECT");//删除
                            cb.Enabled = false;
                        }
                    }
                }
                string aa = Session["UserDept"].ToString();
                if (Session["UserDept"].ToString() != "质量部")
                {
                    //不是质量部的不可质检

                    HyperLink hl = (HyperLink)gr.FindControl("hykzhijian1");
                    hl.Enabled = false;
                    HyperLink hl2 = (HyperLink)gr.FindControl("hykzhijian2");
                    hl2.Enabled = false;
                    HyperLink hl3 = (HyperLink)gr.FindControl("hykupdate");
                    hl3.Enabled = false;
                }


                HyperLink hl5 = (HyperLink)gr.FindControl("hykupdate");
                HyperLink hl4 = (HyperLink)gr.FindControl("hykqqsh");
                HiddenField issh = (HiddenField)gr.FindControl("issh");
                HiddenField shjg = (HiddenField)gr.FindControl("shjg");
                Label lb1 = (Label)gr.FindControl("lb1");
                if (hl5.Enabled == true && hl5.Visible == true)
                {
                    string sql1 = "select distinct PTC from TBQM_APLYFORITEM where UNIQUEID='" + uniqueid + "'";
                    DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
                    if (dt1.Rows[0][0].ToString() != "")
                    {
                        string sql2 = "select  * from TBWS_INDETAIL where WG_PTCODE in  (select distinct PTC from TBQM_APLYFORITEM where UNIQUEID='" + uniqueid + "') ";
                        DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sql2);
                        if (dt2.Rows.Count > 0)
                        {
                            if (issh.Value == "0")
                            {
                                hl5.Enabled = false;
                                hl4.Enabled = true;

                            }

                            if (issh.Value == "1")
                            {
                                hl5.Enabled = false;
                                hl4.Enabled = false;
                                lb1.Text = "审核中";

                            }

                            if (issh.Value == "2" && shjg.Value == "0")
                            {
                                hl5.Enabled = true;
                                hl4.Enabled = false;

                            }

                            if (issh.Value == "2" && shjg.Value == "1")
                            {
                                hl5.Enabled = false;
                                hl4.Enabled = true;

                            }
                        }
                    }
                }

            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#e4ecf7'");//当鼠标停留时更改背景色
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");//当鼠标移开时还原背景色

                (e.Row.FindControl("HyperLink3") as HyperLink).Attributes.Add("onClick", "ShowViewModal('" + GridView1.DataKeys[e.Row.RowIndex]["AFI_ID"].ToString() + "');");
            }
        }

        /// <summary>
        /// 质检状态的查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rblstatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (((RadioButtonList)sender).ID == "rblstatus")
            {
                if (rblstatus.SelectedValue == "1")
                {
                    RadioButtonListResult.Visible = true;
                }
                else
                {
                    RadioButtonListResult.ClearSelection();
                    RadioButtonListResult.Visible = false;
                }
            }
            else if (((RadioButtonList)sender).ID == "RadioButtonListResult")
            {}
            else
            {
                RadioButtonListResult.ClearSelection();
                RadioButtonListResult.Visible = false;
            }
            
            UCPaging1.CurrentPage = 1;
            pager.StrWhere = getStrWhere();
            bindGrid();
            
        }


        protected void QueryButton_Click(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;

            bindGrid();
        }


        private string getStrWhere()
        {
            string condition = "";

            if (RadioButtonListTask.SelectedValue != "all")
            {
                string status = rblstatus.SelectedValue.ToString();

                if (Session["UserDept"].ToString() != "质量部")
                {
                    if (status == "1")
                    {
                        condition = "AFI_STATE!='0' AND AFI_MAN='" + Session["UserID"].ToString() + "'"; //全部质检，限制自己
                    }
                    else
                    {
                        condition = "AFI_STATE='0'  AND AFI_MAN='" + Session["UserID"].ToString() + "'";//全部未质检，限制自己
                    }
                }
                else
                {
                    if (status == "1")
                    {
                        condition = "AFI_STATE!='0' AND AFI_QCMAN='" + Session["UserID"].ToString() + "'"; //全部质检，限制自己
                    }
                    else
                    {
                        condition = "AFI_STATE='0'  AND AFI_QCMAN='" + Session["UserID"].ToString() + "'";//全部未质检，限制自己
                    }
                }
            }
            else
            {
                string status = rblstatus.SelectedValue.ToString();

                if (status == "1")
                {
                    condition = "AFI_STATE!='0'"; //全部质检，没有限制
                }
                else
                {
                    condition = "AFI_STATE='0'";//全部未质检，没有限制
                }
            }

            //项目名称
            if ((TextBoxProj.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND AFI_PJNAME LIKE '%" + TextBoxProj.Text.Trim() + "%'";
            }
            //生产制号
            if ((TextBoxENGID.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND AFI_ENGID LIKE '%" + TextBoxENGID.Text.Trim() + "%'";
            }
            else if ((TextBoxENGID.Text.Trim() != "") && (condition == ""))
            {
                condition += " AFI_ENGID LIKE '%" + TextBoxENGID.Text.Trim() + "%'";
            }

            //质检编号
            if ((TextBoxZJID.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND AFI_ID LIKE '%" + Convert.ToInt32(TextBoxZJID.Text.Trim()) + "%'";
            }
            else if ((TextBoxZJID.Text.Trim() != "") && (condition == ""))
            {
                condition += " AFI_ID LIKE '%" + Convert.ToInt32(TextBoxZJID.Text.Trim()) + "%'";
            }

            //报检人
            if ((DropDownListInspecMan.SelectedIndex == (-1)) && (condition != ""))
            {

            }
            else if ((DropDownListInspecMan.SelectedIndex != 0) && (condition != ""))
            {
                condition += " AND " + " AFI_MANNM='" + DropDownListInspecMan.SelectedValue.ToString().Trim() + "'";
            }
            else if ((DropDownListInspecMan.SelectedIndex != 0) && (condition == ""))
            {
                condition += " AFI_MANNM='" + DropDownListInspecMan.SelectedValue.ToString().Trim() + "'";
            }

            //质检人
            if ((TextBoxMan.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " AFI_QCMANNM LIKE'%" + TextBoxMan.Text.Trim() + "%'";
            }
            else if ((TextBoxMan.Text.Trim() != "") && (condition == ""))
            {
                condition += " AFI_QCMANNM LIKE '%" + TextBoxMan.Text.Trim() + "%'";
            }


            //报检日期
            if ((TextBoxDate.Text.Trim() != "") && (condition != ""))
            {
                condition += " AND " + " AFI_DATE LIKE '%" + TextBoxDate.Text.Trim() + "%'";
            }
            else if ((TextBoxDate.Text != "") && (condition == ""))
            {
                condition += " AFI_DATE LIKE '%" + TextBoxDate.Text.Trim() + "%'";
            }

            //部件名称
            if ((TextBoxBJMC.Text.Trim() != "") && (condition != ""))
            {
                condition += "and" + " AFI_PARTNAME LIKE '%" + TextBoxBJMC.Text.Trim() + "%'";

            }
            else if ((TextBoxBJMC.Text.Trim() != "") && (condition == ""))
            {
                condition += " AFI_PARTNAME LIKE '%" + TextBoxBJMC.Text.Trim() + "%'";

            }
            //工程名称
            if ((TextBoxEng.Text.Trim() != "") && (condition != ""))
            {
                condition += "and" + " AFI_ENGNAME LIKE '%" + TextBoxEng.Text.Trim() + "%'";

            }
            else if ((TextBoxEng.Text.Trim() != "") && (condition == ""))
            {
                condition += " AFI_ENGNAME LIKE '%" + TextBoxEng.Text.Trim() + "%'";

            }
            //供货商
            if ((TextBoxSUPPLERNM.Text.Trim() != "") && (condition != ""))
            {
                condition += "and" + " AFI_SUPPLERNM LIKE '%" + TextBoxSUPPLERNM.Text.Trim() + "%'";

            }
            else if ((TextBoxSUPPLERNM.Text.Trim() != "") && (condition == ""))
            {
                condition += " AFI_SUPPLERNM LIKE '%" + TextBoxSUPPLERNM.Text.Trim() + "%'";

            }

            //报检部门
            if (DropDownListDep.SelectedValue != "")
            {
                if (condition != "")
                {
                    condition += " AND " + " AFI_TSDEP='" + DropDownListDep.SelectedItem.Text + "'";
                }
                else
                {
                    condition += " AFI_TSDEP='" + DropDownListDep.SelectedItem.Text + "'";
                }
            }
            //质检结果
            if (RadioButtonListResult.SelectedIndex!=-1)
            {
                if (RadioButtonListResult.SelectedValue != "all")
                {
                   condition += " AND AFI_ENDRESLUT='" + RadioButtonListResult.SelectedValue+ "'";
                }
            }

            return condition;
        }


        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearCondition();
        }

        private void clearCondition()
        {
           
            TextBoxBJMC.Text = "";
            TextBoxProj.Text = "";
            TextBoxEng.Text = "";
            DropDownListInspecMan.SelectedIndex = 0;
            TextBoxMan.Text = "";
            TextBoxDate.Text = "";
            foreach (ListItem lt in DropDownListDep.Items)
            {
                lt.Selected = false;
                if (lt.Value == "")
                {
                    lt.Selected = true;
                }
            }

        }

    }
}
