using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ls.Dto;

namespace Example.Dto.UI.easyUI
{
    /// <summary>
    /// easyui - combotree 元素
    /// </summary>
    public class ComboTreeItem : TreeNodeDto<ComboTreeItem>
    {
        public ComboTreeItem() : base() { }
        /// <summary>
        /// 键
        /// </summary>
        [JsonProperty("id")]
        public new string Id { get; set; }
        /// <summary>
        /// 值
        /// </summary>
        [JsonProperty("text")]
        public string Text { get; set; }
        /// <summary>
        /// 附加信息
        /// </summary>
        public string Attach { get; set; }
    }
}
