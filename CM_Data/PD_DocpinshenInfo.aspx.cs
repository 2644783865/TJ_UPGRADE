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
using System.IO;
using System.Collections.Generic;
using System.Text;


namespace ZCZJ_DPF.CM_Data
{
    public partial class PD_DocpinshenInfo : System.Web.UI.Page
    {
        string id = string.Empty;
        string action = string.Empty;
        string content = string.Empty;
        string name = string.Empty;
        string reviewx = string.Empty;
        string review = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["id"] != null)
            {
                id = Request.QueryString["id"].ToString();
            }
            if (Request.QueryString["action"] != null)
            {
                action = Request.QueryString["action"].ToString();
            }


            if (!IsPostBack)
            {
                getContractInfo();

                showInfo();//呈现合同基本信息

                bindreviewer(false);

                GVBind();//显示合同文本信息
                if (action == "look")
                {
                    Panel1.Enabled = false;
                    PD_shenheyj1.BackColor = System.Drawing.Color.White;
                    Panel2.Enabled = false;
                    PD_shenheyj2.BackColor = System.Drawing.Color.White;
                    Panel3.Enabled = false;
                    PD_shenheyj3.BackColor = System.Drawing.Color.White;
                    Panel4.Enabled = false;
                    PD_shenheyj4.BackColor = System.Drawing.Color.White;
                    Panel5.Enabled = false;
                    PD_shenheyj5.BackColor = System.Drawing.Color.White;
                    Panel6.Enabled = false;
                    PD_shenheyj6.BackColor = System.Drawing.Color.White;
                    Panel7.Enabled = false;
                    PD_shenheyj7.BackColor = System.Drawing.Color.White;
                    Panel8.Enabled = false;
                    PD_shenheyj8.BackColor = System.Drawing.Color.White;
                    Panel9.Enabled = false;
                    PD_shenheyj9.BackColor = System.Drawing.Color.White;
                    Panel10.Enabled = false;
                    PD_shenheyj10.BackColor = System.Drawing.Color.White;

                    btn_confirm.Visible = false;
                }
            }
        }
        private void getContractInfo()
        {
            string sql = "select BC_CONTEXT from TBBS_CONREVIEW where BC_ID='" + id + "' ";//由BC_ID来获取合同文本的值
            DataSet ds = DBCallCommon.FillDataSet(sql);
            if (ds.Tables[0].Rows.Count > 0)
                content = ds.Tables[0].Rows[0][0].ToString();
        }

        #region 显示Lable的信息，显示的是查看的信息

        private void showInfo()
        {
            string sqlselect = "select * from TBBS_CONREVIEW where BC_ID='" + id + "'";
            DataSet ds = DBCallCommon.FillDataSet(sqlselect);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lb_pronm.Text = ds.Tables[0].Rows[0]["PROJECT"].ToString();

                lb_engnm.Text = ds.Tables[0].Rows[0]["ENGINEER"].ToString();

                lb_yz.Text = ds.Tables[0].Rows[0]["YEZHU"].ToString();

                lb_je.Text = ds.Tables[0].Rows[0]["JINE"].ToString();

                lbMeno.Text = ds.Tables[0].Rows[0]["BP_NOTE"].ToString();//备注

                lb_swfzr.Text = bindNameByID(ds.Tables[0].Rows[0]["BC_DRAFTER"].ToString());
                /********************************************/

                Lbstatus.Text = ds.Tables[0].Rows[0]["BP_SPSTATUS"].ToString();//读取字段状态

                /********************************************/

                PD_shenheren1.Text = bindNameByID(ds.Tables[0].Rows[0]["BC_REVIEWERA"].ToString());//评审人1的姓名

                if (PD_shenheren1.Text != string.Empty)
                {
                    Panel1.Visible = true;
                    string content = ds.Tables[0].Rows[0]["BP_RVIEWA"].ToString();
                    if (content.Length > 1)
                    {
                        string[] yj1 = content.Split('△');
                        PD_shenheyj1.Text = yj1[0];
                        PD_shenhedate1.Text = yj1[1];
                        for (int i = 0; i < PD_jielun1.Items.Count; i++)
                        {
                            if (PD_jielun1.Items[i].Value == yj1[2])
                            {
                                PD_jielun1.Items[i].Selected = true;
                            }
                        }
                    }
                }

                PD_shenheren2.Text = bindNameByID(ds.Tables[0].Rows[0]["BC_REVIEWERB"].ToString());//评审人2的姓名
                if (PD_shenheren2.Text != string.Empty)
                {
                    Panel2.Visible = true;
                    string content = ds.Tables[0].Rows[0]["BP_RVIEWB"].ToString();
                    if (content.Length > 1)
                    {
                        string[] yj2 = content.Split('△');
                        PD_shenheyj2.Text = yj2[0];
                        PD_shenhedate2.Text = yj2[1];
                        for (int i = 0; i < PD_jielun2.Items.Count; i++)
                        {
                            if (PD_jielun2.Items[i].Value == yj2[2])
                            {
                                PD_jielun2.Items[i].Selected = true;
                            }
                        }
                    }
                }

                PD_shenheren3.Text = bindNameByID(ds.Tables[0].Rows[0]["BC_REVIEWERC"].ToString());//评审人3的姓名
                if (PD_shenheren3.Text != string.Empty)
                {
                    Panel3.Visible = true;
                    string content = ds.Tables[0].Rows[0]["BP_RVIEWC"].ToString();
                    if (content.Length > 1)
                    {
                        string[] yj3 = content.Split('△');
                        PD_shenheyj3.Text = yj3[0];
                        PD_shenhedate3.Text = yj3[1];
                        for (int i = 0; i < PD_jielun3.Items.Count; i++)
                        {
                            if (PD_jielun3.Items[i].Value == yj3[2])
                            {
                                PD_jielun3.Items[i].Selected = true;
                            }
                        }
                    }

                }

                PD_shenheren4.Text = bindNameByID(ds.Tables[0].Rows[0]["BC_REVIEWERD"].ToString());//评审人4的姓名
                if (PD_shenheren4.Text != string.Empty)
                {
                    Panel4.Visible = true;
                    string content = ds.Tables[0].Rows[0]["BP_RVIEWD"].ToString();
                    if (content.Length > 1)
                    {
                        string[] yj = content.Split('△');
                        PD_shenheyj4.Text = yj[0];
                        PD_shenhedate4.Text = yj[1];
                        for (int i = 0; i < PD_jielun4.Items.Count; i++)
                        {
                            if (PD_jielun4.Items[i].Value == yj[2])
                            {
                                PD_jielun4.Items[i].Selected = true;
                            }
                        }
                    }

                }

                PD_shenheren5.Text = bindNameByID(ds.Tables[0].Rows[0]["BC_REVIEWERE"].ToString());//评审人5的姓名
                if (PD_shenheren5.Text != string.Empty)
                {
                    Panel5.Visible = true;
                    string content = ds.Tables[0].Rows[0]["BP_RVIEWE"].ToString();
                    if (content.Length > 1)
                    {
                        string[] yj = content.Split('△');
                        PD_shenheyj5.Text = yj[0];
                        PD_shenhedate5.Text = yj[1];
                        for (int i = 0; i < PD_jielun5.Items.Count; i++)
                        {
                            if (PD_jielun5.Items[i].Value == yj[2])
                            {
                                PD_jielun5.Items[i].Selected = true;
                            }
                        }
                    }
                }

                PD_shenheren6.Text = bindNameByID(ds.Tables[0].Rows[0]["BC_REVIEWERF"].ToString());//评审人6的姓名
                if (PD_shenheren6.Text != string.Empty)
                {
                    Panel6.Visible = true;
                    string content = ds.Tables[0].Rows[0]["BP_RVIEWF"].ToString();
                    if (content.Length > 1)
                    {
                        string[] yj = content.Split('△');
                        PD_shenheyj6.Text = yj[0];
                        PD_shenhedate6.Text = yj[1];
                        for (int i = 0; i < PD_jielun6.Items.Count; i++)
                        {
                            if (PD_jielun6.Items[i].Value == yj[2])
                            {
                                PD_jielun6.Items[i].Selected = true;
                            }
                        }
                    }
                }

                PD_shenheren7.Text = bindNameByID(ds.Tables[0].Rows[0]["BC_REVIEWERG"].ToString());//评审人7的姓名
                if (PD_shenheren7.Text != string.Empty)
                {
                    Panel7.Visible = true;
                    string content = ds.Tables[0].Rows[0]["BP_RVIEWG"].ToString();
                    if (content.Length > 1)
                    {
                        string[] yj = content.Split('△');
                        PD_shenheyj7.Text = yj[0];
                        PD_shenhedate7.Text = yj[1];
                        for (int i = 0; i < PD_jielun7.Items.Count; i++)
                        {
                            if (PD_jielun7.Items[i].Value == yj[2])
                            {
                                PD_jielun7.Items[i].Selected = true;
                            }
                        }
                    }
                }

                PD_shenheren8.Text = bindNameByID(ds.Tables[0].Rows[0]["BC_REVIEWERH"].ToString());//评审人8的姓名
                if (PD_shenheren8.Text != string.Empty)
                {
                    Panel8.Visible = true;
                    string content = ds.Tables[0].Rows[0]["BP_RVIEWH"].ToString();
                    if (content.Length > 1)
                    {
                        string[] yj = content.Split('△');
                        PD_shenheyj8.Text = yj[0];
                        PD_shenhedate8.Text = yj[1];
                        for (int i = 0; i < PD_jielun8.Items.Count; i++)
                        {
                            if (PD_jielun8.Items[i].Value == yj[2])
                            {
                                PD_jielun8.Items[i].Selected = true;
                            }
                        }
                    }
                }

                PD_shenheren9.Text = bindNameByID(ds.Tables[0].Rows[0]["BC_REVIEWERI"].ToString());//评审人9的姓名
                if (PD_shenheren9.Text != string.Empty)
                {
                    Panel9.Visible = true;

                    string content = ds.Tables[0].Rows[0]["BP_RVIEWI"].ToString();
                    if (content.Length > 1)
                    {
                        string[] yj = content.Split('△');
                        PD_shenheyj9.Text = yj[0];
                        PD_shenhedate9.Text = yj[1];
                        for (int i = 0; i < PD_jielun9.Items.Count; i++)
                        {
                            if (PD_jielun9.Items[i].Value == yj[2])
                            {
                                PD_jielun9.Items[i].Selected = true;
                            }
                        }
                    }
                }

                PD_shenheren10.Text = bindNameByID(ds.Tables[0].Rows[0]["BC_REVIEWERJ"].ToString());//评审人10的姓名
                if (PD_shenheren10.Text != string.Empty)
                {
                    Panel10.Visible = true;
                    string content = ds.Tables[0].Rows[0]["BP_RVIEWJ"].ToString();
                    if (content.Length > 1)
                    {
                        string[] yj = content.Split('△');
                        PD_shenheyj10.Text = yj[0];
                        PD_shenhedate10.Text = yj[1];
                        for (int i = 0; i < PD_jielun10.Items.Count; i++)
                        {
                            if (PD_jielun10.Items[i].Value == yj[2])
                            {
                                PD_jielun10.Items[i].Selected = true;
                            }
                        }
                    }
                }
            }
        }

        private string bindNameByID(string id)
        {
            string sqlSelect = "select ST_NAME from TBDS_STAFFINFO where ST_ID='" + id + "'";
            string st_name = string.Empty;
            using (DataSet ds = DBCallCommon.FillDataSet(sqlSelect))
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    st_name = ds.Tables[0].Rows[0]["ST_NAME"].ToString();
                }
            }
            return st_name;
        }

        #endregion

        void GVBind()
        {
            string sql = "select * from tb_files where BC_CONTEXT='" + content + "'";
            DataSet ds = DBCallCommon.FillDataSet(sql);
            ViewGridViewFiles.DataSource = ds.Tables[0];
            ViewGridViewFiles.DataBind();
            ViewGridViewFiles.DataKeyNames = new string[] { "fileID" };
        }

        #region  下载文件

        protected void imgbtnDF_Init(object sender, EventArgs e)
        {
            ToolkitScriptManager1.RegisterPostBackControl((Control)sender);
        }

        protected void imgbtnDF_Click(object sender, ImageClickEventArgs e)
        {
            //Response.Write("gvr");
            ////获取imgbtnDelete的ImageButton对象
            ImageButton imgbtn = (ImageButton)sender;
            //引用imgbtnDelete控件的父控件上一级控件
            GridViewRow gvr = (GridViewRow)imgbtn.Parent.Parent;
            GridView gv = (GridView)imgbtn.Parent.Parent.Parent.Parent;
            //获取文件真实姓名
            string sqlStr = "select fileload,fileName from tb_files where fileID='" + gv.DataKeys[gvr.RowIndex].Value.ToString() + "'";
            //打开数据库
            //Response.Write(sqlStr);         
            DataSet ds = DBCallCommon.FillDataSet(sqlStr);
            //获取文件路径
            string strFilePath = ds.Tables[0].Rows[0]["fileload"].ToString() + @"\" + ds.Tables[0].Rows[0]["fileName"].ToString();
            //Response.Write(strFilePath);
            if (File.Exists(strFilePath))
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
            }
            else
            {
                filesError.Visible = true;
                filesError.Text = "文件已被删除，请通知相关人员上传文件！";
            }
        }
        #endregion

        protected void btn_confirm_Click(object sender, EventArgs e)
        {
            name = Session["UserID"].ToString();
            /************评审状态************/
            char BP_STATUS = '0';
            bindreviewer(true);
            /*******评审完了的状态********/
            string BP_SPSTATUS = Lbstatus.Text.Trim();
            int len = Lbstatus.Text.Trim().Length;
            StringBuilder sb_status = new StringBuilder();
            for (int i = 0; i < len; i++)
            {
                sb_status.Append("1");
            }
            if (BP_SPSTATUS == sb_status.ToString())
            {
                BP_STATUS = '1';
            }
            if (reviewx != string.Empty && review != string.Empty)
            {
                string sqlupdate = string.Empty;
                if (lbyesorno.Text == "N")
                {
                    sqlupdate = "update TBBS_CONREVIEW set " + reviewx + " ='" + review + "',BP_SPSTATUS='" + BP_SPSTATUS + "',BP_STATUS='" + BP_STATUS + "',BP_YESORNO='" + lbyesorno.Text + "' where BC_ID='" + id + "'";
                }
                else if (lbyesorno.Text == "Y")
                {
                    if (Session["UserDeptID"].ToString().Equals("01"))
                    {
                        sqlupdate = "update TBBS_CONREVIEW set " + reviewx + " ='" + review + "',BP_SPSTATUS='" + BP_SPSTATUS + "',BP_STATUS='" + BP_STATUS + "' where BC_ID='" + id + "'";
                    }
                    else
                    {
                        sqlupdate = "update TBBS_CONREVIEW set " + reviewx + " ='" + review + "',BP_SPSTATUS='" + BP_SPSTATUS + "',BP_STATUS='" + BP_STATUS + "',BP_LEADPS='" + getReviewer() + "' where BC_ID='" + id + "'";
                    }
                }
                else if (lbyesorno.Text == "")
                {
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "", "alert('请选择评论结果！！！');", true);
                    return;
                }
                DBCallCommon.ExeSqlText(sqlupdate);
            }
            Response.Redirect("PD_DocManage.aspx");
        }

        private int getReviewer()
        {
            string sqlSelect = "select * from TBBS_CONREVIEW where BC_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlSelect);
            DataRow dr = dt.Rows[0];
            int num = 0;
            num = dr["BC_REVIEWERG"].ToString() == "" ? num : num++;
            num = dr["BC_REVIEWERH"].ToString() == "" ? num : num++;
            num = dr["BC_REVIEWERI"].ToString() == "" ? num : num++;
            num = dr["BC_REVIEWERJ"].ToString() == "" ? num : num++;
            string status = dr["BP_SPSTATUS"].ToString();
            status = status.Substring(0, status.Length - num - 1);
            StringBuilder sb_status = new StringBuilder();
            for (int i = 0; i < status.Length; i++)
            {
                sb_status.Append("1");
            }
            if (status == sb_status.ToString())
            {
                num = 1;
            }
            else
            {
                num = 0;
            }
            return num;
        }

        void bindreviewer(bool Ischange)
        {
            if (Session["UserID"] != null)

                name = Session["UserID"].ToString();

            Dictionary<int, string> reviewer = new Dictionary<int, string>();//可以绑定权限

            string sqlselect = "select * from TBBS_CONREVIEW where BC_ID='" + id + "'";

            DataSet ds = DBCallCommon.FillDataSet(sqlselect);

            if (ds.Tables[0].Rows.Count > 0)
            {
                reviewer.Add(1, ds.Tables[0].Rows[0]["BC_REVIEWERA"].ToString());
                reviewer.Add(2, ds.Tables[0].Rows[0]["BC_REVIEWERB"].ToString());
                reviewer.Add(3, ds.Tables[0].Rows[0]["BC_REVIEWERC"].ToString());
                reviewer.Add(4, ds.Tables[0].Rows[0]["BC_REVIEWERD"].ToString());
                reviewer.Add(5, ds.Tables[0].Rows[0]["BC_REVIEWERE"].ToString());
                reviewer.Add(6, ds.Tables[0].Rows[0]["BC_REVIEWERF"].ToString());
                reviewer.Add(7, ds.Tables[0].Rows[0]["BC_REVIEWERG"].ToString());
                reviewer.Add(8, ds.Tables[0].Rows[0]["BC_REVIEWERH"].ToString());
                reviewer.Add(9, ds.Tables[0].Rows[0]["BC_REVIEWERI"].ToString());
                reviewer.Add(10, ds.Tables[0].Rows[0]["BC_REVIEWERJ"].ToString());
            }
            for (int i = 0; i < reviewer.Count; i++)
            {
                if (name == reviewer.Values.ElementAt(i))
                {
                    bindtxtdata(reviewer.Keys.ElementAt(i));
                    if (Ischange)
                    {
                        Lbstatus.Text = setStatusValue(Lbstatus.Text.Trim());
                    }
                }
            }
        }

        private string setStatusValue(string lb)
        {
            char[] status = lb.ToCharArray();
            for (int j = 0; j < status.Length; j++)
            {
                if (status[j] == '0')
                {
                    status[j] = '1';
                    break;
                }
            }
            StringBuilder sb = new StringBuilder();
            sb.Append(status);
            return sb.ToString();
        }

        /***********************************绑定其是否可以评审**************************************************/
        private void bindtxtdata(int i)
        {
            string sqlselect = "select * from TBBS_CONREVIEW where BC_ID='" + id + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlselect);
            switch (i)
            {
                //评审人一
                case 1:
                    {
                        string bp_A = dt.Rows[0]["BP_RVIEWA"].ToString();
                        if (bp_A.Contains("△") && bp_A.Contains("1") && bp_A.Contains("0"))//已经审核过的
                        {
                            Panel1.Enabled = false;
                        }
                        else
                        {
                            PD_shenhedate1.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            review = PD_shenheyj1.Text + "△" + PD_shenhedate1.Text + "△" + PD_jielun1.SelectedValue;
                            reviewx = "BP_RVIEWA";
                            Panel1.Enabled = true;
                            PD_shenheyj1.BackColor = System.Drawing.Color.LightCoral;
                            if (PD_jielun1.SelectedValue == "0")
                            {
                                lbyesorno.Text = "N";
                            }
                            else if (PD_jielun1.SelectedValue == "1")
                            {
                                lbyesorno.Text = "Y";
                            }
                            else
                            {
                                lbyesorno.Text = "";
                            }
                        }
                        break;
                    }
                case 2:
                    {
                        string bp_B = dt.Rows[0]["BP_RVIEWB"].ToString();
                        if (bp_B.Contains("△") && bp_B.Contains("1") && bp_B.Contains("0"))//已经审核过的
                        {
                            Panel2.Enabled = false;
                        }
                        else
                        {
                            PD_shenhedate2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            review = Server.HtmlEncode(PD_shenheyj2.Text + "△" + PD_shenhedate2.Text + "△" + PD_jielun2.SelectedValue);
                            reviewx = "BP_RVIEWB";
                            Panel2.Enabled = true;
                            PD_shenheyj2.BackColor = System.Drawing.Color.LightCoral;
                            if (PD_jielun2.SelectedValue == "0")
                            {
                                lbyesorno.Text = "N";
                            }
                            else if (PD_jielun2.SelectedValue == "1")
                            {
                                lbyesorno.Text = "Y";
                            }
                            else
                            {
                                lbyesorno.Text = "";
                            }
                        }
                        break;
                    }
                case 3:
                    {
                        string bp_C = dt.Rows[0]["BP_RVIEWC"].ToString();
                        if (bp_C.Contains("△") && bp_C.Contains("1") && bp_C.Contains("0"))//已经审核过的
                        {
                            Panel3.Enabled = false;
                        }
                        else
                        {
                            PD_shenhedate3.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            review = PD_shenheyj3.Text + "△" + PD_shenhedate3.Text + "△" + PD_jielun3.SelectedValue;
                            reviewx = "BP_RVIEWC";
                            Panel3.Enabled = true;
                            PD_shenheyj3.BackColor = System.Drawing.Color.LightCoral;
                            if (PD_jielun3.SelectedValue == "0")
                            {
                                lbyesorno.Text = "N";
                            }
                            else if (PD_jielun3.SelectedValue == "1")
                            {
                                lbyesorno.Text = "Y";
                            }
                            else
                            {
                                lbyesorno.Text = "";
                            }
                        }
                        break;
                    }
                case 4:
                    {
                        string bp_D = dt.Rows[0]["BP_RVIEWD"].ToString();
                        if (bp_D.Contains("△") && bp_D.Contains("1") && bp_D.Contains("0"))//已经审核过的
                        {
                            Panel4.Enabled = false;
                        }
                        else
                        {
                            PD_shenhedate4.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            review = PD_shenheyj4.Text + "△" + PD_shenhedate4.Text + "△" + PD_jielun4.SelectedValue;
                            reviewx = "BP_RVIEWD";
                            Panel4.Enabled = true;
                            PD_shenheyj4.BackColor = System.Drawing.Color.LightCoral;
                            if (PD_jielun4.SelectedValue == "0")
                            {
                                lbyesorno.Text = "N";
                            }
                            else if (PD_jielun4.SelectedValue == "1")
                            {
                                lbyesorno.Text = "Y";
                            }
                            else
                            {
                                lbyesorno.Text = "";
                            }
                        }
                        break;
                    }
                case 5:
                    {
                        string bp_E = dt.Rows[0]["BP_RVIEWE"].ToString();
                        if (bp_E.Contains("△") && bp_E.Contains("1") && bp_E.Contains("0"))//已经审核过的
                        {
                            Panel5.Enabled = false;
                        }
                        else
                        {
                            PD_shenhedate5.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            review = PD_shenheyj5.Text + "△" + PD_shenhedate5.Text + "△" + PD_jielun5.SelectedValue;
                            reviewx = "BP_RVIEWE";
                            Panel5.Enabled = true;
                            PD_shenheyj5.BackColor = System.Drawing.Color.LightCoral;
                            if (PD_jielun5.SelectedValue == "0")
                            {
                                lbyesorno.Text = "N";
                            }
                            else if (PD_jielun5.SelectedValue == "1")
                            {
                                lbyesorno.Text = "Y";
                            }
                            else
                            {
                                lbyesorno.Text = "";
                            }
                        }
                        break;
                    }
                case 6:
                    {
                        string bp_F = dt.Rows[0]["BP_RVIEWF"].ToString();
                        if (bp_F.Contains("△") && bp_F.Contains("1") && bp_F.Contains("0"))//已经审核过的
                        {
                            Panel6.Enabled = false;
                        }
                        else
                        {
                            PD_shenhedate6.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            review = PD_shenheyj6.Text + "△" + PD_shenhedate6.Text + "△" + PD_jielun6.SelectedValue;
                            reviewx = "BP_RVIEWF";
                            Panel6.Enabled = true;
                            PD_shenheyj6.BackColor = System.Drawing.Color.LightCoral;
                            if (PD_jielun6.SelectedValue == "0")
                            {
                                lbyesorno.Text = "N";
                            }
                            else if (PD_jielun6.SelectedValue == "1")
                            {
                                lbyesorno.Text = "Y";
                            }
                            else
                            {
                                lbyesorno.Text = "";
                            }
                        }
                        break;
                    }
                case 7:
                    {
                        string bp_G = dt.Rows[0]["BP_RVIEWG"].ToString();
                        if (bp_G.Contains("△") && bp_G.Contains("1") && bp_G.Contains("0"))//已经审核过的
                        {
                            Panel7.Enabled = false;
                        }
                        else
                        {
                            PD_shenhedate7.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            review = PD_shenheyj7.Text + "△" + PD_shenhedate7.Text + "△" + PD_jielun7.SelectedValue;
                            reviewx = "BP_RVIEWG";
                            Panel7.Enabled = true;
                            PD_shenheyj7.BackColor = System.Drawing.Color.LightCoral;
                            if (PD_jielun7.SelectedValue == "0")
                            {
                                lbyesorno.Text = "N";
                            }
                            else if (PD_jielun7.SelectedValue == "1")
                            {
                                lbyesorno.Text = "Y";
                            }
                            else
                            {
                                lbyesorno.Text = "";
                            }
                        }
                        break;
                    }
                case 8:
                    {
                        string bp_H = dt.Rows[0]["BP_RVIEWH"].ToString();
                        if (bp_H.Contains("△") && bp_H.Contains("1") && bp_H.Contains("0"))//已经审核过的
                        {
                            Panel8.Enabled = false;
                        }
                        else
                        {
                            PD_shenhedate8.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            review = PD_shenheyj8.Text + "△" + PD_shenhedate8.Text + "△" + PD_jielun8.SelectedValue;
                            reviewx = "BP_RVIEWH";
                            Panel8.Enabled = true;
                            PD_shenheyj8.BackColor = System.Drawing.Color.LightCoral;
                            if (PD_jielun8.SelectedValue == "0")
                            {
                                lbyesorno.Text = "N";
                            }
                            else if (PD_jielun8.SelectedValue == "1")
                            {
                                lbyesorno.Text = "Y";
                            }
                            else
                            {
                                lbyesorno.Text = "";
                            }
                        }
                        break;
                    }
                case 9:
                    {
                        string bp_I = dt.Rows[0]["BP_RVIEWI"].ToString();
                        if (bp_I.Contains("△") && bp_I.Contains("1") && bp_I.Contains("0"))//已经审核过的
                        {
                            Panel9.Enabled = false;
                        }
                        else
                        {
                            PD_shenhedate9.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            review = PD_shenheyj9.Text + "△" + PD_shenhedate9.Text + "△" + PD_jielun9.SelectedValue;
                            reviewx = "BP_RVIEWI";
                            Panel9.Enabled = true;
                            PD_shenheyj9.BackColor = System.Drawing.Color.LightCoral;
                            if (PD_jielun9.SelectedValue == "0")
                            {
                                lbyesorno.Text = "N";
                            }
                            else if (PD_jielun9.SelectedValue == "1")
                            {
                                lbyesorno.Text = "Y";
                            }
                            else
                            {
                                lbyesorno.Text = "";
                            }
                        }
                        break;
                    }
                case 10:
                    {
                        string bp_J = dt.Rows[0]["BP_RVIEWJ"].ToString();
                        if (bp_J.Contains("△") && bp_J.Contains("1") && bp_J.Contains("0"))//已经审核过的
                        {
                            Panel10.Enabled = false;
                        }
                        else
                        {
                            PD_shenhedate10.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                            review = PD_shenheyj10.Text + "△" + PD_shenhedate9.Text + "△" + PD_jielun9.SelectedValue;
                            reviewx = "BP_RVIEWJ";
                            Panel10.Enabled = true;
                            PD_shenheyj10.BackColor = System.Drawing.Color.LightCoral;
                            if (PD_jielun10.SelectedValue == "0")
                            {
                                lbyesorno.Text = "N";
                            }
                            else if (PD_jielun10.SelectedValue == "1")
                            {
                                lbyesorno.Text = "Y";
                            }
                            else
                            {
                                lbyesorno.Text = "";
                            }
                        }
                        break;
                    }
            }
        }

        protected void btn_back_Click(object sender, EventArgs e)
        {
            Response.Redirect("PD_DocManage.aspx");
        }

        //选择同意或拒绝后自动带出“同意”或“拒绝理由：”

        #region

        protected void RadioJSB_CheckedChanged(object sender, EventArgs e)//技术部
        {
            if (PD_jielun1.SelectedIndex == 0)
            {
                PD_shenheyj1.Text = "同意";
            }
            else
            {
                PD_shenheyj1.Text = "拒绝理由：";
            }
        }
        protected void RadioCGB_CheckedChanged(object sender, EventArgs e)//采购部
        {
            if (PD_jielun2.SelectedIndex == 0)
            {
                PD_shenheyj2.Text = "同意";
            }
            else
            {
                PD_shenheyj2.Text = "拒绝理由：";
            }
        }
        protected void RadioSCB_CheckedChanged(object sender, EventArgs e)//生产部
        {
            if (PD_jielun3.SelectedIndex == 0)
            {
                PD_shenheyj3.Text = "同意";
            }
            else
            {
                PD_shenheyj3.Text = "拒绝理由：";
            }
        }
        protected void RadioZLB_CheckedChanged(object sender, EventArgs e)//质量部
        {
            if (PD_jielun4.SelectedIndex == 0)
            {
                PD_shenheyj4.Text = "同意";
            }

            else
            {
                PD_shenheyj4.Text = "拒绝理由：";
            }
        }
        protected void RadioCYB_CheckedChanged(object sender, EventArgs e)//储运部
        {
            if (PD_jielun5.SelectedIndex == 0)
            {
                PD_shenheyj5.Text = "同意";
            }
            else
            {
                PD_shenheyj5.Text = "拒绝理由：";
            }
        }
        protected void RadioSJ_CheckedChanged(object sender, EventArgs e) //审计
        {
            if (PD_jielun6.SelectedIndex == 0)
            {
                PD_shenheyj6.Text = "同意";
            }
            else
            {
                PD_shenheyj6.Text = "拒绝理由：";
            }
        }
        protected void RadioPZR1_CheckedChanged(object sender, EventArgs e)
        {
            if (PD_jielun7.SelectedIndex == 0)
            {
                PD_shenheyj7.Text = "同意";
            }
            else
            {
                PD_shenheyj7.Text = "拒绝理由：";
            }
        }
        protected void RadioPZR2_CheckedChanged(object sender, EventArgs e)
        {
            if (PD_jielun8.SelectedIndex == 0)
            {
                PD_shenheyj8.Text = "同意";
            }
            else
            {
                PD_shenheyj8.Text = "拒绝理由：";
            }
        }
        protected void RadioPZR3_CheckedChanged(object sender, EventArgs e)
        {
            if (PD_jielun9.SelectedIndex == 0)
            {
                PD_shenheyj9.Text = "同意";
            }
            else
            {
                PD_shenheyj9.Text = "拒绝理由：";
            }
        }
        protected void RadioPZR4_CheckedChanged(object sender, EventArgs e)
        {
            if (PD_jielun10.SelectedIndex == 0)
            {
                PD_shenheyj10.Text = "同意";
            }
            else
            {
                PD_shenheyj10.Text = "拒绝理由：";
            }
        }

        #endregion

    }
}
