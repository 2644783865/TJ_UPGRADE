using System;
using System.Collections;
using System.Collections.Generic;
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


namespace ZCZJ_DPF
{
    public class BasicPage : System.Web.UI.Page
    {
        private string _PageName;

        /*******************************
         * 操作SQL的查询语句
         * ****************************/
        public DataTable _GetSqlDataTable(string sqlText)
        {
            //Trace.Write(sqlText);

            DataTable dt = new DataTable();

            SqlConnection sqlConn = new SqlConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlConn.ConnectionString = DBCallCommon.GetStringValue("connectionStrings");
            sqlCmd.Connection = sqlConn;
            sqlCmd.CommandText = sqlText;
            dt = DBCallCommon.GetDataTableUsingCmd(sqlCmd);

            return dt;
        }

      

        protected void _InitVar()
        {
           // string path = Request.FilePath.Split('?')[0];
            //_PageName = path.Substring(path.LastIndexOf('/'));
            _PageName = Request.FilePath.Split('?')[0];
            //Response.Write(_PageName);
            //Response.End();
            //if (_PageName.Split('/').Count() > 2)
            if (_PageName.Contains("dmp"))
            {
                _PageName = _PageName.Substring(_PageName.Split('/')[1].Length + 1);
            }
            // Response.Write(_PageName);
            // Response.End();
        }

        /*------------------------
         * 获取基本控件数据
         * ----------------------*/
        public List<string> _GetBasicControlID()
        {
            List<string> lsOut = new List<string>();

            //string _PageName = Request.FilePath.Split('?')[0];

            DataTable dt = _GetSqlDataTable("Select * From page_control Where page_id in ("
                                         + "Select page_id From power_page Where page='" + _PageName + "')"
                                          );

            foreach (DataRow dr in dt.Rows)
            {
                string strTemp = "";
                if (dr["namingContainer"].ToString() != "")
                {
                    strTemp = dr["namingContainer"].ToString() + "$" + dr["control"].ToString();
                }
                else
                {
                    strTemp = dr["control"].ToString();
                }

                lsOut.Add(strTemp);
            }

            return lsOut;
        }

        /*------------------------
         * 获取要显示的控件ID
         * ----------------------*/
        public List<string> _GetControlIDToBeChecked()
        {
            List<string> lsOut = new List<string>();

            //string _PageName = Request.FilePath.Split('/')[Request.FilePath.Split('/').Length - 1].Split('?')[0];
            //string _PageName = Request.FilePath.Split('?')[0];
            string ss = "Select control ,namingContainer from page_control "
                                         + "Where con_id in (Select control_id From power_role Where r_id in ( select r_id from role_info where r_name in (" + Session["UserGroup"] + ")) "
                                         + "and page_id in (Select page_id From power_page Where page='" + _PageName + "'))";
            DataTable dt = _GetSqlDataTable(ss);

            foreach (DataRow dr in dt.Rows)
            {
                string strTemp = "";
                if (dr["namingContainer"].ToString() != "")
                {
                    strTemp = dr["namingContainer"].ToString() + "$" + dr["control"].ToString();
                }
                else
                {
                    strTemp = dr["control"].ToString();
                }

                lsOut.Add(strTemp);
            }

            return lsOut;
        }

        /*------------------------
         * 凑出不用显示的ID
         * ----------------------*/
        protected List<string> _MergeList(List<string> lsBasic, List<string> ls)
        {
            List<string> lsOthers = lsBasic;

            foreach (string s in ls)
            {
                if (lsBasic.Contains(s))
                {
                    lsBasic.Remove(s);
                }
            }

            lsOthers = lsBasic;

            return lsOthers;
        }

        /*------------------
         * 设置页面控件
         * ----------------*/
        public bool _CheckPagePower(WebControl Entry)
        {
            List<string> ls = _GetControlIDToBeChecked();  //获取要显示的控件
            List<string> lsBasic = _GetBasicControlID();   //获取基本控件数
            List<string> lsOthers = _MergeList(lsBasic, ls); //获取不显示的控件

            foreach (string s in lsOthers)
            {
                if (s.Contains("$") == true)
                {
                    string strCtrlID = s.Split('$')[1];
                    //List<string> lsIn = new List<string>(s.Split('$')[0].Split(','));
                    List<string> lsIn = new List<string>();
                    string[] szArr = s.Split('$')[0].Split('/');

                    foreach (string str in szArr)
                    {
                        lsIn.Add(str);
                    }

                    try
                    {
                        _CheckControl(Entry, lsIn, strCtrlID);
                    }
                    catch (Exception)
                    { ;}
                }
                else
                {
                    try
                    {
                        _CheckControl(Entry, s);
                    }
                    catch (Exception)
                    { ;}
                }

            }

            return false;
        }

        /*------------------------
         * 设置控件(有命名空间)
         * ----------------------*/
        public void _CheckControl(WebControl Entry, List<string> lsCtrlIDs, string strCtrlID)
        {
            Control wbTemp = Entry.FindControl(lsCtrlIDs[0]);

            GetNearestControl(wbTemp, lsCtrlIDs.Count - 1, lsCtrlIDs, strCtrlID);

            //for (int i = 1; i < lsCtrlIDs.Count; i++)
            //{
            //    if (wbTemp.GetType().FullName.Equals("System.Web.UI.WebControls.Repeater"))
            //    {
            //        foreach (RepeaterItem RI in ((Repeater)wbTemp).Items)
            //        {
            //            wbTemp = (Control)RI.FindControl(lsCtrlIDs[i]);
            //        }
            //    }
            //}

            //if (wbTemp.GetType().FullName.Equals("System.Web.UI.WebControls.Repeater"))
            //{
            //    foreach (RepeaterItem RI in ((Repeater)wbTemp).Items)
            //    {
            //        Control wbFinal = (Control)RI.FindControl(strCtrlID);
            //        wbFinal.Visible = false;
            //    }
            //}
        }

        //Entry需要为一个名称空间的控件
        public void GetNearestControl(Control Entry, int nCtrlCount, List<string> lsCtrlIDs, string strCtrlID)
        {
            if (nCtrlCount > 0)
            {
                if (Entry.GetType().FullName.Equals("System.Web.UI.WebControls.Repeater"))
                {
                    foreach (RepeaterItem RI in ((Repeater)Entry).Items)
                    {
                        Control wbTemp = (Control)RI.FindControl(lsCtrlIDs[lsCtrlIDs.Count - nCtrlCount]);
                        GetNearestControl(wbTemp, nCtrlCount - 1, lsCtrlIDs, strCtrlID);
                    }
                }
                else if (Entry.GetType().FullName.Equals("System.Web.UI.WebControls.GridView"))
                {
                    foreach (GridViewRow gvr in ((GridView)Entry).Rows)
                    {
                        Control wbTemp = (Control)gvr.FindControl(lsCtrlIDs[lsCtrlIDs.Count - nCtrlCount]);
                        GetNearestControl(wbTemp, nCtrlCount - 1, lsCtrlIDs, strCtrlID);
                    }                    
                }
                else if (Entry.GetType().FullName.Equals("System.Web.UI.WebControls.DataList"))
                {
                    foreach (DataListItem dli in ((DataList)Entry).Items)
                    {
                        Control wbTemp = (Control)dli.FindControl(lsCtrlIDs[lsCtrlIDs.Count - nCtrlCount]);
                        GetNearestControl(wbTemp, nCtrlCount - 1, lsCtrlIDs, strCtrlID);
                    }
                }
                else if (Entry.GetType().FullName.Equals("System.Web.UI.WebControls.DetailsView"))
                {
                    foreach (DetailsViewRow dvr in ((DetailsView)Entry).Rows)
                    {
                        Control wbTemp = (Control)dvr.FindControl(lsCtrlIDs[lsCtrlIDs.Count - nCtrlCount]);
                        GetNearestControl(wbTemp, nCtrlCount - 1, lsCtrlIDs, strCtrlID);
                    }
                }
                else if (Entry.GetType().FullName.Equals("System.Web.UI.WebControls.ListView"))
                {
                    foreach (ListViewDataItem lvdi in ((ListView)Entry).Items)
                    {
                        Control wbTemp = (Control)lvdi.FindControl(lsCtrlIDs[lsCtrlIDs.Count - nCtrlCount]);
                        GetNearestControl(wbTemp, nCtrlCount - 1, lsCtrlIDs, strCtrlID);
                    }
                }

                //AjaxControlToolkit.TabContainer
                else if (Entry.GetType().FullName.Equals("AjaxControlToolkit.TabContainer"))
                {

                        string[] ctrlid = lsCtrlIDs.ToArray();
                        if (ctrlid.Length == 2)
                        {

                            Control wbFinal = ((AjaxControlToolkit.TabContainer)Entry).FindControl(ctrlid[1]).FindControl(strCtrlID);
                            wbFinal.Visible = false;
                        }
                        //TabContainer中有YYcontrol
                        else if (ctrlid.Length == 3)
                        {
                            Control con = ((AjaxControlToolkit.TabContainer)Entry).FindControl(ctrlid[1]).FindControl(ctrlid[2]);
                            if (con.GetType().FullName.Equals("YYControls.SmartGridView"))
                            {
                                foreach (GridViewRow gvr in ((YYControls.SmartGridView)con).Rows)
                                {
                                    Control wbFinal = (Control)gvr.FindControl(strCtrlID);
                                    wbFinal.Visible = false;
                                }
                            }
                            else if (con.GetType().FullName.Equals("System.Web.UI.WebControls.GridView"))
                            {

                                foreach (GridViewRow gvr in ((GridView)con).Rows)
                                {
                                    Control wbFinal = (Control)gvr.FindControl(strCtrlID);
                                    wbFinal.Visible = false;
                                } 
                            }
                        }

                        
                       
                       
                }
                //else if (Entry.GetType().FullName.Equals("YYControls.SmartGridView"))
                //{
                //    string[] ctrlid = lsCtrlIDs.ToArray();

                //    Control wbFinal = ((YYControls.SmartGridView)Entry).FindControl(ctrlid[0]).FindControl(strCtrlID);
                    
                //    wbFinal.Visible = false;
                //}
            }
            else
            {
                //设置最终控件
                if (Entry.GetType().FullName.Equals("System.Web.UI.WebControls.Repeater"))
                {
                    foreach (RepeaterItem RI in ((Repeater)Entry).Items)
                    {
                        Control wbFinal = (Control)RI.FindControl(strCtrlID);
                        wbFinal.Visible = false;
                    }
                }
                else if (Entry.GetType().FullName.Equals("System.Web.UI.WebControls.GridView"))
                {
                    foreach (GridViewRow gvr in ((GridView)Entry).Rows)
                    {
                        Control wbFinal = (Control)gvr.FindControl(strCtrlID);
                        wbFinal.Visible = false;
                    }
                }
                else if (Entry.GetType().FullName.Equals("System.Web.UI.WebControls.DataList"))
                {
                    foreach (DataListItem dli in ((DataList)Entry).Items)
                    {
                        Control wbFinal = (Control)dli.FindControl(strCtrlID);
                        wbFinal.Visible = false;
                    }
                }
                else if (Entry.GetType().FullName.Equals("System.Web.UI.WebControls.DetailsView"))
                {
                    foreach (DetailsViewRow dvr in ((DetailsView)Entry).Rows)
                    {
                        Control wbFinal = (Control)dvr.FindControl(strCtrlID);
                        wbFinal.Visible = false;
                    }
                }
                else if (Entry.GetType().FullName.Equals("System.Web.UI.WebControls.ListView"))
                {
                    foreach (ListViewDataItem lvdi in ((ListView)Entry).Items)
                    {
                        Control wbFinal = (Control)lvdi.FindControl(strCtrlID);
                        wbFinal.Visible = false;
                    }
                }

                    //yycontrol.smartgridview
                else if (Entry.GetType().FullName.Equals("YYControls.SmartGridView"))
                {
                    
                    foreach (GridViewRow gvr in ((YYControls.SmartGridView)Entry).Rows)
                    {
                        Control wbFinal = (Control)gvr.FindControl(strCtrlID);
                        wbFinal.Visible = false;
                    }
                }

                else if (Entry.GetType().FullName.Equals("AjaxControlToolkit.TabContainer"))
                {

                    string[] ctrlid = lsCtrlIDs.ToArray();

                    Control wbFinal = ((AjaxControlToolkit.TabContainer)Entry).FindControl(ctrlid[1]).FindControl(strCtrlID);

                    wbFinal.Visible = false;


                }

                return;
            }
        }

        /*------------------------
         * 设置控件(无命名空间)
         * ----------------------*/
        public void _CheckControl(Control Entry, string strCtrlID)
        {
            Control wb = (Control)Entry.FindControl(strCtrlID);
            wb.Visible = false;
        }


        /*------------------------
         * 检查是否对页面有访问权
         * ----------------------*/
        protected bool _CheckPage()
        {
            // Response.Write("Select Count(*) as TotalCount From Power_Group Where GroupName in (" + Session["UserGroup"] +
            //                                ") and PageID=(Select ID From Power_Action_Page Where Page='" + _PageName + "')");
            // Response.End();
            if (Session["UserGroup"] != null && Session["UserGroup"].ToString() != "")
            {
                string ss = "Select Count(*) as TotalCount From power_role where r_id in (select r_id from role_info Where r_name in (" + Session["UserGroup"] +
                                                ")) and page_id in (Select page_id From power_page Where page='" + _PageName + "')";
                DataTable dt = _GetSqlDataTable(ss);

                if ((int)dt.Rows[0][0] > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        /*------------------------
         * 检查用户权限
         * ----------------------*/
        protected void CheckUser(WebControl Entry)
        {
            if (Session["UserID"] != null && Session["UserID"].ToString() != "")
            {
                _InitVar();

                if (_CheckPage())
                {
                    _CheckPagePower(Entry);
                }
                else
                {
                    //Response.Redirect("~/Default.aspx");
                    Response.Write("<script>if(window.parent!=null)window.parent.location.href='../../Default.aspx';else{window.location.href='./Default.aspx'} </script>");

                }
            }
            else
            {
                Response.Write("<script>alert('会话已过期,请重新登录！！！');if(window.parent!=null)window.parent.location.href='../Default.aspx';else{window.location.href='./Default.aspx'} </script>");
            }
        }


        protected override void OnPreRender(EventArgs e)
        {
            base.OnPreRender(e);
        }
    }
}  

