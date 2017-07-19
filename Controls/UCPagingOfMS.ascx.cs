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

namespace ZCZJ_DPF
{ 
   public class UCPagingOfMSItem
   {
        //String _pageName = String.Empty;
        //String _cssClass = String.Empty;
        int _pageNumber = 0;

        //public UCPagingItem(String name, String cssClass, int number)
        //{
        //  //_pageName = name;
        //  //_cssClass = cssClass;
        //  _pageNumber = number;
        //}


        public int PageNumber
        {
          get { return _pageNumber;  }
        }
   }

   public partial class UCPagingOfMS : System.Web.UI.UserControl
   {
	    //private bool _Visible = true;		
	    private QueryStringArgument<int> _PageNumber = new QueryStringArgument<int>(SiteConfig.Page_QS,-1);

        public delegate void PageHandlerOfMS(int pageNumber);
        public event PageHandlerOfMS PageMSChanged;

        public void InitPageInfo()
        {
            lblPageCount.Text = LastPage.ToString();
            lblPageIndex.Text = (CurrentPage).ToString();
            txtNewPageIndex.Text = (CurrentPage).ToString();
            btnFirst.Enabled = (CurrentPage == 1 ? false : true);
            btnPrev.Enabled = (CurrentPage == 1 ? false : true);
            btnLast.Enabled = (CurrentPage == LastPage ? false : true);
            btnNext.Enabled = (CurrentPage == LastPage ? false : true);  
        }

        protected override void OnLoad(EventArgs e)
        {
          base.OnLoad(e);
       	  if (!this.IsPostBack)
	        {
		        // if passed by query string, then use it
		        if (_PageNumber.IsValid())
		        {
			        CurrentPage = _PageNumber.Value;
		        }
                this.InitPageInfo();
		        //_Visible = true;
	        }
          //InitPageInfo();
        }

        #region Properties

        public int CurrentPage
        {
          get
          {
            if (ViewState["CurrentPage"] != null)
              return (int)ViewState["CurrentPage"];
            return 1;
          }
          set
          {      
            ViewState["CurrentPage"] = value;
          }
        }

        public int PageSize
        {
	        get
	        {
		        if (ViewState["PageSize"] != null)
			        return (int)ViewState["PageSize"];
		        return SiteConfig.DefaultPageSize;
	        }
	        set
	        {
		        ViewState["PageSize"] = value;
	        }
        }

        public int TotalItems
        {
          get
          {
	        if (ViewState["TotalItems"] != null)
		        return (int)ViewState["TotalItems"];
            return 0;
          }
          set
          {	
	        ViewState["TotalItems"] = value;
          }
        }

        public int LastPage
        {
	        get
	        {
		        if (TotalItems > 0)
		        {
                  int i = TotalItems / PageSize;
                  if ((TotalItems % PageSize) > 0)
                    i++;
                  return i;
		        }
		        return 0;
	        }
        }

        #endregion	

        protected void OnPageChanged(int pageNumber)
        {
            CurrentPage = pageNumber;
            //_Visible = true;

            // fire the event in the parent control
            if (PageMSChanged != null)
            {
                PageMSChanged(CurrentPage);
                this.InitPageInfo();
            }
        }

        protected void btnFirst_Click(object sender, EventArgs e)
        {
            CurrentPage = 1;
            this.OnPageChanged(Convert.ToInt32(CurrentPage));
        }

        protected void btnPrev_Click(object sender, EventArgs e)
        {
            if (CurrentPage != 1)
            {
                CurrentPage--;
                this.OnPageChanged(Convert.ToInt32(CurrentPage));
            }
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            if (CurrentPage < LastPage)
            {
                CurrentPage++;
                this.OnPageChanged(Convert.ToInt32(CurrentPage));
            }
        }

        protected void btnLast_Click(object sender, EventArgs e)
        {
            CurrentPage = LastPage;
            this.OnPageChanged(Convert.ToInt32(CurrentPage));
        }

        //添加对btnGO单击事件 
        protected void GoIndex(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtNewPageIndex.Text.Trim()) <= LastPage)
            {
                CurrentPage = Convert.ToInt32(txtNewPageIndex.Text.Trim());
                this.OnPageChanged(Convert.ToInt32(CurrentPage));
            }
        }
    }
}