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
using System.Drawing;
using System.IO;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.Contract_Data
{
    public partial class CM_ContractView : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!IsPostBack)
            {
                this.bind_ddlZDR();
                this.Btn_Query_click(null, null);
            }
            this.InitVar();
            CheckUser(ControlFinder);
        }
        #region "数据查询，分页"
        /// <summary>
        /// 初始化分布信息
        /// </summary>
        private void InitVar()
        {
            InitPager();
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            UCPaging1.PageSize = pager.PageSize;    //每页显示的记录数
        }
        /// <summary>
        /// 分页初始化
        /// </summary>
        private void InitPager()
        {
            pager.TableName = "(View_TBCR_CONTRACTREVIEW_ALL as a left join (select distinct ID as ID1 ,CM_XM as CM_XM1,CM_ZJ as CM_JLL from TBPM_HESUAN where CM_XM='净利率') as b on a.CR_ID=b.ID1 left join (select distinct ID as ID2,CM_XM as CM_XM2,CM_ZJ as CM_JLR from TBPM_HESUAN where CM_XM='净利润')as c on a.CR_ID=c.ID2)";
            pager.PrimaryKey = "CR_ID";
            pager.ShowFields = "";
            pager.OrderField = "CR_ZDRQ";
            pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 1;//按时间升序序排列
            pager.PageSize = 10;
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindGrid();
        }
        private void bindGrid()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, grvSP, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件
            }
            //CheckUser(ControlFinder);
        }
        #endregion


        //添加评审合同信息
        protected void btnConfirm_OnClick(object sender, EventArgs e)
        {
            string path = "";
            if (dplPSHTLB_Select.SelectedValue == "%")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要添加的评审合同类别！！！');", true); return;
            }
            else
            {
                if (dplPSHTLB_Select.SelectedValue.ToString() == "6")
                {
                    path = "CM_ContractView_Other_Add.aspx";

                }
                else
                {
                    path = "CM_ContractView_Add.aspx";

                }
                Response.Redirect("" + path + "?Action=add&Type=" + dplPSHTLB_Select.SelectedValue.ToString());
            }
        }

        protected void grvSP_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex >= 0)
            {
                e.Row.Attributes.Add("onclick", "RowClick(this)");

                e.Row.Attributes["style"] = "Cursor:pointer";

                Label pszt = (Label)e.Row.FindControl("lbl_pszt");
                string lb_pszt = pszt.Text.ToString();
                if (lb_pszt == "已驳回")
                {
                    e.Row.Cells[0].BackColor = System.Drawing.Color.Red;
                }
                //只有制单人有编辑权限
                Label lblzdr = (Label)e.Row.FindControl("lblZDR");
                if (lblzdr.Text != Session["UserID"].ToString())
                {
                    e.Row.FindControl("Lbtn_Edit").Visible = false;
                    e.Row.FindControl("Lbtn_Del").Visible = false;
                }

                string ercips = ((Label)e.Row.FindControl("lblERCIPS")).Text;
                if (ercips == "1")
                {

                    e.Row.Cells[0].Attributes.Add("title", "该记录为二次评审");
                    e.Row.Cells[0].BackColor = Color.Green;
                }
            }
        }

        /// <summary>
        /// 编辑、查看
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void lnkAction_OnClick(object sender, EventArgs e)
        {
            string cr_id = ((LinkButton)sender).CommandArgument.ToString();
            string path = "";
            string type = "";
            string action = "";
            string[] split = ((LinkButton)sender).CommandName.ToString().Split('|');
            type = split[0];
            action = split[1];
            if (action == "edit")
            {
                if (type == "6")
                {
                    path = "CM_ContractView_Other_Add.aspx";
                }
                else
                {
                    path = "CM_ContractView_Add.aspx";
                }
            }
            else
            {
                path = "CM_ContractView_Audit.aspx";
            }
            Response.Redirect("" + path + "?Action=" + action + "&ID=" + cr_id + "&Type=" + type + "&Win=self");

        }

        private void bind_ddlZDR()
        {
            string sqlzdr = "select distinct ST_NAME,ST_ID FROM TBDS_STAFFINFO a,TBCR_CONTRACTREVIEW b WHERE a.ST_ID=b.CR_ZDR";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlzdr);
            if (dt.Rows.Count > 0)
            {
                ddlZDR.DataSource = dt;
                ddlZDR.DataTextField = "ST_NAME";
                ddlZDR.DataValueField = "ST_ID";
                ddlZDR.DataBind();
                ddlZDR.Items.Insert(0, new ListItem("-请选择-", "%"));

                //如果登录人在制单人列表中，则默认选中此人，否则选择第一项即全部显示               
                for (int i = 0; i < ddlZDR.Items.Count; i++)
                {
                    if (ddlZDR.Items[i].Value == Session["UserID"].ToString())
                    {
                        ddlZDR.Items[i].Selected = true;
                        break;
                    }
                }

            }
            else
            {
                ddlZDR.DataSource = null;
                ddlZDR.Items.Add(new ListItem("-请选择-", "-请选择-"));
            }
        }

        //其他条件查询
        protected void Btn_Query_click(object sender, EventArgs e)
        {
            string xmmc = txt_xmmc.Text.Trim();
            string startdate = txtStartTime.Text.Trim() == "" ? "1900-01-01" : txtStartTime.Text.Trim();
            string enddate = txtEndTime.Text.Trim() == "" ? "2100-01-01" : txtEndTime.Text.Trim();
            ViewState["sqlText"] = "CR_PSZT like '" + rblZT.SelectedValue.ToString() + "' and CR_HTLX like '" + dplHTLB.SelectedValue.ToString() + "'" +
                " and CR_XMMC like '%" + xmmc + "%' and (CR_ZDRQ between '" + startdate + "' and '" + enddate + "') and CR_ZDR like '" + ddlZDR.SelectedValue.ToString() + "'";
            if (txt_htbh.Text.Trim() != "")
            {
                ViewState["sqlText"] += "and PCON_BCODE like '%" + txt_htbh.Text.Trim() + "%'";
            }

            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }

        //重置
        protected void Btn_Reset_click(object sender, EventArgs e)
        {
            txt_xmmc.Text = "";
            txtStartTime.Text = "";
            txtEndTime.Text = "";
            rblZT.SelectedIndex = 0;
            dplHTLB.SelectedIndex = 0;
            txt_htbh.Text = "";
            ViewState["sqlText"] = "";
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindGrid();
        }

        //删除该合同审批
        protected void Lbtn_Del_OnClick(object sender, EventArgs e)
        {
            List<string> sqlstr = new List<string>();
            string cr_id = ((LinkButton)sender).CommandArgument.ToString();
            string htlx = ((LinkButton)sender).CommandName.ToString();

            //判断是主合同审批还是补充协议审批
            if (htlx != "8")
            {

                //删除前先判断合同是否已付款，如果已付款，不允许删除
                string sql_YFK = "select PCON_YFK,PCON_BCODE from TBPM_CONPCHSINFO where PCON_REVID='" + cr_id + "'";
                DataTable dtYFK = DBCallCommon.GetDTUsingSqlText(sql_YFK);
                if (dtYFK.Rows.Count > 0)
                {
                    if (Convert.ToDouble(dtYFK.Rows[0]["PCON_YFK"]) > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('该合同已付款金额不为0，不能删除！');", true); return;
                    }


                    string sql_Del_Info = "delete from TBCR_CONTRACTREVIEW where CR_ID='" + cr_id + "'";//删除合同评审主表信息
                    string sql_Del_Rev = "delete from TBCR_CONTRACTREVIEW_DETAIL  where CRD_ID='" + cr_id + "'";//删除合同评审意见表信息
                    //string sql_Del_ConInfo = "delete from TBPM_CONPCHSINFO where PCON_REVID='" + cr_id + "'";//删除合同登记信息
                    string sql_Del_Conno = "delete from TBCM_TEMPCONNO where CON_NO='" + dtYFK.Rows[0]["PCON_BCODE"] + "' and USER_NAME='" + Session["UserName"].ToString() + "'";//删除已锁定的合同号

                    //删除附件及附件信息
                    Contract_Data.ContractClass.Del_ConAttachment(dtYFK.Rows[0]["PCON_BCODE"].ToString());
                    Contract_Data.ContractClass.Del_SPAttachment(dtYFK.Rows[0]["PCON_BCODE"].ToString());

                    sqlstr.Add(sql_Del_Info);
                    sqlstr.Add(sql_Del_Rev);
                    //sqlstr.Add(sql_Del_ConInfo);
                    sqlstr.Add(sql_Del_Conno);
                    sqlstr.Add("delete from TBPM_CONTRACTPS where ID='" + cr_id + "'");
                    sqlstr.Add("delete from TBPM_HESUAN where ID='" + cr_id + "'");
                    DBCallCommon.ExecuteTrans(sqlstr);
                    Response.Redirect("CM_ContractView.aspx");
                }
            }
            else
            {
                string sql_Del_Info = "delete from TBCM_ADDCON where CR_ID='" + cr_id + "'";//删除合同评审主表信息
                string sql_Del_Rev = "delete from TBCR_CONTRACTREVIEW_DETAIL  where CRD_ID='" + cr_id + "'";//删除合同评审意见表信息


                //删除附件及附件信息
                Contract_Data.ContractClass.Del_RevAttachment(cr_id);

                sqlstr.Add(sql_Del_Info);
                sqlstr.Add(sql_Del_Rev);
                DBCallCommon.ExecuteTrans(sqlstr);
                Response.Redirect("CM_ContractView.aspx");

            }

        }

        //导出
        protected void btndaochu_click(object sender, EventArgs e)
        {
            string sqltext = "";
            sqltext = "select * from TBPM_CONTRACTPS as a left join View_CM_CONTRACT_REV_INFO_ALL as b on a.ID=b.CR_ID where b.CR_ID is not null";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            string filename = "合同审批目录" + DateTime.Now.ToString("yyyyMMdd") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //1.读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("合同评审目录.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet0 = wk.GetSheetAt(0);//创建第一个sheet


                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    IRow row = sheet0.CreateRow(i + 1);
                    row.CreateCell(0).SetCellValue(Convert.ToString(i + 1));
                    row.CreateCell(1).SetCellValue("" + dt.Rows[i]["CM_CUSNAME"].ToString());
                    row.CreateCell(2).SetCellValue("" + dt.Rows[i]["PCON_BCODE"].ToString());
                    row.CreateCell(3).SetCellValue("" + dt.Rows[i]["PCON_ENGNAME"].ToString());
                    row.CreateCell(4).SetCellValue("" + dt.Rows[i]["CM_MAP"].ToString());
                    row.CreateCell(5).SetCellValue("" + dt.Rows[i]["CM_EQUIP"].ToString());
                    row.CreateCell(6).SetCellValue("" + dt.Rows[i]["CM_PSTIME"].ToString());
                    row.CreateCell(7).SetCellValue("" + dt.Rows[i]["PCON_NOTE"].ToString());

                }
                for (int i = 0; i <= dt.Columns.Count; i++)
                {
                    sheet0.AutoSizeColumn(i);
                }

                sheet0.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }

        }


    }
}
