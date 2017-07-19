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
using System.Data.SqlClient;

namespace ZCZJ_DPF
{
    public partial class UserInputZongxu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DBCallCommon.SessionLostToLogIn(Session["UserID"]);
        }
        /// <summary>
        /// 隐藏当前控件
        /// </summary>
        protected void imbDelete_OnClick(object sender, EventArgs e)
        {
           ((Control)txtAfterFX.NamingContainer).Visible = false;
        }
        /// <summary>
        /// 查看数据源
        /// </summary>
        protected void imbgSource_OnClick(object sender, EventArgs e)
        {
            if (this.DataSourceSelected())
            {
                if (txtBeforeFX.Text.Trim() != "")
                {
                    string fx = "";
                    if (txtBeforeFX.Text.Trim().Contains("|"))
                    {
                        fx = txtBeforeFX.Text.Trim().Split('|')[1].Trim();
                    }
                    else
                    {
                        fx = txtBeforeFX.Text.Trim();
                    }
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "ShowSource('BM_ZONGXU','" + TaskID + "','" + TabelName + "','" + fx + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入【待复制父序】！！！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据源！！！');", true);
            }
        }

        protected void imbgNotImport_OnClick(object sender, EventArgs e)
        {
            if (this.DataSourceSelected())
            {
                if (txtBeforeFX.Text.Trim() != "")
                {
                    string fx = "";
                    if (txtBeforeFX.Text.Trim().Contains("|"))
                    {
                        fx = txtBeforeFX.Text.Trim().Split('|')[1].Trim();
                    }
                    else
                    {
                        fx = txtBeforeFX.Text.Trim();
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入【待复制父序】！！！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据源！！！');", true);
            }
        }
        /// <summary>
        /// 待复制父序改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtBeforeFX_OnTextChanged(object sender, EventArgs e)
        {
            if (txtBeforeFX.Text.Trim() != "")
            {
                string[] arry = txtBeforeFX.Text.Trim().Split('|');
                if (arry.Length == 3)
                {
                    InitTreeViewMp(arry[1].Trim());
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('录入【待复制父序】格式不正确！！！');", true);
                }
            }
        }
        /// <summary>
        /// 查看目标
        /// </summary>
        protected void imgbTarget_OnClick(object sender, EventArgs e)
        {
            if (this.DataSourceSelected())
            {
                if (txtAfterFX.Text.Trim() != "")
                {
                    string fx = txtAfterFX.Text.Trim();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "ShowTarget('BM_ZONGXU','" + TaskIDTarget + "','" + TabelNameTarget + "','" + fx + "','" + ArrayNotImport + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入【复制后父序】！！！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据源！！！');", true);
            }
        }
        /// <summary>
        /// 查看详细信息
        /// </summary>
        protected void lkbDetail_OnClick(object sender, EventArgs e)
        {
            if (this.DataSourceSelected())
            {
                if (txtAfterFX.Text.Trim() != ""&&txtBeforeFX.Text.Trim()!="")
                {
                    string tg_fx = txtAfterFX.Text.Trim();
                    string sc_fx = txtBeforeFX.Text.Trim().Split('|')[1].Trim();
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "", "ShowDetail('BM_ZONGXU','" + TaskIDTarget + "','" + TabelNameTarget + "','" + tg_fx + "','" + TaskID + "','" + TabelName + "','" + sc_fx + "','" + ArrayNotImport + "');", true);                    
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请输入【待复制父序】及【复制后父序】！！！');", true);
                }
            }
            else
            {
               ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据源！！！');", true);
            }
        }
        /// <summary>
        /// 目标源中是否有数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void txtTarget_OnTextChanged(object sender, EventArgs e)
        {
            if (txtAfterFX.Text.Trim() != "")
            {
                //是否存在数据
                string sql = "select count(*) as Num from " + TabelNameTarget + " where BM_ENGID = '" + TaskIDTarget + "' AND (BM_ZONGXU='" + txtAfterFX.Text.Trim() + "' OR BM_ZONGXU LIKE '" + txtAfterFX.Text.Trim() + ".%' )";
                SqlDataReader dr_sql = DBCallCommon.GetDRUsingSqlText(sql);
                dr_sql.Read();
                if (Convert.ToInt16(dr_sql["Num"].ToString()) > 0)
                {
                    lblTip.Text = "已存在(" + dr_sql["Num"].ToString() + ")条，可能无法全部导入";
                    lblTip.ToolTip = "点击【||导入信息||】查看详细信息！！！";
                    lblTip.Visible = true;
                }
                else
                {
                    lblTip.Text = "";
                    lblTip.ToolTip = "";
                    lblTip.Visible = false;
                }
                dr_sql.Close();
                //是否存在父级
                if (lblTip.Visible == false)
                {
                    if (txtAfterFX.Text.Trim().Contains('.'))
                    {
                        string xh_fj = txtAfterFX.Text.Trim().Substring(0, txtAfterFX.Text.Trim().LastIndexOf('.'));
                        string sql_fx = "select * from " + TabelNameTarget + " where BM_ENGID like '" + TaskIDTarget + "%' AND BM_ZONGXU='" + xh_fj + "'";
                        SqlDataReader dr_sql_fx = DBCallCommon.GetDRUsingSqlText(sql_fx);
                        if (!dr_sql_fx.HasRows)
                        {
                            lblTip.Text = "没有父级，无法导入";
                            lblTip.Visible = true;
                        }
                        else
                        {
                            lblTip.Text = "";
                            lblTip.Visible = false;
                        }
                    }
                }
            }
            else
            {
                lblTip.Text = "";
                lblTip.Visible = false;
            }
        }

        protected bool DataSourceSelected()
        {
            if ( TaskID != "" && TabelName != "" && FZ != "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        protected void ddlJishu_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            txtBeforeFX_OnTextChanged(null, null);
            this.QueryStringTreeView(TreeViewMp);
        }

        private void InitTreeViewMp(string xuhao)
        {
            TreeViewMp.Nodes.Clear();
            TreeNode tnode = new TreeNode("部件(总序-名称)", "0");
            TreeViewMp.Nodes.Add(tnode);
            TreeNode tn = TreeViewMp.Nodes[0];
            tn.PopulateOnDemand = false;
            string sql = "select substring(BM_ZONGXU,1,len(BM_ZONGXU)-charindex('.',reverse(BM_ZONGXU))) AS BM_FXUHAO,(CASE WHEN BM_ZONGXU='' THEN '总序空' ELSE BM_ZONGXU END) AS BM_ZONGXU,BM_CHANAME AS  BM_NAME,dbo.Splitnum(BM_ZONGXU,'.') as nParent from " + TabelName + " where BM_ENGID='" + TaskID + "' AND dbo.Splitnum(BM_ZONGXU,'.')<=" + ddlOrgJishu.SelectedValue + " AND (BM_ZONGXU='" + xuhao + "' OR BM_ZONGXU LIKE '" + xuhao + ".%')  AND dbo.Splitnum(BM_ZONGXU,'.')>0  ORDER BY dbo.f_formatstr(BM_ZONGXU, '.')";
            DataTable xuhaoDataTable = DBCallCommon.GetDTUsingSqlText(sql);
            if (xuhaoDataTable.Rows.Count > 0)
            {
                this.InitTreeMp(xuhaoDataTable, tn.ChildNodes, Convert.ToInt16(xuhaoDataTable.Rows[0]["nParent"].ToString()), xuhaoDataTable.Rows[0]["BM_FXUHAO"].ToString());
            }
        }

        protected void InitTreeMp(DataTable xuhaoDataTable, TreeNodeCollection Nds, int nParentID, string Fxuhao)//nParentID父节点ID
        {
            DataView dv = new DataView();
            TreeNode tmpNd;
            //int strId;
            dv.Table = xuhaoDataTable;
            dv.RowFilter = "nParent='" + nParentID + "' and BM_FXUHAO='" + Fxuhao + "'";

            foreach (DataRowView objRow in dv)
            {
                tmpNd = new TreeNode();
                tmpNd.Text =  objRow["BM_ZONGXU"].ToString() + "-" + objRow["BM_NAME"].ToString();
                tmpNd.Value = objRow["BM_ZONGXU"].ToString();
                tmpNd.ShowCheckBox = true;
                Nds.Add(tmpNd);
                this.InitTreeMp(xuhaoDataTable, Nds[Nds.Count - 1].ChildNodes, ((int)objRow["nParent"]) + 1, objRow["BM_ZONGXU"].ToString());
            }
        }

        protected void TreeViewMp_OnTreeNodeCheckChanged(object sender,EventArgs e)
        {
            this.QueryStringTreeView(TreeViewMp);
        }
        /// <summary>
        /// 返回TreeView勾选的项
        /// </summary>
        /// <param name="treeName"></param>
        /// <returns></returns>
        protected void QueryStringTreeView(TreeView treeName)
        {
            int first_level_count = treeName.Nodes[0].ChildNodes.Count;
            string xuhao_str = "";
            for (int i = 0; i < first_level_count; i++)
            {
                TreeNode childnode = (TreeNode)treeName.Nodes[0].ChildNodes[i];
                xuhao_str += this.GetChildNodeZongxu(childnode, "");
            }

            if(xuhao_str!="")
            {
                xuhao_str=xuhao_str.Trim().Substring(0,xuhao_str.Trim().Length-1);
                Label1.Text = "("+xuhao_str.Split(',').Length+")";
            }
            else
            {
                Label1.Text="(0)";
            }
            ModalPopupExtenderSearch.Show();
            ArrayNotImport = xuhao_str.Replace("'","-");
        }

        protected string GetChildNodeZongxu(TreeNode childnodes, string retValue)
        {
            if (childnodes.Checked)
            {
                retValue += "'" + childnodes.Value + "',";
            }

            TreeNode tn_child = childnodes;
            while (tn_child.ChildNodes.Count > 0)
            {
                foreach (TreeNode tn in tn_child.ChildNodes)
                {
                    tn_child = tn;
                    retValue = GetChildNodeZongxu(tn_child, retValue);
                }
            }

            return retValue;
        }

        protected void btnClose_OnClick(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }

        #region Properties


        public string TaskID
        {
            get
            {
                if (ViewState["TaskID"] != null)
                {
                    return (string)ViewState["TaskID"];
                }
                else
                {
                    return "";
                }

            }
            set
            {
                ViewState["TaskID"] = value;
            }
        }

        public string TabelName
        {
            get
            {
                if (ViewState["TabelName"] != null)
                {
                    return (string)ViewState["TabelName"];
                }
                else
                {
                    return "";
                }

            }

            set
            {
                ViewState["TabelName"] = value;
            }
        }

        public string TaskIDTarget
        {
            get
            {
                if (ViewState["TaskIDTarget"] != null)
                {
                    return (string)ViewState["TaskIDTarget"];
                }
                else
                {
                    return "";
                }

            }
            set
            {
                ViewState["TaskIDTarget"] = value;
            }
        }

        public string TabelNameTarget
        {
            get
            {
                if (ViewState["TabelNameTarget"] != null)
                {
                    return (string)ViewState["TabelNameTarget"];
                }
                else
                {
                    return "";
                }

            }

            set
            {
                ViewState["TabelNameTarget"] = value;
            }
        }


        //复制下级
        public string FZ
        {
            get
            {
                if (ViewState["FZ"] != null)
                {
                    return (string)ViewState["FZ"];
                }
                else
                {
                    return "";
                }

            }
            set
            {
                ViewState["FZ"] = value;
            }
        }
        //不导入项
        public string ArrayNotImport
        {
            get
            {
                if (ViewState["ArrayNotImport"] != null)
                {
                    return (string)ViewState["ArrayNotImport"];
                }
                else
                {
                    return "";
                }

            }
            set
            {
                ViewState["ArrayNotImport"] = value;
            }
        }
        #endregion
    }
}