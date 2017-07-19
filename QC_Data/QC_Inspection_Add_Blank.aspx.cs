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
using System.Collections.Generic;

namespace ZCZJ_DPF.QC_Data
{
    public partial class QC_Inspection_Add_Blank : System.Web.UI.Page
    {
        int count = 0;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["UserID"].ToString() == "")
            {
                Response.Write("<script>if(window.parent!=null)window.parent.location.href='../Default.aspx';else{window.location.href='./Default.aspx'} </script>");
            }

            InitVar();

            if(!IsPostBack )
            {
                InitGridview();
            }

        }
        /// <summary>
        /// 页面初始化
        /// </summary>
        private void InitVar()
        {
            TextBoxDep.Text = Session["UserDept"].ToString();
            TextBoxMan.Text = Session["UserName"].ToString();
        }
        /// <summary>
        /// 初始化Gridview
        /// </summary>
        private void InitGridview()
        {
            DataTable dt = this.GetDataFromGrid(true);

            RepeaterItem.DataSource = dt;

            RepeaterItem.DataBind();
        }
        /// <summary>
        /// 初始化表格
        /// </summary>
        /// <param name="isInit">是否是初始化</param>
        /// <returns></returns>
        protected DataTable GetDataFromGrid(bool isInit)
        {
            DataTable dt = new DataTable();

            #region

            dt.Columns.Add("PARTNM");

            dt.Columns.Add("TUHAO");

            dt.Columns.Add("PJNUM");

            dt.Columns.Add("DANZHONG");

            dt.Columns.Add("ZONGZHONG");

            dt.Columns.Add("JHSTATE");

            dt.Columns.Add("CONT");

            #endregion

            if (!isInit)
            {
                for (int i = 0; i < RepeaterItem.Items.Count; i++)
                {
                    RepeaterItem ri = RepeaterItem.Items[i];

                    DataRow newRow = dt.NewRow();


                    newRow["PARTNM"] = ((TextBox)ri.FindControl("TextBoxMarName")).Text;
                    newRow["TUHAO"] = ((TextBox)ri.FindControl("TextBoxDrawingNO")).Text;
                    newRow["PJNUM"] = ((TextBox)ri.FindControl("TextBoxNum")).Text;
                    newRow["DANZHONG"] = ((TextBox)ri.FindControl("TextBoxSW")).Text;
                    newRow["ZONGZHONG"] = ((TextBox)ri.FindControl("TextBoxSumW")).Text;
                    newRow["JHSTATE"] = ((TextBox)ri.FindControl("TextBoxState")).Text;
                    newRow["CONT"] = ((TextBox)ri.FindControl("TextBoxControlContent")).Text;

                    dt.Rows.Add(newRow);
                }
            }

            if (isInit)
            {
                for (int i =0; i < 10; i++)
                {
                    DataRow newRow = dt.NewRow();

                    dt.Rows.Add(newRow);
                }
            }

            dt.AcceptChanges();

            return dt;
        }
        protected void Insert_Click(object sender, EventArgs e)
        {
            DataTable dt = this.GetDataFromGrid(false);
            for (int i = 0; i < RepeaterItem.Items.Count; i++)
            {
                RepeaterItem ri = RepeaterItem.Items[i];
                CheckBox cb = (CheckBox)ri.FindControl("CheckBox1");
                if (cb.Checked)
                {
                    DataRow newRow = dt.NewRow(); 
                    newRow["JHSTATE"] = dt.Rows[i]["JHSTATE"].ToString();
                    dt.Rows.InsertAt(newRow, i + 1 + count);
                    dt.AcceptChanges();
                    count++;
                }
            }

            RepeaterItem.DataSource = dt;

            RepeaterItem.DataBind();
        }
        protected void Delete_Click(object sender, EventArgs e)
        {
            DataTable dt = this.GetDataFromGrid(false);
            for (int i = 0; i < RepeaterItem.Items.Count; i++)
            {
                RepeaterItem ri = RepeaterItem.Items[i];
                CheckBox cb = (CheckBox)ri.FindControl("CheckBox1");
                if (cb.Checked)
                {
                    DataRow newRow = dt.NewRow();
                    dt.Rows.RemoveAt(i-count);
                    dt.Rows.InsertAt(newRow, dt.Rows.Count+count);
                    dt.AcceptChanges();
                    count++;
                }
            }

            RepeaterItem.DataSource = dt;

            RepeaterItem.DataBind();
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            string sqltext;
            string bjname;
            string tuhao;
            float pjnum;
            float dz;
            float zongzhong;
            string conts;
            string state;
            List<string> list_sql = new List<string>();
            int insertcount = 0;//插入行数
            string unitcode = System.Guid.NewGuid().ToString();//唯一码
            string date=System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            string sss = Session["UserID"].ToString();
            for (int i = 0; i < RepeaterItem.Items.Count; i++)
            {
                RepeaterItem ri=RepeaterItem.Items[i];
                bjname=((TextBox)ri.FindControl("TextBoxMarName")).Text.Trim();
                if (bjname != "")
                {
                    insertcount++;
                    tuhao = ((TextBox)ri.FindControl("TextBoxDrawingNO")).Text.Trim();
                    pjnum = float.Parse(((TextBox)ri.FindControl("TextBoxNum")).Text.Trim() == "" ? "0" : ((TextBox)ri.FindControl("TextBoxNum")).Text.Trim());
                    dz = float.Parse(((TextBox)ri.FindControl("TextBoxSW")).Text.Trim() == "" ? "0" : ((TextBox)ri.FindControl("TextBoxSW")).Text.Trim());
                    zongzhong = float.Parse(((TextBox)ri.FindControl("TextBoxSumW")).Text.Trim() == "" ? "0" : ((TextBox)ri.FindControl("TextBoxSumW")).Text.Trim());
                    conts = ((TextBox)ri.FindControl("TextBoxControlContent")).Text.Trim();
                    state = ((TextBox)ri.FindControl("TextBoxState")).Text.Trim();
                    sqltext = "insert into TBQM_APLYFORITEM (UNIQUEID,PARTNM,TUHAO,NUM,DANZHONG,ZONGZHONG,PJNUM,CONT,JHSTATE,BJSJ) ";
                    sqltext += " values ('" + unitcode + "','" + bjname + "','" + tuhao + "'," + pjnum + "," + dz + "," + zongzhong + "," + pjnum + ",'" + conts + "','" + state + "','" + System.DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    list_sql.Add(sqltext);
                }
            }
            if (insertcount != 0)
            {
                sqltext = "insert into TBQM_APLYFORINSPCT (AFI_TSDEP,AFI_PJNAME,AFI_ENGID,AFI_ENGNAME,AFI_PARTNAME,AFI_DATACLCT,";
                sqltext += "AFI_DATE,AFI_RQSTCDATE,AFI_SUPPLERNM,AFI_ISPCTSITE,AFI_CONTACT,AFI_CONTEL,AFI_MAN,AFI_MANNM,AFI_NOTE,";
                sqltext += "UNIQUEID,AFI_NUMBER) values ('" + TextBoxDep.Text.Trim() + "','" + TextBoxProName.Value + "','" + TextBoxEngID.Text.Trim() + "',";
                sqltext += "'" + TextBoxEngNm.Value+ "','" + TextBoxPartName.Text.Trim() + "','" + TextBoxData.Text.Trim() + "','" + date + "',";
                sqltext += "'" + TextBoxDate.Text.Trim() + "','" + TextBoxSupplier.Text.Trim() + "','" + TextBoxSite.Text.Trim() + "','" + TextBoxContracter.Text.Trim() + "',";
                sqltext += "'" + TextBoxTel.Text.Trim() + "','" + Session["UserID"].ToString() + "','" + TextBoxMan.Text.Trim() + "','" + TextBoxNote.Text.Trim() + "','" + unitcode + "','1')";
                list_sql.Add(sqltext);

                DBCallCommon.ExecuteTrans(list_sql);

                //报检邮件发送
                string to = "erp@ztsm.net";

                List<string> bjEmailCC = new List<string>();

                bjEmailCC.Add("erp@ztsm.net");

                List<string> mfEmail = null;

                string body = "新的质量报检任务" + "\n" + "项  目  为:" + TextBoxProName.Value + "\n" + "设备为：" + TextBoxEngNm.Value + "\n" + "供货单位为：" + TextBoxSupplier.Text + "\n" + "报  检  人：" + TextBoxMan.Text + "\n" + "报检部件为：" + TextBoxPartName.Text + "\n" + "任务号为：" + TextBoxEngID.Text;
                string returnvalue = DBCallCommon.SendEmail(to, bjEmailCC, mfEmail, TextBoxProName.Value + "-" + TextBoxEngNm.Value + "/" + TextBoxPartName.Text+ "数字平台质量报检", body);
                if (returnvalue == "邮件已发送!")
                {

                    Response.Write("<script>alert('邮件发送成功');</script>");
                }
                else
                {
                    Response.Write("<script>alert('邮件发送不成功');</script>");

                }

                Response.Write("<script>alert('保存成功！');</script>");
                Response.Redirect("QC_Inspection_Manage.aspx");

            }
            else
            {
                Response.Write("alert('请输入报检明细信息！')");
                return;
            }
        }

        protected void Fillcontract(object sender, EventArgs e)
        {
            string supplier = TextBoxSupplier.Text.Trim();
            string sql1 = "select CS_CONNAME,CS_PHONO from TBCS_CUSUPINFO where CS_NAME='" + supplier + "'";
            DataTable dt1 = DBCallCommon.GetDTUsingSqlText(sql1);
            if (dt1.Rows.Count > 0)
            {
                TextBoxContracter.Text = dt1.Rows[0][0].ToString();
                TextBoxTel.Text = dt1.Rows[0][1].ToString();
            }
        }

        protected void Filljhstate(object sender, EventArgs e)
        {

            RepeaterItem a = ((TextBox)sender).NamingContainer as RepeaterItem;
            string b = a.ItemIndex.ToString();
            if (b == "0")
            {
                TextBox txt = (TextBox)a.FindControl("TextBoxState");
                string jhstate = txt.Text.ToString();
                if (jhstate != "")
                {
                    foreach (RepeaterItem s in RepeaterItem.Items)
                    {
                        TextBox txt1 = (TextBox)s.FindControl("TextBoxState");
                        if (txt1.Text == "")
                        {
                            txt1.Text = jhstate;
                        }
                    }
                }
            }




        }

        protected void Fillcontent(object sender, EventArgs e)
        {

            RepeaterItem a = ((TextBox)sender).NamingContainer as RepeaterItem;
            string b = a.ItemIndex.ToString();
            if (b == "0")
            {
                TextBox txt = (TextBox)a.FindControl("TextBoxControlContent");
                string content = txt.Text.ToString();
                if (content != "")
                {
                    foreach (RepeaterItem s in RepeaterItem.Items)
                    {
                        TextBox txt1 = (TextBox)s.FindControl("TextBoxControlContent");
                        if (txt1.Text == "")
                        {
                            txt1.Text = content;
                        }
                    }
                }
            }




        }
    }
}
