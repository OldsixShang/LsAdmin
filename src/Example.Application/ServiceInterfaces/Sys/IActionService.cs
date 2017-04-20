using Example.Dto.Sys.ActionManage;
using Ls.Model;
using System.Collections.Generic;

namespace Tts.Platform.Application.ServiceInterfaces.Sys
{
    public interface IActionService
    {
        /// <summary>
        /// 获取操作
        /// </summary>
        /// <param name="Id">操作唯一标识</param>
        /// <returns>操作信息</returns>
        ActionDto GetAction(long Id);
        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="dto">操作信息</param>
        void AddAction(ActionDto dto);
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="dto">操作信息</param>
        void DeleteAction(ActionDto dto);
        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="dto">操作信息</param>
        void ModifyAction(ActionDto dto);

        /// <summary>
        /// 查询操作信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <returns>操作信息</returns>
        IList<ActionDto> QueryAction(QueryConditionDto conditionDto);
        /// <summary>
        /// 分页查询操作信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <param name="pager">分页信息</param>
        /// <returns>操作信息</returns>
        IList<ActionDto> QueryPagerAction(QueryConditionDto conditionDto, Pager pager);
    }
}
