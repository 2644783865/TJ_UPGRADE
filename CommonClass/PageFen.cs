using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace ZCZJ_DPF.CommonClass
{
    public class PageFen
    {
        /// <summary> 
        /// 
        /// </summary> 
        /// <typeparam name="T"></typeparam> 
        /// <param name="replist">控件ID</param> 
        /// <param name="DataSource">数据源</param> 
        /// <param name="IndexPage">当前页</param> 
        /// <param name="PageSize">每页数据条数</param> 
        /// <param name="PageParemart">页面搜索参数 like &a=a&b=b </param> 
        /// <returns></returns> 
        public static string ShowPage<T>(System.Web.UI.WebControls.GridView replist, IQueryable<T> DataSource, int IndexPage, int PageSize, string PageParemart)
        {
            string rtnStr = "";
            int sourceCount = DataSource.Count();
            if (sourceCount == 0)//数据源无数据 
            {
                rtnStr = string.Empty;
            }
            else
            {
                int yutemp = sourceCount % PageSize;
                int pagecounts = (yutemp == 0) ? (sourceCount / PageSize) : (sourceCount / PageSize + 1);//总页数 
                rtnStr = "第" + IndexPage + "/" + pagecounts + "页&nbsp;&nbsp;  ";
                if (pagecounts == 1) //总共一页数据 
                {
                    replist.DataSource = DataSource;
                    rtnStr += " <div style='color #CCCCCC;'> 首页&nbsp;&nbsp; 上一页&nbsp;&nbsp; 下一页&nbsp;&nbsp; 尾页 &nbsp;&nbsp; </div> ";
                }
                else
                {
                    ////rtnStr += "<div style=' float:right;'>";
                    if (IndexPage == 1)//首页 
                    {
                        replist.DataSource = DataSource.Take(PageSize);
                        rtnStr += "首页&nbsp;&nbsp;上一页 <a href='?page=" + (IndexPage + 1) + PageParemart + "'>下一页&nbsp;&nbsp;</a> <a href='?page=" + (pagecounts) + PageParemart + "'>尾页&nbsp;&nbsp;</a> ";
                    }
                    else
                    {
                        replist.DataSource = DataSource.Skip((IndexPage - 1) * PageSize).Take(PageSize);
                        if (IndexPage == pagecounts)//末页 
                        {
                            rtnStr += "<a href='?page=1" + PageParemart + "'>首页&nbsp;&nbsp;</a> <a href='?page=" + (IndexPage - 1) + PageParemart + "'>上一页&nbsp;&nbsp;</a>下一页&nbsp;&nbsp;尾页 ";
                        }
                        else
                        {
                            rtnStr += "<a href='?page=1" + PageParemart + "'>首页&nbsp;&nbsp;</a> <a href='?page=" + (IndexPage - 1) + PageParemart + "'>上一页&nbsp;&nbsp;</a> <a href='?page=" + (IndexPage + 1) + PageParemart + "'>下一页&nbsp;&nbsp;</a> <a href='?page=" + (pagecounts) + PageParemart + "'>尾页&nbsp;&nbsp;</a> ";
                        }
                    }
                    rtnStr +="&nbsp;&nbsp;"+"转到"+ "</div></div>";
                }
                replist.DataBind();
            }
            return rtnStr;
        } 




    }
}
