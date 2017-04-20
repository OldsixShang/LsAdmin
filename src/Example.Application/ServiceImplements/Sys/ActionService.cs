using Example.Domain.Entities.Authorization;
using Example.Domain.Repositories.Authorization;
using Example.Dto.Sys.ActionManage;
using Ls.Model;
using Ls.Utilities;
using System.Collections.Generic;
using Example.Application.ServiceInterfaces.Sys;

namespace Example.Application.ServiceImplements.Sys
{
    /// <summary>
    /// 操作领域服务
    /// </summary>
    public class ActionService : BaseService,IActionService
    {
        #region 字段
        public IActionRepository _actionRepository; 
        #endregion

        public ActionService(IActionRepository actionRepository)
        {
            _actionRepository = actionRepository;
        }

        /// <summary>
        /// 获取操作信息
        /// </summary>
        /// <param name="Id">操作唯一标识</param>
        /// <returns>操作信息</returns>
        public ActionDto GetAction(long Id)
        {
           AuthAction entity = _actionRepository.Get(Id);
           return entity.ToDto<ActionDto>();
        }
        /// <summary>
        /// 添加操作
        /// </summary>
        /// <param name="dto">传入操作信息</param>
        public void AddAction(ActionDto dto)
        {
            AuthAction entity = dto.ToEntity<AuthAction> ();
            _actionRepository.Add(entity);
        }
        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="dto">传入操作信息</param>
        public void DeleteAction(ActionDto dto)
        {
            AuthAction entity = _actionRepository.Get(SafeConvert.ToInt64(dto.Id));
            _actionRepository.Delete(entity);
        }
        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="dto">传入操作信息</param>
        public void ModifyAction(ActionDto dto)
        {
            AuthAction entity = _actionRepository.Get(SafeConvert.ToInt64(dto.Id));
            entity.Name = dto.Name;
            entity.Template = dto.Template;
            entity.Description = dto.Description;
            _actionRepository.Update(entity);
        }
        /// <summary>
        /// 查询操作信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <returns>操作信息</returns>
        public IList<ActionDto> QueryAction(QueryConditionDto conditionDto)
        {
            List<AuthAction>  entities = _actionRepository.Query(conditionDto.ActionName);
            return entities.ToListDto<AuthAction, ActionDto>();
        }
        /// <summary>
        /// 分页查询操作信息
        /// </summary>
        /// <param name="conditionDto">查询条件</param>
        /// <param name="pager">分页信息</param>
        /// <returns>分页操作信息</returns>
        public IList<ActionDto> QueryPagerAction(QueryConditionDto conditionDto,Pager pager)
        {
            var entities = _actionRepository.QueryPager(conditionDto.ActionName, pager);
            return entities.ToListDto<AuthAction, ActionDto>();
        }
    }
}
