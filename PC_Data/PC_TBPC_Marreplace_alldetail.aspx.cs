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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_Marreplace_alldetail : System.Web.UI.Page
    {
        public string gloabstr
        {
            get
            {
                object str = ViewState["gloabstr"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabstr"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                initpagemess();
            }
        }
        private void initpagemess()
        {
            if (Request.QueryString["sqcodes"] != null)
            {
                gloabstr = Request.QueryString["sqcodes"].ToString();
            }
            else
            {
                gloabstr = "";
            }
            string ptcode = Request.QueryString["ptcode"].ToString();//相似代用物料计划号
            string sqltext ="";
            sqltext ="select mpcode as MP_PNO, pjid as MP_PJID, pjnm as MP_PJNAME,engid as  MP_ENGID,"+
                     "engnm as MP_ENGNAME, ptcode as MP_PTCODE,marid as MP_MARID,marnm as MP_MARNAME, " +
                     "marguige as MP_MARGUIGE,marcaizhi as MP_MARCAIZHI,marguobiao as MP_MARGUOBIAO,"+
                     "num as MP_NUM,fznum AS MP_FZNUM,marcgunit AS MP_UNIT,fzunit AS MP_FZUNIT,"+
                     "length AS MP_LENGTH,width as MP_WIDTH,allstate AS MP_STATE,allnote AS MP_NOTE,allshstate " +
                     "from View_TBPC_MARREPLACE_all_total where ptcode='" + ptcode + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                PNO.Text = dr["MP_PNO"].ToString();
                ENGID.Text = dr["MP_ENGID"].ToString();
                YPTCODE.Text = dr["MP_PTCODE"].ToString();
                YMARID.Text = dr["MP_MARID"].ToString();
                YMARNAME.Text = dr["MP_MARNAME"].ToString();
                YGUIGE.Text = dr["MP_MARGUIGE"].ToString();
                YCAIZHI.Text = dr["MP_MARCAIZHI"].ToString();
                YGUOBIAO.Text = dr["MP_MARGUOBIAO"].ToString();
                YNUNIT.Text = dr["MP_UNIT"].ToString();
                YFZNUM.Text = dr["MP_FZNUM"].ToString();
                YNUM.Text = dr["MP_NUM"].ToString();
                YLENGTH.Text = dr["MP_LENGTH"].ToString();
                YWIDTH.Text = dr["MP_WIDTH"].ToString();
                YFZUNIT.Text = dr["MP_FZUNIT"].ToString();
                YSTATE.Text = dr["MP_STATE"].ToString();
                if (YSTATE.Text == "0")
                {
                    YSTATETEXT.Text = "未保存";
                }
                else
                {
                    YSTATETEXT.Text = "已保存";
                }
            }
            dr.Close();
            tbpc_puralldydetailRepeaterdatabind();
            tbpc_marrepkumrepeaterdatabind();
        }

        protected void btn_addrow_Click(object sender, EventArgs e)
        {
            //CreateNewRow();
            string sqltext = "";
            string pno = "";
            string ptcode = "";
            string marid = "";
            double length = 0;
            double width = 0;
            string sqcode = "";
            foreach (RepeaterItem Reitem in tbpc_marrepkumrepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        pno = PNO.Text;
                        ptcode = YPTCODE.Text;
                        marid = ((Label)Reitem.FindControl("MARID")).Text;
                        length = Convert.ToDouble(((Label)Reitem.FindControl("LENGTH")).Text == ""?"0":((Label)Reitem.FindControl("LENGTH")).Text);
                        width = Convert.ToDouble(((Label)Reitem.FindControl("WIDTH")).Text == "" ?"0":((Label)Reitem.FindControl("WIDTH")).Text);
                        sqcode = ((Label)Reitem.FindControl("SQCODE")).Text;
                        //插入选中的材料记录到代用单代用材料记录表中
                        sqltext = "insert into TBPC_MARREPLACEDETAIL(MP_CODE,MP_PTCODE,MP_NEWMARID,MP_LENGTH,MP_WIDTH,MP_OLDSQCODE) " +
                                  "values ('" + pno + "','" + ptcode + "','" + marid + "','" + length + "','" + width + "','" + sqcode + "')";
                        DBCallCommon.ExeSqlText(sqltext);
                    }
                }
            }
            tbpc_puralldydetailRepeaterdatabind();
            tbpc_marrepkumrepeaterdatabind();
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('增加成功！');", true);
        }
        protected void btn_delectrow_Click(object sender, EventArgs e)
        {
            string sqltext = "";
            string ptcode = "";
            string sqcode = "";
            string state = "";
            ptcode = YPTCODE.Text;
            int count = 0;
            foreach (RepeaterItem Reitem in tbpc_puralldydetailRepeater.Items)
            {
                CheckBox cbx = Reitem.FindControl("CKBOX_SELECT") as CheckBox;//定义checkbox
                if (cbx != null)//存在行
                {
                    if (cbx.Checked)
                    {
                        state = ((Label)Reitem.FindControl("STATE")).Text;
                        sqcode = ((Label)Reitem.FindControl("OLDSQCODE")).Text;
                        if (state == "0")//未占用库存时，直接删除
                        {
                            sqltext = "delete from TBPC_MARREPLACEDETAIL where MP_PTCODE='" + ptcode + "' and  MP_OLDSQCODE='" + sqcode + "'";
                            DBCallCommon.ExeSqlText(sqltext);
                        }
                        else//已占用库存必须先取消再删除
                        {
                            count++;
                        }
                    }
                }
            }
            if (count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('已占用库存，必须先取消再删除！');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('删除成功！');", true);
            }
            tbpc_puralldydetailRepeaterdatabind();
            tbpc_marrepkumrepeaterdatabind();
        }

        //代用物料信息
        private void tbpc_puralldydetailRepeaterdatabind()
        {
            string ptcode = YPTCODE.Text;
            string state = YSTATE.Text;
            string sqltext = "";
            if (state == "1")//已经保存此时只能查看不能保存
            {
                sqltext = "SELECT sum(NUM) AS KUNUM, sum(FZNUM) AS KUFZNUM,LOTNUM='',LENGTH AS LENGTH, WIDTH AS WIDTH," +
                           "WAREHOUSEID='',POSITIONID='',detailmarnm AS XMARNAME," +
                           "detailmarguige AS XGUIGE, detailmarguobiao AS XGUOBIAO, detailmarunit AS XNUNIT, " +
                           "detailmarcaizhi AS XCAIZHI,detailfzunit,sum(detailmarnuma) AS XUSENUM,detailmpcode," +
                           "sum(detailmarnumb) AS XUSEFZNUM,detailoldptcode,detailmarid AS XMARID,detailnote," +
                           "detailoldsqcode as OLDSQCODE, detailnewsqcode as NEWSQCODE,STATE='1'  " +
                           "FROM View_TBPC_MARREPLACE_detail_STO where (detailoldptcode = '" + ptcode + "') "+
                           "group by LENGTH,WIDTH,detailmarnm,detailmarguige,detailmarguobiao,detailmarunit,"+
                           "detailmarcaizhi,detailfzunit,detailmpcode,detailoldptcode,detailmarid,detailnote,detailoldsqcode,detailnewsqcode";
                
            }
            else
            {

                sqltext = "SELECT detailnewsqcode as NEWSQCODE,detailoldsqcode as OLDSQCODE, a.SQ_MARID as XMARID,a.MNAME as XMARNAME," +
                          "b.detailmarcaizhi as XCAIZHI, b.detailmarguobiao as XGUOBIAO, " +
                          "b.detailmarguige as XGUIGE,b.detailmarunit AS XNUNIT," +
                          "sumnum as KUNUM,sumfznum as KUFZNUM,sumnum as XUSENUM," +
                              "sumfznum as XUSEFZNUM,LOTNUM='',WAREHOUSEID=''," +
                              "POSITIONID='',SQ_LENGTH as LENGTH,SQ_WIDTH as WIDTH,STATE='0' " +
                              "FROM View_TBPC_STOSUM as a  inner join View_TBPC_MARREPLACE_detail as b  " +
                              "on a.SQ_MARID=b.detailmarid AND a.SQ_LENGTH = b.detaillength AND a.SQ_WIDTH = b.detailwidth   " +
                              "where (b.detailoldptcode = '" + ptcode + "')";
                
            }
            DBCallCommon.BindRepeater(tbpc_puralldydetailRepeater, sqltext);
            if (tbpc_puralldydetailRepeater.Items.Count == 0)
            {
                NoDataPane2.Visible = true;
            }
            else
            {
                NoDataPane2.Visible = false;
            }
        }
        protected void rad_detail_OnCheckedChanged(object sender, EventArgs e)
        {
            tbpc_marrepkumrepeaterdatabind();
        }
        protected void rad_summess_OnCheckedChanged(object sender, EventArgs e)
        {
            tbpc_marrepkumrepeaterdatabind();
        }

        //库存物料信息
        private void tbpc_marrepkumrepeaterdatabind()
        {
            string marnm = YMARNAME.Text;
            string state = YSTATE.Text;
            string ptcode = YPTCODE.Text;
            string sqltext = "";
            string caizhi = YCAIZHI.Text;
            if (state == "0")//未保存时显示汇总库存相似物料
            {
               
                sqltext = "select marid as MARID,marnm as MARNAME,margg as GUIGE,margb as GUOBIAO," +
                          "marcz as CAIZHI,marunit as NUNIT,marfzunit as FZUNIT,length as LENGTH," +
                          "width as WIDTH,sum(num) as NUM,sum(fznum) as FZNUM,SQCODE=''  " +
                          "from View_TBPC_STORAGE  " +
                          "where marnm='" + marnm + "' and  ptcode='备库' and marcz='" + caizhi + "' and  marid not in (select detailmarid from View_TBPC_MARREPLACE_detail where detailoldptcode='" + ptcode + "')" +
                          "group by marid,marnm,margg,margb,marcz,marunit,marfzunit,length,width";
                
                DBCallCommon.BindRepeater(tbpc_marrepkumrepeater, sqltext);
                if (tbpc_marrepkumrepeater.Items.Count == 0)
                {
                    NoDataPane3.Visible = true;
                }
                else
                {
                    NoDataPane3.Visible = false;
                }
            }
        }
        protected void btn_lookup_click(object sender, EventArgs e)
        {
            string sqltext = "";
            string ptcode = "";
            string marid = "";
            string marnm = "";
            string margg = "";
            string marcz = "";
            string margb = "";
            ptcode = YPTCODE.Text;
            marid = tb_marid.Text;
            marnm = tb_marnm.Text;
            margg = tb_margg.Text;
            marcz = tb_marcz.Text;
            margb = tb_margb.Text;
            DataTable dt = new DataTable();
            DataView dv = null;
            sqltext = "select marid as MARID, marnm as MARNAME,margg as GUIGE,marcz as CAIZHI , margb as GUOBIAO,marunit as NUNIT,sum(num) as NUM," +
                     "sum(fznum) as FZNUM,length as LENGTH,width as WIDTH,marfzunit as FZUNIT," +
                     "SQCODE='' FROM View_TBPC_STORAGE where ptcode='备库' and marid not in (select detailmarid from View_TBPC_MARREPLACE_detail where detailoldptcode='" + ptcode + "')  " +
                     "group by marid,marnm,margg,margb,marcz,marunit,marfzunit,length, width";

            dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            dv = dt.DefaultView;
            dv.Sort = "MARID ASC";
            if (marid != "")
            {
                dv.RowFilter = "MARID='" + marid + "'";
                dt = dv.ToTable();
            }
            if (marnm != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "MARNAME='" + marnm + "'";
                dt = dv.ToTable();
            }
            if (margg != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "GUIGE='" + margg + "'";
                dt = dv.ToTable();
            }
            if (marcz != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "CAIZHI='" + marcz + "'";
                dt = dv.ToTable();
            }
            if (margb != "")
            {
                dv = dt.DefaultView;
                dv.RowFilter = "GUOBIAO='" + margb + "'";
                dt = dv.ToTable();
            }
            tbpc_marrepkumrepeater.DataSource = dt;
            tbpc_marrepkumrepeater.DataBind();
            if (tbpc_marrepkumrepeater.Items.Count == 0)
            {
                NoDataPane3.Visible = true;
            }
            else
            {
                NoDataPane3.Visible = false;
            }
        }
        protected void btn_cancel_click(object sender, EventArgs e)
        {
            string ptcode = YPTCODE.Text;
            string sqltext = "";
            List<string> sqltextlist = new List<string>();
            string newsqcode = "";
            string oldsqcode = "";
            string marid = "";
            string guige = "";
            double num = 0;
            //double fznum = 0;
            if (YSTATE.Text == "1")//已经保存可以取消
            {
                sqltext = "update TBPC_MARREPLACEALL set MP_STATE='0' where MP_PTCODE='" + ptcode + "'";//状态改为未占用库存
                sqltextlist.Add(sqltext);
                //DBCallCommon.ExeSqlText(sqltext);
                foreach (RepeaterItem Reitem in tbpc_puralldydetailRepeater.Items)
                {
                    newsqcode = ((Label)Reitem.FindControl("NEWSQCODE")).Text;
                    oldsqcode = ((Label)Reitem.FindControl("OLDSQCODE")).Text;
                    //newptcode = ((Label)Reitem.FindControl("XPTCODE")).Text;
                    marid = ((Label)Reitem.FindControl("XMARID")).Text;
                    guige = ((Label)Reitem.FindControl("XGUIGE")).Text;

                    num = Convert.ToDouble(((TextBox)Reitem.FindControl("XUSENUM")).Text);
                    //fznum = Convert.ToDouble(((TextBox)Reitem.FindControl("XUSEFZNUM")).Text);
                    //**********************改变备库量*********************
                    sqltext = "select * from TBWS_STORAGE where SQ_CODE='" + oldsqcode + "' AND SQ_PTC='备库'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (dt.Rows.Count > 0)
                    {
                        sqltext = "update TBWS_STORAGE set SQ_NUM=SQ_NUM+" + num + "  where SQ_CODE='" + oldsqcode + "' AND SQ_PTC='备库'";
                        sqltextlist.Add(sqltext);
                        sqltext = "delete from TBWS_STORAGE where SQ_CODE='" + newsqcode + "'";
                        sqltextlist.Add(sqltext);
                    }
                    else
                    {
                        sqltext = "update TBWS_STORAGE set SQ_PTC='备库',SQ_CODE='" + oldsqcode + "' where SQ_CODE='" + newsqcode + "'";
                        sqltextlist.Add(sqltext);
                    }
                }
                DBCallCommon.ExecuteTrans(sqltextlist);
                YSTATE.Text = "0";//未保存
                YSTATETEXT.Text = "未保存";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('取消成功！');", true);
                tbpc_puralldydetailRepeaterdatabind();
                tbpc_marrepkumrepeaterdatabind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该物料未保存，不需取消！');", true);
            }
        }
        protected void btn_confirm_click(object sender, EventArgs e)
        {
            string pcode=PNO.Text;//单号
            string ptcode = YPTCODE.Text;//计划跟踪号
            DataTable dt = new DataTable();
            string newptcode = "";
            string rowsnum = "";
            string newsqcode = "";
            string oldsqcode = "";
            string newmarid = "";
            double width = 0;
            double length = 0;
            //string submarid = "";
            string lotnum = "";
            string warehouseid = "";
            string positionid = "";
            string sqltext = "";
            List<string> sqltextlist = new List<string>();
            double usenum = 0;
            double usefznum = 0;
            double num = 0;
            //double fznum = 0;
            double allusenum = 0;
            double allusefznum = 0;
            double tempnum = 0;
            //double tempfznum = 0;
            //string state = "";
            //TextBox tbkunfz = tbpc_puralldydetailRepeater.Controls[tbpc_puralldydetailRepeater.Controls.Count - 1].FindControl("tb_kufzntotal") as TextBox;
            TextBox tbkun = tbpc_puralldydetailRepeater.Controls[tbpc_puralldydetailRepeater.Controls.Count - 1].FindControl("tb_kuntotal") as TextBox;
            if (YSTATE.Text == "0")
            {
                sqltext = "update TBPC_MARREPLACEALL set MP_STATE='1' where MP_PTCODE='" + ptcode + "'";//状态改为已占用库存
                sqltextlist.Add(sqltext);
                //DBCallCommon.ExeSqlText(sqltext);
                foreach (RepeaterItem Reitem in tbpc_puralldydetailRepeater.Items)
                {
                    newmarid = ((Label)Reitem.FindControl("XMARID")).Text;//新物料编码
                    length = Convert.ToDouble(((Label)Reitem.FindControl("LENGTH")).Text == "" ? "0" : ((Label)Reitem.FindControl("LENGTH")).Text);
                    width = Convert.ToDouble(((Label)Reitem.FindControl("WIDTH")).Text == "" ? "0" : ((Label)Reitem.FindControl("WIDTH")).Text);
                    //删除代用单代用材料记录表中对应的记录
                    sqltext = "delete from TBPC_MARREPLACEDETAIL where MP_PTCODE='" + ptcode + "' and  "+
                              "MP_NEWMARID='" + newmarid + "' and MP_LENGTH='" + length + "' and MP_WIDTH='"+width+"'";
                    sqltextlist.Add(sqltext);

                    sqltext = "SELECT SQ_CODE as SQCODE, SQ_MARID as MARID, SQ_LENGTH AS LENGTH, " +
                              "SQ_WIDTH AS WIDTH, SQ_LOTNUM AS LOTNUM,SQ_WAREHOUSE AS WAREHOUSEID," +
                              "SQ_LOCATION AS POSITIONID, SQ_NUM as NUM, SQ_FZNUM AS FZNUM,USENUM='0'," +
                              "USEFZNUM='0',STATE='0' " +
                              "FROM TBWS_STORAGE where SQ_MARID='" + newmarid + "' and SQ_PTC = '备库'  "+
                              "and SQ_LENGTH='" + length + "' and SQ_WIDTH='"+width+"' order by SQ_NUM asc";
                     dt = DBCallCommon.GetDTUsingSqlText(sqltext);
                     if (dt.Rows.Count > 0)
                     {
                         num = Convert.ToDouble(((TextBox)Reitem.FindControl("XUSENUM")).Text == "" ? "0" : ((TextBox)Reitem.FindControl("XUSENUM")).Text);
                         //fznum = Convert.ToDouble(((TextBox)Reitem.FindControl("XUSEFZNUM")).Text == "" ? "0" : ((TextBox)Reitem.FindControl("XUSEFZNUM")).Text);
                         for (int j = 0; j < dt.Rows.Count; j++)
                         {
                             tempnum = Convert.ToDouble(dt.Rows[j]["NUM"].ToString());//仓库中的数量
                             rowsnum = (j + 1).ToString();
                             newptcode = ptcode.Substring(0, ptcode.Length - 4) + "MTO1" + rowsnum + ptcode.Substring(ptcode.Length - 4);//新的计划跟踪号
                             oldsqcode = dt.Rows[j]["SQCODE"].ToString();//旧的库存唯一标识码
                             lotnum = dt.Rows[j]["LOTNUM"].ToString();
                             warehouseid = dt.Rows[j]["WAREHOUSEID"].ToString();
                             positionid = dt.Rows[j]["POSITIONID"].ToString();
                             newsqcode = newmarid + newptcode + lotnum + positionid + warehouseid;//新的库存唯一标识码
                             if (num > 0)
                             {
                                 if (num >= tempnum)
                                 {
                                     num = num- tempnum;
                                     sqltext = "update TBWS_STORAGE set SQ_CODE='" + newsqcode + "',SQ_PTC='" + newptcode + "' where SQ_CODE='" + oldsqcode + "'";
                                     sqltextlist.Add(sqltext);
                                     usenum = tempnum;
                                     usefznum = 0;

                                     sqltext = "insert into TBPC_MARREPLACEDETAIL(MP_CODE, MP_PTCODE, MP_NEWMARID, MP_NEWNUMA, " +
                                               "MP_NEWNUMB, MP_LENGTH, MP_WIDTH, MP_OLDSQCODE, MP_NEWSQCODE,MP_STATE,MP_NEWNOTE)  " +
                                               "values('" + pcode + "','" + ptcode + "','" + newmarid + "'," + usenum + "," +
                                               usefznum + "," + length + "," + width + ",'" + oldsqcode + "','" + newsqcode + "','1','')";


                                     sqltextlist.Add(sqltext);
                                 }
                                 else
                                 {
                                     usenum = num;
                                     usefznum = 0;
                                     sqltext = "insert into TBWS_STORAGE(SQ_CODE, SQ_MARID,SQ_LENGTH,SQ_WIDTH, " +
                                               "SQ_LOTNUM, SQ_PMODE, SQ_PTC, SQ_WAREHOUSE,SQ_LOCATION, " +
                                               "SQ_NUM,SQ_FZNUM,SQ_FIXED,SQ_ORDERID,SQ_NOTE,SQ_STATE) " +
                                               "select SQ_CODE='" + newsqcode + "', SQ_MARID,SQ_LENGTH, SQ_WIDTH, " +
                                               "SQ_LOTNUM, SQ_PMODE, SQ_PTC='" + newptcode + "', SQ_WAREHOUSE,SQ_LOCATION, " +
                                               "SQ_NUM='" + usenum + "',SQ_FZNUM=" + usefznum + ",SQ_FIXED,SQ_ORDERID,SQ_NOTE='',SQ_STATE  " +
                                               "from TBWS_STORAGE where SQ_PTC='备库' and SQ_CODE='" + oldsqcode + "'";
                                     sqltextlist.Add(sqltext);
                                     sqltext = "update TBWS_STORAGE set SQ_NUM=SQ_NUM-" + usenum + " where SQ_CODE='" + oldsqcode + "'";
                                     sqltextlist.Add(sqltext);
                                     sqltext = "insert into TBPC_MARREPLACEDETAIL(MP_CODE, MP_PTCODE, MP_NEWMARID, MP_NEWNUMA, " +
                                               "MP_NEWNUMB, MP_LENGTH, MP_WIDTH, MP_OLDSQCODE, MP_NEWSQCODE,MP_STATE,MP_NEWNOTE)  " +
                                               "values('" + pcode + "','" + ptcode + "','" + newmarid + "'," + usenum + "," + 
                                               usefznum + "," +length+","+width+",'"+oldsqcode+"','"+newsqcode+"','1','')";
                                     sqltextlist.Add(sqltext);
                                     num = 0;
                                 }
                                 allusenum = allusenum + usenum;
                                 allusefznum = allusefznum + usefznum;
                             }
                             else
                             {
                                 break;
                             }
                         }
                     }
                     else
                     {
                         allusenum = 0;
                         allusefznum = 0;
                     }
                }
                sqltext = "update TBPC_MARREPLACEALL set MP_USENUM='" + tbkun.Text + "'  where MP_PTCODE='" +ptcode+ "'";
                sqltextlist.Add(sqltext);
                //DBCallCommon.ExeSqlText(sqltext);
                DBCallCommon.ExecuteTrans(sqltextlist);
                YSTATE.Text = "1";//已保存
                YSTATETEXT.Text = "已保存";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('保存成功！');", true);
                tbpc_puralldydetailRepeaterdatabind();
                tbpc_marrepkumrepeaterdatabind();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该物料已占用库存，请先取消后再修改！');", true);
            }

        }
       
        protected void btn_back_click(object sender, EventArgs e)
        {
            Response.Redirect("~/PC_Data/PC_TBPC_Marreplace_all.aspx?mpcode=" + PNO.Text);
        }

        private double sum21 = 0;
        private double sum23 = 0;
        protected void tbpc_puralldydetailRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                if (((Label)e.Item.FindControl("KUNUM")).Text.ToString() == System.DBNull.Value.ToString() || ((Label)e.Item.FindControl("KUNUM")).Text.ToString() == System.String.Empty)
                {
                    ((Label)e.Item.FindControl("KUNUM")).Text = "0";
                }
                sum21 += Convert.ToDouble(((Label)e.Item.FindControl("KUNUM")).Text);

                if (((TextBox)e.Item.FindControl("XUSENUM")).Text.ToString() == System.DBNull.Value.ToString() || ((TextBox)e.Item.FindControl("XUSENUM")).Text.ToString() == System.String.Empty)
                {
                    ((TextBox)e.Item.FindControl("XUSENUM")).Text = "0";
                }
                sum23 += Convert.ToDouble(((TextBox)e.Item.FindControl("XUSENUM")).Text);

                if (YSTATE.Text == "0")
                {
                    ((TextBox)e.Item.FindControl("XUSENUM")).Visible = true;
                    ((Label)e.Item.FindControl("LXUSENUM")).Visible = false;
                }
                else
                {
                    ((TextBox)e.Item.FindControl("XUSENUM")).Visible = false;
                    ((Label)e.Item.FindControl("LXUSENUM")).Visible = true;
                }
                
            }
            else if (e.Item.ItemType == ListItemType.Footer)
            {
                ((TextBox)(e.Item.FindControl("tb_kntotal"))).Text = Convert.ToString(sum21);
                ((TextBox)(e.Item.FindControl("tb_kuntotal"))).Text = Convert.ToString(sum23);
            }
        }
        protected void tbpc_marrepkumrepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {

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

        protected void tb_marid_Textchanged(object sender, EventArgs e)
        {
            string marid = "";
            string sqltext = "";
            DataTable glotb = new DataTable();
            if (tb_marid.Text.ToString().Contains("|"))
            {
                marid = tb_marid.Text.Substring(0, tb_marid.Text.ToString().IndexOf("|"));
                sqltext = "SELECT SQ_MARID,MNAME,GUIGE,CAIZHI,GB FROM View_STORAGE_MATERIAL WHERE SQ_MARID='" + marid + "' ORDER BY SQ_MARID";

                glotb = DBCallCommon.GetDTUsingSqlText(sqltext);
                if (glotb.Rows.Count > 0)
                {
                    tb_marid.Text = marid;
                    tb_marnm.Text = glotb.Rows[0]["MNAME"].ToString();
                    tb_margg.Text = glotb.Rows[0]["GUIGE"].ToString();
                    tb_marcz.Text = glotb.Rows[0]["CAIZHI"].ToString();
                    tb_margb.Text = glotb.Rows[0]["GB"].ToString();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入的物料编码不存在，请重新输入！');", true);
                    return;
                }
            }
            else
            {
                if (!(tb_marid.Text == "" || tb_marid.Text == DBNull.Value.ToString()))
                {
                    sqltext = "SELECT SQ_MARID,MNAME,GUIGE,CAIZHI,GB  FROM View_STORAGE_MATERIAL WHERE SQ_MARID='" + tb_marid.Text + "' ORDER BY SQ_MARID";
                    glotb = DBCallCommon.GetDTUsingSqlText(sqltext);
                    if (glotb.Rows.Count > 0)
                    {
                        tb_marnm.Text = glotb.Rows[0]["MNAME"].ToString();
                        tb_margg.Text = glotb.Rows[0]["GUIGE"].ToString();
                        tb_marcz.Text = glotb.Rows[0]["CAIZHI"].ToString();
                        tb_margb.Text = glotb.Rows[0]["GB"].ToString();
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('仓库中没有该物料的库存，请重新输入！');", true);
                    }
                }
                else if(tb_marid.Text==""||tb_marid.Text==null)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('输入的物料编码不存在，请重新输入！');", true);
                    return;
                }
                
            }
        }
        //清除查询条件
        protected void Butbtn_clear_Click(object sender, EventArgs e)
        {
            tb_marid.Text = "";
            tb_marnm.Text = "";
            tb_margg.Text = "";
            tb_marcz.Text = "";
            tb_margb.Text = "";
            tbpc_marrepkumrepeaterdatabind();
        }
    }
}