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

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_fayun_check_detail : System.Web.UI.Page
    {
        #region  全局变量定义
        //全局变量定义

        DataTable dt = new DataTable();
        public string gloabsheetno
        {
            get
            {
                object str = ViewState["gloabsheetno"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabsheetno"] = value;
            }
        }
        public string Gys1
        {
            get
            {
                object str = ViewState["Gys1"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Gys1"] = value;
            }
        }
        public string Gys2
        {
            get
            {
                object str = ViewState["Gys2"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Gys2"] = value;
            }
        }
        public string Gys3
        {
            get
            {
                object str = ViewState["Gys3"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Gys3"] = value;
            }
        }
        public string Gys4
        {
            get
            {
                object str = ViewState["Gys4"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Gys4"] = value;
            }
        }
        public string Gys5
        {
            get
            {
                object str = ViewState["Gys5"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Gys5"] = value;
            }
        }
        public string Gys6
        {
            get
            {
                object str = ViewState["Gys6"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Gys6"] = value;
            }
        }
        public string Globgysnum
        {
            get
            {
                object str = ViewState["Globgysnum"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["Globgysnum"] = value;
            }
        }
        #endregion
        string bianhao;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Initpage();
                gysnum();
                checked_detailRepeaterdatabind();
                initpower();
            }
            //CheckUser(ControlFinder);
        }
        private void Initpage()
        {

            if (Request.QueryString["sheetno"] != null)
            {
                gloabsheetno = Request.QueryString["sheetno"].ToString();
            }
            else
            {
                gloabsheetno = "";
            }
            //if (Request.QueryString["bianhao"] != null)
            //{
            //   bianhao= Request.QueryString["bianhao"].ToString();
            //}

            TextBoxNo.Text = gloabsheetno;//单号
            string sqltext = "select ICL_REVIEWA,zdrnm  as ICL_REVIEWANM,ICL_REVIEWATIME,ICL_REVIEWAADVC," +
                     "ICL_REVIEWB,shbnm AS ICL_REVIEWBNM,ICL_REVIEWBTIME,ICL_REVIEWBADVC," +
                     "ICL_REVIEWC,shcnm as ICL_REVIEWCNM,ICL_REVIEWCTIME,ICL_REVIEWCADVC," +
                     "ICL_REVIEWD,shdnm as ICL_REVIEWDNM,ICL_REVIEWDTIME,ICL_REVIEWDADVC," +
                     "ICL_REVIEWE,shenm as ICL_REVIEWENM,ICL_REVIEWETIME,ICL_REVIEWEADVC," +
                     "ICL_REVIEWF,shfnm as ICL_REVIEWFNM,ICL_REVIEWFTIME,ICL_REVIEWFADVC," +
                     "ICL_REVIEWG,shgnm as ICL_REVIEWGNM,ICL_REVIEWGTIME,ICL_REVIEWGADVC," +
                     "isnull(ICL_STATE,0) as ICL_STATE,isnull(ICL_STATEA,0) as ICL_STATEA,isnull(ICL_STATEB,0) as ICL_STATEB,isnull(ICL_STATEC,0) as ICL_STATEC,isnull(ICL_STATED,0) as ICL_STATED,isnull(ICL_STATEE,0) as ICL_STATEE,isnull(ICL_STATEF,0) as ICL_STATEF " +
                     "from View_TBMP_FAYUNPRCRVW  where picno='" + gloabsheetno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                TB_zdanrenid.Text = dt.Rows[0]["ICL_REVIEWA"].ToString();
                Tb_zdanren.Text = dt.Rows[0]["ICL_REVIEWANM"].ToString();
                Tb_tjiaot.Text = dt.Rows[0]["ICL_REVIEWATIME"].ToString();
                Tb_zdanyj.Text = dt.Rows[0]["ICL_REVIEWAADVC"].ToString();

                Tb_shenheren1.Text = dt.Rows[0]["ICL_REVIEWBNM"].ToString();
                Tb_shenherencode1.Text = dt.Rows[0]["ICL_REVIEWB"].ToString();
                Tb_shenhet1.Text = dt.Rows[0]["ICL_REVIEWBTIME"].ToString();
                Tb_shenheyj1.Text = dt.Rows[0]["ICL_REVIEWBADVC"].ToString();

                if (dt.Rows[0]["ICL_STATE"].ToString() == "2" && dt.Rows[0]["ICL_STATEA"].ToString() == "1" && dt.Rows[0]["ICL_STATEB"].ToString() == "0" && dt.Rows[0]["ICL_STATEC"].ToString() == "0" && dt.Rows[0]["ICL_STATED"].ToString() == "0" && dt.Rows[0]["ICL_STATEE"].ToString() == "0" && dt.Rows[0]["ICL_STATEF"].ToString() == "0")
                {
                    Rad_butongyi1.Checked = true;
                }
                else if (dt.Rows[0]["ICL_STATE"].ToString() == "3" || dt.Rows[0]["ICL_STATE"].ToString() == "4")
                {
                    Rad_tongyi1.Checked = true;
                }
                else
                {
                    Rad_butongyi1.Checked = false;
                    Rad_tongyi1.Checked = false;
                }

                Tb_shenheren2.Text = dt.Rows[0]["ICL_REVIEWCNM"].ToString();
                Tb_shenherencode2.Value = dt.Rows[0]["ICL_REVIEWC"].ToString();
                Tb_shenhet2.Text = dt.Rows[0]["ICL_REVIEWCTIME"].ToString();
                Tb_shenheyj2.Text = dt.Rows[0]["ICL_REVIEWCADVC"].ToString();
                if (dt.Rows[0]["ICL_STATEB"].ToString() == "1")
                {
                    Rad_butongyi2.Checked = true;

                }
                else if (dt.Rows[0]["ICL_STATEB"].ToString() == "2")
                {
                    Rad_tongyi2.Checked = true;
                }
                else
                {
                    Rad_butongyi2.Checked = false;
                    Rad_tongyi2.Checked = false;
                }

                Tb_shenheren3.Text = dt.Rows[0]["ICL_REVIEWDNM"].ToString();
                Tb_shenherencode3.Text = dt.Rows[0]["ICL_REVIEWD"].ToString();
                Tb_shenhet3.Text = dt.Rows[0]["ICL_REVIEWDTIME"].ToString();
                Tb_shenheyj3.Text = dt.Rows[0]["ICL_REVIEWDADVC"].ToString();
                //if (dt.Rows[0]["ICL_STATEC"].ToString() == "1")
                //{
                //    Rad_butongyi3.Checked = true;

                //}
                //else if (dt.Rows[0]["ICL_STATEC"].ToString() == "2")
                //{
                //    Rad_tongyi3.Checked = true;
                //}
                //else
                //{
                //    Rad_butongyi3.Checked = false;
                //    Rad_tongyi3.Checked = false;
                //}
            }
            if ((dt.Rows[0]["ICL_STATE"].ToString() == "0" || dt.Rows[0]["ICL_STATE"].ToString() == "2") && Session["UserID"].ToString() == dt.Rows[0]["ICL_REVIEWA"].ToString())
            {
                btn_edit.Enabled = true;
            }
            else
            {
                btn_edit.Enabled = false;

            }
        }
        //供应商数量
        private void gysnum()
        {
            int i = 0;
            string sql = "select distinct PM_SUPPLIERAID,PM_SUPPLIERBID,PM_SUPPLIERCID,PM_SUPPLIERDID,PM_SUPPLIEREID,PM_SUPPLIERFID FROM TBMP_FAYUNPRICE WHERE PM_SHEETNO='" + gloabsheetno + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr.Read())
            {
                string SA = dr["PM_SUPPLIERAID"].ToString();
                string SB = dr["PM_SUPPLIERBID"].ToString();
                string SC = dr["PM_SUPPLIERCID"].ToString();
                string SD = dr["PM_SUPPLIERDID"].ToString();
                string SE = dr["PM_SUPPLIEREID"].ToString();
                string SF = dr["PM_SUPPLIERFID"].ToString();
                dr.Close();
                if (SA != "" && SA != null)
                {
                    i++;
                    Gys1 = "yes";
                }
                else
                {
                    Gys1 = "no";
                }
                if (SB != "" && SB != null)
                {
                    i++;
                    Gys2 = "yes";
                }
                else
                {
                    Gys2 = "no";
                }
                if (SC != "" && SC != null)
                {
                    i++;
                    Gys3 = "yes";
                }
                else
                {
                    Gys3 = "no";
                }
                if (SD != "" && SD != null)
                {
                    i++;
                    Gys4 = "yes";
                }
                else
                {
                    Gys4 = "no";
                }
                if (SE != "" && SE != null)
                {
                    i++;
                    Gys5 = "yes";
                }
                else
                {
                    Gys5 = "no";
                }
                if (SF != "" && SF != null)
                {
                    i++;
                    Gys6 = "yes";
                }
                else
                {
                    Gys6 = "no";
                }
                if (i == 0)
                {
                    TextBox1.Text = "3";
                    Globgysnum = "0";
                }
                else
                {
                    TextBox1.Text = Convert.ToString(i);
                    Globgysnum = Convert.ToString(i);
                }
            }
        }
        private void checked_detailRepeaterdatabind()
        {
            ItemTemplatedind();
            HeaderTemplatebind();
        }

        private void ItemTemplatedind()
        {
            string sqltext = "";
            sqltext = "select B.CM_ID, B.CM_FID,B.CM_BIANHAO, B.CM_CONTR,A.TSA_ID,PM_ENGNAME,C.CM_PROJ,C.TSA_PJID,B.CM_JHTIME,B.CM_CUSNAME,PM_MAP,B.TSA_NUMBER,A.PM_ZONGXU,A.PM_SUPPLIERRESID,A.PM_FHDETAIL,A.PM_FYNUM,A.supplierresnm,A.supplierresrank,A.PM_SHUILV,A.PM_WEIGHT,A.PM_LENGTH,PM_AVGA,A.PM_AVGB,A.PM_AVGC,A.PM_AVGD,A.PM_AVGE,A.PM_AVGF,A.PM_ADDRESS," +
                "CONVERT(float, A.price) AS price,A.PM_QOUTEFSTSA,CONVERT(float, A.PM_QOUTELSTSA) AS PM_QOUTELSTSA,A.PM_QOUTEFSTSB,CONVERT(float, A.PM_QOUTELSTSB) AS PM_QOUTELSTSB," +
                "A.PM_QOUTEFSTSC,CONVERT(float, A.PM_QOUTELSTSC) AS PM_QOUTELSTSC,A.PM_QOUTEFSTSD,CONVERT(float, A.PM_QOUTELSTSD) AS PM_QOUTELSTSD," +
                "A.PM_QOUTEFSTSE,CONVERT(float, A.PM_QOUTELSTSE) AS PM_QOUTELSTSE,A.PM_QOUTEFSTSF,CONVERT(float, A.PM_QOUTELSTSF) AS PM_QOUTELSTSF," +
                "isnull(A.PM_STATE,0) as detailstate,isnull(A.PM_CSTATE,0) as detailcstate,A.PM_NOTE FROM View_TBMP_FAYUNPRICE as A left outer join View_CM_FaHuo as B on  (A.TSA_ID=B.TSA_ID and  A.PM_ZONGXU=B.ID and A.PM_FID=B.CM_FID) left join View_CM_TSAJOINPROJ as C on a.TSA_ID=C.TSA_ID where PM_SHEETNO='" + gloabsheetno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            checked_detailRepeater.DataSource = dt;
            checked_detailRepeater.DataBind();

            #region 合并单元格
            for (int i = checked_detailRepeater.Items.Count - 1; i > 0; i--)
            {
                HtmlTableCell oCell_previous1 = checked_detailRepeater.Items[i - 1].FindControl("td_supply") as HtmlTableCell;
                HtmlTableCell oCell1 = checked_detailRepeater.Items[i].FindControl("td_supply") as HtmlTableCell;
                if (oCell_previous1 != null && oCell1 != null)
                {
                    oCell1.RowSpan = (oCell1.RowSpan == -1) ? 1 : oCell1.RowSpan;
                    oCell_previous1.RowSpan = (oCell_previous1.RowSpan == -1) ? 1 : oCell_previous1.RowSpan;
                }
                if (oCell1.InnerText == oCell_previous1.InnerText)
                {
                    oCell1.Visible = false;
                    oCell_previous1.RowSpan += oCell1.RowSpan;
                }


                HtmlTableCell oCell_previous2 = checked_detailRepeater.Items[i - 1].FindControl("dycbj1") as HtmlTableCell;
                HtmlTableCell oCell2 = checked_detailRepeater.Items[i].FindControl("dycbj1") as HtmlTableCell;
                if (oCell_previous2 != null && oCell2 != null)
                {
                    oCell2.RowSpan = (oCell2.RowSpan == -1) ? 1 : oCell2.RowSpan;
                    oCell_previous2.RowSpan = (oCell_previous2.RowSpan == -1) ? 1 : oCell_previous2.RowSpan;
                }
                if (oCell2.InnerText == oCell_previous2.InnerText)
                {
                    oCell2.Visible = false;
                    oCell_previous2.RowSpan += oCell2.RowSpan;
                }

                HtmlTableCell oCell_previous3 = checked_detailRepeater.Items[i - 1].FindControl("zzbj1") as HtmlTableCell;
                HtmlTableCell oCell3 = checked_detailRepeater.Items[i].FindControl("zzbj1") as HtmlTableCell;
                if (oCell_previous3 != null && oCell3 != null)
                {
                    oCell3.RowSpan = (oCell3.RowSpan == -1) ? 1 : oCell3.RowSpan;
                    oCell_previous3.RowSpan = (oCell_previous3.RowSpan == -1) ? 1 : oCell_previous3.RowSpan;
                }
                if (oCell3.InnerText == oCell_previous3.InnerText)
                {
                    oCell3.Visible = false;
                    oCell_previous3.RowSpan += oCell3.RowSpan;
                }

                HtmlTableCell oCell_previous4 = checked_detailRepeater.Items[i - 1].FindControl("dycbj2") as HtmlTableCell;
                HtmlTableCell oCell4 = checked_detailRepeater.Items[i].FindControl("dycbj2") as HtmlTableCell;
                if (oCell_previous4 != null && oCell4 != null)
                {
                    oCell4.RowSpan = (oCell4.RowSpan == -1) ? 1 : oCell4.RowSpan;
                    oCell_previous4.RowSpan = (oCell_previous4.RowSpan == -1) ? 1 : oCell_previous4.RowSpan;
                }
                if (oCell4.InnerText == oCell_previous4.InnerText)
                {
                    oCell4.Visible = false;
                    oCell_previous4.RowSpan += oCell4.RowSpan;
                }

                HtmlTableCell oCell_previous5 = checked_detailRepeater.Items[i - 1].FindControl("zzbj2") as HtmlTableCell;
                HtmlTableCell oCell5 = checked_detailRepeater.Items[i].FindControl("zzbj2") as HtmlTableCell;
                if (oCell_previous5 != null && oCell5 != null)
                {
                    oCell5.RowSpan = (oCell5.RowSpan == -1) ? 1 : oCell5.RowSpan;
                    oCell_previous5.RowSpan = (oCell_previous5.RowSpan == -1) ? 1 : oCell_previous5.RowSpan;
                }
                if (oCell5.InnerText == oCell_previous5.InnerText)
                {
                    oCell5.Visible = false;
                    oCell_previous5.RowSpan += oCell5.RowSpan;
                }

                HtmlTableCell oCell_previous6 = checked_detailRepeater.Items[i - 1].FindControl("dycbj3") as HtmlTableCell;
                HtmlTableCell oCell6 = checked_detailRepeater.Items[i].FindControl("dycbj3") as HtmlTableCell;
                if (oCell_previous6 != null && oCell6 != null)
                {
                    oCell6.RowSpan = (oCell6.RowSpan == -1) ? 1 : oCell6.RowSpan;
                    oCell_previous6.RowSpan = (oCell_previous6.RowSpan == -1) ? 1 : oCell_previous6.RowSpan;
                }
                if (oCell6.InnerText == oCell_previous6.InnerText)
                {
                    oCell6.Visible = false;
                    oCell_previous6.RowSpan += oCell6.RowSpan;
                }

                HtmlTableCell oCell_previous7 = checked_detailRepeater.Items[i - 1].FindControl("zzbj3") as HtmlTableCell;
                HtmlTableCell oCell7 = checked_detailRepeater.Items[i].FindControl("zzbj3") as HtmlTableCell;
                if (oCell_previous7 != null && oCell7 != null)
                {
                    oCell7.RowSpan = (oCell7.RowSpan == -1) ? 1 : oCell7.RowSpan;
                    oCell_previous7.RowSpan = (oCell_previous7.RowSpan == -1) ? 1 : oCell_previous7.RowSpan;
                }
                if (oCell7.InnerText == oCell_previous7.InnerText)
                {
                    oCell7.Visible = false;
                    oCell_previous7.RowSpan += oCell7.RowSpan;
                }

                HtmlTableCell oCell_previous8 = checked_detailRepeater.Items[i - 1].FindControl("dycbj4") as HtmlTableCell;
                HtmlTableCell oCell8 = checked_detailRepeater.Items[i].FindControl("dycbj4") as HtmlTableCell;
                if (oCell_previous8 != null && oCell8 != null)
                {
                    oCell8.RowSpan = (oCell8.RowSpan == -1) ? 1 : oCell8.RowSpan;
                    oCell_previous8.RowSpan = (oCell_previous8.RowSpan == -1) ? 1 : oCell_previous8.RowSpan;
                }
                if (oCell8.InnerText == oCell_previous8.InnerText)
                {
                    oCell8.Visible = false;
                    oCell_previous8.RowSpan += oCell8.RowSpan;
                }

                HtmlTableCell oCell_previous9 = checked_detailRepeater.Items[i - 1].FindControl("zzbj4") as HtmlTableCell;
                HtmlTableCell oCell9 = checked_detailRepeater.Items[i].FindControl("zzbj4") as HtmlTableCell;
                if (oCell_previous9 != null && oCell9 != null)
                {
                    oCell9.RowSpan = (oCell9.RowSpan == -1) ? 1 : oCell9.RowSpan;
                    oCell_previous9.RowSpan = (oCell_previous9.RowSpan == -1) ? 1 : oCell_previous9.RowSpan;
                }
                if (oCell9.InnerText == oCell_previous9.InnerText)
                {
                    oCell9.Visible = false;
                    oCell_previous9.RowSpan += oCell9.RowSpan;
                }

                HtmlTableCell oCell_previous10 = checked_detailRepeater.Items[i - 1].FindControl("dycbj5") as HtmlTableCell;
                HtmlTableCell oCell10 = checked_detailRepeater.Items[i].FindControl("dycbj5") as HtmlTableCell;
                if (oCell_previous10 != null && oCell10 != null)
                {
                    oCell10.RowSpan = (oCell10.RowSpan == -1) ? 1 : oCell10.RowSpan;
                    oCell_previous10.RowSpan = (oCell_previous10.RowSpan == -1) ? 1 : oCell_previous10.RowSpan;
                }
                if (oCell10.InnerText == oCell_previous10.InnerText)
                {
                    oCell10.Visible = false;
                    oCell_previous10.RowSpan += oCell10.RowSpan;
                }

                HtmlTableCell oCell_previous11 = checked_detailRepeater.Items[i - 1].FindControl("zzbj5") as HtmlTableCell;
                HtmlTableCell oCell11 = checked_detailRepeater.Items[i].FindControl("zzbj5") as HtmlTableCell;
                if (oCell_previous11 != null && oCell11 != null)
                {
                    oCell11.RowSpan = (oCell11.RowSpan == -1) ? 1 : oCell11.RowSpan;
                    oCell_previous11.RowSpan = (oCell_previous11.RowSpan == -1) ? 1 : oCell_previous11.RowSpan;
                }
                if (oCell11.InnerText == oCell_previous11.InnerText)
                {
                    oCell11.Visible = false;
                    oCell_previous11.RowSpan += oCell11.RowSpan;
                }

                HtmlTableCell oCell_previous12 = checked_detailRepeater.Items[i - 1].FindControl("dycbj6") as HtmlTableCell;
                HtmlTableCell oCell12 = checked_detailRepeater.Items[i].FindControl("dycbj6") as HtmlTableCell;
                if (oCell_previous12 != null && oCell12 != null)
                {
                    oCell12.RowSpan = (oCell12.RowSpan == -1) ? 1 : oCell12.RowSpan;
                    oCell_previous12.RowSpan = (oCell_previous12.RowSpan == -1) ? 1 : oCell_previous12.RowSpan;
                }
                if (oCell12.InnerText == oCell_previous12.InnerText)
                {
                    oCell12.Visible = false;
                    oCell_previous12.RowSpan += oCell12.RowSpan;
                }

                HtmlTableCell oCell_previous13 = checked_detailRepeater.Items[i - 1].FindControl("zzbj6") as HtmlTableCell;
                HtmlTableCell oCell13 = checked_detailRepeater.Items[i].FindControl("zzbj6") as HtmlTableCell;
                if (oCell_previous13 != null && oCell13 != null)
                {
                    oCell13.RowSpan = (oCell13.RowSpan == -1) ? 1 : oCell13.RowSpan;
                    oCell_previous13.RowSpan = (oCell_previous13.RowSpan == -1) ? 1 : oCell_previous13.RowSpan;
                }
                if (oCell13.InnerText == oCell_previous13.InnerText)
                {
                    oCell13.Visible = false;
                    oCell_previous13.RowSpan += oCell13.RowSpan;
                }

            }
            #endregion


        }
        private void HeaderTemplatebind()
        {
            //初始化供应商信息
            string sqltext = "";
            sqltext = "select PM_SUPPLIERAID,supplieranm,supplierarank,PM_SUPPLIERBID,supplierbnm,supplierbrank,PM_SUPPLIERCID,suppliercnm,suppliercrank," +
                "PM_SUPPLIERDID,supplierdnm,supplierdrank,PM_SUPPLIEREID,supplierenm,suppliererank,PM_SUPPLIERFID,supplierfnm,supplierfrank" +
                " from View_TBMP_FAYUNPRICE where PM_SHEETNO='" + gloabsheetno + "'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt1.Rows.Count > 0)
            {
                ((Label)(checked_detailRepeater.Controls[0].FindControl("PM_SUPPLIERAID"))).Text = dt1.Rows[0]["PM_SUPPLIERAID"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("PM_SUPPLIERANAME"))).Text = dt1.Rows[0]["supplieranm"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("LbA_lei"))).Text = dt1.Rows[0]["supplierarank"].ToString();


                ((Label)(checked_detailRepeater.Controls[0].FindControl("PM_SUPPLIERBID"))).Text = dt1.Rows[0]["PM_SUPPLIERBID"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("PM_SUPPLIERBNAME"))).Text = dt1.Rows[0]["supplierbnm"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("LbB_lei"))).Text = dt1.Rows[0]["supplierbrank"].ToString();


                ((Label)(checked_detailRepeater.Controls[0].FindControl("PM_SUPPLIERCID"))).Text = dt1.Rows[0]["PM_SUPPLIERCID"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("PM_SUPPLIERCNAME"))).Text = dt1.Rows[0]["suppliercnm"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("LbC_lei"))).Text = dt1.Rows[0]["suppliercrank"].ToString();


                ((Label)(checked_detailRepeater.Controls[0].FindControl("PM_SUPPLIERDID"))).Text = dt1.Rows[0]["PM_SUPPLIERDID"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("PM_SUPPLIERDNAME"))).Text = dt1.Rows[0]["supplierdnm"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("LbD_lei"))).Text = dt1.Rows[0]["supplierdrank"].ToString();


                ((Label)(checked_detailRepeater.Controls[0].FindControl("PM_SUPPLIEREID"))).Text = dt1.Rows[0]["PM_SUPPLIEREID"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("PM_SUPPLIERENAME"))).Text = dt1.Rows[0]["supplierenm"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("LbE_lei"))).Text = dt1.Rows[0]["suppliererank"].ToString();


                ((Label)(checked_detailRepeater.Controls[0].FindControl("PM_SUPPLIERFID"))).Text = dt1.Rows[0]["PM_SUPPLIERFID"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("PM_SUPPLIERFNAME"))).Text = dt1.Rows[0]["supplierfnm"].ToString();
                ((Label)(checked_detailRepeater.Controls[0].FindControl("LbF_lei"))).Text = dt1.Rows[0]["supplierfrank"].ToString();
            }
        }
        /// <summary>
        /// 添加审批人
        /// </summary>
        private void initpower()
        {
            string sqltext = "";
            int num = 0;
            string shren1id = "";
            string shren2id = "";
            string shren3id = "";

            string shren1nm = "";
            string shren2nm = "";
            string shren3nm = "";

            string[] strsid1 = { };
            string[] strsid2 = { };
            string[] strsid3 = { };
            string[] strolds = { };

            num = 4;
            tb_pnum.Text = "4";
            if (num > 0)
            {
                string sqltxt1 = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_POSITION='0701'and ST_PD='0'";
                DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltxt1);
                shren1id = dt1.Rows[0]["ST_ID"].ToString();
                shren1nm = dt1.Rows[0]["ST_NAME"].ToString();
                string sqltxt2 = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_POSITION='0101'and ST_PD='0' order by ST_ID desc";
                DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltxt2);
                shren2id = dt2.Rows[0]["ST_ID"].ToString();
                shren2nm = dt2.Rows[0]["ST_NAME"].ToString();
                //string sqltxt3 = "select ST_ID,ST_NAME from TBDS_STAFFINFO where ST_POSITION='0102'and ST_PD='0'";
                //DataTable dt3 = DBCallCommon.GetDTUsingSqlText(sqltxt3);
                //shren3id = dt3.Rows[0]["ST_ID"].ToString();
                //shren3nm = dt3.Rows[0]["ST_NAME"].ToString();

                if (shren1nm == Tb_shenheren1.Text | Tb_shenheren1.Text == "")
                {
                    Tb_shenheren1.Text = shren1nm;
                    Tb_shenherencode1.Text = shren1id;
                }
                if (shren2nm == Tb_shenheren2.Text | Tb_shenheren2.Text == "")
                {
                    Tb_shenheren2.Text = shren2nm;
                    Tb_shenherencode2.Value = shren2id;
                }
                //if (shren3nm == Tb_shenheren3.Text | Tb_shenheren3.Text == "")
                //{
                //    Tb_shenheren3.Text = shren3nm;
                //    Tb_shenherencode3.Text = shren3id;
                //}

                Pan_shenheren2.Visible = true;

            }
            sqltext = "select ICL_REVIEWA,zdrnm  as ICL_REVIEWANM,ICL_REVIEWATIME, ICL_REVIEWAADVC," +
                     "ICL_REVIEWB,shbnm as ICL_REVIEWBNM,ICL_REVIEWBTIME,ICL_REVIEWBADVC," +
                     "ICL_REVIEWC,shcnm as ICL_REVIEWCNM,ICL_REVIEWCTIME,ICL_REVIEWCADVC," +
                     "ICL_REVIEWD,shdnm as ICL_REVIEWDNM,ICL_REVIEWDTIME,ICL_REVIEWDADVC," +
                     "ICL_REVIEWE,shenm as ICL_REVIEWENM,ICL_REVIEWETIME,ICL_REVIEWEADVC," +
                     "isnull(ICL_STATE,0) as ICL_STATE,isnull(ICL_STATEA,0) as ICL_STATEA,isnull(ICL_STATEB,0) as ICL_STATEB,isnull(ICL_STATEC,0) as ICL_STATEC,isnull(ICL_STATED,0) as ICL_STATED " +
                     "from View_TBMP_FAYUNPRCRVW  where picno='" + gloabsheetno + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);

            if (dt.Rows[0]["ICL_STATE"].ToString() != "0" && dt.Rows[0]["ICL_STATE"].ToString() != "2")
            {
                Panel_zd.Enabled = false;
            }
            else
            {
                if (Session["UserID"].ToString() == dt.Rows[0]["ICL_REVIEWA"].ToString())
                {
                    Panel_zd.Enabled = true;
                    Tb_zdanyj.BackColor = System.Drawing.Color.LightCoral;
                    btn_confirm.Enabled = true;
                    if (Tb_tjiaot.Text == "")
                    {
                        Tb_tjiaot.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                else
                {
                    Panel_zd.Enabled = false;
                }
            }
            if (dt.Rows[0]["ICL_STATE"].ToString() != "1")
            {
                Pan_shenheren1.Enabled = false;
            }
            else
            {
                if (Session["UserID"].ToString() == shren1id)
                {
                    Pan_shenheren1.Enabled = true;
                    btn_confirm.Enabled = true;
                    Tb_shenheyj1.BackColor = System.Drawing.Color.LightCoral;
                    Tb_shenheren1.Text = shren1nm;
                    Tb_shenherencode1.Text = shren1id;
                    if (Tb_shenhet1.Text == "")
                    {
                        Tb_shenhet1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    }
                }
                else
                {
                    Pan_shenheren1.Enabled = false;
                }
            }
            if (dt.Rows[0]["ICL_STATE"].ToString() != "3")
            {
                Pan_shenheren2.Enabled = false;
                Pan_shenheren3.Enabled = false;
            }
            else
            {
                if (dt.Rows[0]["ICL_STATEA"].ToString() == "2" && dt.Rows[0]["ICL_STATEB"].ToString() == "0")
                {
                    if (Session["UserID"].ToString() == Tb_shenherencode2.Value)
                    {
                        Pan_shenheren2.Enabled = true;
                        btn_confirm.Enabled = true;
                        Tb_shenheyj2.BackColor = System.Drawing.Color.LightCoral;
                        if (Tb_shenhet2.Text == "")
                        {
                            Tb_shenhet2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                        }
                    }
                    else
                    {
                        Pan_shenheren2.Enabled = false;
                    }
                }
                //else if (dt.Rows[0]["ICL_STATEB"].ToString() == "2" && dt.Rows[0]["ICL_STATEC"].ToString() == "0")
                //{
                //    if (Session["UserID"].ToString() == Tb_shenherencode3.Text)
                //    {
                //        Pan_shenheren3.Enabled = true;
                //        btn_confirm.Enabled = true;
                //        Tb_shenheyj3.BackColor = System.Drawing.Color.LightCoral;
                //        if (Tb_shenhet3.Text == "")
                //        {
                //            Tb_shenhet3.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                //        }
                //    }
                //    else
                //    {
                //        Pan_shenheren3.Enabled = false;
                //    }
                //}

            }

            if (dt.Rows[0]["ICL_STATE"].ToString() == "0")
            {
                Pan_shenheren1.Enabled = true;
                Pan_shenheren2.Enabled = true;
            }

        }
        protected void btn_edit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PM_Data/PM_fayun_comprise.aspx?sheetno=" + gloabsheetno + "");
        }
        protected void Rad_tongyi1_checkedchanged(object sender, EventArgs e)
        {
            settbtongyitext(Rad_tongyi1, Tb_shenheyj1);
        }
        protected void Rad_tongyi2_checkedchanged(object sender, EventArgs e)
        {
            settbtongyitext(Rad_tongyi2, Tb_shenheyj2);
        }
        protected void Rad_tongyi3_checkedchanged(object sender, EventArgs e)
        {
            settbtongyitext(Rad_tongyi3, Tb_shenheyj3);
        }
        protected void settbtongyitext(RadioButton rbt, TextBox tb)
        {
            if (rbt.Checked)
            {
                if (tb.Text.Replace(" ", "") == "")
                {
                    tb.Text = "同意";
                }
            }
        }
        protected void btn_back_Click(object sender, EventArgs e)//返回
        {
            Response.Redirect("PM_FHList.aspx");
        }
        private void SendMail(string subject, string body_title, string sendto)
        {
            string body = body_title +
                        "\r\n编号：" + gloabsheetno +
                        "\r\n发送日期：" + DateTime.Now.ToString("yyyy-MM--dd hh:mm:ss");
            string sql = "select DISTINCT [EMAIL] from TBDS_STAFFINFO where ST_NAME='" + sendto + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            if (dt.Rows.Count > 0)
            {
                DBCallCommon.SendEmail(dt.Rows[0][0].ToString(), null, null, subject, body);
            }
        }
        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            List<string> sqltextlist = new List<string>();
            string subject = "";
            string body_title = "";
            string sendto = "";
            string sqltext = "";
            string state = "";
            string statea = "";
            string stateb = "";
            string statec = "";
            sqltext = "select ICL_STATE,ICL_STATEA,ICL_STATEB,ICL_STATEC " +
                      "from TBMP_FAYUNPRCRVW where ICL_SHEETNO='" + gloabsheetno + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            while (dr.Read())
            {
                state = dr["ICL_STATE"].ToString();
                statea = dr["ICL_STATEA"].ToString();
                stateb = dr["ICL_STATEB"].ToString();
                statec = dr["ICL_STATEC"].ToString();

            }
            dr.Close();


            if (state == "0" || state == "2")
            {
                if (Tb_zdanyj.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写意见！');", true);
                    return;
                }
                else
                {
                    sqltext = "UPDATE TBMP_FAYUNPRCRVW SET ICL_REVIEWATIME=(case when (ICL_REVIEWATIME='' or ICL_REVIEWATIME is null) then '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' else  ICL_REVIEWATIME end)," +
                              "ICL_REVIEWAADVC='" + Tb_zdanyj.Text.ToString() + "',ICL_REVIEWB='" + Tb_shenherencode1.Text + "'," +
                              "ICL_REVIEWBADVC='',ICL_REVIEWC='" + Tb_shenherencode2.Value + "',ICL_REVIEWCADVC='',ICL_REVIEWD='" + Tb_shenherencode3.Text + "'," +
                              "ICL_REVIEWDADVC=''," +
                              "ICL_STATEA='0'," +
                              "ICL_STATEB='0',ICL_STATEC='0',ICL_STATED='0',ICL_STATEE='0',ICL_STATEF='0',ICL_STATE='1' " +
                              "WHERE ICL_SHEETNO='" + gloabsheetno + "'";
                    //邮件发送给第一个审批人
                    subject = "您有新的发运比价单需要审批，请及时处理——" + gloabsheetno;
                    sendto = Tb_shenheren1.Text.Trim();
                    this.SendMail(subject, body_title, sendto);
                }

            }


            else if (state == "1")
            {
                if (Rad_tongyi1.Checked)
                {


                    sqltext = "UPDATE TBMP_FAYUNPRCRVW SET ICL_REVIEWB='" + Tb_shenherencode1.Text + "'," +
                          "ICL_REVIEWBTIME='" + Tb_shenhet1.Text.ToString() + "'," +
                          "ICL_REVIEWBADVC='" + Tb_shenheyj1.Text.ToString() + "',ICL_STATE='3',ICL_STATEA='2' " +
                          "WHERE ICL_SHEETNO='" + gloabsheetno + "'";
                    //邮件发送给第二个审批人
                    subject = "您有新的发运比价单需要审批，请及时处理——" + gloabsheetno;
                    sendto = Tb_shenheren2.Text.Trim();
                    this.SendMail(subject, body_title, sendto);

                }
                else
                {
                    if (Tb_shenheyj1.Text.ToString().Replace(" ", "") == "")
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写拒绝理由！');", true);
                        return;
                    }
                    else
                    {
                        //sqltext = "UPDATE TBMP_FAYUNPRCRVW SET ICL_REVIEWB='" + Tb_shenherencode1.Text + "'," +
                        //                                 "ICL_REVIEWBTIME='" + Tb_shenhet1.Text.ToString() + "'," +
                        //                                 "ICL_REVIEWBADVC='" + Tb_shenheyj1.Text.ToString() + "',ICL_REVIEWCTIME='" + Tb_shenhet1.Text.ToString() + "',ICL_REVIEWCADVC='" + Tb_shenheyj1.Text.ToString() + "',ICL_STATE='2',ICL_STATEA='1',ICL_STATEB='1' " +
                        //                                 "WHERE ICL_SHEETNO='" + gloabsheetno + "'";
                        sqltext = "UPDATE TBMP_FAYUNPRCRVW SET ICL_REVIEWB='" + Tb_shenherencode1.Text + "'," +
                                                         "ICL_REVIEWBTIME='" + Tb_shenhet1.Text.ToString() + "'," +
                                                         "ICL_REVIEWBADVC='" + Tb_shenheyj1.Text.ToString() + "',ICL_STATE='2',ICL_STATEA='1'  " + "WHERE ICL_SHEETNO='" + gloabsheetno + "'";
                    }

                }
            }
            else if (state == "3")
            {
                if (Pan_shenheren2.Enabled == true)
                {
                    if (Rad_tongyi2.Checked)
                    {
                        sqltext = "UPDATE TBMP_FAYUNPRCRVW SET ICL_REVIEWC='" + Tb_shenherencode2.Value + "'," +
                                  "ICL_REVIEWCTIME='" + Tb_shenhet2.Text.ToString() + "'," +
                                  "ICL_REVIEWCADVC='" + Tb_shenheyj2.Text.ToString() + "',ICL_STATE='3',ICL_STATEB='2' " +
                                  "WHERE ICL_SHEETNO='" + gloabsheetno + "'";
                        ////邮件发送第三个审批人
                        //subject = "您有新的发运比价单需要审批，请及时处理——" + gloabsheetno;
                        //sendto = Tb_shenheren3.Text.Trim();
                        //this.SendMail(subject, body_title, sendto);
                    }
                    else
                    {
                        if (Tb_shenheyj2.Text.ToString().Replace(" ", "") == "")
                        {
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写拒绝理由！');", true);
                            return;
                        }
                        else
                        {
                            sqltext = "UPDATE TBMP_FAYUNPRCRVW SET ICL_REVIEWC='" + Tb_shenherencode2.Value + "'," +
                                                                "ICL_REVIEWCTIME='" + Tb_shenhet2.Text.ToString() + "'," +
                                                                "ICL_REVIEWCADVC='" + Tb_shenheyj2.Text.ToString() + "',ICL_STATE='2',ICL_STATEB='1'  " +
                                                                "WHERE ICL_SHEETNO='" + gloabsheetno + "'";
                        }

                    }
                }
                //else if (Pan_shenheren3.Enabled == true)
                //{
                //    if (Rad_tongyi3.Checked)
                //    {
                //        sqltext = "UPDATE TBMP_FAYUNPRCRVW SET ICL_REVIEWD='" + Tb_shenherencode3.Text + "'," +
                //                  "ICL_REVIEWDTIME='" + Tb_shenhet3.Text.ToString() + "'," +
                //                  "ICL_REVIEWDADVC='" + Tb_shenheyj3.Text.ToString() + "',ICL_STATE='3',ICL_STATEC='2' " +
                //                  "WHERE ICL_SHEETNO='" + gloabsheetno + "'";
                //    }
                //    else
                //    {
                //        if (Tb_shenheyj3.Text.ToString().Replace(" ", "") == "")
                //        {
                //            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写拒绝理由！');", true);
                //            return;
                //        }
                //        else
                //        {
                //            sqltext = "UPDATE TBMP_FAYUNPRCRVW SET ICL_REVIEWD='" + Tb_shenherencode3.Text + "'," +
                //                                                 "ICL_REVIEWDTIME='" + Tb_shenhet3.Text.ToString() + "'," +
                //                                                 "ICL_REVIEWDADVC='" + Tb_shenheyj3.Text.ToString() + "',ICL_STATE='2',ICL_STATEC='1' " +
                //                                                 "WHERE ICL_SHEETNO='" + gloabsheetno + "'";
                //        }

                //    }
                //}

            }
            sqltextlist.Add(sqltext);

            sqltext = "update TBMP_FAYUNPRCRVW set ICL_STATE= case when ICL_STATEA='2' and ICL_STATEB='2'  then '4' " +
                                                                       "when (ICL_STATEA='1' or ICL_STATEB='1' ) and  " +
                                                                       "(ICL_STATEA<>'0' and ICL_STATEB<>'0') then '2' " +
                                                                       "else ICL_STATE " +
                                                                       "end " +
                          "where ICL_SHEETNO='" + gloabsheetno + "'";
            sqltextlist.Add(sqltext);

            DBCallCommon.ExecuteTrans(sqltextlist);

            //判断是否全部审核通过
            bool YesOrNO = false;
            bool bohui = false;
            string sqltxt = "select * from TBMP_FAYUNPRCRVW where ICL_STATE='4'and ICL_SHEETNO='" + gloabsheetno + "'";
            string sqltxt1 = "select * from TBMP_FAYUNPRCRVW where ICL_STATE='2'and ICL_SHEETNO='" + gloabsheetno + "'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sqltxt);
            DataTable dt2 = DBCallCommon.GetDTUsingSqlText(sqltxt1);
            if (dt1.Rows.Count >= 1)
            {
                YesOrNO = true;
            }
            if (dt2.Rows.Count >= 1)
            {
                bohui = true;
            }
            foreach (RepeaterItem Reitm in checked_detailRepeater.Items)
            {
                //string tsaid = ((Label)Reitm.FindControl("TSA_ID")).Text;
                //string engname = ((Label)Reitm.FindControl("TSA_ENGNAME")).Text;
                //string map = ((Label)Reitm.FindControl("TSA_MAP")).Text;
                //  string cmid = ((Label)Reitm.FindControl("CM_ID")).Text;
                string tsaId = ((Label)Reitm.FindControl("TSA_ID")).Text;
                string zongxu = ((Label)Reitm.FindControl("PM_ZONGXU")).Text;
                int fynum = Convert.ToInt32(((Label)Reitm.FindControl("PM_FYNUM")).Text);
                string fid = ((Label)Reitm.FindControl("CM_FID")).Text;
                //CM_STATUS 0:未比价 1 已比价 2 比价完成审核通过 3比价驳回处理 4 发货评审中 5 发货评审驳回 6 发货评审通过已发货
                if (YesOrNO)
                {
                    //string sqltxt2 = "update VIEW_CM_FaHuo set CM_STATUS='2',CM_YBJNUM="+fynum+" where TSA_ID='" + tsaid + "' and TSA_ENGNAME='" + engname + "' and TSA_MAP='" + map + "'and CM_CONFIRM='2' and CM_FID='"+fid+"'";
                    //string sqltxt2 = "update TBPM_STRINFODQO set BM_YFNUM=BM_YFNUM+" + fynum + " where BM_ENGID='" + tsaId + "' and BM_ZONGXU='" + zongxu + "'";
                    //DBCallCommon.ExeSqlText(sqltxt2);
                    string sqltxt3 = "update TBPM_STRINFODQO set BM_FHSTATE=case when BM_YFNUM<BM_NUMBER then '4' else '2' end  where  BM_ENGID='" + tsaId + "' and BM_ZONGXU='" + zongxu + "'";
                    DBCallCommon.ExeSqlText(sqltxt3);
                }
                else
                {
                    //string sqltxt4 = "update VIEW_CM_FaHuo set CM_STATUS='1' where TSA_ID='" + tsaid + "' and TSA_ENGNAME='" + engname + "' and TSA_MAP='" + map + "'and CM_CONFIRM='2'and CM_FID='" + fid + "'";
                    string sqltxt4 = "update TBPM_STRINFODQO set BM_FHSTATE='1'  where  BM_ENGID='" + tsaId + "' and BM_ZONGXU='" + zongxu + "'";
                    DBCallCommon.ExeSqlText(sqltxt4);
                }
                if (bohui)//如果单据被驳回
                {
                    //string sqltxt3 = "update VIEW_CM_FaHuo set CM_STATUS='3' where TSA_ID='" + tsaid + "' and TSA_ENGNAME='" + engname + "' and TSA_MAP='" + map + "'and CM_CONFIRM='2'and CM_FID='" + fid + "'";
                    string sqltxt3 = "update TBPM_STRINFODQO set BM_FHSTATE='0' where  BM_ENGID='" + tsaId + "' and BM_ZONGXU='" + zongxu + "'";
                    DBCallCommon.ExeSqlText(sqltxt3);
                }

            }
            Response.Write("<script>alert('提交审核成功！');window.close()</script>");
        }
    }
}
