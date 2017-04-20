using AutoMapper;
using Ls.Domain.Entities;
using Ls.Dto;
using System.Collections.Generic;

namespace Tts.Platform.Application
{
    /// <summary>
    /// 配置关系扩展方法
    /// </summary>
    public static class AutoMapExtensions
    {
        #region dto转entity
        /// <summary>
        /// dto转entity,添加
        /// </summary>
        /// <typeparam name="T">entity</typeparam>
        /// <param name="value">dto</param>
        /// <returns></returns>
        public static T ToEntity<T>(this BaseDto value) where T : Entity
        {
            return Mapper.Map<T>(value);
        }
        /// <summary>
        /// dto转entity,修改
        /// </summary>
        /// <typeparam name="T">entity</typeparam>
        /// <param name="value">dto</param>
        /// <param name="entity">entity</param>
        /// <returns></returns>
        public static T ToEntity<T>(this BaseDto value, T entity) where T : Entity
        {
            return Mapper.Map(value, entity);
        }
        #endregion

        #region entity转化为dto
        /// <summary>
        /// entity转化为dto
        /// </summary>
        /// <typeparam name="T">dto</typeparam>
        /// <param name="value">entity</param>
        /// <returns></returns>
        public static T ToDto<T>(this Entity value) where T : BaseDto
        {
            return Mapper.Map<T>(value);
        }
        /// <summary>
        /// entity列表转化为dto列表
        /// </summary>
        /// <typeparam name="Entity">entity</typeparam>
        /// <typeparam name="Output">dto</typeparam>
        /// <param name="value">实体列表</param>
        /// <returns></returns>
        public static List<Output> ToListDto<Entity, Output>(this IList<Entity> value)
        {
            return Mapper.Map<List<Output>>(value);
        }

        /// <summary>
        /// dynamic列表转化为dto列表
        /// </summary>
        /// <typeparam name="dynamic">entity</typeparam>
        /// <typeparam name="T">dto</typeparam>
        /// <param name="value">实体列表</param>
        /// <returns></returns>
        public static List<T> ToDynamicListDto<T>(dynamic value) where T : BaseDto
        {
            return Mapper.Map<List<T>>(value);
        }

        /// <summary>
        /// dynamic 转 dto
        /// </summary>
        /// <typeparam name="dynamic">entity</typeparam>
        /// <typeparam name="T">dto</typeparam>
        /// <param name="value">动态类型的实体</param>
        /// <returns></returns>
        public static T DynamicToDto<T>(dynamic value)
        {
            return Mapper.Map<T>(value);
        }

        #endregion

        #region 动态类型
        /// <summary>
        /// dynamic转化为dto列表
        /// </summary>
        /// <typeparam name="dynamic">entity</typeparam>
        /// <typeparam name="T">dto</typeparam>
        /// <param name="value">实体列表</param>
        /// <returns></returns>
        public static List<T> ToDtoList<T>(dynamic value)
            where T : class
        {
            return Mapper.Map<List<T>>(value);
        }
        #endregion

    }
}
