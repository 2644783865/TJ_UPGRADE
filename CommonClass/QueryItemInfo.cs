using System;

namespace ZCZJ_DPF
{
    [Serializable]
    public class QueryItemInfo
    {
        private string _userID;

        public string UserID
        {
            get { return _userID; }
            set { _userID = value; }
        }

        private string _pageName;

        public string PageName
        {
            get { return _pageName; }
            set { _pageName = value; }
        }


        private string _controlID;

        public string ControlID
        {
            get { return _controlID; }
            set { _controlID = value; }
        }

        private string _controlType;

        public string ControlType
        {
            get { return _controlType; }
            set { _controlType = value; }
        }

       

        private string _controlValue;

        public string ControlValue
        {
            get { return _controlValue; }
            set { _controlValue = value; }
        }

        /// <summary>
       /// 默认构造函数
       /// </summary>
        public QueryItemInfo() { }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="userID"></param>
       /// <param name="userName"></param>
       /// <param name="userDep"></param>
       /// <param name="userDepID"></param>
        public QueryItemInfo(string userID, string pageName, string controlID, string controlType, string controlValue)
        {
            this._userID = userID;
            this._pageName = pageName;
            this._controlID = controlID;
            this._controlType = controlType;
            this._controlValue = controlValue;
        }
    }
}
