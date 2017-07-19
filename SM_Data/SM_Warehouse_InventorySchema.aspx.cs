using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_Warehouse_InventorySchema : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                bindGV();//绑定条件框

                Date.Text = DateTime.Now.ToString("yyyy-MM-dd");//制定方案时间
                Planer.Text = Session["UserName"].ToString();
                GetStaff();//得到盘点人
                //TextBoxEndDate.Text = DateTime.Now.ToString("yyyy-MM-dd");//截至日期
                GetRootWarehouse();
                GetWarehouse();//盘点仓库
                GetMaType();//物料类型
                GetProject();//项目
            }
        }

        protected string generateCode()
        {
            string Datestring = DateTime.Now.ToString("yyyyMMdd");
            string Code = "";

            string sql = "SELECT MAX(PD_CODE) AS MaxCode FROM TBWS_INVENTORYSCHEMA " +
                "WHERE CHARINDEX('" +Datestring + "',PD_CODE)>0";

            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                if (dr["MaxCode"] != DBNull.Value)
                {
                    Code = dr["MaxCode"].ToString();
                }
            }
            dr.Close();
            if (Code == "")
            {
                return Datestring+"01";
            }
            else
            {
                int codenum = Convert.ToInt32(Code);
                codenum++;
                Code = codenum.ToString().PadLeft(10, '0');
                return Code;
            }
        }

        protected void GetStaff()
        {
            string sql = "SELECT DISTINCT ST_ID,ST_NAME FROM TBDS_STAFFINFO WHERE ST_DEPID='05'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DataRow row = dt.NewRow();
            row["ST_ID"] = 0;
            row["ST_NAME"] = "--请选择--";
            dt.Rows.InsertAt(row, 0);

            //盘点人

            DropDownListClerk.DataTextField = "ST_NAME";
            DropDownListClerk.DataValueField = "ST_ID";
            DropDownListClerk.DataSource = dt;
            DropDownListClerk.DataBind();

            //制单人

            DropDownListZDR.DataTextField = "ST_NAME";
            DropDownListZDR.DataValueField = "ST_ID";
            DropDownListZDR.DataSource = dt;
            DropDownListZDR.DataBind();

        }
        protected void GetRootWarehouse()
        {
            string sql = "SELECT DISTINCT WS_ID,WS_NAME FROM TBWS_WAREHOUSE WHERE WS_FATHERID='ROOT' ORDER BY WS_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            CheckBoxListRootWarehouse.DataTextField = "WS_NAME";
            CheckBoxListRootWarehouse.DataValueField = "WS_ID";
            CheckBoxListRootWarehouse.DataSource = dt;
            CheckBoxListRootWarehouse.DataBind();
        }
        protected void GetWarehouse()
        {
            string sql = "SELECT DISTINCT WS_ID,WS_NAME FROM TBWS_WAREHOUSE WHERE WS_FATHERID<>'ROOT' ORDER BY WS_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            CheckBoxListWarehouse.DataTextField = "WS_NAME";
            CheckBoxListWarehouse.DataValueField = "WS_ID";
            CheckBoxListWarehouse.DataSource = dt;
            CheckBoxListWarehouse.DataBind();
        }

        protected void GetMaType()
        {
            string sql = "SELECT DISTINCT TY_ID,TY_NAME FROM TBMA_TYPEINFO WHERE TY_FATHERID<>'ROOT' ORDER BY TY_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            CheckBoxListMaterialType.DataTextField = "TY_NAME";
            CheckBoxListMaterialType.DataValueField = "TY_ID";
            CheckBoxListMaterialType.DataSource = dt;
            CheckBoxListMaterialType.DataBind();
        }        
        
        //标准件按项目盘点
        protected void GetProject()
        {
            string sql = "SELECT DISTINCT PJ_ID,PJ_NAME FROM TBPM_PJINFO";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DataRow row = dt.NewRow();
            row["PJ_ID"]="备库";
            row["PJ_NAME"] = "备库";
            dt.Rows.Add(row);
            CheckBoxListEng.DataTextField = "PJ_NAME";
            CheckBoxListEng.DataValueField = "PJ_ID";
            CheckBoxListEng.DataSource = dt;
            CheckBoxListEng.DataBind();
        }

        protected void Confirm_Click(object sender, EventArgs e)
        {

            //string TimeFlag = "0";
            string warehouse = "";
            string warehousename = "";
            string matype = "";
            string matypename = "";
            string project = "";
            string projectname = "";
            string FromCode = TextBoxFromCode.Text;
            string ToCode = TextBoxToCode.Text;
            string FromStandard = TextBoxFromStandard.Text;
            string ToStandard = TextBoxToStandard.Text;

            string StrCondition =GetSubCondtion().Replace("'","^");

            if(DropDownListClerk.SelectedValue.ToString()==""||DropDownListZDR.SelectedValue.ToString()=="")
            {
                ClientScript.RegisterStartupScript(this.GetType(), "myscript", "<script type='text/javascript'>alert('请选择制单人和审核人!')</script>");
                return;
            }
            Code.Text = generateCode();//方案编号

            //string EndDate = TextBoxEndDate.Text;//截止日期

            for (int i = 0; i < CheckBoxListWarehouse.Items.Count;i++)
            {
                if (CheckBoxListWarehouse.Items[i].Selected == true)
                {
                    warehouse += CheckBoxListWarehouse.Items[i].Value + "-";
                    warehousename += CheckBoxListWarehouse.Items[i].Text + " ";
                }
            }

            for (int i = 0; i < CheckBoxListMaterialType.Items.Count; i++)
            {
                if (CheckBoxListMaterialType.Items[i].Selected == true)
                {
                    matype += CheckBoxListMaterialType.Items[i].Value + "-";
                    matypename += CheckBoxListMaterialType.Items[i].Text + " ";
                }
            }

            for (int i = 0; i < CheckBoxListEng.Items.Count; i++)
            {
                if (CheckBoxListEng.Items[i].Selected == true)
                {
                    project += CheckBoxListEng.Items[i].Value + "-";
                    projectname += CheckBoxListEng.Items[i].Text + " ";
                }
            }
            
            //如果选择了盘点范围则生成盘点方案和记录

            //=========================生成盘点方案=============

            //PD_EXPORTDATE系统封账时间，在GetInventory的存储过程中，会将其修改为核算时间

            string sql = "INSERT INTO TBWS_INVENTORYSCHEMA(PD_CODE,PD_SCHEMADATE,PD_EXPORTDATE,PD_PLANER," +
                    "PD_WAREHOUSE,PD_WAREHOUSENAME,PD_MATYID,PD_MATYNAME,PD_MCS,PD_MCE," +
                    "PD_MGS,PD_MGE,PD_PJ,PD_PJNAME,PD_CLERK,PD_ZDR,PD_MANAGER,PD_KEEPER,PD_PDDATE," +
                    "PD_DONESTATE,PD_VERIFIER,PD_VERIFYDATE,PD_VERIFYADV," +
                    "PD_NOTE,PD_CONDITION,PD_SCHEMATYPE) VALUES('" + Code.Text + "','" + Date.Text + "',convert(varchar(50),getdate(),120),'" + Session["UserID"].ToString() + "','" +
                    warehouse + "','" + warehousename + "','" + matype + "','" + matypename + "','" +
                    FromCode + "','" + ToCode + "','" + FromStandard + "','" + ToStandard + "','" +
                    project + "','" + projectname + "','" + DropDownListClerk.SelectedValue.ToString() + "','" + DropDownListZDR.SelectedValue.ToString() + "','','','','0','','','','" + Comment.Text + "','" + StrCondition + "','" + DropDownListType.SelectedValue + "')";
               
                DBCallCommon.ExeSqlText(sql);
           

            //=========================通过存储过程，生成盘点明细=======
                if (DropDownListType.SelectedValue == "0")
                {
                    sql = DBCallCommon.GetStringValue("connectionStrings");
                    SqlConnection con2 = new SqlConnection(sql);
                    con2.Open();
                    SqlCommand cmd2 = new SqlCommand("GetInventory1", con2);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandTimeout = 0;
                    cmd2.Parameters.Add("@InventoryCode", SqlDbType.VarChar, 50);
                    cmd2.Parameters["@InventoryCode"].Value = Code.Text;
                    cmd2.Parameters.Add("@ConditionWS", SqlDbType.VarChar, 500);
                    cmd2.Parameters["@ConditionWS"].Value = warehouse;//仓库
                    cmd2.Parameters.Add("@ConditionMT", SqlDbType.VarChar, 500);
                    cmd2.Parameters["@ConditionMT"].Value = matype;//物料类型
                    cmd2.Parameters.Add("@ConditionMCS", SqlDbType.VarChar, 500);
                    cmd2.Parameters["@ConditionMCS"].Value = FromCode;
                    cmd2.Parameters.Add("@ConditionMCE", SqlDbType.VarChar, 500);
                    cmd2.Parameters["@ConditionMCE"].Value = ToCode;
                    cmd2.Parameters.Add("@ConditionMGS", SqlDbType.VarChar, 500);
                    cmd2.Parameters["@ConditionMGS"].Value = FromStandard;
                    cmd2.Parameters.Add("@ConditionMGE", SqlDbType.VarChar, 500);
                    cmd2.Parameters["@ConditionMGE"].Value = ToStandard;
                    cmd2.Parameters.Add("@ConditionPJ", SqlDbType.VarChar, 500);
                    cmd2.Parameters["@ConditionPJ"].Value = project;

                    cmd2.Parameters.Add("@SubCondition", SqlDbType.VarChar, 5000);
                    cmd2.Parameters["@SubCondition"].Value = GetSubCondtion();

                    cmd2.ExecuteNonQuery();
                    con2.Close();
                }
                else
                {
                    sql = DBCallCommon.GetStringValue("connectionStrings");
                    SqlConnection con2 = new SqlConnection(sql);
                    con2.Open();
                    SqlCommand cmd2 = new SqlCommand("GetInventory", con2);
                    cmd2.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandTimeout = 0;
                    cmd2.Parameters.Add("@InventoryCode", SqlDbType.VarChar, 50);
                    cmd2.Parameters["@InventoryCode"].Value = Code.Text;
                    cmd2.Parameters.Add("@ConditionWS", SqlDbType.VarChar, 500);
                    cmd2.Parameters["@ConditionWS"].Value = warehouse;//仓库
                    cmd2.Parameters.Add("@ConditionMT", SqlDbType.VarChar, 500);
                    cmd2.Parameters["@ConditionMT"].Value = matype;//物料类型
                    cmd2.Parameters.Add("@ConditionMCS", SqlDbType.VarChar, 500);
                    cmd2.Parameters["@ConditionMCS"].Value = FromCode;
                    cmd2.Parameters.Add("@ConditionMCE", SqlDbType.VarChar, 500);
                    cmd2.Parameters["@ConditionMCE"].Value = ToCode;
                    cmd2.Parameters.Add("@ConditionMGS", SqlDbType.VarChar, 500);
                    cmd2.Parameters["@ConditionMGS"].Value = FromStandard;
                    cmd2.Parameters.Add("@ConditionMGE", SqlDbType.VarChar, 500);
                    cmd2.Parameters["@ConditionMGE"].Value = ToStandard;
                    cmd2.Parameters.Add("@ConditionPJ", SqlDbType.VarChar, 500);
                    cmd2.Parameters["@ConditionPJ"].Value = project;

                    cmd2.Parameters.Add("@SubCondition", SqlDbType.VarChar, 5000);
                    cmd2.Parameters["@SubCondition"].Value = GetSubCondtion();

                    cmd2.ExecuteNonQuery();
                    con2.Close();
                }

            Response.Redirect("SM_Warehouse_InventoryManage.aspx");
        }

        protected void Cancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("SM_Warehouse_InventoryManage.aspx");
        }


        ////查询
        //protected void Query_Click(object sender, EventArgs e)
        //{

        //    hfdStrCondition.Value = GetSubCondtion();

        //    refreshStyle();

        //    ModalPopupExtenderSearch.Hide();

        //}

        ////关闭
        //protected void btnClose_Click(object sender, EventArgs e)
        //{
        //    ModalPopupExtenderSearch.Hide();

        //}
        //重置
        protected void btnReset_Click(object sender, EventArgs e)
        {
            resetSubcondition();
        }



        #region 条件框


        private void bindGV()
        {
            GridViewSearch.DataSource = CreateTable();

            GridViewSearch.DataBind();
        }

        private DataTable CreateTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add(new DataColumn("index", typeof(int)));


            for (int i = 0; i < 10; i++)
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

            ItemList.Add("c.ID", "物料编码");
            ItemList.Add("c.MNAME", "物料名称");
            ItemList.Add("c.GUIGE", "规格型号");
            ItemList.Add("c.CAIZHI", "材质");
            ItemList.Add("a.SQ_PTC", "计划跟踪号");
            //ItemList.Add("GUIGE", "规格型号");
            //ItemList.Add("CAIZHI", "材质");
           

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

        protected void CheckBoxListRootWarehouse_SelectedIndexChanged(object sender, EventArgs e)
        {

            for (int i = 0; i < CheckBoxListRootWarehouse.Items.Count;i++ )
            {
                if (CheckBoxListRootWarehouse.Items[i].Selected)
                {
                    for (int j = 0; j < CheckBoxListWarehouse.Items.Count; j++)
                    {
                        if (CheckBoxListWarehouse.Items[j].Value.Substring(0, 2) == CheckBoxListRootWarehouse.Items[i].Value)
                        {
                            CheckBoxListWarehouse.Items[j].Selected = true;
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < CheckBoxListWarehouse.Items.Count; j++)
                    {
                        if (CheckBoxListWarehouse.Items[j].Value.Substring(0, 2) == CheckBoxListRootWarehouse.Items[i].Value)
                        {
                            CheckBoxListWarehouse.Items[j].Selected = false;
                        }
                    }
                
                }
            }   
        }
    }
}
