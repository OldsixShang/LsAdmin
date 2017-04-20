using System;

namespace Ls.Mvc.Authorization
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ActionTemplateAttribute : Attribute
    {
        private bool _condition = true;
        /// <summary>
        /// 操作模板
        /// </summary>
        public ActionTemplate Template { get; set; }
        /// <summary>
        /// 条件
        /// </summary>
        public bool Condition
        {
            get
            {
                return _condition;
            }
            set
            {
                _condition = value;
            }
        }
    }
}