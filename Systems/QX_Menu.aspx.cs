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

namespace ZCZJ_DPF.Systems
{
    public partial class QX_Menu :BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            InitUrl();
        }

        private void InitUrl()
        {
            HyperLink1.NavigateUrl = "QX_Power_List.aspx";
            HyperLink2.NavigateUrl = "QX_Role_List.aspx";
           // HyperLink3.NavigateUrl = "TbAjax.aspx";
            //HyperLink3.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink4.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink5.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink6.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink7.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink8.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink9.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink10.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink11.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink12.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink13.NavigateUrl = "BD_Accessory_List.aspx";
            //HyperLink14.NavigateUrl = "BD_Accessory_List.aspx";
           // CheckUser(ControlFinder);
        }
    
    }
}
