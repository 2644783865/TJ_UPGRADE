using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.OM_Data
{
    public partial class OM_Bgyp_Store : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUser(ControlFinder);
        }
    }
}
