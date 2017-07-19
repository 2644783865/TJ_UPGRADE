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

namespace ZCZJ_DPF.Controls
{
    public partial class UCTxtDate : System.Web.UI.UserControl
    {
        private string _Text;//日期值
        public string Text
        {
            get { return _Text; }
            set { _Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                InitVar();
            }
            _Text = txtDate.Text.Trim();
        }

        private void InitVar()
        {
            
            //控制日期控件的显示与消失
            txtDate.Attributes.Add("OnFocus", "javascript:ShowCalendar()");
        }

        protected void calDate_VisibleMonthChanged(object sender, MonthChangedEventArgs e)
        {
            ScriptManager.RegisterStartupScript(upDate, upDate.GetType(), "key", "document.getElementById('layerin').style.display=\"block\";", true);
        }

        protected void calDate_SelectionChanged(object sender, EventArgs e)
        {
            txtDate.Text = calDate.SelectedDate.ToShortDateString();
            calDate.Attributes.Add("OnBlur", "javascript:document.getElementById('calDate').style.display=\"none\";");
        }
    }
}