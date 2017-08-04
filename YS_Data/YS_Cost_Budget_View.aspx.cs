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

namespace ZCZJ_DPF.YS_Data
{
    public partial class YS_Cost_Budget_View : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                btn_import.Visible = false;
                BindPer();
                BindState();
                BindProject();
                BindEngineer();
                InitVar();
                GetTechRequireData();
                control_visible();
            }
            InitVar();
            CheckUser(ControlFinder);
        }

        /// <summary>
        /// 根据登陆人和设定控件权限
        /// </summary>
        protected void control_visible()
        {
            string DEP = Session["UserDeptID"].ToString();
            string name = Session["UserName"].ToString();
            for (int i = 4; i < 32; i++)//从合同号后面的列开始全部隐藏(旧预算数据可见)
            {
                GridView1.Columns[i].Visible = false;
            }
            GridView1.Columns[33].Visible = false; //附件列隐藏

            if (name == "邓朝晖" || name == "于建会" || DEP == "08")//财务部和公司领导
            {
                for (int i = 4; i < 32; i++)//全部显示
                {
                    GridView1.Columns[i].Visible = true;
                }
            }
            else if (DEP == "03" || name == "张超臣")//技术部
            {
                GridView1.Columns[25].Visible = true;
                GridView1.Columns[26].Visible = true;
                GridView1.Columns[27].Visible = true;
                GridView1.Columns[28].Visible = true;
                GridView1.Columns[5].Visible = true;
                for (int i = 12; i < 15; i++)
                {
                    GridView1.Columns[i].Visible = true;
                }
            }
            else if (DEP == "12" || name == "李冰飞")//市场部
            {
                btn_import.Visible = true;
                for (int i = 6; i < 12; i++)
                {
                    GridView1.Columns[i].Visible = true;
                }
                GridView1.Columns[19].Visible = true;
                GridView1.Columns[20].Visible = true;
                for (int i = 25; i < 32; i++)
                {
                    GridView1.Columns[i].Visible = true;
                }
                GridView1.Columns[33].Visible = true;  //附件列可见
            }
            else if (DEP == "04" || name == "柳强")//生产部
            {
                if (DEP == "04")
                {
                    btn_import.Visible = true;
                }
                GridView1.Columns[5].Visible = true;
                GridView1.Columns[25].Visible = true;
                GridView1.Columns[26].Visible = true;
                GridView1.Columns[27].Visible = true;
                GridView1.Columns[28].Visible = true;
                for (int i = 12; i < 15; i++)
                {
                    GridView1.Columns[i].Visible = true;
                }
            }
            else if (DEP == "06" && name == "张志东")
            {
                for (int i = 6; i < 12; i++)
                {
                    GridView1.Columns[i].Visible = true;
                }
                GridView1.Columns[20].Visible = true;
                GridView1.Columns[25].Visible = true;
                GridView1.Columns[26].Visible = true;
                GridView1.Columns[27].Visible = true;
                GridView1.Columns[28].Visible = true;
            }
            else if (DEP == "07" && name == "陈泽盛")
            {
                GridView1.Columns[19].Visible = true;
                GridView1.Columns[25].Visible = true;
                GridView1.Columns[26].Visible = true;
                GridView1.Columns[27].Visible = true;
                GridView1.Columns[28].Visible = true;
            }
            else
            {
                GridView1.Columns[25].Visible = true;
                GridView1.Columns[26].Visible = true;
                GridView1.Columns[27].Visible = true;
                GridView1.Columns[28].Visible = true;
            }
        }
        /// <summary>
        /// 填充制单人下拉框的数据
        /// </summary>
        protected void BindPer()
        {
            string sqltext = "SELECT DISTINCT YS_ADDNAME AS DDLVALUE,YS_ADDNAME AS DDLTEXT FROM YS_COST_BUDGET ORDER BY YS_ADDNAME";
            string dataText = "DDLTEXT";
            string dataValue = "DDLVALUE";
            DBCallCommon.BindDdl(ddl_addper, sqltext, dataText, dataValue);
        }

        /// <summary>
        /// 填充审核专题下拉框的数据
        /// </summary>
        protected void BindState()
        {
            string sqltext = "SELECT DISTINCT YS_STATE AS DDLVALUE,case when YS_STATE is null then '未提交' when YS_STATE='0' then '初始化'" +
            " when YS_STATE='1' then '提交未审批'when YS_STATE='2' then '审批中'when YS_STATE='3' then '已通过'when YS_STATE='4' then '已驳回' end  AS DDLTEXT FROM YS_COST_BUDGET ORDER BY YS_STATE";
            string dataText = "DDLTEXT";
            string dataValue = "DDLVALUE";
            DBCallCommon.BindDdl(ddl_State, sqltext, dataText, dataValue);
        }

        /// <summary>
        /// 填充项目名称下拉框的数据
        /// </summary>
        protected void BindProject()
        {
            string sqltext = "SELECT DISTINCT PCON_PJNAME AS DDLVALUE,PCON_PJNAME AS DDLTEXT FROM View_YS_COST_BUDGET ORDER BY PCON_PJNAME";
            string dataText = "DDLTEXT";
            string dataValue = "DDLVALUE";
            DBCallCommon.BindDdl(ddl_project, sqltext, dataText, dataValue);
        }

        /// <summary>
        /// 填充设备名称下拉框的数据
        /// </summary>
        protected void BindEngineer()
        {
            string sqltext = "SELECT DISTINCT TSA_ENGNAME AS DDLVALUE,PCON_ENGNAME AS DDLTEXT FROM View_YS_COST_BUDGET where PCON_PJNAME='" + ddl_project.SelectedItem.ToString() + "' ORDER BY PCON_ENGNAME";
            string dataText = "DDLTEXT";
            string dataValue = "DDLVALUE";
            DBCallCommon.BindDdl(ddl_engineer, sqltext, dataText, dataValue);
        }


        #region 分页
        /// <summary>
        /// 初始化页面信息（1、初始化页面查询对象，2、将页面相关信息传递给页面控件）
        /// </summary>
        private void InitVar()
        {
            InitPager();
            
            //将页面相关信息传递给页码控件
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);//将翻页事件处理程序添加到翻页事件监听器上           
            UCPaging1.PageSize = pager.PageSize;//每页显示的记录数传递给页面控件
        }

        /// <summary>
        /// 翻页事件处理程序
        /// </summary>
        /// <param name="pageNumber"></param>
        void Pager_PageChanged(int pageNumber)
        {
            GetTechRequireData();
        }

        /// <summary>
        /// 初始化分页查询对象，用于编写sql命令
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "View_YS_COST_BUDGET";
            pager.PrimaryKey = "YS_CONTRACT_NO";
            pager.ShowFields = "YS_CONTRACT_NO,PCON_PJNAME,PCON_ENGNAME,YS_BUDGET_INCOME,YS_OUT_LAB_MAR,YS_FERROUS_METAL,YS_PURCHASE_PART,YS_MACHINING_PART,YS_PAINT_COATING,YS_ELECTRICAL,YS_OTHERMAT_COST,YS_TEAM_CONTRACT, " +
            "YS_FAC_CONTRACT,YS_PRODUCT_OUT,YS_MANU_COST,YS_SELL_COST,YS_MANAGE_COST,YS_Taxes_Cost,YS_TRANS_COST,YS_FERROUS_METAL+YS_PURCHASE_PART+YS_MACHINING_PART+YS_PAINT_COATING+YS_ELECTRICAL+YS_OTHERMAT_COST AS YS_MAR_SUM, " +
            "YS_MANU_COST+YS_SELL_COST+YS_MANAGE_COST AS YS_FINA_SUM,YS_PROFIT,YS_PROFIT_TAX,YS_PROFIT_TAX/YS_BUDGET_INCOME as YS_PROFIT_TAX_RATE,YS_ADDNAME,YS_ADDTIME,YS_NOTE,YS_STATE,YS_TIME";
            pager.OrderField = "YS_ADDTIME";
            pager.StrWhere = this.GetStrWhere();
            pager.OrderType = 1;//按任务名称升序排列
            pager.PageSize = 10;
        }


        /// <summary>
        /// 更新GridView控件和页码控件数据，没有用的？？？
        /// </summary>
        private void InitInfo()
        {
            
            GetTechRequireData();
        }

        /// <summary>
        /// 使用页面查询对象，更新GridView的数据和页码控件的数据
        /// </summary>
        protected void GetTechRequireData()
        {
            //将当前页数传递给页面查询对象
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, GridView1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
        }


        /// <summary>
        /// 获取sql命令的where部分（查询条件）
        /// </summary>
        /// <returns></returns>
        protected string GetStrWhere()
        {
            string strwhere = " 1=1 ";
            strwhere += "and YS_CONTRACT_NO like '%" + txt_search.Text.ToString() + "%'";

            if (ddl_addper.SelectedIndex != 0)//制单人
            {
                strwhere += "and YS_ADDNAME='" + ddl_addper.SelectedValue + "'";
            }

            if (ddl_State.SelectedIndex != 0)//审核状态
            {
                if (ddl_State.SelectedValue == "")
                {
                    strwhere += "and YS_STATE is null";
                }
                else
                {
                    strwhere += "and YS_STATE='" + ddl_State.SelectedValue + "'";
                }
            }

            if (ddl_project.SelectedIndex != 0)//项目名称
            {
                strwhere += " and PCON_PJNAME='" + ddl_project.SelectedValue + "'";
            }
            if (ddl_engineer.SelectedIndex != 0)//工程名称
            {
                strwhere += " and PCON_ENGNAME='" + ddl_engineer.SelectedValue + "'";
            }

            if (ckb_time.Checked == true)
            {
                strwhere += " and YS_TIME<GETDATE() and isnull(YS_STATE,'')!='3'";
            }

            return strwhere;
        }
        #endregion

        protected void GridView1_onrowdatabound(object sender, GridViewRowEventArgs e)
        {
            String controlId = ((GridView)sender).ClientID;
            String uniqueId = "";
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                uniqueId = String.Format("{0}{1}", controlId, e.Row.RowIndex);
                //e.Row.Attributes.Add("onclick", "c=this.style.backgroundColor;this.style.backgroundColor='#ffcc33'");
                //e.Row.Attributes.Add("onclick", "ItemOver(this)");  //单击行变色
                e.Row.Attributes.Add("onclick", String.Format("SelectRow('{0}', this);", uniqueId));
                //e.Row.Attributes.Add("onclick", "style.backgroundColor=c");
                string DEP = Session["UserDeptID"].ToString();
                string name = Session["UserName"].ToString();
                string addper = ((Label)e.Row.FindControl("lbl_addper")).Text.ToString();
                string lbl_CONTRACT_NO = ((Label)e.Row.FindControl("lbl_YS_CONTRACT_NO")).Text.ToString();
                Encrypt_Decrypt ed = new Encrypt_Decrypt();
                string CONTRACT_NO = ed.EncryptText(lbl_CONTRACT_NO);
                e.Row.Cells[1].Attributes.Add("ondblclick", "ShowContract('" + lbl_CONTRACT_NO + "')");
                e.Row.Cells[1].Attributes.Add("title", "双击关联合同信息！");

                string sql_datetime = "select YS_TIME from View_YS_COST_BUDGET where YS_CONTRACT_NO='" + lbl_CONTRACT_NO + "'";
                System.Data.DataTable dt_deadline = DBCallCommon.GetDTUsingSqlText(sql_datetime);
                string datetime_deadline = dt_deadline.Rows[0]["YS_TIME"].ToString();
                DateTime dt_dl = DateTime.Parse(datetime_deadline);//获取截止时间
                DateTime dt_now = DateTime.Now.AddDays(1);//获取当前时间的后天

                #region 其它部门登录

                if (DEP == "03" || name == "张超臣")//技术部
                {
                    string sql_checkstate = "select YS_JISHU from YS_COST_BUDGET where YS_CONTRACT_NO='" + lbl_CONTRACT_NO + "'";
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_checkstate);
                    if (dt.Rows[0][0].ToString() == "0" || dt.Rows[0][0].ToString() == "3")//初次进入，或者保存过
                    {
                        e.Row.Cells[0].BackColor = System.Drawing.Color.Yellow;
                        e.Row.Cells[0].ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[5].BackColor = System.Drawing.Color.Yellow;
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.Blue;
                        e.Row.Cells[5].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','5')");
                        e.Row.Cells[5].Attributes["style"] = "Cursor:hand";
                        e.Row.Cells[5].Attributes.Add("title", "双击对外协费用进行完善");
                    }
                    else if (dt.Rows[0][0].ToString() == "2")//已经提交部门审核
                    {
                        e.Row.Cells[0].BackColor = System.Drawing.Color.Green;
                        e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                        e.Row.Cells[5].BackColor = System.Drawing.Color.Green;
                        e.Row.Cells[5].ForeColor = System.Drawing.Color.White;
                        e.Row.Cells[5].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','5')");
                        e.Row.Cells[5].Attributes["style"] = "Cursor:hand";
                        e.Row.Cells[5].Attributes.Add("title", "双击对外协费用进行审核");
                    }
                    else//部门领导审核完毕
                    {
                        e.Row.Cells[5].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','5')");
                        e.Row.Cells[5].Attributes["style"] = "Cursor:hand";
                        e.Row.Cells[5].Attributes.Add("title", "双击查看外协费用明细");
                    }

                    if (dt.Rows[0][0].ToString() != "1")
                    {
                        if (dt_now > dt_dl)
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                            e.Row.Cells[0].ForeColor = System.Drawing.Color.Blue;
                        }
                    }

                    for (int i = 12; i < 15; i++)//可查看生产部明细
                    {
                        e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
                        e.Row.Cells[i].Attributes.Add("ondblclick", "PurMarView('" + CONTRACT_NO + "','" + i + "')");
                        e.Row.Cells[i].Attributes.Add("title", "双击查看人工费用明细");
                    }
                }
                else if (DEP == "12" || name == "李冰飞")//市场部
                {
                    #region 制单人登录
                    if (name == addper)
                    {
                        string sql_editstate = "select YS_JISHU,YS_SHICHANG,YS_SHENGCHAN,YS_CAIWU,YS_STATE from YS_COST_BUDGET where YS_CONTRACT_NO='" + lbl_CONTRACT_NO + "'";
                        System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_editstate);
                        string jishu = dt.Rows[0][0].ToString();
                        string shichang = dt.Rows[0][1].ToString();
                        string shengchan = dt.Rows[0][2].ToString();
                        string caiwu = dt.Rows[0][3].ToString();
                        string State = dt.Rows[0][4].ToString();

                        if (State == "3")
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Pink;
                            Button btn = ((Button)e.Row.FindControl("btn_YS_Modify"));
                            btn.Visible = true;
                        }
                        else if (State == "4")
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                        }
                    }
                    #endregion

                    string sql_checkstate = "select YS_SHICHANG from YS_COST_BUDGET where YS_CONTRACT_NO='" + lbl_CONTRACT_NO + "'";
                    System.Data.DataTable dt_state = DBCallCommon.GetDTUsingSqlText(sql_checkstate);

                    for (int i = 6; i < 12; i++)
                    {

                        if (dt_state.Rows[0][0].ToString() == "0" || dt_state.Rows[0][0].ToString() == "3")
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Yellow;
                            e.Row.Cells[0].ForeColor = System.Drawing.Color.Blue;
                            e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                            e.Row.Cells[i].ForeColor = System.Drawing.Color.Blue;
                            e.Row.Cells[i].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','" + i + "')");
                            e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
                            e.Row.Cells[i].Attributes.Add("title", "双击对材料费用进行完善");

                            e.Row.Cells[19].BackColor = System.Drawing.Color.Yellow;
                            e.Row.Cells[19].ForeColor = System.Drawing.Color.Blue;
                            e.Row.Cells[19].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','19')");
                            e.Row.Cells[19].Attributes["style"] = "Cursor:hand";
                            e.Row.Cells[19].Attributes.Add("title", "双击对运费用进行完善");
                        }
                        else if (dt_state.Rows[0][0].ToString() == "2")
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Green;
                            e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                            e.Row.Cells[i].BackColor = System.Drawing.Color.Green;
                            e.Row.Cells[i].ForeColor = System.Drawing.Color.White;
                            e.Row.Cells[i].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','" + i + "')");
                            e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
                            e.Row.Cells[i].Attributes.Add("title", "双击对材料费用进行审批");

                            e.Row.Cells[19].BackColor = System.Drawing.Color.Green;
                            e.Row.Cells[19].ForeColor = System.Drawing.Color.White;
                            e.Row.Cells[19].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','19')");
                            e.Row.Cells[19].Attributes["style"] = "Cursor:hand";
                            e.Row.Cells[19].Attributes.Add("title", "双击对运费用进行审批");
                        }
                        else
                        {
                            e.Row.Cells[i].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','" + i + "')");
                            e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
                            e.Row.Cells[i].Attributes.Add("title", "双击查看材料费用明细");
                            e.Row.Cells[19].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','19')");
                            e.Row.Cells[19].Attributes["style"] = "Cursor:hand";
                            e.Row.Cells[19].Attributes.Add("title", "双击查看运费明细");

                        }
                    }

                    if (dt_state.Rows[0][0].ToString() != "1")
                    {
                        if (dt_now > dt_dl)
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                            e.Row.Cells[0].ForeColor = System.Drawing.Color.Blue;
                        }
                    }
                }

                else if (DEP == "04" || name == "柳强")//生产部
                {
                    string sql_checkstate = "select YS_SHENGCHAN from YS_COST_BUDGET where YS_CONTRACT_NO='" + lbl_CONTRACT_NO + "'";
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_checkstate);
                    for (int i = 12; i < 15; i++)
                    {
                        if (dt.Rows[0][0].ToString() == "0" || dt.Rows[0][0].ToString() == "3")
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Yellow;
                            e.Row.Cells[0].ForeColor = System.Drawing.Color.Blue;
                            e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                            e.Row.Cells[i].ForeColor = System.Drawing.Color.Blue;
                            e.Row.Cells[i].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','" + i + "')");
                            e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
                            e.Row.Cells[i].Attributes.Add("title", "双击对人工费用进行完善");
                        }

                        else if (dt.Rows[0][0].ToString() == "2")
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Green;
                            e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                            e.Row.Cells[i].BackColor = System.Drawing.Color.Green;
                            e.Row.Cells[i].ForeColor = System.Drawing.Color.White;
                            e.Row.Cells[i].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','" + i + "')");
                            e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
                            e.Row.Cells[i].Attributes.Add("title", "双击对人工费用进行审批");
                        }
                        else
                        {
                            e.Row.Cells[i].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','" + i + "')");
                            e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
                            e.Row.Cells[i].Attributes.Add("title", "双击查看人工费明细");
                        }
                    }

                    if (dt.Rows[0][0].ToString() != "1")
                    {
                        if (dt_now > dt_dl)
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                            e.Row.Cells[0].ForeColor = System.Drawing.Color.Blue;
                        }
                    }

                    e.Row.Cells[5].Attributes["style"] = "Cursor:hand";
                    e.Row.Cells[5].Attributes.Add("ondblclick", "PurMarView('" + CONTRACT_NO + "','5')");
                    e.Row.Cells[5].Attributes.Add("title", "双击查看外协费用明细");
                }

                else if (DEP == "08" || name == "张勇")
                {
                    string sql_checkstate = "select YS_CAIWU,YS_JISHU,YS_SHENGCHAN,YS_SHICHANG,YS_STATE from YS_COST_BUDGET where YS_CONTRACT_NO='" + lbl_CONTRACT_NO + "'";
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_checkstate);
                    for (int i = 5; i < 25; i++)
                    {
                        if ((i > 15 || i == 15) && (i < 19))
                        {
                            if (dt.Rows[0][0].ToString() == "0" || dt.Rows[0][0].ToString() == "3")
                            {
                                e.Row.Cells[0].BackColor = System.Drawing.Color.Yellow;
                                e.Row.Cells[0].ForeColor = System.Drawing.Color.Blue;
                                e.Row.Cells[i].BackColor = System.Drawing.Color.Yellow;
                                e.Row.Cells[i].ForeColor = System.Drawing.Color.Blue;
                                e.Row.Cells[i].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','" + i + "')");
                                e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
                                e.Row.Cells[i].Attributes.Add("title", "双击对其他费用进行完善");
                            }
                            else if (dt.Rows[0][0].ToString() == "2")
                            {
                                e.Row.Cells[0].BackColor = System.Drawing.Color.Green;
                                e.Row.Cells[0].ForeColor = System.Drawing.Color.White;
                                e.Row.Cells[i].BackColor = System.Drawing.Color.Green;
                                e.Row.Cells[i].ForeColor = System.Drawing.Color.White;
                                e.Row.Cells[i].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','" + i + "')");
                                e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
                                e.Row.Cells[i].Attributes.Add("title", "双击对其他费用进行审批");
                            }
                            else
                            {
                                e.Row.Cells[i].Attributes.Add("ondblclick", "PurMarEdit('" + CONTRACT_NO + "','" + DEP + "','" + i + "')");
                                e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
                                e.Row.Cells[i].Attributes.Add("title", "双击查看其它费用明细");
                            }
                            continue;
                        }
                        e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
                        e.Row.Cells[i].Attributes.Add("ondblclick", "PurMarView('" + CONTRACT_NO + "','" + i + "')");
                        e.Row.Cells[i].Attributes.Add("title", "双击查看明细");
                    }

                    if (name == "张勇")
                    {
                        if (dt.Rows[0]["YS_CAIWU"].ToString() != "1" ||
                            dt.Rows[0]["YS_JISHU"].ToString() != "1" ||
                            dt.Rows[0]["YS_SHENGCHAN"].ToString() != "1" ||
                            dt.Rows[0]["YS_SHICHANG"].ToString() != "1")
                        {
                            if (dt_now > dt_dl)
                            {
                                e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                                e.Row.Cells[0].ForeColor = System.Drawing.Color.Blue;
                            }
                        }
                        if ((dt.Rows[0]["YS_CAIWU"].ToString() == "1" &&
                            dt.Rows[0]["YS_JISHU"].ToString() == "1" &&
                            dt.Rows[0]["YS_SHENGCHAN"].ToString() == "1" &&
                            dt.Rows[0]["YS_SHICHANG"].ToString() == "1") && dt.Rows[0]["YS_STATE"].ToString() == "")
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Sienna;
                        }
                    }
                    else
                    {
                        if (dt.Rows[0]["YS_CAIWU"].ToString() != "1")
                        {
                            if (dt_now > dt_dl)
                            {
                                e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                                e.Row.Cells[0].ForeColor = System.Drawing.Color.Blue;
                            }
                        }
                    }
                }
                else if (name == "邓朝晖" || name == "于建会")
                {
                    string sql_checkstate_l = "select YS_CAIWU,YS_JISHU,YS_SHENGCHAN,YS_SHICHANG from YS_COST_BUDGET where YS_CONTRACT_NO='" + lbl_CONTRACT_NO + "'";
                    System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_checkstate_l);
                    if (dt.Rows[0]["YS_CAIWU"].ToString() != "1" ||
                            dt.Rows[0]["YS_JISHU"].ToString() != "1" ||
                            dt.Rows[0]["YS_SHENGCHAN"].ToString() != "1" ||
                            dt.Rows[0]["YS_SHICHANG"].ToString() != "1")
                    {
                        if (dt_now > dt_dl)
                        {
                            e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                            e.Row.Cells[0].ForeColor = System.Drawing.Color.Blue;
                        }
                    }

                    for (int i = 5; i < 25; i++)
                    {
                        e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
                        e.Row.Cells[i].Attributes.Add("ondblclick", "PurMarView('" + CONTRACT_NO + "','" + i + "')");
                        e.Row.Cells[i].Attributes.Add("title", "双击查看明细");
                    }

                }
                else if (DEP == "06" && name == "张志东")
                {
                    for (int i = 6; i < 12; i++)
                    {
                        e.Row.Cells[i].Attributes["style"] = "Cursor:hand";
                        e.Row.Cells[i].Attributes.Add("ondblclick", "PurMarView('" + CONTRACT_NO + "','" + i + "')");
                        e.Row.Cells[i].Attributes.Add("title", "双击查看明细");
                    }
                }
                else if (DEP == "07" && name == "陈泽盛")
                {
                    e.Row.Cells[19].Attributes["style"] = "Cursor:hand";
                    e.Row.Cells[19].Attributes.Add("ondblclick", "PurMarView('" + CONTRACT_NO + "','19')");
                    e.Row.Cells[19].Attributes.Add("title", "双击查看明细");
                }
                #endregion

                //控制二次调整的查看是否可见
                HyperLink hp = ((HyperLink)e.Row.FindControl("Hp_View"));
                string sql_check = "select count(*) from YS_COST_BUDGET_FIRST where YS_CONTRACT_NO='" + lbl_CONTRACT_NO + "'";
                DataTable dt_check = DBCallCommon.GetDTUsingSqlText(sql_check);
                if (dt_check.Rows[0][0].ToString() == "0")
                {
                    hp.Visible = false;
                }
            }
        }

        /// <summary>
        /// 用于前端获取预算进度
        /// </summary>
        /// <param name="State"></param>
        /// <returns></returns>
        protected string GetState(string State)
        {
            string retValue = "";
            switch (State)
            {
                case "0":
                    retValue = "初始化"; break;
                case "1":
                    retValue = "提交未审批"; break;
                case "2":
                    retValue = "审批中"; break;
                case "3":
                    retValue = "已通过"; break;
                case "4":
                    retValue = "已驳回"; break;
                case "":
                    retValue = "未提交"; break;
                default:
                    break;
            }
            return retValue;
        }

        //删除，用于前端确定完善状态
        protected string GetEditState(string editstate)
        {
            string retValue = "";
            string sqltext = "select YS_SHENGCHAN,YS_JISHU,YS_SHICHANG,YS_CAIWU from YS_COST_BUDGET where YS_CONTRACT_NO='" + editstate + "'";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string shengchan = dt.Rows[0][0].ToString();
            string jishu = dt.Rows[0][1].ToString();
            string shichang = dt.Rows[0][2].ToString();
            string caiwu = dt.Rows[0][3].ToString();
            if ((shengchan == "0") && (jishu == "0") && (shichang == "0") && (caiwu == "0"))
            {
                retValue = "未完善";
            }
            else if ((shengchan == "1") && (jishu == "1") && (shichang == "1") && (caiwu == "1"))
            {
                retValue = "已完善";
            }
            else
            {
                retValue = "完善中";
            }
            return retValue;
        }

        /// <summary>
        /// 查询项目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddl_project_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            BindEngineer();
            UCPaging1.CurrentPage = 1;
            InitVar();
            GetTechRequireData();
        }


        /// <summary>
        /// 查询事件，更新where条件，重新访问一次数据库
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_search_OnClick(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            InitVar();
            GetTechRequireData();
        }

        /// <summary>
        /// /新增预算
        /// </summary>
        /// <param name="sender">事件发出的对象</param>
        /// <param name="e">事件传递的参数</param>
        protected void btnAddMar_OnClick(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "PurMarAdd();", true);
        }

        //待定，修改预算
        protected void btnModify_OnClick(object sender, EventArgs e)
        {
            string YS_CONTRACT_NO = "";

            foreach (GridViewRow grow in GridView1.Rows)
            {
                CheckBox ckb = (CheckBox)grow.FindControl("CheckBox1");
                if (ckb.Checked)
                {
                    Encrypt_Decrypt ed = new Encrypt_Decrypt();
                    YS_CONTRACT_NO = ed.EncryptText(((HiddenField)grow.FindControl("hdfMP_ID")).Value);
                    break;
                }
            }
            if (YS_CONTRACT_NO != "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "PurMarEdit_AddPer('" + YS_CONTRACT_NO + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要修改的行！！！');", true);
            }
        }


        //待定，删除预算
        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            int count = 0;
            string sqltext = string.Empty;
            List<string> listsql = new List<string>();
            foreach (GridViewRow grow in GridView1.Rows)
            {
                HiddenField hdfMP_ID = (HiddenField)grow.FindControl("hdfMP_ID");
                CheckBox ckb = (CheckBox)grow.FindControl("CheckBox1");
                if (ckb.Checked)
                {
                    count++;
                    listsql.Add("DELETE FROM YS_COST_BUDGET_ORIGINAL WHERE YS_CONTRACT_NO='" + hdfMP_ID.Value + "'");  //市场部原始指标
                    listsql.Add("DELETE FROM YS_COST_BUDGET WHERE YS_CONTRACT_NO='" + hdfMP_ID.Value + "'");          //预算主表
                    listsql.Add("DELETE FROM YS_COST_BUDGET_REV WHERE YS_CONTRACT_NO='" + hdfMP_ID.Value + "'");       //预算审核表
                    listsql.Add("DELETE FROM YS_COST_BUDGET_DETAIL WHERE YS_CONTRACT_NO='" + hdfMP_ID.Value + "'");     //预算细表
                    listsql.Add("DELETE FROM YS_COST_REAL WHERE YS_CONTRACT_NO='" + hdfMP_ID.Value + "'");              //实际费用表
                    listsql.Add("DELETE FROM YS_COST_REAL_DETAIL WHERE YS_CONTRACT_NO='" + hdfMP_ID.Value + "'");       //实际分摊费细表
                    listsql.Add("DELETE FROM YS_COST_REAL_OTHER WHERE YS_CONTRACT_NO='" + hdfMP_ID.Value + "'");        //实际分摊费主表
                    listsql.Add("DELETE FROM YS_COST_BUDGET_FIRST WHERE YS_CONTRACT_NO='" + hdfMP_ID.Value + "'");       //一次预算主表
                    listsql.Add("DELETE FROM YS_COST_BUDGET_FIRST_DETAIL WHERE YS_CONTRACT_NO='" + hdfMP_ID.Value + "'");   //一次预算细表

                }
            }

            if (listsql.Count > 0)
            {
                DBCallCommon.ExecuteTrans(listsql);
                InitVar();
                GetTechRequireData();

                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功，共删除【" + count.ToString() + "】条记录！！！');", true);
                return;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要删除的行！！！');", true);
                return;
            }
        }

        //删除
        protected void btn_orginal_OnClick(object sender, EventArgs e)
        {
            string YS_CONTRACT_NO = "";

            foreach (GridViewRow grow in GridView1.Rows)
            {
                CheckBox ckb = (CheckBox)grow.FindControl("CheckBox1");
                if (ckb.Checked)
                {
                    Encrypt_Decrypt ed = new Encrypt_Decrypt();
                    YS_CONTRACT_NO = ed.EncryptText(((HiddenField)grow.FindControl("hdfMP_ID")).Value);
                    break;
                }
            }
            if (YS_CONTRACT_NO != "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "PurView('" + YS_CONTRACT_NO + "');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要查看的行！！！');", true);
            }
        }
        
        //删除
        protected void btn_import_OnClick(object sender, EventArgs e)
        {
            string YS_CONTRACT_NO = "";
            Encrypt_Decrypt ed = new Encrypt_Decrypt();

            foreach (GridViewRow grow in GridView1.Rows)
            {
                CheckBox ckb = (CheckBox)grow.FindControl("CheckBox1");
                if (ckb.Checked)
                {
                    YS_CONTRACT_NO = ed.EncryptText(((HiddenField)grow.FindControl("hdfMP_ID")).Value);
                    break;
                }
            }
            if (YS_CONTRACT_NO != "")
            {
                string sql_check_dep = "";
                string dep_state = "";

                if (Session["UserDeptID"].ToString() == "04")
                {
                    sql_check_dep = "select YS_SHENGCHAN from YS_COST_BUDGET where YS_CONTRACT_NO='" + ed.DecryptText(YS_CONTRACT_NO) + "'";
                }
                else if (Session["UserDeptID"].ToString() == "12")
                {
                    sql_check_dep = "select YS_SHICHANG from YS_COST_BUDGET where YS_CONTRACT_NO='" + ed.DecryptText(YS_CONTRACT_NO) + "'";
                }

                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql_check_dep);
                if (dr.Read())
                {
                    dep_state = dr[0].ToString();
                }
                dr.Close();

                if (dep_state == "0" || dep_state == "3")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "PurImport('" + YS_CONTRACT_NO + "');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已提交部门审核，无法导入明细！！！');", true);
                    return;
                }

            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要导入的行！！！');", true);
            }
        }

        //删除
        protected void btn_YS_Modify_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string CONTRACT_NO = btn.CommandArgument.ToString();
            List<string> listsql = new List<string>();

            //删除备份表下的该合同数据
            listsql.Add("DELETE FROM YS_COST_BUDGET_FIRST WHERE YS_CONTRACT_NO='" + CONTRACT_NO + "'");
            listsql.Add("DELETE FROM YS_COST_BUDGET_FIRST_DETAIL WHERE YS_CONTRACT_NO='" + CONTRACT_NO + "'");

            //主表数据备份
            listsql.Add(" INSERT INTO YS_COST_BUDGET_FIRST ( [YS_CONTRACT_NO] ,[YS_BUDGET_INCOME] ,[YS_OUT_LAB_MAR] ,[YS_FERROUS_METAL] ," +
                "[YS_PURCHASE_PART] ,[YS_MACHINING_PART] ,[YS_PAINT_COATING] ,[YS_ELECTRICAL] ,[YS_OTHERMAT_COST] ,[YS_TEAM_CONTRACT] ,[YS_FAC_CONTRACT] ," +
                "[YS_PRODUCT_OUT] ,[YS_MANU_COST] ,[YS_SELL_COST] ,[YS_MANAGE_COST] ,[YS_Taxes_Cost] ,[YS_TRANS_COST] ,[YS_TOTALCOST_ALL] ," +
                "[YS_PROFIT] ,[YS_PROFIT_TAX] ,[YS_ADDNAME] ,[YS_ADDCODE] ,[YS_ADDTIME] ,[YS_NOTE])" +
                " SELECT  [YS_CONTRACT_NO] ,[YS_BUDGET_INCOME] ,[YS_OUT_LAB_MAR] ,[YS_FERROUS_METAL] ," +
                "[YS_PURCHASE_PART] ,[YS_MACHINING_PART] ,[YS_PAINT_COATING] ,[YS_ELECTRICAL] ,[YS_OTHERMAT_COST] ,[YS_TEAM_CONTRACT] ,[YS_FAC_CONTRACT] ," +
                "[YS_PRODUCT_OUT] ,[YS_MANU_COST] ,[YS_SELL_COST] ,[YS_MANAGE_COST] ,[YS_Taxes_Cost] ,[YS_TRANS_COST] ,[YS_TOTALCOST_ALL] ," +
                "[YS_PROFIT] ,[YS_PROFIT_TAX] ,[YS_ADDNAME] ,[YS_ADDCODE] ,[YS_ADDTIME] ,[YS_NOTE] " +
                "  FROM YS_COST_BUDGET  WHERE YS_CONTRACT_NO='" + CONTRACT_NO + "'");

            //细表数据备份
            listsql.Add("INSERT  INTO YS_COST_BUDGET_FIRST_DETAIL ( [YS_CONTRACT_NO] ,[YS_CODE] ,[YS_NAME] ,[YS_Average_Price] ," +
                "[YS_Union_Amount] ,[YS_MONEY_INPUT] ,[YS_MONEY] ,[YS_NOTE] ,[YS_FATHER] ,[YS_ADDPER] ,[YS_ADDTIME])" +
                " SELECT  [YS_CONTRACT_NO] ,[YS_CODE] ,[YS_NAME] ,[YS_Average_Price] ," +
                "[YS_Union_Amount] ,[YS_MONEY_INPUT] ,[YS_MONEY] ,[YS_NOTE] ,[YS_FATHER] ,[YS_ADDPER] ,[YS_ADDTIME]" +
                "  FROM  YS_COST_BUDGET_DETAIL WHERE YS_CONTRACT_NO='" + CONTRACT_NO + "'");

            //删除主表、细表、审核表以及市场部原始数据表下的该合同数据
            listsql.Add("DELETE FROM YS_COST_BUDGET WHERE YS_CONTRACT_NO='" + CONTRACT_NO + "'");
            listsql.Add("DELETE FROM YS_COST_BUDGET_DETAIL WHERE YS_CONTRACT_NO='" + CONTRACT_NO + "'");
            listsql.Add("DELETE FROM YS_COST_BUDGET_REV WHERE YS_CONTRACT_NO='" + CONTRACT_NO + "'");
            listsql.Add("DELETE FROM YS_COST_BUDGET_ORIGINAL WHERE YS_CONTRACT_NO='" + CONTRACT_NO + "'");
            DBCallCommon.ExecuteTrans(listsql);
            InitVar();
            GetTechRequireData();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('操作成功，请重新添加【" + CONTRACT_NO.ToString() + "】的合同预算，查看上一次预算的数据，请点“旧预算数据”！');", true);
            return;
        }
        
        //删除
        protected string Get_Old_YS(string contractno)
        {
            string sql_check = "select count(*) from YS_COST_BUDGET_FIRST where YS_CONTRACT_NO='" + contractno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql_check);
            if (dt.Rows[0][0].ToString() != "0")
            {
                string url = "";
                Encrypt_Decrypt ed = new Encrypt_Decrypt();
                url = "YS_Cost_Budget_Audit.aspx?action=" + ed.EncryptText("View_Old") + "&ContractNo=" + ed.EncryptText(contractno) + "&User=null";
                return url;
            }

            else
            {
                return string.Empty;
            }

        }
        
        //删除
        protected void btn_attachment_OnClick(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            string CONTRACT_NO = btn.CommandArgument.ToString();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "View_Attachment('" + CONTRACT_NO + "');", true);
        }
    }
}