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

namespace ZCZJ_DPF.SM_Data
{
    public partial class SM_PURCHASEPLAN_REPLACE : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                hfdptc.Value =Server.UrlDecode(Request.QueryString["ptc"].ToString());//计划跟踪号 
                Marreplace_detail_repeaterbind();
                ((System.Web.UI.WebControls.Panel)this.Master.FindControl("PanelHome")).Visible = false;
            }
        }

        //Repeater绑定视图View_TBPC_MARREPLACE_total_all_detail，材料代用详细信息表
        private void Marreplace_detail_repeaterbind()
        {
            string sqltext = "";

            sqltext = "SELECT marid, num, ptcode, usenum, allstate, allshstate, alloption, allnote, marnm, " +
                    "marguige, marguobiao, marcaizhi, marcgunit, mpcode, plancode, zdreson, zdrid, " +
                    "zdtime, shraid, shratime, shrayj, shrbid, shrbtime, shrbyj, totalstate, totalnote, zdrnm, " +
                    "shranm, shrbnm, engid, pjnm, engnm, detailmarnm, detailmarguige, " +
                    "detailmarguobiao, detailmarcaizhi, detailmarunit, detailmpcode, detailmarid, " +
                    "detailmarnuma, detailmarnumb, detailnote, detailoldsqcode, detailnewsqcode, " +
                    "detailstate, fzunit, length, width, detaillength, detailwidth, detailfzunit, fznum, " +
                    "usefznum, pjid, zdwctime " +
                    "FROM View_TBPC_MARREPLACE_total_all_detail " +
                    "WHERE ptcode like '" + hfdptc.Value + "%'";

            DBCallCommon.BindRepeater(Marreplace_detail_repeater, sqltext);
            if (Marreplace_detail_repeater.Items.Count == 0)
            {
                NoDataPane.Visible = true;
            }
            else
            {
                NoDataPane.Visible = false;
            }
        }
    }
}
