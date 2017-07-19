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
using ZCZJ_DPF;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_SUSHEdetail : System.Web.UI.Page
    {
        string action = "";
        string bh = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            action = Request.QueryString["action"].ToString().Trim();
            if (!IsPostBack)
            {
                if (action == "edit")
                {
                    bh = Request.QueryString["id"].ToString().Trim();
                    bindssdata();
                }
                else if (action == "add")
                {
                    tbhousenum.Enabled = true;
                }
            }
        }

        private void bindssdata()
        {
            string sqltext = "select *,(rjrs-xyrs) as krzsl from (select * from OM_SUSHE left join OM_SSDEtail on OM_SUSHE.housenum=OM_SSDEtail.SUSHEnum left join TBDS_STAFFINFO on OM_SSDEtail.stid=TBDS_STAFFINFO.ST_ID left join TBDS_DEPINFO on TBDS_STAFFINFO.ST_DEPID=TBDS_DEPINFO.DEP_CODE)t where housenum='" + bh + "' order by ID asc";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqltext);
            if (dt.Rows.Count > 0)
            {
                tbhousenum.Text = dt.Rows[0]["housenum"].ToString().Trim();
                lbxyrs.Text = dt.Rows[0]["xyrs"].ToString().Trim();
                tbrjrs.Text = dt.Rows[0]["rjrs"].ToString().Trim();
                lbkrzsl.Text = dt.Rows[0]["krzsl"].ToString().Trim();
                tbshangxp.Text = dt.Rows[0]["shangxp"].ToString().Trim();
                tbzuhc.Text = dt.Rows[0]["zuhc"].ToString().Trim();
                tbdanrc.Text = dt.Rows[0]["danrc"].ToString().Trim();
                tbyignum.Text = dt.Rows[0]["yignum"].ToString().Trim();
                tbyiznum.Text = dt.Rows[0]["yiznum"].ToString().Trim();
                tbdiansbum.Text = dt.Rows[0]["diansbum"].ToString().Trim();
                tbdiansgnum.Text = dt.Rows[0]["diansgnum"].ToString().Trim();
                tbkongtnum.Text = dt.Rows[0]["kongtnum"].ToString().Trim();
                tbxieztnum.Text = dt.Rows[0]["xieztnum"].ToString().Trim();
                tbketcpnum.Text = dt.Rows[0]["ketcpnum"].ToString().Trim();
                tbnotess.Text = dt.Rows[0]["notess"].ToString().Trim();
                if (dt.Rows.Count == 1)
                {
                    txtST_NAME1.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    lbstid1.Text = dt.Rows[0]["stid"].ToString().Trim();
                }
                if (dt.Rows.Count == 2)
                {
                    txtST_NAME1.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    lbstid1.Text = dt.Rows[0]["stid"].ToString().Trim();
                    txtST_NAME2.Text = dt.Rows[1]["ST_NAME"].ToString().Trim();
                    lbstid2.Text = dt.Rows[1]["stid"].ToString().Trim();
                }
                if (dt.Rows.Count == 3)
                {
                    txtST_NAME1.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    lbstid1.Text = dt.Rows[0]["stid"].ToString().Trim();
                    txtST_NAME2.Text = dt.Rows[1]["ST_NAME"].ToString().Trim();
                    lbstid2.Text = dt.Rows[1]["stid"].ToString().Trim();
                    txtST_NAME3.Text = dt.Rows[2]["ST_NAME"].ToString().Trim();
                    lbstid3.Text = dt.Rows[2]["stid"].ToString().Trim();
                }
                if (dt.Rows.Count == 4)
                {
                    txtST_NAME1.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    lbstid1.Text = dt.Rows[0]["stid"].ToString().Trim();
                    txtST_NAME2.Text = dt.Rows[1]["ST_NAME"].ToString().Trim();
                    lbstid2.Text = dt.Rows[1]["stid"].ToString().Trim();
                    txtST_NAME3.Text = dt.Rows[2]["ST_NAME"].ToString().Trim();
                    lbstid3.Text = dt.Rows[2]["stid"].ToString().Trim();
                    txtST_NAME4.Text = dt.Rows[3]["ST_NAME"].ToString().Trim();
                    lbstid4.Text = dt.Rows[3]["stid"].ToString().Trim();
                }
            }
        }


        //人员1
        protected void Textname_TextChanged1(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            if (num > 0)
            {
                string stid = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + stid + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    txtST_NAME1.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    lbstid1.Text = dt.Rows[0]["ST_ID"].ToString().Trim();
                }
            }
            rensjs();
        }
        //人员2
        protected void Textname_TextChanged2(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            if (num > 0)
            {
                string stid = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + stid + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    txtST_NAME2.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    lbstid2.Text = dt.Rows[0]["ST_ID"].ToString().Trim();
                }
            }
            rensjs();
        }
        //人员3
        protected void Textname_TextChanged3(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            if (num > 0)
            {
                string stid = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + stid + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    txtST_NAME3.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    lbstid3.Text = dt.Rows[0]["ST_ID"].ToString().Trim();
                }
            }
            rensjs();
        }
        //人员4
        protected void Textname_TextChanged4(object sender, EventArgs e)
        {
            int num = (sender as TextBox).Text.Trim().IndexOf("|", 0);
            if (num > 0)
            {
                string stid = (sender as TextBox).Text.Trim().Substring(0, num);

                string sqlText = "select * from View_TBDS_STAFFINFO where ST_ID='" + stid + "'";

                DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);

                if (dt.Rows.Count > 0)
                {
                    txtST_NAME4.Text = dt.Rows[0]["ST_NAME"].ToString().Trim();
                    lbstid4.Text = dt.Rows[0]["ST_ID"].ToString().Trim();
                }
            }
            rensjs();
        }

        private void rensjs()
        {
            int renshu = 0;
            if (lbstid1.Text.Trim() != "" && txtST_NAME1.Text.Trim()!="")
            {
                renshu = renshu + 1;
            }
            if (lbstid2.Text.Trim() != "" && txtST_NAME2.Text.Trim() != "")
            {
                renshu = renshu + 1;
            }
            if (lbstid3.Text.Trim() != "" && txtST_NAME3.Text.Trim() != "")
            {
                renshu = renshu + 1;
            }
            if (lbstid4.Text.Trim() != "" && txtST_NAME4.Text.Trim() != "")
            {
                renshu = renshu + 1;
            }
            lbxyrs.Text = renshu.ToString().Trim();
            lbkrzsl.Text = (CommonFun.ComTryInt(tbrjrs.Text.Trim()) - renshu).ToString().Trim();
        }


        protected void btnSave_OnClick(object sender, EventArgs e)
        {
            action = Request.QueryString["action"].ToString().Trim();
            List<string> sqltextlist = new List<string>();
            int num = 0;
            if (tbrjrs.Text.Trim() == "" || tbhousenum.Text.Trim() == "")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请填写房间号和容积人数！');", true);
                return;
            }
            if (CommonFun.ComTryInt(tbrjrs.Text.Trim()) > 4)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('容积人数超出！');", true);
                return;
            }
            if (txtST_NAME1.Text.Trim() != "" && lbstid1.Text.Trim()=="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员1错误！');", true);
                return;
            }
            if (txtST_NAME2.Text.Trim() != "" && lbstid2.Text.Trim()=="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员2错误！');", true);
                return;
            }
            if (txtST_NAME3.Text.Trim() != "" && lbstid3.Text.Trim()=="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员3错误！');", true);
                return;
            }
            if (txtST_NAME4.Text.Trim() != "" && lbstid4.Text.Trim()=="")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员4错误！');", true);
                return;
            }


            if (txtST_NAME1.Text.Trim() != "" && lbstid1.Text.Trim()!="")
            {
                string sqlgetdata1 = "select * from TBDS_STAFFINFO where ST_NAME='" + txtST_NAME1.Text.Trim() + "' and ST_ID='" + lbstid1.Text.Trim() + "'";
                DataTable dt1=DBCallCommon.GetDTUsingSqlText(sqlgetdata1);
                if (dt1.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员1错误！');", true);
                    return;
                }
                else
                {
                    num++;
                }
            }
            if (txtST_NAME2.Text.Trim() != "" && lbstid2.Text.Trim()!="")
            {
                string sqlgetdata2 = "select * from TBDS_STAFFINFO where ST_NAME='" + txtST_NAME2.Text.Trim() + "' and ST_ID='" + lbstid2.Text.Trim() + "'";
                DataTable dt2=DBCallCommon.GetDTUsingSqlText(sqlgetdata2);
                if (dt2.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员2错误！');", true);
                    return;
                }
                else
                {
                    num++;
                }
            }
            if (txtST_NAME3.Text.Trim() != "" && lbstid3.Text.Trim()!="")
            {
                string sqlgetdata3 = "select * from TBDS_STAFFINFO where ST_NAME='" + txtST_NAME3.Text.Trim() + "' and ST_ID='" + lbstid3.Text.Trim() + "'";
                DataTable dt3=DBCallCommon.GetDTUsingSqlText(sqlgetdata3);
                if (dt3.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员3错误！');", true);
                    return;
                }
                else
                {
                    num++;
                }
            }
            if (txtST_NAME4.Text.Trim() != "" && lbstid4.Text.Trim()!="")
            {
                string sqlgetdata4 = "select * from TBDS_STAFFINFO where ST_NAME='" + txtST_NAME4.Text.Trim() + "' and ST_ID='" + lbstid4.Text.Trim() + "'";
                DataTable dt4=DBCallCommon.GetDTUsingSqlText(sqlgetdata4);
                if (dt4.Rows.Count == 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('人员4错误！');", true);
                    return;
                }
                else
                {
                    num++;
                }
            }
            //编辑
            if (action == "edit")
            {
                bh = Request.QueryString["id"].ToString().Trim();
                string sqldeleteru = "delete from OM_SSDEtail where SUSHEnum='" + bh + "'";
                DBCallCommon.ExeSqlText(sqldeleteru);
                string sqlupdatess = "update OM_SUSHE set xyrs=" + num + ",rjrs=" + CommonFun.ComTryInt(tbrjrs.Text.Trim()) + ",shangxp=" + CommonFun.ComTryInt(tbshangxp.Text.Trim()) + ",zuhc=" + CommonFun.ComTryInt(tbzuhc.Text.Trim()) + ",danrc=" + CommonFun.ComTryInt(tbdanrc.Text.Trim()) + ",yignum=" + CommonFun.ComTryInt(tbyignum.Text.Trim()) + ",yiznum=" + CommonFun.ComTryInt(tbyiznum.Text.Trim()) + ",diansbum=" + CommonFun.ComTryInt(tbdiansbum.Text.Trim()) + ",diansgnum=" + CommonFun.ComTryInt(tbdiansgnum.Text.Trim()) + ",kongtnum=" + CommonFun.ComTryInt(tbkongtnum.Text.Trim()) + ",xieztnum=" + CommonFun.ComTryInt(tbxieztnum.Text.Trim()) + ",ketcpnum=" + CommonFun.ComTryInt(tbketcpnum.Text.Trim()) + ",notess='" + tbnotess.Text.Trim() + "' where housenum='" + bh + "'";
                sqltextlist.Add(sqlupdatess);
                if (lbstid1.Text.Trim() != "" && txtST_NAME1.Text.Trim() != "")
                {
                    string sqlinsertdetail1 = "insert into OM_SSDEtail(SUSHEnum,stid) values('" + tbhousenum.Text.Trim() + "','" + lbstid1.Text.Trim() + "')";
                    sqltextlist.Add(sqlinsertdetail1);
                }
                if (lbstid2.Text.Trim() != "" && txtST_NAME2.Text.Trim() != "")
                {
                    string sqlinsertdetail2 = "insert into OM_SSDEtail(SUSHEnum,stid) values('" + tbhousenum.Text.Trim() + "','" + lbstid2.Text.Trim() + "')";
                    sqltextlist.Add(sqlinsertdetail2);
                }
                if (lbstid3.Text.Trim() != "" && txtST_NAME3.Text.Trim() != "")
                {
                    string sqlinsertdetail3 = "insert into OM_SSDEtail(SUSHEnum,stid) values('" + tbhousenum.Text.Trim() + "','" + lbstid3.Text.Trim() + "')";
                    sqltextlist.Add(sqlinsertdetail3);
                }
                if (lbstid4.Text.Trim() != "" && txtST_NAME4.Text.Trim() != "")
                {
                    string sqlinsertdetail4 = "insert into OM_SSDEtail(SUSHEnum,stid) values('" + tbhousenum.Text.Trim() + "','" + lbstid4.Text.Trim() + "')";
                    sqltextlist.Add(sqlinsertdetail4);
                }
            }
            


            //添加
            else if (action == "add")
            {
                string sql = "select * from OM_SUSHE where housenum='" + tbhousenum.Text.Trim() + "'";
                DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
                if (dt.Rows.Count > 0)
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('房间号已存在！');", true);
                    return;
                }
                else
                {
                    string sqlinsertss = "insert into OM_SUSHE(housenum,xyrs,rjrs,shangxp,zuhc,danrc,yignum,yiznum,diansbum,diansgnum,kongtnum,xieztnum,ketcpnum,notess) values('" + tbhousenum.Text.Trim() + "'," + num + "," + CommonFun.ComTryInt(tbrjrs.Text.Trim()) + "," + CommonFun.ComTryInt(tbshangxp.Text.Trim()) + "," + CommonFun.ComTryInt(tbzuhc.Text.Trim()) + "," + CommonFun.ComTryInt(tbdanrc.Text.Trim()) + "," + CommonFun.ComTryInt(tbyignum.Text.Trim()) + "," + CommonFun.ComTryInt(tbyiznum.Text.Trim()) + "," + CommonFun.ComTryInt(tbdiansbum.Text.Trim()) + "," + CommonFun.ComTryInt(tbdiansgnum.Text.Trim()) + "," + CommonFun.ComTryInt(tbkongtnum.Text.Trim()) + "," + CommonFun.ComTryInt(tbxieztnum.Text.Trim()) + "," + CommonFun.ComTryInt(tbketcpnum.Text.Trim()) + ",'" + tbnotess.Text.Trim() + "')";
                    sqltextlist.Add(sqlinsertss);
                    if (lbstid1.Text.Trim() != "" && txtST_NAME1.Text.Trim() != "")
                    {
                        string sqlinsertdetail1 = "insert into OM_SSDEtail(SUSHEnum,stid) values('" + tbhousenum.Text.Trim() + "','" + lbstid1.Text.Trim() + "')";
                        sqltextlist.Add(sqlinsertdetail1);
                    }
                    if (lbstid2.Text.Trim() != "" && txtST_NAME2.Text.Trim() != "")
                    {
                        string sqlinsertdetail2 = "insert into OM_SSDEtail(SUSHEnum,stid) values('" + tbhousenum.Text.Trim() + "','" + lbstid2.Text.Trim() + "')";
                        sqltextlist.Add(sqlinsertdetail2);
                    }
                    if (lbstid3.Text.Trim() != "" && txtST_NAME3.Text.Trim() != "")
                    {
                        string sqlinsertdetail3 = "insert into OM_SSDEtail(SUSHEnum,stid) values('" + tbhousenum.Text.Trim() + "','" + lbstid3.Text.Trim() + "')";
                        sqltextlist.Add(sqlinsertdetail3);
                    }
                    if (lbstid4.Text.Trim() != "" && txtST_NAME4.Text.Trim() != "")
                    {
                        string sqlinsertdetail4 = "insert into OM_SSDEtail(SUSHEnum,stid) values('" + tbhousenum.Text.Trim() + "','" + lbstid4.Text.Trim() + "')";
                        sqltextlist.Add(sqlinsertdetail4);
                    }
                }
            }
            DBCallCommon.ExecuteTrans(sqltextlist);
            Response.Redirect("OM_SUSHEdetail.aspx?action=edit&id=" + tbhousenum.Text.Trim()); 
        }
    }
}
