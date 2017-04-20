using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dto.UI.easyUI
{
    public class DataGrid<TDto>
        where TDto : class
    {
        public DataGrid()
        {
            rows = new List<TDto>();
        }
        /// <summary>
        /// 记录总数
        /// </summary>
        public int total { get; set; }

        /// <summary>
        /// 行数据
        /// </summary>
        public List<TDto> rows { get; set; }

        public object footer { get; set; }

    }

    [Serializable]
    public class DataGridRequest
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int page { get; set; }
        /// <summary>
        /// 页容量
        /// </summary>
        public int rows { get; set; }
        /// <summary>
        /// 排序字段
        /// </summary>
        public string sort { get; set; }
        /// <summary>
        /// 排序方式
        /// </summary>
        public string order { get; set; }
    }
}
