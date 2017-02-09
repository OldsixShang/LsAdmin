using Ls.Extensions;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ls.Validation.Interception {
    /// <summary>
    /// 数据验证器。
    /// </summary>
    internal class InputDtoValidator {
        private readonly object[] _parameterValues;
        private readonly ParameterInfo[] _parameters;
        private readonly List<ValidationResult> _validationErrors;

        /// <summary>
        /// 数据验证器构造函数。
        /// </summary>
        /// <param name="method">验证的方法</param>
        /// <param name="parameterValues">方法的参数值</param>
        public InputDtoValidator(MethodInfo method, object[] parameterValues) {
            _parameterValues = parameterValues;
            _parameters = method.GetParameters();
            _validationErrors = new List<ValidationResult>();
        }

        /// <summary>
        /// 验证数据。
        /// </summary>
        public void Validate() {
            if (_parameters.IsNullOrEmpty()) {
                return;
            }
            if (_parameters.Length != _parameterValues.Length) {
                throw new LsException("参数数量与参数值数据不匹配。");
            }
            for (int i = 0; i < _parameters.Length; i++) {
                ValidateMethodParameter(_parameters[i], _parameterValues[i]);
            }
            if (_validationErrors.Any()) {
                StringBuilder sbErrorInfo = new StringBuilder();
                foreach (ValidationResult error in _validationErrors) {
                    sbErrorInfo.AppendLine(error.ErrorMessage);
                }
                throw new LsValidationException(sbErrorInfo.ToString()) { ValidationErrors = _validationErrors };
            }

        }

        #region Private Method

        private void ValidateMethodParameter(ParameterInfo parameterInfo, object parameterValue) {
            if (parameterValue == null) {
                if (!parameterInfo.IsOptional && !parameterInfo.IsOut) {
                    _validationErrors.Add(new ValidationResult(parameterInfo.Name + " 为 null 。", new[] { parameterInfo.Name }));
                }
                return;
            }

            ValidateObjectRecursively(parameterValue);
        }

        private void ValidateObjectRecursively(object validatingObject) {
            if (validatingObject is IEnumerable) {
                foreach (var item in (validatingObject as IEnumerable)) {
                    ValidateObjectRecursively(item);
                }
            }

            if (!(validatingObject is IValidate)) {
                return;
            }

            SetValidationAttributeErrors(validatingObject);

            if (validatingObject is ICustomValidate)
            {
                (validatingObject as ICustomValidate).AddValidationErrors(_validationErrors);
            }

            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();
            foreach (var property in properties) {
                ValidateObjectRecursively(property.GetValue(validatingObject));
            }
        }

        private void SetValidationAttributeErrors(object validatingObject) {
            var properties = TypeDescriptor.GetProperties(validatingObject).Cast<PropertyDescriptor>();
            foreach (var property in properties) {
                var validationAttributes = property.Attributes.OfType<ValidationAttribute>().ToArray();
                if (validationAttributes.IsNullOrEmpty()) {
                    continue;
                }

                var validationContext = new ValidationContext(validatingObject) {
                    DisplayName = property.DisplayName,
                    MemberName = property.Name
                };
                foreach (var attribute in validationAttributes) {
                    var result = attribute.GetValidationResult(property.GetValue(validatingObject), validationContext);
                    if (result != null) {
                        _validationErrors.Add(result);
                    }
                }
            }
        }

        #endregion

    }
}
