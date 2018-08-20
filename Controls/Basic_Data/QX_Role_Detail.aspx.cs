using System;
using System.Collections;
using System.Collections.Generic;
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
using System.Data.SqlClient;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class Role_Detail : System.Web.UI.Page
    {
        private string _Action;
        protected DbAccess dba = new DbAccess();
        protected DataSet _UpdateDataPage;
        protected DataSet _UpdateDataControl;

        protected void Page_Load(object sender, EventArgs e)
        {
            _Action = Request.QueryString["Action"];
            if (_Action == "Update" || _Action == "view")
            {
                if (!IsPostBack)
                {
                    if (_Action == "view")
                    {
                        Confirm.Visible = false;
                        Reset1.Visible = false;
                    }
                    InitUpdateData();
                    TreeNode tn = ConfigPower.Nodes[0];//获得到根节点--权限配置
                    tn.PopulateOnDemand = false;       //如果是更新角色权限配置，则不可添加新节点 
                    //Response.Write(tn.Text + "123" + tn.Value + tn.PopulateOnDemand);
                    //获得所有的页面
                    DataSet pageDataSet = dba.GetSqlDataSet("select * from power_page", "power_page");
                    InitTree(pageDataSet, tn.ChildNodes, 0);
                }
            }
            //点击树的checkbox后，添加回发函数，从而触发TreeNodeCheckChanged事件
            //ConfigPower.Attributes.Add("onclick", "postBackByObject()");
        }
        protected void InitUpdateData()
        {
            string sqlText = "Select r_name From role_info Where r_id='" + Request.QueryString["r_id"] + "'";
            //SqlConnection sqlConn = new SqlConnection();
            //SqlCommand sqlCmd = new SqlCommand();
            //sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            //sqlCmd.Connection = sqlConn;
            //sqlCmd.CommandText = sqlText;
            //SqlDataReader dr = DBCallCommon.GetDataReader(sqlConn, sqlCmd);

            SqlDataReader dr = dba.getDRExSQL(sqlText);


            if (dr.Read())
            {
                GroupName.Text = dr["r_name"].ToString();
                GroupName.Enabled = false;
            }
            dr.Close();

            _UpdateDataPage = dba.GetSqlDataSet(
                "Select Distinct(page_id) From power_role Where r_id='" + Request.QueryString["r_id"] + "'",
                "Data_Page"
                );//本角色对应的权限页面

            _UpdateDataControl = dba.GetSqlDataSet(
                "Select control_id From power_role Where r_id='" + Request.QueryString["r_id"] + "'",
                "Data_Control"
                );//本角色对应的权限页面控件
        }

        //递归函数
        protected void InitTree(DataSet objDataSet, TreeNodeCollection Nds, int nParentID)//nParentID父节点ID
        {
            DataView dv = new DataView();
            TreeNode tmpNd;
            //int strId;
            dv.Table = objDataSet.Tables["power_page"];
            dv.RowFilter = "fatherID='" + nParentID + "'";//fatherID=0的行

            foreach (DataRowView objRow in dv)
            {
                tmpNd = new TreeNode();
                tmpNd.Text = objRow["name"].ToString();
                tmpNd.Value = objRow["page_id"].ToString() + "_Page";
                tmpNd.ShowCheckBox = true;
                //tmpNd.DataItem.

                if (CheckUpdatePage(objRow["page_id"].ToString()))
                    tmpNd.Checked = true;
                Nds.Add(tmpNd);
               
                //获取控件的名称及ID
                GetUpdateControlData(tmpNd, objRow["page_id"].ToString());

                InitTree(objDataSet, Nds[Nds.Count - 1].ChildNodes, (int)objRow["page_id"]);
            }

        }

        protected bool CheckUpdatePage(string page_id)
        {
            DataView dv = new DataView();
            dv.Table = _UpdateDataPage.Tables["Data_Page"];
            dv.RowFilter = "page_id='" + page_id + "'";

            if (dv.Count > 0)
                return true;
            else
                return false;
        }

        protected bool CheckUpdateControl(string control)
        {
            DataView dv = new DataView();
            dv.Table = _UpdateDataControl.Tables["Data_Control"];
            dv.RowFilter = "control_id='" + control + "'";

            if (dv.Count > 0)
                return true;
            else
                return false;
        }

        protected void GetUpdateControlData(TreeNode tn, string strID)
        {
            DataTable dt = dba.GetSqlDataTable("Select * From page_control Where page_id='" + strID + "' and  (gotoPage='' or gotoPage not in (Select page From power_page Where fatherID='" + strID + "'))");

            foreach (DataRow dr in dt.Rows)
            {
                TreeNode tmp = new TreeNode(dr["name"].ToString(), dr["con_id"].ToString() + "_Control");
                tmp.ShowCheckBox = true;

                if (CheckUpdateControl(dr["con_id"].ToString()))
                    tmp.Checked = true;

                tn.ChildNodes.Add(tmp);
            }
        }

        #region ConfigPower_TreeNodePopulate 事件触发

        //当其 PopulateOnDemand 属性设置为 true 的节点在 TreeView 控件中展开时发生。
        protected void ConfigPower_TreeNodePopulate(object sender, TreeNodeEventArgs e)
        {
            GetControlData(e.Node);
            GetPageData(e.Node);
        }

        //获取子页面
        protected void GetPageData(TreeNode n)
        {
            //string s = n.Value.Split('_')[0] + " " + n.Text;
            DataTable dt = dba.GetSqlDataTable("Select * From power_page Where fatherID='" + n.Value.Split('_')[0] + "'");
            
            
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = dr["name"].ToString();
                newNode.Value = dr["page_id"].ToString() + "_Page";
                newNode.PopulateOnDemand = true;
                newNode.SelectAction = TreeNodeSelectAction.Expand;
                newNode.ShowCheckBox = true;
                n.ChildNodes.Add(newNode);
            }
            
        }

        //获取控件
        protected void GetControlData(TreeNode n)
        {
            string sql = "Select * From page_control Where page_id='" + n.Value.Split('_')[0] + "' and (gotoPage='' or gotoPage not in (Select page From power_page Where fatherID='" + n.Value.Split('_')[0] + "')";
          
            
            DataTable dt = dba.GetSqlDataTable("Select * From page_control Where page_id='" + n.Value.Split('_')[0] + "' and (gotoPage='' or gotoPage not in (Select page From power_page Where fatherID='" + n.Value.Split('_')[0] + "'))");
            
            foreach (DataRow dr in dt.Rows)
            {
                TreeNode newNode = new TreeNode();
                newNode.Text = dr["name"].ToString();
                newNode.Value = dr["con_id"].ToString()+"_Control";
                newNode.PopulateOnDemand = false;
                newNode.SelectAction = TreeNodeSelectAction.Expand;
                newNode.ShowCheckBox = true;
                n.ChildNodes.Add(newNode);
            }
        }
       
        #endregion

        
        /*----------------------------------
         * 子节点选中后，自动将其父节点选中
         * ---------------------------------*/
        protected void ConfigPower_TreeNodeCheckChanged(object sender, TreeNodeEventArgs e)
        {
            string s = e.Node.Text+" "+e.Node.Value;
            if (e.Node.Checked == true)
            {
                TreeNode nTemp = e.Node.Parent;
               
                for (int i = e.Node.Depth - 1; i >= 1; i--)
                {
                    nTemp.Checked = true;
                    nTemp = nTemp.Parent;
                }
            }
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {
           
            string sqlText = "";
            if (_Action != "Update")
            {
                
                //检查是否有相同的组名
                sqlText = "select * from role_info where r_name='" + GroupName.Text.Trim() + "'";
                //sqlText = "select * from Power_Group where GroupName='" + GroupName.Text + "'";
                //SqlConnection sqlConn = new SqlConnection();
                //SqlCommand sqlCmd = new SqlCommand();
                //sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                //sqlCmd.Connection = sqlConn;
                //sqlCmd.CommandText = sqlText;
                //SqlDataReader dr = DBCallCommon.GetDataReader(sqlConn, sqlCmd);
                SqlDataReader dr = dba.getDRExSQL(sqlText);
                if (dr.Read())
                {
                    LBCheckIsSame.Text = "已存在相同的角色";
                    return;
                }
                dr.Close();
            }
            SavePower();
            Response.Write("<script>javascript:window.close();</script>");
    
        }

       

        protected void SavePower()
        {
            List<string> lstrCtrlID = new List<string>();
            string role_id = "";
            string sqlText = "";           

            TreeNodeCollection tnc = ConfigPower.CheckedNodes;

            if (_Action == "Update")
            {
                role_id = Request.QueryString["r_id"];
                string strDel = "Delete From power_role Where r_id='" + role_id + "';";
                strDel += "Update role_info set r_name = '" + GroupName.Text.Trim() + "' where r_id = '" + role_id + "'";
                //SqlConnection sqlConn = new SqlConnection();
                //SqlCommand sqlCmd = new SqlCommand();
                //sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
                //sqlCmd.Connection = sqlConn;
                //sqlCmd.CommandText = strDel;
                //SqlDataReader dr = DBCallCommon.GetDataReader(sqlConn, sqlCmd);
                SqlDataReader dr = dba.getDRExSQL(strDel);

                dr.Close();
            }

            if (_Action != "Update")
            {
                string sqlInst = "Insert into role_info(r_name) Values(";
                sqlInst += "'" + GroupName.Text.Trim() + "') select @@Identity as idnty;";
                SqlDataReader dr = dba.getDRExSQL(sqlInst);
                if (dr.Read())
                {
                    role_id = dr["idnty"].ToString();
                }
                dr.Close();
            }

            foreach (TreeNode n in tnc)
            {
               
                //如果是页面节点，则查找相应的控件
                if (n.Value.Contains("Page"))
                {
                    string ss = "Select * From page_control "
                                                 + "Where page_id='" + n.Parent.Value.Split('_')[0] + "' "
                                                 + "and gotoPage=(Select page From power_page Where page_id='" + n.Value.Split('_')[0] + "')";
                    DataTable dt = dba.GetSqlDataTable(ss);

                    if (dt.Rows.Count > 0)
                    {
                        sqlText += "Insert into power_role(r_id, page_id, control_id) Values(";
                        sqlText += "'" + role_id + "',";
                        sqlText += "'" + n.Parent.Value.Split('_')[0] + "',";
                        sqlText += "'" + dt.Rows[0]["con_id"].ToString() + "');";
                    }

                }
                //如果是控件节点，则直接存储
                else if (n.Value.Contains("Control"))
                {
                    sqlText += "Insert into power_role(r_id, page_id, control_id) Values(";                    
                    sqlText += "'" + role_id + "',";
                    sqlText += "'" + n.Parent.Value.Split('_')[0] + "',";
                    sqlText += "'" + n.Value.Split('_')[0] + "');";
                }
            }
            //插入数据库
            if (sqlText != "")
            {
                SqlDataReader dr = dba.getDRExSQL(sqlText);
                dr.Close();
            }

        }
        protected void QueryButton_Click(object sender, EventArgs e)
        {
        
        }

        protected void Reset1_Click(object sender, EventArgs e)
        {
            Response.Write("<script>javascript:window.close();</script>");
        }

    }
    
}
