
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
         * lsIconBox:图标选择控件
         * 参数:options
         * 默认值:xxx
         */
        lsIconBox: function (options, parm1, parm2) {
            var icons = [
                'glass',
                'music',
                'search',
                'envelope-o',
                'heart',
                'star',
                'star-o',
                'user',
                'film',
                'th-large',
                'th',
                'th-list',
                'check',
                'remove',
                'close',
                'times',
                'search-plus',
                'search-minus',
                'power-off',
                'signal',
                'gear',
                'cog',
                'trash-o',
                'home',
                'file-o',
                'clock-o',
                'road',
                'download',
                'arrow-circle-o-down',
                'arrow-circle-o-up',
                'inbox',
                'play-circle-o',
                'rotate-right',
                'repeat',
                'refresh',
                'list-alt',
                'lock',
                'flag',
                'headphones',
                'volume-off',
                'volume-down',
                'volume-up',
                'qrcode',
                'barcode',
                'tag',
                'tags',
                'book',
                'bookmark',
                'print',
                'camera',
                'font',
                'bold',
                'italic',
                'text-height',
                'text-width',
                'align-left',
                'align-center',
                'align-right',
                'align-justify',
                'list',
                'dedent',
                'outdent',
                'indent',
                'video-camera',
                'photo',
                'image',
                'picture-o',
                'pencil',
                'map-marker',
                'adjust',
                'tint',
                'edit',
                'pencil-square-o',
                'share-square-o',
                'check-square-o',
                'arrows',
                'step-backward',
                'fast-backward',
                'backward',
                'play',
                'pause',
                'stop',
                'forward',
                'fast-forward',
                'step-forward',
                'eject',
                'chevron-left',
                'chevron-right',
                'plus-circle',
                'minus-circle',
                'times-circle',
                'check-circle',
                'question-circle',
                'info-circle',
                'crosshairs',
                'times-circle-o',
                'check-circle-o',
                'ban',
                'arrow-left',
                'arrow-right',
                'arrow-up',
                'arrow-down',
                'mail-forward',
                'share',
                'expand',
                'compress',
                'plus',
                'minus',
                'asterisk',
                'exclamation-circle',
                'gift',
                'leaf',
                'fire',
                'eye',
                'eye-slash',
                'warning',
                'exclamation-triangle',
                'plane',
                'calendar',
                'random',
                'comment',
                'magnet',
                'chevron-up',
                'chevron-down',
                'retweet',
                'shopping-cart',
                'folder',
                'folder-open',
                'arrows-v',
                'arrows-h',
                'bar-chart-o',
                'bar-chart',
                'twitter-square',
                'facebook-square',
                'camera-retro',
                'key',
                'gears',
                'cogs',
                'comments',
                'thumbs-o-up',
                'thumbs-o-down',
                'star-half',
                'heart-o',
                'sign-out',
                'linkedin-square',
                'thumb-tack',
                'external-link',
                'sign-in',
                'trophy',
                'github-square',
                'upload',
                'lemon-o',
                'phone',
                'square-o',
                'bookmark-o',
                'phone-square',
                'twitter',
                'facebook',
                'github',
                'unlock',
                'credit-card',
                'rss',
                'hdd-o',
                'bullhorn',
                'bell',
                'certificate',
                'hand-o-right',
                'hand-o-left',
                'hand-o-up',
                'hand-o-down',
                'arrow-circle-left',
                'arrow-circle-right',
                'arrow-circle-up',
                'arrow-circle-down',
                'globe',
                'wrench',
                'tasks',
                'filter',
                'briefcase',
                'arrows-alt',
                'group',
                'users',
                'chain',
                'link',
                'cloud',
                'flask',
                'cut',
                'scissors',
                'copy',
                'files-o',
                'paperclip',
                'save',
                'floppy-o',
                'square',
                'navicon',
                'reorder',
                'bars',
                'list-ul',
                'list-ol',
                'strikethrough',
                'underline',
                'table',
                'magic',
                'truck',
                'pinterest',
                'pinterest-square',
                'google-plus-square',
                'google-plus',
                'money',
                'caret-down',
                'caret-up',
                'caret-left',
                'caret-right',
                'columns',
                'unsorted',
                'sort',
              'sort-down',
            'sort-desc',
              'sort-up',
            'sort-asc',
              'envelope',
              'linkedin',
              'rotate-left',
            'undo',
              'legal',
            'gavel',
              'dashboard',
            'tachometer',
              'comment-o',
              'comments-o',
              'flash',
            'bolt',
              'sitemap',
              'umbrella',
              'paste',
            'clipboard',
              'lightbulb-o',
              'exchange',
              'cloud-download',
              'cloud-upload',
              'user-md',
              'stethoscope',
              'suitcase',
              'bell-o',
              'coffee',
              'cutlery',
              'file-text-o',
              'building-o',
              'hospital-o',
              'ambulance',
              'medkit',
              'fighter-jet',
              'beer',
              'h-square',
              'plus-square',
              'angle-double-left',
              'angle-double-right',
              'angle-double-up',
              'angle-double-down',
              'angle-left',
              'angle-right',
              'angle-up',
              'angle-down',
              'desktop',
              'laptop',
              'tablet',
              'mobile-phone',
            'mobile',
              'circle-o',
              'quote-left',
              'quote-right',
              'spinner',
              'circle',
              'mail-reply',
            'reply',
              'github-alt',
              'folder-o',
              'folder-open-o',
              'smile-o',
              'frown-o',
              'meh-o',
              'gamepad',
              'keyboard-o',
              'flag-o',
              'flag-checkered',
              'terminal',
              'code',
              'mail-reply-all',
            'reply-all',
              'star-half-empty',
            'star-half-full',
            'star-half-o',
              'location-arrow',
              'crop',
              'code-fork',
              'unlink',
            'chain-broken',
              'question',
              'info',
              'exclamation',
              'superscript',
              'subscript',
              'eraser',
              'puzzle-piece',
              'microphone',
              'microphone-slash',
              'shield',
              'calendar-o',
              'fire-extinguisher',
              'rocket',
              'maxcdn',
              'chevron-circle-left',
              'chevron-circle-right',
              'chevron-circle-up',
              'chevron-circle-down',
              'html5',
              'css3',
              'anchor',
              'unlock-alt',
              'bullseye',
              'ellipsis-h',
              'ellipsis-v',
              'rss-square',
              'play-circle',
              'ticket',
              'minus-square',
              'minus-square-o',
              'level-up',
              'level-down',
              'check-square',
              'pencil-square',
              'external-link-square',
              'share-square',
              'compass',
              'toggle-down',
            'caret-square-o-down',
              'toggle-up',
            'caret-square-o-up',
              'toggle-right',
            'caret-square-o-right',
              'euro',
            'eur',
              'gbp',
              'dollar',
            'usd',
              'rupee',
            'inr',
              'cny',
            'rmb',
            'yen',
            'jpy',
              'ruble',
            'rouble',
            'rub',
              'won',
            'krw',
              'bitcoin',
            'btc',
              'file',
              'file-text',
              'sort-alpha-asc',
              'sort-alpha-desc',
              'sort-amount-asc',
              'sort-amount-desc',
              'sort-numeric-asc',
              'sort-numeric-desc',
              'thumbs-up',
              'thumbs-down',
              'youtube-square',
              'youtube',
              'xing',
              'xing-square',
              'youtube-play',
              'dropbox',
              'stack-overflow',
              'instagram',
              'flickr',
              'adn',
              'bitbucket',
              'bitbucket-square',
              'tumblr',
              'tumblr-square',
              'long-arrow-down',
              'long-arrow-up',
              'long-arrow-left',
              'long-arrow-right',
              'apple',
              'windows',
              'android',
              'linux',
              'dribbble',
              'skype',
              'foursquare',
              'trello',
              'female',
              'male',
              'gittip',
              'sun-o',
              'moon-o',
              'archive',
              'bug',
              'vk',
              'weibo',
              'renren',
              'pagelines',
              'stack-exchange',
              'arrow-circle-o-right',
              'arrow-circle-o-left',
              'toggle-left',
            'caret-square-o-left',
              'dot-circle-o',
              'wheelchair',
              'vimeo-square',
              'turkish-lira',
            'try',
              'plus-square-o',
              'space-shuttle',
              'slack',
              'envelope-square',
              'wordpress',
              'openid',
              'institution',
            'bank',
            'university',
              'mortar-board',
            'graduation-cap',
              'yahoo',
              'google',
              'reddit',
              'reddit-square',
              'stumbleupon-circle',
              'stumbleupon',
              'delicious',
              'digg',
              'pied-piper',
              'pied-piper-alt',
              'drupal',
              'joomla',
              'language',
              'fax',
              'building',
              'child',
              'paw',
              'spoon',
              'cube',
              'cubes',
              'behance',
              'behance-square',
              'steam',
              'steam-square',
              'recycle',
              'automobile',
            'car',
              'cab',
            'taxi',
              'tree',
              'spotify',
              'deviantart',
              'soundcloud',
              'database',
              'file-pdf-o',
              'file-word-o',
              'file-excel-o',
              'file-powerpoint-o',
              'file-photo-o',
            'file-picture-o',
            'file-image-o',
              'file-zip-o',
            'file-archive-o',
              'file-sound-o',
            'file-audio-o',
              'file-movie-o',
            'file-video-o',
              'file-code-o',
              'vine',
              'codepen',
              'jsfiddle',
              'life-bouy',
            'life-buoy',
            'life-saver',
            'support',
            'life-ring',
              'circle-o-notch',
              'ra',
            'rebel',
              'ge',
            'empire',
              'git-square',
              'git',
              'hacker-news',
              'tencent-weibo',
              'qq',
              'wechat',
            'weixin',
              'send',
            'paper-plane',
              'send-o',
            'paper-plane-o',
              'history',
              'circle-thin',
              'header',
              'paragraph',
              'sliders',
              'share-alt',
              'share-alt-square',
              'bomb',
              'soccer-ball-o',
            'futbol-o',
              'tty',
              'binoculars',
              'plug',
              'slideshare',
              'twitch',
              'yelp',
              'newspaper-o',
              'wifi',
              'calculator',
              'paypal',
              'google-wallet',
              'cc-visa',
              'cc-mastercard',
              'cc-discover',
              'cc-amex',
              'cc-paypal',
              'cc-stripe',
              'bell-slash',
              'bell-slash-o',
              'trash',
              'copyright',
              'at',
              'eyedropper',
              'paint-brush',
              'birthday-cake',
              'area-chart',
              'pie-chart',
              'line-chart',
              'lastfm',
              'lastfm-square',
              'toggle-off',
              'toggle-on',
              'bicycle',
              'bus',
              'ioxhost',
              'angellist',
              'cc',
              'shekel',
            'sheqel',
            'ils',
              'meanpath'
            ];
            var $content = $('<div style="height: 480px;overflow-y:scroll;"></div>');
            $input = $(this).attr('readonly', 'readonly');
            for (var i = 0; i < icons.length; i++) {
                $content.append('<i class="fa fa-' + icons[i] + ' ls-icon-item" value="fa-' + icons[i] + '"></i>');
            }
            var $btn = $('<i class="fa fa-list ls-btn-icon-sl"></i>').click(function () {
                top.dg_icon_sel = $.ls.showDialog({
                    title: '请选择图标',
                    height: 500,
                    width: 500,
                    content: $content,
                });
                $content.find('i').click(function (e) {
                    if ($input.hasClass('easyui-textbox'))
                        $input.textbox('setValue', $(e.target).attr('value'));
                    else
                        $input.val($(e.target).attr('value'));
                    top.dg_icon_sel.Close();
                })
            }),
            $container = $('<div style="position:relative;"></div>');
            $input.wrap($container);
            $btn.insertBefore($input);
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
            if ($hidPageId && settings.url)
                settings.url = this.setUrlParam(settings.url, "pageId", $hidPageId.val());
            lsdialog = top.dialog(settings);
            lsdialog.showModal();
            //对话框关闭
            lsdialog.Close = function (result) {
                lsdialog.close(result); // 关闭（隐藏）对话框
                lsdialog.remove();
            };
            return lsdialog;
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