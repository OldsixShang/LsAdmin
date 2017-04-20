(function ($) {
    $.fn.extend({

        /*
         * 自定义表单提交
         * option
         * {url:'提交地址',onSuccess:'操作成功回调',onFailure:'操作失败回调'}
         */
        submitForm: function (option) {
            $.ls.loading(true, "正在处理,请稍后...", -1);
            $(this).attr("disabled", true);
            var defaults = {
                url: "",
                onSuccess: function (content, data) {
                    $.ls.success(content);
                },
                onFailure: function (description, data) {
                    $.ls.warning(description);
                },
                attachData: {},
            };
            var settings = $.extend(defaults, option);
            var formObj = $(this);
            var postData = $.ls.buidFormData(formObj);
            if (!$.ls.isNullOrEmpty(settings.attachData))
                postData['attachData'] = settings.attachData;
            $.ls.ajax(
                {
                    url: settings.url,
                    onSuccess: function (content, data) {
                        $.ls.loading(false);
                        $(this).removeAttr("disabled");
                        settings.onSuccess(content, data);
                    },
                    onFailure: function (description, data) {
                        $.ls.loading(false);
                        $.ls.warning(description);
                        settings.onFailure(description, data);
                    },
                    data: postData
                });
        },

    });
})(jQuery)