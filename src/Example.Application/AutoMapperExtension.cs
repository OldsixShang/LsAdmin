using AutoMapper;
using Ls.Domain.Entities;
using Ls.Dto;
using System.Collections.Generic;

namespace Example.Application
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
        /// <typeparam name="TDto">dto</typeparam>
        /// <param name="value">实体列表</param>
        /// <returns></returns>
        public static List<TDto> ToDtoList<TEnity,TDto>(this List<TEnity> value)
            where TEnity : Entity
            where TDto : BaseDto
        {
            return Mapper.Map<List<TDto>>(value);
        }
        #endregion


    }
}
