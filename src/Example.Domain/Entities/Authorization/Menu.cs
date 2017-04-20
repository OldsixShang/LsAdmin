using Ls.Authorization;
using Ls.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Domain.Entities.Authorization
{
    /// <summary>
    /// 菜单
    /// </summary>
    public class Menu : Entity,IMenu
    {
        /// <summary>
        /// 菜单类型Id
        /// </summary>
        public MenuType MenuType { get; set; }
        /// <summary>
        /// 菜单名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }
    }

    /// <summary>
    /// 菜单类型
    /// </summary>
    public enum MenuType
    {
        /// <summary>
        /// 左侧导航栏
        /// </summary>
        左侧导航栏 = 0,
        /// <summary>
        /// 固定菜单
        /// </summary>
        固定 = 1,
        /// <summary>
        /// WebApi
        /// </summary>
        WebApi = 2,
        /// <summary>
        /// 顶部快捷菜单栏
        /// </summary>
        顶部快捷菜单栏 = 3,
    }
}
