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
    public partial class PC_TBPC_Purchange_detaila : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void tbpc_purbgdetailRepeater_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                //e.Item.Attributes.Add("onDblClick", "document.all." + btn_dbclick.ClientID + ".click()");
                //e.Item.Attributes.Add("onDblClick", "databinddbl(this)");//单击，调用JS函数

            }
        }
    }
}
