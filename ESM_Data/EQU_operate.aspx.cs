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
using ZCZJ_DPF;

namespace ZCZJ_DPF.ESM
{
    public partial class equipment_operate : System.Web.UI.Page
    {
        SqlConnection sqlConn = new SqlConnection();
        
        string sql = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.Title = "机械设备管理";
                string actionstr = Request.QueryString["action"].ToString();
                if (actionstr == "show")
                {
                    string Id = Request.QueryString["Id"].ToString();
                    sql = "select * from ESM_EQU where Id='" + Id + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        CNum.Text = dr["CNum"].ToString();
                        SDate.Text = dr["SDate"].ToString();
                        Dmonth.Text = dr["Dmonth"].ToString();
                        Oval.Text = dr["Oval"].ToString();
                        ANum.Text = dr["ANum"].ToString();
                        ReRate.Text = dr["ReRate"].ToString();
                        AcDep.Text = dr["AcDep"].ToString();
                        UsDe.Text = dr["UsDe"].ToString();
                        CName.Text = dr["CName"].ToString();
                        AName.Text = dr["AName"].ToString();
                        NetVal.Text = dr["NetVal"].ToString();
                        Spec.Text = dr["Spec"].ToString();
                        Depre.Text = dr["Depre"].ToString();
                        Unit.Text = dr["Unit"].ToString();
                        Stor.Text = dr["Stor"].ToString();
                        CorAccount.Text = dr["CorAccount"].ToString();
                    }
                    dr.Close();
                }
                else if (actionstr == "add")
                {
                    btnupdate.Text = "添加";
                }
                else if(actionstr=="update")
                {
                    string Id = Request.QueryString["Id"].ToString();
                    sql = "select * from ESM_EQU where Id='" + Id + "'";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        CNum.Text = dr["CNum"].ToString();
                        SDate.Text = dr["SDate"].ToString();
                        Dmonth.Text = dr["Dmonth"].ToString();
                        Oval.Text = dr["Oval"].ToString();
                        ANum.Text = dr["ANum"].ToString();
                        ReRate.Text = dr["ReRate"].ToString();
                        AcDep.Text = dr["AcDep"].ToString();
                        UsDe.Text = dr["UsDe"].ToString();
                        CName.Text = dr["CName"].ToString();
                        AName.Text = dr["AName"].ToString();
                        NetVal.Text = dr["NetVal"].ToString();
                        Spec.Text = dr["Spec"].ToString();
                        Depre.Text = dr["Depre"].ToString();
                        Unit.Text = dr["Unit"].ToString();
                        Stor.Text = dr["Stor"].ToString();
                        CorAccount.Text = dr["CorAccount"].ToString();
                    }
                    dr.Close();
                   
                }
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            string actionstr = Request.QueryString["action"].ToString();
           
            if (actionstr == "add")
            {
                string sql = "";
                string maxid = generateID();
                sql = " insert into ESM_EQU(Id,CNum,SDate,Dmonth,Oval,ANum,ReRate,AcDep,UsDe,CName,AName,NetVal,Spec,Depre,Unit,Stor,CorAccount) values('" + maxid + "','" + CNum.Text.Trim() + "','" + SDate.Text.Trim() + "','" + Dmonth.Text.Trim() + "','" + Oval.Text.Trim() + "'," + ANum.Text.Trim() + ",'" + ReRate.Text.Trim() + "','" + AcDep.Text.Trim() + "','" + UsDe.Text.Trim() + "','" + CName.Text.Trim() + "','" + AName.Text.Trim() + "','" + NetVal.Text.Trim() + "','" + Spec.Text.Trim() + "','" + Depre.Text.Trim() + "','" + Unit.Text.Trim() + "','" + Stor.Text.Trim() + "','" + CorAccount.Text.Trim() + "') ";
                DBCallCommon.ExeSqlText(sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "if(confirm('操作成功，返回主界面？')){window.close();window.opener.location.reload();}", true);
            }
            else if (actionstr == "update")
            {
                string Id = Request.QueryString["Id"].ToString();
                string sqltext = "";
                sqltext = " update  ESM_EQU  set SDate='" + SDate.Text.Trim() + "',Dmonth='" + Dmonth.Text.Trim() + "',Oval='" + Oval.Text.Trim() + "',ANum='" + ANum.Text.Trim() + "',ReRate='" + ReRate.Text.Trim() + "',AcDep='" + AcDep.Text.Trim() + "',UsDe='" + UsDe.Text.Trim() + "',CName='" + CName.Text.Trim() + "',AName='" + AName.Text.Trim() + "',NetVal='" + NetVal.Text.Trim() + "',Spec='" + Spec.Text.Trim() + "',Depre='" + Depre.Text.Trim() + "',Unit='" + Unit.Text.Trim() + "',Stor='" + Stor.Text.Trim() + "',CorAccount='" + CorAccount.Text.Trim() + "' where  Id = '" + Id + "'";
                DBCallCommon.ExeSqlText(sqltext);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "if(confirm('操作成功，返回主界面？')){window.close();window.opener.location.reload();}", true);
                
                //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "window.close();", true);

            }
        }
        protected string generateID()
        {
            string sqltext = "select max(Id) as maxid from ESM_EQU";
            SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sqltext);
            dr.Read();
            string max = dr["maxid"].ToString();
            dr.Close();
            int id = Convert.ToInt32(max);
            id++;
            string maxid = Convert.ToString(id);
            return maxid;
        }
    }
}
