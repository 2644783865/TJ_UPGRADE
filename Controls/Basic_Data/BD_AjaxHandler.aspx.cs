using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Reflection;
using System.Data;
using EasyUI;


namespace ZCZJ_DPF.Basic_Data
{
    public partial class BD_AjaxHandler : System.Web.UI.Page
    {
        string sqlText;
        string result;
        protected void Page_Load(object sender, EventArgs e)
        {
            String methodName = Request["method"];
            Type type = this.GetType();
            MethodInfo method = type.GetMethod(methodName);
            if (method == null) throw new Exception("method is null");

            try
            {

                method.Invoke(this, null);
            }
            catch
            {
                throw;
            }
        }

        public void GetOfficeTH()
        {
            int Id = 0;
            if (Request.Form["id"] != null)
            {
                Id = int.Parse(Request.Form["id"].ToString());
            }
            sqlText = "select a.*,b.name as pName,case when a.IsBottom='on' then 'open' else 'closed' end as state from TBMA_OFFICETH as a left join TBMA_OFFICETH as b on a.pId=b.Id  where a.IsDel=0 and a.pId=" + Id;
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            Response.Write(JsonHelper.CreateJsonOne(dt, false));

        }


        public void GetAllPth()
        {
            sqlText = "select Id,name from TBMA_OFFICETH where IsBottom<>'on'";
            DataTable dt = DBCallCommon.GetDTUsingSqlText(sqlText);
            result = JsonHelper.CreateJsonOne(dt, false);
            Response.Write(result);
        }
        //增加和编辑
        public void SaveOfficeTH()
        {
            string id = Request.Form["id"];
            string action = Request.Form["action"];
            string maid = Request.Form["maid"];
            string pId = Request.Form["pid"];
            string name = Request.Form["name"];
            string canshu = Request.Form["canshu"];
            string price = Request.Form["price"];
            string unit = Request.Form["unit"];
            string isbottom = Request.Form["isbottom"];
            string kc = Request.Form["kc"];
            if (action == "add")
            {
                sqlText = "insert into TBMA_OFFICETH(maId, name, canshu, price, addTime, addMan, addManNm, unit, pId, IsBottom,kc) values ('" + maid + "','" + name + "','" + canshu + "','" + price + "','" + DateTime.Now.ToString() + "','" + Session["UserId"] + "','" + Session["UserName"] + "','" + unit + "','" + pId + "','" + isbottom + "','"+kc+"') ";
            }
            else if (action == "edit")
            {
                sqlText = "update TBMA_OFFICETH set maId='" + maid + "',name='" + name + "',canshu='" + canshu + "',price='" + price + "',addTime='" + DateTime.Now.ToString() + "',addMan='" + Session["UserId"] + "',addManNm='" + Session["UserName"] + "',unit='" + unit + "',pId='" + pId + "',kc='"+kc+"',IsBottom='" + isbottom + "' where Id=" + id;
            }
            try
            {
                DBCallCommon.ExeSqlText(sqlText);
                result = "保存成功";
            }
            catch (Exception)
            {

                result = "保存失败";
            }
            Response.Write(result);
        }
        //删除
        public void RemoveOfficeTH()
        {
            string Id = Request.Form["Id"].ToString();
            sqlText = "update TBMA_OFFICETH set IsDel=1 where Id=" + Id + " or pId=" + Id;
            try
            {
                DBCallCommon.ExeSqlText(sqlText);
                result = "删除成功";

            }
            catch (Exception)
            {

                result = "删除失败，请联系管理员";
            }
            Response.Write(result);
        }
        //添加用户名和密码
        public void Adduser()
        {
            string sql="";
            List<string> list = new List<string>();
            string supplyid = Request.Form["supplyid"].ToString().Trim();
            string username = Request.Form["username"].ToString().Trim();
            string userpassword = Request.Form["userpassword"].ToString().Trim();
            sql = "select count(1) from PC_USERINFO where supplyid='" + supplyid + "' or supplyusername='" + username + "'";
            DataTable dtuser = DBCallCommon.GetDTUsingSqlText(sql);
            if (!dtuser.Rows[0][0].ToString().Contains('0'))
            {
                Response.Write("false");
            }
            else
            {
                sql = "insert into PC_USERINFO(supplyid,supplyusername,supplypassword) values ('" + supplyid + "','" + username + "','" + userpassword + "')";
                list.Add(sql);
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                    Response.Write("true");
                }
                catch (Exception)
                {

                    Response.Write("false");
                }
            }
        }


        //修改用户名和密码
        public void edituser()
        {
            string sql = "";
            List<string> list = new List<string>();
            string supplyid = Request.Form["supplyid"].ToString().Trim();
            string username = Request.Form["username"].ToString().Trim();
            string userpassword = Request.Form["userpassword"].ToString().Trim();
            sql = "select count(1) from PC_USERINFO where supplyid!='" + supplyid + "' and supplyusername='" + username + "'";
            DataTable dtuser = DBCallCommon.GetDTUsingSqlText(sql);
            if (!dtuser.Rows[0][0].ToString().Contains('0'))
            {
                Response.Write("false");
            }
            else
            {
                sql = "update PC_USERINFO set supplyusername='" + username + "',supplypassword='" + userpassword + "' where supplyid='" + supplyid + "'";
                list.Add(sql);
                try
                {
                    DBCallCommon.ExecuteTrans(list);
                    Response.Write("true");
                }
                catch (Exception)
                {

                    Response.Write("false");
                }
            }
        }
    }
}
