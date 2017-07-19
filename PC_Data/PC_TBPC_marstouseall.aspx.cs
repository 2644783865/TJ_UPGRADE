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
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Drawing;

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_marstouseall : System.Web.UI.Page
    {
        public string gloabpt
        {
            get
            {
                object str = ViewState["gloabpt"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabpt"] = value;
            }
        }
        public string gloabguige
        {
            get
            {
                object str = ViewState["gloabguige"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabguige"] = value;
            }
        }
        public string gloabid
        {
            get
            {
                object str = ViewState["gloabid"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabid"] = value;
            }
        }
        public string glostate
        {
            get
            {
                object str = ViewState["glostate"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["glostate"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initpagemess();
                //tbpc_manmodmarrepallRepeaterdatabind();
                allrepeaterbind();
            }
        }

        private void Initpagemess()
        {
            string sqltext = "";
            string pcode = Request.QueryString["pcode"].ToString();
            sqltext = "SELECT PR_PCODE, PR_PJID, PJ_NAME, PR_ENGID, TSA_ENGNAME," +
                     "PR_REVIEWA, PR_REVIEWANM, PR_REVIEWATIME, PR_REVIEWB, PR_REVIEWBNM, " +
                     "PR_REVIEWBTIME, PR_REVIEWBADVC, PR_STATE, PR_NOTE  " +
                     "FROM View_TBPC_MARSTOUSETOTAL where PR_PCODE='" + pcode + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                TextBox_pid.Text = dr["PR_PCODE"].ToString();
                tb_pjid.Text = dr["PR_PJID"].ToString();
                tb_pjname.Text = dr["PJ_NAME"].ToString();
                tb_pjinfo.Text = tb_pjid.Text + tb_pjname.Text;
                tb_engid.Text = dr["PR_ENGID"].ToString();
                tb_engname.Text = dr["TSA_ENGNAME"].ToString();
                tb_enginfo.Text = tb_engid.Text + tb_engname.Text;
                tb_peoid.Text = dr["PR_REVIEWA"].ToString();
                tb_peoname.Text = dr["PR_REVIEWANM"].ToString();
                Tb_shijian.Text = dr["PR_REVIEWATIME"].ToString();
                tb_zh.Text = dr["PR_ENGID"].ToString();
            }
            dr.Close();
        }
        private void allrepeaterbind()
        {
            string sqltext = "";
            sqltext = "SELECT planno, pjid, pjnm, engid, engnm, ptcode, marid, marnm, margg, margb, marcz, "+
                      "marunit, marfzunit, length, width, num, fznum, usenum, usefznum, allstata, " +
                      "allshstate, allnote  "+
                      "FROM View_TBPC_MARSTOUSEALL where planno='" + TextBox_pid.Text + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                gloabid = dr["marid"].ToString();
            }
            dr.Close();
            DBCallCommon.BindRepeater(tbpc_manmodmarrepallRepeater, sqltext);
        }
        private void automodeinit()
        {
            string sqltext = "";
            List<string> sqltextlist = new List<string>();
            DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            string ptcode = "";
            string newptcode = "";
            string rowsnum = "";
            string newsqcode = "";
            string oldsqcode = "";
            string lotnum = "";
            string warehouseid = "";
            string positionid = "";
            string marid = "";
            string submarid = "";
            string marnm = "";
            string marunit = "";
            string margg = "";
            string marcz = "";
            string margb = "";
            string state = "";
            double usenum = 0;
            double useweight = 0;
            double allusenum = 0;
            double allusefznum = 0;
            double plannum = 0;
            double planfznum = 0;
            double num = 0;
            double tempnum = 0;
            double tempweight = 0;
            sqltext ="SELECT planno, pjid, pjnm, engid, engnm, ptcode, marid, marnm, margg, margb, marcz, "+
                     "marunit, marfzunit, length, width, num, fznum, usenum, usefznum, allstata, allshstate, "+
                     "allnote "+
                     "FROM View_TBPC_MARSTOUSEALL where planno='" + TextBox_pid.Text + "'";
            dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                marid = dt1.Rows[i]["marid"].ToString();
                marnm = dt1.Rows[i]["marnm"].ToString();
                marunit = dt1.Rows[i]["marunit"].ToString();
                state = dt1.Rows[i]["allstata"].ToString();
                ptcode = dt1.Rows[i]["ptcode"].ToString();
                plannum = Convert.ToDouble(dt1.Rows[i]["num"].ToString());
                planfznum = Convert.ToDouble(dt1.Rows[i]["fznum"].ToString());
                allusenum = 0;
                allusefznum = 0;
                if (state == "0")
                {
                    sqltext = "SELECT SQ_CODE as SQCODE, SQ_MARID as MARID, SQ_LENGTH AS LENGTH, "+
                              "SQ_WIDTH AS WIDTH, SQ_LOTNUM AS LOTNUM,SQ_WAREHOUSE AS WAREHOUSEID,"+
                              "SQ_LOCATION AS POSITIONID, SQ_NUM as NUM, SQ_FZNUM AS FZNUM,USENUM='0',"+
                              "USEFZNUM='0',STATE='0' " +
                              "FROM TBWS_STORAGE where SQ_MARID='" + marid + "' and SQ_PTC = '备库' order by SQ_NUM asc";
                    dt2 = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dt2.Rows.Count > 0)
                    {
                        num = plannum;
                        for (int j = 0; j < dt2.Rows.Count; j++)
                        {
                            tempnum = Convert.ToDouble(dt2.Rows[j]["NUM"].ToString());
                            rowsnum = (j + 1).ToString();
                            newptcode = ptcode.Substring(0, ptcode.Length - 4) + "MTO0" + rowsnum + ptcode.Substring(ptcode.Length - 4);//MTO0占用，MTO1相似代用
                            oldsqcode = dt2.Rows[j]["SQCODE"].ToString();
                            submarid = marid.Substring(0, 5);
                            lotnum = dt2.Rows[j]["LOTNUM"].ToString();
                            warehouseid = dt2.Rows[j]["WAREHOUSEID"].ToString();
                            positionid = dt2.Rows[j]["POSITIONID"].ToString();
                            newsqcode = marid + newptcode + lotnum + positionid + warehouseid;//库存唯一标识码
                            if (num > 0)
                            {
                                if (num >= tempnum)
                                {
                                    num = num - tempnum;
                                    sqltext = "update TBWS_STORAGE set SQ_CODE='" + newsqcode + "',SQ_PTC='" + newptcode + "' where SQ_CODE='" + oldsqcode + "'";
                                    sqltextlist.Add(sqltext);
                                    if (submarid == "01.07")
                                    {
                                        usenum = 0;
                                        useweight = tempnum;
                                    }
                                    else
                                    {
                                        usenum = tempnum;
                                        useweight = 0;
                                    }
                                    sqltext = "insert into TBPC_MARSTOUSEALLDETAIL(PUR_PTCODE,PUR_MARNAME, " +
                                              "PUR_NUNIT, PUR_USTNUM,PUR_USTWEIGHT,PUR_NEWSQCODE,PUR_SQCODE) " +
                                              "values('" + ptcode + "','" + marnm + "','" + marunit + "'," + usenum + "," +
                                              useweight + ",'" + newsqcode + "','" + oldsqcode + "')";
                                    sqltextlist.Add(sqltext);
                                }
                                else
                                {
                                    if (submarid == "01.07")
                                    {
                                        usenum = 0;
                                        useweight = num;
                                    }
                                    else
                                    {
                                        usenum = num;
                                        useweight = 0;
                                    }
                                    sqltext = "insert into TBWS_STORAGE(SQ_CODE, SQ_MARID, SQ_MARNAME, SQ_ATTRIBUTE, SQ_GB, SQ_STANDARD, SQ_LENGTH, SQ_WIDTH, " +
                                              "SQ_LOTNUM, SQ_PMODE, SQ_PTC, SQ_WAREHOUSEID, SQ_WAREHOUSENAME, SQ_POSITIONID, " +
                                              "SQ_POSITIONNAME, SQ_UNIT, SQ_NUM, SQ_UNITPRICE, SQ_AMOUNT,SQ_NOTE) " +
                                              "select SQ_CODE='" + newsqcode + "', SQ_MARID, SQ_MARNAME, SQ_ATTRIBUTE, SQ_GB, SQ_STANDARD, SQ_LENGTH, SQ_WIDTH, " +
                                              "SQ_LOTNUM, SQ_PMODE, SQ_PTC='" + newptcode + "', SQ_WAREHOUSEID, SQ_WAREHOUSENAME, SQ_POSITIONID, " +
                                              "SQ_POSITIONNAME, SQ_UNIT,SQ_NUM=" + num + ",SQ_UNITPRICE, SQ_AMOUNT=0,SQ_NOTE='' " +
                                              "from TBWS_STORAGE where SQ_PTC='备库' and SQ_CODE='" + oldsqcode + "'";
                                    sqltextlist.Add(sqltext);
                                    sqltext = "update TBWS_STORAGE set SQ_NUM=SQ_NUM-" + num + " where SQ_CODE='" + oldsqcode + "'";
                                    sqltextlist.Add(sqltext);
                                    sqltext = "insert into TBPC_MARSTOUSEALLDETAIL(PUR_PTCODE,PUR_MARNAME, " +
                                              "PUR_NUNIT, PUR_USTNUM,PUR_USTWEIGHT,PUR_NEWSQCODE,PUR_SQCODE) " +
                                              "values('" + ptcode + "','" + marnm + "','" + marunit + "'," + usenum + "," +
                                              useweight + ",'" + newsqcode + "','" + oldsqcode + "')";
                                    sqltextlist.Add(sqltext);
                                    num = 0;
                                }
                                allusenum = allusenum + usenum;
                                allusefznum = allusefznum + useweight;
                            }
                            else
                            {
                                break;
                            }
                        }
                        dt1.Rows[i]["PUR_USTNUM"] = allusenum;
                        dt1.Rows[i]["PUR_USTWEIGHT"] = allusefznum;
                    }
                    else
                    {
                        dt1.Rows[i]["PUR_USTNUM"] = 0;
                        dt1.Rows[i]["PUR_USTWEIGHT"] = 0;
                        allusenum = 0;
                        allusefznum = 0;
                    }
                    sqltext = "update TBPC_MARSTOUSEALL set PUR_USTNUM=" + allusenum + ",PUR_USTWEIGHT=" + allusefznum + "  where PUR_PTCODE='" + ptcode + "'";
                    sqltextlist.Add(sqltext);
                    sqltext = "update TBPC_MARSTOUSEALL set  PUR_STATE='1' where PUR_PTCODE='" + ptcode + "'";
                    sqltextlist.Add(sqltext);
                    DBCallCommon.ExecuteTrans(sqltextlist);
                    sqltextlist.Clear();
                }
            }
            tbpc_manmodmarrepallRepeater.DataSource = dt1;
            tbpc_manmodmarrepallRepeater.DataBind();
        }
        protected void btn_concel_Click(object sender, EventArgs e)
        {
            string ptcode = gloabpt;
            string sqltext = "";
            List<string> sqltextlist = new List<string>();
            string newsqcode = "";
            string oldsqcode = "";
            double num = 0;
            if (rad_detail.Checked)
            {
                if (glostate == "1")
                {
                    foreach (RepeaterItem Reitem in tbpc_marrepallbzjdetailRepeater.Items)
                    {
                        newsqcode = ((Label)Reitem.FindControl("NEWSQCODE")).Text;
                        oldsqcode = ((Label)Reitem.FindControl("SQCODE")).Text;
                        num = Convert.ToDouble(((TextBox)Reitem.FindControl("USENUM")).Text);
                        #region
                        //选择checkbox占用前程序
                        //**********************改变备库量*********************
                        sqltext = "select * from TBWS_STORAGE where SQ_CODE ='" + oldsqcode + "' AND SQ_PTC='备库'";
                        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                        if (dt.Rows.Count > 0)
                        {
                            sqltext = "update TBWS_STORAGE set SQ_NUM=SQ_NUM+" + num + " where SQ_CODE ='" + oldsqcode + "' AND SQ_PTC='备库'";
                            DBCallCommon.ExeSqlText(sqltext);
                            sqltext = "delete from TBWS_STORAGE where SQ_CODE='" + newsqcode + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                        else
                        {
                            sqltext = "update TBWS_STORAGE set SQ_PTC='备库',SQ_CODE='" + oldsqcode + "' where SQ_CODE='" + newsqcode + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                        sqltext = "delete from TBPC_MARSTOUSEALLDETAIL where PUR_NEWSQCODE='" + newsqcode + "' and PUR_PTCODE='" + gloabpt + "'";
                        sqltextlist.Add(sqltext);
                        #endregion
                    }
                    glostate = "0";//未保存
                    sqltext = "update TBPC_MARSTOUSEALL set PUR_USTNUM=0,PUR_USTFZNUM=0,PUR_STATE='0' where PUR_PTCODE='" + gloabpt + "'";//状态改为未用库存
                    sqltextlist.Add(sqltext);
                    DBCallCommon.ExecuteTrans(sqltextlist);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该物料未保存，不需取消！');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择详细信息，否则不能取消！');", true);
            }     
            allrepeaterbind();
            tbpc_marrepallbzjdetailRepeaterdatabind(); 
        }
        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            List<string> sqltextlist = new List<string>();
            if (rad_detail.Checked)
            {
                #region
                if (glostate == "0")
                {
                    bzjconfirm();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该物料已占用库存，请先取消后再修改！');", true);
                } 
                #endregion
            }
            allrepeaterbind();
            detailRepeaterdatabind();
        }
        protected void bzjconfirm()
        {
            string sqltext = "";
            List<string> sqltextlist = new List<string>();
            string ptcode = gloabpt;
            string newptcode = "";
            string rowsnum = "";
            string newsqcode = "";
            string oldsqcode = "";
            string lotnum = "";
            string warehouseid = "";
            string positionid = "";
            string marid = "";
            double usenum = 0;
            double usefznum = 0;
            double kcnum = 0;
            string state = "";
            int i = 0;
            TextBox tbkundet = tbpc_marrepallbzjdetailRepeater.Controls[tbpc_marrepallbzjdetailRepeater.Controls.Count - 1].FindControl("tb_kuntotal") as TextBox;
            foreach (RepeaterItem Reitem in tbpc_marrepallbzjdetailRepeater.Items)
            {
                CheckBox cbk = (CheckBox)Reitem.FindControl("CKBOX_SELECT");
                if (cbk.Checked)
                {
                    if (i == 0)
                    {
                        sqltext = "update TBPC_MARSTOUSEALL set PUR_STATE='1' where PUR_PTCODE='" + gloabpt + "'";//状态改为已占用库存
                        sqltextlist.Add(sqltext);
                    }
                    rowsnum = ((Label)Reitem.FindControl("ROWBGSNUM")).Text;
                    newptcode = ptcode.Substring(0, ptcode.Length - 4) + "MTO0" + rowsnum + ptcode.Substring(ptcode.Length - 4);//MTO0占用，MTO1相似代用
                    oldsqcode = ((Label)Reitem.FindControl("SQCODE")).Text;
                    marid = ((Label)Reitem.FindControl("MARID")).Text;
                    lotnum = ((Label)Reitem.FindControl("LOTNUM")).Text;
                    warehouseid = ((Label)Reitem.FindControl("WAREHOUSEID")).Text;
                    positionid = ((Label)Reitem.FindControl("POSITIONID")).Text;
                    newsqcode = marid + newptcode + lotnum + positionid + warehouseid;//库存唯一标识码
                    usenum = Convert.ToDouble(((TextBox)Reitem.FindControl("USENUM")).Text == "" ? "0" : ((TextBox)Reitem.FindControl("USENUM")).Text);
                    kcnum = Convert.ToDouble(((Label)Reitem.FindControl("NUM")).Text == "" ? "0" : ((Label)Reitem.FindControl("NUM")).Text);
                    state = ((Label)Reitem.FindControl("STATE")).Text;
                    if (usenum == kcnum)
                    {
                        sqltext = "update TBWS_STORAGE set SQ_CODE='" + newsqcode + "',SQ_PTC='" + newptcode + "' where SQ_CODE='" + oldsqcode + "'";
                        sqltextlist.Add(sqltext);

                    }
                    else
                    {
                        sqltext = "insert into TBWS_STORAGE(SQ_CODE, SQ_MARID,SQ_LENGTH,SQ_WIDTH, " +
                                               "SQ_LOTNUM, SQ_PMODE, SQ_PTC, SQ_WAREHOUSE,SQ_LOCATION, " +
                                               "SQ_NUM,SQ_FZNUM,SQ_FIXED,SQ_ORDERID,SQ_NOTE,SQ_STATE) " +
                                               "select SQ_CODE='" + newsqcode + "', SQ_MARID,SQ_LENGTH, SQ_WIDTH, " +
                                               "SQ_LOTNUM, SQ_PMODE, SQ_PTC='" + newptcode + "', SQ_WAREHOUSE,SQ_LOCATION, " +
                                               "SQ_NUM=" + usenum + ",SQ_FZNUM=" + usefznum + ",SQ_FIXED,SQ_ORDERID,SQ_NOTE='',SQ_STATE  " +
                                               "from TBWS_STORAGE where SQ_PTC='备库' and SQ_CODE='" + oldsqcode + "'";
                        sqltextlist.Add(sqltext);
                        sqltext = "update TBWS_STORAGE set SQ_NUM=SQ_NUM-" + usenum + " where SQ_CODE='" + oldsqcode + "'";
                        sqltextlist.Add(sqltext);
                    }
                    sqltext = "insert into TBPC_MARSTOUSEALLDETAIL(PUR_PTCODE,PUR_MARID," +
                                            "PUR_USTNUM,PUR_USTFZNUM,PUR_NEWSQCODE,PUR_SQCODE) " +
                                            "values('" + ptcode + "','" + marid + "'," + usenum + "," +
                                            usefznum + ",'" + newsqcode + "','" + oldsqcode + "')";
                    sqltextlist.Add(sqltext);
                    i++;
                } 

            }
            if (i > 0)
            {
                sqltext = "update TBPC_MARSTOUSEALL set PUR_USTNUM='" + Convert.ToDouble(tbkundet.Text == "" ? "0" : tbkundet.Text) + "',PUR_USTFZNUM='0' where PUR_PTCODE='" + ptcode + "'";
                sqltextlist.Add(sqltext);
                DBCallCommon.ExecuteTrans(sqltextlist);
                glostate = "1";//已保存
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择数据！');", true);
            }
            
        }
        
        private void detailRepeaterdatabind()
        {
            string marid = "";
            marid = gloabid;
            if (rad_detail.Checked)
            {
                tbpc_marrepallbzjdetailRepeater.Visible = true;
                tr5.Visible = true;
                tbpc_marrepallbzjdetailRepeaterdatabind();
               
            }
            else
            {
                tr5.Visible = false;
            }
        }
        protected void btn_back_Click(object sender, EventArgs e)
        {
            string pcode = Request.QueryString["pcode"].ToString();
            Response.Redirect("~/PC_Data/PC_TBPC_Purchaseplan_check_detail.aspx?sheetno=" + pcode + "");
        }
        protected void rad_detail_OnCheckedChanged(object sender, EventArgs e)
        {
            //tbpc_marrepallsumRepeater.Visible = false;
            //detailRepeaterdatabind();
        }
        protected void btn_edit_Click(object sender, EventArgs e)
        {
            Button lbtn = (Button)sender;
            RepeaterItem Reitem = (RepeaterItem)lbtn.Parent;
            //((HtmlTableRow)Reitem.FindControl("row")).BgColor = "#FF0000";
            string ptcode = ((Label)Reitem.FindControl("PUR_PTCODE")).Text;
            gloabpt = ptcode;
            glostate = ((Label)Reitem.FindControl("PUR_STATE")).Text;
            gloabid = ((Label)Reitem.FindControl("PUR_MARID")).Text;
            detailRepeaterdatabind();
            foreach (RepeaterItem allreitem in tbpc_manmodmarrepallRepeater.Items)
            {
                if (((Label)allreitem.FindControl("PUR_PTCODE")).Text == ptcode)
                {
                    ((Button)allreitem.FindControl("btn_edit")).BackColor = Color.Red;
                }
                else
                {
                    ((Button)allreitem.FindControl("btn_edit")).BackColor = Color.Empty;
                }
            }
        }
        private void tbpc_marrepallbzjdetailRepeaterdatabind()
        {
            string sqltext = "";
            string marid = "";
            marid = gloabid;
            string ptcode = gloabpt;
            if (glostate == "1")//已经保存此时只能查看不能保存
            {
                sqltext ="SELECT NUM,LOTNUM,WAREHOUSE as WAREHOUSEID,LOCATION as POSITIONID,"+
                         "STATE='1',detailmarid as MARID,detailmarnm as MARNAME,detailusenum as USENUM," +
                         "newsqcode as NEWSQCODE,oldsqcode as SQCODE  " +
                         "FROM View_TBPC_MARSTOUSEALLDETAIL_STO  " +
                         "where (ptcode = '" + ptcode + "')";
            }
            else
            {
                sqltext = "SELECT sqcode as SQCODE,NEWSQCODE='',marid as MARID, marnm as MARNAME," +
                          "lotnum AS LOTNUM,warehouse AS WAREHOUSEID, location AS POSITIONID, " +
                          "num AS NUM, USENUM='0',STATE='0' " +
                          "FROM View_TBPC_STORAGE where marid='" + marid + "' and  ptcode = '备库' order by num asc";
            }
            DBCallCommon.BindRepeater(tbpc_marrepallbzjdetailRepeater, sqltext);
           
        }
       
       
        private double sum11 = 0;
        private double sum12 = 0;
        protected void tbpc_manmodmarrepallRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //((Button)e.Item.FindControl("btn_edit")).Attributes.Add("onclick", "form.target='_blank'");
                if (((Label)e.Item.FindControl("PUR_NUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("PUR_NUM")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("PUR_NUM")).Text = "0";
                }
                sum11 += Convert.ToDouble(((Label)e.Item.FindControl("PUR_NUM")).Text);
                if (((Label)e.Item.FindControl("PUR_FZNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("PUR_FZNUM")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("PUR_FZNUM")).Text = "0";
                }
                sum12 += Convert.ToDouble(((Label)e.Item.FindControl("PUR_FZNUM")).Text);
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((TextBox)(e.Item.FindControl("tb_ntotal"))).Text = Convert.ToString(sum11);
                ((TextBox)(e.Item.FindControl("tb_fztotal"))).Text = Convert.ToString(sum12);
            }
        }
        private double sum51 = 0;
        private double sum52 = 0;
        protected void tbpc_marrepallbzjdetailRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("NUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("NUM")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("NUM")).Text = "0";
                }
                sum51 += Convert.ToDouble(((Label)e.Item.FindControl("NUM")).Text);

                if (((TextBox)e.Item.FindControl("USENUM")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("USENUM")).Text.ToString() == System.String.Empty)
                {
                    ((TextBox)e.Item.FindControl("USENUM")).Text = "0";
                }
                sum52 += Convert.ToDouble(((TextBox)e.Item.FindControl("USENUM")).Text);

                if (glostate == "0")
                {
                    ((TextBox)e.Item.FindControl("USENUM")).Visible = true;
                    ((Label)e.Item.FindControl("LUSENUM")).Visible = false;
                }
                else
                {
                    ((TextBox)e.Item.FindControl("USENUM")).Visible = false;
                    ((Label)e.Item.FindControl("LUSENUM")).Visible = true;
                }
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((TextBox)(e.Item.FindControl("tb_kntotal"))).Text = Convert.ToString(sum51);
                ((TextBox)(e.Item.FindControl("tb_kuntotal"))).Text = Convert.ToString(sum52);
            }

        }
        protected string get_all_state(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "未保存";
            }
            else
            {
                state = "已保存";
            }
            return state;
        }
        protected string get_alldet_state(string i)
        {
            string state = "";
            if (i == "0")
            {
                state = "未保存";
            }
            else
            {
                state = "已保存";
            }
            return state;
        }
    }
}
