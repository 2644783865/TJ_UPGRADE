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
using System.Collections.Generic;
using System.Drawing;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_HZY_info : System.Web.UI.Page
    {
        string tsa_id;
        string ma_unit="";
        string txt_tuhao = "";
        string txt_zongxu = "";
        string txt_ch_name = "";
        string txt_en_name = "";
        string txt_guige = "";
        string txt_tiaojianshuxing = "";
        string txt_beizhushuxing = "";
        string txt_cailiaoname = "";
        string txt_cailiaoguige = "";
        string txt_cailiaocd = "";
        string txt_cailiaokd = "";
        string txt_lilunzhl = "";
        string txt_cailiaodzh = "";
        string txt_cailiaozongzhong = "";
        string txt_cailiaozongchang = "";
        string txt_bgzmy = "";
        string txt_caizhi = "";
        string txt_tuzhicaizhi = "";
        string txt_tuzhibiaozhun = "";
        string txt_tuzhiwenti = "";
        string txt_tuzhidanzhong = "";
        string txt_jisuandanzhong = "";
        string txt_shuliang = "";
        string txt_xinzhuang = "";
        string txt_zhuangtai = "";
        string txt_pici = "";
        string txt_wgzht = "";
        string txt_beizhu = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            tsa_id = Request.QueryString["tsa_id"];
            if (!IsPostBack)
            {
                InitInfo();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_tuhao")).Attributes.Add("onkeypress", "EnterTextBox('btnlink')");
                }
            }
            if (GridView1.Rows.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
            }
        }

        //protected void FstBind()
        //{
        //    DataTable dt = GetDataFromGrid();
        //    for (int i = 0; i < 10; i++)
        //    {
        //        DataRow newRow = dt.NewRow();
        //        dt.Rows.Add(newRow);
        //    }
        //    dt.AcceptChanges();
        //    GridView1.DataSource = dt;
        //    GridView1.DataBind();
        //}
        private void InitInfo()
        {
            string strsql = "select * from TBPM_TCTSASSGN where TSA_ID='"+tsa_id+"'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(strsql);
            while (dr.Read())
            {
                lab_proname.Text = dr[2].ToString();
                lab_engname.Text = dr[3].ToString();
                lab_childname.Text = dr[1].ToString();
            }
            dr.Close();
            BindTestData();
        }
        protected void BindTestData()
        {
            string strsql = "select * from TBPM_HZYSTRINFO order by dbo.f_formatstr(HZY_ZONGXU, '.')";
            DBCallCommon.BindGridView(GridView1, strsql);
        }
        protected DataTable GetDataFromGrid()
        {
            DataTable dt1 = new DataTable("Table1");
            dt1.Columns.Add("HZY_ID");
            dt1.Columns.Add("HZY_TUHAO");
            dt1.Columns.Add("HZY_ZONGXU");
            dt1.Columns.Add("HZY_CHANAME");
            dt1.Columns.Add("HZY_ENGSHNAME");
            dt1.Columns.Add("HZY_GUIGE");
            dt1.Columns.Add("HZY_CONDICTIONATR");
            dt1.Columns.Add("HZY_BEIZHUATR");
            dt1.Columns.Add("HZY_MANAME");
            dt1.Columns.Add("HZY_MAGUIGE");
            dt1.Columns.Add("HZY_MALENGTH");
            dt1.Columns.Add("HZY_MAWIDTH");
            dt1.Columns.Add("HZY_THRYWGHT");
            dt1.Columns.Add("HZY_MAUNITWGHT");
            dt1.Columns.Add("HZY_MATOTALWGHT");
            dt1.Columns.Add("HZY_MATOTALLGTH");
            dt1.Columns.Add("HZY_MABGZMY");
            dt1.Columns.Add("HZY_MAQUALITY");
            dt1.Columns.Add("HZY_TUMAQLTY");
            dt1.Columns.Add("HZY_TUSTAD");
            dt1.Columns.Add("HZY_TUPROBLEM");
            dt1.Columns.Add("HZY_TUUNITWGHT");
            dt1.Columns.Add("HZY_CALUNITWGHT");
            dt1.Columns.Add("HZY_NUMBER");
            dt1.Columns.Add("HZY_MASHAPE");
            dt1.Columns.Add("HZY_MASTATE");
            dt1.Columns.Add("HZY_PICI");
            dt1.Columns.Add("HZY_FINSTATE");
            dt1.Columns.Add("HZY_NOTE");
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                DataRow newRow = dt1.NewRow();
                newRow[0] = ((Label)gRow.FindControl("lblID")).Text.ToString().Trim();
                newRow[1] = ((TextBox)gRow.FindControl("txt_tuhao")).Text;
                newRow[2] = ((TextBox)gRow.FindControl("txt_zongxu")).Text;
                newRow[3] = ((TextBox)gRow.FindControl("txt_ch_name")).Text;
                newRow[4] = ((TextBox)gRow.FindControl("txt_en_name")).Text;
                newRow[5] = ((TextBox)gRow.FindControl("txt_guige")).Text;
                newRow[6] = ((TextBox)gRow.FindControl("txt_tiaojianshuxing")).Text;
                newRow[7] = ((TextBox)gRow.FindControl("txt_beizhushuxing")).Text;
                newRow[8] = ((TextBox)gRow.FindControl("txt_cailiaoname")).Text;
                newRow[9] = ((TextBox)gRow.FindControl("txt_cailiaoguige")).Text;
                newRow[10] = ((TextBox)gRow.FindControl("txt_cailiaocd")).Text;
                newRow[11] = ((TextBox)gRow.FindControl("txt_cailiaokd")).Text;
                newRow[12] = ((TextBox)gRow.FindControl("txt_lilunzhl")).Text;
                newRow[13] = ((TextBox)gRow.FindControl("txt_cailiaodzh")).Text;
                newRow[14] = ((TextBox)gRow.FindControl("txt_cailiaozongzhong")).Text;
                newRow[15] = ((TextBox)gRow.FindControl("txt_cailiaozongchang")).Text;
                newRow[16] = ((TextBox)gRow.FindControl("txt_bgzmy")).Text;
                newRow[17] = ((TextBox)gRow.FindControl("txt_caizhi")).Text;
                newRow[18] = ((TextBox)gRow.FindControl("txt_tuzhicaizhi")).Text;
                newRow[19] = ((TextBox)gRow.FindControl("txt_tuzhibiaozhun")).Text;
                newRow[20] = ((TextBox)gRow.FindControl("txt_tuzhiwenti")).Text;
                newRow[21] = ((TextBox)gRow.FindControl("txt_tuzhidanzhong")).Text;
                newRow[22] = ((TextBox)gRow.FindControl("txt_jisuandanzhong")).Text;
                newRow[23] = ((TextBox)gRow.FindControl("txt_shuliang")).Text;
                newRow[24] = ((TextBox)gRow.FindControl("txt_xinzhuang")).Text;
                newRow[25] = ((TextBox)gRow.FindControl("txt_zhuangtai")).Text;
                newRow[26] = ((TextBox)gRow.FindControl("txt_pici")).Text;
                newRow[27] = ((TextBox)gRow.FindControl("txt_wgzht")).Text;
                newRow[28] = ((TextBox)gRow.FindControl("txt_beizhu")).Text;
                //newRow[8] = ((DropDownList)gRow.FindControl("DropDownList1")).SelectedValue;
                dt1.Rows.Add(newRow);
            }
            dt1.AcceptChanges();
            return dt1;
        }

        protected void btnadd_Click(object sender, EventArgs e)
        {
            if (txtnum.Text != "")
            {
                CreateNewRow(Convert.ToInt32(txtnum.Text));
                txtnum.Text = "";
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_tuhao")).Attributes.Add("onkeypress", "EnterTextBox('btnlink')");
                }
            }
            else
            {
                Response.Write("<script>alert('请输入行数！！！');history.go(-1)</script>");
            }
        }

        protected void btninsert_Click(object sender, EventArgs e)
        {
            //**********在指定位置插入行**********
            if (istid.Value == "1")
            {
                int count = 0;
                DataTable dt = this.GetDataFromGrid();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];
                    CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");
                    if (chk.Checked)
                    {
                        DataRow newRow = dt.NewRow();
                        dt.Rows.InsertAt(newRow, i + 1 + count);
                        count++;
                    }
                }
                istid.Value = "0";
                this.GridView1.DataSource = dt;
                this.GridView1.DataBind();
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    ((TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_tuhao")).Attributes.Add("onkeypress", "EnterTextBox('btnlink')");
                }
            }
        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            //***********删除数据不对数据库操作************
            int count = 0;
            DataTable dt = this.GetDataFromGrid();
            if (txtid.Value == "1")
            {
                for (int i = 0; i < GridView1.Rows.Count; i++)
                {
                    GridViewRow gRow = GridView1.Rows[i];
                    CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");
                    if (chk.Checked)
                    {
                        dt.Rows.RemoveAt(i - count);
                        count++;
                    }
                }
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            btnsubmit.Enabled = true;
            DataTable dt = GetDataFromGrid();
            //判定是否存在相同序号
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (GridView1.Rows[i].BackColor == System.Drawing.Color.Red)
                {
                    GridView1.Rows[i].BackColor = System.Drawing.Color.White;
                }
                else
                {
                    if (dt.Rows[i][2].ToString().Trim() != "")
                    {
                        for (int j = i + 1; j < dt.Rows.Count; j++)
                        {
                            if (dt.Rows[i][2].ToString().Trim() == dt.Rows[j][2].ToString().Trim())
                            {
                                bjid.Value = "1";
                                GridView1.Rows[i].BackColor = System.Drawing.Color.Red;
                                GridView1.Rows[j].BackColor = System.Drawing.Color.Red;
                                //Response.Write("<script>alert('数据库中不能存在相同序号，请确认！')</script>");
                                return;
                            }
                        }
                    }
                }
            }
            Deletebind();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (dt.Rows[i][2].ToString() != "")
                {
                    GridViewRow gRow = GridView1.Rows[i];
                    CheckBox chk = (CheckBox)gRow.FindControl("CheckBox1");
                    txt_tuhao = ((TextBox)gRow.FindControl("txt_tuhao")).Text;
                    txt_zongxu = ((TextBox)gRow.FindControl("txt_zongxu")).Text;
                    txt_ch_name = ((TextBox)gRow.FindControl("txt_ch_name")).Text;
                    txt_en_name = ((TextBox)gRow.FindControl("txt_en_name")).Text;
                    txt_guige = ((TextBox)gRow.FindControl("txt_guige")).Text;
                    txt_tiaojianshuxing = ((TextBox)gRow.FindControl("txt_tiaojianshuxing")).Text;
                    txt_beizhushuxing = ((TextBox)gRow.FindControl("txt_beizhushuxing")).Text;
                    txt_cailiaoname = ((TextBox)gRow.FindControl("txt_cailiaoname")).Text;
                    txt_cailiaoguige = ((TextBox)gRow.FindControl("txt_cailiaoguige")).Text;
                    txt_cailiaocd = ((TextBox)gRow.FindControl("txt_cailiaocd")).Text;
                    txt_cailiaokd = ((TextBox)gRow.FindControl("txt_cailiaokd")).Text;
                    txt_lilunzhl = ((TextBox)gRow.FindControl("txt_lilunzhl")).Text.Trim();
                    txt_cailiaodzh = ((TextBox)gRow.FindControl("txt_cailiaodzh")).Text;
                    txt_cailiaozongzhong = ((TextBox)gRow.FindControl("txt_cailiaozongzhong")).Text;
                    txt_cailiaozongchang = ((TextBox)gRow.FindControl("txt_cailiaozongchang")).Text;
                    txt_bgzmy = ((TextBox)gRow.FindControl("txt_bgzmy")).Text;
                    txt_caizhi = ((TextBox)gRow.FindControl("txt_caizhi")).Text;
                    txt_tuzhicaizhi = ((TextBox)gRow.FindControl("txt_tuzhicaizhi")).Text;
                    txt_tuzhibiaozhun = ((TextBox)gRow.FindControl("txt_tuzhibiaozhun")).Text;
                    txt_tuzhiwenti = ((TextBox)gRow.FindControl("txt_tuzhiwenti")).Text;
                    txt_tuzhidanzhong = ((TextBox)gRow.FindControl("txt_tuzhidanzhong")).Text.Trim();
                    txt_jisuandanzhong = ((TextBox)gRow.FindControl("txt_jisuandanzhong")).Text;
                    txt_shuliang = ((TextBox)gRow.FindControl("txt_shuliang")).Text;
                    txt_xinzhuang = ((TextBox)gRow.FindControl("txt_xinzhuang")).Text;
                    txt_zhuangtai = ((TextBox)gRow.FindControl("txt_zhuangtai")).Text;
                    txt_pici = ((TextBox)gRow.FindControl("txt_pici")).Text;
                    txt_wgzht = ((TextBox)gRow.FindControl("txt_wgzht")).Text;
                    txt_beizhu = ((TextBox)gRow.FindControl("txt_beizhu")).Text;
                    Insertbind();
                }
            }
            BindTestData();
        }
        //材料计划需用表
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                txt_zongxu = ((TextBox)gRow.FindControl("txt_zongxu")).Text;
                txt_guige = ((TextBox)gRow.FindControl("txt_guige")).Text;
                txt_wgzht = ((TextBox)gRow.FindControl("txt_wgzht")).Text;
                if (txt_guige != "" && txt_wgzht == "0")
                {
                    string str = "update TBPM_HZYSTRINFO set HZY_FINSTATE='1' where HZY_ZONGXU='" + txt_zongxu + "'";
                    DBCallCommon.ExeSqlText(str);
                    string ma_id = "TBPM_HZYSTRINFO" + "_" + ((Label)gRow.FindControl("lblID")).Text;
                    string ma_name = ((TextBox)gRow.FindControl("txt_cailiaoname")).Text;
                    string ma_hmcode = ((TextBox)gRow.FindControl("txt_tuhao")).Text;
                    string ma_guige = ((TextBox)gRow.FindControl("txt_cailiaoguige")).Text;
                    string ma_caizhi = ((TextBox)gRow.FindControl("txt_caizhi")).Text;
                    string ma_num = ((TextBox)gRow.FindControl("txt_cailiaozongzhong")).Text;
                    if (ma_name == "钢板" || ma_name == "钢格板")
                    {
                        ma_unit = "kg";
                    }
                    else
                    {
                        ma_unit = "kg/m";
                    }
                    string sql = "insert into TBPM_MATERIALPLAN (MP_MATID,MP_NAME,MP_HRCODE,MP_GUIGE,MP_CAIZHI,MP_UNIT,MP_NUMBER) values ('" + ma_id + "','" + ma_name + "','" + ma_hmcode + "','" + ma_guige + "','" + ma_caizhi + "','" + ma_unit + "','" + ma_num + "')";
                    DBCallCommon.ExeSqlText(sql);
                }
            }
            Response.Redirect("TM_HZY_pur.aspx");
        }


        //生成输入行函数
        private void CreateNewRow(int num)
        {
            DataTable dt = this.GetDataFromGrid();
            for (int i = 0; i < num; i++)
            {
                DataRow newRow = dt.NewRow();
                dt.Rows.Add(newRow);
            }
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
        }
        protected void Deletebind()
        {
            string sql = "delete from TBPM_HZYSTRINFO ";
            DBCallCommon.ExeSqlText(sql);
        }
        protected void Insertbind()
        {
            string strsql = "insert into TBPM_HZYSTRINFO";
            strsql+="(HZY_TUHAO,HZY_ZONGXU,HZY_CHANAME,HZY_ENGSHNAME,HZY_GUIGE,HZY_CONDICTIONATR,HZY_BEIZHUATR,HZY_MANAME,HZY_MAGUIGE,HZY_MALENGTH,HZY_MAWIDTH,HZY_THRYWGHT,HZY_MAUNITWGHT,HZY_MATOTALWGHT,HZY_MATOTALLGTH,HZY_MABGZMY,HZY_MAQUALITY,HZY_NUMBER,HZY_MASHAPE,HZY_MASTATE,HZY_PICI,HZY_FINSTATE,HZY_NOTE)";
            strsql += "values ('" + txt_tuhao + "','" + txt_zongxu + "','" + txt_ch_name + "','" + txt_en_name + "','" + txt_guige + "','" + txt_tiaojianshuxing + "','" + txt_beizhushuxing + "','" + txt_cailiaoname + "','" + txt_cailiaoguige + "','" + txt_cailiaocd + "','" + txt_cailiaokd + "','" + txt_lilunzhl + "','" + txt_cailiaodzh + "','" + txt_cailiaozongzhong + "','" + txt_cailiaozongchang + "','" + txt_bgzmy + "','" + txt_caizhi + "','" + txt_shuliang + "','" + txt_xinzhuang + "','" + txt_zhuangtai + "','" + txt_pici + "','" + txt_wgzht + "','" + txt_beizhu + "')";
            DBCallCommon.ExeSqlText(strsql);
        }

        protected void btnmp_Click(object sender, EventArgs e)
        {
            GridView1.Columns[8].Visible=true;
            GridView1.Columns[9].Visible = true;
            GridView1.Columns[10].Visible = true;
            GridView1.Columns[11].Visible = true;
            GridView1.Columns[12].Visible = true;
            GridView1.Columns[13].Visible = true;
            GridView1.Columns[14].Visible = true;
            GridView1.Columns[15].Visible = true;
            GridView1.Columns[16].Visible = true;
            GridView1.Columns[17].Visible = true;
            GridView1.Columns[18].Visible = true;
        }

        protected void btntzp_Click(object sender, EventArgs e)
        {
            GridView1.Columns[20].Visible = true;
            GridView1.Columns[21].Visible = true;
            GridView1.Columns[22].Visible = true;
            GridView1.Columns[23].Visible = true;
            GridView1.Columns[24].Visible = true;
        }

        protected void btnmh_Click(object sender, EventArgs e)
        {
            GridView1.Columns[8].Visible = false;
            GridView1.Columns[9].Visible = false;
            GridView1.Columns[10].Visible = false;
            GridView1.Columns[11].Visible = false;
            GridView1.Columns[12].Visible = false;
            GridView1.Columns[13].Visible = false;
            GridView1.Columns[14].Visible = false;
            GridView1.Columns[15].Visible = false;
            GridView1.Columns[16].Visible = false;
            GridView1.Columns[17].Visible = false;
            GridView1.Columns[18].Visible = false;
        }

        protected void btntzh_Click(object sender, EventArgs e)
        {
            GridView1.Columns[20].Visible = false;
            GridView1.Columns[21].Visible = false;
            GridView1.Columns[22].Visible = false;
            GridView1.Columns[23].Visible = false;
            GridView1.Columns[24].Visible = false;
        }

        protected void btnlink_Click(object sender, EventArgs e)
        {
            float shuliang = 0;
            float cd = 0;
            float kd=0;
            float j = 0;
            //DataTable dt = GetDataFromGrid();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                txt_tuhao = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_tuhao")).Text.Trim();
                txt_zongxu = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_zongxu")).Text.Trim();
                if (txt_tuhao == "")
                {
                    txt_ch_name = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_ch_name")).Text.Trim();
                    string str1 = "insert into TBPM_HZYSTRINFO (HZY_ZONGXU,HZY_CHANAME) values ('" + txt_zongxu + "','" + txt_ch_name + "')";
                    DBCallCommon.ExeSqlText(str1);
                }
                if (txt_tuhao != "")
                {
                    txt_en_name = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_en_name")).Text.Trim();
                    txt_cailiaocd = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_cailiaocd")).Text.Trim();
                    txt_cailiaokd = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_cailiaokd")).Text;
                    txt_shuliang = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_shuliang")).Text.Trim();
                    txt_zhuangtai = ((TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_wgzht")).Text.Trim();
                    if (txt_shuliang == "")
                    {
                        shuliang = 0;
                    }
                    else
                    {
                        shuliang = float.Parse(txt_shuliang);
                    }
                    if (txt_cailiaocd != "")
                    {
                        cd = float.Parse(txt_cailiaocd) / 1000;
                    }
                    else
                    {
                        txt_cailiaocd = "0";
                        cd = float.Parse(txt_cailiaocd);
                    }
                    if (txt_cailiaokd != "")
                    {
                        kd = float.Parse(txt_cailiaokd) / 1000;
                    }
                    else
                    {
                        txt_cailiaokd = "0";
                        kd = float.Parse(txt_cailiaokd);
                    }
                    string strsql = "select RM_NAME,RM_MWEIGHT,RM_MAREA,RM_GUIGE,RM_CAIZHI,RM_HMCODE from TBMA_RAWMAINFO where RM_HMCODE='" + txt_tuhao + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(strsql);
                    while (dr.Read())
                    {
                        //DataRow da = dt.NewRow();
                        txt_ch_name = dr[0].ToString().Trim();
                        txt_cailiaoname = dr[0].ToString().Trim();
                        txt_lilunzhl = dr[1].ToString().Trim();
                        txt_cailiaoguige = dr[3].ToString().Trim();
                        txt_caizhi = dr[4].ToString().Trim();
                        txt_tuhao = dr[5].ToString().Trim();
                        if (dr[0].ToString().Trim() == "钢板" || dr[0].ToString().Trim() == "钢格板")
                        {
                            j = float.Parse("1.1");
                            txt_guige = dr[3].ToString().Trim() + "X" + ((TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_cailiaokd")).Text + "+" + txt_cailiaocd;
                            txt_cailiaodzh = (float.Parse(dr[1].ToString().Trim()) * cd * kd * (float.Parse(txt_cailiaoguige))).ToString("0.00");
                            txt_cailiaozongzhong = (float.Parse(dr[1].ToString().Trim()) * cd * kd * (float.Parse(txt_cailiaoguige)) * shuliang * j).ToString("0.00");
                            txt_cailiaozongchang = "";
                        }
                        else
                        {
                            j = float.Parse("1.05");
                            txt_guige = dr[3].ToString().Trim() + "+" + txt_cailiaocd;
                            txt_cailiaodzh = (float.Parse(dr[1].ToString().Trim()) * cd).ToString("0.00");
                            txt_cailiaozongzhong = (float.Parse(dr[1].ToString().Trim()) * cd * shuliang * j).ToString("0.00");
                            txt_cailiaozongchang = (cd * 1000 * shuliang * 1.05).ToString("0.00");
                        }
                        #region
                        //da[1] = txt_tuhao;
                        //da[2] = txt_zongxu;
                        //da[3] = txt_ch_name;
                        //da[4] = txt_en_name;
                        //da[5] = txt_guige;
                        //da[8] = txt_cailiaoname;
                        //da[9] = txt_cailiaoguige;
                        //da[10] = txt_cailiaocd;
                        //da[11] = txt_cailiaokd;
                        //da[12]=txt_lilunzhl;
                        //da[13] = txt_cailiaodzh;
                        //da[14] = txt_cailiaozongzhong;
                        //da[15] = txt_cailiaozongchang;
                        //da[17] = txt_caizhi;
                        //da[23] = txt_shuliang;
                        //da[27] = txt_zhuangtai;
                        //dt.Rows.Add(da);
                        #endregion
                        string sql = "insert into TBPM_HZYSTRINFO (HZY_TUHAO,HZY_ZONGXU,HZY_CHANAME,HZY_GUIGE,HZY_MANAME,HZY_MAGUIGE,HZY_MALENGTH,HZY_MAWIDTH,HZY_THRYWGHT,HZY_MAUNITWGHT,HZY_MATOTALWGHT,HZY_MATOTALLGTH,HZY_MAQUALITY,HZY_NUMBER,HZY_FINSTATE) values ('" + txt_tuhao + "','" + txt_zongxu + "','" + txt_ch_name + "','" + txt_guige + "','" + txt_cailiaoname + "','" + txt_cailiaoguige + "','" + txt_cailiaocd + "','" + txt_cailiaokd + "','" + txt_lilunzhl + "','" + txt_cailiaodzh + "','" + txt_cailiaozongzhong + "','" + txt_cailiaozongchang + "','" + txt_caizhi + "','" + txt_shuliang + "','" + txt_zhuangtai + "')";
                        DBCallCommon.ExeSqlText(sql);
                    }
                    dr.Close();
                }
                
            }
            //GridView1.DataSource = dt;
            //GridView1.DataBind();
            //以总序为标准删除重复记录
            string str = "delete from TBPM_HZYSTRINFO where HZY_ZONGXU in (select HZY_ZONGXU from TBPM_HZYSTRINFO group by HZY_ZONGXU,HZY_ZONGXU having count(*)>1) and HZY_ID not in (select MAX(HZY_ID) from TBPM_HZYSTRINFO group by HZY_ZONGXU,HZY_ZONGXU having count(*)>1)";
            DBCallCommon.ExeSqlText(str);
            BindTestData();
        }

        protected void btnnp_Click(object sender, EventArgs e)
        {
            GridView1.Columns[6].Visible = true;
        }

        protected void btnnh_Click(object sender, EventArgs e)
        {
            GridView1.Columns[6].Visible = false;
        }

        protected void btnunfold_Click(object sender, EventArgs e)
        {
            DataTable dt = GetDataFromGrid();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                GridViewRow gr = GridView1.Rows[i];
                CheckBox chk = (CheckBox)gr.FindControl("CheckBox1");
                txt_zongxu = ((TextBox)gr.FindControl("txt_zongxu")).Text;
                if (chk.Checked && txt_zongxu != "")
                {
                    chk.Checked = false;
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        string txt_tg = ((TextBox)GridView1.Rows[j].FindControl("txt_zongxu")).Text;
                        if (txt_zongxu.Length != txt_tg.Length)
                        {
                            GridView1.Rows[j].Visible = true;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
                bjid.Value = "0";
            }
        }

        protected void btnfold_Click(object sender, EventArgs e)
        {
            DataTable dt = GetDataFromGrid();
            for (int i = 0; i < dt.Rows.Count;i++)
            {
                GridViewRow gr=GridView1.Rows[i];
                CheckBox chk = (CheckBox)gr.FindControl("CheckBox1");
                txt_zongxu = ((TextBox)gr.FindControl("txt_zongxu")).Text;
                if (chk.Checked&&txt_zongxu!="")
                {
                    bjid.Value = "1";
                    GridView1.Rows[i].BackColor = System.Drawing.Color.Purple;
                    for (int j = i + 1; j < dt.Rows.Count; j++)
                    {
                        string txt_tg=((TextBox)GridView1.Rows[j].FindControl("txt_zongxu")).Text;
                        if (txt_zongxu.Length != txt_tg.Length)
                        {
                            GridView1.Rows[j].Visible = false;
                        }
                        else
                        {
                            return;
                        }
                    }
                }
            }
        }

        protected void btnselectall_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                ((CheckBox)gRow.FindControl("CheckBox1")).Checked = true;
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                GridViewRow gRow = GridView1.Rows[i];
                ((CheckBox)gRow.FindControl("CheckBox1")).Checked = false;
            }
        }

        protected void btnInto_Click(object sender, EventArgs e)
        {
            //不对数据库操作
            DataTable dt = GetDataFromGrid();
            string sql = "select PDS_NAME,PDS_ENGNAME,PDS_CODE from TBPD_STRUINFO where PDS_NAME='"+txtInto.Text+"'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            while (dr.Read())
            {
                DataRow da = dt.NewRow();
                txt_ch_name=dr[0].ToString();
                txt_en_name = dr[1].ToString();
                txt_zongxu = dr[2].ToString();
                da[3] = txt_ch_name;
                da[4] = txt_en_name;
                da[2] = txt_zongxu;
                dt.Rows.Add(da);
                //string str = "insert into TBPM_HZYSTRINFO (HZY_ZONGXU,HZY_CHANAME,HZY_ENGSHNAME) values ('" + txt_zongxu + "','" + txt_ch_name + "','" + txt_en_name + "')";
                //DBCallCommon.ExeSqlText(str);
            }
            dr.Close();
            string strsql = "select PDS_NAME,PDS_ENGNAME,PDS_CODE from TBPD_STRUINFO where PDS_CODE like '" + txt_zongxu + "." + "%' order by dbo.f_formatstr(PDS_CODE, '.')";
            SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(strsql);
            while (dr1.Read())
            {
                DataRow da = dt.NewRow();
                txt_ch_name = dr1[0].ToString();
                txt_en_name = dr1[1].ToString();
                txt_zongxu = dr1[2].ToString();
                da[3] = txt_ch_name;
                da[4] = txt_en_name;
                da[2] = txt_zongxu;
                dt.Rows.Add(da);
                //string str = "insert into TBPM_HZYSTRINFO (HZY_ZONGXU,HZY_CHANAME,HZY_ENGSHNAME) values ('" + txt_zongxu + "','" + txt_ch_name + "','" + txt_en_name + "')";
                //DBCallCommon.ExeSqlText(str);
            }
            dr1.Close();
            this.GridView1.DataSource = dt;
            this.GridView1.DataBind();
            for (int i = 0; i < GridView1.Rows.Count; i++)
            {
                ((TextBox)GridView1.Rows[i].Cells[0].FindControl("txt_tuhao")).Attributes.Add("onkeypress", "EnterTextBox('btnlink')");
            }
            //BindTestData();
            txtInto.Text = "";
        }
    }
}
