using Ls.Validation;

namespace Ls.Application.Dto {
    /// <summary>
    /// 服务参数 DTO，实现<see cref="IValidate"/>接口。
    /// </summary>
    public interface IInputDto : IValidate, IDto {
    }
}
