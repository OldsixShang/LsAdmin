using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Model
{
    /// <summary>
    /// 分页通用数据对象
    /// </summary>
    [Serializable]
    public class Pager
    {
        #region 构造函数
        public Pager()
        {

        }
        public Pager(string defaultSort = "id")
        {
            _sort = defaultSort;
        }
        #endregion

        private string _sort = "id";
        private string _order = "desc";

        #region 请求参数
        /// <summary>
        /// 排序规则
        /// </summary>
        public string order
        {
            get
            {
                if (string.IsNullOrEmpty(_order))
                    _order = "asc";
                return _order;
            }
            set
            {
                _order = value;
            }
        }
        /// <summary>
        /// 排序列名
        /// </summary>
        public string sort
        {
            get
            {
                if (string.IsNullOrEmpty(_sort))
                    _sort = "Id";
                return _sort;
            }
            set
            {
                _sort = value;
            }
        }
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 每页条数
        /// </summary>
        public int rows { get; set; }

        #endregion

        /// <summary>
        /// 记录总数
        /// </summary>
        public int totalCount { get; set; }

        public int totalPage
        {
            get
            {
                if (rows == 0) return 0;
                return (totalCount % rows == 0) ? totalCount / rows : totalCount / rows + 1;
            }
        }
    }
}
