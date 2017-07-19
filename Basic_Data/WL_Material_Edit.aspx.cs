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

namespace ZCZJ_DPF.Basic_Data
{
    public partial class WL_TYPE_Edit : System.Web.UI.Page
    {
        string sql = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string person = "";
                string time = "";
                this.Title = "物料信息管理";
                if (Request.QueryString["edit"] != null)
                {
                    getClassOne();
                    getClassTwo();
                    tr1.Visible = false;
                    tr2.Visible = false;
                    person = Session["UserName"].ToString();
                    time = System.DateTime.Now.ToString();
                }
                else
                {
                    tr3.Visible = false;
                    string id = Request.QueryString["ID"];
                    m_id.Text = id;
                    m_standard.Enabled = true;
                    m_caizhi.Enabled = true;
                    m_guobiao.Enabled = true;
                    m_TECHUNIT.Enabled = true;
                    m_CONVERTRATE.Enabled = true;
                    m_PURCUNIT.Enabled = true;
                     string sql = "";
                     sql = " select * from View_Material where ID='" + id + "'  ";
                    SqlDataReader dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        m_name.Text = dr["MNAME"].ToString();
                        txtEngName.Text = dr["MENGNAME"].ToString();
                        m_helpcode.Text = dr["HMCODE"].ToString();
                        m_meterweight.Text = dr["MWEIGHT"].ToString();
                        txtMare.Text = dr["MAREA"].ToString();
                        m_standard.Text = dr["GUIGE"].ToString();
                        m_caizhi.Text = dr["CAIZHI"].ToString();
                        m_guobiao.Text = dr["GB"].ToString();
                        m_TECHUNIT.Text = dr["TECHUNIT"].ToString();
                        m_CONVERTRATE.Text = dr["CONVERTRATE"].ToString();
                        m_PURCUNIT.Text = dr["PURCUNIT"].ToString();
                        m_fzunit.Text = dr["FUZHUUNIT"].ToString();
                        m_status.SelectedValue = dr["STATE"].ToString();
                        m_comment.Text = dr["NOTE"].ToString();
                        person = dr["ST_NAME"].ToString();
                        time = dr["FILLDATE"].ToString().Split(' ')[0];
                    }
                    dr.Close();
                    sql = " select TY_NAME from TBMA_TYPEINFO where TY_ID='" + id.Substring(0, 5) + "' ";
                    dr = DBCallCommon.GetDRUsingSqlText(sql);
                    if (dr.Read())
                    {
                        m_parentname.Text = dr["TY_NAME"].ToString();
                    }
                    dr.Close();
                }
                lblPerson.Text = person;
                lblTime.Text = time;
            }
        }

        protected void getClassOne()
        {
            string sql = "SELECT DISTINCT TY_NAME,TY_ID FROM TBMA_TYPEINFO WHERE TY_FATHERID='ROOT' ORDER BY TY_ID";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DDLclass.DataSource = dt;
            DDLclass.DataTextField = "TY_NAME";
            DDLclass.DataValueField = "TY_ID";
            DDLclass.DataBind();

        }

        protected void getClassTwo()
        {
            string sql = "SELECT DISTINCT TY_NAME,TY_ID FROM TBMA_TYPEINFO WHERE TY_FATHERID='" + DDLclass.SelectedItem.Value + "'  ORDER BY TY_NAME";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DDLname.DataSource = dt;
            DDLname.DataTextField = "TY_NAME";
            DDLname.DataValueField = "TY_ID";
            DDLname.DataBind();

        }

        //执行取消操作
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(Page, GetType(), "", "window.close()", true);
        }
          
        //执行确认操作
        protected void btnConfirm_Click(object sender, EventArgs e)
        {
            string maxid = generateID();
            string id = m_id.Text.Trim();
            string name = m_name.Text.Trim();
            string engname = txtEngName.Text.Trim();
            string helpcode = m_helpcode.Text.Trim();
            float meterweight = 0;
            
            if (m_meterweight.Visible == true)
            { 
                meterweight = Convert.ToSingle(m_meterweight.Text);               
            }
            float marea = Convert.ToSingle(txtMare.Text.Trim());
            string standard = m_standard.Text.Trim();
            string caizhi = m_caizhi.Text.Trim();
            string guobiao = m_guobiao.Text.Trim();
            string jsunit = m_TECHUNIT.Text.Trim();
            string convertrate = m_CONVERTRATE.Text.Trim();
            string cgunit = m_PURCUNIT.Text.Trim();
            string fzunit = m_fzunit.Text.Trim();            
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string person = Session["UserID"].ToString(); //根据当前登录状况确定，与权限相关
            string status = m_status.SelectedItem.Value;
            string comment = m_comment.Text.Trim();
            //转换率检查
            if(jsunit.Contains("kg")&&cgunit=="T")
            {
                if(convertrate!="1000")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('单位转换率填写不正确！！！');", true);
                    return;
                }
            }

            if (convertrate == "0")
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('单位转换率不能为0！！！');", true);
                return;
            }
            //采购单位与采购辅助单位检查
            if(cgunit==fzunit)
            {
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('辅助单位与采购单位不能相同，请将辅助单位置空或填写其它单位！');", true);
                return;
            }

            if (Request.QueryString["edit"] != null)
            {
                int flag = checkData();
                if (flag == 1)
                {
                    sql = " insert into TBMA_MATERIAL(ID,MNAME,MENGNAME,HMCODE,GUIGE,MAREA,MWEIGHT,GB,CAIZHI,TECHUNIT,CONVERTRATE,PURCUNIT,FUZHUUNIT,TYPEID,FILLDATE,CLERK,STATE,NOTE)  " +
                      " values('" + maxid + "','" + name + "','"+engname+"','" + helpcode + "','" + standard + "',"+marea+",'" + meterweight +
                      "','" + guobiao + "','" + caizhi + "','" + jsunit + "','" + convertrate + "','" + cgunit +
                      "','" + fzunit + "','" +DDLname.SelectedValue+ "','" + date + "','" + person + "','" + status + "','" + comment + "')  ";

                       DBCallCommon.ExeSqlText(sql);
                       ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "if(confirm('操作成功，返回主界面？')){window.close();}", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('提示:数据已经存在不能添加!\\r\\r判断规格:【名称】、【规格】、【材质】、【国标】、【技术单位】、【采购单位】、【是否停用】完全相同时不能添加！！！');", true);
                }
                               
            }
            else
            {
                sql = "update TBMA_MATERIAL set MNAME='" + name + "',MENGNAME='"+engname+"', HMCODE='" + helpcode + "', GUIGE='" + standard + "' " +
                    "  ,MWEIGHT='" + meterweight + "',MAREA="+marea+",GB='" + guobiao + "', CAIZHI='" + caizhi + "'," +
                    "  TECHUNIT='" + jsunit + "', CONVERTRATE='" + convertrate + "', PURCUNIT='" + cgunit + "',FUZHUUNIT='" + fzunit + "',FILLDATE='"+date+"',CLERK='"+person+"',state='" + status + "', NOTE='"+comment+"' " +
                    "  where id='" + id + "'";
                DBCallCommon.ExeSqlText(sql);
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "if(confirm('操作成功，返回主界面？')){window.close();}", true);
            }
            
        }

        protected void DDLclass_SelectedIndexChanged(object sender, EventArgs e)
        {
            getClassTwo();
        }

        protected string generateID()
        {
            string sqltext = " select max(substring(id,7,12)) as maxid from TBMA_MATERIAL where id like '"+DDLname.SelectedValue+"%'  ";
            SqlDataReader dr=DBCallCommon.GetDRUsingSqlText(sqltext);
            dr.Read();
            string max_id = dr["maxid"].ToString();
            dr.Close();
            if (max_id!="")
            {
                return DDLname.SelectedValue + '.' + (Convert.ToInt32(max_id) + 1).ToString().PadLeft(6, '0');
            }
            else
            {
                return DDLname.SelectedValue + '.' + "000001";
            }
        }

        protected int checkData()
        {
            int flag = 1;
            sql = " select * from TBMA_MATERIAL where MNAME='" + m_name.Text.Trim() + "' and GUIGE='" + m_standard.Text.Trim() + "' and GB='" + m_guobiao.Text.Trim() + "' and CAIZHI='" + m_caizhi.Text.Trim() + "'and STATE='" + m_status.Text.Trim() + "' AND TECHUNIT='"+m_TECHUNIT.Text.Trim()+"' AND PURCUNIT='"+m_PURCUNIT.Text.Trim()+"'";
            SqlDataReader dr1 = DBCallCommon.GetDRUsingSqlText(sql);
            if (dr1.HasRows)
            {
                flag = 0;
            }
            dr1.Close();
            return flag;

        }
               
    }
}
