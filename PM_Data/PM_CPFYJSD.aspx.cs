using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_CPFYJSD : System.Web.UI.Page
    {
        DataTable dts = new DataTable();
        string action = "";
        string SHEETNO = "";
        string ZONGXU = "";
        string CM_CONTR_P = "";
        string CM_PROJ_P = "";
        string TSA_ID_P = "";
        string CM_CUSNAME_P = "";
        string CM_JHTIME_P = "";
        string TSA_ENGNAME_P = "";
        string supplierresnm = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"];
            SHEETNO = Request.QueryString["SHEETNO"];
            CM_CONTR_P = Request.QueryString["CM_CONTR"];
            CM_PROJ_P = Request.QueryString["CM_PROJ"];
            TSA_ID_P = Request.QueryString["TSA_ID"];
            CM_CUSNAME_P = Request.QueryString["CM_CUSNAME"];
            CM_JHTIME_P = Request.QueryString["CM_JHTIME"];
            TSA_ENGNAME_P = Request.QueryString["TSA_ENGNAME"];

            if (action == "add")
            {
                supplierresnm = Request.QueryString["supplierresnm"];
                ZONGXU = Request.QueryString["ZONGXU"];
                dts = DBCallCommon.GetDTUsingSqlText(SJY());
            }

            if (!IsPostBack)
            {
                if (action == "add")
                {

                    btnSubmit.Visible = true;
                    btnBack.Visible = true;
                    btnDelete.Visible = true;
                    BindrptJSD();
                    BindData();
                }
                if (action == "read")
                {
                    BindAllData();
                    txtJS_BZ.Enabled = false;
                    // btnSubmit.Visible = false;

                    btnBack.Visible = true;
                    string sql = "select JS_ZDR from PM_CPFYJSD where JS_FATHERID like '%" + SHEETNO + "%'";
                    DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                    if (dt.Rows[0]["JS_ZDR"].ToString() == Session["UserName"].ToString())
                    {
                        btnDelete.Visible = true;
                    }
                    else
                    {
                        btnDelete.Visible = false;
                    }
                }
            }
        }

        private void BindAllData()
        {
            string sql = "select * from PM_CPFYJSD where JS_FATHERID like '%" + SHEETNO + "%'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptJSD.DataSource = dt;
            rptJSD.DataBind();
            NoDataPane.Visible = false;
            lbJS_BH.Text = dt.Rows[0]["JS_BH"].ToString();
            lbJS_RQ.Text = dt.Rows[0]["JS_RQ"].ToString();
            lbJS_ZDR.Text = dt.Rows[0]["JS_ZDR"].ToString();
            lbJS_GYS.Text = dt.Rows[0]["JS_GYS"].ToString();
            txtJS_BZ.Text = dt.Rows[0]["JS_BZ"].ToString();
        }

        private string SJY()//获得数据源
        {
            string sql = "select A.ICL_SHEETNO,A.PM_ZONGXU,A.PM_FYNUM, A.ICL_REVIEWA, A.zdrnm, A.ICL_REVIEWATIME,A.ICL_ZHUGUANID ,A.ICL_IQRDATE,A.iclzgnm,A.ICL_FUZRID, A.iclfzrnm,A.PM_PRICE,A.PM_STATE,A.ICL_STATE,A.PM_CSTATE,A.PM_NOTE, A.PM_SUPPLIERRESID,A.supplierresnm,A.PM_ID,A.PM_MAP,A.PM_SHUILV,A.PM_FHDETAIL ,B.CM_CONTR,B.CM_FID,B.CM_PROJ,B.TSA_ID,B.TSA_ENGNAME,B.CM_JHTIME,B.CM_CUSNAME,C.BM_TUUNITWGHT,C.BM_ZONGXU from View_TBMP_FAYUNPRICE_RVW AS A LEFT OUTER JOIN View_CM_FaHuo AS B on (A.TSA_ID2=B.TSA_ID and A.PM_ZONGXU=B.ID and A.PM_FID=B.CM_FID)left join TBPM_STRINFODQO as C on (A.TSA_ID2=C.BM_ENGID and A.PM_ZONGXU=C.BM_ZONGXU) where ICL_SHEETNO =";
            sql += "'" + SHEETNO + "'";
            return sql;
        }

        private decimal GetZJ()//获取含税总金额
        {
            decimal hszje = 0;
            decimal hszz = 0;
            decimal hsje = 0;
            string sql = "select ICL_SHEETNO,PM_ZONGXU,PM_PRICE from View_TBMP_FAYUNPRICE_RVW where ICL_SHEETNO =";
            sql += "'" + SHEETNO + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            //for (int i = 0, length = dt.Rows.Count; i < length; i++)
            //{
            //    if (dt.Rows[i]["PM_PRICE"].ToString() != "")
            //    {
            //        hszje += Convert.ToDecimal(dt.Rows[i]["PM_PRICE"].ToString());
            //    }
            //}
            hszje += CommonFun.ComTryDecimal(dt.Rows[0]["PM_PRICE"].ToString());
            for (int i = 0, length = dts.Rows.Count; i < length; i++)
            {
                if (dts.Rows[i]["BM_TUUNITWGHT"].ToString().Trim() == "" || dts.Rows[i]["PM_FYNUM"].ToString().Trim() == "")
                {
                    hszz += 0;
                }
                else
                {
                    hszz += Convert.ToDecimal(dts.Rows[i]["BM_TUUNITWGHT"].ToString().Trim()) * Convert.ToDecimal(dts.Rows[i]["PM_FYNUM"].ToString().Trim());
                }
            }
            if (hszz == 0)
            {
                hsje = Math.Round((hszje / 1), 12);
            }
            else
            {
                hsje = Math.Round((hszje / hszz), 12);
            }
            return hsje;
        }

        private void BindrptJSD()
        {
            decimal hsje = GetZJ();
            List<string> list = new List<string>();
            for (int i = 0, length = dts.Rows.Count; i < length; i++)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (dts.Rows[i]["CM_CONTR"].ToString() == "" || dts.Rows[i]["TSA_ID"].ToString() == "")
                {
                    string JS_JHGZH = CM_PROJ_P + "-" + TSA_ID_P + "-" + TSA_ENGNAME_P + "-" + dts.Rows[i]["PM_MAP"].ToString() + "-" + dts.Rows[i]["BM_ZONGXU"].ToString() + "-" + dts.Rows[i]["ICL_SHEETNO"].ToString() + "-" + i.ToString().Trim();
                    dic.Add("JS_JHGZH", JS_JHGZH);
                    dic.Add("JS_HTH", CM_CONTR_P);//合同号
                    dic.Add("JS_XMMC", CM_PROJ_P);//项目名称
                    dic.Add("JS_RWH", TSA_ID_P);//任务号
                    dic.Add("JS_ZX", dts.Rows[i]["BM_ZONGXU"].ToString());//总序
                    dic.Add("JS_SBMC", TSA_ENGNAME_P);//设备名称
                    dic.Add("JS_TUHAO", dts.Rows[i]["PM_MAP"].ToString());//图号
                    dic.Add("JS_JHQ", CM_JHTIME_P);//交货期
                    dic.Add("JS_SHDW", CM_CUSNAME_P);//交货单位
                    dic.Add("JS_BJSL", dts.Rows[i]["PM_FYNUM"].ToString());//比价数量
                    //dic.Add("JS_JSSL", dts.Rows[i][""].ToString());//结算数量
                    dic.Add("JS_DANZ", dts.Rows[i]["BM_TUUNITWGHT"].ToString());//图纸单重
                    dic.Add("JS_GYS", dts.Rows[i]["supplierresnm"].ToString());//发货商
                    //dic.Add("JS_ZONGZ", zz.ToString());//总重
                }
                else
                {
                    string JS_JHGZH = dts.Rows[i]["CM_PROJ"].ToString() + "-" + dts.Rows[i]["TSA_ID"].ToString() + "-" + dts.Rows[i]["TSA_ENGNAME"].ToString() + "-" + dts.Rows[i]["PM_MAP"].ToString() + "-" + dts.Rows[i]["BM_ZONGXU"].ToString() + "-" + dts.Rows[i]["ICL_SHEETNO"].ToString() + "-" + i.ToString().Trim();
                    dic.Add("JS_JHGZH", JS_JHGZH);
                    dic.Add("JS_HTH", dts.Rows[i]["CM_CONTR"].ToString());//合同号
                    dic.Add("JS_XMMC", dts.Rows[i]["CM_PROJ"].ToString());//项目名称
                    dic.Add("JS_RWH", dts.Rows[i]["TSA_ID"].ToString());//任务号
                    dic.Add("JS_ZX", dts.Rows[i]["BM_ZONGXU"].ToString());//总序
                    dic.Add("JS_SBMC", dts.Rows[i]["TSA_ENGNAME"].ToString());//设备名称
                    dic.Add("JS_TUHAO", dts.Rows[i]["PM_MAP"].ToString());//图号
                    dic.Add("JS_JHQ", dts.Rows[i]["CM_JHTIME"].ToString());//交货期
                    dic.Add("JS_SHDW", dts.Rows[i]["CM_CUSNAME"].ToString());//交货单位
                    dic.Add("JS_BJSL", dts.Rows[i]["PM_FYNUM"].ToString());//比价数量
                    //dic.Add("JS_JSSL", dts.Rows[i][""].ToString());//结算数量
                    dic.Add("JS_DANZ", dts.Rows[i]["BM_TUUNITWGHT"].ToString());//图纸单重
                    dic.Add("JS_GYS", dts.Rows[i]["supplierresnm"].ToString());//发货商
                    //dic.Add("JS_ZONGZ", zz.ToString());//总重
                }

                decimal number = Convert.ToDecimal(dts.Rows[i]["PM_FYNUM"].ToString());
                decimal dz = 0;
                if (dts.Rows[i]["BM_TUUNITWGHT"].ToString().Trim() == "")
                {
                    dz = 1;
                }
                else
                {
                    dz = Convert.ToDecimal(dts.Rows[i]["BM_TUUNITWGHT"].ToString());
                }

                decimal hszje = Math.Round((hsje * number * dz), 2);
                dic.Add("JS_HSJE", hszje.ToString());//含税金额
                dic.Add("JS_SHUIL", "11");//税率
                dic.Add("JS_FATHERID", dts.Rows[i]["ICL_SHEETNO"].ToString());

                string keys = "";
                string values = "";
                string sql1 = "insert into PM_CPFYJSDBY(";

                foreach (KeyValuePair<string, object> pair in dic)
                {
                    keys += pair.Key + ",";
                    if (pair.Value.ToString() == dts.Rows[i]["BM_TUUNITWGHT"].ToString() || pair.Value.ToString() == dts.Rows[i]["PM_FYNUM"].ToString() || pair.Value.ToString() == hszje.ToString() || pair.Value.ToString() == "11")
                    {
                        // values += pair.Value + ",";
                        values += "'" + pair.Value + "',";
                    }
                    else
                    {
                        values += "'" + pair.Value + "',";
                    }
                }
                sql1 += keys.Trim(',') + ") values (" + values.Trim(',') + ")";
                list.Add(sql1);
            }
            DBCallCommon.ExecuteTrans(list);
            string sql = "select * from PM_CPFYJSDBY where JS_FATHERID =";
            sql += "'" + SHEETNO + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            rptJSD.DataSource = dt;
            rptJSD.DataBind();
            NoDataPane.Visible = false;
        }

        private void BindData()
        {
            string sql = "select  max(convert(int, substring(JS_BH,3,8)))+1 as maxJS_BH from PM_CPFYJSD";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            int max = 0;
            if (dt.Rows[0]["maxJS_BH"].ToString() == "")
            {
                max = 1;
            }
            else
            {
                max = Convert.ToInt32(dt.Rows[0]["maxJS_BH"].ToString());
            }
            lbJS_BH.Text = "JS" + max.ToString().PadLeft(8, '0');//编号

            //如果当月运费已核算，那么之后生成的结算单自动转为下个月
            string sqlyfhs = "select * from TBFM_YFHS where YFHS_STATE='1' and YFHS_YEAR+'-'+YFHS_MONTH like '" + DateTime.Now.ToString("yyyy-MM") + "%'";
            System.Data.DataTable dtyfhs = DBCallCommon.GetDTUsingSqlText(sqlyfhs);
            lbJS_RQ.Text = DateTime.Now.ToString("yyyy-MM-dd");
            if (dtyfhs.Rows.Count > 0)
            {
                if (Convert.ToInt32(DateTime.Now.Month.ToString()) < 12)
                {
                    lbJS_RQ.Text = DateTime.Now.AddMonths(1).ToString("yyyy-MM") + "-01";
                }
                else if (Convert.ToInt32(DateTime.Now.Month.ToString()) == 12)
                {
                    lbJS_RQ.Text = DateTime.Now.AddYears(1).AddMonths(-11).ToString("yyyy-MM") + "-01";
                }
            }


            lbJS_ZDR.Text = Session["UserName"].ToString();
            lbJS_GYS.Text = supplierresnm;
        }

        protected void btnSubmit_OnClick(object sender, EventArgs e)
        {
            if (action == "add")
            {
                //2016.11.21修改
                decimal hszje = 0;
                decimal sum_money = 0;
                string sql_hszje = "select ICL_SHEETNO,PM_ZONGXU,PM_PRICE from View_TBMP_FAYUNPRICE_RVW where ICL_SHEETNO =";
                sql_hszje += "'" + SHEETNO + "'";
                DataTable dt_hszje = DBCallCommon.GetDTUsingSqlText(sql_hszje);

                if (dt_hszje.Rows.Count > 0)
                {
                    hszje += CommonFun.ComTryDecimal(dt_hszje.Rows[0]["PM_PRICE"].ToString());

                    for (int i = 0; i < rptJSD.Items.Count; i++)
                    {
                        TextBox hsje_per = (TextBox)rptJSD.Items[i].FindControl("JS_HSJE");
                        sum_money += decimal.Parse(hsje_per.Text.Trim());
                    }
                    if (hszje != sum_money)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('每条单据含税金额相加和为" + sum_money + ",不等于总金额" + hszje + "！');", true);
                        return;
                    }
                }



                IsertRpt();
                IsertData();
                string sql = "delete from PM_CPFYJSDBY";
                DBCallCommon.ExeSqlText(sql);
            }
            else if (action == "read")
            {
                //2016.11.21修改
                decimal hszje = 0;
                decimal sum_money = 0;
                string sql_hszje = "select ICL_SHEETNO,PM_ZONGXU,PM_PRICE from View_TBMP_FAYUNPRICE_RVW where ICL_SHEETNO =";
                sql_hszje += "'" + SHEETNO + "'";
                DataTable dt_hszje = DBCallCommon.GetDTUsingSqlText(sql_hszje);
                if (dt_hszje.Rows.Count > 0)
                {
                    hszje += CommonFun.ComTryDecimal(dt_hszje.Rows[0]["PM_PRICE"].ToString());

                    for (int i = 0; i < rptJSD.Items.Count; i++)
                    {
                        TextBox hsje_per = (TextBox)rptJSD.Items[i].FindControl("JS_HSJE");
                        sum_money += decimal.Parse(hsje_per.Text.Trim());
                    }
                    if (hszje != sum_money)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('每条单据含税金额相加和为" + sum_money + ",不等于总金额" + hszje + "！');", true);
                        return;
                    }
                }


                //如果该结算单所在月份已核算或者该结算单已勾稽或者正在勾稽，那么该结算单不能删除
                //有已勾稽的
                string sqlifgj = "select * from PM_CPFYJSD where JS_BH='" + lbJS_BH.Text.Trim() + "' and (JS_GJSTATE!='0' or JS_XTSTATE!='0')";
                System.Data.DataTable dtifgj = DBCallCommon.GetDTUsingSqlText(sqlifgj);
                if (dtifgj.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该结算单已勾稽或正在勾稽,不能修改！');", true);
                    return;
                }
                //有已核算的
                string sqlyfhs = "select * from TBFM_YFHS where YFHS_STATE='1' and YFHS_YEAR+'-'+YFHS_MONTH like '" + lbJS_RQ.Text.Trim().Substring(0, 7).ToString().Trim() + "%'";
                System.Data.DataTable dtyfhs = DBCallCommon.GetDTUsingSqlText(sqlyfhs);
                if (dtyfhs.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该结算单已核算,不能修改！');", true);
                    return;
                }
                IsertData();
            }
            Response.Redirect("PM_CPFYJSDGL.aspx");
        }

        private void IsertRpt()
        {
            decimal hsje = GetZJ();
            List<string> list = new List<string>();
            for (int i = 0, length = dts.Rows.Count; i < length; i++)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                if (dts.Rows[i]["CM_CONTR"].ToString() == "" || dts.Rows[i]["TSA_ID"].ToString() == "")
                {
                    string JS_JHGZH = CM_PROJ_P + "-" + TSA_ID_P + "-" + TSA_ENGNAME_P + "-" + dts.Rows[i]["PM_MAP"].ToString() + "-" + dts.Rows[i]["BM_ZONGXU"].ToString() + "-" + dts.Rows[i]["ICL_SHEETNO"].ToString() + "-" + i.ToString().Trim();
                    dic.Add("JS_JHGZH", JS_JHGZH);
                    dic.Add("JS_HTH", CM_CONTR_P);//合同号
                    dic.Add("JS_XMMC", CM_PROJ_P);//项目名称
                    dic.Add("JS_RWH", TSA_ID_P);//任务号
                    dic.Add("JS_ZX", dts.Rows[i]["BM_ZONGXU"].ToString());//总序
                    dic.Add("JS_SBMC", TSA_ENGNAME_P);//设备名称
                    dic.Add("JS_TUHAO", dts.Rows[i]["PM_MAP"].ToString());//图号
                    dic.Add("JS_JHQ", CM_JHTIME_P);//交货期
                    dic.Add("JS_SHDW", CM_CUSNAME_P);//交货单位
                    dic.Add("JS_BJSL", dts.Rows[i]["PM_FYNUM"].ToString());//比价数量
                    //dic.Add("JS_JSSL", dts.Rows[i][""].ToString());//结算数量
                    dic.Add("JS_DANZ", dts.Rows[i]["BM_TUUNITWGHT"].ToString());//图纸单重
                    dic.Add("JS_GYS", dts.Rows[i]["supplierresnm"].ToString());//发货商
                    //dic.Add("JS_ZONGZ", zz.ToString());//总重
                }
                else
                {
                    string JS_JHGZH = dts.Rows[i]["CM_PROJ"].ToString() + "-" + dts.Rows[i]["TSA_ID"].ToString() + "-" + dts.Rows[i]["TSA_ENGNAME"].ToString() + "-" + dts.Rows[i]["PM_MAP"].ToString() + "-" + dts.Rows[i]["BM_ZONGXU"].ToString() + "-" + dts.Rows[i]["ICL_SHEETNO"].ToString() + "-" + i.ToString().Trim();
                    dic.Add("JS_JHGZH", JS_JHGZH);
                    dic.Add("JS_HTH", dts.Rows[i]["CM_CONTR"].ToString());//合同号
                    dic.Add("JS_XMMC", dts.Rows[i]["CM_PROJ"].ToString());//项目名称
                    dic.Add("JS_RWH", dts.Rows[i]["TSA_ID"].ToString());//任务号
                    dic.Add("JS_ZX", dts.Rows[i]["BM_ZONGXU"].ToString());//总序
                    dic.Add("JS_SBMC", dts.Rows[i]["TSA_ENGNAME"].ToString());//设备名称
                    dic.Add("JS_TUHAO", dts.Rows[i]["PM_MAP"].ToString());//图号
                    dic.Add("JS_JHQ", dts.Rows[i]["CM_JHTIME"].ToString());//交货期
                    dic.Add("JS_SHDW", dts.Rows[i]["CM_CUSNAME"].ToString());//交货单位
                    dic.Add("JS_BJSL", dts.Rows[i]["PM_FYNUM"].ToString());//比价数量
                    //dic.Add("JS_JSSL", dts.Rows[i][""].ToString());//结算数量
                    dic.Add("JS_DANZ", dts.Rows[i]["BM_TUUNITWGHT"].ToString());//图纸单重
                    dic.Add("JS_GYS", dts.Rows[i]["supplierresnm"].ToString());//发货商
                    //dic.Add("JS_ZONGZ", zz.ToString());//总重
                }
                decimal number = Convert.ToDecimal(dts.Rows[i]["PM_FYNUM"].ToString());
                decimal dz = 0;
                if (dts.Rows[i]["BM_TUUNITWGHT"].ToString().Trim() == "")
                {
                    dz = 1;
                }
                else
                {
                    dz = Convert.ToDecimal(dts.Rows[i]["BM_TUUNITWGHT"].ToString());
                }
                decimal hszje = Math.Round((hsje * number * dz), 2);
                dic.Add("JS_HSJE", hszje.ToString());//含税金额
                dic.Add("JS_SHUIL", "11");//税率
                dic.Add("JS_FATHERID", dts.Rows[i]["ICL_SHEETNO"].ToString());

                string keys = "";
                string values = "";
                string sql1 = "insert into PM_CPFYJSD(";

                foreach (KeyValuePair<string, object> pair in dic)
                {
                    keys += pair.Key + ",";
                    if (pair.Value.ToString() == dts.Rows[i]["BM_TUUNITWGHT"].ToString() || pair.Value.ToString() == dts.Rows[i]["PM_FYNUM"].ToString() || pair.Value.ToString() == hszje.ToString() || pair.Value.ToString() == "11")
                    {
                        //  values += pair.Value + ",";
                        values += "'" + pair.Value + "',";
                    }
                    else
                    {
                        values += "'" + pair.Value + "',";
                    }
                }
                sql1 += keys.Trim(',') + ") values (" + values.Trim(',') + ")";
                list.Add(sql1);
            }
            DBCallCommon.ExecuteTrans(list);
        }

        private void IsertData()
        {
            List<string> list = new List<string>();
            string sql = "update PM_CPFYJSD set ";
            sql += " JS_BH='" + lbJS_BH.Text + "',";
            sql += " JS_RQ='" + lbJS_RQ.Text + "',";
            sql += " JS_ZDR='" + lbJS_ZDR.Text + "',";
            sql += " JS_GYS='" + lbJS_GYS.Text + "',";
            sql += " JS_BZ='" + txtJS_BZ.Text + "'";
            sql += " where JS_FATHERID='" + SHEETNO + "'";
            DBCallCommon.ExeSqlText(sql);
            for (int i = 0; i < rptJSD.Items.Count; i++)
            {
                string sqltext = "update PM_CPFYJSD set JS_HSJE='" + ((TextBox)rptJSD.Items[i].FindControl("JS_HSJE")).Text.Trim() + "' where JS_FATHERID='" + SHEETNO + "' and JS_JHGZH='" + ((Label)rptJSD.Items[i].FindControl("JS_JHGZH")).Text.Trim() + "'";
                list.Add(sqltext);
            }
            DBCallCommon.ExecuteTrans(list);
        }

        protected void btnDelete_OnClick(object sender, EventArgs e)
        {
            //如果该结算单所在月份已核算或者该结算单已勾稽或者正在勾稽，那么该结算单不能删除
            //有已勾稽的
            string sqlifgj = "select * from PM_CPFYJSD where JS_BH='" + lbJS_BH.Text.Trim() + "' and (JS_GJSTATE!='0' or JS_XTSTATE!='0')";
            System.Data.DataTable dtifgj = DBCallCommon.GetDTUsingSqlText(sqlifgj);
            if (dtifgj.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该结算单已勾稽或正在勾稽,不能删除！');", true);
                return;
            }
            //有已核算的
            string sqlyfhs = "select * from TBFM_YFHS where YFHS_STATE='1' and YFHS_YEAR+'-'+YFHS_MONTH like '" + lbJS_RQ.Text.Trim().Substring(0, 7).ToString().Trim() + "%'";
            System.Data.DataTable dtyfhs = DBCallCommon.GetDTUsingSqlText(sqlyfhs);
            if (dtyfhs.Rows.Count > 0)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('该结算单已核算,不能删除！');", true);
                return;
            }
            else
            {
                string sql = "delete from PM_CPFYJSD where JS_FATHERID='" + SHEETNO + "'";
                string sql1 = "delete from PM_CPFYJSDBY";
                DBCallCommon.ExeSqlText(sql);
                DBCallCommon.ExeSqlText(sql1);
                Response.Write("<script>alert('您已成功删除该单据！！！')</script>");
                Response.Redirect("PM_fayun_list.aspx");
            }
        }

        protected void btnBack_OnClick(object sender, EventArgs e)
        {
            string sql = "delete from PM_CPFYJSDBY";
            DBCallCommon.ExeSqlText(sql);
            Response.Redirect("PM_CPFYJSDGL.aspx");
        }
    }
}
