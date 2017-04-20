using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Mvc.Authorization
{
    /// <summary>
    /// 操作模板
    /// </summary>
    public enum ActionTemplate
    {
        Query,
        Add,
        Modify,
        Delete,
        RePull,
        Export,
        Import,
        DistributePermission,
        HighCharts,
    }
}
