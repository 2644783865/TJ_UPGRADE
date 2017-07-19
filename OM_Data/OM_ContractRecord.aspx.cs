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
using NPOI.SS.UserModel;
using System.IO;
using NPOI.HSSF.UserModel;
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_ContractRecord : BasicPage
    {
        PagerQueryParam pager = new PagerQueryParam();
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Title = "合同签订记录";
            UCPaging1.PageChanged += new UCPaging.PageHandler(Pager_PageChanged);
            if (!IsPostBack)
            {
                GetDepartment();//绑定部门下拉框
                GetContract();//合同主体下拉框
                databind();//人员信息表绑定
            }
            CheckUser(ControlFinder);
        }

        #region =======================查询=========================

        protected void Query(object sender, EventArgs e)
        {
            UCPaging1.CurrentPage = 1;
            databind();
        }
        #endregion

        #region 初始化分页
        void Pager_PageChanged(int pageNumber)
        {
            databind();
        }

        private void databind()
        {
            pager.TableName = "(select a.*,b.ST_PD,b.ST_DEPID from OM_ContractRecord as a left join TBDS_STAFFINFO as b on a.C_STID=b.ST_ID)t";
            pager.PrimaryKey = "";
            pager.ShowFields = "*";
            pager.OrderField = "C_STID";
            pager.StrWhere = StrWhere();
            pager.OrderType = 0;
            pager.PageSize = int.Parse(ddl_pageno.SelectedValue);
            UCPaging1.PageSize = pager.PageSize;
            pager.PageIndex = UCPaging1.CurrentPage;
            DataTable dt = CommonFun.GetDataByPagerQueryParam(pager);
            CommonFun.Paging(dt, SmartGridView1, UCPaging1, NoDataPanel);
            if (NoDataPanel.Visible)
            {
                UCPaging1.Visible = false;//如果筛选结果没有，则UCPaging不显示
            }
            else
            {
                UCPaging1.Visible = true;
                UCPaging1.InitPageInfo();
            }
            for (int i = 0; i < SmartGridView1.Rows.Count; i++)
            {
                Label s = (Label)SmartGridView1.Rows[i].FindControl("lbXuhao");
                s.Text = (i + 1 + (pager.PageIndex - 1) * UCPaging1.PageSize).ToString();
                Label lbTimes = (Label)SmartGridView1.Rows[i].FindControl("lbTimes");
                for (int j = 5; j < dt.Columns.Count - 12; j++)
                {
                    string columntext = dt.Rows[i][j].ToString();
                    if (!string.IsNullOrEmpty(columntext))
                    {
                        if (j == dt.Columns.Count - 13)
                        {
                            SmartGridView1.Rows[i].Cells[5].Text = dt.Rows[i][j].ToString();
                            if ((SmartGridView1.Rows[i].Cells[5].Text.Length > 17) && (SmartGridView1.Rows[i].Cells[5].Text.Substring(11, 7) == DateTime.Now.ToString("yyyy.MM")))
                            {
                                SmartGridView1.Rows[i].Cells[3].BackColor = System.Drawing.Color.Red;
                                SmartGridView1.Rows[i].Cells[3].ToolTip = "合同本月到期";
                                SmartGridView1.Rows[i].Cells[1].ForeColor = System.Drawing.Color.Red;
                            }
                            lbTimes.Text = "30";
                        }
                        else
                            continue;
                    }
                    else
                    {
                        if (j > 5)
                        {
                            SmartGridView1.Rows[i].Cells[5].Text = dt.Rows[i][j - 1].ToString();
                            if ((!string.IsNullOrEmpty(SmartGridView1.Rows[i].Cells[5].Text.ToString())) && (SmartGridView1.Rows[i].Cells[5].Text.ToString().Length > 17) && (SmartGridView1.Rows[i].Cells[5].Text.Substring(11, 7) == DateTime.Now.ToString("yyyy.MM")))
                            {
                                SmartGridView1.Rows[i].Cells[3].BackColor = System.Drawing.Color.Red;
                                SmartGridView1.Rows[i].Cells[3].ToolTip = "本月合同到期";
                                SmartGridView1.Rows[i].Cells[1].ForeColor = System.Drawing.Color.Red;
                            }
                        }
                        else
                            SmartGridView1.Rows[i].Cells[5].Text = "";
                        lbTimes.Text = (j - 5).ToString();
                        break;
                    }
                }
            }
            string sql = "select a.C_STID  from OM_ContractRecord as a left join TBDS_STAFFINFO as b on a.C_STID=b.ST_ID where " + StrWhere();
            lbPeople.Text = DBCallCommon.GetDTUsingSqlText(sql).Rows.Count.ToString();//筛选人数
            CellsMerge(SmartGridView1, 1);
        }

        private string StrWhere()
        {
            string strWhere = "1=1";
            if (rblStaffStatus.Text != "全部")
            {
                strWhere += " and  ST_PD='" + rblStaffStatus.SelectedValue.ToString() + "'";
            }
            if (ddlPartment.SelectedValue != "00")
            {
                strWhere += "and ST_DEPID ='" + ddlPartment.SelectedValue + "'";
            }
            if (ddlContract.SelectedValue != "00")
            {
                strWhere += "and C_STContract ='" + ddlContract.SelectedValue + "' ";
            }
            if (txtName.Text.ToString().Trim() != "")
            {
                strWhere += " and C_STName like '%" + txtName.Text.ToString().Trim() + "%'";
            }
            return strWhere;
        }
        #endregion

        #region ===RowDataBound改变颜色等===

        protected void SmartGridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                DataRowView dr = (DataRowView)e.Row.DataItem;
                string STID = ((Label)e.Row.FindControl("lbC_STID")).Text.ToString();
                string STNAME = e.Row.Cells[1].Text.ToString();
                string CSTContract = e.Row.Cells[3].Text.ToString();
                e.Row.Attributes.Add("onmouseover", "this.style.backgroundColor='#E6F5FA'");
                e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor='#ffffff'");
                e.Row.Attributes.Add("title", "双击查看详情");
                e.Row.Attributes.Add("ondblclick", "javascript:window.showModalDialog('OM_ContractEditDialog.aspx?action=view&ST_NAME=" + STNAME + "&ST_ID=" + STID + "&C_STContract=" + CSTContract + "','','DialogWidth=475px;DialogHeight=600px')");
                //e.Row.Cells[5].ToolTip = dr["C_STName"] + "-" + dr["C_STDep"] + "-" + dr["C_STContract"] + "-最新合同";
                if (e.Row.RowIndex != -1)
                {
                    e.Row.Cells[6].Text = SubStr(dr["C_EditNote"].ToString(), 30);
                    e.Row.Cells[6].ToolTip = dr["C_EditNote"].ToString();
                }
            }
        }

        public string SubStr(string sString, int nLeng)
        {
            if (sString.Length <= nLeng)
            {
                return sString;
            }
            string sNewStr = sString.Substring(0, nLeng);
            sNewStr = sNewStr + "...";
            return sNewStr;
        }
        #endregion

        private void GetDepartment()//绑定部门
        {
            string sqlText = "select distinct DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE LIKE '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlPartment.DataSource = dt;
            ddlPartment.DataTextField = "DEP_NAME";
            ddlPartment.DataValueField = "DEP_CODE";
            ddlPartment.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddlPartment.Items.Insert(0, item);
            ddlPartment.SelectedValue = "00";
        }

        private void GetContract()//绑定合同主体
        {
            string sqlText = "select distinct C_STContract from OM_ContractRecord";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            ddlContract.DataTextField = "C_STContract";
            ddlContract.DataValueField = "C_STContract";
            ddlContract.DataSource = dt;

            ddlContract.DataBind();
            ListItem item = new ListItem();
            item.Text = "全部";
            item.Value = "00";
            ddlContract.Items.Insert(0, item);
            ddlContract.SelectedValue = "00";
        }

        protected string edit(string STID, string STNAME, string CSTContract)//修改记录弹窗
        {
            return "javascript:window.showModalDialog('OM_ContractEditDialog.aspx?action=edit&ST_NAME=" + STNAME + "&ST_ID=" + STID + "&C_STContract=" + CSTContract + "','','DialogWidth=475px;DialogHeight=600px')";
        }

        #region 导入功能
        protected void btnImport_Click(object sender, EventArgs e) //导出
        {
            List<string> list = new List<string>();
            string FilePath = @"E:\合同签订记录\";
            if (!Directory.Exists(FilePath))
            {
                Directory.CreateDirectory(FilePath);
            }
            //将文件上传到服务器
            HttpPostedFile UserHPF = FileUpload1.PostedFile;
            try
            {
                string fileContentType = UserHPF.ContentType;// 获取客户端发送的文件的 MIME 内容类型   
                if (fileContentType == "application/vnd.ms-excel")
                {
                    if (UserHPF.ContentLength > 0)
                    {
                        UserHPF.SaveAs(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName));//将上传的文件存放在指定的文件夹中 
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件类型不符合要求，请您核对后重新上传！');", true);
                    return;
                }
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('文件上传过程中出现错误！');", true);
                return;
            }
            using (FileStream fs = File.OpenRead(FilePath + "//" + System.IO.Path.GetFileName(UserHPF.FileName)))
            {
                //根据文件流创建一个workbook
                IWorkbook wk = new HSSFWorkbook(fs);
                //获取第一个工作表
                ISheet sheet0 = wk.GetSheetAt(0);
                string namecheck = "";
                string contractcheck = "";
                string sqlcontractcheck = "";
                DataTable dtcontractcheck;
                string sqlnamecheck = "";
                DataTable dtnamecheck;
                string stidcheck = "";
                string content = "";
                string sqlinsert = "";
                string key = "";
                string value = "";
                for (int i = 2; i < sheet0.LastRowNum + 1; i++)
                {
                    IRow rowi = sheet0.GetRow(i);
                    ICell cell1 = rowi.GetCell(1);
                    ICell cell3 = rowi.GetCell(3);
                    try
                    {
                        namecheck = cell1.ToString().Trim();
                    }
                    catch
                    {
                        namecheck = "";
                    }
                    try
                    {
                        contractcheck = cell3.ToString().Trim();
                    }
                    catch
                    {
                        contractcheck = "";
                    }
                    if (namecheck != "")
                    {
                        sqlnamecheck = "select ST_ID from TBDS_STAFFINFO where ST_NAME='" + namecheck + "'";
                        dtnamecheck = DBCallCommon.GetDTUsingSqlText(sqlnamecheck);
                        if (dtnamecheck.Rows.Count > 0)
                        {
                            stidcheck = dtnamecheck.Rows[0]["ST_ID"].ToString().Trim();
                            sqlcontractcheck = "select C_STID from OM_ContractRecord where C_STID='" + stidcheck + "'and C_STContract='" + contractcheck + "'";
                            dtcontractcheck = DBCallCommon.GetDTUsingSqlText(sqlcontractcheck);
                            if (dtcontractcheck.Rows.Count > 0)
                            {
                                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('【" + namecheck + "】的【" + contractcheck + "】合同签订信息已存在，请修改上传文件！');", true);
                                return;
                            }
                            else
                            {
                                key = "C_STID,C_STName,C_STDep, C_STContract,C_Contract1,C_Contract2,C_Contract3,C_Contract4,C_Contract5,C_Contract6,C_Contract7,C_Contract8,C_Contract9,C_Contract10,C_Contract11,C_Contract12,C_Contract13,C_Contract14,C_Contract15,C_Contract16,C_Contract17,C_Contract18,C_Contract19,C_Contract20,C_Contract21,C_Contract22,C_Contract23,C_Contract24,C_Contract25,C_Contract26,C_Contract27,C_Contract28,C_Contract29,C_Contract30,C_ImportPersonID,C_ImportPerson,C_ImportTime";
                                value = stidcheck;
                                for (int j = 1; j < 34; j++)
                                {
                                    ICell cellj = rowi.GetCell(j);
                                    try
                                    {
                                        content = cellj.StringCellValue.Trim();
                                    }
                                    catch
                                    {
                                        content = "";
                                    }
                                    value += ",'" + content + "'";
                                }
                                value += ",'" + Session["UserID"].ToString() + "','" + Session["UserName"].ToString() + "','" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                                sqlinsert = "insert into OM_ContractRecord(" + key + ") values(" + value + ")";
                                list.Add(sqlinsert);
                            }
                        }
                    }
                    else
                    {
                        break;
                    }
                }
            }
            try
            {
                DBCallCommon.ExecuteTrans(list);
                foreach (string fileName in Directory.GetFiles(FilePath))//清空该文件夹下的文件
                {
                    string newName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
                    System.IO.File.Delete(FilePath + "\\" + newName);//删除文件下储存的文件
                }
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导入成功');", true);
                UCPaging1.CurrentPage = 1;
                databind();
            }
            catch
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('导入失败，请检查Excel单元格数据格式和长度！');", true);
            }
        }
        #endregion

        #region 导出功能

        protected void btnExport_Click(object sender, EventArgs e) //导出
        {
            string sqltext = "select C_STName,C_STDep,C_STContract,C_Contract1,C_Contract2,C_Contract3,C_Contract4,C_Contract5,C_Contract6,C_Contract7,C_Contract8,C_Contract9,C_Contract10,C_Contract11,C_Contract12,C_Contract13,C_Contract14,C_Contract15,C_Contract16,C_Contract17,C_Contract18,C_Contract19,C_Contract20,C_Contract21,C_Contract22,C_Contract23,C_Contract24,C_Contract25,C_Contract26,C_Contract27,C_Contract28,C_Contract29,C_Contract30 from OM_ContractRecord as a left join TBDS_STAFFINFO as b on a.C_STID=b.ST_ID where " + StrWhere();
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            ExportDataItem(dt);
        }

        private void ExportDataItem(System.Data.DataTable dt)
        {
            string filename = "合同签订记录表" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".xls";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            HttpContext.Current.Response.AddHeader("Content-Disposition", string.Format("attachment;filename={0}", System.Web.HttpContext.Current.Server.UrlEncode(filename)));
            HttpContext.Current.Response.Clear();
            //读取Excel到FileStream 
            using (FileStream fs = File.OpenRead(System.Web.HttpContext.Current.Server.MapPath("合同签订记录表.xls")))
            {
                IWorkbook wk = new HSSFWorkbook(fs);
                ISheet sheet0 = wk.GetSheetAt(0);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    IRow row = sheet0.CreateRow(i + 2);
                    ICell cell0 = row.CreateCell(0);
                    cell0.SetCellValue(i + 1);
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        row.CreateCell(j + 1).SetCellValue(dt.Rows[i][j].ToString());
                    }
                }
                MemoryStream file = new MemoryStream();
                wk.Write(file);
                HttpContext.Current.Response.BinaryWrite(file.GetBuffer());
                HttpContext.Current.Response.End();
            }
        }
        #endregion

        private void CellsMerge(GridView gv, params int[] cols)//合并单元格
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (gv.Rows.Count < 1 || cols[i] > gv.Rows[0].Cells.Count - 1)
                {
                    return;
                }
                TableCell oldTc = gv.Rows[0].Cells[cols[i]];
                for (int j = 1; j < gv.Rows.Count; j++)
                {
                    TableCell tc = gv.Rows[j].Cells[cols[i]];
                    if (oldTc.Text == tc.Text)
                    {
                        tc.Visible = false;
                        if (oldTc.RowSpan == 0)
                        {
                            oldTc.RowSpan = 1;
                        }
                        oldTc.RowSpan++;
                        oldTc.VerticalAlign = VerticalAlign.Middle;
                    }
                    else
                    {
                        oldTc = tc;
                    }
                }
            }
        }
    }
}
