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
using System.Text.RegularExpressions;
using System.Text;

namespace ZCZJ_DPF
{

    public partial class UserDefinedQueryConditions : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               this.InitControl(GridView1);
            }
            
        }

        private  void InitControl(GridView grv)
        {
            this.BindQueryTable(grv);
            this.BindQueryColumns("ddlColumnsName", grv, QueryColumnsType);
        }

        public static void UserDefinedExternalCallForInitControl(GridView grv)
        {
            for (int i = 0; i < grv.Rows.Count; i++)
            {
                DropDownList ddl_other_logic = (DropDownList)grv.Rows[i].FindControl("ddlLogic");
                DropDownList ddl_other_startKH = (DropDownList)grv.Rows[i].FindControl("ddlStartKuohao");
                DropDownList ddl_other_colunm = (DropDownList)grv.Rows[i].FindControl("ddlColumnsName");
                DropDownList ddl_other_relation = (DropDownList)grv.Rows[i].FindControl("ddlRelation");
                TextBox txt_other_text = (TextBox)grv.Rows[i].FindControl("txtVaue");
                DropDownList ddl_other_endKH = (DropDownList)grv.Rows[i].FindControl("ddlEndKuohao");

                ddl_other_logic.SelectedIndex = 0;
                ddl_other_startKH.SelectedIndex = 0;
                ddl_other_colunm.SelectedIndex = 0;
                ddl_other_relation.SelectedIndex = 0;
                txt_other_text.Text = "";
                ddl_other_endKH.SelectedIndex = 0;
            }
        }

        protected  void BindQueryColumns(string colunmDrpName, GridView grv, string bindType)
        {
            for (int i = 0; i < grv.Rows.Count; i++)
            {
                GridViewRow grow = grv.Rows[i];
                DropDownList ddlQueryControl = (DropDownList)grow.FindControl(colunmDrpName);
                BindDdlColumns(ddlQueryControl, bindType);
            }
            ((DropDownList)grv.Rows[0].FindControl("ddlLogic")).Visible = false;
            ((DropDownList)grv.Rows[0].FindControl("ddlStartKuohao")).Visible = false;
            ((DropDownList)grv.Rows[0].FindControl("ddlEndKuohao")).Visible = false;
        }

        protected  void BindQueryTable(GridView grv)
        {
            DataTable dt = new DataTable();
            for (int i = 0; i < QueryRows ; i++)
            {
                DataRow row = dt.NewRow();
                dt.Rows.Add(row);
            }
            grv.DataSource = dt;
            grv.DataBind();
        }


        protected  void BindDdlColumns(DropDownList ddl, string bindType)
        {
            DataTable dt = GetQueryColumnsText_Value();
            ddl.DataSource = dt;
            ddl.DataTextField = "DropDownList_Text";
            ddl.DataValueField = "DropDownList_Value";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem("-请选择-", "-请选择-"));
            ddl.SelectedIndex = 0;
        }

        protected  DataTable GetQueryColumnsText_Value()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("DropDownList_Text");
            dt.Columns.Add("DropDownList_Value");
            switch (QueryColumnsType)
            {
                case "TaskView":
                    #region
                    DataRow newRow1 = dt.NewRow();
                    newRow1[0] = "总序";
                    newRow1[1] = "BM_ZONGXU";
                    dt.Rows.Add(newRow1);
                    DataRow newRow2 = dt.NewRow();
                    newRow2[0] = "中文名称";
                    newRow2[1] = "BM_CHANAME";
                    dt.Rows.Add(newRow2);

                    DataRow newRow2_1 = dt.NewRow();
                    newRow2_1[0] = "英文名称";
                    newRow2_1[1] = "BM_ENGSHNAME";
                    dt.Rows.Add(newRow2_1);

                    DataRow newRow3 = dt.NewRow();
                    newRow3[0] = "序号";
                    newRow3[1] = "BM_XUHAO";
                    dt.Rows.Add(newRow3);


                    DataRow newRow4 = dt.NewRow();
                    newRow4[0] = "图号";
                    newRow4[1] = "BM_TUHAO";
                    dt.Rows.Add(newRow4);

                    DataRow newRow41 = dt.NewRow();
                    newRow41[0] = "物料编码";
                    newRow41[1] = "BM_MARID";
                    dt.Rows.Add(newRow41);

                    DataRow newRow6 = dt.NewRow();
                    newRow6[0] = "材料规格";
                    newRow6[1] = "BM_MAGUIGE";
                    dt.Rows.Add(newRow6);

                    DataRow newRow51 = dt.NewRow();
                    newRow51[0] = "材质";
                    newRow51[1] = "BM_MAQUALITY";
                    dt.Rows.Add(newRow51);

                    DataRow newRow61 = dt.NewRow();
                    newRow61[0] = "国标(标准)";
                    newRow61[1] = "BM_STANDARD";
                    dt.Rows.Add(newRow61);

                    DataRow newRow62 = dt.NewRow();
                    newRow62[0] = "单位";
                    newRow62[1] = "BM_MAUNIT";
                    dt.Rows.Add(newRow62);


                    DataRow newRow7 = dt.NewRow();
                    newRow7[0] = "材料种类";
                    newRow7[1] = "BM_MASHAPE";
                    dt.Rows.Add(newRow7);
                    DataRow newRow8 = dt.NewRow();
                    newRow8[0] = "库";
                    newRow8[1] = "BM_KU";
                    dt.Rows.Add(newRow8);
               
                 
                    DataRow newRow11 = dt.NewRow();
                    newRow11[0] = "长度";
                    newRow11[1] = "BM_MALENGTH";
                    dt.Rows.Add(newRow11);
                    DataRow newRow12 = dt.NewRow();
                    newRow12[0] = "宽度";
                    newRow12[1] = "BM_MAWIDTH";
                    dt.Rows.Add(newRow12);
                    DataRow newRow13 = dt.NewRow();
                    newRow13[0] = "材料单重";
                    newRow13[1] = "BM_MAUNITWGHT";
                    dt.Rows.Add(newRow13);
                    DataRow newRow14 = dt.NewRow();
                    newRow14[0] = "材料总重";
                    newRow14[1] = "BM_MATOTALWGHT";
                    dt.Rows.Add(newRow14);
                    DataRow newRow15 = dt.NewRow();
                    newRow15[0] = "材料总长";
                    newRow15[1] = "BM_MATOTALLGTH";
                    dt.Rows.Add(newRow15);
                    DataRow newRow16 = dt.NewRow();
                    newRow16[0] = "面域";
                    newRow16[1] = "BM_MABGZMY";
                    dt.Rows.Add(newRow16);

                    DataRow newRow16_1 = dt.NewRow();
                    newRow16_1[0] = "计划面域";
                    newRow16_1[1] = "BM_MPMY";
                    dt.Rows.Add(newRow16_1);

                    DataRow newRow17 = dt.NewRow();
                    newRow17[0] = "数量";
                    newRow17[1] = "BM_NUMBER";
                    dt.Rows.Add(newRow17);
                    DataRow newRow19 = dt.NewRow();
                    newRow19[0] = "下料";
                    newRow19[1] = "BM_XIALIAO";
                    dt.Rows.Add(newRow19);
                    DataRow newRow20 = dt.NewRow();
                    newRow20[0] = "工艺流程";
                    newRow20[1] = "BM_PROCESS";
                    dt.Rows.Add(newRow20);
                    DataRow newRow21 = dt.NewRow();
                    newRow21[0] = "备注";
                    newRow21[1] = "BM_NOTE";
                    dt.Rows.Add(newRow21);
                    DataRow newRow22 = dt.NewRow();
                    newRow22[0] = "库";
                    newRow22[1] = "BM_KU";
                    dt.Rows.Add(newRow22);

                    DataRow newRow28 = dt.NewRow();
                    newRow28[0] = "体现明细";
                    newRow28[1] = "BM_ISMANU";
                    dt.Rows.Add(newRow28);

                    DataRow newRow29 = dt.NewRow();
                    newRow29[0] = "是否定尺";
                    newRow29[1] = "BM_FIXEDSIZE";
                    dt.Rows.Add(newRow29);



           

                    break;
                    #endregion
                case "TaskidView":
                    #region
                    DataRow newRow_1 = dt.NewRow();
                    newRow_1[0] = "工程名称";
                    newRow_1[1] = "TSA_ENGNAME";
                    dt.Rows.Add(newRow_1);

                    DataRow newRow_2 = dt.NewRow();
                    newRow_2[0] = "项目名称";
                    newRow_2[1] = "TSA_PJNAME";
                    dt.Rows.Add(newRow_2);

                    DataRow newRow_3 = dt.NewRow();
                    newRow_3[0] = "生产制号";
                    newRow_3[1] = "TSA_ID";
                    dt.Rows.Add(newRow_3);

                    DataRow newRow_4 = dt.NewRow();
                    newRow_4[0] = "项目ID";
                    newRow_4[1] = "TSA_PJID";
                    dt.Rows.Add(newRow_4);

                    DataRow newRow_5 = dt.NewRow();
                    newRow_5[0] = "技术员";
                    newRow_5[1] = "TSA_TCCLERKNM";
                    dt.Rows.Add(newRow_5);

                    break;
                    #endregion
                case "MP":
                    #region
                    DataRow _newRow1 = dt.NewRow();
                    _newRow1[0] = "物料编码";
                    _newRow1[1] = "MP_MARID";
                    dt.Rows.Add(_newRow1);

                    DataRow _newRow2 = dt.NewRow();
                    _newRow2[0] = "物料名称";
                    _newRow2[1] = "MP_NAME";
                    dt.Rows.Add(_newRow2);

                    DataRow _newRow3 = dt.NewRow();
                    _newRow3[0] = "规格";
                    _newRow3[1] = "MP_GUIGE";
                    dt.Rows.Add(_newRow3);

                    DataRow _newRow4 = dt.NewRow();
                    _newRow4[0] = "材质";
                    _newRow4[1] = "MP_CAIZHI";
                    dt.Rows.Add(_newRow4);

                    DataRow _newRow5 = dt.NewRow();
                    _newRow5[0] = "国标";
                    _newRow5[1] = "MP_STANDARD";
                    dt.Rows.Add(_newRow5);
                    #endregion
                    break;
                case "BJ"://备件
                    #region
                    DataRow newRow__1 = dt.NewRow();
                    newRow__1[0] = "总序";
                    newRow__1[1] = "BM_ZONGXU";
                    dt.Rows.Add(newRow__1);
                    DataRow newRow__2 = dt.NewRow();
                    newRow__2[0] = "中文名称";
                    newRow__2[1] = "BM_CHANAME";
                    dt.Rows.Add(newRow__2);

                    DataRow newRow__3 = dt.NewRow();
                    newRow__3[0] = "序号";
                    newRow__3[1] = "BM_XUHAO";
                    dt.Rows.Add(newRow__3);


                    DataRow newRow__4 = dt.NewRow();
                    newRow__4[0] = "图号";
                    newRow__4[1] = "BM_TUHAO";
                    dt.Rows.Add(newRow__4);

                    DataRow newRow__41 = dt.NewRow();
                    newRow__41[0] = "物料编码";
                    newRow__41[1] = "BM_MARID";
                    dt.Rows.Add(newRow__41);

                    DataRow newRow__5 = dt.NewRow();
                    newRow__5[0] = "物料名称";
                    newRow__5[1] = "BM_MANAME";
                    dt.Rows.Add(newRow__5);

                    DataRow newRow__6 = dt.NewRow();
                    newRow__6[0] = "物料规格";
                    newRow__6[1] = "BM_MAGUIGE";
                    dt.Rows.Add(newRow__6);

                    DataRow newRow__61 = dt.NewRow();
                    newRow__61[0] = "国标(标准)";
                    newRow__61[1] = "BM_STANDARD";
                    dt.Rows.Add(newRow__61);


                    DataRow newRow__7 = dt.NewRow();
                    newRow__7[0] = "毛坯形状";
                    newRow__7[1] = "BM_MASHAPE";
                    dt.Rows.Add(newRow__7);
                    DataRow newRow__8 = dt.NewRow();
                    newRow__8[0] = "毛坯状态";
                    newRow__8[1] = "BM_MASTATE";
                    dt.Rows.Add(newRow__8);
                    DataRow newRow__9 = dt.NewRow();
                    newRow__9[0] = "明细序号";
                    newRow__9[1] = "BM_MSXUHAO";
                    dt.Rows.Add(newRow__9);
                    DataRow newRow__10 = dt.NewRow();
                    newRow__10[0] = "规格";
                    newRow__10[1] = "BM_GUIGE";
                    dt.Rows.Add(newRow__10);
                    #endregion
                    break;
                case "MS":
                    #region
                    DataRow msrow0 = dt.NewRow();
                    msrow0[0] = "序号";
                    msrow0[1] = "MS_NEWINDEX";
                    dt.Rows.Add(msrow0);

                    DataRow msrow1 = dt.NewRow();
                    msrow1[0] = "总序";
                    msrow1[1] = "MS_ZONGXU";
                    dt.Rows.Add(msrow1);

                    DataRow msrow2 = dt.NewRow();
                    msrow2[0] = "图号";
                    msrow2[1] = "MS_TUHAO";
                    dt.Rows.Add(msrow2);

                    DataRow msrow3 = dt.NewRow();
                    msrow3[0] = "名称";
                    msrow3[1] = "MS_NAME";
                    dt.Rows.Add(msrow3);

                    DataRow msrow4 = dt.NewRow();
                    msrow4[0] = "规格";
                    msrow4[1] = "MS_GUIGE";
                    dt.Rows.Add(msrow4);

                    DataRow msrow5 = dt.NewRow();
                    msrow5[0] = "材质";
                    msrow5[1] = "MS_CAIZHI";
                    dt.Rows.Add(msrow5);

                    DataRow msrow6 = dt.NewRow();
                    msrow6[0] = "标准";
                    msrow6[1] = "MS_STANDARD";
                    dt.Rows.Add(msrow6);

                    DataRow msrow7 = dt.NewRow();
                    msrow7[0] = "材质";
                    msrow7[1] = "MS_CAIZHI";
                    dt.Rows.Add(msrow7);

                    DataRow msrow8 = dt.NewRow();
                    msrow8[0] = "工艺流程";
                    msrow8[1] = "MS_PROCESS";
                    dt.Rows.Add(msrow8);

                    DataRow msrow9 = dt.NewRow();
                    msrow9[0] = "库";
                    msrow9[1] = "MS_KU";
                    dt.Rows.Add(msrow9);

                    DataRow msrow10 = dt.NewRow();
                    msrow10[0] = "备注";
                    msrow10[1] = "MS_NOTE";
                    dt.Rows.Add(msrow10);
                    break;
                    #endregion
                default: break;
            }
            dt.AcceptChanges();
            return dt;

        }


        public static string ReturnQueryString(GridView GridView1, Label Label1)
        {
            string retValue = "";
            UserDefinedQueryConditions Udqc=new UserDefinedQueryConditions();
            string exist_lastfour =Udqc.ExistedConditions.Trim().Length>5?Udqc.ExistedConditions.Trim().Substring(Udqc.ExistedConditions.Trim().Length-5,5).ToLower():"";
            string sql_gridview=Udqc.GetGridViewQueryString(GridView1);
            int startkh = Regex.Matches(sql_gridview, "\\(").Count;
            int endkh = Regex.Matches(sql_gridview, "\\)").Count;
            if (startkh == endkh)
            {
                if (sql_gridview.Trim() != "")
                {
                    if (exist_lastfour == "where")
                    {
                        retValue = "(" + sql_gridview + ")";
                    }
                    else
                    {
                        retValue = " AND " + "(" + sql_gridview + ")";
                    }
                }
                Label1.Visible = false;
            }
            else
            {
                Label1.Visible = true;
            }
            return retValue;
        }

        protected string GetGridViewQueryString(GridView GridView1)
        {
            StringBuilder strb_sql = new StringBuilder();
            strb_sql.Append("");
            if (((DropDownList)(GridView1.Rows[0].FindControl("ddlColumnsName"))).SelectedIndex != 0)
            {
                //第一行
                DropDownList ddl_first_colunm = (DropDownList)GridView1.Rows[0].FindControl("ddlColumnsName");
                DropDownList ddl_first_relation = (DropDownList)GridView1.Rows[0].FindControl("ddlRelation");
                TextBox txt_first_text = (TextBox)GridView1.Rows[0].FindControl("txtVaue");
                switch (ddl_first_relation.SelectedValue)
                {
                    case "0":
                        if (ddl_first_colunm.SelectedItem.Text == "总序" || ddl_first_colunm.SelectedItem.Text == "序号")
                        {
                            strb_sql.Append(" (" + ddl_first_colunm.SelectedValue + "='" + txt_first_text.Text.Trim() + "' or " + ddl_first_colunm.SelectedValue + " like '" + txt_first_text.Text.Trim() + ".%')");
                        }
                        else
                        {
                            strb_sql.Append(" " + ddl_first_colunm.SelectedValue + " like '%" + txt_first_text.Text.Trim() + "%'");
                        }
                        break;//包含
                    case "1":
                        strb_sql.Append(" " + ddl_first_colunm.SelectedValue + "='" + txt_first_text.Text.Trim() + "'");
                        break;//等于
                    case "2":
                        strb_sql.Append(" " + ddl_first_colunm.SelectedValue + "!='" + txt_first_text.Text.Trim() + "'");
                        break;//不等于
                    case "3":
                        strb_sql.Append(" " + ddl_first_colunm.SelectedValue + ">'" + txt_first_text.Text.Trim() + "'");
                        break;//大于
                    case "4":
                        strb_sql.Append(" " + ddl_first_colunm.SelectedValue + ">='" + txt_first_text.Text.Trim() + "'");
                        break;//大于或等于
                    case "5":
                        strb_sql.Append(" " + ddl_first_colunm.SelectedValue + "<'" + txt_first_text.Text.Trim() + "'");
                        break;//小于
                    case "6":
                        strb_sql.Append(" " + ddl_first_colunm.SelectedValue + "<='" + txt_first_text.Text.Trim() + "'");
                        break;//小于或等于
                    case "7":
                        if (ddl_first_colunm.SelectedItem.Text == "总序" || ddl_first_colunm.SelectedItem.Text == "序号")
                        {
                            strb_sql.Append(" (" + ddl_first_colunm.SelectedValue + "!='" + txt_first_text.Text.Trim() + "' and " + ddl_first_colunm.SelectedValue + " not like '" + txt_first_text.Text.Trim() + ".%')");
                        }
                        else
                        {
                            strb_sql.Append(" " + ddl_first_colunm.SelectedValue + " not like '%" + txt_first_text.Text.Trim() + "%'");
                        }
                        break;//不包含
                    default: break;
                }

                for (int i = 1; i < GridView1.Rows.Count; i++)
                {
                    //其它行
                    DropDownList ddl_other_logic = (DropDownList)GridView1.Rows[i].FindControl("ddlLogic");
                    DropDownList ddl_other_startKH = (DropDownList)GridView1.Rows[i].FindControl("ddlStartKuohao");
                    DropDownList ddl_other_colunm = (DropDownList)GridView1.Rows[i].FindControl("ddlColumnsName");
                    DropDownList ddl_other_relation = (DropDownList)GridView1.Rows[i].FindControl("ddlRelation");
                    TextBox txt_other_text = (TextBox)GridView1.Rows[i].FindControl("txtVaue");
                    DropDownList ddl_other_endKH = (DropDownList)GridView1.Rows[i].FindControl("ddlEndKuohao");
                    if (ddl_other_colunm.SelectedIndex != 0)
                    {
                        switch (ddl_other_relation.SelectedValue)
                        {
                            case "0":
                                if (ddl_other_colunm.SelectedItem.Text == "总序" || ddl_other_colunm.SelectedItem.Text == "序号")
                                {
                                    strb_sql.Append(" "+ddl_other_logic.SelectedValue+" "+ddl_other_startKH.SelectedValue+"(" + ddl_other_colunm.SelectedValue + "='" + txt_other_text.Text.Trim() + "' or " + ddl_other_colunm.SelectedValue + " like '" + txt_other_text.Text.Trim() + ".%')"+ddl_other_endKH.SelectedValue+"");
                                }
                                else
                                {
                                    strb_sql.Append(" " + ddl_other_logic.SelectedValue + " " + ddl_other_startKH.SelectedValue + "" + ddl_other_colunm.SelectedValue + " like '%" + txt_other_text.Text.Trim() + "%'" + ddl_other_endKH.SelectedValue + "");
                                }
                                break;//包含
                            case "1":
                                strb_sql.Append(" " + ddl_other_logic.SelectedValue + " " + ddl_other_startKH.SelectedValue + "" + ddl_other_colunm.SelectedValue + "='" + txt_other_text.Text.Trim() + "'" + ddl_other_endKH.SelectedValue + "");
                                break;//等于
                            case "2":
                                strb_sql.Append(" " + ddl_other_logic.SelectedValue + " " + ddl_other_startKH.SelectedValue + "" + ddl_other_colunm.SelectedValue + "!='" + txt_other_text.Text.Trim() + "'" + ddl_other_endKH.SelectedValue + "");
                                break;//不等于
                            case "3":
                                strb_sql.Append(" " + ddl_other_logic.SelectedValue + " " + ddl_other_startKH.SelectedValue + "" + ddl_other_colunm.SelectedValue + ">'" + txt_other_text.Text.Trim() + "'" + ddl_other_endKH.SelectedValue + "");
                                break;//大于
                            case "4":
                                strb_sql.Append(" " + ddl_other_logic.SelectedValue + " " + ddl_other_startKH.SelectedValue + "" + ddl_other_colunm.SelectedValue + ">='" + txt_other_text.Text.Trim() + "'" + ddl_other_endKH.SelectedValue + "");
                                break;//大于或等于
                            case "5":
                                strb_sql.Append(" " + ddl_other_logic.SelectedValue + " " + ddl_other_startKH.SelectedValue + "" + ddl_other_colunm.SelectedValue + "<'" + txt_other_text.Text.Trim() + "'" + ddl_other_endKH.SelectedValue + "");
                                break;//小于
                            case "6":
                                strb_sql.Append(" " + ddl_other_logic.SelectedValue + " " + ddl_other_startKH.SelectedValue + "" + ddl_other_colunm.SelectedValue + "<='" + txt_other_text.Text.Trim() + "'" + ddl_other_endKH.SelectedValue + "");
                                break;//小于或等于
                            case "7":
                                if (ddl_other_colunm.SelectedItem.Text == "总序" || ddl_other_colunm.SelectedItem.Text == "序号")
                                {
                                    strb_sql.Append(" " + ddl_other_logic.SelectedValue + " " + ddl_other_startKH.SelectedValue + "(" + ddl_other_colunm.SelectedValue + "!='" + txt_other_text.Text.Trim() + "' and " + ddl_other_colunm.SelectedValue + " not like '" + txt_other_text.Text.Trim() + ".%')" + ddl_other_endKH.SelectedValue + "");
                                }
                                else
                                {
                                    strb_sql.Append(" " + ddl_other_logic.SelectedValue + " " + ddl_other_startKH.SelectedValue + "" + ddl_other_colunm.SelectedValue + " not like '%" + txt_other_text.Text.Trim() + "%'" + ddl_other_endKH.SelectedValue + "");
                                }
                                break;//不包含
                            default: break;
                        }
                    }
                }
            }
            return strb_sql.ToString();
        }

        #region Properties
        //自定义查询条件多少行
        public  int QueryRows
        {
            get
            {
                if (ViewState["QueryRows"] != null)
                {
                    return (int)ViewState["QueryRows"];
                }
                else
                {
                    return 6;
                }

            }
            set
            {
                ViewState["QueryRows"] = value;
            }
        }

        //绑定字段类型
        public  string QueryColumnsType
        {
            get
            {
                if (ViewState["QueryColumnsType"] != null)
                {
                    return ViewState["QueryColumnsType"].ToString();
                }
                else
                {
                    return "";
                }
            }

            set
            {
                ViewState["QueryColumnsType"] = value;
            }
        }
        //已存在的查询条件
        public  string ExistedConditions
        {
            get
            {
                if (ViewState["ExistedConditions"] != null)
                {
                    return ViewState["ExistedConditions"].ToString();
                }
                else
                {
                    return "";
                }
            }

            set
            {
                ViewState["ExistedConditions"] = value;
            }
        }

        #endregion


    }
}