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
using System.Data.OleDb;
using System.Data.SqlClient;

namespace ZCZJ_DPF.PM_Data
{
    public partial class PM_Gongshi_Daoru : System.Web.UI.Page
    {
        //string strConn = "Data Source=192.168.10.44;DataBase=TS;Uid=sa;Pwd=123456"; //链接SQL数据库
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }
        public DataSet ExecleDs(string filenameurl, string table)
        {

            string strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + filenameurl + ";Extended Properties='Excel 8.0; HDR=YES; IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataSet ds = new DataSet();
            OleDbDataAdapter odda = new OleDbDataAdapter("select * from [Sheet1$A2:L2000]", conn);
            try
            {
                odda.Fill(ds, table);
            }
            catch (Exception)
            {
                return ds;
            }
            finally
            {

            }
            conn.Close();
            return ds;
        }

        public DataSet ExeclCOL(string filenameurl, string table)
        {

            string strConn = "Provider=Microsoft.Jet.OleDb.4.0;" + "data source=" + filenameurl + ";Extended Properties='Excel 8.0; HDR=YES; IMEX=1'";
            OleDbConnection conn = new OleDbConnection(strConn);
            conn.Open();
            DataSet ds = new DataSet();
            OleDbDataAdapter odda = new OleDbDataAdapter("select 顾客名称,合同号,任务单号,sum(加工工时费用（元）)项目工时费用 from [Sheet1$A2:I2000] group by 顾客名称,合同号,任务单号", conn);
            try
            {
                odda.Fill(ds, table);
            }
            catch (Exception)
            {
                return ds;
            }
            finally
            {

            }
            conn.Close();
            return ds;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string year;
            string month;
            if (txt_date.Text.Trim() != "")
            {
                string ps_sj = txt_date.Text.Trim().ToString();
                year = ps_sj.Split('.')[0];
                month = ps_sj.Split('.')[1];
            }
            else
            {
                Response.Write("<script>alert('请输入日期信息！！！')</script> ");
                return;
            }
            if (FileUpload1.HasFile == false)//HasFile用来检查FileUpload是否有指定文件
            {
                Response.Write("<script>alert('请您选择Excel文件')</script> ");
                return;//当无文件时,返回
            }
            string IsXls = System.IO.Path.GetExtension(FileUpload1.FileName).ToString().ToLower();//System.IO.Path.GetExtension获得文件的扩展名
            if (IsXls != ".xls")
            {
                Response.Write("<script>alert('只可以选择Excel文件')</script>");
                return;//当选择的不是Excel文件时,返回
            }

            string strConn = DBCallCommon.GetStringValue("connectionStrings");
            SqlConnection cn = new SqlConnection(strConn);
            cn.Open();
            string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + FileUpload1.FileName;  //获取Execle文件名  DateTime日期函数
            string savePath = @"E:/upfiles/" + filename;//Server.MapPath 获得虚拟服务器相对路径
            FileUpload1.SaveAs(savePath);                        //SaveAs 将上传的文件内容保存在服务器上

            //DataSet ds1 = ExeclCOL(savePath, filename);           //调用自定义方法            
            //DataRow[] dr1 = ds1.Tables[0].Select();

            DataSet ds = ExecleDs(savePath, filename);           //调用自定义方法
            try
            {
                DataRow[] dr = ds.Tables[0].Select();            //定义一个DataRow数组
                int rowsnum = ds.Tables[0].Rows.Count;
                if (rowsnum == 0)
                {
                    Response.Write("<script>alert('Excel表为空表,无数据!')</script>");   //当Excel表为空时,对用户进行提示
                }
                else
                {
                    for (int i = 0; i < dr.Length; i++)
                    {
                        
                        string ps_xh = dr[i]["序号"].ToString();//excel列名【名称不能变,否则就会出错】
                        string ps_gk = dr[i]["顾客名称"].ToString();//列名 以下类似
                        string ps_ht = dr[i]["合同号"].ToString();
                        string ps_rw = dr[i]["任务单号"].ToString();
                        string ps_th = dr[i]["图号"].ToString();
                        string ps_tm = dr[i]["图名"].ToString();
                        string ps_sbh = dr[i]["加工设备号"].ToString();
                        string ps_sbmc = dr[i]["加工设备名称"].ToString();
                        string ps_sbdj = dr[i]["加工设备单价（元）"].ToString();
                        string ps_gs = dr[i]["加工工时"].ToString();
                        string ps_my = dr[i]["加工工时费用（元）"].ToString();
                        string ps_bz = dr[i]["备注"].ToString();
                        //string ps_sj = dr[i]["时间"].ToString();
                        //string year = ps_sj.Split('.')[0];
                        //string month = ps_sj.Split('.')[1];
                       
                        if (ps_xh != "")
                        {
                            string sqlcheck = "select count(*) from TBMP_GS_DETAIL_LIST where DATEYEAR='" + year + "'AND DATEMONTH='" + month + "'AND GS_TSAID='" + ps_rw + "'AND GS_CUSNAME='" + ps_gk + "' AND GS_CONTR= '" + ps_ht + "' AND GS_TUHAO = '" + ps_th + "'AND GS_TUMING='" + ps_tm + "'AND GS_EQUID='" + ps_sbh + "'AND IsDel='0'";
                            SqlCommand sqlcmd = new SqlCommand(sqlcheck, cn);
                            int count = Convert.ToInt32(sqlcmd.ExecuteScalar().ToString());

                            if (count == 0)
                            {
                                string insertstr = "insert into TBMP_GS_DETAIL_LIST (GS_NUM,GS_CUSNAME,GS_CONTR,GS_TSAID,GS_TUHAO,GS_TUMING,GS_EQUID,GS_EQUNAME,GS_EQUFACTOR,GS_EQUHOUR,GS_EQUMONEY,GS_NOTE,DATEYEAR,DATEMONTH,IsDel)";
                                insertstr += " values('" + ps_xh + "','" + ps_gk + "','" + ps_ht + "','" + ps_rw + "','" + ps_th + "','" + ps_tm + "','" + ps_sbh + "','" + ps_sbmc + "','" + ps_sbdj + "','" + ps_gs + "','" + ps_my + "','" + ps_bz + "','" + year + "','" + month + "','0')";

                                SqlCommand cmd = new SqlCommand(insertstr, cn);
                                try
                                {
                                    cmd.ExecuteNonQuery();
                                }
                                catch (MembershipCreateUserException ex)       //捕捉异常
                                {
                                    Response.Write("<script>alert('导入内容:" + ex.Message + "')</script>");
                                }
                            }
                            //else if (count != 0)
                            //{
                            //    Response.Write("<script>alert('此条工时记录已导入，不需再次导入！！！')</script>");      
                            //}
                        }
                        //else { continue; }
                    }

                    System.IO.FileInfo path = new System.IO.FileInfo(savePath);
                    path.Delete();
                    
                   
                }
            }
            catch (Exception)
            {
                Response.Write("<script>alert('Excle表格式有问题，请仔细检查！')</script> ");
            }
            finally
            {
                cn.Close();
            }
            string sqlcol = "select GS_CUSNAME,GS_CONTR,GS_TSAID,sum(GS_EQUMONEY)jysl from dbo.TBMP_GS_DETAIL_LIST " + "where DATEYEAR='" + year + "' and DATEMONTH='" + month + "'group by GS_CUSNAME,GS_CONTR,GS_TSAID order by GS_CONTR";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlcol);
            string strConn1 = DBCallCommon.GetStringValue("connectionStrings");
            SqlConnection cn1 = new SqlConnection(strConn);
            cn1.Open();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr_code in dt.Rows)
                {
                    string dt_cn = dr_code["GS_CUSNAME"].ToString();
                    string dt_co = dr_code["GS_CONTR"].ToString();
                    string dt_td = dr_code["GS_TSAID"].ToString();
                    string dt_tm = dr_code["jysl"].ToString();

                    string sqlcheck = "select count(*) from TBMP_GS_COL_LIST where DATEYEAR='" + year + "'AND DATEMONTH='" + month + "'AND GS_TSAID='" + dt_td + "'AND GS_CUSNAME='" + dt_cn + "' AND GS_CONTR= '" + dt_co + "'AND IsDel='0'";
                    SqlCommand sqlcmd = new SqlCommand(sqlcheck, cn1);
                    int count = Convert.ToInt32(sqlcmd.ExecuteScalar().ToString());

                    if (count == 0)
                    {
                        string insertstr = "insert into TBMP_GS_COL_LIST (GS_CUSNAME,GS_CONTR,GS_TSAID,GS_TSAMONEY,DATEYEAR,DATEMONTH,IsDel)";
                        insertstr += " values('" + dt_cn + "','" + dt_co + "','" + dt_td + "','" + dt_tm + "','" + year + "','" + month + "','0')";

                        SqlCommand cmd = new SqlCommand(insertstr, cn1);
                        try
                        {
                            cmd.ExecuteNonQuery();
                        }
                        catch (MembershipCreateUserException ex)       //捕捉异常
                        {
                            Response.Write("<script>alert('导入内容:" + ex.Message + "')</script>");
                        }
                    }

                }
            }
            cn1.Close();
            Response.Write("<script>alert('Excle表导入成功!');window.open('PM_GongShi_List.aspx');</script>");
        }
    }
}
