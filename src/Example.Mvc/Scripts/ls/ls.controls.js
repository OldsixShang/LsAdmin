
(function ($) {
    $.fn.extend({
        /*tabs*/
        lsTabs: function (options) {
            var defaults = {
                border: false,
                onSelect: function (title) {
                    alert(title + ' is selected');
                }
            };
            var settings = $.extend({}, defaults, options);
            $(this).tabs(settings);
        },
        /*Tree*/
        lsTree: function (options) {
            var defaults = {
                animate: true,
            };
            var settings = $.extend({}, defaults, options);
            $(this).tree(settings);
        },
        //数据控件lsGrid构造
        //参数说明:
        //options 配置项
        //默认值:
        /*
        
        */
        lsGrid: function (options) {
            var gridId = $(this).attr('id');
            if ($.ls.isNullOrEmpty(gridId)) alert("no id with gid!");
            var defaults = {
                url: '',
                formId: 'QueryForm',
                sortName: 'Id',
                sortOrder: 'desc',
                fitColumns: false,
                striped: true,
                idField: 'Id',
                data: [],
                loadMsg: '数据加载中...',
                pagination: true,
                pageSize: 20,
                pageList: ['20', '50', '100', '500'],
                autoLoad: false,
                rownumbers: true,
                singleSelect: true,
                loadFilter: function (data) {
                    if (data.ResponseCode === 500) {
                        $.ls.warning(data.Description);
                    }
                    else {
                        return data;
                    }
                }
            };
            var gridSettings = {};
            var settings = $.extend({}, defaults, options);

            if (!$.ls.isNullOrEmpty(settings.url)) {
                gridSettings.reloadUrl = settings.url;
                if (!settings.autoLoad)
                    settings.url = "";//不自动加载时 清空url
            }
            /*
                 * param：参数对象传递给远程服务器。
                 * success(data)：当检索数据成功的时候会调用该回调函数。
                 * error()：
                 */
            //settings.loader = function (param, success, error) {
            //    $.ls.ajax({
            //        url: settings.url,
            //        postData: param,
            //        onSuccess: function (data) {
            //            success(data);
            //        },
            //        onFailure: function (content) {
            //            $.ls.warning(data.Description);
            //        }
            //    });

            //};
            var $hidPageId = $("#_hidPageId");
            if ($hidPageId) gridSettings.reloadUrl = $.ls.setUrlParam(gridSettings.reloadUrl, "pageId", $hidPageId.val());

            var grid = $(this).datagrid(settings);
            grid.url = gridSettings.reloadUrl;
            //lsGrid方法：
            //重新载入grid
            //参数:opts
            grid.reload = function (opt) {
                var dfts = {
                    url: '',
                    objData: {},//附加请求数据 对象
                    listData: [],//附加请求数据 列表
                };
                var setgs = $.extend({}, dfts, opt);
                if ($.ls.isNullOrEmpty($.data($(grid)[0], "datagrid").options.url)) {
                    $.data($(grid)[0], "datagrid").options.url = grid.url;
                }
                if (!$.ls.isNullOrEmpty(setgs.url)) {
                    $.data($(grid)[0], "datagrid").options.url = setgs.url;
                }
                var formObj = $('#' + $.data($(grid)[0], "datagrid").options.formId);
                var postData = $.ls.buidFormData(formObj);
                setgs.postData = $.extend({}, postData, { ObjData: setgs.objData, ListData: setgs.listData });

                grid.datagrid('reload', setgs.postData)
            };
            //获取选中的行
            grid.getSelectedRow = function () {
                return grid.datagrid('getSelected');
            }
            return grid;
        },
        /*
         *
         *
         */
        lsTreeGrid: function (options) {
            
            if (typeof (options) === 'object') {
                var defaults = {
                    idField: "Id",
                    treeField: "Name",
                    formId: 'QueryForm',
                    animate: true,
                };
                var $hidPageId = $("#_hidPageId");
                if ($hidPageId) options.url = $.ls.setUrlParam(options.url, "pageId", $hidPageId.val());
                var settings = $.extend(defaults, options);
                var treegrid = $(this).treegrid(settings);
                
                //lsGrid方法：
                //重新载入grid
                //参数:opts
                treegrid.reload = function (opt) {
                    var dfts = {
                        url: '',
                        objData: {},//附加请求数据 对象
                        listData: [],//附加请求数据 列表
                    };
                    var setgs = $.extend({}, dfts, opt);
                    if ($.ls.isNullOrEmpty($.data($(treegrid)[0], "treegrid").options.url)) {
                        $.data($(treegrid)[0], "treegrid").options.url = treegrid.url;
                    }
                    if (!$.ls.isNullOrEmpty(setgs.url)) {
                        $.data($(treegrid)[0], "treegrid").options.url = setgs.url;
                    }
                    var formObj = $('#' + $.data($(treegrid)[0], "treegrid").options.formId);
                    var postData = $.ls.buidFormData(formObj);
                    postData = $.extend({}, postData, { ObjData: setgs.objData, ListData: setgs.listData });
                    treegrid.treegrid('reload', postData)
                };
                //获取选中的行
                treegrid.getSelectedRow = function () {
                    return treegrid.treegrid('getSelected');
                }
                return treegrid;
            }
        },
        /*
         * lsCombobox:下拉列表
         * 参数:options
         * 默认值:xxx
         */
        lsCombobox: function (options, parm1, parm2) {

            var lsMethods = {
                pushEmptyItem: function (arr, valueField, textField) {
                    var emptyItem = {};
                    emptyItem[valueField] = "";
                    emptyItem[textField] = "请选择";
                    arr.insert(0, emptyItem);
                    return arr;
                },
                lsloadData: function ($combo, data, required) {
                    var sts = $combo.lsCombobox("options");
                    if (!required) {
                        data = lsMethods.pushEmptyItem(data, sts.valueField, sts.textField);
                    }
                    $combo.combobox("setValue", "");
                    return $combo.combobox("loadData", data);
                }
            };

            if (typeof (options) === 'string') {
                if (lsMethods[options]) return lsMethods[options]($(this), parm1, parm2);
                return $(this).combobox(options, parm1, parm2);
            }

            else if (typeof (options) === 'object')
                var defaults = {
                    valueField: 'id',
                    textField: 'text',
                    required: false,
                    data: [],
                    panelHeight: 150
                };
            var settings = $.extend(defaults, options);
            //输入可空添加空选项
            if (!settings.required) {
                settings.data = lsMethods.pushEmptyItem(settings.data, settings.valueField, settings.textField);
            }
            else {
                //增加验证属性
                if (settings.data.length > 0) settings.value = settings.data[0][settings.valueField]
            }
            //初始化
            return $(this).combobox(settings);
        },
        lsMultipleCombobox: function (options, parm1, parm2) {

            var lsMethods = {
                pushEmptyItem: function (arr, valueField, textField) {
                    var emptyItem = {};
                    emptyItem[valueField] = "";
                    emptyItem[textField] = "请选择";
                    arr.insert(0, emptyItem);
                    return arr;
                },
                lsloadData: function ($combo, data, required) {
                    var sts = $combo.lsCombobox("options");
                    if (!required) {
                        data = lsMethods.pushEmptyItem(data, sts.valueField, sts.textField);
                    }
                    $combo.combobox("setValue", "");
                    return $combo.combobox("loadData", data);
                }
            };

            if (typeof (options) === 'string') {
                if (lsMethods[options]) return lsMethods[options]($(this), parm1, parm2);
                return $(this).combobox(options, parm1, parm2);
            }

            else if (typeof (options) === 'object')
                var defaults = {
                    valueField: 'id',
                    textField: 'text',
                    required: false,
                    multiple: true,
                    data: [],
                    panelHeight: 150,
                    formatter: function (row) {
                        var opts = $(this).combobox('options');
                        return '<input type="checkbox" class="combobox-checkbox">' + row[opts.textField]
                    },
                    onShowPanel: function () {
                        var opts = $(this).combobox('options');
                        var target = this;
                        var values = $(target).combobox('getValues');
                        $.map(values, function (value) {
                            var el = opts.finder.getEl(target, value);
                            el.find('input.combobox-checkbox')._propAttr('checked', true);
                        })
                    },
                    onLoadSuccess: function () {
                        var opts = $(this).combobox('options');
                        var target = this;
                        var values = $(target).combobox('getValues');
                        $.map(values, function (value) {
                            var el = opts.finder.getEl(target, value);
                            el.find('input.combobox-checkbox')._propAttr('checked', true);
                        })
                    },
                    onSelect: function (row) {
                        //console.log(row);
                        var opts = $(this).combobox('options');
                        var el = opts.finder.getEl(this, row[opts.valueField]);
                        el.find('input.combobox-checkbox')._propAttr('checked', true);
                    },
                    onUnselect: function (row) {
                        var opts = $(this).combobox('options');
                        var el = opts.finder.getEl(this, row[opts.valueField]);
                        el.find('input.combobox-checkbox')._propAttr('checked', false);
                    }
                };
            var settings = $.extend(defaults, options);
            //输入可空添加空选项
            if (!settings.required) {
                //settings.data = lsMethods.pushEmptyItem(settings.data, settings.valueField, settings.textField);
            }
            else {
                //增加验证属性
                //if (settings.data.length > 0) settings.value = settings.data[0][settings.valueField]
            }
            //初始化
            return $(this).combobox(settings);
        },
        /*
         * lsCombobox:下拉树列表
         * 参数:options
         * 默认值:xxx
         */
        lsComboTree: function (options) {
            if (typeof (options) === 'string')
                return $(this).combotree(options);
            else if (typeof (options) === 'object')
                var defaults = {
                    required: false,
                    data: [],
                    //childComboes: {
                    //    url: '',
                    //    items: [
                    //        {
                    //            id: '',
                    //            type: 'combobox', //combobox,combotree
                    //            dataKey:''
                    //        }
                    //    ]
                    //}
                };
            var settings = $.extend(defaults, options);
            //输入可空添加空选项
            if (!settings.required) {
                var emptyItem = { id: "", text: "请选择", Attach: null };
                settings.data.insert(0, emptyItem);
            }
            else {
                //增加验证属性
            }

            //级联控件
            if (settings.childComboes) {
                //初始化
                var childComboes = [];
                for (var i = 0, len = settings.childComboes.items.length; i < len; i++) {
                    var combo = {};
                    combo = settings.childComboes.items[i].type == "combobox" ?
                        $("#" + settings.childComboes.items[i].id).lsCombobox({ data: settings.childComboes.items[i].data })
                        :
                        $("#" + settings.childComboes.items[i].id).lsCombobox({ data: settings.childComboes.items[i].data });
                    childComboes.push(combo);
                }
                //级联数据
                settings.onSelect = function (node) {
                    $.ls.ajax({
                        url: settings.childComboes.url + "?id=" + node.id,
                        onSuccess: function (content) {
                            for (var i = 0, len = childComboes.length; i < len; i++) {
                                if (!$.ls.isNullOrEmpty(childComboes[i].dataKey))
                                    childComboes[i].lsCombobox("lsloadData", content[childComboes[i].dataKey]);
                                else
                                    childComboes[i].lsCombobox("lsloadData", content);
                            }
                        }
                    })
                };
            }
            //初始化
            return $(this).combotree(settings);
        },
        /*
         * 头部菜单栏
         */
        lsHeaderMenu: function (options) {
            var defaults = {
                menus: []
            };
            var panelmv = false, panelShown = false;
            var $panel = $('<ul></ul>').addClass("ls-menu-panel").appendTo($('body').click(function () { $(".ls-menu-panel").fadeOut(100); panelShown = false; }));
            var settings = $.extend(defaults, options);
            for (var i = 0, len = settings.menus.length; i < len; i++) {
                $panel.append($('<li><i class="fa fa-' + settings.menus[i].icon + '"></i>' + settings.menus[i].text + '</li>').click(settings.menus[i].click));
            }
            $(this).click(function () {
                if (!panelShown) {
                    var leftOff = ($(this).offset().left + 120) > $('body').width() ? ($('body').width() - 122) : $(this).offset().left;
                    $panel.css("top", $(this).offset().top + $(this).height() - 2 + 'px')
                          .css("left", leftOff + 'px')
                          .fadeIn(100);
                    panelShown = true;
                }
                else {
                    $panel.fadeOut(100);
                    panelShown = false;
                }
                return false;
            });
        }
    });


    /*
     * ls公共控件 - windows
     * common contorls
     */
    $.ls = $.ls || {};
    $.extend($.ls, {
        showDialog: function (options) {
            var lsdialog,
                defaults = {
                    title: '对话框',
                    padding: 5,
                    url: '',
                    onClose: function (dialogResult) {

                    },
                    onclose: function () {
                        this.onClose(this.returnValue)
                    }
                };
            var settings = $.extend({}, defaults, options);
            var $hidPageId = $("#_hidPageId");
            if ($hidPageId) settings.url = this.setUrlParam(settings.url, "pageId", $hidPageId.val());
            lsdialog = top.dialog(settings);
            lsdialog.showModal();
            //对话框关闭
            lsdialog.Close = function (result) {
                lsdialog.close(result); // 关闭（隐藏）对话框
                lsdialog.remove();
            };
        },
        alert: function (msg, caption) {
            caption = caption || "消息";
            var defaults = {
                width: 300,
                height: 100,
                title: caption,
                content: msg,
                button: [
                {
                    value: '确定',
                    callback: function () {
                        return true;
                    },
                    autofocus: true
                }]
            };
            var lsdialog = top.dialog(defaults);
            lsdialog.showModal();
        },
        confirm: function (msg, callback, caption) {
            caption = caption || "消息";
            var defaults = {
                width: 300,
                height: 100,
                title: caption,
                content: msg,
                button: [
                {
                    value: '确定',
                    callback: function () {
                        callback(true);
                    },
                    autofocus: true
                },
                {
                    value: '取消',
                    callback: function () {
                        callback(false);
                    },
                    autofocus: false
                }]
            };
            var lsdialog = top.dialog(defaults);
            lsdialog.showModal();
        },
        loading: function (open, msg, timeout) {
            timeout = timeout || 6000;
            msg = msg || "请稍后...";
            if (!top.loading || !top.loading._popup) {
                var defaults = {
                    //content: '<div><span style="float:left;top:8px;" class="ui-dialog-loading"></span><span style="float:left;padding-left:5px;">' + msg + "</span></table>",
                };
                top.loading = top.dialog(defaults);
            }
            top.loading.content('<div><span style="float:left;top:8px;" class="ui-dialog-loading"></span><span style="float:left;padding-left:5px;">' + msg + "</span></table>");
            if (open) {
                top.loading.showModal();
                if (timeout > 0) {
                    setTimeout(function () {
                        top.loading.close();
                    }, timeout);
                }
            }
            else
                top.loading.close();
        },
        warning: function (msg, timeout) {
            timeout = timeout || 2000;
            msg = msg || "请稍后...";
            var defaults = {
                skin: "artdialog-warning",
                content: '<i class="fa fa-warning"</i>&nbsp;' + msg,
            };
            var d = top.dialog(defaults);
            d.show();
            setTimeout(function () {
                d.close().remove();
            }, timeout);
        },
        success: function (msg, timeout) {
            timeout = timeout || 2000;
            msg = msg || "请稍后...";
            var defaults = {
                skin: "artdialog-success",
                content: '<i class="fa fa-check-circle"</i>&nbsp;' + msg,
            };
            var d = top.dialog(defaults);
            d.show();
            setTimeout(function () {
                d.close().remove();
            }, timeout);
        }
    });
})(jQuery)