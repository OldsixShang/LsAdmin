(function ($) {
    $.ls = $.ls || {};
    $.extend($.ls, {
        isNullOrEmpty: function (v) {
            return v == '' || v == undefined || v == null || typeof (v) == "undefined";
        },
        setUrlParam : function (oldurl, pname, pvalue) {
            var reg = new RegExp("(\\?|&)" + pname + "=([^&]*)(&|$)", "gi");
            var pst = oldurl.match(reg);
            if ((pst == undefined) || (pst == null)) {
                return oldurl + ((oldurl.indexOf("?") == -1) ? "?" : "&") + pname + "=" + pvalue;
            }
            var t = pst[0];
            var retxt = t.substring(0, t.indexOf("=") + 1) + pvalue;
            if (t.charAt(t.length - 1) == '&') retxt += "&";
            return oldurl.replace(reg, retxt);
        },
        ajax: function (options) {
            var defaults = {
                type: "POST",
                url: "",
                async: true,
                data: {},
                dataType: "json",
                beforsend: function (XMLHttpRequest) {
                    this;   //调用本次ajax请求时传递的options参数
                },
                complete: function (XMLHttpRequest, textStatus) {
                    this;    //调用本次ajax请求时传递的options参数
                },
                success: function (data) {
                    var jsonData = data;//eval('(' + data + ')');
                    if (jsonData.ResponseCode == 200) {
                        this.onSuccess(jsonData.Content,jsonData);
                    }
                    else if (jsonData.ResponseCode == 500) {
                        this.onFailure(jsonData.Description, jsonData);
                    }
                    else if (jsonData.ResponseCode == 100) {
                        this.onExpired();
                    }
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    //通常情况下textStatus和errorThrown只有其中一个包含信息
                    this;   //调用本次ajax请求时传递的options参数
                },
                contentType: "application/x-www-form-urlencoded",
                /*自定义回掉函数*/
                onSuccess: function (content, data) {

                },
                onFailure: function (des,data) {
                    $.ls.warning(des);
                },
                onExpired: function (data) {
                    window.top.location = "~/Home/Login";
                }
            };
            var settings = $.extend(defaults, options);
            var $hidPageId = $("#_hidPageId");
            if ($hidPageId) settings.url = this.setUrlParam(settings.url, "pageId", $hidPageId.val());
            $.ajax(settings);
        },
        /*
         * 构建表单提交数据
         * parms:
         * formObj：表单对象
         */
        buidFormData: function (formObj) {
            if ($.ls.isNullOrEmpty(formObj)) alert("找不到表单对象");
            var postData = {}, elements = formObj[0].elements;
            for (var i = 0, len = elements.length; i < len; i++) {
                var $el = $(elements[i]), elname = $el.attr('name');
                if ($.ls.isNullOrEmpty(elname)) continue;
                //1.lsCombox组建
                if ($el.is('input') && $el.hasClass('easyui-combobox')) {
                    if ($el.attr("multiple")) postData[elname] = $el.combobox('getValues')
                    else
                        postData[elname] = $el.lsCombobox('getValue');
                }
                //2.基础input组建
                else if ($el.is('input') || $el.is('textarea')) {
                    if (postData[elname]) postData[elname] = postData[elname] + "," + $el.val();
                    else postData[elname] = $el.val();
                }
            }
            return postData;
        },
        /*
         * Excel导出
         * params:
         * url:地址
         * formObj:导出条件查询表单对象
         * attachData:导出附加条件数据
         */
        Export: function (url, formId, attachData) {
            formId = formId || "QueryForm";
            var formObj = $("#" + formId);
            var postData = $.ls.buidFormData(formObj);
            for (var p in postData) {
                url = $.ls.setUrlParam(url, p, postData[p]);
            }
            for (var p in attachData) {
                url = $.ls.setUrlParam(url, p, attachData[p]);
            }
            window.location = url;
        }
    });

    /*消息*/
    $.ls.messages = {
        SuccessAdded : "添加成功！",
        SuccessModified : "修改成功！",
        SuccessDeleted: "删除成功！",
        SuccessOperation: "操作成功！",
    }
})(jQuery)