using Example.Dto.Sys.MenuManage;
using Ls.Model;
using System.Collections.Generic;

namespace Example.Application.ServiceInterfaces.Sys
{
    public interface IMenuService
    {
        /// <summary>
        /// 获取角色
        /// </summary>
        /// <param name="Id">角色唯一标识</param>
        /// <returns>角色信息</returns>
        MenuDto GetMenu(string Id);
        /// <summary>
        /// 添加角色
        /// </summary>
        /// <param name="dto">角色信息</param>
        void AddMenu(MenuDto dto);
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="dto">角色信息</param>
        void DeleteMenu(MenuDto dto);
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="dto">角色信息</param>
        void ModifyMenu(MenuDto dto);

        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <returns>角色信息</returns>
        IList<MenuDto> QueryMenu(QueryConditionDto conditionDto);
        /// <summary>
        /// 分页查询角色信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <param name="pager">分页信息</param>
        /// <returns>角色信息</returns>
        IList<MenuDto> QueryPagerMenu(QueryConditionDto conditionDto, Pager pager);
    }
}
