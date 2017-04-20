//**************************************/
//  描述：公共操作方法
//  文件：jquery.tts.common.js
//  created by CharlesShang 2015-04-09
//**************************************/

(function ($) {
    //easyUI验证拓展
    $.extend($.fn.validatebox.defaults.rules, {
        equals: {
            validator: function (value, param) {
                return value == $(param[0]).val();
            },
            message: '输入的值不同'
        },
        integer: {
            validator: function (value) {
                return /^-?\d+$/.test(value);
            },
            message: '该项应为整数'
        },
        NegativeABC:
        {
            validator: function (value) {
                return /^-?\d+$/.test(value);
            },
            message: '该项应为整数'
        },
        positiveInteger: {
            validator: function (value) {
                return /^[0-9]*[1-9][0-9]*$/.test(value);
            },
            message: '该项应为大于0的整数'
        },
        Negative: {
            validator: function (value) {
                return /^[0-9]*[0-9][0-9]*$/.test(value);
            },
            message: '该项应为非负整数'
        },
        decimal: {
            validator: function (value) {
                return /^(-?\d+)(\.\d+)?$/.test(value);
            },
            message: '该项应为数字'
        },
        ZIPCode: {
            validator: function (value) {
                return /^[1-9]\d{5}$/.test(value);
            },
            message: '邮政编码不存在'
        },
        AreaNum: {
            validator: function (value) {
                return /^\d{3,4}$/.test(value);
            },
            message: '区号不正确'
        },
        Tell: {
            validator: function (value) {
                return /^(\d{3,4}-)?\d{6,8}$/.test(value);
            },
            message: '电话号码格式不正确'
        },
        PHONENUMBER: {
            validator: function (value) {
                return /^[1]+[3,5,8]+\d{9}/.test(value);
            },
            message: '手机号码格式不正确'
        },
        extensionNumber: {
            validator: function (value) {
                return /^[0-9]*[1-9][0-9]*$/.test(value);
            },
            message: '分机号码格式不正确'
        },
        Fax: {
            validator: function (value) {
                return /^(\d{3,4}-)?\d{7,8}$/.test(value);
            },
            message: '传真号码格式不正确'
        },
        FaxextensionNumber: {
            validator: function (value) {
                return /^[0-9]*[1-9][0-9]*$/.test(value);
            },
            message: '传真分机号码格式不正确'
        },

        IncludeChinese: {
            validator: function (value) {
                return !/.*[\u4e00-\u9fa5]+.*$/.test(value);
            },
            message: '字符中不能包含中文'
        },
        EngAndNum: {
            validator: function (value) {
                return /[a-zA-Z0-9]+/.test(value);
            },
            message: '只能包含英文字符和数字'
        },
        MaxLength: {
            validator: function (value, parm) {
                return value.length <= parm[0];
            },
            message: '字符长度不能超过{0}位'
        },

        MinLength: {
            validator: function (value, parm) {
                return value.length >= parm[0];
            },
            message: '字符长度必须大于{0}位'
        },
        //combobox 必填
        ComboRequired: {
            validator: function (value, parm) {
                if (parm == undefined) {
                    $.tts.alert('提示', '请指定 [ComboRequired] 验证参数。'); return;
                }
                if ($("#" + parm[0]).length == 0) {
                    $.tts.alert('提示', '错误的 [ComboRequired] 验证参数。'); return;
                }
                var data = $("#" + parm[0]).combobox('getData');
                var find = false;
                for (var j = 0; j < data.length; j++) {
                    if ((value == data[j].id || value == data[j].text) && !$.IsNullOrEmpty(data[j].id)) {
                        find = true;
                        break;
                    }
                }
                return find;
            },
            message: '此项为必选项'
        },
        ComboTreeRequired: {
            validator: function (value, parm) {
                if (parm == undefined) {
                    $.tts.alert('请指定 [ComboTreeRequired] 验证参数。'); return;
                }
                if ($("#" + parm[0]).length == 0) {
                    $.tts.alert('错误的 [ComboTreeRequired] 验证参数。'); return;
                }
                if ($("#" + parm[0]).combotree('getValues') == '') {
                    return false;
                }
                return true;
            },
            message: '必选项'
        },
        DecimelThree: {
            validator: function (value) {
                return /(^-?(?:(?:\d{0,3}(?:,\d{3})*)|\d*))(\.(\d{1,3}))?$/.test(value);
            },
            message: '大于0且小数不能超过3位'
        },
        DecimelTwo: {
            validator: function (value) {
                return /^[0-9]+(.[0-9]{2})?$/.test(value);
            },
            message: '非负数，如果是有小数部分，必须是2位小数'
        },
        TwoCharacter: {
            validator: function (value) {
                return /^[A-Za-z0-9]{2}/.test(value);
            },
            message: '必须为2位数字或者字符组合'
        },
        TellAndPhone: {
            validator: function (value) {
                return /(^(\d{3,4}-)?\d{7,8}$)|(^1[2|3|4|5|6|7|8|9][0-9]\d{4,8}$)/.test(value);
            },
            message: '电话或者手机格式不正确'//既可以是电话号码也可以是手机号码，7到11位
        },
        Email: {
            validator: function (value) {
                return /^(\w)+(\.\w+)*@(\w)+((\.\w+)+)$/.test(value);
            },
            message: '邮箱格式不正确'
        },
        CompareTo: {
            validator: function (value, parm) {
                if (parm == undefined) {
                    $.tts.alert('请指定 [CompareTo] 验证参数。'); return;
                }
                var val = $("#" + parm[0]).numberbox('getValue');
                return parseFloat(val) < parseFloat(value);
            },
            message: '不能小于指定值'
        }
    });
})(jQuery)