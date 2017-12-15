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
using System.Text;
using System.Data.SqlClient;
using System.IO;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using NPOI.SS.UserModel;
using NPOI.HSSF.UserModel;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Otherpur_Bill_List : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            delete.Attributes.Add("OnClick", "Javascript:return confirm('是否确定删除?\\r会将整批都删除!\\r若要删除其中一部分则点击修改进入修改页面进行删除！');");
            if (!IsPostBack)
            {
                string sqltext = "select distinct DEP_NAME from TBDS_DEPINFO where DEP_CODE='" + Session["UserDeptID"].ToString() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                dplBM.SelectedValue = dt.Rows[0]["DEP_NAME"].ToString();
                initpageinfo();
                this.ShenHe();
                this.GetSqlText();
                InitVar();
                this.bindRepeater();
            }
            InitVar();
            CheckUser(ControlFinder);
        }
        private void initpageinfo()
        {            
            string sqlText = "select ST_NAME,ST_ID from TBDS_STAFFINFO WHERE ST_DEPID='" + Session["UserDeptID"].ToString() + "'";
            string dataText = "ST_NAME";
            string dataValue = "ST_ID";
            DBCallCommon.BindDdl(drp_stu, sqlText, dataText, dataValue);
            //drp_stu.SelectedValue = Session["UserID"].ToString();
            //部门
            string sqltext_bm = "select distinct DEP_NAME,DEP_CODE from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'order by DEP_CODE";
            DataTable dt_bm = DBCallCommon.GetDTUsingSqlText(sqltext_bm);
            dplBM.DataSource = dt_bm;
            dplBM.DataTextField = "DEP_NAME";
            dplBM.DataValueField = "DEP_NAME";
            dplBM.DataBind();
            dplBM.Items.Insert(0, new ListItem("全部", "%"));
        }
        protected void btn_search_click(object sender, EventArgs e)
        {
            this.ShenHe();
            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindRepeater();
        }
        protected void btn_search1_click(object sender, EventArgs e)
        {
            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindRepeater();
        }
           protected void tbpc_otherpurbill_list_Repeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
           {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HiddenField mxzt = (HiddenField)e.Item.FindControl("DETAILSTATE");
                if (mxzt.Value.ToString().Trim() == "是")
                {
                    ((HtmlTableRow)e.Item.FindControl("row")).BgColor = "Yellow";
                    ((HtmlTableRow)e.Item.FindControl("row")).Attributes.Add("title", "该条明细已被采购驳回!");
                }
                HtmlTableCell cellbedit = (HtmlTableCell)e.Item.FindControl("bedit");
                Label zdr = e.Item.FindControl("SUBMITID") as Label;
                Label spzt = e.Item.FindControl("MP_SPZT") as Label;
                HyperLink hyp_edit = e.Item.FindControl("hyp_edit") as HyperLink;
                string pcode = ((Label)e.Item.FindControl("PCODE")).Text;
                ((Label)e.Item.FindControl("PUR_DD")).ForeColor = System.Drawing.Color.Red;
                if (pcode.Contains("+"))
                {
                    pcode = pcode.Replace("+", "@");
                }
                ((HyperLink)e.Item.FindControl("HyperLink_lookup")).NavigateUrl = "PC_TBPC_Otherpur_Bill_look.aspx?action=view&mp_id=" + Server.UrlEncode(pcode) + "";
                if (rbl_xiatui.SelectedIndex == 0)
                {
                    if (cellbedit != null)
                    {
                        cellbedit.Visible = true;
                    }
                    //只有制单人本人，且未提交审核或已驳回时才允许修改
                    if (zdr.Text != Session["UserID"].ToString() || (spzt.Text.ToString() != "0" && spzt.Text.ToString() != "4"))
                    {
                        hyp_edit.Visible = false;
                    }
                    else
                    {
                        ((Label)e.Item.FindControl("Label1")).ForeColor = System.Drawing.Color.Red;
                        ((HyperLink)e.Item.FindControl("hyp_edit")).NavigateUrl = "PC_TBPC_Otherpur_Bill_edit.aspx?action=edit&mp_id=" + Server.UrlEncode(pcode) + "";
                    }
                }
                else
                {
                    if (cellbedit != null)
                    {
                        cellbedit.Visible = false;
                    }
                }

                string MP_IFFAST = ((Label)e.Item.FindControl("MP_IFFAST")).Text.Trim();
                if (MP_IFFAST == "1")
                {
                    ((Label)e.Item.FindControl("rownum")).ForeColor = System.Drawing.Color.Red;
                    ((Label)e.Item.FindControl("rownum")).ToolTip = "该物料为加急物料";
                }
            }
            if (e.Item.ItemType == ListItemType.Header)
            {
                HtmlTableCell cellhedit = (HtmlTableCell)e.Item.FindControl("hedit");
                if (rbl_xiatui.SelectedIndex == 0)
                {
                    if (cellhedit != null)
                    {
                        cellhedit.Visible = true;
                    }
                }
                else
                {
                    if (cellhedit != null)
                    {
                        cellhedit.Visible = false;
                    }
                }
                
            }
        }

        public string get_pr_state(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "未下推";
            }
            else if (i == "1")
            {
                state = "已下推";
            }
            return state;
        }

        public string get_spzt(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "初始化";
            }
            else if (i == "1")
            {
                state = "提交未审批";
            }
            else if (i == "2")
            {
                state = "审批中";
            }
            else if (i == "3")
            {
                state = "已通过";
            }
            else if (i == "4")
            {
                state = "已驳回";
            }
            return state;
        }

        protected void delete_Click(object sender, EventArgs e)
        {
            int temp=candelete();
            if(temp==0)
            {
                foreach (RepeaterItem Retem in tbpc_otherpurbill_list_Repeater.Items)
                {
                    CheckBox cbk = Retem.FindControl("CKBOX_SELECT") as CheckBox;
                    if (cbk.Checked)
                    {
                        string code = ((Label)Retem.FindControl("PCODE")).Text;
                        string sqltext = "delete from TBPC_OTPURRVW where MP_PCODE='" + code + "'";
                        string sqltext1 = "delete from TBPC_OTPURPLAN where MP_PCODE='" + code + "'";
                        string sqltext2 = "delete from TBPC_OTPUR_AUDIT where PA_CODE='" + code + "'";
                        DBCallCommon.ExeSqlText(sqltext);
                        DBCallCommon.ExeSqlText(sqltext1);
                        DBCallCommon.ExeSqlText(sqltext2);
                    }
                }               
                bindRepeater();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！');window.location.href=window.location.href;", true);
            }
            else if(temp==1)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('未选择数据！');", true);
            }
            else if(temp==2)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您不是制单人，无权删除！');", true);
            }
            else if (temp == 3)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('正在审核中，不能删除！');", true);
            } 
        }

         //判断能否删除
        private int candelete()
        {
            int temp = 0;
            int i = 0;//是否选择数据
            int j = 0;//制单是否为登录用户
            int k = 0;//是否已提交审批 
            string postid = "";
            string userid = Session["UserID"].ToString();
            foreach (RepeaterItem Reitem in tbpc_otherpurbill_list_Repeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        i++;
                        postid = ((Label)Reitem.FindControl("SUBMITID")).Text;
                        if (postid != userid)//登录人不是制单人
                        {
                            j++;
                            break;
                        }
                        Label spzt = Reitem.FindControl("MP_SPZT") as Label;
                        if (spzt.Text != "0" && spzt.Text != "4")  //已提交审批
                        {
                            k++;
                            break;
                        }
                    }
                }
            }
            if (i == 0)//未选择数据
            {
                temp = 1;
            }
            else if (j > 0)//登录人不是制单人
            {
                temp = 2;
            }
            else if (k > 0)//登录人不是制单人
            {
                temp = 3;
            }
            else
            {
                temp = 0;
            }
            return temp;
        } 
        
        protected void btnConfirm_OnClick(object sender, EventArgs e)
        {
            string leixing = "";
            string shape = "";
            switch (dplPSHTLB_Select.SelectedIndex)
            {
                case 0:
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择要添加的采购申请类别！！！');", true); return;
                case 1:
                    leixing = "ZC"; break;//正常
                //case 2:
                //    leixing = "YTJH"; break;//预提计划
                //case 3:
                //    leixing = "JSWX"; break;//技术外协
            }
            switch (DropDownList1.SelectedIndex)
            {
                case 0:
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择物料类别！！！');", true); return;
                case 1:
                    shape = "定尺板"; break;
                case 2:
                    shape = "非定尺板"; break;
                case 3:
                    shape = "型材"; break;
                case 4:
                    shape = "采"; break;
                case 5:
                    shape = "锻"; break;
                case 6:
                    shape = "铸"; break;
                case 7:
                    shape = "采购成品"; break;
                case 8:
                    shape = "非"; break;
                //case 9:
                //    shape = "电气电料"; break;
                case 9:
                    shape = "油漆"; break;
                case 10:
                    shape = "其它"; break;

            }
            Response.Redirect("PC_TBPC_Otherpur_Bill_edit.aspx?pId=&action=add&Type=" + leixing + "&shape=" + shape + "");
        }

        protected void btnBeiku_Click(object sender, EventArgs e)
        {
            string beikuadd = "BEIKU" + DateTime.Now.Year.ToString();
            List<string> listbeiku = new List<string>();
            string sqlvalid = "SELECT distinct  ISNULL(b.CM_CONTR,'') + '|' + ISNULL(a.TSA_ID,'')+ '|' + ISNULL(b.CM_PROJ,'')  as Expr1 FROM TBCM_Basic a, TBCM_Plan b WHERE a.ID=b.ID and a.TSA_ID ='" + beikuadd + "' ";
            DataTable dtvalid = DBCallCommon.GetDTUsingSqlText(sqlvalid);
            if (dtvalid.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('本年度备库已存在，请勿重复添加！');", true);
                return;
            }
            else
            {
                if (Session["POSITION"].ToString() == "0301" || Session["UserID"].ToString() == "67")
                {
                    string sqlbasic = "insert into TBCM_Basic(ID,TSA_ID,TSA_ENGNAME,TSA_NUMBER,TSA_SCSTATUS) values ('" + DateTime.Now.ToString("yyyyMMddHHmmss") + "','" + beikuadd + "','备库" + DateTime.Now.Year.ToString() + "','1','0')";
                    listbeiku.Add(sqlbasic);
                    string sqlplan = "insert into TBCM_PLAN(ID,CM_CONTR,CM_PROJ,CM_SPSTATUS,CM_PSJB,CM_TASKTYPE) values ('" + DateTime.Now.ToString("yyyyMMddHHmmss") + "','" + beikuadd + "','备库" + DateTime.Now.Year.ToString() + "','1','0','1')";
                    listbeiku.Add(sqlplan);
                    string sqltassgn = "insert into TBPM_TCTSASSGN(TSA_ID,TSA_PJID,TSA_ENGNAME,TSA_STATE,TSA_STATUS,TSA_NUMBER,TSA_REVIEWER,TSA_REVIEWERID) values ('" + beikuadd + "','" + beikuadd + "','备库" + DateTime.Now.Year.ToString() + "','9','0','1','陈永秀','69')";//"陈永秀1"
                    listbeiku.Add(sqltassgn);
                    try
                    {
                        DBCallCommon.ExecuteTrans(listbeiku);
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('添加成功！');window.location.href='PC_TBPC_Otherpur_Bill_List.aspx'", true);
                    }
                    catch
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('添加失败，请联系管理员！');", true);
                        return;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('您没有添加权限！');", true);
                    return;
                }
            }
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
            pager.TableName = "View_OTHER_PLAN_RVW";
            pager.PrimaryKey = "PCODE";
            pager.ShowFields = "PCODE AS PCODE,substring(TJDATE,1,11) AS SUBMITTM,DEPID AS USEDEPID,DEPNM AS USEDEPNAME,TJRID AS SUBMITID,TJRNM AS SUBMITNM,TOTALSTATE AS STATE,MP_SPZT AS SPZT,PJNM,ENGNM,MARID,MARNM,MARGG,MARCZ,MARGB,NUM,UNIT,(case DETAILSTATE when '1' then '是' else '否' end) as DETAILSTATE,DETAILNOTE,MP_IFFAST";
            pager.OrderField = "SUBMITTM";
            pager.StrWhere = ViewState["sqlText"].ToString();
            pager.OrderType = 1;//按时间降序排列
            pager.PageSize = 50;
            
        }
        void Pager_PageChanged(int pageNumber)
        {
            bindRepeater();
        }
        /// <summary>
        /// 绑定tbpc_otherpurbill_list_Repeater数据
        /// </summary>
        private void bindRepeater()
        {
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, tbpc_otherpurbill_list_Repeater, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();  //分页控件中要显示的控件  
            }
        }
        /// <summary>
        /// 获取查询条件
        /// </summary>
        private void GetSqlText()
        {
            StringBuilder sqltext = new StringBuilder();
            //是否下堆
            sqltext.Append(" TOTALSTATE='" + rbl_xiatui.SelectedValue.ToString() + "'");
            //审批状态
             if (rbl_shenhe.SelectedValue.ToString()!="5")
            {
             sqltext.Append("and MP_SPZT='"+rbl_shenhe.SelectedValue.ToString()+"' ");   
            }
            //申请人
            if (drp_stu.SelectedValue.ToString() != "-请选择-")
            {
                sqltext.Append(" and TJRID='" + drp_stu.SelectedValue.ToString() + "'");
            }
            if (dplBM.SelectedIndex != 0)
            {
                sqltext.Append(" AND DEPNM='" + dplBM.SelectedItem.Text + "'");
            }
            string startdate = tb_StartTime.Text.ToString() == "" ? "1900-01-01" : tb_StartTime.Text.ToString();
            string enddate = tb_EndTime.Text.ToString() == "" ? "2100-01-01" : tb_EndTime.Text.ToString();
            enddate = enddate + " 23:59:59";

            sqltext.Append(" and  (TJDATE>='" + startdate + "' AND TJDATE<='" + enddate + "') ");
            if (tb_orderno.Text.Trim() != "")
            {
                sqltext.Append(" and PCODE like '%" + tb_orderno.Text.Trim() + "%' ");
            }
            if(tb_name.Text.Trim()!="")
            {
                sqltext.Append("and MARNM like '%" + tb_name.Text.Trim() + "%' ");
            }

            if (tb_orderno.Text.Trim() != "")
            {
                sqltext.Append(" and PCODE like '%" + tb_orderno.Text.Trim() + "%' ");
            }
            
            if (DropDownList2.SelectedIndex != 0)
            {
                sqltext.Append(" and MP_SHAPE='" + DropDownList2.SelectedItem.Text + "'");
            }
            if (tb_cz.Text != "")
            {
                sqltext.Append(" and MARCZ like '%" + tb_cz.Text + "%'");
            }
            if (tb_gg.Text != "")
            {
                sqltext.Append(" and MARGG like '%" + tb_gg.Text + "%'");
            }
            if (tb_gb.Text != "")
            {
                sqltext.Append(" and MARGB like '%" + tb_gb.Text + "%'");
            }
           
            if (tb_eng.Text != "")
            {
                sqltext.Append(" and ENGNM like '%" + tb_eng.Text + "%'");
            }
            if (txtdetailnote.Text.Trim() != "")
            {
                sqltext.Append(" and DETAILNOTE like '%" + txtdetailnote.Text.Trim() + "%'");
            }
            ViewState["sqlText"] = sqltext.ToString();
        }
        private void ShenHe()
        {   
            int a = 0;//初始化
            int b = 0;//未审批
            int c = 0;//审批中
            int d = 0;//已驳回
            string startdate = tb_StartTime.Text.ToString() == "" ? "1900-01-01" : tb_StartTime.Text.ToString();
            string enddate = tb_EndTime.Text.ToString() == "" ? "2100-01-01" : tb_EndTime.Text.ToString();
            enddate = enddate + " 23:59:59";
            string sql = "";
            if (drp_stu.SelectedValue.ToString() == "-请选择-" && DropDownList2.SelectedIndex == 0)
            {
                sql = "";
            }
            if (drp_stu.SelectedValue.ToString() == "-请选择-" && DropDownList2.SelectedIndex != 0)
            {
                sql = "and MP_SHAPE='" + DropDownList2.SelectedItem.Text + "'";
            }
            if (drp_stu.SelectedValue.ToString() != "-请选择-" && DropDownList2.SelectedIndex == 0)
            {
                sql = "and TJRID='" + drp_stu.SelectedValue.ToString() + "'";
            }
            if (drp_stu.SelectedValue.ToString() != "-请选择-" && DropDownList2.SelectedIndex != 0)
            {
                sql = "and MP_SHAPE='" + DropDownList2.SelectedItem.Text + "'and TJRID='" + drp_stu.SelectedValue.ToString() + "'";
            }
            string sqltext1 = "select MP_SPZT from View_OTHER_PLAN_RVW where TOTALSTATE='" + rbl_xiatui.SelectedValue.ToString() + "'AND DEPNM='" + dplBM.SelectedItem.Text + "'and PCODE like '%" + tb_orderno.Text.Trim() + "%' and (TJDATE>='" + startdate + "' AND TJDATE<='" + enddate + "') and MARNM like '%" + tb_name.Text.Trim() + "%'"
                                + "and MARCZ like '%" + tb_cz.Text + "%'and MARGG like '%" + tb_gg.Text + "%'and MARGB like '%" + tb_gb.Text + "%' and ENGNM like '%" + tb_eng.Text + "%'"+sql.ToString()+"";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext1);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i]["MP_SPZT"].ToString() == "0")
                {
                    a++;
                }
                if (dt.Rows[i]["MP_SPZT"].ToString() == "1")
                {
                    b++;
                }
                if (dt.Rows[i]["MP_SPZT"].ToString() == "2")
                {
                    c++;
                }
                if (dt.Rows[i]["MP_SPZT"].ToString() == "4")
                {
                    d++;
                }
            }
            rbl_shenhe.Items.Clear();
            rbl_shenhe.Items.Add(new ListItem("全部", "5"));
            if (a != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("初始化" + "</label><label><font color=red>(" + a + ")</font>", "0"));
                rbl_shenhe.SelectedIndex = 1;
                btn_search1_click(null,null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("初始化", "0"));
            }
            if (b != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("未审批" + "</label><label><font color=red>(" + b + ")</font>", "1"));
                rbl_shenhe.SelectedIndex = 2;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("未审批", "1"));
            }
            if (c != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("审批中" + "</label><label><font color=red>(" + c + ")</font>", "2"));
                rbl_shenhe.SelectedIndex = 3;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("审批中", "2"));
            }
            rbl_shenhe.Items.Add(new ListItem("已通过", "3"));
            if (d != 0)
            {
                rbl_shenhe.Items.Add(new ListItem("已驳回" + "</label><label><font color=red>(" + d + ")</font>", "4"));
                rbl_shenhe.SelectedIndex = 5;
                btn_search1_click(null, null);
            }
            else
            {
                rbl_shenhe.Items.Add(new ListItem("已驳回", "4"));
            }
            rbl_shenhe.SelectedIndex = 0;
        }

        #endregion
        //重置条件
        protected void btnReset_Click(object sender, EventArgs e)
        {
            tb_orderno.Text = "";
            DropDownList2.SelectedIndex = 0;
            tb_StartTime.Text = "";
            tb_EndTime.Text = "";
            tb_name.Text = "";
            tb_cz.Text = "";
            tb_gg.Text = "";
            tb_gb.Text = "";
            
            tb_eng.Text = "";
            drp_stu.SelectedIndex = 0;
            QueryButton_Click(null,null);
        }
        //取消
        protected void btnClose_Click(object sender, EventArgs e)
        {
            ModalPopupExtenderSearch.Hide();
        }
        protected void QueryButton_Click(object sender, EventArgs e)
        {  
            this.ShenHe();
            this.GetSqlText();
            this.InitVar();
            UCPaging1.CurrentPage = 1;
            this.bindRepeater();
        }

        //导出
        protected void btndaochu_Click(object sender, EventArgs e)
        {
            string strwhere = ViewState["sqlText"].ToString().Trim();
            string sql = "select PCODE,MARID,MARNM,MARGG,MARCZ,MARGB,NUM,UNIT,substring(TJDATE,1,11) AS SUBMITTM,DEPNM,TJRNM,DETAILNOTE from View_OTHER_PLAN_RVW where " + strwhere + " order by SUBMITTM DESC";
            System.Data.DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            string filename = "新增采购申请导出.xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("新增采购申请导出模板.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);//创建workbook对象
                ISheet sheet1 = wk.GetSheetAt(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet1.CreateRow(i + 1);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        string str = dt.Rows[i][j].ToString();
                        row.CreateCell(j + 1).SetCellValue(str);
                    }
                }
                //for (int r = 0; r <= dt.Columns.Count; r++)
                //{
                //    sheet1.AutoSizeColumn(r);
                //}
                sheet1.ForceFormulaRecalculation = true;
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
    }
}
