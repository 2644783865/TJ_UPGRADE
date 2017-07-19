using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/// <summary>
/// 
/// </summary>
public class RepeaterExportUtil
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="gv"></param>
    public static void Export(string fileName, Repeater rt)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Charset = "UTF-8";
        HttpContext.Current.Response.AddHeader(
            "content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter htw = new HtmlTextWriter(sw))
            {
                rt.Page.EnableViewState = false;
                rt.RenderControl(htw); //将服务器控件的内容输出到所提供的 System.Web.UI.HtmlTextWriter 对象中；如果已启用跟踪功能，则存储有关控件的跟踪信息。
                HttpContext.Current.Response.Write(sw.ToString());
                HttpContext.Current.Response.End();
            }
        }
    }
}
