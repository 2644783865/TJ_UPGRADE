using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;




namespace ZCZJ_DPF
{
  public class ListPagerItem
  {
    String _pageName = String.Empty;
    String _cssClass = String.Empty;
    int _pageNumber = 0;

    public ListPagerItem(String name, String cssClass, int number)
    {
      _pageName = name;
      _cssClass = cssClass;
      _pageNumber = number;
    }

    public String PageName
    {
      get { return _pageName; }
    }

    public String CssClass
    {
      get { return _cssClass; }
    }

    public int PageNumber
    {
      get { return _pageNumber;  }
    }
  }

  public class ListPagerCss
  {
    public const String Page = "页数";
    public const String FirstPage = "首页";
    public const String PreviousPage = "上一页";
    public const String NextPage = "下一页";
    public const String LastPage = "末页";
  }

  public partial class ListPager : System.Web.UI.UserControl
  {
		private bool _NeedsBind = true;		
		private QueryStringArgument<int> _PageNumber = new QueryStringArgument<int>(SiteConfig.Page_QS,-1);

    public delegate void PageHandler(int pageNumber);
    public event PageHandler PageChanged;

    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      PageRepeater.ItemCommand += new RepeaterCommandEventHandler(PageRepeater_ItemCommand);

			if (!this.IsPostBack)
			{
				// if passed by query string, then use it
				if (_PageNumber.IsValid())
				{
					CurrentPage = _PageNumber.Value;
				}

				_NeedsBind = true;
			}
    }

    #region Properties

    public int CurrentPage
    {
      get
      {
        if (ViewState["CurrentPage"] != null)
          return (int)ViewState["CurrentPage"];
        return 0;
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

		public int StartIndex
		{
			get { return CurrentPage * PageSize; }
		}

		public int StartRecordNumber
		{
			get { return StartIndex + 1; }
		}

		public int StopRecordNumber
		{
			get
			{
				if (TotalItems > PageSize)
				{
					int difference = ((TotalItems - StartIndex) > PageSize) ? PageSize : (TotalItems - StartIndex);
					return (StartRecordNumber + difference) - 1;
				}
				else
				{
					return TotalItems;
				}
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

					#region // Alternate version using double math and rounding
					//return (int)Math.Round(((double)TotalItems / (double)PageSize) + 0.5, MidpointRounding.AwayFromZero);
					#endregion
				}
				return 0;
			}
		}

    public bool ShowFirstAndLast
    {
      get
      {
        if (ViewState["ShowFirstAndLast"] != null)
          return (bool)ViewState["ShowFirstAndLast"];
        return false;
      }
      set
      { 
        ViewState["ShowFirstAndLast"] = value;
      }
    }

    public int MaxDisplayPages
    {
      get
      {
        if (ViewState["MaxDisplayPages"] != null)
          return (int)ViewState["MaxDisplayPages"];
        return 9;
      }
      set
      { 
        ViewState["MaxDisplayPages"] = value;
      }
    }

    #endregion		

    void BindData()
    {
      // ensure safe values
			if (CurrentPage > LastPage)
				CurrentPage = LastPage;

			// only visible if there is more than one page
			this.Visible = (TotalItems > PageSize);
			if (this.Visible)
			{
				// setup the page list - add first and previous links
				PartialList<ListPagerItem> pages = new PartialList<ListPagerItem>();
				if (CurrentPage > 0)
				{
					if (ShowFirstAndLast)
						pages.Add(new ListPagerItem("第一页", ListPagerCss.FirstPage, 0));
					pages.Add(new ListPagerItem("上一页", ListPagerCss.PreviousPage, CurrentPage -1));
				}

				// determine start of page links
        int pageRenderIndex = CurrentPage - (MaxDisplayPages / 2);
        if (pageRenderIndex < 0)
          pageRenderIndex = 0;
        if (LastPage > MaxDisplayPages && (pageRenderIndex + MaxDisplayPages) > LastPage)
        {
          pageRenderIndex = LastPage - MaxDisplayPages;
				}

				#region // Old "first page selected" approach
				/*int pageRenderIndex = 0;
        while ((pageRenderIndex + MaxDisplayPages) <= CurrentPage)
				{
          pageIndex += MaxDisplayPages;
				}*/
				#endregion

				// loop to create page links
				for (int i = 0; i < MaxDisplayPages; i++)
				{
          pages.Add(new ListPagerItem((pageRenderIndex + 1).ToString(), ListPagerCss.Page, pageRenderIndex));
          if (++pageRenderIndex >= LastPage)
						break;
				}

				// add next and last links
				if (CurrentPage+1 < LastPage)
				{
					pages.Add(new ListPagerItem("下一页", ListPagerCss.NextPage, CurrentPage +1));
					if (ShowFirstAndLast)
						pages.Add(new ListPagerItem("最后一页", ListPagerCss.LastPage, LastPage));
				}
			
				// output the list of page links
				PageRepeater.DataSource = pages;
				PageRepeater.DataBind();
			}

			_NeedsBind = false;
    }

		protected override void OnPreRender(EventArgs e)
		{
			if (_NeedsBind)
			{
				BindData();
			}

			base.OnPreRender(e);
		}

    protected void OnPageChanged(int pageNumber)
    {
      CurrentPage = pageNumber;
			_NeedsBind = true;

      // fire the event in the parent control
      if (PageChanged != null)
      {
        PageChanged(CurrentPage);
      }
    }

    void PageRepeater_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
      OnPageChanged(Convert.ToInt32(e.CommandArgument));
    }

  }
}