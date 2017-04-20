using Example.Domain.Entities.Authorization;
using Example.Domain.Repositories.Authorization;
using Example.Dto.Sys.MenuManage;
using System;
using System.Collections.Generic;
using Example.Application.ServiceInterfaces.Sys;
using Ls.Utilities;
using Ls.Model;

namespace Example.Application.ServiceImplements.Sys
{
    /// <summary>
    /// 菜单领域服务
    /// </summary>
    public class MenuService : BaseService,IMenuService
    {
        #region 字段
        public IMenuRepository _menuRepository; 
        #endregion

        public MenuService(IMenuRepository menuRepository)
        {
            _menuRepository = menuRepository;
        }

        /// <summary>
        /// 获取菜单信息
        /// </summary>
        /// <param name="Id">菜单唯一标识</param>
        /// <returns>菜单信息</returns>
        public MenuDto GetMenu(long Id)
        {
           Menu entity = _menuRepository.Get(Id);
           return entity.ToDto<MenuDto>();
        }
        /// <summary>
        /// 添加菜单
        /// </summary>
        /// <param name="dto">传入菜单信息</param>
        public void AddMenu(MenuDto dto)
        {
            Menu entity = dto.ToEntity<Menu>();
            _menuRepository.Add(entity);
        }
        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="dto">传入菜单信息</param>
        public void DeleteMenu(MenuDto dto)
        {
            Menu entity = _menuRepository.Get(SafeConvert.ToInt64(dto.Id));
            _menuRepository.Delete(entity);
        }
        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="dto">传入菜单信息</param>
        public void ModifyMenu(MenuDto dto)
        {
            Menu entity = _menuRepository.Get(SafeConvert.ToInt64(dto.Id));
            entity.Name = dto.Name;
            entity.Url = dto.Url;
            entity.Icon = dto.Icon;
            entity.MenuType =(MenuType)Enum.Parse(typeof(MenuType), dto.MenuType);
            _menuRepository.Update(entity);
        }
        /// <summary>
        /// 查询菜单信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <returns>菜单信息</returns>
        public IList<MenuDto> QueryMenu(QueryConditionDto conditionDto)
        {
            List<Menu> entities = _menuRepository.Query(conditionDto.Name);
            return entities.ToListDto<Menu, MenuDto>();
        }
        /// <summary>
        /// 分页查询菜单信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <param name="pager">分页信息</param>
        /// <returns>分页菜单信息</returns>
        public IList<MenuDto> QueryPagerMenu(QueryConditionDto conditionDto,Pager pager)
        {
            var entities = _menuRepository.QueryPager(conditionDto.Name, pager);
            return entities.ToListDto<Menu,MenuDto>();
        }
    }
}
