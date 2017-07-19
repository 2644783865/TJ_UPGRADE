using System;
using System.Data;
using System.Configuration;
using System.IO;
using System.Reflection;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF
{
	public class SiteConfig
	{
		public const int DefaultPageSize = 10;
		public const int MaxWidgetBoxGadgets = 5;

		public const string ActionAccept_QS = "accept";
		public const string ActionReject_QS = "reject";
		public const string ActionFriends_QS = "friends";
		public const string ActionFamily_QS = "family";
		public const string BoardID_QS = "b";
		public const string BoardPostID_QS = "bp";
		public const string BlogID_QS = "bl";
		public const string BlogPostID_QS = "blp";
		public const string BlogCommentID_QS = "blc";
		public const string LinkPostID_QS = "lp";
		public const string LinkPostCommentID_QS = "lpc";
		public const string CategoryID_QS = "c";
		public const string ConfirmCode_QS = "c";
		public const string ContentID_QS = "c";
		public const string ContentReportID_QS = "cr";
		public const string EmailAddress_QS = "e";
		public const string Filter_QS = "f";
		public const string ForumID_QS = "fm";
		public const string Function_QS = "fn";
		public const string GalleryID_QS = "gl";
		public const string GroupID_QS = "g";
		public const string ImageID_QS = "i";
		public const string FakeImageID_QS = "_i";
		public const string ImageCommentID_QS = "ic";
		public const string Location_QS = "lc";
		public const string MarketItemID_QS = "mi";
		public const string MarketPurchaseID_QS = "mp";
		public const string MarketFeedbackID_QS = "mf";
		public const string MyUserID_QS = "me"; // generally used for popup menus
		public const string OrganizationID_QS = "o";
		public const string OrganizationMasterID_QS = "om";
		public const string OrganizationReviewID_QS = "or";
		public const string Page_QS = "page";
		public const string Query_QS = "q";
		public const string RegionID_QS = "rn";
		public const string RequestID_QS = "r";
		public const string Secure_QS = "s";
		public const string SubCategoryID_QS = "sc";
		public const string Tag_QS = "tag";
		public const string ThreadID_QS = "t";
		public const string UndoID_QS = "uo";
		public const string UserID_QS = "u";
		public const string SecondUserID_QS = "u1";
		public const string AlertType_QS = "at";
		public const string OfferID_QS = "of";
		public const string OfferAcceptanceID_QS = "oa";
		public const string CoinEscrowID_QS = "ce";

		private static String _PageTitleStart = String.Empty;
		private static String _Version = String.Empty;

		public static String VersionString
		{
			get
			{
				if (String.IsNullOrEmpty(SiteConfig._Version))
				{
					Version ver = Assembly.GetExecutingAssembly().GetName(false).Version;
					FileInfo aInf = new FileInfo(Assembly.GetExecutingAssembly().Location);
					SiteConfig._Version = "version: " + ver.ToString();
					SiteConfig._Version += ", built: " + aInf.CreationTime.ToString("MM/dd/yyyy HH:mm");
				}
				return SiteConfig._Version;
			}
		}

		public static String GetPageTitle(String val)
		{
			if ((!Production) && (String.IsNullOrEmpty(SiteConfig._PageTitleStart)))
			{
				SiteConfig._PageTitleStart = " ";
				foreach (object attr in Assembly.GetExecutingAssembly().GetCustomAttributes(true))
				{
					if (attr is AssemblyConfigurationAttribute)
					{
						switch (((AssemblyConfigurationAttribute)attr).Configuration.ToLower())
						{
							case "dev":
								{
									SiteConfig._PageTitleStart = "DEV";
									break;
								}
							case "stage":
								{
									SiteConfig._PageTitleStart = "STAGE";
									break;
								}
							default:
								break;
						}
						break;
					}
				}
			}
			return String.Format("{0} {1}", SiteConfig._PageTitleStart, val).Trim();
		}

		public static bool Production
		{
			get
			{
				if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["Production"]))
				{
					return (ConfigurationManager.AppSettings["Production"].Equals("true"));
				}
				return true;
			}
		}

		public static String GamesSiteUrl
		{
			get
			{
				if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["GamesSiteUrl"]))
				{
					return ConfigurationManager.AppSettings["GamesSiteUrl"];
				}
				return "~/apps/games/default.aspx";
			}
		}

        public static String MadeBigUrl
        {
            get
            {
                if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["MadeBigUrl"]))
                {
                    return ConfigurationManager.AppSettings["MadeBigUrl"];
                }
                return "";
            }
        }

		public static String WidgetBoxInstallUrl
		{
			get
			{ 
				if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["WidgetBoxInstallUrl"]))
				{
					return ConfigurationManager.AppSettings["WidgetBoxInstallUrl"];
				}
				return "http://www.widgetbox.com/?wbx.network=madebig";
			}
		}

		public static String WidgetBoxWidgetUrl
		{
			get
			{
				if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["WidgetBoxWidgetUrl"]))
				{
					return ConfigurationManager.AppSettings["WidgetBoxWidgetUrl"];
				}
				return "http://widgetserver.com/syndication/subscriber/InsertWidget.js?appId=";
			}
		}

    public static String WidgetBoxCatalogUrl
		{
			get
			{
        if (!String.IsNullOrEmpty(ConfigurationManager.AppSettings["WidgetBoxCatalogUrl"]))
				{
					return ConfigurationManager.AppSettings["WidgetBoxCatalogUrl"];
				}
        return "http://next.widgetbox.com/gallery/madebig/home";
			}
		}

		public static bool IsProduction
		{
			get { return false; }
		}

	}
}