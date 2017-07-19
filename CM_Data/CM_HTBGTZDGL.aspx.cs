using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ZCZJ_DPF.CM_Data
{
    public partial class CM_HTBGTZDGL : BasicPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            CheckUser(ControlFinder);
        }
    }
}
