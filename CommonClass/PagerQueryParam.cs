using System;
using System.Collections.Generic;
using System.Text;

namespace ZCZJ_DPF
{
    /// <summary>
    /// 分页存储过程查询参数类
    /// </summary>
    public class PagerQueryParam
    {

        #region "Private Variables"
        private string _TableName;
        private string _PrimaryKey;
        private string _OrderField;
        private int _OrderType=0;
        private string _ShowFields;     
        private int _PageIndex = 1;
        private int _PageSize = int.MaxValue;
        private int _TotalCount = 0;
        private int _ReTotalCount = 0;
        private string _StrWhere;
        #endregion

        #region "Public Variables"
        /// <summary>
        /// 表名称
        /// </summary>
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }
        /// <summary>
        /// 主键字段
        /// </summary>
        public string PrimaryKey
        {
            get { return _PrimaryKey; }
            set { _PrimaryKey = value; }
        }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string OrderField
        {
            get { return _OrderField; }
            set { _OrderField = value; }
        }
        /// <summary>
        /// 排序类型 1:升序 0:降序 默认0
        /// </summary>
        public int OrderType
        {
            get { return _OrderType; }
            set { _OrderType = value; }
        }
        /// <summary>
        /// 显示字段 中间用,隔开，如果是关联表字段表名.[字段名]
        /// </summary>
        public string ShowFields
        {
            get { return _ShowFields; }
            set { _ShowFields = value; }
        }
        /// <summary>
        /// 页码　默认等于1
        /// </summary>
        public int PageIndex
        {
            get { return _PageIndex; }
            set { _PageIndex = value; }
        }
        /// <summary>
        /// 页大小　默认int.MaxValue
        /// </summary>
        public int PageSize
        {
            get { return _PageSize; }
            set { _PageSize = value; }
        }
        /// <summary>
        /// 记录总数 初始值为0,当PageIndex!=1,请转入此参数 已提高存储过程的执行速度
        /// </summary>
        public int TotalCount
        {
            get { return _TotalCount; }
            set { _TotalCount = value; }
        } 
        /// <summary>
        /// 记录总数 初始值为0,当访问PageIndex==1,返回总数
        /// </summary>
        public int ReTotalCount
        {
            get { return _ReTotalCount; }
            set { _ReTotalCount = value; }
        }
        /// <summary>
        /// 查询条件，不带Where,多表查询时，请在字段前加上表别名
        /// </summary>
        public string StrWhere
        {
            get { return _StrWhere; }
            set { _StrWhere = value; }
        }
        #endregion
    }
}
