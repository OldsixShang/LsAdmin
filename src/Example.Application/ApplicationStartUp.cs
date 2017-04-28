using AutoMapper;
using Example.Domain.Entities.Authorization;
using Example.Dto.Auth;
using Example.Dto.Sys.ActionManage;
using Example.Dto.Sys.MenuManage;
using Example.Dto.Sys.PermissionManage;
using Example.Dto.Sys.RoleManage;
using Example.Dto.Sys.UserManage;
using Ls.Utilities;
using Ls.Utilities.Formatter;
using System;
using System.Collections.Generic;

namespace Example.Application
{
    public static class ApplicationStartUp
    {
        public static void AutoMapInit()
        {
            Mapper.Initialize(config=> {
                #region 系统管理

                #region 菜单管理
                config.CreateMap<Menu, MenuDto>()
                    .ForMember(ts => ts.MenuType, opt => opt.MapFrom(t => (int)t.MenuType))
                    .ForMember(ts => ts.MenuType, opt => opt.MapFrom(t => t.MenuType.ToString()));
                config.CreateMap<MenuDto, Menu>()
                    .ForMember(ts => ts.MenuType, opt => opt.MapFrom(t => Enum.Parse(typeof(MenuType), t.MenuType)))
                    .AfterMap((src, dest) => dest.Id = LsIdGenerator.CreateIdentity());
                #endregion

                #region 操作管理
                config.CreateMap<AuthAction, ActionDto>();
                config.CreateMap<ActionDto, AuthAction>()
                    .AfterMap((src, dest) => dest.Id = LsIdGenerator.CreateIdentity());
                #endregion

                #region 角色管理
                config.CreateMap<Role, RoleDto>();
                config.CreateMap<RoleDto, Role>()
                    .AfterMap((src, dest) => dest.Id = LsIdGenerator.CreateIdentity());
                #endregion

                #region 用户管理
                config.CreateMap<User, UserDto>()
                    .ForMember(ts => ts.CreatedTime, opt => opt.MapFrom(td => td.CreatedTime.ToString(DateTimeFormatterConstString.DateTimeH24)))
                    .ForMember(ts => ts.LastUpdatedTime, opt => opt.MapFrom(td => td.LastUpdatedTime.ToString(DateTimeFormatterConstString.DateTimeH24)))
                    .ForMember(ts => ts.Role, opt => opt.MapFrom(td => td.Role));
                config.CreateMap<UserDto, User>()
                    .ForMember(ts => ts.LoginId, opt => opt.MapFrom(td => td.Name))
                    .AfterMap((src, dest) => dest.Id = LsIdGenerator.CreateIdentity());
                #endregion

                #region 权限管理
                config.CreateMap<PermissionDto, Permission>()
                    .AfterMap((src, dest) => dest.Id = LsIdGenerator.CreateIdentity());
                config.CreateMap<Permission, PermissionDto>()
                    .ForMember(ts => ts.MenuName, opt => opt.MapFrom(td => td.Menu.Name))
                    .ForMember(ts => ts.MenuType, opt => opt.MapFrom(td => td.Menu.MenuType.ToString()))
                    .ForMember(ts => ts.Url, opt => opt.MapFrom(td => td.Menu.Url??""))
                    .ForMember(ts => ts.ActionName, opt => opt.MapFrom(td => td.Action.Name))
                    .ForMember(ts => ts.Template, opt => opt.MapFrom(td => td.Action.Template));
                config.CreateMap<Permission, PermissionMenuDto>()
                    .ForMember(ts => ts.MenuName, opt => opt.MapFrom(td => td.Menu.Name))
                    .ForMember(ts => ts.Url, opt => opt.MapFrom(td => td.Menu.Url))
                    .ForMember(ts => ts.Icon, opt => opt.MapFrom(td => td.Menu.Icon));
                config.CreateMap<Permission, PermissionMenuActionDto>()
                    .ForMember(ts => ts.MenuType, opt => opt.MapFrom(td => td.Menu.MenuType.ToString()))
                    .ForMember(ts => ts.Icon, opt => opt.MapFrom(td => td.Menu.Icon));
                #endregion
                #endregion

            });
           
        }
    }
}
