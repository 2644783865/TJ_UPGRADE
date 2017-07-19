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
using System.Text.RegularExpressions;
using System.Collections.Generic;

namespace ZCZJ_DPF.TM_Data
{
    public partial class TM_MSAdjustInput : System.Web.UI.Page
    {
        LiveOverData liveoverdata = new LiveOverData();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.PageInfo();
            }
        }
        /// <summary>
        /// 页面信息初始化
        /// </summary>
        protected void PageInfo()
        {
            ViewState["TaskID"] = Request.QueryString["TaskID"];
            tsaid.Text = ViewState["TaskID"].ToString();
            string sqlText = "";
            sqlText = "select TSA_PJID,TSA_PJNAME,TSA_ENGNAME,TSA_ENGSTRTYPE ";
            sqlText += "from View_TM_TaskAssign where TSA_ID='" + ViewState["TaskID"].ToString() + "'";
            System.Data.SqlClient.SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqlText);
            if (dr.Read())
            {
                pro_id.Value = dr[0].ToString();
                proname.Text = dr[1].ToString();
                engname.Text = dr[2].ToString();
                eng_type.Value = dr[3].ToString();
            }
            dr.Close();


            //读取台数
            string sql = "select TSA_NUMBER from View_TM_TaskAssign where TSA_ID='" + tsaid.Text.Split('-')[0] + "'";
            System.Data.SqlClient.SqlDataReader dr_number = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr_number.HasRows)
            {
                dr_number.Read();
                lblNumber.Text = dr_number["TSA_NUMBER"].ToString();
                dr_number.Close();
            }

            switch (eng_type.Value)
            {
                case "回转窑":
                    ViewState["str_table"] = "TBPM_STRINFOHZY";
                    ViewState["ms_table"] = "TBPM_MSOFHZY";
                    break;
                case "球、立磨":
                    ViewState["str_table"] = "TBPM_STRINFOQLM";
                    ViewState["ms_table"] = "TBPM_MSOFQLM";
                    break;
                case "篦冷机":
                    ViewState["str_table"] = "TBPM_STRINFOBLJ";
                    ViewState["ms_table"] = "TBPM_MSOFBLJ";
                    break;
                case "堆取料机":
                    ViewState["str_table"] = "TBPM_STRINFODQLJ";
                    ViewState["ms_table"] = "TBPM_MSOFDQJ";
                    break;
                case "钢结构及非标":
                    ViewState["str_table"] = "TBPM_STRINFOGFB";
                    ViewState["ms_table"] = "TBPM_MSOFGFB";
                    break;
                case "电气及其他":
                    ViewState["str_table"] = "TBPM_STRINFODQO";
                    ViewState["ms_table"] = "TBPM_MSOFDQO";
                    break;
                default: break;
            }
        }
        /// <summary>
        /// 保存提交
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            liveoverdata.XuHao = txtXuHao.Text.Trim();
            liveoverdata.TuHao = txtTuHao.Text.Trim();
            liveoverdata.Zongxu = txtZongxu.Text.Trim();
            liveoverdata.Shuliang =(Convert.ToInt16(lblNumber.Text)*Convert.ToDouble(txtMSXuHao.Text.Trim())).ToString();
            liveoverdata.MC = txtMC.Text.Trim();
            liveoverdata.KU = txtKu.Text.Trim();
            liveoverdata.BZ = txtBZ.Text.Trim();
            liveoverdata.PJID = pro_id.Value.Trim();
            liveoverdata.SingShuliang = txtMSXuHao.Text.Trim();
            liveoverdata.MSXuhao = txtMs.Text.Trim();
            if (liveoverdata.XuHao == "" || liveoverdata.MC == "")
            {
                this.ClientScript.RegisterStartupScript(GetType(), "js", "alert('【序号】【名称】不能为空！！！');", true);
            }
            else
            {
                string returnValue = this.CheckSubmit();
                if (returnValue == "Ok" || returnValue.Contains("ZongxuMarid"))
                {
                    List<string> list_insert = new List<string>();
                    string sql_insert_org = "insert into " + ViewState["str_table"] + "(BM_MSXUHAO,BM_XUHAO,BM_ZONGXU,BM_TUHAO,BM_MARID,BM_CHANAME,BM_NOTE,BM_KU,BM_NUMBER,BM_PNUMBER,BM_ENGID,BM_ISMANU,BM_SINGNUMBER,BM_PJID)" +
                        " Values('"+liveoverdata.MSXuhao+"','" + liveoverdata.XuHao + "','"+liveoverdata.Zongxu+"','" + liveoverdata.TuHao + "','','" + liveoverdata.MC + "','" + liveoverdata.BZ + "','" + liveoverdata.KU + "','" + liveoverdata.Shuliang + "','" + liveoverdata.Shuliang + "','" + tsaid.Text.Trim() + "','Y'," + liveoverdata.SingShuliang + ",'" + liveoverdata.PJID + "')";
                    list_insert.Add(sql_insert_org);
                    sql_insert_org = "insert into TBPM_TEMPMARDATA(BM_ZONGXU,BM_XUHAO,BM_MARID,BM_ENGID,BM_MAUNITWGHT,BM_UNITWGHT,BM_NUMBER,BM_MALENGTH,BM_MAWIDTH,BM_MATOTALLGTH)" +
                        " Values('"+liveoverdata.Zongxu+"','"+liveoverdata.XuHao+"','','"+tsaid.Text.Trim()+"',0,0,"+liveoverdata.Shuliang+",0,0,0)";
                    list_insert.Add(sql_insert_org);
                    DBCallCommon.ExecuteTrans(list_insert);
                    txtXuHao.Text = "";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('数据已保存！！！\\r\\r如需继续添加,请输入后保存,否则请关闭');", true);//////window.returnValue='Refesh';window.close();
                }
                else if (returnValue.Contains("FormatError"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r序号\"" + liveoverdata.XuHao + "\"格式错误！！！');", true);
                }
                else if (returnValue.Contains("ZongxuError"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r总序\"" + liveoverdata.Zongxu + "\"格式错误！！！');", true);
                }
                //////////////////else if (returnValue.Contains("ZongxuMarid"))
                //////////////////{
                //////////////////    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r总序\"" + liveoverdata.Zongxu + "\"已存在且有物料编码！！！');", true);
                //////////////////}
                else if (returnValue.Contains("ZongxuFJ"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r总序\"" + liveoverdata.Zongxu + "\"父级存在物料编码！！！');", true);
                }
                else if (returnValue.Contains("BelongToMar"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r序号\"" + liveoverdata.XuHao + "\"归属到物料！！！');", true);
                }
                else if (returnValue.Contains("FatherNoExist"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r序号\"" + liveoverdata.XuHao + "\"的父序不存在！！！');", true);
                }
                else if (returnValue.Contains("XuHaoExist"))
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:无法保存!\\r\\r序号\"" + liveoverdata.XuHao + "\"已存在！！！');", true);
                }
            }
        }
        /// <summary>
        /// 能否提交检查
        /// </summary>
        /// <returns></returns>
        protected string CheckSubmit()
        {
            //FormatError
            string[] a = tsaid.Text.Split('-');
            string firstCharofZX = "1";
            string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "(\\.[1-9]{1}[0-9]*)*)$";

            ////////////////////////////////string pattern = @"^(" + firstCharofZX + "|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}|" + firstCharofZX + "\\.[1-9]{1}[0-9]{0,1}(\\.[1-9]{1}[0-9]*)*)$";
            Regex rgx = new Regex(pattern);

            /************
             *总序验证
             ************/
            if(liveoverdata.Zongxu!="")
            {
                if (!rgx.IsMatch(liveoverdata.Zongxu))
                {
                    return "ZongxuError";
                }
                else
                {
                    //相同总序是否存在且有物料编码
                    string sql = "select * from " + ViewState["str_table"] + " where BM_ENGID like '" + a[0] + "-%' and BM_ZONGXU='"+liveoverdata.Zongxu+"' AND BM_MARID<>''";
                    System.Data.SqlClient.SqlDataReader dr_sql = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr_sql.HasRows)
                    {
                        dr_sql.Close();
                        return "ZongxuMarid";
                    }
                    dr_sql.Close();

                    //总序父级是否存在且有物料编码
                    string fj = liveoverdata.Zongxu.Substring(0, liveoverdata.Zongxu.LastIndexOf('.'));
                    string sql_fj = "select * from " + ViewState["str_table"] + " where BM_ENGID like '" + a[0] + "-%' and BM_ZONGXU='" + fj + "' AND BM_MARID<>''";
                    System.Data.SqlClient.SqlDataReader dr_sql_fj = DBCallCommon.GetDRUsingSqlText(sql_fj);
                    if (dr_sql_fj.HasRows)
                    {
                        dr_sql_fj.Close();
                        return "ZongxuFJ";
                    }
                    dr_sql_fj.Close();
                }
            }


            /*************
             * 序号验证
             * ***********/
            if (!rgx.IsMatch(liveoverdata.XuHao))
            {
                return "FormatError";
            }
            else
            { 
                //XuHaoExist
                string sql_select_xhexist="select BM_XUHAO from "+ViewState["str_table"]+" where BM_ENGID like '"+a[0]+"-%' AND BM_XUHAO='"+liveoverdata.XuHao+"'";
                System.Data.SqlClient.SqlDataReader dr_xhexist=DBCallCommon.GetDRUsingSqlText(sql_select_xhexist);
                if (dr_xhexist.HasRows)
                {
                    return "XuHaoExist";
                }
                else
                {
                    //FatherNoExist
                    string father = liveoverdata.XuHao.Substring(0, liveoverdata.XuHao.LastIndexOf('.'));
                    string sql_select_fath = "select BM_XUHAO from " + ViewState["str_table"] + " where BM_ENGID like '" + a[0] + "-%'  AND BM_XUHAO='" + father + "'";

                    System.Data.SqlClient.SqlDataReader dr_far = DBCallCommon.GetDRUsingSqlText(sql_select_fath);
                    if (!dr_far.HasRows)
                    {
                        return "FatherNoExist";
                    }
                    else
                    {
                        //BelongToMar
                        string sql_select_fmar = "select BM_XUHAO from " + ViewState["str_table"] + " where BM_ENGID like '" + a[0] + "-%'  AND BM_XUHAO='" + father + "' and BM_MARID!=''";
                        System.Data.SqlClient.SqlDataReader dr_fmar = DBCallCommon.GetDRUsingSqlText(sql_select_fmar);
                        if (dr_fmar.HasRows)
                        {
                            return "BelongToMar";
                        }
                        else
                        {
                            return "Ok";
                        }
                    }

                }
            }
        }

        protected void txtZongxu_OnTextChanged(object sender, EventArgs e)
        {
            //相同总序是否存在且有物料编码
            string[] a = tsaid.Text.Split('-');

            string sql = "select * from " + ViewState["str_table"] + " where BM_ENGID like '" + a[0] + "-%' and BM_ZONGXU='" + txtZongxu.Text.Trim() + "' AND BM_MARID<>''";
            System.Data.SqlClient.SqlDataReader dr_sql = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr_sql.HasRows)
            {
                dr_sql.Close();
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:总序\"" + txtZongxu.Text.Trim() + "\"已存在且有物料编码！！！请核对！！！');", true);
            }
            dr_sql.Close();
        }

        protected class LiveOverData
        {
            private string _xuhao;
            private string _zongxu;
            private string _tuhao;
            private string _shuliang;
            private string _mc;
            private string _ku;
            private string _bz;
            private string _pjid;
            private string _singshuliang;
            private string _MSXuhao;
            
            public string XuHao
            {
                get { return _xuhao; }
                set { _xuhao = value; }
            }

            public string Zongxu
            {
                get { return _zongxu; }
                set { _zongxu = value; }
            }


            public string TuHao
            {
                get { return _tuhao; }
                set { _tuhao = value; }
            }
            public string Shuliang
            {
                get { return _shuliang; }
                set { _shuliang = value; }
            }
            public string MC
            {
                get { return _mc; }
                set { _mc = value; }
            }
            public string KU
            {
                get { return _ku; }
                set { _ku = value; }
            }
            public string BZ
            {
                get { return _bz; }
                set { _bz = value; }
            }
            public string PJID
            {
                get { return _pjid; }
                set { _pjid = value; }
            }
            public string SingShuliang
            {
                get { return _singshuliang; }
                set { _singshuliang = value; }
            }

            public string MSXuhao
            {
                get { return _MSXuhao; }
                set { _MSXuhao = value; }
            }
        }
    }
}
