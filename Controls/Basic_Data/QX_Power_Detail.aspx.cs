using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class QX_Power_Detail : System.Web.UI.Page
    {
        DbAccess dba = new DbAccess(); 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["bIsNumChanged"] = false;
                //GetChildPage("1", "2,", 1);
                GetCategoryData();
            }

            if (Request.QueryString["Action"] == "Add")
            {
                if (TBControlNum.Text != "")
                {
                    CreateControl(Convert.ToInt32(TBControlNum.Text));
                }
            }
            else if (Request.QueryString["Action"] == "Update")
            {
                if ((bool)ViewState["bIsNumChanged"] == false)
                    CreateUpdate();
                else
                    CreateControl(Convert.ToInt32(TBControlNum.Text));
            }
        }

        private void GetCategoryData()
        {
            
            DataTable dt = dba.GetSqlDataTable("Select * from power_page Where fatherID='0'");            
            DDLCategory.Items.Add(new ListItem("--基础大类--", "0,"));
            foreach (DataRow dr in dt.Rows)
            {
                DDLCategory.Items.Add(new ListItem(dr["name"].ToString(), dr["page_id"].ToString() + ","));
                GetChildPage(dr["page_id"].ToString(), dr["page_id"].ToString() + ",", 1);
            }
        }

        protected void GetChildPage(string strID, string strIDPath, int nDepth)
        {
            string sqlText = "Select * from power_page Where fatherID='" + strID + "'";
          
            //SqlDataReader dr = dba.getDRExSQL(sqlText) ;
            SqlConnection sqlConn = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = sqlText;
            SqlDataReader dr = DBCallCommon.GetDataReader(sqlConn, sqlCmd);
            //dr.IsClosed
            if (dr.Read())
            {
                do
                {
                    DDLCategory.Items.Add(new ListItem(GetLeft("--", nDepth) + dr["Name"].ToString(), strIDPath + dr["page_id"].ToString() + ","));
                    GetChildPage(dr["page_id"].ToString(), strIDPath + "," + dr["page_id"].ToString() + ",", nDepth + 1);
                } while (dr.Read());
            }
            else
            {
                return;
            }
            dr.Close();
            sqlConn.Close();
        }
        protected string GetLeft(string strLeftUnit, int nDepth)
        {
            string strOut = "├";
            for (int i = 0; i < nDepth; i++)
            {
                strOut += strLeftUnit;
            }

            return strOut;
        }
       
        protected void TBControlNum_TextChanged(object sender, EventArgs e)
        {
            TCtrlID.Controls.Clear();
            TCtrlName.Controls.Clear();

            int nNum = Convert.ToInt32(TBControlNum.Text);

            CreateControl(nNum);

            ViewState["bIsNumChanged"] = true;
        }

        protected void CreateControl(int nNum)
        {
            for (int i = 0; i < nNum; i++)
            {
                TableRow trCtrlID = new TableRow();
                trCtrlID.ID = "TRCTRLID_" + i.ToString();
                TableCell tcCtrlID = new TableCell();
                TextBox tbCtrlID = new TextBox();
                tbCtrlID.ID = "CtrlID_" + i.ToString();
                tcCtrlID.Controls.Add(tbCtrlID);

                Label lbID = new Label();
                lbID.Text = "New";
                lbID.ID = "LBID_" + i.ToString();
                lbID.Visible = false;
                tcCtrlID.Controls.Add(lbID);

                Label lbNameingContainer = new Label();
                lbNameingContainer.Text = " | 命名空间(<span style='color:#ff0000;'>用'/'分隔</span>)：";
                tcCtrlID.Controls.Add(lbNameingContainer);

                TextBox tbNameingContainer = new TextBox();
                tbNameingContainer.ID = "CtrlNameingContainer_" + i.ToString();
                tcCtrlID.Controls.Add(tbNameingContainer);

                Label lbA = new Label();
                lbA.Text = " | 跳转页面：";
                tcCtrlID.Controls.Add(lbA);

                TextBox tbA = new TextBox();
                tbA.ID = "CtrlUrl_" + i.ToString();
                tcCtrlID.Controls.Add(tbA);


                Button btnDel = new Button();
                btnDel.ID = "CtrlDel_" + i.ToString();
                btnDel.CssClass = "del_new_img";
                btnDel.Attributes.Add("onClick", "Javascript:return confirm('你确认删除吗?');");
                btnDel.Click += new EventHandler(BtrDel_OnClick);
                tcCtrlID.Controls.Add(btnDel);
                trCtrlID.Cells.Add(tcCtrlID);
                TCtrlID.Rows.Add(trCtrlID);

                TableRow trCtrlName = new TableRow();
                trCtrlName.ID = "TRCTRLName_" + i.ToString();
                TableCell tcCtrlName = new TableCell();
                TextBox tbCtrlName = new TextBox();
                tbCtrlName.ID = "CtrlName_" + i.ToString();
                tcCtrlName.Controls.Add(tbCtrlName);
                trCtrlName.Cells.Add(tcCtrlName);
                TCtrlName.Rows.Add(trCtrlName);
            }
        }

       
        protected void Confirm_Click(object sender, EventArgs e)
        {
            string strID = "";
            string strCtrl = "";

            if (Request.QueryString["Action"] == "Add")
            {
                //保存页面到power_page
                string fatherID = "";
                fatherID = DDLCategory.SelectedItem.Value.Split(',')[DDLCategory.SelectedItem.Value.Split(',').Length - 2];

                string sqlText = "Insert Into power_page(page, controlNum, fatherID, name, IDPath, pageLevel, create_date, creator) Values(";
                sqlText += "'" + TBPage.Text + "',";
                sqlText += "'" + TBControlNum.Text + "',";
                sqlText += "'" + fatherID + "',";
                sqlText += "'" + TBPageName.Text + "',";
                sqlText += "'" + (DDLCategory.SelectedItem.Value == "0," ? "" : DDLCategory.SelectedItem.Value) + "',";
                sqlText += "'" + (DDLCategory.SelectedItem.Value == "0," ? "" : DDLCategory.SelectedItem.Value).Split(',').Length + "',";
                sqlText += "'" + DateTime.Now.ToString() + "',";
                sqlText += "'" + Session["UserName"] + "') select @@Identity as idnty";//select @@Identity"标记到当前行的序号

               
                SqlDataReader dr = dba.getDRExSQL(sqlText);

                if (dr.Read())
                {
                    strID = dr["idnty"].ToString();
                }
                dr.Close();

                //更新当前行的IDPatch，添加当前的PageID主键
                sqlText = "Update power_page set IDPath=IDPath+'" + strID + ",' Where page_id='" + strID + "'";

                dr = dba.getDRExSQL(sqlText);
                dr.Close();

                //保存控件信息到page_control
                for (int i = 0; i < Convert.ToInt32(TBControlNum.Text); i++)
                {
                    TextBox tbID = (TextBox)TCtrlID.FindControl("CtrlID_" + i.ToString());
                    TextBox tbName = (TextBox)TCtrlName.FindControl("CtrlName_" + i.ToString());
                    TextBox tbUrl = (TextBox)TCtrlID.FindControl("CtrlUrl_" + i.ToString());
                    //CheckBox cb = (CheckBox)TCtrlID.FindControl("CtrlIsRepeater_" + i.ToString());
                    TextBox tbNameing = (TextBox)TCtrlID.FindControl("CtrlNameingContainer_" + i.ToString());

                    strCtrl += "Insert Into page_control(page_id, control, gotoPage, name, namingContainer, create_date, creator) Values(";
                    strCtrl += "'" + strID + "',";
                    strCtrl += "'" + tbID.Text + "',";
                    strCtrl += "'" + tbUrl.Text + "',";
                    strCtrl += "'" + tbName.Text + "',";
                    strCtrl += "'" + tbNameing.Text + "',";
                    strCtrl += "'" + DateTime.Now.ToString() + "',";
                    strCtrl += "'" + Session["UserName"] + "');";
                }

                dr = dba.getDRExSQL(strCtrl);
                dr.Close();
            }
            else if (Request.QueryString["Action"] == "Update")
            {
                string fatherID = "";
                fatherID = DDLCategory.SelectedItem.Value.Split(',')[DDLCategory.SelectedItem.Value.Split(',').Length - 2];
                string strPageID = Request.QueryString["page_id"];

                string sqlText = "Update power_page Set ";
                sqlText += "page='" + TBPage.Text + "', ";
                sqlText += "fatherID='" + fatherID + "', ";
                sqlText += "controlNum='" + TBControlNum.Text + "', ";
                sqlText += "name='" + TBPageName.Text + "', ";
                sqlText += "IDPath='" + (DDLCategory.SelectedItem.Value == "0," ? "" : DDLCategory.SelectedItem.Value) + Request.QueryString["page_id"].ToString() + "," + "', ";
                sqlText += "pageLevel='" + (DDLCategory.SelectedItem.Value == "0," ? "" : DDLCategory.SelectedItem.Value).Split(',').Length + "' ";
                sqlText += "Where page_id='" + strPageID + "';";
                Label lb = new Label();
                int i =0;
                while((lb = (Label)TCtrlID.FindControl("LBID_" + i.ToString())) != null)
                {
                    TextBox tbCtrlID = (TextBox)TCtrlID.FindControl("CtrlID_" + i.ToString());                    
                    TextBox tbCtrlUrl = (TextBox)TCtrlID.FindControl("CtrlUrl_" + i.ToString());
                    TextBox tbNameing = (TextBox)TCtrlID.FindControl("CtrlNameingContainer_" +i.ToString());
                    TextBox tbName = (TextBox)TCtrlName.FindControl("CtrlName_" + i.ToString());
                    if (lb.Text != "New")
                    {
                        sqlText += "Update page_control Set ";
                        sqlText += "control='" + tbCtrlID.Text + "', ";
                        sqlText += "namingContainer='" + tbNameing.Text + "', ";
                        sqlText += "name='" + tbName.Text + "', ";
                        sqlText += "gotoPage='" + tbCtrlUrl.Text + "' ";
                        sqlText += "Where con_id='"+Convert.ToInt32(lb.Text).ToString()+"';";
                    }
                    else
                    {
                        if (tbCtrlID.Text != "")
                        {
                            sqlText += "Insert Into page_control(page_id, control, gotoPage, name, namingContainer, create_date, creator) Values(";
                            sqlText += "'" + strPageID + "',";
                            sqlText += "'" + tbCtrlID.Text + "',";
                            sqlText += "'" + tbCtrlUrl.Text + "',";
                            sqlText += "'" + tbName.Text + "',";
                            sqlText += "'" + tbNameing.Text + "',";
                            sqlText += "'" + DateTime.Now.ToString() + "',";
                            sqlText += "'" + Session["UserName"] + "');";
                        }
                        else
                        {
                            Response.Write("<script>alert('请输入控件ID！！！')</script>");
                            return;
                        }
                    }

                    i++;
                }              
                SqlDataReader dr = dba.getDRExSQL(sqlText);
                dr.Close();
            }
            Response.Write("<script>javascript:window.close();</script>");
        }


        protected void BtrDel_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string strIDNO = btn.ID.Split('_')[1];

            Label lb = (Label)TCtrlID.FindControl("LBID_" + strIDNO);
            TableRow trID = (TableRow)TCtrlID.FindControl("TRCTRLID_" + strIDNO);
            TableRow trName = (TableRow)TCtrlID.FindControl("TRCTRLName_" + strIDNO);
                //如果是新添加的则直接隐藏掉那一行
                if (lb.Text == "New")
                {
                    TBControlNum.Text = (Convert.ToInt32(TBControlNum.Text) - 1).ToString();
                    //移除TR
                    TCtrlID.Rows.Remove(trID);
                    TCtrlName.Rows.Remove(trName);
                }
                //如果是以前的则：
                else
                {
                    //删除CONTROL中的记录            
                    string sqlText = "Delete From page_control Where con_id='" + lb.Text + "';";

                    //删除Power_ROLE中的记录
                    sqlText += "Delete From power_role Where control_id='" + lb.Text + "';";

                    //如果这个CONTROL对应于一条PAGE，则删除相应的PAGE记录
                    //sqlText += "Delete From Power_Action_Page Where Page=(Select RelatedPage From Power_Action_Control Where ID='" + lb.Text + "');";

                    dba.exSQLv(sqlText);
                    TBControlNum.Text = (Convert.ToInt32(TBControlNum.Text) - 1).ToString();

                    //移除TR
                    TCtrlID.Rows.Remove(trID);
                    TCtrlName.Rows.Remove(trName);
                }
                string sql = "update power_page set controlnum=" + TBControlNum.Text + " where page_id=" + Request.QueryString["page_id"].ToString() + "";
                dba.exSQLv(sql);
                Response.Write("<script>javascript:window.location.href=window.location.href;</script>");
        }
        

        #region Update部分

        protected void BtnCtrlAdd_Click(object sender, EventArgs e)
        {
            CreateNewUpdateControl(Convert.ToInt32(TBControlNum.Text));

            TBControlNum.Text = (Convert.ToInt32(TBControlNum.Text) + 1).ToString();
        }

        protected void CreateNewUpdateControl(int nID)//nID标识定nID+1个控件
        {
            TableRow trCtrlID = new TableRow();
            trCtrlID.ID = "TRCTRLID_" + nID.ToString();
            TableRow trCtrlName = new TableRow();
            trCtrlName.ID = "TRCTRLName_" + nID.ToString();

            TableCell tcCtrlID = new TableCell();
            TextBox tbCtrlID = new TextBox();
            tbCtrlID.ID = "CtrlID_" + nID.ToString();
            tcCtrlID.Controls.Add(tbCtrlID);

            Label lbID = new Label();
            lbID.Text = "New";
            lbID.ID = "LBID_" + nID.ToString();
            lbID.Visible = false;
            tcCtrlID.Controls.Add(lbID);

            Label lbNameingContainer = new Label();
            lbNameingContainer.Text = " | 命名空间(<span style='color:#ff0000;'>用'/'分隔</span>)：";
            tcCtrlID.Controls.Add(lbNameingContainer);

            TextBox tbNameingContainer = new TextBox();
            tbNameingContainer.ID = "CtrlNameingContainer_" + nID.ToString();
            tcCtrlID.Controls.Add(tbNameingContainer);

            Label lbA = new Label();
            lbA.Text = " | 跳转页面：";
            tcCtrlID.Controls.Add(lbA);

            TextBox tbA = new TextBox();
            tbA.ID = "CtrlUrl_" + nID.ToString();
            tcCtrlID.Controls.Add(tbA);

            Button btnDel = new Button();
            btnDel.ID = "CtrlDel_" + nID.ToString();
            btnDel.CssClass = "del_new_img";
            btnDel.Attributes.Add("onClick", "Javascript:return confirm('你确认删除吗?');");
            btnDel.Click += new EventHandler(BtrDel_OnClick);
            tcCtrlID.Controls.Add(btnDel);
            trCtrlID.Cells.Add(tcCtrlID);
            TCtrlID.Rows.Add(trCtrlID);

            TableCell tcCtrlName = new TableCell();
            TextBox tbCtrlName = new TextBox();
            tbCtrlName.ID = "CtrlName_" + nID.ToString();

            tcCtrlName.Controls.Add(tbCtrlName);
            trCtrlName.Cells.Add(tcCtrlName);
            TCtrlName.Rows.Add(trCtrlName);
        }

        protected void CreateUpdate()
        {
            if (!IsPostBack)//第一次调用本页面执行的动作
            {
                TBControlNum.Enabled = false;

                DataTable dt =dba.GetSqlDataTable("Select * From power_page Where page_id='" + Request.QueryString["page_id"] + "'");

                if (dt.Rows.Count > 0)
                {
                    TBPage.Text = dt.Rows[0]["page"].ToString();
                    TBPageName.Text = dt.Rows[0]["name"].ToString();
                    TBControlNum.Text = dt.Rows[0]["controlNum"].ToString();
                    DDLCategory.SelectedValue = dt.Rows[0]["IDPath"].ToString().Replace(Request.QueryString["page_id"] + ",", "");
                    CreateUpdateControl(Convert.ToInt32(dt.Rows[0]["controlNum"].ToString()));
                }
            }
            else
            {
                CreateUpdateControl(Convert.ToInt32(TBControlNum.Text));
            }
        }

        protected void CreateUpdateControl(int nNum)
        {
            DataTable dt =dba.GetSqlDataTable("Select * From page_control Where page_id='" + Request.QueryString["page_id"] + "'");

            int i = 0;
            foreach (DataRow dr in dt.Rows)
            {
                TableRow trCtrlID = new TableRow();
                trCtrlID.ID = "TRCTRLID_" + i.ToString();
                TableCell tcCtrlID = new TableCell();
                TextBox tbCtrlID = new TextBox();
                tbCtrlID.ID = "CtrlID_" + i.ToString();
                tcCtrlID.Controls.Add(tbCtrlID);

                Label lbID = new Label();
                lbID.Text = dr["con_id"].ToString();
                lbID.ID = "LBID_" + i.ToString();
                lbID.Visible = false;
                tcCtrlID.Controls.Add(lbID);

                Label lbNameingContainer = new Label();
                lbNameingContainer.Text = " | 命名空间(<span style='color:#ff0000;'>用'/'分隔</span>)：";
                tcCtrlID.Controls.Add(lbNameingContainer);

                TextBox tbNameingContainer = new TextBox();
                tbNameingContainer.ID = "CtrlNameingContainer_" + i.ToString();
                tcCtrlID.Controls.Add(tbNameingContainer);

                Label lbA = new Label();
                lbA.Text = " | 跳转页面：";
                tcCtrlID.Controls.Add(lbA);

                TextBox tbA = new TextBox();
                tbA.ID = "CtrlUrl_" + i.ToString();
                tcCtrlID.Controls.Add(tbA);

                Button btnDel = new Button();
                btnDel.ID = "CtrlDel_" + i.ToString();
                btnDel.CssClass = "del_new_img";
                btnDel.Attributes.Add("onClick", "Javascript:return confirm('你确认删除吗?');");
                btnDel.Click += new EventHandler(BtrDel_OnClick);
                tcCtrlID.Controls.Add(btnDel);
                trCtrlID.Cells.Add(tcCtrlID);
                TCtrlID.Rows.Add(trCtrlID);

                TableRow trCtrlName = new TableRow();
                trCtrlName.ID = "TRCTRLName_" + i.ToString();
                TableCell tcCtrlName = new TableCell();
                TextBox tbCtrlName = new TextBox();
                tbCtrlName.ID = "CtrlName_" + i.ToString();


                if (!IsPostBack)
                {
                    tbNameingContainer.Text = dr["namingContainer"].ToString();
                    tbA.Text = dr["gotoPage"].ToString();
                    tbCtrlName.Text = dr["name"].ToString();
                    tbCtrlID.Text = dr["Control"].ToString();
                }

                tcCtrlName.Controls.Add(tbCtrlName);
                trCtrlName.Cells.Add(tcCtrlName);
                TCtrlName.Rows.Add(trCtrlName);

                i++;
            }


            for (int j = i; j < nNum; j++)
            {
                CreateNewUpdateControl(j);
            }
        }
       
       

       
        #endregion

  }

    
}
