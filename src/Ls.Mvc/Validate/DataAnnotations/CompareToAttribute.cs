using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Ls.Mvc.Validate.DataAnnotations
{
    public class CompareToAttribute : ValidationAttribute
    {
        public CompareToAttribute(string otherProperty)
            : base()
        {
            if (otherProperty == null)
            {
                throw new ArgumentNullException("otherProperty");
            }
            this.OtherProperty = otherProperty;
        }
        /// <summary>
        /// 其他属性
        /// </summary>
        public string OtherProperty { get; set; }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            PropertyInfo property = validationContext.ObjectType.GetProperty(this.OtherProperty);
            if (property == null)
            {
                object[] args = new object[] { this.OtherProperty };
                return new ValidationResult(string.Format(CultureInfo.CurrentCulture, "未知属性", args));
            }
            
            object objB = property.GetValue(validationContext.ObjectInstance, null);
            if (property.PropertyType == typeof(Int16) ||
                property.PropertyType == typeof(Int32) ||
                property.PropertyType == typeof(Int64) ||
                property.PropertyType == typeof(Decimal) ||
                property.PropertyType == typeof(Double) ||
                property.PropertyType == typeof(float))
            {
                return objB.GetHashCode() <= value.GetHashCode() ? null : new ValidationResult(this.ErrorMessage);
            }
            else if (property.PropertyType == typeof(DateTime))
            {
                return (DateTime)objB <= (DateTime)value ? null : new ValidationResult(this.ErrorMessage);
            }
            else if (property.PropertyType == typeof(String))
            {
                return objB.ToString().CompareTo(value) <= 0 ? null : new ValidationResult(this.ErrorMessage);
            }
            else
            {
                return new ValidationResult("不支持的类型");
            }
        }
    }
}
