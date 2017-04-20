/// <reference path="ls.common.js" />
/// <reference path="ls.controls.js" />
(function ($) {
    $.ls = $.ls || {};

    /*增加标签页*/
    $.ls.addIframeTab = function (containerId, options) {

        /**************     functions     *************/ {
            //判断选项卡是否存在,根据tabId进行判断
            function IsTabExists() {
                var allTabs = tabContainer.tabs("tabs");
                for (var index = 0, max = allTabs.length; index < max; index++) {
                    iframes = allTabs[index].find('iframe');
                    if (iframes.length == 0)
                        continue;
                    if ($(iframes[0]).attr('tabId') === options.tabId) {
                        toSelectIndex = index;
                        return true;
                    }
                }
                return false;
            }

            //创建选项卡
            function createTab() {
                $.ls.loading(true,"页面加载中,请稍后...");
                var content = '<div class="easyui-layout" data-options="fit:true">'
                    + "<div data-options=\"region:'center',border:false\", style='padding-bottom:-20px;padding-left:5px;padding-right:5px;overflow:hidden'>"
                    + '<iframe tabId="' + options.tabId + '"scrolling="no" frameborder="0"  src="' + options.iframeUrl + '" style="width:100%;height:100%;"></iframe></div><div>';
                tabContainer.tabs('add', {
                    id: options.tabId,
                    title: options.tabTitle,
                    closable: options.closable,
                    content: content
                });
            }

        }


        var toSelectIndex = 0;
        var defaults = {
            tabId: '',
            tabTitle: '新的标签页',
            iframeUrl: '',
            closable: true,
        };
        options = $.extend(defaults, options);
        //获取容器
        var tabContainer = $('#' + containerId);
        if (IsTabExists()) {//标签页已经存在选中
            tabContainer.tabs('select', toSelectIndex);
        }
        else {//不存在 新增标签页
            createTab();
        }
        
    }
})(jQuery)