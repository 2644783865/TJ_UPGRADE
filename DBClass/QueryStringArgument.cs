using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace ZCZJ_DPF
{
	public class QueryStringArgument<T>
	{
		private String _QS = String.Empty;
		private T _Value = default(T);
		private T _DefaultValue = default(T);
		private bool _Valid = false;

		public QueryStringArgument(String qs)
		{
			if (String.IsNullOrEmpty(qs))
				throw new ArgumentNullException("Must have a Query String variable name");
			_QS = qs;
		}

		public QueryStringArgument(String qs, T defaultValue) : this(qs)
		{
			_DefaultValue = defaultValue;
		}

		public bool Exists()
		{
			return !String.IsNullOrEmpty(HttpContext.Current.Request.QueryString[_QS]);
		}

		public bool IsValid()
		{
			// get the value to determine validity
			T value = Value;
			return _Valid;
		}

		public T Value
		{
			get
			{
				if (Exists())
				{
					// convert the string to the type
					try
					{
						if (typeof(T) == typeof(Guid))
						{
							// deal with the guid special case							
							_Value = (T)Convert.ChangeType(new Guid(HttpContext.Current.Request.QueryString[_QS]), typeof(T));
						}
						else
						{
							// everything else
							_Value = (T) Convert.ChangeType(HttpContext.Current.Request.QueryString[_QS], typeof(T));
						}
						_Valid = true;
						return _Value;
					}
					catch (Exception)
					{
						_Valid = false;
					}
					return _DefaultValue;
				}
				else
				{
					_Valid = false;
					// doesn't exist, return the default
					return _DefaultValue;
				}
			}
		}
	}
}
