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

namespace ZCZJ_DPF.OM_Data
{
    public partial class Sta_RecordDetail : System.Web.UI.Page
    {
        string actionstr = string.Empty;
        string date = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            actionstr = Request.QueryString["action"].ToString();
            date = Request.QueryString["ST_DATATIME"].ToString();

            if (!IsPostBack)
            {
                DropBind();
                //Salary.Visible = false;

                AddRole();

                Showdata();


            }
        }

        private void DropBind() //绑定下拉框信息
        {
            //  List<DropDownList> ddl = new List<DropDownList>() { ST_GENDER, DEP_NAME, ST_SEQUEN };
            //  DropListBind(ddl);

            string sql = "select DEP_NAME,DEP_CODE from TBDS_DEPINFO where DEP_CODE like '[0-9][0-9]'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            DEP_NAME.DataSource = dt;
            DEP_NAME.DataTextField = "DEP_NAME";
            DEP_NAME.DataValueField = "DEP_CODE";
            DEP_NAME.DataBind();
            AddNew(DEP_NAME);


            //性别
            List<string> list = new List<string>() { "男", "女" };
            ST_GENDER.DataSource = list;
            ST_GENDER.DataBind();
            AddNew(ST_GENDER);
            //岗位序列
            list = new List<string>() { "管理", "技术", "操作" };
            ST_SEQUEN.DataSource = list;
            ST_SEQUEN.DataBind();
            AddNew(ST_SEQUEN);



            //绑定名族
            list = new List<string>() { "汉族", "蒙古族", "回族", "藏族", "维吾尔族", "苗族", "彝族", "壮族", "布依族", "朝鲜族", "满族", "侗族", "瑶族", "白族", "土家族", "哈尼族", "哈萨克族", "傣族", "黎族", "僳僳族", "佤族", "畲族", "高山族", "拉祜族", "水族", "东乡族", "纳西族", "景颇族", "柯尔克孜族", "土族", "达斡尔族", "仫佬族", "羌族", "布朗族", "撒拉族", "毛南族", "仡佬族", "锡伯族", "阿昌族", "普米族", "塔吉克族", "怒族", "乌孜别克族", "俄罗斯族", "鄂温克族", "德昂族", "保安族", "裕固族", "京族", "塔塔尔族", "独龙族", "鄂伦春族", "赫哲族", "门巴族", "珞巴族", "基诺族" };
            ST_PEOPLE.DataSource = list;
            ST_PEOPLE.DataBind();
            AddNew(ST_PEOPLE);
            //绑定政治面貌
            list = new List<string>() { "群众", "中共党员", "共青团员", "民主党派", "无党派", "农工民主党", "致公党", "九三学社", "预备党员" };
            ST_POLITICAL.DataSource = list;
            ST_POLITICAL.DataBind();
            AddNew(ST_POLITICAL);
            //婚姻状况
            list = new List<string>() { "未婚", "再婚", "丧偶", "离异", "已婚" };
            ST_MARRY.DataSource = list;
            ST_MARRY.DataBind();
            AddNew(ST_MARRY);
            //第一学历  最高学历
            list = new List<string>() { "本科", "大普", "大专", "中专", "高中", "初中", "小学", "研究生", "在职研究生" };
            ST_XUELI.DataSource = list;
            ST_XUELI.DataBind();
            AddNew(ST_XUELI);
            ST_XUELIHI.DataSource = list;
            ST_XUELIHI.DataBind();
            AddNew(ST_XUELIHI);
            //第一学位  最高学位
            list = new List<string>() { "博士后", "博士", "硕士", "双学士", "学士", "无" };
            ST_XUEWEI.DataSource = list;
            ST_XUEWEI.DataBind();
            AddNew(ST_XUEWEI);
            ST_XUEWEIHI.DataSource = list;
            ST_XUEWEIHI.DataBind();
            AddNew(ST_XUEWEIHI);
            //技术职务（称）
            list = new List<string>() { "技术员", "助理工程师", "工程师", "高级工程师", "教授级高级工程师", "助理经济师", "经济师", "高级经济师", "助理政工师", "政工师", "高级政工师", "会计员", "助理会计师", "会计师", "高级会计师", "助理国际商务师", "国际商务师", "高级国务商务师", "助理馆员", "馆员", "副研究馆员", "研究馆员", "护师", "主管护师", "主任护师", "助理统计师", "统计师" };
            ST_ZHICH.DataSource = list;
            ST_ZHICH.DataBind();
            AddNew(ST_ZHICH);
            //职能等级
            list = new List<string>() { "高级技师", "技师", "普通工", "初级工", "中级工", "高级工" };
            ST_ZHICHXU.DataSource = list;
            ST_ZHICHXU.DataBind();
            AddNew(ST_ZHICHXU);

            //学历类型
            list = new List<string>() { "普通统招", "成教", "自考", "专升本", "电大", "网络教育" };
            ST_XUELITYPE.DataSource = list;
            ST_XUELITYPE.DataBind();
            AddNew(ST_XUELITYPE);
            //最高学历类型
            list = new List<string>() { "普通统招", "成教", "自考", "专升本", "电大", "网络教育" };
            ST_XUELITYPEHI.DataSource = list;
            ST_XUELITYPEHI.DataBind();
            AddNew(ST_XUELITYPEHI);
        }


        private void AddNew(DropDownList ddl) //下拉框添加-请选择-
        {
            ListItem item = new ListItem();
            item.Text = "-请选择-";
            item.Value = "00";
            ddl.Items.Insert(0, item);
            ddl.SelectedValue = "00";
        }

        private void AddRole()
        {
            string sql = "select * from ROLE_INFO";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sql);
            chk_Role.DataSource = dt;
            chk_Role.DataTextField = "R_NAME";
            chk_Role.DataBind();
        }


        private void Showdata() //将数据绑定到textbox
        {
            string st_id = Request.QueryString["ST_ID"].ToString();//得到修改人员编码
            string data = Request.QueryString["ST_DATATIME"].ToString();//得到修改人员编码
            string sqlText = "select * from View_TBDS_STAFFINFO_record where ST_ID='" + st_id + "' and ST_DATATIME='" + data + "'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            DataRow dr = dt.Rows[0];
            foreach (Control control in Panel1.Controls)
            {
                if (control is TextBox)
                {

                    ((TextBox)control).Text = dr[((TextBox)control).ID.ToString()].ToString();
                }
                else if (control is DropDownList)
                {
                    DEP_NAME.ID = "ST_DEPID";
                    ((DropDownList)control).SelectedValue = dr[((DropDownList)control).ID.ToString()].ToString();
                    DEP_NAME.ID = "DEP_NAME";
                }
            }
            string role = dr["R_NAME"].ToString();
            if (!string.IsNullOrEmpty(role))
            {
                string[] roles = role.Split(',');
                string uRole = "";
                for (int i = 0; i < roles.Length; i++)
                {
                    uRole = roles[i].Substring(1, roles[i].Length - 2);
                    for (int j = 0; j < chk_Role.Items.Count; j++)
                    {
                        if (uRole == chk_Role.Items[j].Text)
                        {
                            chk_Role.Items[j].Selected = true;
                        }
                    }
                }
            }


            Ddl_Post();
            DEP_POSITION.SelectedValue = dr["ST_POSITION"].ToString();
            showImage.ImageUrl = "~/staff_images/" + dr["JPGURL"].ToString();
            sqlText = "select * from TBDS_WORKHIS where ST_ID='" + st_id + "'";
            Det_Repeater.DataSource = DBCallCommon.GetDTUsingSqlText(sqlText);
            Det_Repeater.DataBind();
            sqlText = "select * from TBDS_EDUCA where ST_ID='" + st_id + "'";
            Det_Repeater1.DataSource = DBCallCommon.GetDTUsingSqlText(sqlText);
            Det_Repeater1.DataBind();
            sqlText = "select * from TBDS_RELATION where ST_ID='" + st_id + "'";
            Det_Repeater2.DataSource = DBCallCommon.GetDTUsingSqlText(sqlText);
            Det_Repeater2.DataBind();
            InitVar();
            InitVar1();
            InitVar2();
        }

        protected void Ddl_Post() //将职位信息绑定到职位下拉框
        {
            string sqlText = string.Format("select DEP_CODE,DEP_NAME from TBDS_DEPINFO where DEP_CODE like '{0}[0-9][0-9]'", DEP_NAME.SelectedValue);//按照表里面的顺序排。不然直接distinct会按拼音排序。
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            DEP_POSITION.DataSource = dt;
            DEP_POSITION.DataTextField = "DEP_NAME";
            DEP_POSITION.DataValueField = "DEP_CODE";
            DEP_POSITION.DataBind();
            AddNew(DEP_POSITION);
        }

        private void InitVar()
        {
            if (Det_Repeater.Items.Count == 0)
            {
                NoDataPanel.Visible = true;
            }
            else
            {
                NoDataPanel.Visible = false;
              
            }
        }
        private void InitVar1()
        {
            if (Det_Repeater1.Items.Count == 0)
            {
                NoDataPanel1.Visible = true;
            }
            else
            {
                NoDataPanel1.Visible = false;
               
            }
        }
        private void InitVar2()
        {
            if (Det_Repeater2.Items.Count == 0)
            {
                NoDataPanel2.Visible = true;
            }
            else
            {
                NoDataPanel2.Visible = false;
               
            }
        }

    }
}
