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

namespace ZCZJ_DPF.PC_Data
{
    public partial class PC_TBPC_PLAN_PLACE : System.Web.UI.Page
    {
        public string gloabptc
        {
            get
            {
                object str = ViewState["gloabptc"];
                return str == null ? null : (string)str;
            }
            set
            {
                ViewState["gloabptc"] = value;
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                gloabptc = Server.UrlDecode(Request.QueryString["ptc"].ToString());//计划跟踪号 
                Marreplace_detail_repeaterbind();
            }
        }

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
                    "WHERE ptcode='" + gloabptc + "'";

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
