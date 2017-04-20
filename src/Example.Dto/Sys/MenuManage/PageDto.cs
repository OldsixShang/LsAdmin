using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Dto.Sys.MenuManage
{
    public class PageDto
    {
        public PageDto()
        {
            QueryForm = new QueryConditionDto();
        }
        public QueryConditionDto QueryForm { get; set; }
    }
}
