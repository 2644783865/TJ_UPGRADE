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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_add_delete : BasicPage
    {
        
        protected void Page_Load(object sender, EventArgs e)
        {

            
            if (!IsPostBack)
            {
                Delete.Click += new EventHandler(Delete_Click);
                Delete.Attributes.Add("OnClick", "Javascript:return confirm('你确定删除吗?');");
                bindGV();
                this.bindData();
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
            }
            CheckUser(ControlFinder);
        }


        private void bindData()
        {


            DataTable dt = DBCallCommon.GetDTUsingSqlText(this.StrWhere());
            if (dt.Rows.Count == 0)
            {                    
                NoDataPanel.Visible = true;
            }
            else { NoDataPanel.Visible = false; }


            this.GridViewShow.DataSource = dt;
            this.GridViewShow.DataBind();
        }
        protected void Add_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SM_Data/SM_Warehouse_add_detail.aspx?flag=add&&ID=");

        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            if (SelectCheckbox() != 0)
            {
                foreach (GridViewRow gr in GridViewShow.Rows)
                {
                    CheckBox cbx = gr.FindControl("CheckBox1") as CheckBox;
                    if (cbx.Checked)
                    {
                        string marid = GridViewShow.DataKeys[gr.RowIndex].Values["MARID"].ToString();//获取marid
                        string sqlstr1 = "delete  from TBWS_STORAGE_WARN where MARID='" + marid + "'";
                        //string sqlstr2 = "delete  from TBMA_MATERIAL where ID='" + GridView1.DataKeys + "'";
                        //DBCallCommon.ExeSqlText(sqlstr1);
                        DBCallCommon.ExeSqlText(sqlstr1);
                    }
                   
                 }
                bindData();

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);

            }
         
       }
        protected int SelectCheckbox()
        {
            int temp = 0;

            foreach (GridViewRow gr in GridViewShow.Rows)
            {
                CheckBox cbx = gr.FindControl("CheckBox1") as CheckBox;
                if (cbx.Checked)
                {
                     
                    temp++;
                }


            }
            return temp;
        }
    


        //查询
        protected void Query_Click(object sender, EventArgs e)
        {
            this.bindData();

            refreshStyle();

            ModalPopupExtenderSearch.Hide();

           UpdatePanelBody.Update();

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }
        //重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            clearCondition();
            resetSubcondition();
        }
        private void clearCondition()
        {

            //领料单编号
            TextBoxMCode.Text = string.Empty;
            TextBoxMName.Text = string.Empty;
            TextBoxMStandard.Text = string.Empty;
            TextBoxCZ.Text = string.Empty;
            TextBoxGB.Text = string.Empty;
            TextBoxWarnNum.Text = string.Empty;
            
        }


        protected string StrWhere()
        {
            string sql = "select MARID,MNAME,GUIGE,CAIZHI,GB,PURCUNIT,cast(WARNNUM as float) as WARNNUM,Type,BZJSBZ,cast(REASONABLENUM as float) as REASONABLENUM  from View_STORAGE_ADD_DELETE";
            string condition="";
            //物料代码条件
            if ((TextBoxMCode.Text != "") && (condition != ""))
            {
                condition += " AND " + " MARID LIKE '%" + TextBoxMCode.Text.Trim() + "%'";
            }
            else if ((TextBoxMCode.Text != "") && (condition == ""))
            {
                condition += " MARID LIKE '%" + TextBoxMCode.Text.Trim() + "%'";
            }
            //物料名称条件
            if ((TextBoxMName.Text != "") && (condition != ""))
            {
                condition += " AND " + " MNAME LIKE '%" + TextBoxMName.Text.Trim() + "%'";
            }
            else if ((TextBoxMName.Text != "") && (condition == ""))
            {
                condition += " MNAME LIKE '%" + TextBoxMName.Text.Trim() + "%'";
            }
            //规格型号条件
            if ((TextBoxMStandard.Text != "") && (condition != ""))
            {
                condition += " AND " + " GUIGE LIKE '%" + TextBoxMStandard.Text.Trim() + "%'";
            }
            else if ((TextBoxMStandard.Text != "") && (condition == ""))
            {
                condition += " GUIGE LIKE '%" + TextBoxMStandard.Text.Trim() + "%'";
            }
            //材质
            if ((TextBoxCZ.Text != "") && (condition != ""))
            {
                condition += " AND " + " CAIZHI LIKE '%" + TextBoxCZ.Text.Trim() + "%'";
            }
            else if ((TextBoxCZ.Text != "") && (condition == ""))
            {
                condition += " CAIZHI LIKE '%" + TextBoxCZ.Text.Trim() + "%'";
            }

            //材质
            if ((TextBoxGB.Text != "") && (condition != ""))
            {
                condition += " AND " + " GB LIKE '%" + TextBoxGB.Text.Trim() + "%'";
            }
            else if ((TextBoxGB.Text != "") && (condition == ""))
            {
                condition += " GB LIKE '%" + TextBoxGB.Text.Trim() + "%'";
            }
            //安全库存数
            if ((TextBoxWarnNum.Text != "") && (condition != ""))
            {
                condition += " AND " + " WARNNUM LIKE '%" + TextBoxWarnNum.Text.Trim() + "%'";
            }
            else if ((TextBoxWarnNum.Text != "") && (condition == ""))
            {
                condition += " WARNNUM LIKE '%" + TextBoxWarnNum.Text.Trim() + "%'";
            }

            //类别
            if ((DropDownListType.SelectedIndex != 0)&&(condition != ""))
            {
                condition += " and Type like '%"+DropDownListType.SelectedValue.ToString().Trim()+"'";
            }
            else if ((DropDownListType.SelectedIndex != 0) && (condition == ""))
            {
                condition += " Type like '%" + DropDownListType.SelectedValue.ToString().Trim() + "'";
            }


            string SubCondition = GetSubCondtion();

            if (condition != "")
            {

                if (SubCondition != "")

                    condition += DropDownListFatherLogic.SelectedValue + " (" + SubCondition + ")";

            }
            else
            {
                if (SubCondition != "")
                    condition += SubCondition;
            }





            if (condition != "")
            {                

                sql += " WHERE "+condition;
            }

            return sql;
        }



        #region 条件框


        private void bindGV()
        {
            this.GridViewSearch.DataSource = CreateTable();

            this.GridViewSearch.DataBind();
        }
        private DataTable CreateTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("index", typeof(int)));


            for (int i = 0; i < 4; i++)
            {
                DataRow row = dt.NewRow();
                row["index"] = i;
                dt.Rows.Add(row);
            }

            return dt;
        }
        

        private Dictionary<string, string> bindItemList()
        {
            Dictionary<string, string> ItemList = new Dictionary<string, string>();

            ItemList.Add("NO", "");            
            ItemList.Add("MARID", "物料编码");
            ItemList.Add("MNAME", "物料名称");
            ItemList.Add("GUIGE", "规格型号");
            ItemList.Add("CAIZHI", "材质");          
            ItemList.Add("GB", "国标");            

            return ItemList;
        }

        protected void GridViewSearch_DataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                ddl.DataTextField = "value";
                ddl.DataValueField = "key";
                ddl.DataSource = bindItemList();
                ddl.DataBind();

                if (gr.RowIndex == 0)
                {
                    (gr.FindControl("tb_logic") as TextBox).Visible = false;
                }
            }
        }

        protected string GetSubCondtion()
        {
            string subCondition = "";

            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                if (gr.RowIndex == 0)
                {

                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                    if (ddl.SelectedValue != "NO")
                    {
                        TextBox txtValue = (gr.FindControl("TextBoxValue") as TextBox);

                        DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                        subCondition = ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim());
                    }
                    else
                    {
                        break;
                    }
                }
                else
                {
                    DropDownList ddl = (gr.FindControl("DropDownListName") as DropDownList);

                    if (ddl.SelectedValue != "NO")
                    {
                        DropDownList ddlLogic = (gr.FindControl("DropDownListLogic") as DropDownList);

                        TextBox txtValue = (gr.FindControl("TextBoxValue") as TextBox);

                        DropDownList ddlRel = (gr.FindControl("DropDownListRelation") as DropDownList);

                        subCondition += " " + ddlLogic.SelectedValue + " " + ConvertRelation(ddl.SelectedValue, ddlRel.SelectedValue, txtValue.Text.Trim());
                    }

                    else
                    {
                        break;
                    }
                }
            }
            return subCondition;
        }

        private string ConvertRelation(string field, string relation, string fieldValue)
        {
            string obj = string.Empty;

         

            switch (relation)
            {
                case "0":
                    {
                        //包含

                        obj = field + "  LIKE  '%" + fieldValue + "%'";
                        break;
                    }
                case "1":
                    {
                        //等于
                        obj = field + "  =  '" + fieldValue + "'";
                        break;
                    }
                case "2":
                    {
                        //不等于
                        obj = field + "  !=  '" + fieldValue + "'";
                        break;
                    }
                case "3":
                    {
                        //大于
                        obj = field + "  >  '" + fieldValue + "'";
                        break;
                    }
                case "4":
                    {
                        //大于或等于
                        obj = field + "  >=  '" + fieldValue + "'";
                        break;
                    }
                case "5":
                    {
                        //小于
                        obj = field + "  <  '" + fieldValue + "'";
                        break;
                    }
                case "6":
                    {
                        //小于或等于
                        obj = field + "  <=  '" + fieldValue + "'";
                        break;
                    }
                case "7":
                    {
                        //不包含
                        obj = field + " NOT LIKE  '%" + fieldValue + "%'";
                        break;
                    }
                case "8":
                    {
                        //左包含
                        obj = field + "  LIKE  '" + fieldValue + "%'";
                        break;
                    }
                case "9":
                    {
                        //右包含
                        obj = field + "  LIKE  '%" + fieldValue + "'";
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            return obj;
        }

        private void resetSubcondition()
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                DropDownList ddl = gr.FindControl("DropDownListName") as DropDownList;
                foreach (ListItem lt in ddl.Items)
                {
                    if (lt.Selected)
                        lt.Selected = false;
                }
                ddl.Items[0].Selected = true;
                (gr.FindControl("TextBoxValue") as TextBox).Text = string.Empty; ;
            }

            refreshStyle();
        }
        private void refreshStyle()
        {
            foreach (GridViewRow gr in GridViewSearch.Rows)
            {
                if ((gr.FindControl("DropDownListName") as DropDownList).SelectedValue != "NO")
                {
                    if (gr.RowIndex != 0)
                    {
                        (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "block");
                        (gr.FindControl("tb_logic") as TextBox).Style.Add("display", "none");
                    }

                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_name") as TextBox).Style.Add("display", "none");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "block");
                    (gr.FindControl("tb_relation") as TextBox).Style.Add("display", "none");
                }
                else
                {
                    (gr.FindControl("DropDownListLogic") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_logic") as TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListName") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_name") as TextBox).Style.Add("display", "block");
                    (gr.FindControl("DropDownListRelation") as DropDownList).Style.Add("display", "none");
                    (gr.FindControl("tb_relation") as TextBox).Style.Add("display", "block");
                }
            }
        }

        #endregion


    }
}
