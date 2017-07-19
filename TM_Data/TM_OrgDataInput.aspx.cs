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
using System.Text;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_OrgDataInput : System.Web.UI.Page
    {
        string sqlText;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.InitInfo();
                this.GetddlNameData();
                this.tsaid.DataBind();
            }

            if (IsPostBack)
            {
                this.InitVar();
            }
        }
        /// <summary>
        /// 初始化页面(基本信息)
        /// </summary>
        private void InitInfo()
        {
            string tsa_id = "";
            string[] fields;
            string sqlText = "";
            tsa_id = Request.QueryString["action"];
            fields = tsa_id.Split('-');
            sqlText = "select CM_PROJ,TSA_ENGNAME,TSA_ENGSTRTYPE,TSA_PJID ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + tsa_id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                tsaid.Text = tsa_id;

                proname.Text = dr[0].ToString();
                engname.Text = dr[1].ToString();
                eng_type.Value = dr[2].ToString();
                lblContract.Text = dr[3].ToString();
            }
            dr.Close();
            
            this.BindData();
        }
        
      
        /// <summary>
        /// 部件绑定
        /// </summary>
        private void GetddlNameData()
        {
           
            sqlText = "select BM_ZONGXU+'|'+BM_CHANAME AS BM_CHANAME,BM_ZONGXU from View_TM_DQO ";
            sqlText += "where BM_ENGID='" + tsaid.Text + "' and  (BM_MARID='' or BM_MARID is null) order by BM_CHANAME collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "BM_CHANAME";
            string dataValue = "BM_ZONGXU";
            DBCallCommon.BindAJAXCombox(ddlbjname, sqlText, dataText, dataValue);
        }
        /// <summary>
        /// 绑定材料名称(名称不为空)
        /// </summary>
        private void GetddlCLnameData()
        {

            sqlText = "select distinct BM_MANAME collate  Chinese_PRC_CS_AS_KS_WS as BM_MANAME from View_TM_DQO ";
            sqlText += "where BM_ENGID='" + tsaid.Text + "' and BM_MANAME is not null and ( BM_ZONGXU='"+ddlbjname.SelectedValue+"' or BM_ZONGXU like '" + ddlbjname.SelectedValue + ".%') order by BM_MANAME collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "BM_MANAME";
            string dataValue = "BM_MANAME";
            DBCallCommon.BindDdl(ddlname, sqlText, dataText, dataValue);
        }
        /// <summary>
        /// 材料规格
        /// </summary>
        private void GetGuigeData()
        {
           
            sqlText = "select distinct BM_GUIGE collate  Chinese_PRC_CS_AS_KS_WS as BM_GUIGE from View_TM_DQO ";
            sqlText += "where BM_ENGID='" + tsaid.Text + "' and BM_MAGUIGE is not null and (BM_ZONGXU='" + ddlbjname.SelectedValue + "' or  BM_ZONGXU like '" + ddlbjname.SelectedValue + ".%') and BM_MANAME='" + ddlname.SelectedValue + "' order by  BM_GUIGE collate  Chinese_PRC_CS_AS_KS_WS";
            string dataText = "BM_GUIGE";
            string dataValue = "BM_GUIGE";
            DBCallCommon.BindDdl(ddlguige, sqlText, dataText, dataValue);
        }
        /// <summary>
        /// 绑定数据
        /// </summary>
        private void BindData()
        {
            ViewState["sqlText"] = "BM_ENGID='" + tsaid.Text + "'";
            UCPaging1.CurrentPage = 1;
            pager_org.TableName = "View_TM_DQO";
            pager_org.PrimaryKey = "BM_ID";
            pager_org.ShowFields = "BM_ID,BM_XUHAO,BM_TUHAO,BM_MARID,BM_ZONGXU,BM_CHANAME,BM_NUMBER,BM_MAUNITWGHT,BM_MATOTALWGHT,BM_UNITWGHT,BM_TOTALWGHT,BM_GUIGE,BM_MAQUALITY,BM_MALENGTH,BM_MAWIDTH,BM_MATOTALLGTH,BM_MPSTATE,BM_MPSTATUS,BM_MSSTATE,BM_MSSTATUS,BM_OSSTATE,BM_OSSTATUS,BM_CONDICTIONATR,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar) +' | '+cast(BM_PNUMBER as varchar) AS NUMBER";
            pager_org.OrderField = "dbo.f_formatstr(" + ddlSort.SelectedValue + ",'.')";
            pager_org.StrWhere = ViewState["sqlText"].ToString();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 50;
            this.bindGrid();

        }
        protected void grv_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                HiddenField hdforgstate = (HiddenField)e.Row.FindControl("hdfOrgState");
                HiddenField hdmarid = (HiddenField)e.Row.FindControl("hdmarid");
                if (hdmarid.Value == "")
                {
                    e.Row.Cells[0].Attributes.Add("title", "蓝色标识为不含物料编码【部件】");
                    e.Row.Cells[0].BackColor = System.Drawing.Color.FromName("#B9D3EE");
                }

                //标签颜色
                string[] color = hdforgstate.Value.Split('-');//状态-变更状态
                //材料计划
                #region
                if (color[1].ToString() == "1")
                {
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Gray;
                    if (color[0].ToString() == "1")
                    {
                        e.Row.Cells[2].Text = "Y";
                        e.Row.Cells[2].Attributes.Add("title", "删除-材料计划已提交");
                    }
                    else
                    {
                        e.Row.Cells[2].Attributes.Add("title", "删除-材料计划未提交");
                    }
                }
                else if (color[1].ToString() == "2")
                {
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Orange;
                    if (color[0].ToString() == "1")
                    {
                        e.Row.Cells[2].Text = "Y";
                        e.Row.Cells[2].Attributes.Add("title", "增加-材料计划已提交");
                    }
                    else
                    {
                        e.Row.Cells[2].Attributes.Add("title", "增加-材料计划未提交");
                    }
                }
                else if (color[1].ToString() == "3")
                {
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Red;
                    if (color[0].ToString() == "1")
                    {
                        e.Row.Cells[2].Text = "Y";
                        e.Row.Cells[2].Attributes.Add("title", "修改-材料计划已提交");
                    }
                    else
                    {
                        e.Row.Cells[2].Attributes.Add("title", "修改-材料计划未提交");
                    }
                }
                else if (color[0].ToString() == "1")
                {
                    e.Row.Cells[2].BackColor = System.Drawing.Color.Green;
                    e.Row.Cells[2].Attributes.Add("title", "正常-材料计划已提交");
                }
                #endregion
                //制作明细
                #region
                if (color[3].ToString() == "1")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Gray;
                    if (color[2].ToString() == "1")
                    {
                        e.Row.Cells[3].Text = "Y";
                        e.Row.Cells[3].Attributes.Add("title", "删除-制作明细已提交");
                    }
                    else
                    {
                        e.Row.Cells[3].Attributes.Add("title", "删除-制作明细未提交");

                    }
                }
                else if (color[3].ToString() == "2")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Orange;
                    if (color[2].ToString() == "1")
                    {
                        e.Row.Cells[3].Text = "Y";
                        e.Row.Cells[3].Attributes.Add("title", "增加-制作明细已提交");
                    }
                    else
                    {
                        e.Row.Cells[3].Attributes.Add("title", "增加-制作明细未提交");

                    }
                }
                else if (color[3].ToString() == "3")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Red;
                    if (color[2].ToString() == "1")
                    {
                        e.Row.Cells[3].Text = "Y";
                        e.Row.Cells[3].Attributes.Add("title", "修改-制作明细已提交");
                    }
                    else
                    {
                        e.Row.Cells[3].Attributes.Add("title", "修改-制作明细未提交");
                    }
                }
                else if (color[2].ToString() == "1")
                {
                    e.Row.Cells[3].BackColor = System.Drawing.Color.Green;
                    e.Row.Cells[3].Attributes.Add("title", "正常-制作明细已提交");
                }
                #endregion
                //外协
                #region
                if (color[5].ToString() == "1")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Gray;
                    if (color[5].ToString() == "1")
                    {
                        e.Row.Cells[4].Text = "Y";
                        if (color[6].ToString() == "06")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "删除-外协(采购)已提交");
                        }
                        else if (color[6].ToString() == "03")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "删除-外协(技术)已提交");
                        }
                    }
                    else
                    {
                        if (color[6].ToString() == "06")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "删除-外协(采购)未提交");
                        }
                        else if (color[6].ToString() == "03")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "删除-外协(技术)未提交");
                        }
                    }
                }
                else if (color[5].ToString() == "2")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Orange;
                    if (color[5].ToString() == "1")
                    {
                        e.Row.Cells[4].Text = "Y";
                        if (color[6].ToString() == "06")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "增加-外协(采购)已提交");
                        }
                        else if (color[6].ToString() == "03")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "增加-外协(技术)已提交");
                        }
                    }
                    else
                    {
                        if (color[6].ToString() == "06")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "增加-外协(采购)未提交");
                        }
                        else if (color[6].ToString() == "03")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "增加-外协(技术)未提交");
                        }
                    }
                }
                else if (color[5].ToString() == "3")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Red;
                    if (color[5].ToString() == "1")
                    {
                        e.Row.Cells[4].Text = "Y";
                        if (color[6].ToString() == "06")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "修改-外协(采购)已提交");
                        }
                        else if (color[6].ToString() == "03")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "修改-外协(技术)已提交");
                        }
                    }
                    else
                    {
                        if (color[6].ToString() == "06")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "修改-外协(采购)未提交");
                        }
                        else if (color[6].ToString() == "03")
                        {
                            e.Row.Cells[4].Attributes.Add("title", "修改-外协(技术)未提交");
                        }
                    }
                }
                else if (color[4].ToString() == "1")
                {
                    e.Row.Cells[4].BackColor = System.Drawing.Color.Green;
                    if (color[6].ToString() == "06")
                    {
                        e.Row.Cells[4].Attributes.Add("title", "正常-外协(采购)已提交");
                    }
                    else if (color[6].ToString() == "03")
                    {
                        e.Row.Cells[4].Attributes.Add("title", "正常-外协(技术)已提交");
                    }
                }
                #endregion
            }
        }
        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnQuery_OnClick(object sender, EventArgs e)
        {
            ddlbjname.SelectedIndex = 0;
            ddlname.SelectedIndex = 0;
            ddlguige.SelectedIndex = 0;
            if (ddpQueryType.SelectedIndex == 0)
            {
                txtItem.Text = "";

                UCPaging1.CurrentPage = 1;

                this.InitVar();
                this.bindGrid();
            }
            else
            {
                if (txtItem.Text.Trim() != "")
                {
                    if(ddpQueryType.SelectedValue=="BM_ZONGXU"||ddpQueryType.SelectedValue=="BM_XUHAO")
                    {
                        ViewState["sqlText"] = "BM_ENGID='" + tsaid.Text + "' and (" + ddpQueryType.SelectedValue + "='"+txtItem.Text.Trim()+"' OR " + ddpQueryType.SelectedValue + " like '" + txtItem.Text.Trim() + ".%')";
                    }
                    else
                    {
                        ViewState["sqlText"] = "BM_ENGID='" + tsaid.Text + "' and " + ddpQueryType.SelectedValue + " like '%" + txtItem.Text.Trim() + "%'";
                    }
                    UCPaging1.CurrentPage = 1;
                    
                    this.InitVar();
                    this.bindGrid();
                }
                else
                {
                    ViewState["sqlText"] = "BM_ENGID='" + tsaid.Text + "' and " + ddpQueryType.SelectedValue + "='%" + txtItem.Text.Trim() + "%'";
                    UCPaging1.CurrentPage = 1;
                    this.InitVar();
                    this.bindGrid();
                }
            }
        }
        /// <summary>
        /// 部件查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlbjname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlbjname.SelectedIndex == 0)
            {
                ddlname.SelectedIndex = 0;
                ddlbjname.SelectedIndex = 0;
                ViewState["sqlText"] = "BM_ENGID='" + tsaid.Text + "'";
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
            else
            {
                this.GetddlCLnameData();
                ViewState["sqlText"] = "BM_ENGID='" + tsaid.Text + "' and (BM_ZONGXU='"+ddlbjname.SelectedValue+"' OR BM_ZONGXU like '" + ddlbjname.SelectedValue + ".%') ";
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
        }
        protected void ddlname_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlbjname.SelectedIndex == 0)
            {
                ddlguige.SelectedIndex = 0;
                ViewState["sqlText"] = "BM_ENGID='" + tsaid.Text + "' and (BM_ZONGXU='"+ddlbjname.SelectedValue+"' OR BM_ZONGXU like '" + ddlbjname.SelectedValue + ".%') ";
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
            else
            {
                this.GetGuigeData();
                ViewState["sqlText"] = "BM_ENGID='" + tsaid.Text + "' and (BM_ZONGXU='"+ddlbjname.SelectedValue+"' or BM_ZONGXU like '" + ddlbjname.SelectedValue + ".%') and BM_MANAME='" + ddlname.SelectedValue + "' ";
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
        }
        protected void ddlguige_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlguige.SelectedIndex == 0)
            {
                ViewState["sqlText"] = "BM_ENGID='" + tsaid.Text + "' and (BM_ZONGXU='"+ddlbjname.SelectedValue+"' OR BM_ZONGXU like '" + ddlbjname.SelectedValue + ".%') and BM_MANAME='" + ddlname.SelectedValue + "' ";
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
            else
            {
                ViewState["sqlText"] = "BM_ENGID='" + tsaid.Text + "' and (BM_ZONGXU='"+ddlbjname.SelectedValue+"' OR BM_ZONGXU like '" + ddlbjname.SelectedValue + ".%') and BM_MANAME='" + ddlname.SelectedValue + "' and BM_GUIGE='" + ddlguige.SelectedValue + "' ";
                UCPaging1.CurrentPage = 1;
                this.InitVar();
                this.bindGrid();
            }
        }

        protected void ddlOrgjishu_SelectedIndexChanged(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            this.InitVar();
            this.bindGrid();
        }
        /// <summary>
        ///查询条件清空
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnMSClear_OnClick(object sender, EventArgs e)
        {
            UserDefinedQueryConditions.UserDefinedExternalCallForInitControl((GridView)udqMS.FindControl("GridView1"));
        }
        /// <summary>
        /// 连选
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelect_OnClick(object sender, EventArgs e)
        {
            CheckBoxSelectDefine(grv, "CheckBox1");
        }

        /// <summary>
        /// CheckBox连选(此函数勿动)
        /// </summary>
        /// <param name="smartgridview"></param>
        /// <param name="ckbname"></param>
        public void CheckBoxSelectDefine(YYControls.SmartGridView smartgridview, string  ckbname)
        {
            int startindex=-1;
            int endindex=-1;
            for (int i = 0; i < smartgridview.Rows.Count; i++)
            {
                CheckBox cbx = (CheckBox)smartgridview.Rows[i].FindControl(ckbname);
                if (cbx.Checked)
                {
                    startindex = i;
                    break;
                }
            }

            for (int j = smartgridview.Rows.Count-1; j >-1; j--)
            {
                CheckBox cbx = (CheckBox)smartgridview.Rows[j].FindControl(ckbname);
                if (cbx.Checked)
                {
                    endindex = j;
                    break;
                }
            }

            if (startindex<0||endindex<0|| startindex == endindex)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('需要勾选2条记录！！！');", true);
            }
            else
            {
                for (int k = startindex; k <= endindex; k++)
                {
                    CheckBox cbx = (CheckBox)smartgridview.Rows[k].FindControl(ckbname);
                    cbx.Checked = true;
                }
            }
        }

        #region  分页  UCPaging

        PagerQueryParam pager_org = new PagerQueryParam();

        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager_org.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager_org.TableName = "View_TM_DQO";
            pager_org.PrimaryKey = "BM_ID";
            pager_org.ShowFields = "BM_ID,BM_XUHAO,BM_TUHAO,BM_MARID,BM_ZONGXU,BM_CHANAME,BM_NUMBER,BM_MAUNITWGHT,BM_MATOTALWGHT,BM_UNITWGHT,BM_TOTALWGHT,BM_GUIGE,BM_MAQUALITY,BM_MALENGTH,BM_MAWIDTH,BM_MATOTALLGTH,BM_MPSTATE,BM_MPSTATUS,BM_MSSTATE,BM_MSSTATUS,BM_OSSTATE,BM_OSSTATUS,BM_CONDICTIONATR,cast(BM_SINGNUMBER as varchar)+' | '+cast(BM_NUMBER as varchar)+' | '+cast(BM_PNUMBER as varchar) AS NUMBER";
            pager_org.OrderField = "dbo.f_formatstr(" + ddlSort.SelectedValue + ",'.')";
            pager_org.StrWhere = this.Get_StrWhere();
            pager_org.OrderType = 0;//升序排列
            pager_org.PageSize = 50;
        }

        private string Get_StrWhere()
        {
            string returnValue = ViewState["sqlText"].ToString();

            //显示级数
            if (ddlOrgJishu.SelectedIndex != 0)
            {
                returnValue+=" AND [dbo].[Splitnum]("+ddlShowType.SelectedValue+",'.')=" + ddlOrgJishu.SelectedValue + " ";
            }
            udqMS.ExistedConditions = returnValue;
            returnValue = returnValue + UserDefinedQueryConditions.ReturnQueryString((GridView)udqMS.FindControl("GridView1"), (Label)udqMS.FindControl("Label1"));
            return returnValue;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager_org.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager_org);
            CommonFun.Paging(dt, grv, UCPaging1, NoDataPanel);
            if ( NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }

        }
        #endregion
    }
}
