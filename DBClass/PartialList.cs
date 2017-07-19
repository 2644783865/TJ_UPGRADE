using System;
using System.Collections.Generic;
using System.Text;

namespace ZCZJ_DPF
{
	public class PartialList<T> : List<T>
	{
		protected long _TotalRecords = -1;

		public long TotalRecords
		{
			get { return _TotalRecords; }
			internal set { _TotalRecords = value; }
		}
	}
}
