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
using System.Collections.Generic;
using System.IO;

namespace ZCZJ_DPF.Basic_Data
{
    public partial class tbcs_cusupinfo_operate : System.Web.UI.Page
    {
        public string action = "";
        public string id = "";
        string filePath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"].ToString();
            if (action == "update")
            {
                id = Request.QueryString["id"].ToString();
            }
            else if (action == "add")
            {
                Label1.Text = "-->添加厂商信息";
            }
            GVBind();
            if (!IsPostBack)
            {
                //在第一次加载时，如果为更新，则需要根据id绑定数据
                if (action == "update")
                {
                    Label1.Text = "-->修改厂商信息";
                    this.GetDataByID(id);
                   // btnCreatID.Visible = false;
                    //2017.1.11将所在地显示
                    //palLocation.Visible = false;

                    txtCS_LOCATION.Enabled = false;
                    txtCS_MANCLERK.Text = Session["UserName"].ToString();
                    txtCS_FILLDATE.Value = System.DateTime.Now.ToString();
                    //GetCSCODE();
                }
                this.GetLocationData();
                this.GetLocationNextData();
            }
        }

        //protected void GetCSCODE()
        //{
        //    string sqltext = "select TOP 1 (CS_CODE) AS index1 from TBCS_CUSUPINFO ORDER BY CS_ID DESC ";
        //    DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //    int index;
        //    if (dt.Rows.Count > 0)
        //    {
        //        index = Convert.ToInt32(dt.Rows[0]["index1"].ToString());
        //    }
        //    else
        //    {
        //        index = 0;
        //    }
        //    string code = (index + 1).ToString();
        //    txtCS_CODE.Text = code;
        //}
        /// <summary>
        /// 从地区信息表中绑定地区信息
        /// </summary>
        protected void GetLocationData()
        {
            string sqltext = "select distinct CL_NAME,CL_CODE from TBCS_LOCINFO where CL_FATHERCODE='ROOT'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            dopCL_LOCATION.Items.Add(new ListItem("-未选择-", "请选择省份和市/区信息！"));
            while (dr.Read())
            {
                //分别将CL_NAME和CL_CODE绑定到Text和Value
                dopCL_LOCATION.Items.Add(new ListItem(dr["CL_NAME"].ToString(), dr["CL_CODE"].ToString()));
            }
            dr.Close();
        }
        /// <summary>
        /// 从地区表中绑定二级地区信息
        /// </summary>
        protected void GetLocationNextData()
        {
            dopCL_LOCATION_NEXT.Items.Clear();
            dopCL_LOCATION_NEXT.Items.Add(new ListItem("-未选择-", "请选择省份和市/区信息！"));
            if (dopCL_LOCATION.SelectedIndex != 0)
            {
                string fathercode = dopCL_LOCATION.SelectedValue;
                string sqltext = "select distinct CL_NAME,CL_CODE from TBCS_LOCINFO where CL_FATHERCODE='" + fathercode + "'";
                SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
                while (dr.Read())
                {
                    dopCL_LOCATION_NEXT.Items.Add(new ListItem(dr["CL_NAME"].ToString(), dr["CL_CODE"].ToString()));
                }
                dr.Close();
            }
            dopCL_LOCATION_NEXT.SelectedIndex = 0;
        }

        protected void dopCL_LOCATION_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.GetLocationNextData();
            txtCS_LOCATION.Text = "";
            txtCS_CODE.Text = "";
            if (dopCL_LOCATION.SelectedIndex == 0)
            {
                Response.Write("<script>alert('请选择省份！')</script>");
                txtCS_LOCATION.Text = "";
            }
        }

        protected void dopCL_LOCATION_NEXT_TextChanged(object sender, EventArgs e)
        {
            if (dopCL_LOCATION_NEXT.SelectedIndex == 0)
            {
                Response.Write("<script>alert('请选择（市/区）！')</script>");
                txtCS_LOCATION.Text = "";
                txtCS_CODE.Text = "";
            }
            else
            {
                txtCS_LOCATION.Text = dopCL_LOCATION.SelectedItem.Text + dopCL_LOCATION_NEXT.SelectedItem.Text;
                txtCS_CODE.Text = "";
            }
        }

        protected void dopCS_TYPE_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (dopCS_TYPE.SelectedIndex == 0)
            {
                Response.Write("<script>alert('请选择公司所属类别！')</script>");
                txtCS_TYPE.Text = "";
            }
            else
            {
                txtCS_TYPE.Text = dopCS_TYPE.SelectedItem.Text;
            }
        }

        //protected void dopCS_RANK_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (dopCS_RANK.SelectedIndex == 0)
        //    {
        //        txtCS_RANK.Text = "";
        //    }
        //    else
        //    {
        //        txtCS_RANK.Text = dopCS_RANK.SelectedItem.Text;
        //    }
        //}
        /// <summary>
        /// 修改操作之按id读出数据
        /// </summary>
        protected void GetDataByID(string id)
        {
            txtCS_CODE.Enabled = false;
            string sqltext = "select * from TBCS_CUSUPINFO where CS_CODE='" + id + "'";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            txtCS_CODE.Text = id;
            string[] date = new string[2];
            while (dr.Read())
            {
                purchase_identifier.Text = dr["CS_Purc_identifier"].ToString();
                txtCS_NAME.Text = dr["CS_NAME"].ToString();
                txtCS_LOCATION.Text = dr["CS_LOCATION"].ToString();
                txtCS_HRCODE.Text = dr["CS_HRCODE"].ToString();
                dopCS_TYPE.SelectedValue = dr["CS_TYPE"].ToString();
                switch (dr["CS_TYPE"].ToString())
                {
                    case "1":
                        txtCS_TYPE.Text = "客户";
                        break;
                    case "2":
                        txtCS_TYPE.Text = "采购供应商";
                        break;
                    case "3":
                        txtCS_TYPE.Text = "运输公司";
                        break;
                    case "4":
                        txtCS_TYPE.Text = "技术外协分包商";
                        break;
                    case "5":
                        txtCS_TYPE.Text = "生产外协分包商";
                        break;
                    case "6":
                        txtCS_TYPE.Text = "原材料销售供应商";
                        break;
                    case "7":
                        txtCS_TYPE.Text = "其它";
                        break;

                }

                txtCS_COREBS.Text = dr["CS_COREBS"].ToString();
                //txtCS_MCODE.Text = dr["CS_MCODE"].ToString();
                txtCS_ADDRESS.Text = dr["CS_ADDRESS"].ToString();
                txtCS_PHONO.Text = dr["CS_PHONO"].ToString();
                //txtCS_RANK.Text = dr["CS_RANK"].ToString();
                //dopCS_RANK.SelectedValue = dr["CS_RANK"].ToString();
                txtCS_ZIP.Text = dr["CS_ZIP"].ToString();
                txtCS_FAX.Text = dr["CS_FAX"].ToString();
                txtCS_NOTE.Text = dr["CS_NOTE"].ToString();
                txtCS_MANCLERK.Text = dr["CS_MANCLERK"].ToString();
                txtCS_CONNAME.Text = dr["CS_CONNAME"].ToString();
                date = (dr["CS_FILLDATE"].ToString()).Split(' ');
                txtCS_MAIL.Text = dr["CS_MAIL"].ToString();
                txtCS_BANK.Text = dr["CS_BANK"].ToString();
                txtCS_ACCOUNT.Text = dr["CS_ACCOUNT"].ToString();
                txtCS_TAX.Text = dr["CS_TAX"].ToString();
                TB_Scope.Text = dr["CS_Scope"].ToString();
                drpkhtype.SelectedValue = dr["kehutype"].ToString();



                input_qiyexinzhi.Text = dr["CS_COM_QUA_OTH"].ToString();
                input_caigoup.Text = dr["CS_PRO_BUY"].ToString();
                input_renzhentixi.Text = dr["CS_PRO_AU_OTH"].ToString();

                if (!string.IsNullOrEmpty(dr["cs_com_qua"].ToString()))
                {
                    int cs_com_qua_int = int.Parse(dr["cs_com_qua"].ToString().Trim());
                    CheckBoxList_qiyexinzhi.Items[cs_com_qua_int].Selected = true;
                }

                if (!string.IsNullOrEmpty(dr["cs_pro_au"].ToString()))
                {
                    int cs_pro_au_int = int.Parse(dr["cs_pro_au"].ToString().Trim());
                    CheckBoxList_renzhentixi.Items[cs_pro_au_int].Selected = true;
                }

                if (!string.IsNullOrEmpty(dr["cs_pro_qua"].ToString()))
                {
                    int cs_pro_qua_int = int.Parse(dr["cs_pro_qua"].ToString().Trim());
                    CheckBoxList1.Items[cs_pro_qua_int].Selected = true;
                }

                if (!string.IsNullOrEmpty(dr["cs_pro_fav"].ToString()))
                {
                    int cs_pao_fav_int = int.Parse(dr["cs_pro_fav"].ToString().Trim());
                    CheckBoxList2.Items[cs_pao_fav_int].Selected = true;
                }

                if (!string.IsNullOrEmpty(dr["cs_com_fit_oth"].ToString()))
                {
                    int cs_com_fit_oth_int = int.Parse(dr["cs_com_fit_oth"].ToString().Trim());
                    CheckBoxList3.Items[cs_com_fit_oth_int].Selected = true;
                }

                if (!string.IsNullOrEmpty(dr["cs_com_te"].ToString()))
                {
                    int cs_com_te_int = int.Parse(dr["cs_com_te"].ToString().Trim());
                    CheckBoxList4.Items[cs_com_te_int].Selected = true;
                }

                if (!string.IsNullOrEmpty(dr["cs_com_eq"].ToString()))
                {
                    int cs_com_eq_int = int.Parse(dr["cs_com_eq"].ToString().Trim());
                    CheckBoxList5.Items[cs_com_eq_int].Selected = true;
                }

                if (!string.IsNullOrEmpty(dr["cs_pro_ma"].ToString()))
                {
                    int cs_pro_ma_int = int.Parse(dr["cs_pro_ma"].ToString().Trim());
                    CheckBoxList6.Items[cs_pro_ma_int].Selected = true;
                }

                if (!string.IsNullOrEmpty(dr["cs_com_promi"].ToString()))
                {
                    int cs_com_paomi_int = int.Parse(dr["cs_com_promi"].ToString().Trim());
                    CheckBoxList7.Items[cs_com_paomi_int].Selected = true;
                }

                if (!string.IsNullOrEmpty(dr["cs_te_sup"].ToString()))
                {
                    int cs_te_sup_int = int.Parse(dr["cs_te_sup"].ToString().Trim());
                    CheckBoxList8.Items[cs_te_sup_int].Selected = true;
                }

                if (!string.IsNullOrEmpty(dr["cs_pro_mon"].ToString()))
                {
                    int cs_pro_mon_int = int.Parse(dr["cs_pro_mon"].ToString().Trim());
                    CheckBoxList9.Items[cs_pro_mon_int].Selected = true;
                }

            }
            txtCS_FILLDATE.Value = date[0].ToString();
            dr.Close();
        }
        /// <summary>
        /// 添加/修改厂商信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnLoad_Click(object sender, EventArgs e)
        {
            string cs_com_qua = "";
            string cs_com_qua_oth = "";
            string cs_pro_au = "";
            string cs_pro_au_oth = "";
            string cs_pro_qua = "";
            string cs_pro_fav = "";
            string cs_com_fit_oth = "";
            string cs_com_te = "";
            string cs_com_eq = "";
            string cs_pro_ma = "";
            string cs_com_promi = "";
            string cs_te_sup = "";
            string cs_pro_mon = "";

            string cs_pro_buy = "";

            string CS_Purc_identifier = "";

            cs_com_qua = CheckBoxList_qiyexinzhi.SelectedValue.ToString();
            for (int i = 0; i < CheckBoxList_qiyexinzhi.Items.Count; i++)
            {
                int len_jian=CheckBoxList_qiyexinzhi.Items.Count-1;
                if (i == len_jian && CheckBoxList_qiyexinzhi.Items[len_jian].Selected == true)
                {
                    cs_com_qua_oth = input_qiyexinzhi.Text.Trim();

                }
                else
                {
                    cs_com_qua_oth = "";
                }
            }
            //cs_com_qua_oth = Request.Form["input_qiyexinzhi"];
           // cs_com_qua_oth = input_qiyexinzhi.Text.Trim();
            // cs_com_qua_oth = Request.Form["input_qiyexinzhi"];
            cs_pro_au = CheckBoxList_renzhentixi.SelectedValue.ToString();
          //  cs_pro_au_oth = Request.Form["input_renzhentixi"];
           // cs_pro_au_oth = input_renzhentixi.Text.Trim();
            for (int i = 0; i < CheckBoxList_renzhentixi.Items.Count; i++)
            {
                int len_jian2 = CheckBoxList_renzhentixi.Items.Count - 1;
                if (i == len_jian2 && CheckBoxList_renzhentixi.Items[len_jian2].Selected == true)
                {
                    cs_pro_au_oth = input_renzhentixi.Text.Trim();

                }
                else
                {
                    cs_pro_au_oth = "";
                }
            }

            //cs_pro_au_oth = Request.Form["input_renzhentixi"];

            cs_pro_qua = CheckBoxList1.SelectedValue.ToString();
            cs_pro_fav = CheckBoxList2.SelectedValue.ToString();
            cs_com_fit_oth = CheckBoxList3.SelectedValue.ToString();
            cs_com_te = CheckBoxList4.SelectedValue.ToString();
            cs_com_eq = CheckBoxList5.SelectedValue.ToString();
            cs_pro_ma = CheckBoxList6.SelectedValue.ToString();
            cs_com_promi = CheckBoxList7.SelectedValue.ToString();
            cs_te_sup = CheckBoxList8.SelectedValue.ToString();
            cs_pro_mon = CheckBoxList9.SelectedValue.ToString();

            cs_pro_buy = input_caigoup.Text.Trim();

            CS_Purc_identifier = purchase_identifier.Text.Trim();

            string sqlText = "";
            if (action == "add")
            {
                string cs_code = txtCS_CODE.Text.Trim();
                string cs_name = txtCS_NAME.Text.Trim();
                string cs_location = txtCS_LOCATION.Text.Trim();
                string cs_hrcode = txtCS_HRCODE.Text.Trim();
                string cs_type1 = txtCS_TYPE.Text.Trim();
                string cs_type = "";
                switch (cs_type1)
                {
                    case "客户":
                        cs_type = "1";
                        break;
                    case "采购供应商":
                        cs_type = "2";
                        break;
                    case "运货商":
                        cs_type = "3";
                        break;
                    case "技术外协分包商":
                        cs_type = "4";
                        break;
                    case "生产外协分包商":
                        cs_type = "5";
                        break;
                    //case "客户和供应商":
                    //    cs_type = "3";
                    //    break;
                    case "原材料销售供应商":
                        cs_type = "6";
                        break;
                    case "其它":
                        cs_type = "7";
                        break;

                }

                string sqltextcheck = "select CS_NAME,CS_LOCATION,CS_TYPE from TBCS_CUSUPINFO where CS_NAME='" + cs_name + "'and CS_TYPE='" + cs_type + "'and CS_State='0' ";
                DataTable dtcheck = DBCallCommon.GetDTUsingSqlText(sqltextcheck);
                if (dtcheck.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('厂商已添加，请勿重复添加！！');", true); return;
                }
                string cs_address = txtCS_ADDRESS.Text.Trim();
                string cs_phono = txtCS_PHONO.Text.Trim();
                string cs_conname = txtCS_CONNAME.Text.Trim();
                string cs_mail = txtCS_MAIL.Text.Trim();
                string cs_zip = txtCS_ZIP.Text.Trim();
                string cs_fax = txtCS_FAX.Text.Trim();
                string cs_corebs = txtCS_COREBS.Text.Trim();
                //string cs_mcode = txtCS_MCODE.Text.Trim();
                //string cs_rank = txtCS_RANK.Text.Trim();
                string cs_manclerk = txtCS_MANCLERK.Text.Trim();
                string cs_filldate = txtCS_FILLDATE.Value.Trim();
                string cs_note = txtCS_NOTE.Text.Trim();
                string cs_bank = txtCS_BANK.Text.Trim();
                string cs_account = txtCS_ACCOUNT.Text.Trim();
                string cs_tax = txtCS_TAX.Text.Trim();

                sqlText = "insert into TBCS_CUSUPINFO(CS_CODE,CS_NAME,CS_LOCATION,CS_HRCODE,CS_TYPE,CS_ADDRESS,CS_PHONO,CS_CONNAME,CS_MAIL,CS_ZIP,CS_FAX,CS_COREBS,CS_RANK,CS_MANCLERK,CS_FILLDATE,CS_NOTE,CS_BANK,CS_ACCOUNT,CS_MCODE,CS_Scope,kehutype) VALUES(\'" + cs_code + "\',\'" + cs_name + "\',\'" + cs_location + "\',\'" + cs_hrcode + "\',\'" + cs_type + "\',\'" + cs_address + "\',\'" + cs_phono + "\',\'" + cs_conname + "\',\'" + cs_mail + "\',\'" + cs_zip + "\',\'" + cs_fax + "\',\'" + cs_corebs + "\',\'" + cs_manclerk + "\',\'" + cs_filldate + "\',\'" + cs_note + "\',\'" + cs_bank + "\',\'" + cs_account + "\',\'" + cs_tax + "\','" + TB_Scope.Text + "','" + drpkhtype.SelectedValue.Trim() + "')";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Write("<script>alert('添加成功！')</script>");
                Response.Write("<script>javascript:window.close();</script>");

                txtCS_ADDRESS.Text = "";
                txtCS_CODE.Text = "";
                txtCS_CONNAME.Text = "";
                txtCS_COREBS.Text = "";
                //txtCS_MCODE.Text = "";
                txtCS_FILLDATE.Value = "";
                txtCS_HRCODE.Text = "";
                txtCS_LOCATION.Text = "";
                txtCS_MAIL.Text = "";
                txtCS_MANCLERK.Text = "";
                txtCS_NAME.Text = "";
                txtCS_NOTE.Text = "";
                txtCS_PHONO.Text = "";
                //txtCS_RANK.Text = "";
                txtCS_TYPE.Text = "";
                txtCS_ZIP.Text = "";
                txtCS_FAX.Text = "";
                dopCL_LOCATION.SelectedIndex = 0;
                dopCL_LOCATION_NEXT.SelectedIndex = 0;
                //dopCS_RANK.SelectedIndex = 0;
                dopCS_TYPE.SelectedIndex = 0;

            }
            else
            {
                string cs_code = txtCS_CODE.Text.Trim();
                string cs_name = txtCS_NAME.Text.Trim();
                string cs_location = txtCS_LOCATION.Text.Trim();
                string cs_hrcode = txtCS_HRCODE.Text.Trim();
                string cs_type1 = txtCS_TYPE.Text.Trim();
                string cs_type = "";
                switch (cs_type1)
                {
                    case "客户":
                        cs_type = "1";
                        break;
                    case "采购供应商":
                        cs_type = "2";
                        break;

                    case "技术外协分包商":
                        cs_type = "4";
                        break;
                    case "生产外协分包商":
                        cs_type = "5";
                        break;
                    case "运货商":
                        cs_type = "3";
                        break;
                    case "原材料销售供应商":
                        cs_type = "6";
                        break;
                    case "其它":
                        cs_type = "7";
                        break;
                }

                //string sqltextcheck = "select CS_NAME,CS_LOCATION,CS_TYPE from TBCS_CUSUPINFO where CS_NAME='" + cs_name + "'and CS_TYPE='" + cs_type + "'and CS_State='0' ";
                //DataTable dtcheck = DBCallCommon.GetDTUsingSqlText(sqltextcheck);
                //if (dtcheck.Rows.Count > 0)
                //{
                //    ScriptManager.RegisterStartupScript(this.Page, GetType(), "", "alert('厂商已添加，请勿重复添加！！');", true); return;
                //}

                string cs_address = txtCS_ADDRESS.Text.Trim();
                string cs_phono = txtCS_PHONO.Text.Trim();
                string cs_conname = txtCS_CONNAME.Text.Trim();
                string cs_mail = txtCS_MAIL.Text.Trim();
                string cs_zip = txtCS_ZIP.Text.Trim();
                string cs_fax = txtCS_FAX.Text.Trim();
                string cs_corebs = txtCS_COREBS.Text.Trim();
                //string cs_rank = txtCS_RANK.Text.Trim();
                string cs_manclerk = txtCS_MANCLERK.Text.Trim();
                string cs_filldate = txtCS_FILLDATE.Value.Trim();
                string cs_note = txtCS_NOTE.Text.Trim();
                string cs_bank = txtCS_BANK.Text.Trim();
                string cs_account = txtCS_ACCOUNT.Text.Trim();
                string cs_tax = txtCS_TAX.Text.Trim();
                //string cs_mcode = txtCS_MCODE.Text.Trim();
                string cs_scope = TB_Scope.Text.Trim();

                sqlText = "update TBCS_CUSUPINFO set CS_Purc_identifier=\'" + CS_Purc_identifier + "\', CS_NAME=\'" + cs_name + "\',CS_LOCATION=\'" + cs_location + "\',CS_HRCODE=\'" + cs_hrcode + "\',CS_TYPE=\'" + cs_type + "\',CS_ADDRESS=\'" + cs_address + "\',CS_PHONO=\'" + cs_phono + "\',CS_CONNAME=\'" + cs_conname + "\',CS_MAIL=\'" + cs_mail + "\',CS_ZIP=\'" + cs_zip + "\',CS_FAX=\'" + cs_fax + "\',CS_COREBS=\'" + cs_corebs + "\',CS_MANCLERK=\'" + cs_manclerk + "\',CS_FILLDATE=\'" + cs_filldate + "\',CS_NOTE=\'" + cs_note + "\' ,CS_BANK=\'" + cs_bank + "\',CS_ACCOUNT=\'" + cs_account + "\',CS_TAX=\'" + cs_tax + "\',CS_Scope='" + cs_scope + "',kehutype='" + drpkhtype.SelectedValue.Trim() + "' ,cs_com_qua=\'" + cs_com_qua + "\',cs_com_qua_oth=\'" + cs_com_qua_oth + "\',cs_pro_au=\'" + cs_pro_au + "\',cs_pro_au_oth=\'" + cs_pro_au_oth + "\',cs_pro_qua=\'" + cs_pro_qua + "\',cs_pro_fav=\'" + cs_pro_fav + "\',cs_com_fit_oth=\'" + cs_com_fit_oth + "\',cs_com_te=\'" + cs_com_te + "\',cs_com_eq=\'" + cs_com_eq + "\',cs_pro_ma=\'" + cs_pro_ma + "\',cs_com_promi=\'" + cs_com_promi + "\' ,cs_te_sup=\'" + cs_te_sup + "\',cs_pro_mon=\'" + cs_pro_mon + "\',cs_pro_buy=\'" + cs_pro_buy + "\'  where CS_CODE=\'" + cs_code + "\'";
                DBCallCommon.ExeSqlText(sqlText);
                Response.Write("<script>alert('修改成功！')</script>");
                Response.Write("<script>javascript:window.close();</script>");

                //dopCS_RANK.SelectedIndex = 0;
                dopCS_TYPE.SelectedIndex = 0;
                this.GetDataByID(id);
            }
        }
        /// <summary>
        /// 生成ID号,公司编号ID为8位，前4位为地区号，后4位为流水号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //protected void btnCreatID_Click(object sender, EventArgs e)
        //{
        //    if (txtCS_LOCATION.Text == "")
        //    {
        //        Response.Write("<script>alert('请选择省、（市/区）！')</script>");
        //    }
        //    else
        //    {
        //        //需要创建的编号
        //        string createdCS_CODE = "";
        //        //由于地区编号的限制，直接获取TOP CS_CODE不行
        //        //采用的方法为读出与地区编号匹配的记录
        //        string locationCode = dopCL_LOCATION_NEXT.SelectedValue.ToString();
        //        string sqltext = "SELECT substring(CS_CODE,5,4) AS ID FROM TBCS_CUSUPINFO Where CS_CODE LIKE \'" + locationCode + "%\' ORDER BY ID ASC";
        //        //Response.Write(sqltext);
        //        //SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
        //        DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
        //        createdCS_CODE = locationCode.ToString() + this.CreatedID(dt).ToString();
        //        txtCS_CODE.Text = createdCS_CODE.ToString();
        //    }

        //}
        /// <summary>
        /// 找出中间空缺的最小编号
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        //private string CreatedID(DataTable dt)
        //{
        //    string creatID = "";
        //    int recordAmount = dt.Rows.Count;
        //    //Response.Write(recordAmount);

        //    string[] id_string = new string[recordAmount];
        //    int[] id_integer = new int[recordAmount];
        //    //获取编号字符串，并将其转换为可运算整型
        //    for (int i = 0; i < recordAmount; i++)
        //    {
        //        id_string[i] = dt.Rows[i]["ID"].ToString();
        //        id_integer[i] = Convert.ToInt32(id_string[i].ToString());
        //    }
        //    //分不同的情况进行判断
        //    //主要分为三大类：记录为0；为1；大于1（每一种还要细分）
        //    if (recordAmount == 0 || (recordAmount == 1 && id_integer[0] > 1))
        //    {
        //        creatID = "0001";
        //    }
        //    else if (recordAmount == 1 && id_integer[0] == 1)
        //    {
        //        creatID = string.Format("{0:0000}", id_integer[0] + 1);
        //        //Response.Write(creatID);
        //    }
        //    else
        //    {
        //        if (recordAmount == id_integer[recordAmount - 1])
        //        {
        //            creatID = string.Format("{0:0000}", id_integer[recordAmount - 1] + 1);
        //        }
        //        else if (id_integer[0] - 1 > 0)
        //        {
        //            creatID = "0001";
        //        }
        //        else
        //        {
        //            for (int m = 0; m < recordAmount - 1; m++)
        //            {
        //                int n = m + 1;
        //                if (id_integer[n] - id_integer[m] - 1 > 0)
        //                {
        //                    creatID = string.Format("{0:0000}", id_integer[m] + 1);
        //                    break;
        //                }
        //            }
        //        }
        //    }
        //    return creatID;
        //}

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "", "window.close()", true);

        }

        ///// <summary>
        ///// 检查公司名称是否存在
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //protected void txtCS_NAME_TextChanged(object sender, EventArgs e)
        //{
        //    string cs_name = txtCS_NAME.Text.Trim();
        //    string cs_code=txtCS_CODE.Text.Trim();
        //    string sqltext = "select * from TBCS_CUSUPINFO where CS_NAME=\'" + cs_name + "\' AND CS_CODE <> \'" + cs_code + "\'";
        //    //Response.Write(sqltext);
        //    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
        //    if (dr.HasRows)
        //    {
        //        Response.Write("<script>alert('该公司名称已存在！请核对！')</script>");
        //        txtCS_NAME.Focus();
        //    }
        //}

        protected void bntupload_Click(object sender, EventArgs e)
        {
            //if (txtCS_CODE.Text == "")
            //{
            //    Response.Write("<script>alert('先生成供货商编号！')</script>");
            //    return;
            //}

            //执行上传文件
            uploafFile();

        }

        private void uploafFile()
        {
            int IntIsUF = 0;

            //获取文件保存的路径 
            filePath = @"E:/基础数据";//附件上传位置            

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            try
            {
                HttpPostedFile hpf = FileUpload1.PostedFile;

                string fileContentType = hpf.ContentType;// 获取客户端发送的文件的 MIME 内容类型  

                if (fileContentType == "application/msword" || fileContentType == "application/vnd.ms-excel" || fileContentType == "application/pdf" || fileContentType == "image/pjpeg" || fileContentType == "image/gif" || fileContentType == "image/png" || fileContentType == "image/bmp" || fileContentType == "application/octet-stream" || fileContentType == "application/vnd.openxmlformats-officedocument.wordprocessingml.document" || fileContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")//传送文件类型
                {
                    if (hpf.ContentLength > 0)
                    {
                        if (hpf.ContentLength < (2048 * 1024))
                        {
                            string strFileName = System.IO.Path.GetFileName(hpf.FileName);
                            //string filename = System.DateTime.Now.ToString("yyyyMMddHHmmss") + strFileName;
                            if (!File.Exists(filePath + @"\" + strFileName))
                            {
                                string sqlStr = string.Empty;
                                //定义插入字符串，将上传文件信息保存在数据库中

                                sqlStr = "insert into TBMA_FILES (SUPPLYID,SAVEURL,UPLOADDATE,FILENAME)";
                                sqlStr += "values('" + txtCS_CODE.Text + "' ";
                                sqlStr += ",'" + filePath + "'";
                                sqlStr += ",'" + DateTime.Now.ToString() + "'";
                                sqlStr += ",'" + strFileName + "')";

                                DBCallCommon.ExeSqlText(sqlStr);
                                hpf.SaveAs(filePath + @"\" + strFileName);
                                //isHasFile = true;
                                IntIsUF = 1;
                            }
                            else
                            {
                                filesError.Visible = true;
                                filesError.Text = "上传文件名与服务器文件名重名，请您核对后重新命名上传！";
                                IntIsUF = 1;
                            }
                        }
                        else
                        {
                            filesError.Text = "上传文件过大，上传文件必须小于1M!";
                            IntIsUF = 1;
                        }
                    }
                }
                else
                {
                    filesError.Visible = true;
                    filesError.Text = "文件类型不符合要求，请您核对后重新上传！";
                    IntIsUF = 1;
                }
            }
            catch (Exception e)
            {
                filesError.Text = "文件上传过程中出现错误！" + e.ToString();
                filesError.Visible = true;
                return;
            }
            if (IntIsUF == 1)
            {
                IntIsUF = 0;
            }
            else
            {
                filesError.Visible = true;
                filesError.Text = "请选择上传文件!";
            }
            GVBind();
        }

        protected void imgbtndelete_Click(object sender, ImageClickEventArgs e)
        {
            //获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            //引用imgbtnDelete控件的父控件上一级控件
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            GridView gv = (GridView)imgbtn.Parent.Parent.Parent.Parent;
            string idd = ((Label)gvr.FindControl("lbid")).Text;
            //获取文件真实姓名
            string sqlStr = "select SAVEURL,FILENAME from TBMA_FILES where ID='" + idd + "'";
            //在文件夹Files下，删除该文件
            DeleteFile(sqlStr);
            string sqlDelStr = "delete from TBMA_FILES where ID='" + idd + "'";//删除数据库中的记录
            DBCallCommon.ExeSqlText(sqlDelStr);
            GVBind();

        }

        protected void DeleteFile(string sqlStr)
        {
            //打开数据库
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取指定文件的路径
            string strFilePath = ds.Tables[0].Rows[0]["SAVEURL"].ToString() + @"\" + ds.Tables[0].Rows[0]["FILENAME"].ToString();
            //调用File类的Delete方法，删除指定文件
            File.Delete(strFilePath);//文件不存在也不会引发异常
        }


        private void GVBind()
        {
            if (txtCS_CODE.Text == "")
            {
                txtCS_CODE.Text = id;
            }

            string sql = "select * from TBMA_FILES where SUPPLYID='" + txtCS_CODE.Text + "' ";
            DataSet ds = DBCallCommon.FillDataSet(sql);
            gvfileslist.DataSource = ds.Tables[0];
            gvfileslist.DataBind();
            gvfileslist.DataKeyNames = new string[] { "ID" };
        }


        protected void imgbtndownload_Click(object sender, ImageClickEventArgs e)
        {
            //获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            //引用imgbtnDelete控件的父控件上一级控件
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            GridView gv = (GridView)imgbtn.Parent.Parent.Parent.Parent;
            string idd = ((Label)gvr.FindControl("lbid")).Text;

            //获取文件真实姓名
            string sqlStr = "select SAVEURL,FILENAME from TBMA_FILES where ID='" + idd + "'";
            //打开数据库
            //Response.Write(sqlStr);         
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取文件路径
            string strFilePath = ds.Tables[0].Rows[0]["SAVEURL"].ToString() + @"\" + ds.Tables[0].Rows[0]["FILENAME"].ToString();
            Response.Write(strFilePath);
            //if (File.Exists(strFilePath))
            //{
            //    System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
            //    Response.Clear();
            //    Response.ClearHeaders();
            //    Response.Buffer = true;
            //    Response.ContentType = "application/octet-stream";
            //    Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name));
            //    Response.AppendHeader("Content-Length", file.Length.ToString());
            //    Response.WriteFile(file.FullName);
            //    //Response.Flush();
            //    //Response.End();
            //}
            //else
            //{
            //    filesError.Visible = true;
            //    filesError.Text = "文件已被删除，请通知相关人员上传文件！";
            //}




            //判断文件是否存在，如果不存在提示重新上传
            if (System.IO.File.Exists(strFilePath))
            {
                System.IO.FileInfo file = new System.IO.FileInfo(strFilePath);
                Response.Clear();
                Response.ClearHeaders();
                Response.Buffer = true;
                Response.ContentType = "application/octet-stream";
                Response.AddHeader("Content-Disposition", "attachment;filename=" + HttpUtility.UrlEncode(file.Name));
                Response.AppendHeader("Content-Length", file.Length.ToString());
                Response.WriteFile(file.FullName);
                Response.Flush();
                Response.End();

               // DBCallCommon.FileDown_byte(ds.Tables[0].Rows[0]["fileName"].ToString(), strFilePath);
            }
            else
            {
                filesError.Visible = true;
                filesError.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }
    }
}