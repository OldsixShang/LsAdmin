using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Dto
{
    public class CreatedAndUpdatedDto : BaseDto
    {
        public string CreatedTime { get; set; }
        public string LastUpdatedTime { get; set; }
    }
}
