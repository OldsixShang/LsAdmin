using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Dto
{
    public interface IDto<T>
    {
        T Id { get; set; }
    }

    [Serializable]
    public class BaseDto<T> : IDto<T>
    {
        public T Id { get; set; }
    }
}
